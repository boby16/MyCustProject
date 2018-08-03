using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
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
using Xceed.Wpf.DataGrid;

namespace Gssy.Capi.View
{
	public class SurveyQuery : Page, IComponentConnector, IStyleConnector
	{
		[CompilerGenerated]
		private static class _003F14_003F
		{
			public static CallSite<Func<CallSite, object, Worksheet>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, Worksheet, object>> _003C_003Ep__1;

			public static CallSite<Func<CallSite, object, object, object, object>> _003C_003Ep__2;

			public static CallSite<Func<CallSite, object, Range>> _003C_003Ep__3;

			public static CallSite<Func<CallSite, Worksheet, object>> _003C_003Ep__4;

			public static CallSite<Func<CallSite, object, object, object, object>> _003C_003Ep__5;

			public static CallSite<Func<CallSite, object, Range>> _003C_003Ep__6;
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

		internal TextBlock txtQuestionTitle;

		internal ComboBox cmbList;

		internal Button btnQuery;

		internal CheckBox checkBox1;

		internal GroupBox GroupRecord;

		internal StackPanel StkRecord;

		internal MediaElement mediaElement;

		internal TextBlock txtPlace;

		internal Button openBtn;

		internal Button playBtn;

		internal Button stopBtn;

		internal Button backBtn;

		internal Button forwardBtn;

		internal Slider volumeSlider;

		internal TextBlock txtQName;

		internal DataGridControl DataGrid1;

		internal Button btnExcel;

		internal Button btnXml;

		internal Button btnAllDat;

		internal Button btnExit;

		private bool _contentLoaded;

		public List<V_SurveyQC> CurrentSurvey
		{
			get;
			private set;
		}

		public List<SurveyMain> ListSurveyMain
		{
			get;
			private set;
		}

		public SurveyMain CurrentSurveyMain
		{
			get;
			private set;
		}

		public List<SurveyAnswer> ListRecord
		{
			get;
			private set;
		}

		public SurveyQuery()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0082: Incompatible stack heights: 0 vs 2
			StkRecord.Visibility = Visibility.Collapsed;
			SurveyHelper.TestVersion = false;
			string surveyID = SurveyHelper.SurveyID;
			if (!_003F199_003F(surveyID))
			{
				IsShort = true;
				checkBox1.IsChecked = true;
				_003F199_003F(surveyID);
			}
			if (surveyID != _003F487_003F._003F488_003F(""))
			{
				TextBlock txtQuestionTitle2 = txtQuestionTitle;
				string msgFrmCode = SurveyMsg.MsgFrmCode;
				string text = string.Concat(str1: _003F487_003F._003F488_003F("#ĸȡ"), str0: (string)/*Error near IL_008d: Stack underflow*/, str2: surveyID);
				((TextBlock)/*Error near IL_0092: Stack underflow*/).Text = text;
				_003F200_003F(surveyID);
			}
		}

		private void _003F197_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00ab: Incompatible stack heights: 0 vs 2
			//IL_00c0: Incompatible stack heights: 0 vs 1
			//IL_00d5: Incompatible stack heights: 0 vs 1
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				GroupBox groupRecord = GroupRecord;
				((UIElement)/*Error near IL_001e: Stack underflow*/).Visibility = (Visibility)/*Error near IL_001e: Stack underflow*/;
				DataGrid1.get_Columns().get_Item(_003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u065bݧ\u0870\u0945੪୨\u0c76൯\u0e6f")).set_Visible(true);
			}
			if (SurveyMsg.FunctionQueryEdit == _003F487_003F._003F488_003F("PŠɺͰѦոٿݡ\u085fॸ੩\u0b79\u0c73\u0d4c\u0e6c\u0f6e\u1072ᅚተ፱ᑷᕤ"))
			{
				DataGrid1.get_Columns();
				string text = _003F487_003F._003F488_003F("HŨɢ;ћէ\u0670\u0745\u086a२\u0a76୯౯");
				/*Error near IL_0066: Stack underflow*/.get_Item(text).set_Visible(true);
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				DataGrid1.get_Columns();
				string text2 = _003F487_003F._003F488_003F("Nźɹ\u036dѨբ\u065bݧ\u0870\u0945੪୨\u0c76൯\u0e6f");
				/*Error near IL_0094: Stack underflow*/.get_Item(text2).set_Visible(true);
			}
		}

		private void _003F68_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00af: Incompatible stack heights: 0 vs 1
			//IL_00e4: Incompatible stack heights: 0 vs 1
			if (StkRecord.Visibility == Visibility.Visible)
			{
				object content = playBtn.Content;
				if ((string)/*Error near IL_0011: Stack underflow*/ == SurveyMsg.MsgPause)
				{
					_003F172_003F();
				}
			}
			if (StkRecord.Visibility == Visibility.Visible)
			{
				mediaElement.Close();
			}
			Cell val = Cell.FindFromChild(_003F347_003F as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl((DependencyObject)val).GetItemFromContainer((DependencyObject)val.get_ParentRow());
			SurveySequence audioByPageId = oSurvey.GetAudioByPageId(v_SurveyQC.SURVEY_ID, v_SurveyQC.SEQUENCE_ID, v_SurveyQC.PAGE_ID);
			if (!(audioByPageId.RECORD_FILE == _003F487_003F._003F488_003F("")))
			{
				string rECORD_FILE = audioByPageId.RECORD_FILE;
				if ((int)/*Error near IL_00e9: Stack underflow*/ != 0)
				{
					txtQName.Text = v_SurveyQC.QUESTION_TITLE;
					_003F198_003F(audioByPageId);
					return;
				}
			}
			StkRecord.Visibility = Visibility.Collapsed;
			MessageBox.Show(SurveyMsg.MsgQuestionNoRecord, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F198_003F(SurveySequence _003F398_003F)
		{
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected I4, but got Unknown
			//IL_00e3: Incompatible stack heights: 0 vs 2
			//IL_00f4: Incompatible stack heights: 0 vs 3
			//IL_0106: Incompatible stack heights: 0 vs 2
			string rECORD_FILE = _003F398_003F.RECORD_FILE;
			string str = rECORD_FILE.Replace(_003F487_003F._003F488_003F("*Ŵɣͷ"), _003F487_003F._003F488_003F("*Ůɲ\u0332"));
			string text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[Ŕɠ\u0367Ѭհ٥");
			string text2 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("XŎɲ\u0332") + _003F487_003F._003F488_003F("]") + str;
			if (!File.Exists(text2))
			{
				_003F487_003F._003F488_003F("]");
				text2 = (string)/*Error near IL_006d: Stack underflow*/ + (string)/*Error near IL_006d: Stack underflow*/ + rECORD_FILE;
				if (!File.Exists(text2))
				{
					string msgNoRecordFile = SurveyMsg.MsgNoRecordFile;
					MessageBox.Show(string.Format((string)/*Error near IL_007e: Stack underflow*/, (object)/*Error near IL_007e: Stack underflow*/, (object)/*Error near IL_007e: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
			}
			StkRecord.Visibility = Visibility.Visible;
			mediaElement.Source = new Uri(text2, UriKind.Relative);
			int seconds = _003F398_003F.RECORD_BEGIN_TIME;
			int num = 1;
			if (_003F398_003F.RECORD_BEGIN_TIME > num)
			{
				int rECORD_BEGIN_TIME = _003F398_003F.RECORD_BEGIN_TIME;
				seconds = /*Error near IL_00c4: Stack underflow*/- /*Error near IL_00c4: Stack underflow*/;
			}
			TimeSpan position = new TimeSpan(0, 0, seconds);
			mediaElement.Position = position;
			txtPlace.Text = SurveyMsg.MsgStartAt + position.ToString();
			_003F172_003F();
		}

		private bool _003F199_003F(string _003F397_003F)
		{
			bool result = false;
			if (IsShort)
			{
				ListSurveyMain = oSurvey.GetSurveyMainList(2);
			}
			else
			{
				ListSurveyMain = oSurvey.GetSurveyMainList(1);
			}
			cmbList.ItemsSource = ListSurveyMain;
			cmbList.DisplayMemberPath = _003F487_003F._003F488_003F("Zŝɕ\u0350р՝\u065c\u074bࡅ");
			cmbList.SelectedValuePath = _003F487_003F._003F488_003F("Zŝɕ\u0350р՝\u065c\u074bࡅ");
			int num = 0;
			if (!(_003F397_003F == _003F487_003F._003F488_003F("")))
			{
				{
					foreach (SurveyMain item in ListSurveyMain)
					{
						if (item.SURVEY_ID == _003F397_003F)
						{
							cmbList.SelectedIndex = num;
							return true;
						}
						num++;
					}
					return result;
				}
			}
			if (ListSurveyMain.Count > 0)
			{
				cmbList.SelectedIndex = 0;
				result = true;
			}
			return result;
		}

		private void _003F200_003F(string _003F397_003F)
		{
			CurrentSurveyMain = oSurvey.GetSurveyMainListBySurveyId(_003F397_003F);
			oSurvey.GetSurveyyQC(_003F397_003F);
			CurrentSurvey = oSurvey.QVSurveyQC;
			ListAnswer = oSurveyAnswerDal.GetListBySurveyId(_003F397_003F);
			((ItemsControl)DataGrid1).ItemsSource = CurrentSurvey;
		}

		private void _003F201_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_007f: Incompatible stack heights: 0 vs 3
			//IL_0095: Incompatible stack heights: 0 vs 1
			if (IsEdit)
			{
				string msgQueryEditNoSave = SurveyMsg.MsgQueryEditNoSave;
				string msgCaption = SurveyMsg.MsgCaption;
				if (MessageBox.Show((string)/*Error near IL_0012: Stack underflow*/, (string)/*Error near IL_0012: Stack underflow*/, (MessageBoxButton)/*Error near IL_0012: Stack underflow*/, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				goto IL_002b;
			}
			goto IL_0032;
			IL_0085:
			goto IL_002b;
			IL_002b:
			IsEdit = false;
			goto IL_0032;
			IL_0032:
			if (cmbList.SelectedValue == null || (string)((SurveyQuery)/*Error near IL_0047: Stack underflow*/).cmbList.SelectedValue == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelectedSurvey, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				string text = cmbList.SelectedValue.ToString();
				txtQuestionTitle.Text = SurveyMsg.MsgFrmCode + _003F487_003F._003F488_003F("#ĸȡ") + text;
				_003F200_003F(text);
				SurveyID = text;
				MessageBox.Show(SurveyMsg.MsgReadSurveyOK, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void _003F202_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00de: Incompatible stack heights: 0 vs 3
			if (MessageBox.Show(SurveyMsg.MsgExportOverwrite, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
			{
				return;
			}
			goto IL_002b;
			IL_002b:
			string text = cmbList.SelectedValue.ToString();
			SurveytoXml surveytoXml = new SurveytoXml();
			string text2 = Environment.CurrentDirectory;
			if (SurveyMsg.FunctionQueryEdit == _003F487_003F._003F488_003F("PŠɺͰѦոٿݡ\u085fॸ੩\u0b79\u0c73\u0d4c\u0e6c\u0f6e\u1072ᅚተ፱ᑷᕤ"))
			{
				text2 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("RłɹͿѺռټݛࡋ४\u0a60୪\u0c64\u0d78");
				surveytoXml.OutputPath = _003F487_003F._003F488_003F("");
			}
			surveytoXml.SaveSurveyAnswer(text, text2, ListAnswer, true);
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				((SurveytoXml)/*Error near IL_00a1: Stack underflow*/).SaveSurveySequence((string)/*Error near IL_00a1: Stack underflow*/, (string)/*Error near IL_00a1: Stack underflow*/, (List<SurveySequence>)null, true);
			}
			Logging.Data.WriteLog(_003F487_003F._003F488_003F("柯諫姴勽тդ\u0670揎䝞\u093b"), string.Format(_003F487_003F._003F488_003F("寬僵韠偺Ьհغݴ\u0828\u0943੧ୱత梄䃴༯"), text));
			MessageBox.Show(string.Format(SurveyMsg.MsgExportDatFile, text, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			IsEdit = false;
			return;
			IL_00ad:
			goto IL_002b;
		}

		private void _003F203_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0096: Incompatible stack heights: 0 vs 1
			//IL_009d: Incompatible stack heights: 0 vs 1
			if (!MessageBox.Show(SurveyMsg.MsgExportOverwrite, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
			{
				new CheckPsw().ShowDialog();
				if (SurveyMsg.SurveyRangePswOk)
				{
					SurveyMsg.SurveyRangePswOk = false;
					bool flag = true;
					bool? isChecked = checkBox1.IsChecked;
					bool flag2 = true;
					if (isChecked.GetValueOrDefault() == flag2)
					{
						bool hasValue = isChecked.HasValue;
					}
					flag = (((int)/*Error near IL_00ac: Stack underflow*/ != 0) ? true : false);
					int _003F9_003F = 1;
					if (flag)
					{
						_003F9_003F = 2;
					}
					List<SurveyMain> surveyMain = new SurveyMainDal().GetSurveyMain(_003F9_003F);
					SurveytoXml surveytoXml = new SurveytoXml();
					string currentDirectory = Environment.CurrentDirectory;
					foreach (SurveyMain item in surveyMain)
					{
						surveytoXml.SaveSurveyAnswer(item.SURVEY_ID, currentDirectory, null, true);
						if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
						{
							surveytoXml.SaveSurveySequence(item.SURVEY_ID, currentDirectory, null, true);
						}
						if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
						{
							surveytoXml.SaveSurveyAttach(item.SURVEY_ID, currentDirectory, null, true);
						}
						Logging.Data.WriteLog(_003F487_003F._003F488_003F("闫剳趐勸л"), string.Format(_003F487_003F._003F488_003F("*Ųȸ\u037aЦ誖埾岏樒\uf600"), item.SURVEY_ID));
					}
					currentDirectory += _003F487_003F._003F488_003F("[ŉɰͰѳշٵ");
					MessageBox.Show(string.Format(SurveyMsg.MsgOutputOk, currentDirectory), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
			}
		}

		private void _003F127_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_01d4: Incompatible stack heights: 0 vs 1
			//IL_01f6: Incompatible stack heights: 0 vs 1
			//IL_0221: Incompatible stack heights: 0 vs 2
			//IL_0245: Incompatible stack heights: 0 vs 1
			Cell val = Cell.FindFromChild(_003F347_003F as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl((DependencyObject)val).GetItemFromContainer((DependencyObject)val.get_ParentRow());
			bool flag = false;
			if (v_SurveyQC.ANSWER_USE == 1)
			{
				flag = true;
				if (((V_SurveyQC)/*Error near IL_0036: Stack underflow*/).QUESTION_NAME == _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44"))
				{
					flag = false;
				}
			}
			if (flag)
			{
				SurveyHelper.QueryEditSurveyId = v_SurveyQC.SURVEY_ID;
				SurveyHelper.QueryEditQTitle = ((V_SurveyQC)/*Error near IL_0055: Stack underflow*/).QUESTION_TITLE;
				SurveyHelper.QueryEditQName = v_SurveyQC.QUESTION_NAME;
				SurveyHelper.QueryEditDetailID = v_SurveyQC.DETAIL_ID;
				SurveyHelper.QueryEditCODE = v_SurveyQC.CODE;
				SurveyHelper.QueryEditCODE_TEXT = v_SurveyQC.CODE_TEXT;
				SurveyHelper.QueryEditSequence = v_SurveyQC.SEQUENCE_ID;
				SurveyHelper.QueryEditMemModel = true;
				new EditAnswer().ShowDialog();
				if (SurveyHelper.QueryEditConfirm)
				{
					IsEdit = true;
					for (int i = 0; i < CurrentSurvey.Count; i++)
					{
						if (CurrentSurvey[i].QUESTION_NAME == SurveyHelper.QueryEditQName)
						{
							List<V_SurveyQC> currentSurvey = CurrentSurvey;
							((List<V_SurveyQC>)/*Error near IL_00d7: Stack underflow*/)[(int)/*Error near IL_00d7: Stack underflow*/].CODE = SurveyHelper.QueryEditCODE;
							CurrentSurvey[i].CODE_TEXT = SurveyHelper.QueryEditCODE_TEXT + SurveyMsg.MsgEditNote;
							break;
						}
					}
					int selectedIndex = DataGrid1.get_SelectedIndex();
					((ItemsControl)DataGrid1).ItemsSource = null;
					((ItemsControl)DataGrid1).ItemsSource = CurrentSurvey;
					DataGrid1.set_SelectedIndex(selectedIndex);
					for (int j = 0; j < ListAnswer.Count; j++)
					{
						if (ListAnswer[j].QUESTION_NAME == SurveyHelper.QueryEditQName)
						{
							List<SurveyAnswer> listAnswer = ListAnswer;
							((List<SurveyAnswer>)/*Error near IL_0184: Stack underflow*/)[j].CODE = SurveyHelper.QueryEditCODE;
							break;
						}
					}
					SurveyHelper.QueryEditConfirm = false;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgNotEdit, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00ab: Incompatible stack heights: 0 vs 2
			Cell val = Cell.FindFromChild(_003F347_003F as DependencyObject);
			V_SurveyQC v_SurveyQC = (V_SurveyQC)DataGridControl.GetParentDataGridControl((DependencyObject)val).GetItemFromContainer((DependencyObject)val.get_ParentRow());
			SurveyHelper.AttachReadOnlyModel = true;
			SurveyHelper.AttachSurveyId = v_SurveyQC.SURVEY_ID;
			SurveyHelper.AttachQName = v_SurveyQC.QUESTION_NAME;
			SurveyHelper.AttachPageId = v_SurveyQC.PAGE_ID;
			oListAttach = oSurveyAttachDal.GetListByQName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName);
			SurveyHelper.AttachCount = oListAttach.Count();
			if (SurveyHelper.AttachCount == 0)
			{
				_003F487_003F._003F488_003F("朠馓ȪͲиպئ殤漍齇䓴\uf400");
				MessageBox.Show(string.Format(arg0: ((V_SurveyQC)/*Error near IL_0084: Stack underflow*/).QUESTION_NAME, format: (string)/*Error near IL_0089: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				new EditAttachments().ShowDialog();
			}
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_004c: Incompatible stack heights: 0 vs 3
			if (IsEdit)
			{
				string msgQueryEditNoSave = SurveyMsg.MsgQueryEditNoSave;
				string msgCaption = SurveyMsg.MsgCaption;
				if (MessageBox.Show((string)/*Error near IL_0012: Stack underflow*/, (string)/*Error near IL_0012: Stack underflow*/, (MessageBoxButton)/*Error near IL_0012: Stack underflow*/, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				goto IL_002b;
			}
			goto IL_0057;
			IL_0052:
			goto IL_002b;
			IL_002b:
			IsEdit = false;
			goto IL_0057;
			IL_0057:
			System.Windows.Application.Current.Shutdown();
		}

		private void _003F204_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_004c: Incompatible stack heights: 0 vs 3
			if (IsEdit)
			{
				string msgQueryEditNoSave = SurveyMsg.MsgQueryEditNoSave;
				string msgCaption = SurveyMsg.MsgCaption;
				if (MessageBox.Show((string)/*Error near IL_0012: Stack underflow*/, (string)/*Error near IL_0012: Stack underflow*/, (MessageBoxButton)/*Error near IL_0012: Stack underflow*/, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				goto IL_002b;
			}
			goto IL_0057;
			IL_0052:
			goto IL_002b;
			IL_002b:
			IsEdit = false;
			goto IL_0057;
			IL_0057:
			IsShort = true;
			_003F199_003F(_003F487_003F._003F488_003F(""));
		}

		private void _003F205_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_004c: Incompatible stack heights: 0 vs 3
			if (IsEdit)
			{
				string msgQueryEditNoSave = SurveyMsg.MsgQueryEditNoSave;
				string msgCaption = SurveyMsg.MsgCaption;
				if (MessageBox.Show((string)/*Error near IL_0012: Stack underflow*/, (string)/*Error near IL_0012: Stack underflow*/, (MessageBoxButton)/*Error near IL_0012: Stack underflow*/, MessageBoxImage.Asterisk).Equals(MessageBoxResult.No))
				{
					return;
				}
				goto IL_002b;
			}
			goto IL_0057;
			IL_0052:
			goto IL_002b;
			IL_002b:
			IsEdit = false;
			goto IL_0057;
			IL_0057:
			IsShort = false;
			_003F199_003F(_003F487_003F._003F488_003F(""));
		}

		private void _003F206_003F(string _003F397_003F)
		{
			//IL_018d: Incompatible stack heights: 0 vs 2
			//IL_01ee: Incompatible stack heights: 0 vs 2
			//IL_037b: Incompatible stack heights: 0 vs 2
			//IL_03dc: Incompatible stack heights: 0 vs 2
			string text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Zŀɼ\u0360ѧխ");
			string str = string.Format(_003F487_003F._003F488_003F("AŌɝ\u0378Ѿսٯݰ\u0873ष\u0a7bଫ౼൯\u0e71\u0f79"), _003F397_003F);
			string text2 = text + _003F487_003F._003F488_003F("]") + str;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(_003F487_003F._003F488_003F("\u0014ēȒ\u0313ДԪخܭ࠱फਪ\u0b29నഺฦ༥ဤᄣሿፒᐠᔿᘾᜠᠼ\u193bᨺ\u1b39\u1c38\u1d37Ḷἵ‴ℳ∶⌷"))));
			if (application != null)
			{
				Workbooks workbooks = application.Workbooks;
				Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
				Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
				string[] array = new string[3]
				{
					_003F487_003F._003F488_003F("颚矯"),
					_003F487_003F._003F488_003F("缔礀"),
					_003F487_003F._003F488_003F("丯撆")
				};
				if (_003F14_003F._003C_003Ep__3 == null)
				{
					_003F14_003F._003C_003Ep__3 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Range), typeof(SurveyQuery)));
				}
				Func<CallSite, object, Range> target = _003F14_003F._003C_003Ep__3.Target;
				CallSite<Func<CallSite, object, Range>> _003C_003Ep__ = _003F14_003F._003C_003Ep__3;
				if (_003F14_003F._003C_003Ep__2 == null)
				{
					_003F14_003F._003C_003Ep__2 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof(SurveyQuery), new CSharpArgumentInfo[3]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object, object> target2 = _003F14_003F._003C_003Ep__2.Target;
				CallSite<Func<CallSite, object, object, object, object>> _003C_003Ep__2 = _003F14_003F._003C_003Ep__2;
				if (_003F14_003F._003C_003Ep__1 == null)
				{
					_003F14_003F._003C_003Ep__1 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, _003F487_003F._003F488_003F("Wťɭ\u0365Ѥ"), typeof(SurveyQuery), new CSharpArgumentInfo[1]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				object arg = _003F14_003F._003C_003Ep__1.Target(_003F14_003F._003C_003Ep__1, worksheet);
				object arg2 = worksheet.Cells[1, 1];
				object arg3 = worksheet.Cells[1, 3];
				object arg4 = /*Error near IL_026e: Stack underflow*/((CallSite)/*Error near IL_026e: Stack underflow*/, arg, arg2, arg3);
				/*Error near IL_0273: Stack underflow*/((CallSite)/*Error near IL_0273: Stack underflow*/, arg4).set_Value(Type.Missing, (object)array);
				int count = CurrentSurvey.Count;
				string[,] array2 = new string[count, 3];
				int num = 0;
				foreach (V_SurveyQC item in CurrentSurvey)
				{
					string[,] array3 = array2;
					int num2 = num;
					string qUESTION_TITLE = item.QUESTION_TITLE;
					array3[num2, 0] = qUESTION_TITLE;
					string[,] array4 = array2;
					int num3 = num;
					string cODE = item.CODE;
					array4[num3, 1] = cODE;
					string[,] array5 = array2;
					int num4 = num;
					string cODE_TEXT = item.CODE_TEXT;
					array5[num4, 2] = cODE_TEXT;
					num++;
				}
				if (_003F14_003F._003C_003Ep__6 == null)
				{
					_003F14_003F._003C_003Ep__6 = CallSite<Func<CallSite, object, Range>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Range), typeof(SurveyQuery)));
				}
				Func<CallSite, object, Range> target3 = _003F14_003F._003C_003Ep__6.Target;
				CallSite<Func<CallSite, object, Range>> _003C_003Ep__3 = _003F14_003F._003C_003Ep__6;
				if (_003F14_003F._003C_003Ep__5 == null)
				{
					_003F14_003F._003C_003Ep__5 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof(SurveyQuery), new CSharpArgumentInfo[3]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object, object> target4 = _003F14_003F._003C_003Ep__5.Target;
				CallSite<Func<CallSite, object, object, object, object>> _003C_003Ep__4 = _003F14_003F._003C_003Ep__5;
				if (_003F14_003F._003C_003Ep__4 == null)
				{
					_003F14_003F._003C_003Ep__4 = CallSite<Func<CallSite, Worksheet, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, _003F487_003F._003F488_003F("Wťɭ\u0365Ѥ"), typeof(SurveyQuery), new CSharpArgumentInfo[1]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				object arg5 = _003F14_003F._003C_003Ep__4.Target(_003F14_003F._003C_003Ep__4, worksheet);
				object arg6 = worksheet.Cells[2, 1];
				object arg7 = worksheet.Cells[count + 1, 3];
				object arg8 = /*Error near IL_045f: Stack underflow*/((CallSite)/*Error near IL_045f: Stack underflow*/, arg5, arg6, arg7);
				/*Error near IL_0464: Stack underflow*/((CallSite)/*Error near IL_0464: Stack underflow*/, arg8).set_Value(Type.Missing, (object)array2);
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
				MessageBox.Show(string.Format(SurveyMsg.MsgExcelOutputDone, _003F397_003F, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void _003F207_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_005e: Incompatible stack heights: 0 vs 3
			if (!new ExcelHelper().CheckExcelInstall())
			{
				string msgNoExcel = SurveyMsg.MsgNoExcel;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0016: Stack underflow*/, (string)/*Error near IL_0016: Stack underflow*/, (MessageBoxButton)/*Error near IL_0016: Stack underflow*/, MessageBoxImage.Hand);
			}
			else if (MessageBox.Show(SurveyMsg.MsgExcelOutput, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
			{
				string surveyID = SurveyID;
				_003F206_003F(surveyID);
			}
		}

		private void _003F171_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
		}

		private void _003F172_003F()
		{
			if (playBtn.Content.ToString() == SurveyMsg.MsgPlay)
			{
				mediaElement.Play();
				playBtn.Content = SurveyMsg.MsgPause;
				mediaElement.ToolTip = _003F487_003F._003F488_003F("Mšɥ\u0368ѡԩټݨ\u0826\u0955\u0a65୶\u0c71\u0d64");
			}
			else
			{
				mediaElement.Pause();
				playBtn.Content = SurveyMsg.MsgPlay;
				mediaElement.ToolTip = _003F487_003F._003F488_003F("NŠɢ\u0369ѢԨٳݩ\u0825\u0954੯\u0b63౸");
			}
		}

		private void _003F173_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F172_003F();
		}

		private void _003F175_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			_003F172_003F();
		}

		private void _003F208_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			mediaElement.Position -= TimeSpan.FromSeconds(10.0);
			txtPlace.Text = SurveyMsg.MsgStartAt + mediaElement.Position.ToString();
		}

		private void _003F209_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			mediaElement.Position += TimeSpan.FromSeconds(10.0);
			txtPlace.Text = SurveyMsg.MsgStartAt + mediaElement.Position.ToString();
		}

		private void _003F210_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F172_003F();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭺\u1c7cᵻṩὲ⁻ⅼ≭⍵⑿┫♼❢⡯⥭"), UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
			return;
			IL_0016:
			goto IL_001b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			//IL_0211: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Expected O, but got Unknown
			switch (_003F349_003F)
			{
			case 1:
				((SurveyQuery)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				cmbList = (ComboBox)_003F350_003F;
				break;
			case 4:
				btnQuery = (Button)_003F350_003F;
				btnQuery.Click += _003F201_003F;
				break;
			case 5:
				checkBox1 = (CheckBox)_003F350_003F;
				checkBox1.Checked += _003F204_003F;
				checkBox1.Unchecked += _003F205_003F;
				break;
			case 6:
				GroupRecord = (GroupBox)_003F350_003F;
				break;
			case 7:
				StkRecord = (StackPanel)_003F350_003F;
				break;
			case 8:
				mediaElement = (MediaElement)_003F350_003F;
				break;
			case 9:
				txtPlace = (TextBlock)_003F350_003F;
				break;
			case 10:
				openBtn = (Button)_003F350_003F;
				openBtn.Click += _003F171_003F;
				break;
			case 11:
				playBtn = (Button)_003F350_003F;
				playBtn.Click += _003F173_003F;
				break;
			case 12:
				stopBtn = (Button)_003F350_003F;
				stopBtn.Click += _003F210_003F;
				break;
			case 13:
				backBtn = (Button)_003F350_003F;
				backBtn.Click += _003F208_003F;
				break;
			case 14:
				forwardBtn = (Button)_003F350_003F;
				forwardBtn.Click += _003F209_003F;
				break;
			case 15:
				volumeSlider = (Slider)_003F350_003F;
				break;
			case 16:
				txtQName = (TextBlock)_003F350_003F;
				break;
			case 17:
				DataGrid1 = _003F350_003F;
				((FrameworkElement)DataGrid1).Loaded += _003F197_003F;
				break;
			case 21:
				btnExcel = (Button)_003F350_003F;
				btnExcel.Click += _003F207_003F;
				break;
			case 22:
				btnXml = (Button)_003F350_003F;
				btnXml.Click += _003F202_003F;
				break;
			case 23:
				btnAllDat = (Button)_003F350_003F;
				btnAllDat.Click += _003F203_003F;
				break;
			case 24:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 18:
				((Button)_003F350_003F).Click += _003F68_003F;
				break;
			case 19:
				((Button)_003F350_003F).Click += _003F127_003F;
				break;
			case 20:
				((Button)_003F350_003F).Click += _003F85_003F;
				break;
			}
		}
	}
}
