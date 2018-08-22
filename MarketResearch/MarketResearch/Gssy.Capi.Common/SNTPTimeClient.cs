using System;
using System.Net;
using System.Net.Sockets;

namespace Gssy.Capi.Common
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
					throw new Exception(GClass0.smethod_0("_ŻɢͲѾոٴܯࡼ२੿୻౥൧๻རဦᅣቶ፬ᑯᔡ") + this.TimeServer);
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
			string str = GClass0.smethod_0("\\ŪɯͽЬՂ٤ݭࡡ।੧ୱ౫൱ุ༡");
			switch (this.LeapIndicator)
			{
			case _LeapIndicator.NoWarning:
				str += GClass0.smethod_0("DŦȨͰѧշ٪ݪ࡬०");
				break;
			case _LeapIndicator.LastMinute61:
				str += GClass0.smethod_0("VŸɫͣжոٽݽࡧ॥ੵଯ౦൬๿༫ြᄸረ፴ᑣᕦᙫ᝭ᡦᥲ");
				break;
			case _LeapIndicator.LastMinute59:
				str += GClass0.smethod_0("VŸɫͣжոٽݽࡧ॥ੵଯ౦൬๿༫ဿᄰረ፴ᑣᕦᙫ᝭ᡦᥲ");
				break;
			case _LeapIndicator.Alarm:
				str += GClass0.smethod_0("iŋɇ͗щԃ١ݎࡎॻ੷୩౵൴๴༹ူᅴቺ፺ᑷᕸᘲ᝿᡿᥻ᨮ᭾ᱵᵥṩὡ⁺Ⅸ≨⍬⑾╦♦✨");
				break;
			}
			str = str + GClass0.smethod_0("\u001fěɆͪѼվ٥ݤࡤऩ੦୲౫൧๡ཱးᄡ") + this.VersionNumber.ToString() + GClass0.smethod_0("\u000fċ");
			str += GClass0.smethod_0("KŪɠͦиԡ");
			switch (this.Mode)
			{
			case _Mode.SymmetricActive:
				str += GClass0.smethod_0("CŶɣ͠ѩտٸݠ࡫धੇ୦౰൪๴ཤ");
				break;
			case _Mode.SymmetricPassive:
				str += GClass0.smethod_0("CŶɣ͠ѩտٸݠ࡫ध੖୤౷൪๴ཤ");
				break;
			case _Mode.Client:
				str += GClass0.smethod_0("EũɭͦѬյ");
				break;
			case _Mode.Server:
				str += GClass0.smethod_0("UŠɶ͵ѧճ");
				break;
			case _Mode.Broadcast:
				str += GClass0.smethod_0("Kźɨͧѡէ٢ݱࡵ");
				break;
			case _Mode.Unknown:
				str += GClass0.smethod_0("RŨɮͪѬյٯ");
				break;
			}
			str += GClass0.smethod_0("\u0006Āɚͼѵէٱݱ࡮सਡ");
			switch (this.Stratum)
			{
			case _Stratum.Unspecified:
			case _Stratum.Reserved:
				str += GClass0.smethod_0("^Ťɺ͸Ѣե٬ݢࡪ१੥");
				break;
			case _Stratum.PrimaryReference:
				str += GClass0.smethod_0("AŢɦͣѬվٲܪ࡛७੡ୣ౷ൡ๭ཡၤ");
				break;
			case _Stratum.SecondaryReference:
				str += GClass0.smethod_0("@ŷɲͿѡժ٬ݾࡲपਜ਼୭ౡൣ๷ཡၭᅡቤ");
				break;
			}
			str = str + GClass0.smethod_0("\u0003ćɀͤѩը٤ܧࡲ६੩୦సഡ") + this.TransmitTimestamp.ToString();
			str = str + GClass0.smethod_0("\0Ćɛ͸Ѭիٮݵ࡬५੭ସడ") + this.Precision.ToString() + GClass0.smethod_0("#ůɲ");
			str = str + GClass0.smethod_0("\u001cĚɟ͡ѡՠث݃ࡧॼ੢୴౳൥๯༸အ") + this.PollInterval.ToString() + GClass0.smethod_0("\"Ų");
			str = str + GClass0.smethod_0("\u001dąɜͨѪծٸݬࡦ।੣ଥ్േุ༡") + this.ReferenceID.ToString();
			str = str + GClass0.smethod_0("\u001eĘɃͿѠպح݈ࡢॹ੹୭౵൵๬ཫၭᄸሡ") + this.RootDispersion.ToString() + GClass0.smethod_0("#ůɲ");
			str = str + GClass0.smethod_0("\u0019ęɀ;ѥա٪ܭࡘॹ੣୹నൃ๣ཀྵၥᅺሸጡ") + this.RoundTripDelay.ToString() + GClass0.smethod_0("#ůɲ");
			str = str + GClass0.smethod_0("\u001bğɘͼѱհټܯࡍॡ੣୨ౡഩ็ཡၠᅶቡ፷ᐸᔡ") + this.LocalClockOffset.ToString() + GClass0.smethod_0("#ůɲ");
			return str + GClass0.smethod_0("\u000fċ");
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
