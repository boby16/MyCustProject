using System;
using System.ComponentModel;

namespace Gssy.Capi.Class
{
	// Token: 0x02000065 RID: 101
	public class Persent5 : INotifyPropertyChanged
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x00003E6F File Offset: 0x0000206F
		public Persent5()
		{
			this.lastAutoPersent = this.totalPersent - this.persent1;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00003E9A File Offset: 0x0000209A
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00003EA2 File Offset: 0x000020A2
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00003EC4 File Offset: 0x000020C4
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00003ECC File Offset: 0x000020CC
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00003EF5 File Offset: 0x000020F5
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00003EFD File Offset: 0x000020FD
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00003F26 File Offset: 0x00002126
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00003F2E File Offset: 0x0000212E
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00003F57 File Offset: 0x00002157
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00003F5F File Offset: 0x0000215F
		public int Persent4
		{
			get
			{
				return this.persent4;
			}
			set
			{
				if (this.Persent4 != value)
				{
					this.persent4 = value;
					this.method_1(global::GClass0.smethod_0("XŢɴͶѡխٶܵ"));
					this.method_0(4);
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00003F88 File Offset: 0x00002188
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00003F90 File Offset: 0x00002190
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

		// Token: 0x0600065A RID: 1626 RVA: 0x0009E090 File Offset: 0x0009C290
		private void method_0(int int_0)
		{
			this.LastAutoPersent = 100 - this.Persent1 - this.Persent2 - this.Persent3 - this.Persent4;
			if (this.LastAutoPersent <= 0)
			{
				this.LastAutoPersent = 0;
				this.TotalPersent = this.Persent1 + this.Persent2 + this.Persent3 + this.Persent4;
				return;
			}
			this.TotalPersent = this.Persent1 + this.Persent2 + this.Persent3 + this.Persent4 + this.LastAutoPersent;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600065B RID: 1627 RVA: 0x0009E11C File Offset: 0x0009C31C
		// (remove) Token: 0x0600065C RID: 1628 RVA: 0x0009E154 File Offset: 0x0009C354
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600065D RID: 1629 RVA: 0x0009E18C File Offset: 0x0009C38C
		private void method_1(string string_0)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(string_0));
			}
		}

		// Token: 0x04000B09 RID: 2825
		private int totalPersent = 100;

		// Token: 0x04000B0A RID: 2826
		private int persent1;

		// Token: 0x04000B0B RID: 2827
		private int persent2;

		// Token: 0x04000B0C RID: 2828
		private int persent3;

		// Token: 0x04000B0D RID: 2829
		private int persent4;

		// Token: 0x04000B0E RID: 2830
		private int lastAutoPersent = 100;
	}
}
