using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;

namespace LoyalFilial.MarketResearch.QEdit
{
	public partial class FillMode : Window
	{
		public FillMode()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText("FillMode");
			string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText("StopFillPage");
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			this.listBtn.Add(this.btn1);
			this.listBtn.Add(this.btn2);
			this.listBtn.Add(this.btn3);
			foreach (Button button in this.listBtn)
			{
				button.Style = style2;
			}
			if (byCodeText == "1")
			{
				this.btn1.Style = style;
			}
			else if (byCodeText == "2")
			{
				this.btn2.Style = style;
			}
			else
			{
				this.btn3.Style = style;
			}
			this.StopFillPageId.Text = byCodeText2;
			this.Capture.IsChecked = new bool?(SurveyHelper.AutoCapture);
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (this.StopFillPageId.Text != "" && (this.oFunc.LEFT(this.StopFillPageId.Text, 1) != "#" || this.oFunc.RIGHT(this.StopFillPageId.Text, 1) != "#"))
			{
				MessageBox.Show(SurveyMsg.MsgFillMode_NotJING, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.StopFillPageId.Focus();
				return;
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			if (this.btn1.Style == style)
			{
				SurveyHelper.FillMode = "1";
			}
			else if (this.btn2.Style == style)
			{
				SurveyHelper.FillMode = "2";
			}
			else
			{
				SurveyHelper.FillMode = "3";
			}
			SurveyHelper.StopFillPage = this.StopFillPageId.Text;
			SurveyHelper.AutoCapture = (this.Capture.IsChecked == true);
			this.oSurveyConfigBiz.Save("FillMode", SurveyHelper.FillMode);
			this.oSurveyConfigBiz.Save("StopFillPage", SurveyHelper.StopFillPage);
			base.Close();
		}

		private void btn3_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Button button = (Button)sender;
			foreach (Button button2 in this.listBtn)
			{
				button2.Style = style2;
			}
			button.Style = style;
		}

		private void StopFillPageId_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.StopFillPageId.Text == "")
			{
				this.StopFillPageId.Text = "#1#";
				this.StopFillPageId.SelectAll();
			}
		}

		private List<Button> listBtn = new List<Button>();

		private UDPX oFunc = new UDPX();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();
	}
}
