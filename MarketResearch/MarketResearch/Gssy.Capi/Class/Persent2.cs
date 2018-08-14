using System;
using System.ComponentModel;

namespace Gssy.Capi.Class
{
	// Token: 0x0200006D RID: 109
	public class Persent2 : INotifyPropertyChanged
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x000040E3 File Offset: 0x000022E3
		public Persent2()
		{
			this.lastAutoPersent = this.totalPersent - this.persent1;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000410E File Offset: 0x0000230E
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00004116 File Offset: 0x00002316
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00004138 File Offset: 0x00002338
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00004140 File Offset: 0x00002340
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
					this.method_0();
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00004168 File Offset: 0x00002368
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00004170 File Offset: 0x00002370
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
					this.method_0();
				}
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00004198 File Offset: 0x00002398
		private void method_0()
		{
			this.LastAutoPersent = this.TotalPersent - this.Persent1;
			if (this.LastAutoPersent < 0)
			{
				this.LastAutoPersent = 0;
				this.Persent1 = 100;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600069C RID: 1692 RVA: 0x000A0BF4 File Offset: 0x0009EDF4
		// (remove) Token: 0x0600069D RID: 1693 RVA: 0x000A0C2C File Offset: 0x0009EE2C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600069E RID: 1694 RVA: 0x000A0C64 File Offset: 0x0009EE64
		private void method_1(string string_0)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(string_0));
			}
		}

		// Token: 0x04000C40 RID: 3136
		private int totalPersent = 100;

		// Token: 0x04000C41 RID: 3137
		private int persent1;

		// Token: 0x04000C42 RID: 3138
		private int lastAutoPersent = 100;
	}
}
