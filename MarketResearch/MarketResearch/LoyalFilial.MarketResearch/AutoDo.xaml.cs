using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch
{
	public partial class AutoDo : Window
	{
		public AutoDo()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			string text = this.oSurveyConfigBiz.GetByCodeText("AutoDo_StartNumber");
			text = ((text == null) ? "" : text.Trim());
			string text2 = this.oSurveyConfigBiz.GetByCodeText("AutoDo_Count");
			text2 = ((text2 == null) ? "" : text2.Trim());
			this.txtStartNumber.Text = ((text == "") ? "1" : text);
			this.txtCount.Text = ((text2 == "") ? "1" : text2);
			this.txtProjectName.Text = SurveyMsg.MsgProjectName;
			this.oSurveyConfigBiz.Save("AutoDo_StartNumber", this.txtStartNumber.Text);
			this.oSurveyConfigBiz.Save("AutoDo_Count", this.txtCount.Text);
			this.CurPageId = "CITY";
			this.oQuestion.Init(this.CurPageId, 0, true);
			this.txtCity.Text = this.oQuestion.QDefine.QUESTION_TITLE + "：";
			SurveyHelper.StopFillPage = "";
			this.method_2();
			SurveyHelper.AutoDo_listCity.Clear();
			this.method_3(this.listButton[0], e);
			this.txtStartNumber.Focus();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		private void btnDo_Click(object sender, RoutedEventArgs e)
		{
			int num = SurveyHelper.AutoDo_listCity.Count * this.oFunc.StringToInt(this.txtCount.Text.Trim());
			if (num == 0)
			{
				MessageBox.Show(SurveyMsg.MsgAutoDo_NoCase, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (MessageBox.Show(string.Format(SurveyMsg.MsgAutoDo_Ask, num.ToString()), SurveyMsg.MsgAutoDo_AskTitle, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				this.btnDo.IsEnabled = false;
				SurveyHelper.AutoDo_Start = DateTime.Now;
				SurveyHelper.AutoDo = true;
				SurveyHelper.AutoFill = true;
				SurveyHelper.AutoDo_CityOrder = 0;
				SurveyHelper.AutoDo_StartOrder = this.oFunc.StringToInt(this.txtStartNumber.Text.Trim());
				SurveyHelper.AutoDo_Total = this.oFunc.StringToInt(this.txtCount.Text.Trim());
				SurveyHelper.AutoDo_Count = 0;
				string byCodeText = this.oSurveyConfigBiz.GetByCodeText("PCCode");
				if (byCodeText == null || byCodeText == "")
				{
					MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
				string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText("SurveyIDBegin");
				string byCodeText3 = this.oSurveyConfigBiz.GetByCodeText("SurveyIDEnd");
				string byCodeText4 = this.oSurveyConfigBiz.GetByCodeText("TouchPad");
				if (byCodeText2 != "")
				{
					SurveyHelper.SurveyCity = byCodeText2.Substring(0, byCodeText2.Length - SurveyMsg.SurveyIDEnd.Length);
					SurveyMsg.SurveyIDBegin = byCodeText2;
					SurveyMsg.SurveyIDEnd = byCodeText3;
					SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
					if (byCodeText4 == "1")
					{
						SurveyHelper.IsTouch = "IsTouch_true";
					}
					else
					{
						SurveyHelper.IsTouch = "IsTouch_false";
					}
				}
				SurveyHelper.SurveyStart = "CITY";
				MainWindow mainWindow = new MainWindow();
				mainWindow.ShowDialog();
				mainWindow.Close();
			}
		}

		private void method_1(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgRightTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_2()
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			WrapPanel wrapPanel = this.wpCity;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = "b_" + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(15.0, 0.0, 0.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.CODE;
				button.Click += this.method_3;
				button.FontSize = 18.0;
				button.MinWidth = 80.0;
				button.MinHeight = 30.0;
				wrapPanel.Children.Add(button);
				this.listButton.Add(button);
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			string item = button.Tag.ToString();
			if (button.Style == style)
			{
				SurveyHelper.AutoDo_listCity.Remove(item);
				button.Style = style2;
			}
			else
			{
				SurveyHelper.AutoDo_listCity.Add(item);
				button.Style = style;
			}
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void btnUnSel_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			foreach (Button button in this.listButton)
			{
				button.Style = style;
			}
			SurveyHelper.AutoDo_listCity.Clear();
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void btnSelAll_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			SurveyHelper.AutoDo_listCity.Clear();
			foreach (Button button in this.listButton)
			{
				button.Style = style;
				string item = button.Tag.ToString();
				SurveyHelper.AutoDo_listCity.Add(item);
			}
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void btnFillMode_Click(object sender, RoutedEventArgs e)
		{
			new FillMode().ShowDialog();
		}

		private void txtStartNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.txtCount.Focus();
			}
		}

		private void txtStartNumber_LostFocus(object sender, RoutedEventArgs e)
		{
			this.oSurveyConfigBiz.Save("AutoDo_StartNumber", this.txtStartNumber.Text);
		}

		private void txtCount_LostFocus(object sender, RoutedEventArgs e)
		{
			this.oSurveyConfigBiz.Save("AutoDo_Count", this.txtCount.Text);
		}

		private void txtCount_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtCount.SelectAll();
		}

		private void txtStartNumber_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtStartNumber.SelectAll();
		}

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();

		private string CurPageId;

		private QMultiple oQuestion = new QMultiple();

		private List<Button> listButton = new List<Button>();
	}
}
