using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace LoyalFilial.MarketResearch.QEdit
{
	public partial class EditAttachments : Window
	{
		public EditAttachments()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			if (SurveyHelper.AttachReadOnlyModel)
			{
				this.btnSelectAttach.Visibility = Visibility.Hidden;
				this.btnAddAttach.Visibility = Visibility.Hidden;
				this.btnRemoveAttach.Visibility = Visibility.Hidden;
			}
			this.txtQuestionTitle.Text = SurveyHelper.AttachQName;
			this.method_1();
		}

		private void btnSelectAttach_Click(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
			commonOpenFileDialog.EnsureReadOnly = true;
			commonOpenFileDialog.Title = SurveyMsg.MsgCaption;
			if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				this.txtAttach.Text = commonOpenFileDialog.FileName;
			}
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void btnAddAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class64 @class = new EditAttachments.Class64();
			string text = this.txtAttach.Text;
			if (text == "C:\\")
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			int num = text.LastIndexOf("\\");
			text.Substring(0, num);
			@class.strFileName = text.Substring(num + 1);
			int num2 = @class.strFileName.LastIndexOf(".");
			string text2 = @class.strFileName.Substring(num2 + 1);
			if (this.oListSource.FindIndex(new Predicate<SurveyAttach>(@class.method_0)) > -1)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgAttachSame, @class.strFileName), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			SurveyAttach surveyAttach = new SurveyAttach();
			surveyAttach.SURVEY_ID = SurveyHelper.AttachSurveyId;
			surveyAttach.PAGE_ID = SurveyHelper.AttachPageId;
			surveyAttach.QUESTION_NAME = SurveyHelper.AttachQName;
			surveyAttach.FILE_NAME = string.Format("{0}_{1}_{2:MMdd_HHmmss}.{3}", new object[]
			{
				SurveyHelper.AttachSurveyId,
				SurveyHelper.AttachQName,
				DateTime.Now,
				text2
			});
			surveyAttach.FILE_TYPE = text2;
			surveyAttach.ORIGINAL_NAME = @class.strFileName;
			string destFileName = Environment.CurrentDirectory + "\\Output\\" + surveyAttach.FILE_NAME;
			if (!Directory.Exists(Environment.CurrentDirectory + "\\Output\\"))
			{
				Directory.CreateDirectory(Environment.CurrentDirectory + "\\Output\\");
			}
			try
			{
				File.Copy(text, destFileName, true);
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgAttachCopyFail, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			this.oSurveyAttachDal.Add(surveyAttach);
			this.oListSource.Add(surveyAttach);
			this.ListAttach.Items.Add(@class.strFileName);
		}

		private void btnRemoveAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class65 @class = new EditAttachments.Class65();
			@class.strOriginalName = this.txtSelectedAttach.Text;
			if (@class.strOriginalName == "")
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			int num = this.oListSource.FindIndex(new Predicate<SurveyAttach>(@class.method_0));
			if (num < 0)
			{
				return;
			}
			string file_NAME = this.oListSource[num].FILE_NAME;
			string text = Environment.CurrentDirectory + "\\OutPut\\" + file_NAME;
			string destFileName = Environment.CurrentDirectory + "\\UnSave\\" + file_NAME;
			try
			{
				if (!Directory.Exists(Environment.CurrentDirectory + "\\UnSave"))
				{
					Directory.CreateDirectory(Environment.CurrentDirectory + "\\UnSave");
				}
				File.Copy(text, destFileName, true);
				File.Delete(text);
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgAttachDelFail, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			this.oSurveyAttachDal.DeleteByQNameByFileName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName, file_NAME);
			this.oListSource.RemoveAt(num);
			this.ListAttach.Items.Remove(@class.strOriginalName);
		}

		private void ListAttach_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.txtSelectedAttach.Text = (string)this.ListAttach.SelectedValue;
		}

		private void method_1()
		{
			this.oListSource = this.oSurveyAttachDal.GetListByQName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName);
			this.ListAttach.Items.Clear();
			foreach (SurveyAttach surveyAttach in this.oListSource)
			{
				this.ListAttach.Items.Add(surveyAttach.ORIGINAL_NAME);
			}
		}

		private void btnOpenAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class66 @class = new EditAttachments.Class66();
			@class.strOriginalName = this.txtSelectedAttach.Text;
			if (@class.strOriginalName == "")
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			int num = this.oListSource.FindIndex(new Predicate<SurveyAttach>(@class.method_0));
			if (num < 0)
			{
				return;
			}
			string file_NAME = this.oListSource[num].FILE_NAME;
			string text = Environment.CurrentDirectory + "\\Output\\" + file_NAME;
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			MessageBox.Show(string.Format(SurveyMsg.MsgAttachNotExist, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		private List<SurveyAttach> oListSource = new List<SurveyAttach>();

		[CompilerGenerated]
		private sealed class Class64
		{
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strFileName;
			}

			public string strFileName;
		}

		[CompilerGenerated]
		private sealed class Class65
		{
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strOriginalName;
			}

			public string strOriginalName;
		}

		[CompilerGenerated]
		private sealed class Class66
		{
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strOriginalName;
			}

			public string strOriginalName;
		}
	}
}
