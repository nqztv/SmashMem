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
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
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

		[DllImport("USER32.DLL")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		private string _setOutputButtonText = @"C:\Users\nqztv\Desktop\Smash\Stream\browserfiles\assets\streamcontrol.json";
		public string SetOutputButtonText
		{
			get { return _setOutputButtonText; }
			set { SetProperty(ref _setOutputButtonText, value); }
		}

		private string _outputFile = @"C:\Users\nqztv\Desktop\Smash\Stream\browserfiles\assets\streamcontrol.json";
		public string OutputFile
		{
			get { return _outputFile; }
			set { SetProperty(ref _outputFile, value); }
		}

		private string _header1 = "Winner's R1";
		public string Header1
		{
			get { return _header1; }
			set { SetProperty(ref _header1, value); }
		}

		private string _header2 = "";
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

		private bool _getCharacters = true;
		public bool GetCharacters
		{
			get { return _getCharacters; }
			set { SetProperty(ref _getCharacters, value); }
		}

		private bool _incrementScore = true;
		public bool IncrementScore
		{
			get { return _incrementScore; }
			set { SetProperty(ref _incrementScore, value); }
		}

		private bool _decrementScore = false;
		public bool DecrementScore
		{
			get { return _decrementScore; }
			set { SetProperty(ref _decrementScore, value); }
		}

		private bool _fireGameStartHotkey = true;
		public bool FireGameStartHotkey
		{
			get { return _fireGameStartHotkey; }
			set { SetProperty(ref _fireGameStartHotkey, value); }
		}

		private bool _fireGameEndHotkey = true;
		public bool FireGameEndHotkey
		{
			get { return _fireGameEndHotkey; }
			set { SetProperty(ref _fireGameEndHotkey, value); }
		}

		private ObservableCollection<String> _processes = new ObservableCollection<string>();
		public ObservableCollection<String> Processes
		{
			get { return _processes; }
			set { SetProperty(ref _processes, value); }
		}

		private string _selectedProcess = @"OBS 22.0.2 (64-bit, windows) - Profile: Untitled - Scenes: Melee";
		public string SelectedProcess
		{
			get { return _selectedProcess; }
			set { SetProperty(ref _selectedProcess, value); }
		}

		public DelegateCommand SetOutputCommand { get; set; }
		public DelegateCommand UpdateCommand { get; set; }
		public DelegateCommand ResetScoreCommand { get; set; }
		public DelegateCommand SwapCommand { get; set; }
		public DelegateCommand IncrementPlayer1ScoreCommand { get; set; }
		public DelegateCommand DecrementPlayer1ScoreCommand { get; set; }
		public DelegateCommand IncrementPlayer2ScoreCommand { get; set; }
		public DelegateCommand DecrementPlayer2ScoreCommand { get; set; }
		public DelegateCommand ChangePort1Command { get; set; }
		public DelegateCommand ChangePort2Command { get; set; }
		public DelegateCommand GetProcessesCommand { get; set; }

		public StreamViewModel(IEventAggregator eventAggregator)
		{
			eventAggregator.GetEvent<Port1CharacterChangedEvent>().Subscribe(ChangePort1Character);
			eventAggregator.GetEvent<Port2CharacterChangedEvent>().Subscribe(ChangePort2Character);
			eventAggregator.GetEvent<Port3CharacterChangedEvent>().Subscribe(ChangePort3Character);
			eventAggregator.GetEvent<Port4CharacterChangedEvent>().Subscribe(ChangePort4Character);
			eventAggregator.GetEvent<GameStartEvent>().Subscribe(GameStart);
			eventAggregator.GetEvent<GameEndEvent>().Subscribe(GameEnd);

			SetOutputCommand = new DelegateCommand(SetOutput, CanSetOutput);
			UpdateCommand = new DelegateCommand(Update, CanUpdate);
			ResetScoreCommand = new DelegateCommand(ResetScore, CanResetScore).ObservesProperty(() => Player1Score).ObservesProperty(() => Player2Score);
			SwapCommand = new DelegateCommand(Swap, CanSwap);
			IncrementPlayer1ScoreCommand = new DelegateCommand(IncrementPlayer1Score, CanIncrementPlayer1Score).ObservesProperty(() => Player1Score);
			DecrementPlayer1ScoreCommand = new DelegateCommand(DecrementPlayer1Score, CanDecrementPlayer1Score).ObservesProperty(() => Player1Score);
			IncrementPlayer2ScoreCommand = new DelegateCommand(IncrementPlayer2Score, CanIncrementPlayer2Score).ObservesProperty(() => Player2Score);
			DecrementPlayer2ScoreCommand = new DelegateCommand(DecrementPlayer2Score, CanDecrementPlayer2Score).ObservesProperty(() => Player2Score);
			GetProcessesCommand = new DelegateCommand(GetProcesses, CanGetProcesses);
		}

		private void ChangePort1Character(string obj)
		{
			if (GetCharacters)
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
		}

		private void ChangePort2Character(string obj)
		{
			if (GetCharacters)
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
		}

		private void ChangePort3Character(string obj)
		{
			if (GetCharacters)
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
		}

		private void ChangePort4Character(string obj)
		{
			if (GetCharacters)
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
		}

		private void GameStart(string obj)
		{
			if (FireGameStartHotkey)
			{
				IntPtr processHandle = FindWindow(null, SelectedProcess);
				if (processHandle != IntPtr.Zero)
				{
					SetForegroundWindow(processHandle);
					System.Threading.Thread.Sleep(15);
					SendKeys.SendWait("{F7}");
				}
			}
		}

		private void GameEnd(string obj)
		{
			if (IncrementScore)
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

				Update();

				if (FireGameEndHotkey && (Player1Score > (GameCountLimit / 2) || Player2Score > (GameCountLimit / 2)))
				{
					IntPtr processHandle = FindWindow(null, SelectedProcess);
					if (processHandle != IntPtr.Zero)
					{
						SetForegroundWindow(processHandle);
						System.Threading.Thread.Sleep(15);
						SendKeys.SendWait("{F8}");
					}
				}
			}
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
			}
		}

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

			if (Header1.Contains("Finals") || Header1.Contains("Loser's Semis"))
			{
				GameCountLimit = 5;
			}

			File.WriteAllText(OutputFile, output);
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

		private bool CanIncrementPlayer1Score()
		{
			return Player1Score < 99;
		}

		private void IncrementPlayer1Score()
		{
			Player1Score++;
		}

		private bool CanDecrementPlayer1Score()
		{
			return Player1Score > 0;
		}

		private void DecrementPlayer1Score()
		{
			Player1Score--;
		}

		private bool CanIncrementPlayer2Score()
		{
			return Player2Score < 99;
		}

		private void IncrementPlayer2Score()
		{
			Player2Score++;
		}

		private bool CanDecrementPlayer2Score()
		{
			return Player2Score > 0;
		}

		private void DecrementPlayer2Score()
		{
			Player2Score--;
		}

		private bool CanGetProcesses()
		{
			return true;
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
	}
}
