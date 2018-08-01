using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashMem
{
	public class MemoryAddressModel : BindableBase
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}

		private string _type;
		public string Type
		{
			get { return _type; }
			set { SetProperty(ref _type, value); }
		}

		private string _address;
		public string Address
		{
			get { return _address; }
			set { SetProperty(ref _address, value); }
		}

		private string _offset;
		public string Offset
		{
			get { return _offset; }
			set { SetProperty(ref _offset, value); }
		}

		private uint _length;
		public uint Length
		{
			get { return _length; }
			set { SetProperty(ref _length, value); }
		}

		private string _hexResult;
		public string HexResult
		{
			get { return _hexResult; }
			set { SetProperty(ref _hexResult, value); }
		}

		private string _convertedResult;
		public string ConvertedResult
		{
			get { return _convertedResult; }
			set { SetProperty(ref _convertedResult, value); }
		}

		private string _desiredResult;
		public string DesiredResult
		{
			get { return _desiredResult; }
			set { SetProperty(ref _desiredResult, value); }
		}

		private long _timeCost;
		public long TimeCost
		{
			get { return _timeCost; }
			set { SetProperty(ref _timeCost, value); }
		}

		private bool _doPeek;
		public bool DoPeek
		{
			get { return _doPeek; }
			set { SetProperty(ref _doPeek, value); }
		}

		private bool _doPoke;
		public bool DoPoke
		{
			get { return _doPoke; }
			set { SetProperty(ref _doPoke, value); }
		}
	}

	public static class MemoryAddressService
	{
		public static ObservableCollection<MemoryAddressModel> CollectFromCSV(string filePath)
		{
			// grab each row from a csv file.
			string[] rows = File.ReadAllLines(filePath);

			// convert each row to a MemoryAddress object.
			var data = from row in rows.Skip(1)
								 let column = row.Split(',')
								 select new MemoryAddressModel
								 {
									 Name = column[0],
									 Type = column[1],
									 Address = column[2],
									 Offset = column[3],
									 Length = uint.Parse(column[4]),
									 HexResult = column[5],
									 ConvertedResult = column[6],
									 DesiredResult = column[7],
									 TimeCost = long.Parse(column[8]),
									 DoPeek = bool.Parse(column[9]),
									 DoPoke = bool.Parse(column[10])
								 };

			// convert linq query to an ObservableCollection.
			ObservableCollection<MemoryAddressModel> oc = new ObservableCollection<MemoryAddressModel>(data);

			// return the ObservableCollection.
			return oc;
		}
	}
}
