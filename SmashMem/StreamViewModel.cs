using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmashMem
{
	//[DllImport("User32.dll")]
	//static extern int SetForegroundWindow(IntPtr point);

	
	public enum Port
	{
		[Description("Port 1 (Red)")]
		Port1,
		[Description("Port 2 (Blue)")]
		Port2,
		[Description("Port 3 (Yellow)")]
		Port3,
		[Description("Port 4 (Green)")]
		Port4
	}
	public class StreamViewModel : BindableBase
	{
		[DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		// Activate an application window.
		[DllImport("USER32.DLL")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[TypeConverter(typeof(EnumDescriptionTypeConverter))]
		private string _outputFile;
		public string OutputFile
		{
			get { return _outputFile; }
			set { SetProperty(ref _outputFile, value); }
		}

		private string _setOutputButtonText = "Set Output File";
		public string SetOutputButtonText
		{
			get { return _setOutputButtonText; }
			set { SetProperty(ref _setOutputButtonText, value); }
		}

		private string _selectedProcess;
		public string SelectedProcess
		{
			get { return _selectedProcess; }
			set { SetProperty(ref _selectedProcess, value); }
		}

		private ObservableCollection<String> _processes = new ObservableCollection<string>();
		public ObservableCollection<String> Processes
		{
			get { return _processes; }
			set { SetProperty(ref _processes, value); }
		}

		private string _header1 = "Winner's R1";
		public string Header1
		{
			get { return _header1; }
			set { SetProperty(ref _header1, value); }
		}

		private string _header2 = "test";
		public string Header2
		{
			get { return _header2; }
			set { SetProperty(ref _header2, value); }
		}

		private string _player1Name = "Player 1";
		public string Player1Name
		{
			get { return _player1Name; }
			set { SetProperty(ref _player1Name, value); }
		}

		private string _player2Name = "Player 2";
		public string Player2Name
		{
			get { return _player2Name; }
			set { SetProperty(ref _player2Name, value); }
		}

		private int _player1Score = 0;
		public int Player1Score
		{
			get { return _player1Score; }
			set { SetProperty(ref _player1Score, value); }
		}

		private int _player2Score = 0;
		public int Player2Score
		{
			get { return _player2Score; }
			set { SetProperty(ref _player2Score, value); }
		}

		private Port _player1Port = Port.Port1;
		public Port Player1Port
		{
			get { return _player1Port; }
			set { SetProperty(ref _player1Port, value); }
		}

		private Port _player2Port = Port.Port2;
		public Port Player2Port
		{
			get { return _player2Port; }
			set { SetProperty(ref _player2Port, value); }
		}

		private string _player1Character = "Player1Character";
		public string Player1Character
		{
			get { return _player1Character; }
			set { SetProperty(ref _player1Character, value); }
		}

		private string _player2Character = "Player2Character";
		public string Player2Character
		{
			get { return _player2Character; }
			set { SetProperty(ref _player2Character, value); }
		}

		private int _gameCountLimit = 3;
		public int GameCountLimit
		{
			get { return _gameCountLimit; }
			set { SetProperty(ref _gameCountLimit, value); }
		}

		//private bool booleanTrue = true;
		//public bool BooleanTrue
		//{
		//	get { return booleanTrue; }
		//	set { SetProperty(ref booleanTrue, value); }
		//}

		public DelegateCommand IncrementPlayer1ScoreCommand { get; set; }
		public DelegateCommand DecrementPlayer1ScoreCommand { get; set; }
		public DelegateCommand IncrementPlayer2ScoreCommand { get; set; }
		public DelegateCommand DecrementPlayer2ScoreCommand { get; set; }
		public DelegateCommand ResetScoreCommand { get; set; }
		public DelegateCommand UpdateCommand { get; set; }
		public DelegateCommand SwapCommand { get; set; }
		public DelegateCommand SetOutputCommand { get; set; }
		public DelegateCommand ChangePort1Command { get; set; }
		public DelegateCommand ChangePort2Command { get; set; }
		public DelegateCommand GetProcessesCommand { get; set; }

		public StreamViewModel(IEventAggregator eventAggregator)
		{
			eventAggregator.GetEvent<Port1CharacterChangedEvent>().Subscribe(ChangePort1Character);
			eventAggregator.GetEvent<Port2CharacterChangedEvent>().Subscribe(ChangePort2Character);
			eventAggregator.GetEvent<Port3CharacterChangedEvent>().Subscribe(ChangePort3Character);
			eventAggregator.GetEvent<Port4CharacterChangedEvent>().Subscribe(ChangePort4Character);
			eventAggregator.GetEvent<GameEndEvent>().Subscribe(GameEnd);
			eventAggregator.GetEvent<GameStartEvent>().Subscribe(GameStart);


			IncrementPlayer1ScoreCommand = new DelegateCommand(IncrementPlayer1Score, CanIncrementPlayer1Score).ObservesProperty(() => Player1Score);
			DecrementPlayer1ScoreCommand = new DelegateCommand(DecrementPlayer1Score, CanDecrementPlayer1Score).ObservesProperty(() => Player1Score);
			IncrementPlayer2ScoreCommand = new DelegateCommand(IncrementPlayer2Score, CanIncrementPlayer2Score).ObservesProperty(() => Player2Score);
			DecrementPlayer2ScoreCommand = new DelegateCommand(DecrementPlayer2Score, CanDecrementPlayer2Score).ObservesProperty(() => Player2Score);
			ResetScoreCommand = new DelegateCommand(ResetScore, CanResetScore).ObservesProperty(() => Player1Score).ObservesProperty(() => Player2Score);
			UpdateCommand = new DelegateCommand(Update, CanUpdate);
			SwapCommand = new DelegateCommand(Swap, CanSwap);
			SetOutputCommand = new DelegateCommand(SetOutput, CanSetOutput);
			GetProcessesCommand = new DelegateCommand(GetProcesses, CanGetProcesses);
			//ChangePort1Command = new DelegateCommand(ChangePort1).ObservesCanExecute(() => BooleanTrue);
			//ChangePort2Command = new DelegateCommand(ChangePort2).ObservesCanExecute(() => BooleanTrue);

		}

		private void GetProcesses()
		{
			Process[] localAll = Process.GetProcesses();
			string[] processNames = new string[localAll.Length];

			for (var i = 0; i < localAll.Length; i++)
			{
				processNames[i] = localAll[i].MainWindowTitle;
			}

			processNames = processNames.Where(x => !string.IsNullOrEmpty(x)).ToArray();

			Processes = new ObservableCollection<String>(processNames);
		}

		private bool CanGetProcesses()
		{
			return true;
		}

		private void GameEnd(string obj)
		{
			if (obj == "00")
			{
				if (Player1Port == Port.Port1)
				{
					Player1Score++;
				}
				if (Player2Port == Port.Port1)
				{
					Player2Score++;
				}
			}

			if (obj == "01")
			{
				if (Player1Port == Port.Port2)
				{
					Player1Score++;
				}
				if (Player2Port == Port.Port2)
				{
					Player2Score++;
				}
			}

			if (obj == "02")
			{
				if (Player1Port == Port.Port3)
				{
					Player1Score++;
				}
				if (Player2Port == Port.Port3)
				{
					Player2Score++;
				}
			}

			if (obj == "03")
			{
				if (Player1Port == Port.Port4)
				{
					Player1Score++;
				}
				if (Player2Port == Port.Port4)
				{
					Player2Score++;
				}
			}

			if (Player1Score > (GameCountLimit / 2) || Player2Score > (GameCountLimit / 2))
			{
				IntPtr wordHandle = FindWindow(null, SelectedProcess);
				if (wordHandle != IntPtr.Zero)
				{
					SetForegroundWindow(wordHandle);
					System.Threading.Thread.Sleep(100);
					SendKeys.SendWait("{F8}");
					SendKeys.Flush();
				}
			}

			Update();

			//Header2 = obj;
		}

		private void GameStart(string obj)
		{
			if (Player1Score == 0 && Player2Score == 0)
			{
				IntPtr wordHandle = FindWindow(null, SelectedProcess);
				if (wordHandle != IntPtr.Zero)
				{
					SetForegroundWindow(wordHandle);
					System.Threading.Thread.Sleep(100);
					SendKeys.SendWait("{F7}");
					SendKeys.Flush();
				}
			}
			//Process p = Process.GetProcessesByName("test").FirstOrDefault();
			//Header2 = obj;
			//Update();
		}

		private void ChangePort1Character(string obj)
		{
			if (Player1Port == Port.Port1)
			{
				Player1Character = obj;
			}

			if (Player2Port == Port.Port1)
			{
				Player2Character = obj;
			}

			Update();
		}

		private void ChangePort2Character(string obj)
		{
			if (Player1Port == Port.Port2)
			{
				Player1Character = obj;
			}

			if (Player2Port == Port.Port2)
			{
				Player2Character = obj;
			}

			Update();
		}

		private void ChangePort3Character(string obj)
		{
			if (Player1Port == Port.Port3)
			{
				Player1Character = obj;
			}

			if (Player2Port == Port.Port3)
			{
				Player2Character = obj;
			}

			Update();
		}

		private void ChangePort4Character(string obj)
		{
			if (Player1Port == Port.Port4)
			{
				Player1Character = obj;
			}

			if (Player2Port == Port.Port4)
			{
				Player2Character = obj;
			}

			Update();
		}


		//private void ChangePort2(Port radioSelection)
		//{
		//	Player2Port = radioSelection;
		//}

		//private void ChangePort1(Port radioSelection)
		//{
		//	Player1Port = radioSelection;
		//}

		private bool CanUpdate()
		{
			return true;
		}

		private void Update()
		{
			string json = File.ReadAllText(OutputFile);
			dynamic jsonObj = JsonConvert.DeserializeObject(json);
			jsonObj["header1"] = Header1;
			jsonObj["header2"] = Header2;
			jsonObj["player1GamerTag"] = Player1Name;
			jsonObj["player2GamerTag"] = Player2Name;
			jsonObj["player1Score"] = Player1Score.ToString(); // if not tostring, no quotes will be around number and both numbers will update :-/
			jsonObj["player2Score"] = Player2Score.ToString();
			jsonObj["player1Port"] = Player1Port.ToString();
			jsonObj["player2Port"] = Player2Port.ToString();
			jsonObj["player1Character"] = Player1Character;
			jsonObj["player2Character"] = Player2Character;
			string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(OutputFile, output);
		}

		private bool CanSetOutput()
		{
			return true;
		}

		private void SetOutput()
		{
			// create OpenFileDialog.
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			// set filter for file extension and default file extension to show only csv files.
			dlg.DefaultExt = ".json";
			dlg.Filter = "JSON Files (*.json)|*.json";

			// display OpenFileDialog.
			Nullable<bool> result = dlg.ShowDialog();

			// check if user selected a file. 
			if (result == true)
			{
				// update components.
				OutputFile = dlg.FileName;
				SetOutputButtonText = OutputFile;
				//MemoryAddresses = MemoryAddressService.CollectFromCSV(InputCSV);
				//dbgMemoryAddresses.ItemsSource = memoryAddresses;
				//txtStatus.Text = inputCSV + " opened as input.";
			}
		}

		private bool CanSwap()
		{
			return true;
		}

		private void Swap()
		{
			string temp = Player1Name;
			Player1Name = Player2Name;
			Player2Name = temp;

			Update();
		}

		private bool CanResetScore()
		{
			return !(Player1Score == 0 && Player2Score == 0);
		}

		private void ResetScore()
		{
			Player1Score = 0;
			Player2Score = 0;

			Update();
		}

		private bool CanDecrementPlayer2Score()
		{
			return Player2Score > 0;
		}

		private void DecrementPlayer2Score()
		{
			Player2Score--;
		}

		private bool CanIncrementPlayer2Score()
		{
			return Player2Score < 99;
		}

		private void IncrementPlayer2Score()
		{
			Player2Score++;
		}

		private bool CanDecrementPlayer1Score()
		{
			return Player1Score > 0;
		}

		private void DecrementPlayer1Score()
		{
			Player1Score--;
		}

		private bool CanIncrementPlayer1Score()
		{
			return Player1Score < 99;
		}

		private void IncrementPlayer1Score()
		{
			Player1Score++;
		}
	}
}
