using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Gssy.Capi.Control
{
	public class UCProgress : UserControl, IComponentConnector
	{
		internal ScaleTransform SpinnerScale;

		internal RotateTransform SpinnerRotate;

		private bool _contentLoaded;

		public UCProgress()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕴᙹ\u177bᡠᥡ\u1a7d\u1b7d᰿ᵺṭώ⁾Ⅴ≭⍻⑭╴♵✫⡼⥢⩯⭭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0017:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			//IL_0029: Incompatible stack heights: 0 vs 2
			if (_003F349_003F == 1)
			{
				SpinnerScale = (ScaleTransform)_003F350_003F;
			}
			else if (/*Error near IL_002e: Stack underflow*/ == /*Error near IL_002e: Stack underflow*/)
			{
				SpinnerRotate = (RotateTransform)_003F350_003F;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
