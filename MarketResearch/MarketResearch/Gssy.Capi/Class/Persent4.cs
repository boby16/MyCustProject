using System;
using System.ComponentModel;

namespace Gssy.Capi.Class
{
	// Token: 0x0200006E RID: 110
	public class Persent4 : INotifyPropertyChanged
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x000041C5 File Offset: 0x000023C5
		public Persent4()
		{
			this.lastAutoPersent = this.totalPersent - this.persent1;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000041F0 File Offset: 0x000023F0
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x000041F8 File Offset: 0x000023F8
		public int TotalPersent
		{
			get
			{
				return this.totalPersent;
			}
			set
			{
				if (this.TotalPersent != value)
				{
					this.totalPersent = value;
					this.method_1(global::GClass0.smethod_0("XŤɾͨѤ՗٣ݷࡷ०੬୵"));
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000421A File Offset: 0x0000241A
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00004222 File Offset: 0x00002422
		public int Persent1
		{
			get
			{
				return this.persent1;
			}
			set
			{
				if (this.Persent1 != value)
				{
					this.persent1 = value;
					this.method_1(global::GClass0.smethod_0("XŢɴͶѡխٶܰ"));
					this.method_0(1);
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000424B File Offset: 0x0000244B
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00004253 File Offset: 0x00002453
		public int Persent2
		{
			get
			{
				return this.persent2;
			}
			set
			{
				if (this.Persent2 != value)
				{
					this.persent2 = value;
					this.method_1(global::GClass0.smethod_0("XŢɴͶѡխٶܳ"));
					this.method_0(2);
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000427C File Offset: 0x0000247C
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00004284 File Offset: 0x00002484
		public int Persent3
		{
			get
			{
				return this.persent3;
			}
			set
			{
				if (this.Persent3 != value)
				{
					this.persent3 = value;
					this.method_1(global::GClass0.smethod_0("XŢɴͶѡխٶܲ"));
					this.method_0(3);
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000042AD File Offset: 0x000024AD
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x000042B5 File Offset: 0x000024B5
		public int LastAutoPersent
		{
			get
			{
				return this.lastAutoPersent;
			}
			set
			{
				if (this.LastAutoPersent != value)
				{
					this.lastAutoPersent = value;
					this.method_1(global::GClass0.smethod_0("Cůɾ͸ъտٽݧࡗॣ੷୷౦൬๵"));
				}
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000A0C88 File Offset: 0x0009EE88
		private void method_0(int int_0)
		{
			this.LastAutoPersent = 100 - this.Persent1 - this.Persent2 - this.Persent3;
			if (this.LastAutoPersent <= 0)
			{
				this.LastAutoPersent = 0;
				this.TotalPersent = this.Persent1 + this.Persent2 + this.Persent3;
				return;
			}
			this.TotalPersent = this.Persent1 + this.Persent2 + this.Persent3 + this.LastAutoPersent;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060006AB RID: 1707 RVA: 0x000A0D00 File Offset: 0x0009EF00
		// (remove) Token: 0x060006AC RID: 1708 RVA: 0x000A0D38 File Offset: 0x0009EF38
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060006AD RID: 1709 RVA: 0x000A0D70 File Offset: 0x0009EF70
		private void method_1(string string_0)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(string_0));
			}
		}

		// Token: 0x04000C44 RID: 3140
		private int totalPersent = 100;

		// Token: 0x04000C45 RID: 3141
		private int persent1;

		// Token: 0x04000C46 RID: 3142
		private int persent2;

		// Token: 0x04000C47 RID: 3143
		private int persent3;

		// Token: 0x04000C48 RID: 3144
		private int lastAutoPersent = 100;
	}
}
