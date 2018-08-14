using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gssy.Capi.Control
{
	// Token: 0x0200005D RID: 93
	public class MaskedTextBox : TextBox
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0009B324 File Offset: 0x00099524
		static MaskedTextBox()
		{
			TextBox.TextProperty.OverrideMetadata(typeof(MaskedTextBox), new FrameworkPropertyMetadata(null, new CoerceValueCallback(MaskedTextBox.smethod_0)));
			MaskedTextBox.InputMaskProperty = DependencyProperty.Register(global::GClass0.smethod_0("@ŦɷͳѱՉ٢ݱࡪ"), typeof(string), typeof(MaskedTextBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MaskedTextBox.smethod_1)));
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00003CDC File Offset: 0x00001EDC
		public MaskedTextBox()
		{
			this._maskChars = new List<MaskedTextBox.Class67>();
			DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(this.method_0));
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00003D01 File Offset: 0x00001F01
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00003D13 File Offset: 0x00001F13
		public string InputMask
		{
			get
			{
				return base.GetValue(MaskedTextBox.InputMaskProperty) as string;
			}
			set
			{
				base.SetValue(MaskedTextBox.InputMaskProperty, value);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0009B398 File Offset: 0x00099598
		public bool IsTextValid()
		{
			string text;
			return this.ValidateTextInternal(base.Text, out text);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00003D21 File Offset: 0x00001F21
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00003D2A File Offset: 0x00001F2A
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);
			this._caretIndex = base.CaretIndex;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0009B3B4 File Offset: 0x000995B4
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this._maskChars.Count == 0)
			{
				return;
			}
			if (e.Key == Key.Delete)
			{
				base.Text = this.method_3();
				base.CaretIndex = 0;
				this._caretIndex = 0;
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Back)
			{
				if (this._caretIndex > 0 || base.SelectionLength > 0)
				{
					if (base.SelectionLength > 0)
					{
						this.DeleteSelectedText();
					}
					else
					{
						this.method_5();
						char[] array = base.Text.ToCharArray();
						array[this._caretIndex] = this._maskChars[this._caretIndex].GetDefaultChar();
						base.Text = new string(array);
					}
					base.CaretIndex = this._caretIndex;
					e.Handled = true;
					return;
				}
			}
			else
			{
				if (e.Key == Key.Left)
				{
					this.method_5();
					e.Handled = true;
					return;
				}
				if (e.Key == Key.Right || e.Key == Key.Space)
				{
					this.method_4();
					e.Handled = true;
				}
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0009B4BC File Offset: 0x000996BC
		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			base.OnPreviewTextInput(e);
			if (this._maskChars.Count == 0)
			{
				return;
			}
			this._caretIndex = (base.CaretIndex = base.SelectionStart);
			if (this._caretIndex == this._maskChars.Count)
			{
				e.Handled = true;
				return;
			}
			if (this.ValidateInputChar(char.Parse(e.Text), this._maskChars[this._caretIndex].ValidationFlags))
			{
				if (base.SelectionLength > 0)
				{
					this.DeleteSelectedText();
				}
				char[] array = base.Text.ToCharArray();
				array[this._caretIndex] = char.Parse(e.Text);
				base.Text = new string(array);
				this.method_4();
			}
			e.Handled = true;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0009B580 File Offset: 0x00099780
		protected virtual bool ValidateInputChar(char char_0, MaskedTextBox.GEnum0 genum0_0)
		{
			bool result;
			if (!(result = (genum0_0 == MaskedTextBox.GEnum0.None)))
			{
				foreach (object obj in Enum.GetValues(typeof(MaskedTextBox.GEnum0)))
				{
					MaskedTextBox.GEnum0 genum = (MaskedTextBox.GEnum0)((int)obj);
					if ((genum & genum0_0) != MaskedTextBox.GEnum0.None && this.method_2(char_0, genum))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0009B5F8 File Offset: 0x000997F8
		protected virtual bool ValidateTextInternal(string string_0, out string string_1)
		{
			if (this._maskChars.Count == 0)
			{
				string_1 = string_0;
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(this.method_3());
			bool result;
			if (result = (!string.IsNullOrEmpty(string_0) && string_0.Length <= this._maskChars.Count))
			{
				for (int i = 0; i < string_0.Length; i++)
				{
					if (!this._maskChars[i].IsLiteral())
					{
						if (this.ValidateInputChar(string_0[i], this._maskChars[i].ValidationFlags))
						{
							stringBuilder[i] = string_0[i];
						}
						else
						{
							result = false;
						}
					}
				}
			}
			string_1 = stringBuilder.ToString();
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0009B6A8 File Offset: 0x000998A8
		protected virtual void DeleteSelectedText()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Text);
			string text = this.method_3();
			int selectionStart = base.SelectionStart;
			int selectionLength = base.SelectionLength;
			stringBuilder.Remove(selectionStart, selectionLength);
			stringBuilder.Insert(selectionStart, text.Substring(selectionStart, selectionLength));
			base.Text = stringBuilder.ToString();
			base.CaretIndex = (this._caretIndex = selectionStart);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0009B710 File Offset: 0x00099910
		protected virtual bool IsPlaceholderChar(char char_0, out MaskedTextBox.GEnum0 genum0_0)
		{
			genum0_0 = MaskedTextBox.GEnum0.None;
			string a = char_0.ToString().ToUpper();
			if (!(a == global::GClass0.smethod_0("H")))
			{
				if (!(a == global::GClass0.smethod_0("E")))
				{
					if (!(a == global::GClass0.smethod_0("@")))
					{
						if (a == global::GClass0.smethod_0("V"))
						{
							genum0_0 = MaskedTextBox.GEnum0.AllowAlphanumeric;
						}
					}
					else
					{
						genum0_0 = MaskedTextBox.GEnum0.AllowAlphabet;
					}
				}
				else
				{
					genum0_0 = MaskedTextBox.GEnum0.AllowDecimal;
				}
			}
			else
			{
				genum0_0 = MaskedTextBox.GEnum0.AllowInteger;
			}
			return genum0_0 > MaskedTextBox.GEnum0.None;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0009B78C File Offset: 0x0009998C
		private static object smethod_0(DependencyObject dependencyObject_0, object object_0)
		{
			MaskedTextBox maskedTextBox = (MaskedTextBox)dependencyObject_0;
			if (object_0 != null && !object_0.Equals(string.Empty))
			{
				if (object_0.ToString().Length > 0)
				{
					string text;
					maskedTextBox.ValidateTextInternal(object_0.ToString(), out text);
					object_0 = text;
				}
			}
			else
			{
				object_0 = maskedTextBox.method_3();
			}
			return object_0;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00003D3F File Offset: 0x00001F3F
		private static void smethod_1(DependencyObject dependencyObject_0, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0)
		{
			(dependencyObject_0 as MaskedTextBox).method_1();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0009B7DC File Offset: 0x000999DC
		private void method_0(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				string string_ = e.DataObject.GetData(typeof(string)).ToString();
				string text;
				if (this.ValidateTextInternal(string_, out text))
				{
					base.Text = text;
				}
			}
			e.CancelCommand();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0009B834 File Offset: 0x00099A34
		private void method_1()
		{
			string text = base.Text;
			this._maskChars.Clear();
			base.Text = string.Empty;
			string inputMask = this.InputMask;
			if (string.IsNullOrEmpty(inputMask))
			{
				return;
			}
			MaskedTextBox.GEnum0 genum0_ = MaskedTextBox.GEnum0.None;
			for (int i = 0; i < inputMask.Length; i++)
			{
				if (this.IsPlaceholderChar(inputMask[i], out genum0_))
				{
					this._maskChars.Add(new MaskedTextBox.Class67(genum0_));
				}
				else
				{
					this._maskChars.Add(new MaskedTextBox.Class67(inputMask[i]));
				}
			}
			string text2;
			if (text.Length > 0 && this.ValidateTextInternal(text, out text2))
			{
				base.Text = text2;
				return;
			}
			base.Text = this.method_3();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0009B8EC File Offset: 0x00099AEC
		private bool method_2(char char_0, MaskedTextBox.GEnum0 genum0_0)
		{
			bool result = false;
			switch (genum0_0)
			{
			case MaskedTextBox.GEnum0.AllowInteger:
			case MaskedTextBox.GEnum0.AllowDecimal:
			{
				int num;
				result = ((genum0_0 == MaskedTextBox.GEnum0.AllowDecimal && char_0 == '.' && !base.Text.Contains('.')) || int.TryParse(char_0.ToString(), out num));
				break;
			}
			case MaskedTextBox.GEnum0.AllowInteger | MaskedTextBox.GEnum0.AllowDecimal:
				break;
			case MaskedTextBox.GEnum0.AllowAlphabet:
				result = char.IsLetter(char_0);
				break;
			default:
				if (genum0_0 == MaskedTextBox.GEnum0.AllowAlphanumeric)
				{
					result = (char.IsLetter(char_0) || char.IsNumber(char_0));
				}
				break;
			}
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0009B964 File Offset: 0x00099B64
		private string method_3()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MaskedTextBox.Class67 @class in this._maskChars)
			{
				stringBuilder.Append(@class.GetDefaultChar());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0009B9CC File Offset: 0x00099BCC
		private void method_4()
		{
			int i = this._caretIndex;
			while (i < this._maskChars.Count)
			{
				if (++i == this._maskChars.Count || !this._maskChars[i].IsLiteral())
				{
					this._caretIndex = (base.CaretIndex = i);
					return;
				}
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0009BA28 File Offset: 0x00099C28
		private void method_5()
		{
			int i = this._caretIndex;
			while (i > 0)
			{
				if (--i == 0 || !this._maskChars[i].IsLiteral())
				{
					this._caretIndex = (base.CaretIndex = i);
					return;
				}
			}
		}

		// Token: 0x04000AF8 RID: 2808
		public static readonly DependencyProperty InputMaskProperty;

		// Token: 0x04000AF9 RID: 2809
		private List<MaskedTextBox.Class67> _maskChars;

		// Token: 0x04000AFA RID: 2810
		private int _caretIndex;

		// Token: 0x020000C2 RID: 194
		[Flags]
		protected enum GEnum0
		{
			// Token: 0x04000D4C RID: 3404
			None = 0,
			// Token: 0x04000D4D RID: 3405
			AllowInteger = 1,
			// Token: 0x04000D4E RID: 3406
			AllowDecimal = 2,
			// Token: 0x04000D4F RID: 3407
			AllowAlphabet = 4,
			// Token: 0x04000D50 RID: 3408
			AllowAlphanumeric = 8
		}

		// Token: 0x020000C3 RID: 195
		private class Class67
		{
			// Token: 0x060007AB RID: 1963 RVA: 0x00004712 File Offset: 0x00002912
			public Class67(MaskedTextBox.GEnum0 genum0_0)
			{
				this._validationFlags = genum0_0;
				this._literal = '\0';
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x00004728 File Offset: 0x00002928
			public Class67(char char_0)
			{
				this._literal = char_0;
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060007AD RID: 1965 RVA: 0x00004737 File Offset: 0x00002937
			// (set) Token: 0x060007AE RID: 1966 RVA: 0x0000473F File Offset: 0x0000293F
			public MaskedTextBox.GEnum0 ValidationFlags
			{
				get
				{
					return this._validationFlags;
				}
				set
				{
					this._validationFlags = value;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060007AF RID: 1967 RVA: 0x00004748 File Offset: 0x00002948
			// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00004750 File Offset: 0x00002950
			public char Literal
			{
				get
				{
					return this._literal;
				}
				set
				{
					this._literal = value;
				}
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x00004759 File Offset: 0x00002959
			public bool IsLiteral()
			{
				return this._literal > '\0';
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x00004764 File Offset: 0x00002964
			public char GetDefaultChar()
			{
				if (!this.IsLiteral())
				{
					return '_';
				}
				return this.Literal;
			}

			// Token: 0x04000D51 RID: 3409
			private MaskedTextBox.GEnum0 _validationFlags;

			// Token: 0x04000D52 RID: 3410
			private char _literal;
		}
	}
}
