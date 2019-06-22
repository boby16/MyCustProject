using System;
using System.Net;
using System.Net.Sockets;

namespace LoyalFilial.MarketResearch.Common
{
	public class SNTPTimeClient
	{
		public _LeapIndicator LeapIndicator
		{
			get
			{
				switch ((byte)(this.NTPData[0] >> 6))
				{
				case 0:
					return _LeapIndicator.NoWarning;
				case 1:
					return _LeapIndicator.LastMinute61;
				case 2:
					return _LeapIndicator.LastMinute59;
				}
				return _LeapIndicator.Alarm;
			}
		}

		public byte VersionNumber
		{
			get
			{
				return (byte)((this.NTPData[0] & 56) >> 3);
			}
		}

		public _Mode Mode
		{
			get
			{
				switch (this.NTPData[0] & 7)
				{
				case 1:
					return _Mode.SymmetricActive;
				case 2:
					return _Mode.SymmetricPassive;
				case 3:
					return _Mode.Client;
				case 4:
					return _Mode.Server;
				case 5:
					return _Mode.Broadcast;
				}
				return _Mode.Unknown;
			}
		}

		public _Stratum Stratum
		{
			get
			{
				byte b = this.NTPData[1];
				_Stratum result;
				if (b == 0)
				{
					result = _Stratum.Unspecified;
				}
				else if (b == 1)
				{
					result = _Stratum.PrimaryReference;
				}
				else if (b <= 15)
				{
					result = _Stratum.SecondaryReference;
				}
				else
				{
					result = _Stratum.Reserved;
				}
				return result;
			}
		}

		public uint PollInterval
		{
			get
			{
				return (uint)Math.Round(Math.Pow(2.0, (double)this.NTPData[2]));
			}
		}

		public double Precision
		{
			get
			{
				return 1000.0 * Math.Pow(2.0, (double)this.NTPData[3]);
			}
		}

		public double RootDelay
		{
			get
			{
				int num = 256 * (256 * (256 * (int)this.NTPData[4] + (int)this.NTPData[5]) + (int)this.NTPData[6]) + (int)this.NTPData[7];
				return 1000.0 * ((double)num / 65536.0);
			}
		}

		public double RootDispersion
		{
			get
			{
				int num = 256 * (256 * (256 * (int)this.NTPData[8] + (int)this.NTPData[9]) + (int)this.NTPData[10]) + (int)this.NTPData[11];
				return 1000.0 * ((double)num / 65536.0);
			}
		}

		public string ReferenceID
		{
			get
			{
				string text = "";
				switch (this.Stratum)
				{
				case _Stratum.Unspecified:
				case _Stratum.PrimaryReference:
					text += Convert.ToChar(this.NTPData[12]).ToString();
					text += Convert.ToChar(this.NTPData[13]).ToString();
					text += Convert.ToChar(this.NTPData[14]).ToString();
					text += Convert.ToChar(this.NTPData[15]).ToString();
					break;
				}
				return text;
			}
		}

		public DateTime ReferenceTimestamp
		{
			get
			{
				DateTime d = this.method_0(this.method_1(16));
				long value = Convert.ToInt64(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now));
				TimeSpan t = TimeSpan.FromTicks(value);
				return d + t;
			}
		}

		public DateTime OriginateTimestamp
		{
			get
			{
				return this.method_0(this.method_1(24));
			}
		}

		public DateTime ReceiveTimestamp
		{
			get
			{
				DateTime d = this.method_0(this.method_1(32));
				long ticks = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Ticks;
				TimeSpan t = TimeSpan.FromTicks(ticks);
				return d + t;
			}
		}

		public DateTime TransmitTimestamp
		{
			get
			{
				DateTime d = this.method_0(this.method_1(40));
				long ticks = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Ticks;
				TimeSpan t = TimeSpan.FromTicks(ticks);
				return d + t;
			}
			set
			{
				this.method_2(40, value);
			}
		}

		public int RoundTripDelay
		{
			get
			{
				return (int)(this.ReceiveTimestamp - this.OriginateTimestamp + (this.ReceptionTimestamp - this.TransmitTimestamp)).TotalMilliseconds;
			}
		}

		public int LocalClockOffset
		{
			get
			{
				return (int)((this.ReceiveTimestamp - this.OriginateTimestamp - (this.ReceptionTimestamp - this.TransmitTimestamp)).TotalMilliseconds / 2.0);
			}
		}

		private DateTime method_0(ulong ulong_0)
		{
			TimeSpan t = TimeSpan.FromMilliseconds(ulong_0);
			DateTime dateTime = new DateTime(1900, 1, 1);
			dateTime += t;
			return dateTime;
		}

		private ulong method_1(byte byte_0)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			for (int i = 0; i <= 3; i++)
			{
				num = 256UL * num + (ulong)this.NTPData[(int)byte_0 + i];
			}
			for (int j = 4; j <= 7; j++)
			{
				num2 = 256UL * num2 + (ulong)this.NTPData[(int)byte_0 + j];
			}
			return num * 1000UL + num2 * 1000UL / 4294967296UL;
		}

		private void method_2(byte byte_0, DateTime dateTime_0)
		{
			DateTime d = new DateTime(1900, 1, 1, 0, 0, 0);
			ulong num = (ulong)(dateTime_0 - d).TotalMilliseconds;
			ulong num2 = num / 1000UL;
			ulong num3 = num % 1000UL * 4294967296UL / 1000UL;
			ulong num4 = num2;
			for (int i = 3; i >= 0; i--)
			{
				this.NTPData[(int)byte_0 + i] = (byte)(num4 % 256UL);
				num4 /= 256UL;
			}
			num4 = num3;
			for (int j = 7; j >= 4; j--)
			{
				this.NTPData[(int)byte_0 + j] = (byte)(num4 % 256UL);
				num4 /= 256UL;
			}
		}

		private void method_3()
		{
			this.NTPData[0] = 27;
			for (int i = 1; i < 48; i++)
			{
				this.NTPData[i] = 0;
			}
			this.TransmitTimestamp = DateTime.Now;
		}

		public void Connect()
		{
			try
			{
				IPAddress address = IPAddress.Parse(this.TimeServer);
				IPEndPoint endPoint = new IPEndPoint(address, Convert.ToInt32(this.TimePort));
				UdpClient udpClient = new UdpClient();
				udpClient.Connect(endPoint);
				this.method_3();
				udpClient.Send(this.NTPData, this.NTPData.Length);
				this.NTPData = udpClient.Receive(ref endPoint);
				if (!this.IsResponseValid())
				{
					throw new Exception("Invalid response from " + this.TimeServer);
				}
				this.ReceptionTimestamp = DateTime.Now;
			}
			catch (SocketException ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool IsResponseValid()
		{
			return this.NTPData.Length >= 48 && this.Mode == _Mode.Server;
		}

		public override string ToString()
		{
			string str = "Leap Indicator: ";
			switch (this.LeapIndicator)
			{
			case _LeapIndicator.NoWarning:
				str += "No warning";
				break;
			case _LeapIndicator.LastMinute61:
				str += "Last minute has 61 seconds";
				break;
			case _LeapIndicator.LastMinute59:
				str += "Last minute has 59 seconds";
				break;
			case _LeapIndicator.Alarm:
				str += "Alarm Condition (clock not synchronized)";
				break;
			}
			str = str + "\r\nVersion number: " + this.VersionNumber.ToString() + "\r\n";
			str += "Mode: ";
			switch (this.Mode)
			{
			case _Mode.SymmetricActive:
				str += "Symmetric Active";
				break;
			case _Mode.SymmetricPassive:
				str += "Symmetric Pasive";
				break;
			case _Mode.Client:
				str += "Client";
				break;
			case _Mode.Server:
				str += "Server";
				break;
			case _Mode.Broadcast:
				str += "Broadcast";
				break;
			case _Mode.Unknown:
				str += "Unknown";
				break;
			}
			str += "\r\nStratum: ";
			switch (this.Stratum)
			{
			case _Stratum.Unspecified:
			case _Stratum.Reserved:
				str += "Unspecified";
				break;
			case _Stratum.PrimaryReference:
				str += "Primary Reference";
				break;
			case _Stratum.SecondaryReference:
				str += "Secondary Reference";
				break;
			}
			str = str + "\r\nLocal time: " + this.TransmitTimestamp.ToString();
			str = str + "\r\nPrecision: " + this.Precision.ToString() + " ms";
			str = str + "\r\nPoll Interval: " + this.PollInterval.ToString() + " s";
			str = str + "\r\nReference ID: " + this.ReferenceID.ToString();
			str = str + "\r\nRoot Dispersion: " + this.RootDispersion.ToString() + " ms";
			str = str + "\r\nRound Trip Delay: " + this.RoundTripDelay.ToString() + " ms";
			str = str + "\r\nLocal Clock Offset: " + this.LocalClockOffset.ToString() + " ms";
			return str + "\r\n";
		}

		public SNTPTimeClient(string string_0, string string_1)
		{
			this.TimeServer = string_0;
			this.TimePort = string_1;
		}

		private const byte NTPDataLength = 48;

		private byte[] NTPData = new byte[48];

		private const byte offReferenceID = 12;

		private const byte offReferenceTimestamp = 16;

		private const byte offOriginateTimestamp = 24;

		private const byte offReceiveTimestamp = 32;

		private const byte offTransmitTimestamp = 40;

		public DateTime ReceptionTimestamp;

		private string TimeServer;

		private string TimePort;
	}
}
