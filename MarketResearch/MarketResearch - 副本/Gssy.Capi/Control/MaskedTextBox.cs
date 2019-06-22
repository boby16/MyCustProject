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
		static MaskedTextBox()
		{
			TextBox.TextProperty.OverrideMetadata(typeof(MaskedTextBox), new FrameworkPropertyMetadata(null, new CoerceValueCallback(MaskedTextBox.smethod_0)));
			MaskedTextBox.InputMaskProperty = DependencyProperty.Register("InputMask", typeof(string), typeof(MaskedTextBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MaskedTextBox.smethod_1)));
		}

		public MaskedTextBox()
		{
			this._maskChars = new List<MaskedTextBox.Class67>();
			DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(this.method_0));
		}

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

		public bool IsTextValid()
		{
			string text;
			return this.ValidateTextInternal(base.Text, out text);
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);
			this._caretIndex = base.CaretIndex;
		}

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

		protected virtual bool IsPlaceholderChar(char char_0, out MaskedTextBox.GEnum0 genum0_0)
		{
			genum0_0 = MaskedTextBox.GEnum0.None;
			string a = char_0.ToString().ToUpper();
			if (!(a == "I"))
			{
				if (!(a == "D"))
				{
					if (!(a == "A"))
					{
						if (a == "W")
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

		private static void smethod_1(DependencyObject dependencyObject_0, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs_0)
		{
			(dependencyObject_0 as MaskedTextBox).method_1();
		}

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

		private string method_3()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MaskedTextBox.Class67 @class in this._maskChars)
			{
				stringBuilder.Append(@class.GetDefaultChar());
			}
			return stringBuilder.ToString();
		}

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

		public static readonly DependencyProperty InputMaskProperty;

		private List<MaskedTextBox.Class67> _maskChars;

		private int _caretIndex;

		[Flags]
		protected enum GEnum0
		{
			None = 0,
			AllowInteger = 1,
			AllowDecimal = 2,
			AllowAlphabet = 4,
			AllowAlphanumeric = 8
		}

		private class Class67
		{
			public Class67(MaskedTextBox.GEnum0 genum0_0)
			{
				this._validationFlags = genum0_0;
				this._literal = '\0';
			}

			public Class67(char char_0)
			{
				this._literal = char_0;
			}

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

			public bool IsLiteral()
			{
				return this._literal > '\0';
			}

			public char GetDefaultChar()
			{
				if (!this.IsLiteral())
				{
					return '_';
				}
				return this.Literal;
			}

			private MaskedTextBox.GEnum0 _validationFlags;

			private char _literal;
		}
	}
}
