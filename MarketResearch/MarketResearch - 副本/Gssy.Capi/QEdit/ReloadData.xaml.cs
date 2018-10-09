using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;

namespace Gssy.Capi.QEdit
{
	public partial class ReloadData : Window
	{
		public ReloadData()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText("ReloadData");
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
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			SurveyHelper.StopFillPage = this.StopFillPageId.Text;
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

		private List<Button> listBtn = new List<Button>();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();
	}
}
