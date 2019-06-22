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
	public partial class EditAnswer : Window
	{
		public EditAnswer()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.txtQuestionTitle.Text = SurveyHelper.QueryEditQTitle;
			this.txtTitle.Text = SurveyHelper.QueryEditQName;
			if (SurveyHelper.QueryEditDetailID != "")
			{
				SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
				this.lOptions = surveyDetailDal.GetDetails(SurveyHelper.QueryEditDetailID);
				this.ListSelect.ItemsSource = this.lOptions;
				this.ListSelect.SelectedValuePath = "CODE";
				base.Height = 450.0;
				this.ListSelect.Visibility = Visibility.Visible;
				this.txtAnswer.Text = SurveyHelper.QueryEditCODE +"（" + SurveyHelper.QueryEditCODE_TEXT + "）";
				this.txtNewAnswer.IsEnabled = false;
				this.ListSelect.Focus();
				return;
			}
			this.ListSelect.Visibility = Visibility.Collapsed;
			base.Height = 350.0;
			this.txtAnswer.Text = SurveyHelper.QueryEditCODE;
			this.txtNewAnswer.Focus();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtNewAnswer.Text;
			if (text == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			SurveyHelper.QueryEditCODE = text;
			if (SurveyHelper.QueryEditDetailID != "")
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
			Logging.Data.WriteLog("查询修改功能操作:", string.Concat(new string[]
			{
				SurveyHelper.QueryEditSurveyId,
				" -- ",
				SurveyHelper.QueryEditQName,
				" -- ",
				this.txtAnswer.Text,
				" --> ",
				text
			}));
			SurveyHelper.QueryEditConfirm = true;
			base.Close();
		}

		private void txtNewAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnSave.IsEnabled)
			{
				this.btnSave_Click(sender, e);
			}
		}

		private void ListSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.txtNewAnswer.Text = (string)this.ListSelect.SelectedValue;
		}

		private List<SurveyDetail> lOptions = new List<SurveyDetail>();
	}
}
