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
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Gssy.Capi.QEdit
{
	// Token: 0x0200005A RID: 90
	public partial class EditAttachments : Window
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00003C0A File Offset: 0x00001E0A
		public EditAttachments()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0009A374 File Offset: 0x00098574
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

		// Token: 0x060005F3 RID: 1523 RVA: 0x0009A3D8 File Offset: 0x000985D8
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

		// Token: 0x060005F4 RID: 1524 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0009A418 File Offset: 0x00098618
		private void btnAddAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class64 @class = new EditAttachments.Class64();
			string text = this.txtAttach.Text;
			if (text == global::GClass0.smethod_0("@ĸɝ"))
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			int num = text.LastIndexOf(global::GClass0.smethod_0("]"));
			text.Substring(0, num);
			@class.strFileName = text.Substring(num + 1);
			int num2 = @class.strFileName.LastIndexOf(global::GClass0.smethod_0("/"));
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
			surveyAttach.FILE_NAME = string.Format(global::GClass0.smethod_0("`Īɤ͇Ѭԧ٨݋ࡨठਫଢ଼ూ൪๩ན၃ᅂቤ፥ᑴᕵᙸᜪᡸᤱ᩼"), new object[]
			{
				SurveyHelper.AttachSurveyId,
				SurveyHelper.AttachQName,
				DateTime.Now,
				text2
			});
			surveyAttach.FILE_TYPE = text2;
			surveyAttach.ORIGINAL_NAME = @class.strFileName;
			string destFileName = Environment.CurrentDirectory + global::GClass0.smethod_0("TňɳͱѴնٶݝ") + surveyAttach.FILE_NAME;
			if (!Directory.Exists(Environment.CurrentDirectory + global::GClass0.smethod_0("TňɳͱѴնٶݝ")))
			{
				Directory.CreateDirectory(Environment.CurrentDirectory + global::GClass0.smethod_0("TňɳͱѴնٶݝ"));
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

		// Token: 0x060005F6 RID: 1526 RVA: 0x0009A620 File Offset: 0x00098820
		private void btnRemoveAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class65 @class = new EditAttachments.Class65();
			@class.strOriginalName = this.txtSelectedAttach.Text;
			if (@class.strOriginalName == global::GClass0.smethod_0(""))
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
			string text = Environment.CurrentDirectory + global::GClass0.smethod_0("Tňɳͱєնٶݝ") + file_NAME;
			string destFileName = Environment.CurrentDirectory + global::GClass0.smethod_0("TŒɨ͖ѥյ٧ݝ") + file_NAME;
			try
			{
				if (!Directory.Exists(Environment.CurrentDirectory + global::GClass0.smethod_0("[œɫ͗Ѣմ٤")))
				{
					Directory.CreateDirectory(Environment.CurrentDirectory + global::GClass0.smethod_0("[œɫ͗Ѣմ٤"));
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

		// Token: 0x060005F7 RID: 1527 RVA: 0x00003C2E File Offset: 0x00001E2E
		private void ListAttach_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.txtSelectedAttach.Text = (string)this.ListAttach.SelectedValue;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0009A778 File Offset: 0x00098978
		private void method_1()
		{
			this.oListSource = this.oSurveyAttachDal.GetListByQName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName);
			this.ListAttach.Items.Clear();
			foreach (SurveyAttach surveyAttach in this.oListSource)
			{
				this.ListAttach.Items.Add(surveyAttach.ORIGINAL_NAME);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0009A808 File Offset: 0x00098A08
		private void btnOpenAttach_Click(object sender, RoutedEventArgs e)
		{
			EditAttachments.Class66 @class = new EditAttachments.Class66();
			@class.strOriginalName = this.txtSelectedAttach.Text;
			if (@class.strOriginalName == global::GClass0.smethod_0(""))
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
			string text = Environment.CurrentDirectory + global::GClass0.smethod_0("TňɳͱѴնٶݝ") + file_NAME;
			if (File.Exists(text))
			{
				Process.Start(text);
				return;
			}
			MessageBox.Show(string.Format(SurveyMsg.MsgAttachNotExist, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x04000AD1 RID: 2769
		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		// Token: 0x04000AD2 RID: 2770
		private List<SurveyAttach> oListSource = new List<SurveyAttach>();

		// Token: 0x020000BF RID: 191
		[CompilerGenerated]
		private sealed class Class64
		{
			// Token: 0x060007A6 RID: 1958 RVA: 0x000046D9 File Offset: 0x000028D9
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strFileName;
			}

			// Token: 0x04000D48 RID: 3400
			public string strFileName;
		}

		// Token: 0x020000C0 RID: 192
		[CompilerGenerated]
		private sealed class Class65
		{
			// Token: 0x060007A8 RID: 1960 RVA: 0x000046EC File Offset: 0x000028EC
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strOriginalName;
			}

			// Token: 0x04000D49 RID: 3401
			public string strOriginalName;
		}

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		private sealed class Class66
		{
			// Token: 0x060007AA RID: 1962 RVA: 0x000046FF File Offset: 0x000028FF
			internal bool method_0(SurveyAttach surveyAttach_0)
			{
				return surveyAttach_0.ORIGINAL_NAME == this.strOriginalName;
			}

			// Token: 0x04000D4A RID: 3402
			public string strOriginalName;
		}
	}
}
