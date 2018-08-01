using FTD2XX_NET;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashMem
{
	public class USBGeckoModel : BindableBase
	{
		FTDI ftdiDevice = new FTDI();
		FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

		private bool _isConnected = false;
		public bool IsConnected
		{
			get { return _isConnected; }
			set { SetProperty(ref _isConnected, value); }
		}

		private bool Initialize()
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			const uint FT_PURGE_RX = 1;
			const uint FT_PURGE_TX = 2;

			// reset device
			ftStatus = this.ftdiDevice.ResetDevice();
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return false;
			}

			// purge rx buffers
			ftStatus = this.ftdiDevice.Purge(FT_PURGE_RX);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return false;
			}

			// purge tx buffers
			ftStatus = this.ftdiDevice.Purge(FT_PURGE_TX);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return false;
			}

			return true;
		}

		public void Connect()
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			// open first device in our list by serial number
			ftStatus = this.ftdiDevice.OpenBySerialNumber("GECKUSB0");
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return;
			}

			// set read timeout to 2 seconds, write timeout to 2 second
			ftStatus = this.ftdiDevice.SetTimeouts(2000, 2000);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return;
			}

			// set latency timer to minimum of 2ms.
			byte latencyTimer = 2;
			ftStatus = this.ftdiDevice.SetLatency(latencyTimer);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return;
			}

			// set transfer rate from default of 4096 bytes to max 64k.
			uint transferSize = 65536;
			ftStatus = this.ftdiDevice.InTransferSize(transferSize);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				Disconnect();
				return;
			}

			// initialise usb gecko
			if (Initialize())
			{
				this.IsConnected = true;
				return;
			}
			else
			{
				Disconnect();
				return;
			}
		}

		public void Disconnect()
		{
			this.IsConnected = false;
			this.ftdiDevice.Close();
		}

		private FTDI.FT_STATUS ftdiRead(byte[] dataBuffer, uint numBytesToRead)
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			uint numBytesRead = 0;

			ftStatus = this.ftdiDevice.Read(dataBuffer, numBytesToRead, ref numBytesRead);

			return ftStatus;
		}

		private FTDI.FT_STATUS ftdiWrite(byte[] dataBuffer, uint numBytesToWrite)
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			uint numBytesWritten = 0;

			ftStatus = this.ftdiDevice.Write(dataBuffer, numBytesToWrite, ref numBytesWritten);

			return ftStatus;
		}

		public byte[] peek(uint address, uint length)
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			// reset connection
			Initialize();

			// get start and end address and put them in powerpc endianness.
			ulong startAddress = address;
			ulong endAddress = address + length;
			ulong memRange = Tools.ReverseBytes((startAddress << 32) + endAddress);

			// set necessary packets
			byte[] cmdRead = { 4 };
			byte[] ack = { 170 };
			byte[] memRangeAsBytes = BitConverter.GetBytes(memRange);
			byte[] response = new Byte[length];
			byte[] emptyResponse = { 0 };

			// transmit readmem command to gecko.
			ftStatus = ftdiWrite(cmdRead, 1);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return emptyResponse;
			}

			// receive ack from gecko.
			byte[] ackResponse = new Byte[1];
			ftStatus = ftdiRead(ackResponse, 1);
			if (ftStatus != FTDI.FT_STATUS.FT_OK || BitConverter.ToString(ackResponse) != BitConverter.ToString(ack))
			{
				return emptyResponse;
			}

			// send memory range for the readmem command.
			ftStatus = ftdiWrite(memRangeAsBytes, 8);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return emptyResponse;
			}

			// get memory values from given range.
			ftStatus = ftdiRead(response, length);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return emptyResponse;
			}

			// send ack to gecko.
			ftStatus = ftdiWrite(ack, 1);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return emptyResponse;
			}

			//Array.Reverse(response);
			return response;
		}

		public void poke(uint address, uint length, uint data)
		{
			ftStatus = FTDI.FT_STATUS.FT_OK;

			// reset connection
			Initialize();

			// get start and end address and put them in powerpc endianness.
			ulong writeAddress = address;
			ulong writeData = data;
			ulong addressAndData = Tools.ReverseBytes((writeAddress << 32) + data);

			// set necessary packets
			byte[] response = new Byte[length];
			byte[] ack = { 170 };
			byte[] addressAndDataAsBytes = BitConverter.GetBytes(addressAndData);
			byte[] cmdWrite = { 3 };
			switch (length)
			{
				case 1:
					cmdWrite[0] = 1;
					break;
				case 2:
					cmdWrite[0] = 2;
					break;
				case 4:
					cmdWrite[0] = 3;
					break;
				default:
					return;
			}

			// transmit writemem command to gecko.
			ftStatus = ftdiWrite(cmdWrite, 1);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return;
			}

			// send address and data for the writemem command.
			ftStatus = ftdiWrite(addressAndDataAsBytes, 8);
			if (ftStatus != FTDI.FT_STATUS.FT_OK)
			{
				return;
			}

			return;
		}
	}
}
