using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using Xceed.Wpf.DataGrid;

namespace LoyalFilial.MarketResearch.View
{
	public partial class SurveyQuery : System.Windows.Controls.Page, IStyleConnector
	{
		public List<V_SurveyQC> CurrentSurvey { get; private set; }

		public List<SurveyMain> ListSurveyMain { get; private set; }

		public SurveyMain CurrentSurveyMain { get; private set; }

		public List<SurveyAnswer> ListRecord { get; private set; }

		public SurveyQuery()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.StkRecord.Visibility = Visibility.Collapsed;
			SurveyHelper.TestVersion = false;
			string surveyID = SurveyHelper.SurveyID;
			if (!this.method_3(surveyID))
			{
				this.IsShort = true;
				this.checkBox1.IsChecked = new bool?(true);
				this.method_3(surveyID);
			}
			if (surveyID != "")
			{
				this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode + " : " + surveyID;
				this.method_4(surveyID);
			}
		}

		private void DataGrid1_Loaded(object sender, RoutedEventArgs e)
		{
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
			{
				this.GroupRecord.Visibility = Visibility.Visible;
				this.DataGrid1.Columns["RecordRowColumn"].Visible = true;
			}
			if (SurveyMsg.FunctionQueryEdit == "FunctionQueryEdit_true")
			{
				this.DataGrid1.Columns["EditRowColumn"].Visible = true;
			}
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true")
			{
				this.DataGrid1.Columns["AttachRowColumn"].Visible = true;
			}
		}

		private void method_1(object sender, RoutedEventArgs e)
		{
			if (this.StkRecord.Visibility == Visibility.Visible && (string)this.playBtn.Content == SurveyMsg.MsgPause)
			{
				this.method_8();
			}
			if (this.StkRecord.Visibility == Visibility.Visible)
			{
				this.mediaElement.Close();
			}
			Cell cell = Cell.FindFromChild(sender as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl(cell).GetItemFromContainer(cell.ParentRow);
			SurveySequence audioByPageId = this.oSurvey.GetAudioByPageId(v_SurveyQC.SURVEY_ID, v_SurveyQC.SEQUENCE_ID, v_SurveyQC.PAGE_ID);
			if (!(audioByPageId.RECORD_FILE == "") && audioByPageId.RECORD_FILE != null)
			{
				this.txtQName.Text = v_SurveyQC.QUESTION_TITLE;
				this.method_2(audioByPageId);
				return;
			}
			this.StkRecord.Visibility = Visibility.Collapsed;
			MessageBox.Show(SurveyMsg.MsgQuestionNoRecord, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_2(SurveySequence surveySequence_0)
		{
			string record_FILE = surveySequence_0.RECORD_FILE;
			string text = record_FILE.Replace(".wav", ".mp3");
			string str = Environment.CurrentDirectory + "\\Record";
			string text2 = Environment.CurrentDirectory + "\\Mp3" + "\\" + text;
			if (!File.Exists(text2))
			{
				text2 = str + "\\" + record_FILE;
				if (!File.Exists(text2))
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNoRecordFile, text, record_FILE), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
			}
			this.StkRecord.Visibility = Visibility.Visible;
			this.mediaElement.Source = new Uri(text2, UriKind.Relative);
			int seconds = surveySequence_0.RECORD_BEGIN_TIME;
			int num = 1;
			if (surveySequence_0.RECORD_BEGIN_TIME > 1)
			{
				seconds = surveySequence_0.RECORD_BEGIN_TIME - num;
			}
			TimeSpan position = new TimeSpan(0, 0, seconds);
			this.mediaElement.Position = position;
			this.txtPlace.Text = SurveyMsg.MsgStartAt + position.ToString();
			this.method_8();
		}

		private bool method_3(string string_0)
		{
			bool result = false;
			if (this.IsShort)
			{
				this.ListSurveyMain = this.oSurvey.GetSurveyMainList(2);
			}
			else
			{
				this.ListSurveyMain = this.oSurvey.GetSurveyMainList(1);
			}
			this.cmbList.ItemsSource = this.ListSurveyMain;
			this.cmbList.DisplayMemberPath = "SURVEY_ID";
			this.cmbList.SelectedValuePath = "SURVEY_ID";
			int num = 0;
			if (string_0 == "")
			{
				if (this.ListSurveyMain.Count > 0)
				{
					this.cmbList.SelectedIndex = 0;
					result = true;
				}
			}
			else
			{
				using (List<SurveyMain>.Enumerator enumerator = this.ListSurveyMain.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.SURVEY_ID == string_0)
						{
							this.cmbList.SelectedIndex = num;
							result = true;
							break;
						}
						num++;
					}
				}
			}
			return result;
		}

		private void method_4(string string_0)
		{
			this.CurrentSurveyMain = this.oSurvey.GetSurveyMainListBySurveyId(string_0);
			this.oSurvey.GetSurveyyQC(string_0);
			this.CurrentSurvey = this.oSurvey.QVSurveyQC;
			this.ListAnswer = this.oSurveyAnswerDal.GetListBySurveyId(string_0);
			this.DataGrid1.ItemsSource = this.CurrentSurvey;
		}

		private void btnQuery_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsEdit)
			{
				if (MessageBox.Show(SurveyMsg.MsgQueryEditNoSave, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				this.IsEdit = false;
			}
			if (this.cmbList.SelectedValue != null && !((string)this.cmbList.SelectedValue == ""))
			{
				string text = this.cmbList.SelectedValue.ToString();
				this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode + " : " + text;
				this.method_4(text);
				this.SurveyID = text;
				MessageBox.Show(SurveyMsg.MsgReadSurveyOK, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNotSelectedSurvey, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void btnXml_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(SurveyMsg.MsgExportOverwrite, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
			{
				return;
			}
			string text = this.cmbList.SelectedValue.ToString();
			SurveytoXml surveytoXml = new SurveytoXml();
			string text2 = Environment.CurrentDirectory;
			if (SurveyMsg.FunctionQueryEdit == "FunctionQueryEdit_true")
			{
				text2 = Environment.CurrentDirectory + "\\Output\\Modify";
				surveytoXml.OutputPath = "";
			}
			surveytoXml.SaveSurveyAnswer(text, text2, this.ListAnswer, true);
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
			{
				surveytoXml.SaveSurveySequence(text, text2, null, true);
			}
			Logging.Data.WriteLog("查询导出Dat操作:", string.Format("导出问卷 {0} Dat 文件.", text));
			MessageBox.Show(string.Format(SurveyMsg.MsgExportDatFile, text, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.IsEdit = false;
		}

		private void btnAllDat_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(SurveyMsg.MsgExportOverwrite, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
			{
				return;
			}
			new CheckPsw().ShowDialog();
			if (!SurveyMsg.SurveyRangePswOk)
			{
				return;
			}
			SurveyMsg.SurveyRangePswOk = false;
			bool flag = this.checkBox1.IsChecked == true;
			int int_ = 1;
			if (flag)
			{
				int_ = 2;
			}
			List<SurveyMain> surveyMain = new SurveyMainDal().GetSurveyMain(int_);
			SurveytoXml surveytoXml = new SurveytoXml();
			string text = Environment.CurrentDirectory;
			foreach (SurveyMain surveyMain2 in surveyMain)
			{
				surveytoXml.SaveSurveyAnswer(surveyMain2.SURVEY_ID, text, null, true);
				if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
				{
					surveytoXml.SaveSurveySequence(surveyMain2.SURVEY_ID, text, null, true);
				}
				if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true")
				{
					surveytoXml.SaveSurveyAttach(surveyMain2.SURVEY_ID, text, null, true);
				}
				Logging.Data.WriteLog("问卷输出:", string.Format(" {0} 输出完成！", surveyMain2.SURVEY_ID));
			}
			text += "\\Output";
			MessageBox.Show(string.Format(SurveyMsg.MsgOutputOk, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_5(object sender, RoutedEventArgs e)
		{
			Cell cell = Cell.FindFromChild(sender as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl(cell).GetItemFromContainer(cell.ParentRow);
			bool flag = false;
			if (v_SurveyQC.ANSWER_USE == 1)
			{
				flag = true;
				if (v_SurveyQC.QUESTION_NAME == "SURVEY_CODE")
				{
					flag = false;
				}
			}
			if (!flag)
			{
				MessageBox.Show(SurveyMsg.MsgNotEdit, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			SurveyHelper.QueryEditSurveyId = v_SurveyQC.SURVEY_ID;
			SurveyHelper.QueryEditQTitle = v_SurveyQC.QUESTION_TITLE;
			SurveyHelper.QueryEditQName = v_SurveyQC.QUESTION_NAME;
			SurveyHelper.QueryEditDetailID = v_SurveyQC.DETAIL_ID;
			SurveyHelper.QueryEditCODE = v_SurveyQC.CODE;
			SurveyHelper.QueryEditCODE_TEXT = v_SurveyQC.CODE_TEXT;
			SurveyHelper.QueryEditSequence = v_SurveyQC.SEQUENCE_ID;
			SurveyHelper.QueryEditMemModel = true;
			new EditAnswer().ShowDialog();
			if (SurveyHelper.QueryEditConfirm)
			{
				this.IsEdit = true;
				for (int i = 0; i < this.CurrentSurvey.Count; i++)
				{
					if (this.CurrentSurvey[i].QUESTION_NAME == SurveyHelper.QueryEditQName)
					{
						this.CurrentSurvey[i].CODE = SurveyHelper.QueryEditCODE;
						this.CurrentSurvey[i].CODE_TEXT = SurveyHelper.QueryEditCODE_TEXT + SurveyMsg.MsgEditNote;
						
						int selectedIndex = this.DataGrid1.SelectedIndex;
						this.DataGrid1.ItemsSource = null;
						this.DataGrid1.ItemsSource = this.CurrentSurvey;
						this.DataGrid1.SelectedIndex = selectedIndex;
						for (int j = 0; j < this.ListAnswer.Count; j++)
						{
							if (this.ListAnswer[j].QUESTION_NAME == SurveyHelper.QueryEditQName)
							{
								this.ListAnswer[j].CODE = SurveyHelper.QueryEditCODE;
								
								SurveyHelper.QueryEditConfirm = false;
								return;
							}
						}
					}
				}
			}
		}

		private void method_6(object sender, RoutedEventArgs e)
		{
			Cell cell = Cell.FindFromChild(sender as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl(cell).GetItemFromContainer(cell.ParentRow);
			SurveyHelper.AttachReadOnlyModel = true;
			SurveyHelper.AttachSurveyId = v_SurveyQC.SURVEY_ID;
			SurveyHelper.AttachQName = v_SurveyQC.QUESTION_NAME;
			SurveyHelper.AttachPageId = v_SurveyQC.PAGE_ID;
			this.oListAttach = this.oSurveyAttachDal.GetListByQName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName);
			SurveyHelper.AttachCount = this.oListAttach.Count<SurveyAttach>();
			if (SurveyHelper.AttachCount == 0)
			{
				MessageBox.Show(string.Format("本题 {0} 没有附件！", v_SurveyQC.QUESTION_NAME), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			new EditAttachments().ShowDialog();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsEdit)
			{
				if (MessageBox.Show(SurveyMsg.MsgQueryEditNoSave, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				this.IsEdit = false;
			}
			System.Windows.Application.Current.Shutdown();
		}

		private void checkBox1_Checked(object sender, RoutedEventArgs e)
		{
			if (this.IsEdit)
			{
				if (MessageBox.Show(SurveyMsg.MsgQueryEditNoSave, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				this.IsEdit = false;
			}
			this.IsShort = true;
			this.method_3("");
		}

		private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
		{
			if (this.IsEdit)
			{
				if (MessageBox.Show(SurveyMsg.MsgQueryEditNoSave, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				this.IsEdit = false;
			}
			this.IsShort = false;
			this.method_3("");
		}

		private void method_7(string string_0)
		{
			string text = Environment.CurrentDirectory + "\\Excel";
			string str = string.Format("QCSurvey{0}.xlsx", string_0);
			string text2 = text + "\\" + str;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			if (application == null)
			{
				return;
			}
			Workbooks workbooks = application.Workbooks;
			Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
			if (SurveyQuery.Class60.callSite == null)
			{
				SurveyQuery.Class60.callSite = CallSite<Func<CallSite, object, Worksheet>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Worksheet), typeof(SurveyQuery)));
			}
			Worksheet worksheet = SurveyQuery.Class60.callSite.Target(SurveyQuery.Class60.callSite, workbook.Worksheets[1]);
			string[] value = new string[]
			{
				"题目",
				"编码",
				"中文"
			};
			if (SurveyQuery.Class60.callSite3 == null)
			{
				SurveyQuery.Class60.callSite3 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Range), typeof(SurveyQuery)));
			}
			Func<CallSite, object, Range> target = SurveyQuery.Class60.callSite3.Target;
			CallSite callSite = SurveyQuery.Class60.callSite3;
			if (SurveyQuery.Class60.callSite2 == null)
			{
				SurveyQuery.Class60.callSite2 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof(SurveyQuery), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, object, object, object> target2 = SurveyQuery.Class60.callSite2.Target;
			CallSite callSite2 = SurveyQuery.Class60.callSite2;
			if (SurveyQuery.Class60.callSite1 == null)
			{
				SurveyQuery.Class60.callSite1 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Range", typeof(SurveyQuery), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			target(callSite, target2(callSite2, SurveyQuery.Class60.callSite1.Target(SurveyQuery.Class60.callSite1, worksheet), worksheet.Cells[1, 1], worksheet.Cells[1, 3])).set_Value(Type.Missing, value);
			int count = this.CurrentSurvey.Count;
			string[,] array = new string[count, 3];
			int num = 0;
			foreach (V_SurveyQC v_SurveyQC in this.CurrentSurvey)
			{
				array[num, 0] = v_SurveyQC.QUESTION_TITLE;
				array[num, 1] = v_SurveyQC.CODE;
				array[num, 2] = v_SurveyQC.CODE_TEXT;
				num++;
			}
			if (SurveyQuery.Class60.callSite6 == null)
			{
				SurveyQuery.Class60.callSite6 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Range), typeof(SurveyQuery)));
			}
			Func<CallSite, object, Range> target3 = SurveyQuery.Class60.callSite6.Target;
			CallSite callSite3 = SurveyQuery.Class60.callSite6;
			if (SurveyQuery.Class60.callSite5 == null)
			{
				SurveyQuery.Class60.callSite5 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof(SurveyQuery), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, object, object, object> target4 = SurveyQuery.Class60.callSite5.Target;
			CallSite callSite4 = SurveyQuery.Class60.callSite5;
			if (SurveyQuery.Class60.callSite4 == null)
			{
				SurveyQuery.Class60.callSite4 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Range", typeof(SurveyQuery), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			target3(callSite3, target4(callSite4, SurveyQuery.Class60.callSite4.Target(SurveyQuery.Class60.callSite4, worksheet), worksheet.Cells[2, 1], worksheet.Cells[count + 1, 3])).set_Value(Type.Missing, array);
			try
			{
				workbook.Saved = true;
				workbook.SaveCopyAs(text2);
			}
			catch (Exception)
			{
			}
			finally
			{
				workbook.Close(true, Type.Missing, Type.Missing);
				workbook = null;
				workbooks.Close();
				workbooks = null;
				new ExcelHelper().KillExcelProcess(application);
				application = null;
			}
			MessageBox.Show(string.Format(SurveyMsg.MsgExcelOutputDone, string_0, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void btnExcel_Click(object sender, RoutedEventArgs e)
		{
			if (!new ExcelHelper().CheckExcelInstall())
			{
				MessageBox.Show(SurveyMsg.MsgNoExcel, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (MessageBox.Show(SurveyMsg.MsgExcelOutput, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
			{
				string surveyID = this.SurveyID;
				this.method_7(surveyID);
			}
		}

		private void openBtn_Click(object sender, RoutedEventArgs e)
		{
		}

		private void method_8()
		{
			if (this.playBtn.Content.ToString() == SurveyMsg.MsgPlay)
			{
				this.mediaElement.Play();
				this.playBtn.Content = SurveyMsg.MsgPause;
				this.mediaElement.ToolTip = "Click to Pause";
				return;
			}
			this.mediaElement.Pause();
			this.playBtn.Content = SurveyMsg.MsgPlay;
			this.mediaElement.ToolTip = "Click to Play";
		}

		private void playBtn_Click(object sender, RoutedEventArgs e)
		{
			this.method_8();
		}

		private void method_9(object sender, MouseButtonEventArgs e)
		{
			this.method_8();
		}

		private void backBtn_Click(object sender, RoutedEventArgs e)
		{
			this.mediaElement.Position = this.mediaElement.Position - TimeSpan.FromSeconds(10.0);
			this.txtPlace.Text = SurveyMsg.MsgStartAt + this.mediaElement.Position.ToString();
		}

		private void forwardBtn_Click(object sender, RoutedEventArgs e)
		{
			this.mediaElement.Position = this.mediaElement.Position + TimeSpan.FromSeconds(10.0);
			this.txtPlace.Text = SurveyMsg.MsgStartAt + this.mediaElement.Position.ToString();
		}

		private void stopBtn_Click(object sender, RoutedEventArgs e)
		{
			this.method_8();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 18:
				((System.Windows.Controls.Button)target).Click += this.method_1;
				return;
			case 19:
				((System.Windows.Controls.Button)target).Click += this.method_5;
				return;
			case 20:
				((System.Windows.Controls.Button)target).Click += this.method_6;
				return;
			default:
				return;
			}
		}

		private string SurveyID;

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		public NavBase MyNav = new NavBase();

		private SurveyBiz oSurvey = new SurveyBiz();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private bool IsShort;

		private bool IsEdit;

		private bool IsXml;

		public List<SurveyAnswer> ListAnswer = new List<SurveyAnswer>();

		public List<SurveySequence> ListSequence = new List<SurveySequence>();

		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		private List<SurveyAttach> oListAttach = new List<SurveyAttach>();

		private DispatcherTimer timer = new DispatcherTimer();

		[CompilerGenerated]
		private static class Class60
		{
			public static CallSite<Func<CallSite, object, Worksheet>> callSite;

			public static CallSite<Func<CallSite, Worksheet, object>> callSite1;

			public static CallSite<Func<CallSite, object, object, object, object>> callSite2;

			public static CallSite<Func<CallSite, object, Range>> callSite3;

			public static CallSite<Func<CallSite, Worksheet, object>> callSite4;

			public static CallSite<Func<CallSite, object, object, object, object>> callSite5;

			public static CallSite<Func<CallSite, object, Range>> callSite6;
		}
	}
}
