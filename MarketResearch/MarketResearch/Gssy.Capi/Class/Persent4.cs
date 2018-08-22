using System;
using System.ComponentModel;

namespace Gssy.Capi.Class
{
	public class Persent4 : INotifyPropertyChanged
	{
		public Persent4()
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
					this.method_0(1);
				}
			}
		}

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
					this.method_1("Persent2");
					this.method_0(2);
				}
			}
		}

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
					this.method_1("Persent3");
					this.method_0(3);
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
				}
			}
		}

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

		private int persent2;

		private int persent3;

		private int lastAutoPersent = 100;
	}
}
