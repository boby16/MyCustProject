using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.QEdit
{
	public partial class DebugCheck : Window
	{
		public DebugCheck()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.txtHelper.Text = this.method_1();
			this.PageId = SurveyHelper.NavCurPage;
			this.SurveyId = SurveyHelper.SurveyID;
			this.oLogicEngine.SurveyID = SurveyHelper.SurveyID;
			this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
			this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
			this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
			this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
			this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
			this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
			this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			if (SurveyHelper.SurveyID == "")
			{
				this.TabItem3.IsEnabled = false;
				this.TabItem4.IsEnabled = false;
			}
		}

		private string method_1()
		{
			string str = "" + Environment.NewLine + "=========上一页答案==========";
			SurveyBiz surveyBiz = new SurveyBiz();
			return str + surveyBiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo();
		}

		private void method_2(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(1);
		}

		private void btnQuery_Click(object sender, RoutedEventArgs e)
		{
			bool flag = true;
			if (this.Chk_Filter.IsChecked == true)
			{
				this.IsFilter = false;
			}
			else
			{
				this.IsFilter = true;
			}
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			if (this.Chk_SurveyDefine.IsChecked == true)
			{
				this.method_3();
			}
			if (this.Chk_SurveyDetail.IsChecked == true)
			{
				this.method_4();
			}
			if (this.Chk_SurveyRoadmap.IsChecked == true)
			{
				this.method_5();
			}
			if (this.Chk_SurveyLogic.IsChecked == true)
			{
				this.method_9();
			}
			if (this.Chk_S_Define.IsChecked == true)
			{
				this.method_6();
			}
			if (this.Chk_SurveyRandomBase.IsChecked == true)
			{
				this.method_7();
			}
			if (this.Chk_SurveyConfig.IsChecked == true)
			{
				this.method_10();
			}
			if (this.ChkW_SurveyMain.IsChecked == true)
			{
				this.method_13();
			}
			if (this.ChkW_SurveyAnswer.IsChecked == true)
			{
				this.method_14(1);
			}
			if (this.ChkW_SurveySequence.IsChecked == true)
			{
				this.method_15();
			}
			if (this.ChkW_SurveyRandom.IsChecked == true)
			{
				this.method_16();
			}
			if (this.ChkW_SurveyAnswerHis.IsChecked == true)
			{
				this.method_17();
			}
			if (flag)
			{
				this.txtTable.Text = SurveyMsg.MsgCurrentContent + this.TableName;
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNoFunction, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_3()
		{
			this.TableName = "SurveyDefine";
			SurveyDefineDal surveyDefineDal = new SurveyDefineDal();
			if (this.IsFilter)
			{
				this.lSurveyDefine = surveyDefineDal.GetListByPageId(this.FilterPageId);
			}
			else
			{
				this.lSurveyDefine = surveyDefineDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyDefine;
		}

		private void method_4()
		{
			this.TableName = "SurveyDetail";
			SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
			if (this.IsFilter)
			{
				using (List<SurveyDefine>.Enumerator enumerator = this.lSurveyDefine.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SurveyDefine surveyDefine = enumerator.Current;
						if (surveyDefine.DETAIL_ID != "")
						{
							this.lSurveyDetail = surveyDetailDal.GetDetails(surveyDefine.DETAIL_ID);
							break;
						}
					}
					goto IL_85;
				}
			}
			this.lSurveyDetail = surveyDetailDal.GetList();
			IL_85:
			this.DataGrid1.ItemsSource = this.lSurveyDetail;
		}

		private void method_5()
		{
			this.TableName = "SurveyRoadMap";
			SurveyRoadMapDal surveyRoadMapDal = new SurveyRoadMapDal();
			if (this.IsFilter)
			{
				string string_ = string.Format("select * from SurveyRoadMap where page_id ='{0}'", this.FilterPageId);
				this.lSurveyRoadMap = surveyRoadMapDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyRoadMap = surveyRoadMapDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyRoadMap;
		}

		private void method_6()
		{
			this.TableName = "S_Define";
			S_DefineDal s_DefineDal = new S_DefineDal();
			if (this.IsFilter)
			{
				string string_ = string.Format("select * from S_Define where page_id ='{0}'", this.FilterPageId);
				this.lS_Define = s_DefineDal.GetListBySql(string_);
			}
			else
			{
				this.lS_Define = s_DefineDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lS_Define;
		}

		private void method_7()
		{
			this.TableName = "SurveyRandomBase";
			SurveyRandomBaseDal surveyRandomBaseDal = new SurveyRandomBaseDal();
			this.lSurveyRandomBase = surveyRandomBaseDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyRandomBase;
		}

		private void method_8()
		{
			this.TableName = "S_Jump";
			S_JUMPDal s_JUMPDal = new S_JUMPDal();
			this.lS_Jump = s_JUMPDal.GetList();
			this.DataGrid1.ItemsSource = this.lS_Jump;
		}

		private void method_9()
		{
			this.TableName = "SurveyLogic";
			SurveyLogicDal surveyLogicDal = new SurveyLogicDal();
			if (this.IsFilter)
			{
				string string_ = string.Format("select * from SurveyLogic where page_id ='{0}'", this.FilterPageId);
				this.lSurveyLogic = surveyLogicDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyLogic = surveyLogicDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyLogic;
		}

		private void method_10()
		{
			this.TableName = "SurveyConfig";
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			this.lSurveyConfig = surveyConfigDal.GetListRead();
			this.DataGrid1.ItemsSource = this.lSurveyConfig;
		}

		private void method_11()
		{
			this.TableName = "SurveyDict";
			SurveyDictDal surveyDictDal = new SurveyDictDal();
			this.lSurveyDict = surveyDictDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyDict;
		}

		private void method_12()
		{
			this.TableName = "SurveyUsers";
			SurveyUsersDal surveyUsersDal = new SurveyUsersDal();
			this.lSurveyUsers = surveyUsersDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyUsers;
		}

		private void method_13()
		{
			this.TableName = "SurveyMain";
			SurveyMainDal surveyMainDal = new SurveyMainDal();
			if (this.IsFilter)
			{
				string string_ = string.Format("select * from SurveyMain where SURVEY_ID ='{0}'", this.FilterSurveyId);
				this.lSurveyMain = surveyMainDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyMain = surveyMainDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyMain;
		}

		private void method_14(int int_0 = 1)
		{
			this.TableName = "SurveyAnswer";
			SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
			if (this.IsFilter)
			{
				this.lSurveyAnswer = surveyAnswerDal.GetListBySequenceId(this.FilterSurveyId, SurveyHelper.SurveySequence - 1);
			}
			else
			{
				string string_ = string.Format("select * from SurveyAnswer where SURVEY_ID ='{0}' and Sequence_id < 80000 order by id", this.FilterSurveyId);
				this.lSurveyAnswer = surveyAnswerDal.GetListBySql(string_);
			}
			if (int_0 == 1)
			{
				this.DataGrid1.ItemsSource = this.lSurveyAnswer;
				return;
			}
			if (int_0 == 2)
			{
				this.DataGrid2.ItemsSource = this.lSurveyAnswer;
				return;
			}
			if (int_0 == 3)
			{
				this.DataGrid3.ItemsSource = this.lSurveyAnswer;
			}
		}

		private void method_15()
		{
			this.TableName = "SurveySequence";
			SurveySequenceDal surveySequenceDal = new SurveySequenceDal();
			this.lSurveySequence = surveySequenceDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveySequence;
		}

		private void method_16()
		{
			this.TableName = "SurveyRandom";
			SurveyRandomDal surveyRandomDal = new SurveyRandomDal();
			this.lSurveyRandom = surveyRandomDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyRandom;
		}

		private void method_17()
		{
			this.TableName = "SurveyAnswerHis";
			SurveyAnswerHisDal surveyAnswerHisDal = new SurveyAnswerHisDal();
			this.lSurveyAnswerHis = surveyAnswerHisDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyAnswerHis;
		}

		private void method_18()
		{
			this.TableName = "SurveyConfig";
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			this.lSurveyConfig = surveyConfigDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyConfig;
		}

		private void method_19()
		{
			this.TableName = "SurveyLog";
			SurveyLogDal surveyLogDal = new SurveyLogDal();
			this.lSurveyLog = surveyLogDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyLog;
		}

		private void method_20()
		{
			this.TableName = "SurveyOption";
			SurveyOptionDal surveyOptionDal = new SurveyOptionDal();
			this.lSurveyOption = surveyOptionDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyOption;
		}

		private void method_21()
		{
			this.TableName = "S_MT";
			S_MTDal s_MTDal = new S_MTDal();
			this.lS_MT = s_MTDal.GetList();
			this.DataGrid1.ItemsSource = this.lS_MT;
		}

		private void method_22()
		{
			this.TableName = "SurveySync";
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			this.lSurveySync = surveySyncDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveySync;
		}

		private void TabItem3_Initialized(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(3);
			this.txtQName.Focus();
		}

		private void DataGrid3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				return;
			}
			this.txtQName.Text = ((SurveyAnswer)this.DataGrid3.CurrentItem).QUESTION_NAME;
			if (this.txtLogic.Text == "")
			{
				this.AutoAdd.IsChecked = new bool?(true);
			}
			if (this.AutoAdd.IsChecked == true)
			{
				TextBox textBox = this.txtLogic;
				textBox.Text = textBox.Text + ((SurveyAnswer)this.DataGrid3.CurrentItem).QUESTION_NAME + " ";
			}
		}

		private void btnRunAnswer_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtQName.Text.Trim();
			if (text == "")
			{
				this.txtQAnswer.Text = "(请先输入题号)";
				this.txtQName.Focus();
				return;
			}
			SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
			string text2 = "[" + surveyAnswerDal.GetOneCode(SurveyHelper.SurveyID, text) + "]";
			if (text2 == "[]")
			{
				text2 = "<空> (提醒：题号对大小写敏感)";
			}
			this.txtQAnswer.Text = this.txtQName.Text + " = " + text2;
			this.txtQName.Focus();
			this.txtQName.SelectAll();
		}

		private void btnRunLogic_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.oLogicEngine.boolResult(this.txtLogic.Text);
			this.txtLogicResult.Text = flag.ToString();
		}

		private void btnRunText_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.strShowText(this.txtLogic.Text, true);
			if (text == "[]")
			{
				text = "<空> (提醒：题号对大小写敏感)";
			}
			this.txtLogicResult.Text = text;
		}

		private void btnRunRoute_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.Route(this.txtLogic.Text);
			if (text == "[]")
			{
				text = "<空> (提醒：题号对大小写敏感)";
			}
			this.txtLogicResult.Text = text;
		}

		private void btnRunOption_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.stringResult(this.txtLogic.Text);
			if (text == "[]")
			{
				text = "<空> (提醒：题号对大小写敏感)";
			}
			this.txtLogicResult.Text = text;
		}

		private void txtQName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnRunAnswer_Click(sender, e);
			}
		}

		private void txtQName_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtQName.SelectAll();
		}

		private void TabItem4_Initialized(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(2);
			this.txtQuestionName.Focus();
		}

		private void DataGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				this.txtNewAnswer.Focus();
				this.txtNewAnswer.SelectAll();
				return;
			}
			this.txtQuestionName.Text = ((SurveyAnswer)this.DataGrid2.CurrentItem).QUESTION_NAME;
			this.btnGetAnswer_Click(null, null);
		}

		private void btnGetAnswer_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				return;
			}
			string text = this.txtQuestionName.Text;
			if (text == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtQuestionName.Focus();
				return;
			}
			string oneCode = new SurveyAnswerDal().GetOneCode(SurveyHelper.SurveyID, text);
			this.txtAnswer.Text = oneCode;
			if (this.txtAnswer.Text == "")
			{
				this.txtAnswer.Text = "不存在这题答案，将用新增方式保存！";
			}
			this.txtNewAnswer.Text = oneCode;
			this.txtQuestionName.IsEnabled = false;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.txtNewAnswer.IsEnabled = true;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.btnGetAnswer.Opacity = this.ButtonDisable;
			this.btnSave.Opacity = this.ButtonEnable;
			this.btnCancel.Opacity = this.ButtonEnable;
			this.txtNewAnswer.Focus();
			this.txtNewAnswer.SelectAll();
		}

		private void txtQuestionName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnGetAnswer_Click(sender, e);
			}
		}

		private void btnKeyboard1_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		private void txtNewAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		private void btnSave_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.btnSave.Opacity == this.ButtonDisable)
			{
				return;
			}
			string text = this.txtAnswer.Text;
			string text2 = this.txtNewAnswer.Text;
			if (text != text2)
			{
				string text3 = this.txtQuestionName.Text;
				if (text2 == "" && MessageBox.Show("是否把空白答案存放到这个变量中？", SurveyMsg.MsgCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
				{
					return;
				}
				new SurveyAnswerDal().AddOne(SurveyHelper.SurveyID, text3, text2, 8888);
				this.method_14(2);
				this.method_14(3);
			}
			this.txtQuestionName.IsEnabled = true;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.txtNewAnswer.IsEnabled = false;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.btnGetAnswer.Opacity = this.ButtonEnable;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			this.txtQuestionName.Focus();
			this.txtQuestionName.SelectAll();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.txtQuestionName.IsEnabled = true;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.txtNewAnswer.IsEnabled = false;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.btnGetAnswer.Opacity = this.ButtonEnable;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			this.method_14(2);
			this.txtQuestionName.Focus();
			this.txtQuestionName.SelectAll();
		}

		public void Refresh()
		{
			DispatcherFrame dispatcherFrame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(DebugCheck.Class62.instance.method_0), dispatcherFrame);
			Dispatcher.PushFrame(dispatcherFrame);
		}

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private string TableName;

		private bool IsFilter = true;

		private string FilterSurveyId = "";

		private string FilterPageId = "";

		private List<S_Define> lS_Define = new List<S_Define>();

		private List<SurveyRandom> lSurveyRandomBase = new List<SurveyRandom>();

		private List<S_JUMP> lS_Jump = new List<S_JUMP>();

		private List<SurveyDefine> lSurveyDefine = new List<SurveyDefine>();

		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		private List<SurveyRoadMap> lSurveyRoadMap = new List<SurveyRoadMap>();

		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		private List<SurveyConfig> lSurveyConfig = new List<SurveyConfig>();

		private List<SurveyDict> lSurveyDict = new List<SurveyDict>();

		private List<SurveyUsers> lSurveyUsers = new List<SurveyUsers>();

		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		private List<SurveySequence> lSurveySequence = new List<SurveySequence>();

		private List<SurveyRandom> lSurveyRandom = new List<SurveyRandom>();

		private List<SurveyAnswerHis> lSurveyAnswerHis = new List<SurveyAnswerHis>();

		private List<SurveyLog> lSurveyLog = new List<SurveyLog>();

		private List<SurveyOption> lSurveyOption = new List<SurveyOption>();

		private List<S_MT> lS_MT = new List<S_MT>();

		private List<SurveySync> lSurveySync = new List<SurveySync>();

		private LogicEngine oLogicEngine = new LogicEngine();

		private string PageId = "";

		private string SurveyId = "";

		private double ButtonEnable = 1.0;

		private double ButtonDisable = 0.5;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class62
		{
			internal object method_0(object object_0)
			{
				((DispatcherFrame)object_0).Continue = false;
				return null;
			}

			public static readonly DebugCheck.Class62 instance = new DebugCheck.Class62();

			public static DispatcherOperationCallback compare0;
		}
	}
}
