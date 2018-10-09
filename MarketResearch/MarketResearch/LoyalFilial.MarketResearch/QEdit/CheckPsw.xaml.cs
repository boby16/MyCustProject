using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.Class;

namespace LoyalFilial.MarketResearch.QEdit
{
	public partial class CheckPsw : Window
	{
		public CheckPsw()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.passwordBox1.Focus();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string password = this.passwordBox1.Password;
			if (!(password == "") && !(password != SurveyMsg.SurveyRangePsw))
			{
				SurveyMsg.SurveyRangePswOk = true;
				base.Close();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgRangePswError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.passwordBox1.Focus();
		}

		private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		private void passwordBox1_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void passwordBox1_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void btnKeyboard_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}
	}
}
