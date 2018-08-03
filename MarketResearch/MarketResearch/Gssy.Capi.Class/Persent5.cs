using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Gssy.Capi.Class
{
	public class Persent5 : INotifyPropertyChanged
	{
		private int totalPersent = 100;

		private int persent1;

		private int persent2;

		private int persent3;

		private int persent4;

		private int lastAutoPersent = 100;

		public int TotalPersent
		{
			get
			{
				return totalPersent;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (TotalPersent != value)
				{
					totalPersent = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
				}
			}
		}

		public int Persent1
		{
			get
			{
				return persent1;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (Persent1 != value)
				{
					persent1 = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
					_003F290_003F(1);
				}
			}
		}

		public int Persent2
		{
			get
			{
				return persent2;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (Persent2 != value)
				{
					persent2 = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
					_003F290_003F(2);
				}
			}
		}

		public int Persent3
		{
			get
			{
				return persent3;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (Persent3 != value)
				{
					persent3 = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
					_003F290_003F(3);
				}
			}
		}

		public int Persent4
		{
			get
			{
				return persent4;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (Persent4 != value)
				{
					persent4 = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
					_003F290_003F(4);
				}
			}
		}

		public int LastAutoPersent
		{
			get
			{
				return lastAutoPersent;
			}
			set
			{
				//IL_002d: Incompatible stack heights: 0 vs 2
				if (LastAutoPersent != value)
				{
					lastAutoPersent = value;
					string _003F437_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
					((Persent5)/*Error near IL_0032: Stack underflow*/)._003F291_003F(_003F437_003F);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				//IL_0036: Incompatible stack heights: 2 vs 0
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler3;
				PropertyChangedEventHandler propertyChangedEventHandler4;
				do
				{
					PropertyChangedEventHandler propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
					propertyChangedEventHandler3 = propertyChangedEventHandler;
					propertyChangedEventHandler4 = propertyChangedEventHandler2;
					continue;
					IL_0031:;
				}
				while ((object)propertyChangedEventHandler3 != propertyChangedEventHandler4);
			}
			[CompilerGenerated]
			remove
			{
				//IL_0036: Incompatible stack heights: 2 vs 0
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler3;
				PropertyChangedEventHandler propertyChangedEventHandler4;
				do
				{
					PropertyChangedEventHandler propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
					propertyChangedEventHandler3 = propertyChangedEventHandler;
					propertyChangedEventHandler4 = propertyChangedEventHandler2;
					continue;
					IL_0031:;
				}
				while ((object)propertyChangedEventHandler3 != propertyChangedEventHandler4);
			}
		}

		public Persent5()
		{
			lastAutoPersent = totalPersent - persent1;
		}

		private void _003F290_003F(int _003F436_003F)
		{
			//IL_004d: Incompatible stack heights: 0 vs 2
			LastAutoPersent = 100 - Persent1 - Persent2 - Persent3 - Persent4;
			if (LastAutoPersent <= 0)
			{
				LastAutoPersent = 0;
				int num = ((Persent5)/*Error near IL_0035: Stack underflow*/).Persent1 + Persent2 + Persent3 + Persent4;
				((Persent5)/*Error near IL_0067: Stack underflow*/).TotalPersent = num;
			}
			else
			{
				TotalPersent = Persent1 + Persent2 + Persent3 + Persent4 + LastAutoPersent;
			}
		}

		private void _003F291_003F(string _003F437_003F)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(_003F437_003F));
			}
		}
	}
}
