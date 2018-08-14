using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.QEdit
{
	// Token: 0x02000058 RID: 88
	public partial class EditAnswer : Window
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x00003B77 File Offset: 0x00001D77
		public EditAnswer()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000993A8 File Offset: 0x000975A8
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.txtQuestionTitle.Text = SurveyHelper.QueryEditQTitle;
			this.txtTitle.Text = SurveyHelper.QueryEditQName;
			if (SurveyHelper.QueryEditDetailID != global::GClass0.smethod_0(""))
			{
				SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
				this.lOptions = surveyDetailDal.GetDetails(SurveyHelper.QueryEditDetailID);
				this.ListSelect.ItemsSource = this.lOptions;
				this.ListSelect.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
				base.Height = 450.0;
				this.ListSelect.Visibility = Visibility.Visible;
				this.txtAnswer.Text = SurveyHelper.QueryEditCODE + global::GClass0.smethod_0("）") + SurveyHelper.QueryEditCODE_TEXT + global::GClass0.smethod_0("（");
				this.txtNewAnswer.IsEnabled = false;
				this.ListSelect.Focus();
				return;
			}
			this.ListSelect.Visibility = Visibility.Collapsed;
			base.Height = 350.0;
			this.txtAnswer.Text = SurveyHelper.QueryEditCODE;
			this.txtNewAnswer.Focus();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000994D8 File Offset: 0x000976D8
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtNewAnswer.Text;
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			SurveyHelper.QueryEditCODE = text;
			if (SurveyHelper.QueryEditDetailID != global::GClass0.smethod_0(""))
			{
				text = this.ListSelect.SelectedValue.ToString();
				foreach (SurveyDetail surveyDetail in this.lOptions)
				{
					if (text == surveyDetail.CODE)
					{
						SurveyHelper.QueryEditCODE_TEXT = surveyDetail.CODE_TEXT;
						break;
					}
				}
			}
			if (!SurveyHelper.QueryEditMemModel)
			{
				new SurveyAnswerDal().AddOne(SurveyHelper.QueryEditSurveyId, SurveyHelper.QueryEditQName, text, SurveyHelper.QueryEditSequence);
			}
			Logging.Data.WriteLog(global::GClass0.smethod_0("柬諪䷩昿嚚藹拎䡞࠻"), string.Concat(new string[]
			{
				SurveyHelper.QueryEditSurveyId,
				global::GClass0.smethod_0("$Įȯ̡"),
				SurveyHelper.QueryEditQName,
				global::GClass0.smethod_0("$Įȯ̡"),
				this.txtAnswer.Text,
				global::GClass0.smethod_0("%ĩȮ̼С"),
				text
			}));
			SurveyHelper.QueryEditConfirm = true;
			base.Close();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00003B90 File Offset: 0x00001D90
		private void txtNewAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnSave.IsEnabled)
			{
				this.btnSave_Click(sender, e);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00003BB0 File Offset: 0x00001DB0
		private void ListSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.txtNewAnswer.Text = (string)this.ListSelect.SelectedValue;
		}

		// Token: 0x04000AA4 RID: 2724
		private List<SurveyDetail> lOptions = new List<SurveyDetail>();
	}
}
