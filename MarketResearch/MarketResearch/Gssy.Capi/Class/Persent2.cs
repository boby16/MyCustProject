using System;
using System.ComponentModel;

namespace Gssy.Capi.Class
{
	public class Persent2 : INotifyPropertyChanged
	{
		public Persent2()
		{
			this.lastAutoPersent = this.totalPersent - this.persent1;
		}

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
					this.method_1("TotalPersent");
				}
			}
		}

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
					this.method_1("Persent1");
					this.method_0();
				}
			}
		}

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
					this.method_1("LastAutoPersent");
					this.method_0();
				}
			}
		}

		private void method_0()
		{
			this.LastAutoPersent = this.TotalPersent - this.Persent1;
			if (this.LastAutoPersent < 0)
			{
				this.LastAutoPersent = 0;
				this.Persent1 = 100;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void method_1(string string_0)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(string_0));
			}
		}

		private int totalPersent = 100;

		private int persent1;

		private int lastAutoPersent = 100;
	}
}
