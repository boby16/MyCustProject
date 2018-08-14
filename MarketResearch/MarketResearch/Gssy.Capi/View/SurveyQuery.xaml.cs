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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using Xceed.Wpf.DataGrid;

namespace Gssy.Capi.View
{
	// Token: 0x02000050 RID: 80
	public partial class SurveyQuery : System.Windows.Controls.Page, IStyleConnector
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00003886 File Offset: 0x00001A86
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0000388E File Offset: 0x00001A8E
		public List<V_SurveyQC> CurrentSurvey { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00003897 File Offset: 0x00001A97
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0000389F File Offset: 0x00001A9F
		public List<SurveyMain> ListSurveyMain { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000038A8 File Offset: 0x00001AA8
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x000038B0 File Offset: 0x00001AB0
		public SurveyMain CurrentSurveyMain { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000038B9 File Offset: 0x00001AB9
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x000038C1 File Offset: 0x00001AC1
		public List<SurveyAnswer> ListRecord { get; private set; }

		// Token: 0x0600054A RID: 1354 RVA: 0x00090C44 File Offset: 0x0008EE44
		public SurveyQuery()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00090CC0 File Offset: 0x0008EEC0
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
			if (surveyID != global::GClass0.smethod_0(""))
			{
				this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode + global::GClass0.smethod_0("#ĸȡ") + surveyID;
				this.method_4(surveyID);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00090D48 File Offset: 0x0008EF48
		private void DataGrid1_Loaded(object sender, RoutedEventArgs e)
		{
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				this.GroupRecord.Visibility = Visibility.Visible;
				this.DataGrid1.Columns[global::GClass0.smethod_0("]ūɮͣѹծٛݧࡰॅ੪୨౶൯๯")].Visible = true;
			}
			if (SurveyMsg.FunctionQueryEdit == global::GClass0.smethod_0("PŠɺͰѦոٿݡ࡟ॸ੩୹౳ൌ๬཮ၲᅚተ፱ᑷᕤ"))
			{
				this.DataGrid1.Columns[global::GClass0.smethod_0("HŨɢ;ћէٰ݅ࡪ२੶୯౯")].Visible = true;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				this.DataGrid1.Columns[global::GClass0.smethod_0("NźɹͭѨբٛݧࡰॅ੪୨౶൯๯")].Visible = true;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00090E04 File Offset: 0x0008F004
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
			if (!(audioByPageId.RECORD_FILE == global::GClass0.smethod_0("")) && audioByPageId.RECORD_FILE != null)
			{
				this.txtQName.Text = v_SurveyQC.QUESTION_TITLE;
				this.method_2(audioByPageId);
				return;
			}
			this.StkRecord.Visibility = Visibility.Collapsed;
			MessageBox.Show(SurveyMsg.MsgQuestionNoRecord, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00090EF0 File Offset: 0x0008F0F0
		private void method_2(SurveySequence surveySequence_0)
		{
			string record_FILE = surveySequence_0.RECORD_FILE;
			string text = record_FILE.Replace(global::GClass0.smethod_0("*Ŵɣͷ"), global::GClass0.smethod_0("*Ůɲ̲"));
			string str = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŔɠͧѬհ٥");
			string text2 = Environment.CurrentDirectory + global::GClass0.smethod_0("XŎɲ̲") + global::GClass0.smethod_0("]") + text;
			if (!File.Exists(text2))
			{
				text2 = str + global::GClass0.smethod_0("]") + record_FILE;
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

		// Token: 0x0600054F RID: 1359 RVA: 0x00091014 File Offset: 0x0008F214
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
			this.cmbList.DisplayMemberPath = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
			this.cmbList.SelectedValuePath = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
			int num = 0;
			if (string_0 == global::GClass0.smethod_0(""))
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

		// Token: 0x06000550 RID: 1360 RVA: 0x00091120 File Offset: 0x0008F320
		private void method_4(string string_0)
		{
			this.CurrentSurveyMain = this.oSurvey.GetSurveyMainListBySurveyId(string_0);
			this.oSurvey.GetSurveyyQC(string_0);
			this.CurrentSurvey = this.oSurvey.QVSurveyQC;
			this.ListAnswer = this.oSurveyAnswerDal.GetListBySurveyId(string_0);
			this.DataGrid1.ItemsSource = this.CurrentSurvey;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00091180 File Offset: 0x0008F380
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
			if (this.cmbList.SelectedValue != null && !((string)this.cmbList.SelectedValue == global::GClass0.smethod_0("")))
			{
				string text = this.cmbList.SelectedValue.ToString();
				this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode + global::GClass0.smethod_0("#ĸȡ") + text;
				this.method_4(text);
				this.SurveyID = text;
				MessageBox.Show(SurveyMsg.MsgReadSurveyOK, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNotSelectedSurvey, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0009125C File Offset: 0x0008F45C
		private void btnXml_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(SurveyMsg.MsgExportOverwrite, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
			{
				return;
			}
			string text = this.cmbList.SelectedValue.ToString();
			SurveytoXml surveytoXml = new SurveytoXml();
			string text2 = Environment.CurrentDirectory;
			if (SurveyMsg.FunctionQueryEdit == global::GClass0.smethod_0("PŠɺͰѦոٿݡ࡟ॸ੩୹౳ൌ๬཮ၲᅚተ፱ᑷᕤ"))
			{
				text2 = Environment.CurrentDirectory + global::GClass0.smethod_0("RłɹͿѺռټݛࡋ४੠୪౤൸");
				surveytoXml.OutputPath = global::GClass0.smethod_0("");
			}
			surveytoXml.SaveSurveyAnswer(text, text2, this.ListAnswer, true);
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				surveytoXml.SaveSurveySequence(text, text2, null, true);
			}
			Logging.Data.WriteLog(global::GClass0.smethod_0("柯諫姴勽тդٰ揎䝞ऻ"), string.Format(global::GClass0.smethod_0("寬僵韠偺Ьհغݴࠨृ੧ୱత梄䃴༯"), text));
			MessageBox.Show(string.Format(SurveyMsg.MsgExportDatFile, text, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.IsEdit = false;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00091360 File Offset: 0x0008F560
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
				if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
				{
					surveytoXml.SaveSurveySequence(surveyMain2.SURVEY_ID, text, null, true);
				}
				if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ"))
				{
					surveytoXml.SaveSurveyAttach(surveyMain2.SURVEY_ID, text, null, true);
				}
				Logging.Data.WriteLog(global::GClass0.smethod_0("闫剳趐勸л"), string.Format(global::GClass0.smethod_0("*ŲȸͺЦ誖埾岏樒"), surveyMain2.SURVEY_ID));
			}
			text += global::GClass0.smethod_0("[ŉɰͰѳշٵ");
			MessageBox.Show(string.Format(SurveyMsg.MsgOutputOk, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000914F4 File Offset: 0x0008F6F4
		private void method_5(object sender, RoutedEventArgs e)
		{
			Cell cell = Cell.FindFromChild(sender as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl(cell).GetItemFromContainer(cell.ParentRow);
			bool flag = false;
			if (v_SurveyQC.ANSWER_USE == 1)
			{
				flag = true;
				if (v_SurveyQC.QUESTION_NAME == global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄"))
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

		// Token: 0x06000555 RID: 1365 RVA: 0x000916D4 File Offset: 0x0008F8D4
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
				MessageBox.Show(string.Format(global::GClass0.smethod_0("朠馓ȪͲиպئ殤漍齇䓴"), v_SurveyQC.QUESTION_NAME), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			new EditAttachments().ShowDialog();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0009178C File Offset: 0x0008F98C
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

		// Token: 0x06000557 RID: 1367 RVA: 0x000917DC File Offset: 0x0008F9DC
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
			this.method_3(global::GClass0.smethod_0(""));
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0009183C File Offset: 0x0008FA3C
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
			this.method_3(global::GClass0.smethod_0(""));
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0009189C File Offset: 0x0008FA9C
		private void method_7(string string_0)
		{
			string text = Environment.CurrentDirectory + global::GClass0.smethod_0("Zŀɼ͠ѧխ");
			string str = string.Format(global::GClass0.smethod_0("AŌɝ͸Ѿսٯݰࡳष੻ଫ౼൯๱ཹ"), string_0);
			string text2 = text + global::GClass0.smethod_0("]") + str;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(global::GClass0.smethod_0("\u0014ēȒ̓ДԪخܭ࠱फਪ଩నഺฦ༥ဤᄣሿፒᐠᔿᘾᜠᠼ᤻ᨺᬹ᰸ᴷḶἵ‴ℳ∶⌷"))));
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
				global::GClass0.smethod_0("颚矯"),
				global::GClass0.smethod_0("缔礀"),
				global::GClass0.smethod_0("丯撆")
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
				SurveyQuery.Class60.callSite1 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, global::GClass0.smethod_0("WťɭͥѤ"), typeof(SurveyQuery), new CSharpArgumentInfo[]
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
				SurveyQuery.Class60.callSite4 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, global::GClass0.smethod_0("WťɭͥѤ"), typeof(SurveyQuery), new CSharpArgumentInfo[]
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

		// Token: 0x0600055A RID: 1370 RVA: 0x00091D14 File Offset: 0x0008FF14
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

		// Token: 0x0600055B RID: 1371 RVA: 0x0000228F File Offset: 0x0000048F
		private void openBtn_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00091D78 File Offset: 0x0008FF78
		private void method_8()
		{
			if (this.playBtn.Content.ToString() == SurveyMsg.MsgPlay)
			{
				this.mediaElement.Play();
				this.playBtn.Content = SurveyMsg.MsgPause;
				this.mediaElement.ToolTip = global::GClass0.smethod_0("Mšɥͨѡԩټݨࠦॕ੥୶౱൤");
				return;
			}
			this.mediaElement.Pause();
			this.playBtn.Content = SurveyMsg.MsgPlay;
			this.mediaElement.ToolTip = global::GClass0.smethod_0("NŠɢͩѢԨٳݩࠥ॔੯ୣ౸");
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000038CA File Offset: 0x00001ACA
		private void playBtn_Click(object sender, RoutedEventArgs e)
		{
			this.method_8();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000038CA File Offset: 0x00001ACA
		private void method_9(object sender, MouseButtonEventArgs e)
		{
			this.method_8();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00091E04 File Offset: 0x00090004
		private void backBtn_Click(object sender, RoutedEventArgs e)
		{
			this.mediaElement.Position = this.mediaElement.Position - TimeSpan.FromSeconds(10.0);
			this.txtPlace.Text = SurveyMsg.MsgStartAt + this.mediaElement.Position.ToString();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00091E68 File Offset: 0x00090068
		private void forwardBtn_Click(object sender, RoutedEventArgs e)
		{
			this.mediaElement.Position = this.mediaElement.Position + TimeSpan.FromSeconds(10.0);
			this.txtPlace.Text = SurveyMsg.MsgStartAt + this.mediaElement.Position.ToString();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000038CA File Offset: 0x00001ACA
		private void stopBtn_Click(object sender, RoutedEventArgs e)
		{
			this.method_8();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000921CC File Offset: 0x000903CC
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

		// Token: 0x040009B1 RID: 2481
		private string SurveyID;

		// Token: 0x040009B2 RID: 2482
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x040009B3 RID: 2483
		public NavBase MyNav = new NavBase();

		// Token: 0x040009B4 RID: 2484
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x040009B5 RID: 2485
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040009B6 RID: 2486
		private bool IsShort;

		// Token: 0x040009B7 RID: 2487
		private bool IsEdit;

		// Token: 0x040009B8 RID: 2488
		private bool IsXml;

		// Token: 0x040009B9 RID: 2489
		public List<SurveyAnswer> ListAnswer = new List<SurveyAnswer>();

		// Token: 0x040009BA RID: 2490
		public List<SurveySequence> ListSequence = new List<SurveySequence>();

		// Token: 0x040009BB RID: 2491
		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		// Token: 0x040009BC RID: 2492
		private List<SurveyAttach> oListAttach = new List<SurveyAttach>();

		// Token: 0x040009C1 RID: 2497
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		private static class Class60
		{
			// Token: 0x04000D3B RID: 3387
			public static CallSite<Func<CallSite, object, Worksheet>> callSite;

			// Token: 0x04000D3C RID: 3388
			public static CallSite<Func<CallSite, Worksheet, object>> callSite1;

			// Token: 0x04000D3D RID: 3389
			public static CallSite<Func<CallSite, object, object, object, object>> callSite2;

			// Token: 0x04000D3E RID: 3390
			public static CallSite<Func<CallSite, object, Range>> callSite3;

			// Token: 0x04000D3F RID: 3391
			public static CallSite<Func<CallSite, Worksheet, object>> callSite4;

			// Token: 0x04000D40 RID: 3392
			public static CallSite<Func<CallSite, object, object, object, object>> callSite5;

			// Token: 0x04000D41 RID: 3393
			public static CallSite<Func<CallSite, object, Range>> callSite6;
		}
	}
}
