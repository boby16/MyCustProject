using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gssy.Capi.Control
{
	public class MaskedTextBox : TextBox
	{
		[Flags]
		protected enum _003F18_003F
		{
			None = 0x0,
			AllowInteger = 0x1,
			AllowDecimal = 0x2,
			AllowAlphabet = 0x4,
			AllowAlphanumeric = 0x8
		}

		private class _003F19_003F
		{
			private _003F18_003F _validationFlags;

			private char _literal;

			public _003F18_003F ValidationFlags
			{
				get
				{
					return _validationFlags;
				}
				set
				{
					_validationFlags = value;
				}
			}

			public char Literal
			{
				get
				{
					return _literal;
				}
				set
				{
					_literal = value;
				}
			}

			public _003F19_003F(_003F18_003F _003F408_003F)
			{
				_validationFlags = _003F408_003F;
				_literal = '\0';
			}

			public _003F19_003F(char _003F486_003F)
			{
				_literal = _003F486_003F;
			}

			public bool IsLiteral()
			{
				return _literal != '\0';
			}

			public char GetDefaultChar()
			{
				if (!IsLiteral())
				{
					return '_';
				}
				goto IL_000b;
				IL_0019:
				goto IL_000b;
				IL_000b:
				return Literal;
			}
		}

		public static readonly DependencyProperty InputMaskProperty;

		private List<_003F19_003F> _maskChars;

		private int _caretIndex;

		public string InputMask
		{
			get
			{
				return GetValue(InputMaskProperty) as string;
			}
			set
			{
				SetValue(InputMaskProperty, value);
			}
		}

		static MaskedTextBox()
		{
			TextBox.TextProperty.OverrideMetadata(typeof(MaskedTextBox), new FrameworkPropertyMetadata(null, _003F265_003F));
			InputMaskProperty = DependencyProperty.Register(_003F487_003F._003F488_003F("@ŦɷͳѱՉ٢ݱ\u086a"), typeof(string), typeof(MaskedTextBox), new PropertyMetadata(string.Empty, _003F266_003F));
		}

		public MaskedTextBox()
		{
			_maskChars = new List<_003F19_003F>();
			DataObject.AddPastingHandler(this, _003F267_003F);
		}

		public bool IsTextValid()
		{
			string _003F410_003F;
			return ValidateTextInternal(base.Text, out _003F410_003F);
		}

		protected override void OnInitialized(EventArgs _003F348_003F)
		{
			base.OnInitialized(_003F348_003F);
		}

		protected override void OnMouseUp(MouseButtonEventArgs _003F348_003F)
		{
			base.OnMouseUp(_003F348_003F);
			_caretIndex = base.CaretIndex;
		}

		protected override void OnPreviewKeyDown(KeyEventArgs _003F348_003F)
		{
			//IL_0114: Incompatible stack heights: 0 vs 2
			//IL_0124: Incompatible stack heights: 0 vs 1
			//IL_0129: Invalid comparison between Unknown and I4
			//IL_016c: Incompatible stack heights: 0 vs 2
			OnKeyDown(_003F348_003F);
			if (_maskChars.Count == 0)
			{
				return;
			}
			goto IL_0017;
			IL_0017:
			if (_003F348_003F.Key == Key.Delete)
			{
				base.Text = _003F270_003F();
				int num2 = _caretIndex = (base.CaretIndex = 0);
				_003F348_003F.Handled = true;
			}
			else if (_003F348_003F.Key == Key.Back)
			{
				int caretIndex = _caretIndex;
				if (/*Error near IL_0119: Stack underflow*/ <= /*Error near IL_0119: Stack underflow*/)
				{
					int selectionLength = base.SelectionLength;
					if ((int)/*Error near IL_0129: Stack underflow*/ <= 0)
					{
						return;
					}
				}
				if (base.SelectionLength <= 0)
				{
					_003F272_003F();
					char[] array = base.Text.ToCharArray();
					array[_caretIndex] = _maskChars[_caretIndex].GetDefaultChar();
					base.Text = new string(array);
				}
				else
				{
					DeleteSelectedText();
				}
				base.CaretIndex = _caretIndex;
				_003F348_003F.Handled = true;
			}
			else if (_003F348_003F.Key == Key.Left)
			{
				_003F272_003F();
				_003F348_003F.Handled = true;
			}
			else
			{
				if (_003F348_003F.Key != Key.Right)
				{
					Key key = _003F348_003F.Key;
					if (/*Error near IL_0171: Stack underflow*/ != /*Error near IL_0171: Stack underflow*/)
					{
						return;
					}
				}
				_003F271_003F();
				_003F348_003F.Handled = true;
			}
			return;
			IL_00e8:
			goto IL_0017;
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs _003F348_003F)
		{
			//IL_00a6: Incompatible stack heights: 0 vs 2
			base.OnPreviewTextInput(_003F348_003F);
			if (_maskChars.Count == 0)
			{
				return;
			}
			goto IL_0017;
			IL_0017:
			int num = _caretIndex = (base.CaretIndex = base.SelectionStart);
			if (_caretIndex == _maskChars.Count)
			{
				_003F348_003F.Handled = true;
				return;
			}
			goto IL_0042;
			IL_0090:
			goto IL_0042;
			IL_0042:
			if (ValidateInputChar(char.Parse(_003F348_003F.Text), _maskChars[_caretIndex].ValidationFlags))
			{
				int selectionLength = base.SelectionLength;
				if (/*Error near IL_00ab: Stack underflow*/ > /*Error near IL_00ab: Stack underflow*/)
				{
					DeleteSelectedText();
				}
				char[] array = base.Text.ToCharArray();
				array[_caretIndex] = char.Parse(_003F348_003F.Text);
				base.Text = new string(array);
				_003F271_003F();
			}
			_003F348_003F.Handled = true;
			return;
			IL_007e:
			goto IL_0017;
		}

		protected virtual bool ValidateInputChar(char _003F407_003F, _003F18_003F _003F408_003F)
		{
			bool flag = _003F408_003F == _003F18_003F.None;
			if (!flag)
			{
				foreach (int value in Enum.GetValues(typeof(_003F18_003F)))
				{
					if ((value & (int)_003F408_003F) != 0 && _003F269_003F(_003F407_003F, (_003F18_003F)value))
					{
						return true;
					}
				}
				return flag;
			}
			return flag;
		}

		protected virtual bool ValidateTextInternal(string _003F409_003F, out string _003F410_003F)
		{
			//IL_002e: Invalid comparison between Unknown and I4
			//IL_00bc: Incompatible stack heights: 0 vs 2
			//IL_00ea: Incompatible stack heights: 0 vs 3
			//IL_00f6: Incompatible stack heights: 0 vs 2
			if (_maskChars.Count == 0)
			{
				_003F410_003F = _003F409_003F;
				return true;
			}
			goto IL_0010;
			IL_00a1:
			goto IL_0010;
			IL_0010:
			StringBuilder stringBuilder = new StringBuilder(_003F270_003F());
			int num;
			if (!string.IsNullOrEmpty(_003F409_003F))
			{
				int length = _003F409_003F.Length;
				List<_003F19_003F> maskChar = _maskChars;
				int count = ((List<_003F19_003F>)/*Error near IL_002c: Stack underflow*/).Count;
				num = (((int)/*Error near IL_002e: Stack underflow*/ <= count) ? 1 : 0);
			}
			else
			{
				num = 0;
			}
			bool flag = (byte)num != 0;
			if (flag)
			{
				for (int i = 0; i < _003F409_003F.Length; i++)
				{
					if (!_maskChars[i].IsLiteral())
					{
						char c = _003F409_003F[i];
						List<_003F19_003F> maskChar2 = _maskChars;
						_003F18_003F validationFlags = ((List<_003F19_003F>)/*Error near IL_005f: Stack underflow*/)[i].ValidationFlags;
						if (((MaskedTextBox)/*Error near IL_0069: Stack underflow*/).ValidateInputChar((char)/*Error near IL_0069: Stack underflow*/, validationFlags))
						{
							char value = _003F409_003F[i];
							((StringBuilder)/*Error near IL_007a: Stack underflow*/)[(int)/*Error near IL_007a: Stack underflow*/] = value;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			_003F410_003F = stringBuilder.ToString();
			return flag;
		}

		protected virtual void DeleteSelectedText()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Text);
			string text = _003F270_003F();
			int selectionStart = base.SelectionStart;
			int selectionLength = base.SelectionLength;
			stringBuilder.Remove(selectionStart, selectionLength);
			stringBuilder.Insert(selectionStart, text.Substring(selectionStart, selectionLength));
			base.Text = stringBuilder.ToString();
			base.CaretIndex = (_caretIndex = selectionStart);
		}

		protected virtual bool IsPlaceholderChar(char _003F411_003F, out _003F18_003F _003F408_003F)
		{
			//IL_0084: Incompatible stack heights: 0 vs 1
			//IL_0094: Incompatible stack heights: 0 vs 2
			//IL_00a4: Incompatible stack heights: 0 vs 2
			_003F408_003F = _003F18_003F.None;
			string a = _003F411_003F.ToString().ToUpper();
			if (a == _003F487_003F._003F488_003F("H"))
			{
				_003F408_003F = _003F18_003F.AllowInteger;
			}
			else
			{
				bool flag = a == _003F487_003F._003F488_003F("E");
				if ((int)/*Error near IL_0089: Stack underflow*/ != 0)
				{
					_003F408_003F = _003F18_003F.AllowDecimal;
				}
				else
				{
					string b = _003F487_003F._003F488_003F((string)/*Error near IL_002f: Stack underflow*/);
					if ((string)/*Error near IL_0034: Stack underflow*/ == b)
					{
						_003F408_003F = _003F18_003F.AllowAlphabet;
					}
					else
					{
						string b2 = _003F487_003F._003F488_003F((string)/*Error near IL_003e: Stack underflow*/);
						if ((string)/*Error near IL_0043: Stack underflow*/ == b2)
						{
							goto IL_00c2;
						}
					}
				}
			}
			goto IL_00c5;
			IL_00c2:
			_003F408_003F = _003F18_003F.AllowAlphanumeric;
			goto IL_00c5;
			IL_0065:
			goto IL_00c2;
			IL_00c5:
			return _003F408_003F != _003F18_003F.None;
		}

		private static object _003F265_003F(DependencyObject _003F412_003F, object _003F346_003F)
		{
			//IL_0056: Incompatible stack heights: 0 vs 1
			//IL_0071: Incompatible stack heights: 0 vs 2
			MaskedTextBox maskedTextBox = (MaskedTextBox)_003F412_003F;
			if (_003F346_003F != null)
			{
				_003F346_003F.Equals(string.Empty);
				if ((int)/*Error near IL_005b: Stack underflow*/ == 0)
				{
					if (_003F346_003F.ToString().Length > 0)
					{
						string _003F409_003F = ((object)/*Error near IL_0035: Stack underflow*/).ToString();
						string _003F410_003F;
						((MaskedTextBox)/*Error near IL_003c: Stack underflow*/).ValidateTextInternal(_003F409_003F, out _003F410_003F);
						_003F346_003F = _003F410_003F;
					}
					goto IL_0075;
				}
			}
			_003F346_003F = maskedTextBox._003F270_003F();
			goto IL_0075;
			IL_0075:
			return _003F346_003F;
		}

		private static void _003F266_003F(DependencyObject _003F412_003F, DependencyPropertyChangedEventArgs _003F348_003F)
		{
			(_003F412_003F as MaskedTextBox)._003F268_003F();
		}

		private void _003F267_003F(object _003F347_003F, DataObjectPastingEventArgs _003F348_003F)
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			if (_003F348_003F.DataObject.GetDataPresent(typeof(string)))
			{
				_003F348_003F.DataObject.GetData(typeof(string));
				string _003F409_003F = ((object)/*Error near IL_001f: Stack underflow*/).ToString();
				string _003F410_003F;
				if (ValidateTextInternal(_003F409_003F, out _003F410_003F))
				{
					base.Text = _003F410_003F;
				}
			}
			_003F348_003F.CancelCommand();
		}

		private void _003F268_003F()
		{
			//IL_00c0: Incompatible stack heights: 0 vs 2
			//IL_00d6: Incompatible stack heights: 0 vs 2
			//IL_00e2: Incompatible stack heights: 0 vs 2
			string text = base.Text;
			_maskChars.Clear();
			base.Text = string.Empty;
			string inputMask = InputMask;
			if (string.IsNullOrEmpty(inputMask))
			{
				return;
			}
			goto IL_002f;
			IL_002f:
			_003F18_003F _003F408_003F = _003F18_003F.None;
			for (int i = 0; i < inputMask.Length; i++)
			{
				if (IsPlaceholderChar(inputMask[i], out _003F408_003F))
				{
					List<_003F19_003F> maskChar = _maskChars;
					new _003F19_003F(_003F408_003F);
					((List<_003F19_003F>)/*Error near IL_0053: Stack underflow*/).Add((_003F19_003F)/*Error near IL_0053: Stack underflow*/);
				}
				else
				{
					_maskChars.Add(new _003F19_003F(inputMask[i]));
				}
			}
			int length = text.Length;
			string _003F410_003F;
			if (/*Error near IL_00db: Stack underflow*/ > /*Error near IL_00db: Stack underflow*/&& ((MaskedTextBox)/*Error near IL_008f: Stack underflow*/).ValidateTextInternal((string)/*Error near IL_008f: Stack underflow*/, out _003F410_003F))
			{
				base.Text = _003F410_003F;
				return;
			}
			goto IL_0094;
			IL_00a0:
			goto IL_002f;
			IL_0094:
			base.Text = _003F270_003F();
			return;
			IL_00ef:
			goto IL_0094;
		}

		private bool _003F269_003F(char _003F407_003F, _003F18_003F _003F413_003F)
		{
			//IL_0092: Incompatible stack heights: 0 vs 2
			//IL_00a4: Incompatible stack heights: 0 vs 2
			//IL_00cf: Incompatible stack heights: 0 vs 1
			//IL_00d5: Incompatible stack heights: 0 vs 1
			bool result = false;
			switch (_003F413_003F)
			{
			case _003F18_003F.AllowInteger:
			{
				int result2;
				result = int.TryParse(_003F407_003F.ToString(), out result2);
				break;
			}
			case _003F18_003F.AllowAlphabet:
				result = char.IsLetter(_003F407_003F);
				break;
			case _003F18_003F.AllowAlphanumeric:
				if (char.IsLetter(_003F407_003F))
				{
					goto IL_00d4;
				}
				char.IsNumber(_003F407_003F);
				goto IL_00d5;
			case _003F18_003F.AllowDecimal:
				{
					if (/*Error near IL_0097: Stack underflow*/ == /*Error near IL_0097: Stack underflow*/)
					{
						string text = base.Text;
						if (!((IEnumerable<char>)/*Error near IL_0037: Stack underflow*/).Contains((char)/*Error near IL_0037: Stack underflow*/))
						{
							result = true;
							break;
						}
					}
					goto case _003F18_003F.AllowInteger;
				}
				IL_00d5:
				result = ((byte)/*Error near IL_00d6: Stack underflow*/ != 0);
				break;
				IL_00d4:
				goto IL_00d5;
			}
			return result;
			IL_0071:
			goto IL_00d4;
		}

		private string _003F270_003F()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (_003F19_003F maskChar in _maskChars)
			{
				stringBuilder.Append(maskChar.GetDefaultChar());
			}
			return stringBuilder.ToString();
		}

		private void _003F271_003F()
		{
			//IL_0068: Incompatible stack heights: 0 vs 1
			int num = _caretIndex;
			goto IL_0037;
			IL_0037:
			do
			{
				if (num >= _maskChars.Count)
				{
					return;
				}
				if (++num == _maskChars.Count)
				{
					break;
				}
				_maskChars[num].IsLiteral();
			}
			while ((int)/*Error near IL_006d: Stack underflow*/ != 0);
			int num3 = _caretIndex = (base.CaretIndex = num);
			return;
			IL_0078:
			goto IL_0037;
		}

		private void _003F272_003F()
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			int num = _caretIndex;
			goto IL_002c;
			IL_002c:
			do
			{
				if (num <= 0)
				{
					return;
				}
				if (--num == 0)
				{
					break;
				}
				_maskChars[num].IsLiteral();
			}
			while ((int)/*Error near IL_0058: Stack underflow*/ != 0);
			int num3 = _caretIndex = (base.CaretIndex = num);
			return;
			IL_0063:
			goto IL_002c;
		}
	}
}
