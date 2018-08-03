using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[CompilerGenerated]
internal sealed class _003F1_003F<_003CCODE_003Ej__TPar, _003CCODE_TEXT_003Ej__TPar, _003CIS_OTHER_003Ej__TPar, _003CEXTEND_1_003Ej__TPar>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly _003CCODE_003Ej__TPar _003CCODE_003Ei__Field;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly _003CCODE_TEXT_003Ej__TPar _003CCODE_TEXT_003Ei__Field;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly _003CIS_OTHER_003Ej__TPar _003CIS_OTHER_003Ei__Field;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly _003CEXTEND_1_003Ej__TPar _003CEXTEND_1_003Ei__Field;

	public _003CCODE_003Ej__TPar CODE
	{
		get
		{
			return _003CCODE_003Ei__Field;
		}
	}

	public _003CCODE_TEXT_003Ej__TPar CODE_TEXT
	{
		get
		{
			return _003CCODE_TEXT_003Ei__Field;
		}
	}

	public _003CIS_OTHER_003Ej__TPar IS_OTHER
	{
		get
		{
			return _003CIS_OTHER_003Ei__Field;
		}
	}

	public _003CEXTEND_1_003Ej__TPar EXTEND_1
	{
		get
		{
			return _003CEXTEND_1_003Ei__Field;
		}
	}

	[DebuggerHidden]
	public _003F1_003F(_003CCODE_003Ej__TPar _003F342_003F, _003CCODE_TEXT_003Ej__TPar _003F343_003F, _003CIS_OTHER_003Ej__TPar _003F344_003F, _003CEXTEND_1_003Ej__TPar _003F345_003F)
	{
		_003CCODE_003Ei__Field = _003F342_003F;
		_003CCODE_TEXT_003Ei__Field = _003F343_003F;
		_003CIS_OTHER_003Ei__Field = _003F344_003F;
		_003CEXTEND_1_003Ei__Field = _003F345_003F;
	}

	[DebuggerHidden]
	public override bool Equals(object _003F346_003F)
	{
		//IL_0050: Incompatible stack heights: 0 vs 1
		//IL_0066: Incompatible stack heights: 0 vs 3
		//IL_0081: Incompatible stack heights: 0 vs 3
		//IL_00a1: Incompatible stack heights: 0 vs 1
		global::_003F1_003F<_003CCODE_003Ej__TPar, _003CCODE_TEXT_003Ej__TPar, _003CIS_OTHER_003Ej__TPar, _003CEXTEND_1_003Ej__TPar> _003F1_003F = _003F346_003F as global::_003F1_003F<_003CCODE_003Ej__TPar, _003CCODE_TEXT_003Ej__TPar, _003CIS_OTHER_003Ej__TPar, _003CEXTEND_1_003Ej__TPar>;
		if (_003F1_003F != null)
		{
			EqualityComparer<_003CCODE_003Ej__TPar>.Default.Equals(_003CCODE_003Ei__Field, _003F1_003F._003CCODE_003Ei__Field);
			if ((int)/*Error near IL_0055: Stack underflow*/ != 0)
			{
				EqualityComparer<_003CCODE_TEXT_003Ej__TPar> @default = EqualityComparer<_003CCODE_TEXT_003Ej__TPar>.Default;
				_003CCODE_TEXT_003Ej__TPar _003CCODE_TEXT_003Ei__Field2 = _003CCODE_TEXT_003Ei__Field;
				_003CCODE_TEXT_003Ej__TPar y = ((global::_003F1_003F<_003CCODE_003Ej__TPar, _003CCODE_TEXT_003Ej__TPar, _003CIS_OTHER_003Ej__TPar, _003CEXTEND_1_003Ej__TPar>)/*Error near IL_0017: Stack underflow*/)._003CCODE_TEXT_003Ei__Field;
				if (((EqualityComparer<_003CCODE_TEXT_003Ej__TPar>)/*Error near IL_001c: Stack underflow*/).Equals((_003CCODE_TEXT_003Ej__TPar)/*Error near IL_001c: Stack underflow*/, y))
				{
					EqualityComparer<_003CIS_OTHER_003Ej__TPar> default2 = EqualityComparer<_003CIS_OTHER_003Ej__TPar>.Default;
					_003CIS_OTHER_003Ej__TPar _003CIS_OTHER_003Ei__Field2 = _003CIS_OTHER_003Ei__Field;
					_003CIS_OTHER_003Ej__TPar _003CIS_OTHER_003Ei__Field3 = _003F1_003F._003CIS_OTHER_003Ei__Field;
					if (((EqualityComparer<_003CIS_OTHER_003Ej__TPar>)/*Error near IL_0026: Stack underflow*/).Equals((_003CIS_OTHER_003Ej__TPar)/*Error near IL_0026: Stack underflow*/, (_003CIS_OTHER_003Ej__TPar)/*Error near IL_0026: Stack underflow*/))
					{
						EqualityComparer<_003CEXTEND_1_003Ej__TPar>.Default.Equals(_003CEXTEND_1_003Ei__Field, _003F1_003F._003CEXTEND_1_003Ei__Field);
						return (byte)/*Error near IL_00a2: Stack underflow*/ != 0;
					}
				}
			}
		}
		return false;
	}

	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((1306959481 * -1521134295 + EqualityComparer<_003CCODE_003Ej__TPar>.Default.GetHashCode(_003CCODE_003Ei__Field)) * -1521134295 + EqualityComparer<_003CCODE_TEXT_003Ej__TPar>.Default.GetHashCode(_003CCODE_TEXT_003Ei__Field)) * -1521134295 + EqualityComparer<_003CIS_OTHER_003Ej__TPar>.Default.GetHashCode(_003CIS_OTHER_003Ei__Field)) * -1521134295 + EqualityComparer<_003CEXTEND_1_003Ej__TPar>.Default.GetHashCode(_003CEXTEND_1_003Ei__Field);
	}

	[DebuggerHidden]
	public unsafe override string ToString()
	{
		//IL_00fd: Incompatible stack heights: 0 vs 6
		//IL_0110: Incompatible stack heights: 0 vs 2
		//IL_011c: Incompatible stack heights: 0 vs 1
		//IL_0121: Incompatible stack heights: 1 vs 0
		//IL_0126: Incompatible stack heights: 0 vs 3
		//IL_0136: Incompatible stack heights: 0 vs 1
		//IL_013d: Incompatible stack heights: 0 vs 1
		//IL_0142: Incompatible stack heights: 1 vs 0
		//IL_0147: Incompatible stack heights: 0 vs 3
		//IL_015c: Incompatible stack heights: 0 vs 2
		//IL_0168: Incompatible stack heights: 0 vs 1
		//IL_016d: Incompatible stack heights: 1 vs 0
		//IL_0172: Incompatible stack heights: 0 vs 3
		//IL_0182: Incompatible stack heights: 0 vs 2
		//IL_018e: Incompatible stack heights: 0 vs 1
		//IL_0193: Incompatible stack heights: 1 vs 0
		_003F487_003F._003F488_003F(":ĻȟͽѲոپܚࠄघ\u0a4cଆ\u0c48ഘณ\u0f71ၾᅴቪ፱ᑹᕩᙳ\u177e᠉ᤕᨇ᭝ᰔ\u1d59ḏἂ\u2068ⅳ≀⍑⑉╔♞❈⠹⤥⨷⭭Ⱗ\u2d69⸿⼲ごㅈ㉛㍋㑃㕈㙔㜻㠩㤵㨧㭽㰶㵹㸣㽿䁼");
		object[] array = new object[4];
		_003CCODE_003Ej__TPar val = _003CCODE_003Ei__Field;
		if (default(_003CCODE_003Ej__TPar) == null)
		{
			_003CCODE_003Ej__TPar val2 = *(_003CCODE_003Ej__TPar*)(long)(IntPtr)(void*)/*Error near IL_00fe: Stack underflow*/;
			object obj = (object)val2;
			if ((int)/*Error near IL_0115: Stack underflow*/ == 0)
			{
				goto IL_0044;
			}
		}
		string text = ((object)/*Error near IL_0044: Stack underflow*/).ToString();
		goto IL_0044;
		IL_00f1:
		object text2;
		((object[])/*Error near IL_00f2: Stack underflow*/)[(long)/*Error near IL_00f2: Stack underflow*/] = text2;
		return string.Format((IFormatProvider)/*Error near IL_00f7: Stack underflow*/, (string)/*Error near IL_00f7: Stack underflow*/, (object[])/*Error near IL_00f7: Stack underflow*/);
		IL_0080:
		object text3;
		((object[])/*Error near IL_0081: Stack underflow*/)[(long)/*Error near IL_0081: Stack underflow*/] = text3;
		/*Error near IL_0081: Stack underflow*/;
		_003CIS_OTHER_003Ej__TPar val3 = _003CIS_OTHER_003Ei__Field;
		if (default(_003CIS_OTHER_003Ej__TPar) == null)
		{
			_003CIS_OTHER_003Ej__TPar val4 = *(_003CIS_OTHER_003Ej__TPar*)(long)(IntPtr)(void*)/*Error near IL_0148: Stack underflow*/;
			object obj2 = (object)val4;
			if ((int)/*Error near IL_0161: Stack underflow*/ == 0)
			{
				goto IL_00b6;
			}
		}
		string text4 = ((object)/*Error near IL_00b6: Stack underflow*/).ToString();
		goto IL_00b6;
		IL_00b6:
		((object[])/*Error near IL_00b7: Stack underflow*/)[(long)/*Error near IL_00b7: Stack underflow*/] = text4;
		/*Error near IL_00b7: Stack underflow*/;
		_003CEXTEND_1_003Ej__TPar val5 = _003CEXTEND_1_003Ei__Field;
		if (default(_003CEXTEND_1_003Ej__TPar) == null)
		{
			_003CEXTEND_1_003Ej__TPar val6 = *(_003CEXTEND_1_003Ej__TPar*)(long)(IntPtr)(void*)/*Error near IL_0173: Stack underflow*/;
			if ((object)/*Error near IL_00dc: Stack underflow*/ == null)
			{
				goto IL_00f1;
			}
		}
		text2 = ((object)/*Error near IL_00f1: Stack underflow*/).ToString();
		goto IL_00f1;
		IL_0044:
		((object[])/*Error near IL_0045: Stack underflow*/)[(long)/*Error near IL_0045: Stack underflow*/] = text;
		/*Error near IL_0045: Stack underflow*/;
		_003CCODE_TEXT_003Ej__TPar val7 = _003CCODE_TEXT_003Ei__Field;
		if (default(_003CCODE_TEXT_003Ej__TPar) == null)
		{
			_003CCODE_TEXT_003Ej__TPar val8 = *(_003CCODE_TEXT_003Ej__TPar*)(long)(IntPtr)(void*)/*Error near IL_0127: Stack underflow*/;
			if (val8 == null)
			{
				goto IL_0080;
			}
		}
		text3 = ((object)/*Error near IL_0080: Stack underflow*/).ToString();
		goto IL_0080;
	}
}
