using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Gssy.Capi.Class
{
	public class Persent2 : INotifyPropertyChanged
	{
		private int totalPersent = 100;

		private int persent1;

		private int lastAutoPersent = 100;

		public int TotalPersent
		{
			get
			{
				return totalPersent;
			}
			set
			{
				//IL_0022: Incompatible stack heights: 0 vs 2
				if (TotalPersent != value)
				{
					((Persent2)/*Error near IL_0011: Stack underflow*/).totalPersent = (int)/*Error near IL_0011: Stack underflow*/;
					_003F291_003F(_003F487_003F._003F488_003F("XŤɾ\u0368Ѥ\u0557٣ݷ\u0877०੬୵"));
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
				//IL_0022: Incompatible stack heights: 0 vs 2
				if (Persent1 != value)
				{
					((Persent2)/*Error near IL_0011: Stack underflow*/).persent1 = (int)/*Error near IL_0011: Stack underflow*/;
					_003F291_003F(_003F487_003F._003F488_003F("XŢɴͶѡխٶ\u0730"));
					_003F290_003F();
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
				//IL_0022: Incompatible stack heights: 0 vs 2
				if (LastAutoPersent != value)
				{
					((Persent2)/*Error near IL_0011: Stack underflow*/).lastAutoPersent = (int)/*Error near IL_0011: Stack underflow*/;
					_003F291_003F(_003F487_003F._003F488_003F("Cůɾ\u0378ъտٽݧࡗ\u0963\u0a77୷౦൬\u0e75"));
					_003F290_003F();
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

		public Persent2()
		{
			lastAutoPersent = totalPersent - persent1;
		}

		private void _003F290_003F()
		{
			//IL_0035: Incompatible stack heights: 0 vs 2
			LastAutoPersent = TotalPersent - Persent1;
			if (LastAutoPersent < 0)
			{
				((Persent2)/*Error near IL_0024: Stack underflow*/).LastAutoPersent = (int)/*Error near IL_0024: Stack underflow*/;
				Persent1 = 100;
			}
		}

		private void _003F291_003F(string _003F437_003F)
		{
			//IL_001f: Incompatible stack heights: 0 vs 2
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				PropertyChangedEventArgs e = new PropertyChangedEventArgs(_003F437_003F);
				/*Error near IL_0029: Stack underflow*/((object)/*Error near IL_0029: Stack underflow*/, e);
			}
		}
	}
}
