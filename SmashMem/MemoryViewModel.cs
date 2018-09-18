using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmashMem
{
	public class MemoryViewModel : BindableBase
	{
		USBGeckoModel gecko = new USBGeckoModel();
		private readonly IEventAggregator _eventAggregator;
		CancellationTokenSource cancellationTokenSource;

		Dictionary<string, string> currentResult = new Dictionary<string, string>();
		Dictionary<string, string> previousResult = new Dictionary<string, string>();

		private ObservableCollection<MemoryAddressModel> _memoryAddresses;
		public ObservableCollection<MemoryAddressModel> MemoryAddresses
		{
			get { return _memoryAddresses; }
			set { SetProperty(ref _memoryAddresses, value); }
		}

		private bool _geckoConnected = false;
		public bool GeckoConnected
		{
			get { return _geckoConnected; }
			set { SetProperty(ref _geckoConnected, value); }
		}

		private bool _autoPeek = false;
		public bool AutoPeek
		{
			get { return _autoPeek; }
			set { SetProperty(ref _autoPeek, value); }
		}

		private int _interval = 250;
		public int Interval
		{
			get { return _interval; }
			set { SetProperty(ref _interval, value); }
		}

		private string _inputCSV = "input.csv";
		public string InputCSV
		{
			get { return _inputCSV; }
			set { SetProperty(ref _inputCSV, value); }
		}

		private string _outputJSON;
		public string OutputJSON
		{
			get { return _outputJSON; }
			set { SetProperty(ref _outputJSON, value); }
		}

		public DelegateCommand ToggleConnectionCommand { get; set; }
		public DelegateCommand SetInputCommand { get; set; }
		public DelegateCommand PeekCommand { get; set; }
		public DelegateCommand PokeCommand { get; set; }
		public DelegateCommand ToggleAutoPeekCommand { get; set; }

		public MemoryViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			ToggleConnectionCommand = new DelegateCommand(ToggleConnection, CanToggleConnection);
			SetInputCommand = new DelegateCommand(SetInput, CanSetInput);
			PeekCommand = new DelegateCommand(Peek, CanPeek).ObservesProperty(() => GeckoConnected).ObservesProperty(() => InputCSV);
			PokeCommand = new DelegateCommand(Poke, CanPoke).ObservesProperty(() => GeckoConnected).ObservesProperty(() => InputCSV);
			ToggleAutoPeekCommand = new DelegateCommand(ToggleAutoPeek, CanToggleAutoPeek).ObservesProperty(() => GeckoConnected).ObservesProperty(() => InputCSV);

            if (InputCSV != "")
            {
                MemoryAddresses = MemoryAddressService.CollectFromCSV(InputCSV);
            }
        }

		private bool CanToggleConnection()
		{
			return true;
		}

		private void ToggleConnection()
		{
			if (GeckoConnected == true)
			{
				gecko.Connect();
            }
            else
			{
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				AutoPeek = false;
				gecko.Disconnect();
			}
		}

		private bool CanToggleAutoPeek()
		{
			return !String.IsNullOrWhiteSpace(InputCSV) && GeckoConnected;
		}

		private void ToggleAutoPeek()
		{
			if (AutoPeek == true)
			{
				// set cancellation token for task.
				cancellationTokenSource = new CancellationTokenSource();
				var token = cancellationTokenSource.Token;

				// start task to repeatedly peek.
				var listener = Task.Factory.StartNew(() =>
				{
					while (true)
					{
						Peek();
						Thread.Sleep(Interval);
						if (token.IsCancellationRequested) { break; }
					}
				}, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			}
			else
			{
				// cancel task to repeatedly poke.
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}

		}

		private bool CanPoke()
		{
			return !String.IsNullOrWhiteSpace(InputCSV) && GeckoConnected;
		}

		private void Poke()
		{
			// cycle through each MemoryAddress in collection.
			foreach (MemoryAddressModel address in MemoryAddresses)
			{
				// skip current address if not checked to peek.
				if (!address.DoPoke)
				{
					continue;
				}

				// skip current address if not acceptable length.
				if (address.Length != 1 && address.Length != 2 && address.Length != 4)
				{
					//txtStatus.Text = "poke length must be 1, 2, or 4.";
					continue;
				}

				// start timer to determing time cost.
				Stopwatch watch = new Stopwatch();
				watch.Start();

				// try to poke the current address.
				try
				{
					uint addressAsUInt = Convert.ToUInt32(address.Address, 16);
					uint dataAsUint = Convert.ToUInt32(address.DesiredResult, 16);
					byte[] response;

					if (address.Offset == "")
					{
						gecko.poke(addressAsUInt, address.Length, dataAsUint);
					}
					else
					{
						response = gecko.peek(addressAsUInt, 4);
						Array.Reverse(response);
						addressAsUInt = BitConverter.ToUInt32(response, 0);
						addressAsUInt += Convert.ToUInt32(address.Offset, 16);
						gecko.poke(addressAsUInt, address.Length, dataAsUint);
					}
				}
				catch (Exception)
				{
					address.HexResult = "UNABLE TO POKE!";
				}

				// stop timer and update time cost.
				watch.Stop();
				address.TimeCost = watch.ElapsedMilliseconds;
			}
		}

		private bool CanPeek()
		{
			return !String.IsNullOrWhiteSpace(InputCSV) && GeckoConnected;
		}

		private void Peek()
		{
			// declare output for json.
			//Dictionary<string, string> output = new Dictionary<string, string>();

			// cycle through each MemoryAddress in collection.
			foreach (MemoryAddressModel address in MemoryAddresses)
			{
				// skip current address if not checked to peek.
				if (!address.DoPeek)
				{
					continue;
				}

				// start timer to determing time cost.
				Stopwatch watch = new Stopwatch();
				watch.Start();

				// try to peek the current address.
				try
				{
					uint addressAsUInt = Convert.ToUInt32(address.Address, 16);
					byte[] response;

					if (address.Offset == "")
					{
						response = gecko.peek(addressAsUInt, address.Length);
					}
					else
					{
						response = gecko.peek(addressAsUInt, 4);
						Array.Reverse(response);
						addressAsUInt = BitConverter.ToUInt32(response, 0);
						addressAsUInt += Convert.ToUInt32(address.Offset, 16);
						response = gecko.peek(addressAsUInt, address.Length);
					}

					address.HexResult = BitConverter.ToString(response).Replace("-", "");
					address.ConvertedResult = Tools.ConvertResult(response, address.Type);
				}
				catch (Exception)
				{
					address.HexResult = "UNABLE TO PEEK!";
				}

				// stop timer and update time cost.
				watch.Stop();
				address.TimeCost = watch.ElapsedMilliseconds;

				if (address.TimeCost < 100 && address.HexResult != "00") // if gecko doesn't timeout
				{
					currentResult[address.Name] = address.ConvertedResult.ToString();

					if (!previousResult.ContainsKey(address.Name))
					{
						previousResult[address.Name] = currentResult[address.Name];
					}

					if (currentResult[address.Name] != previousResult[address.Name])
					{
						if (address.Name == "P1Character")
						{
							_eventAggregator.GetEvent<Port1CharacterChangedEvent>().Publish(address.HexResult.ToString());
						}

						if (address.Name == "P2Character")
						{
							_eventAggregator.GetEvent<Port2CharacterChangedEvent>().Publish(address.HexResult.ToString());
						}

						if (address.Name == "P3Character")
						{
							_eventAggregator.GetEvent<Port3CharacterChangedEvent>().Publish(address.HexResult.ToString());
						}

						if (address.Name == "P4Character")
						{
							_eventAggregator.GetEvent<Port4CharacterChangedEvent>().Publish(address.HexResult.ToString());
						}

						if (address.Name == "GameWinner")
						{
							if (currentResult[address.Name].Substring(0, 8) == "00000300")
							{
								_eventAggregator.GetEvent<GameEndEvent>().Publish(currentResult[address.Name].Substring(8, 2));
							}
						}

						if (address.Name == "MatchFrameCount")
						{
							if (Convert.ToInt32(currentResult[address.Name]) < Convert.ToInt32(previousResult[address.Name]))
							{
								_eventAggregator.GetEvent<GameStartEvent>().Publish("GAMESTART");
							}
						}
					}
				}

				previousResult[address.Name] = currentResult[address.Name];

				// add result to output json.
				//output.Add(address.Name, address.ConvertedResult);
			}

			// write json to file.
			//File.WriteAllText(outputJSON, JsonConvert.SerializeObject(output, Formatting.Indented));

		}

		private bool CanSetInput()
		{
			return true;
		}

		private void SetInput()
		{
			// create OpenFileDialog.
			OpenFileDialog dlg = new OpenFileDialog();

			// set filter for file extension and default file extension to show only csv files.
			dlg.DefaultExt = ".csv";
			dlg.Filter = "CSV Files (*.csv)|*.csv";

			// display OpenFileDialog.
			Nullable<bool> result = dlg.ShowDialog();

			// check if user selected a file. 
			if (result == true)
			{
				// update components.
				InputCSV = dlg.FileName;
				//Title = InputCSV;
				MemoryAddresses = MemoryAddressService.CollectFromCSV(InputCSV);
				//dbgMemoryAddresses.ItemsSource = memoryAddresses;
				//txtStatus.Text = inputCSV + " opened as input.";
			}
		}
	}
}
