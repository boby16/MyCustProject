using System;
using System.Net;
using System.Net.Sockets;

namespace Gssy.Capi.Common
{
	// Token: 0x02000018 RID: 24
	public class SNTPTimeClient
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000489C File Offset: 0x00002A9C
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000048E0 File Offset: 0x00002AE0
		public byte VersionNumber
		{
			get
			{
				return (byte)((this.NTPData[0] & 56) >> 3);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004900 File Offset: 0x00002B00
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000495C File Offset: 0x00002B5C
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004998 File Offset: 0x00002B98
		public uint PollInterval
		{
			get
			{
				return (uint)Math.Round(Math.Pow(2.0, (double)this.NTPData[2]));
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000049C4 File Offset: 0x00002BC4
		public double Precision
		{
			get
			{
				return 1000.0 * Math.Pow(2.0, (double)this.NTPData[3]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000049F4 File Offset: 0x00002BF4
		public double RootDelay
		{
			get
			{
				int num = 256 * (256 * (256 * (int)this.NTPData[4] + (int)this.NTPData[5]) + (int)this.NTPData[6]) + (int)this.NTPData[7];
				return 1000.0 * ((double)num / 65536.0);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004A54 File Offset: 0x00002C54
		public double RootDispersion
		{
			get
			{
				int num = 256 * (256 * (256 * (int)this.NTPData[8] + (int)this.NTPData[9]) + (int)this.NTPData[10]) + (int)this.NTPData[11];
				return 1000.0 * ((double)num / 65536.0);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public string ReferenceID
		{
			get
			{
				string text = GClass0.smethod_0("");
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004B60 File Offset: 0x00002D60
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public DateTime OriginateTimestamp
		{
			get
			{
				return this.method_0(this.method_1(24));
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004BC8 File Offset: 0x00002DC8
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004C10 File Offset: 0x00002E10
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002241 File Offset: 0x00000441
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004C58 File Offset: 0x00002E58
		public int RoundTripDelay
		{
			get
			{
				return (int)(this.ReceiveTimestamp - this.OriginateTimestamp + (this.ReceptionTimestamp - this.TransmitTimestamp)).TotalMilliseconds;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004C98 File Offset: 0x00002E98
		public int LocalClockOffset
		{
			get
			{
				return (int)((this.ReceiveTimestamp - this.OriginateTimestamp - (this.ReceptionTimestamp - this.TransmitTimestamp)).TotalMilliseconds / 2.0);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CE4 File Offset: 0x00002EE4
		private DateTime method_0(ulong ulong_0)
		{
			TimeSpan t = TimeSpan.FromMilliseconds(ulong_0);
			DateTime dateTime = new DateTime(1900, 1, 1);
			dateTime += t;
			return dateTime;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D14 File Offset: 0x00002F14
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

		// Token: 0x0600009F RID: 159 RVA: 0x00004DB4 File Offset: 0x00002FB4
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

		// Token: 0x060000A0 RID: 160 RVA: 0x00004EA0 File Offset: 0x000030A0
		private void method_3()
		{
			this.NTPData[0] = 27;
			for (int i = 1; i < 48; i++)
			{
				this.NTPData[i] = 0;
			}
			this.TransmitTimestamp = DateTime.Now;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004EDC File Offset: 0x000030DC
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

		// Token: 0x060000A2 RID: 162 RVA: 0x00004F90 File Offset: 0x00003190
		public bool IsResponseValid()
		{
			return this.NTPData.Length >= 48 && this.Mode == _Mode.Server;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004FC4 File Offset: 0x000031C4
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

		// Token: 0x060000A4 RID: 164 RVA: 0x0000224C File Offset: 0x0000044C
		public SNTPTimeClient(string string_0, string string_1)
		{
			this.TimeServer = string_0;
			this.TimePort = string_1;
		}

		// Token: 0x04000059 RID: 89
		private const byte NTPDataLength = 48;

		// Token: 0x0400005A RID: 90
		private byte[] NTPData = new byte[48];

		// Token: 0x0400005B RID: 91
		private const byte offReferenceID = 12;

		// Token: 0x0400005C RID: 92
		private const byte offReferenceTimestamp = 16;

		// Token: 0x0400005D RID: 93
		private const byte offOriginateTimestamp = 24;

		// Token: 0x0400005E RID: 94
		private const byte offReceiveTimestamp = 32;

		// Token: 0x0400005F RID: 95
		private const byte offTransmitTimestamp = 40;

		// Token: 0x04000060 RID: 96
		public DateTime ReceptionTimestamp;

		// Token: 0x04000061 RID: 97
		private string TimeServer;

		// Token: 0x04000062 RID: 98
		private string TimePort;
	}
}
