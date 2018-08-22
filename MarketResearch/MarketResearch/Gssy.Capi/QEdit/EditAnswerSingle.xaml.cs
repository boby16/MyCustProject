using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.QEdit
{
	public partial class EditAnswerSingle : Window
	{
		public EditAnswerSingle()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.MySurveyId = SurveyHelper.SurveyID;
			SurveyHelper.QueryEditQn = this.oLogicEngine.ReplaceSpecialFlag(SurveyHelper.QueryEditQn);
			List<string> list = this.oBoldTitle.ParaToList(SurveyHelper.QueryEditQn, "//");
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNoModifyQSet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				base.Close();
				return;
			}
			this.EditQn = list[0];
			this.EditVar = this.EditQn;
			if (list.Count > 1)
			{
				this.EditVar = this.EditVar + "_R" + list[1];
			}
			if (list.Count > 2)
			{
				this.EditVar = this.EditVar + "_R" + list[2];
			}
			this.QDefine = this.oSurveyDefineDal.GetByName(this.EditQn);
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			if (list.Count > 1 && this.QDefine.GROUP_CODEA != "")
			{
				this.QCircleDefineA = this.oSurveyDefineDal.GetByName(this.QDefine.GROUP_CODEA);
				if (this.QCircleDefineA.DETAIL_ID != "")
				{
					this.QCircleDetailsA = this.oSurveyDetailDal.GetDetails(this.QCircleDefineA.DETAIL_ID);
				}
				if (list.Count > 0)
				{
					foreach (SurveyDetail surveyDetail in this.QCircleDetailsA)
					{
						if (surveyDetail.CODE == list[1])
						{
							this.CircleTextA = surveyDetail.CODE_TEXT;
							break;
						}
					}
				}
			}
			if (list.Count > 2 && this.QDefine.GROUP_CODEB != "")
			{
				this.QCircleDefineB = this.oSurveyDefineDal.GetByName(this.QDefine.GROUP_CODEB);
				if (this.QCircleDefineB.DETAIL_ID != "")
				{
					this.QCircleDetailsB = this.oSurveyDetailDal.GetDetails(this.QCircleDefineB.DETAIL_ID);
				}
				if (list.Count > 1)
				{
					foreach (SurveyDetail surveyDetail2 in this.QCircleDetailsB)
					{
						if (surveyDetail2.CODE == list[2])
						{
							this.CircleTextB = surveyDetail2.CODE_TEXT;
							break;
						}
					}
				}
			}
			this.EditTitle = this.QDefine.SPSS_TITLE;
			this.txtQuestionTitle.Text = this.EditTitle;
			this.txtTitle.Text = this.EditVar;
			if (this.CircleTextA != "")
			{
				TextBlock textBlock = this.txtTitle;
				textBlock.Text = textBlock.Text + " - " + this.CircleTextA;
			}
			if (this.CircleTextB != "")
			{
				TextBlock textBlock2 = this.txtTitle;
				textBlock2.Text = textBlock2.Text + " - " + this.CircleTextB;
			}
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(this.MySurveyId, this.EditVar);
			this.AnswerCode = one.CODE;
			this.iSeq = one.SEQUENCE_ID;
			this.txtAnswer.Text = this.AnswerCode;
			if (this.QDefine.DETAIL_ID != "")
			{
				foreach (SurveyDetail surveyDetail3 in this.QDetails)
				{
					if (surveyDetail3.CODE == this.AnswerCode)
					{
						this.CodeText = surveyDetail3.CODE_TEXT;
						break;
					}
				}
				if (this.CodeText != "")
				{
					this.txtAnswer.Text = this.AnswerCode +"（" + this.CodeText + "）";
				}
				this.method_1();
				this.ListSelect.ItemsSource = this.QDetails;
				this.ListSelect.SelectedValuePath = "CODE";
				this.ListSelect.Visibility = Visibility.Visible;
				this.txtNewAnswer.IsEnabled = false;
				this.ListSelect.Focus();
				return;
			}
			this.ListSelect.Visibility = Visibility.Collapsed;
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
			this.oSurveyAnswerDal.AddOne(this.MySurveyId, this.EditVar, text, this.iSeq);
			Logging.Data.WriteLog("逻辑检查时手工修改答案操作:", string.Concat(new string[]
			{
				this.MySurveyId,
				" -- ",
				this.EditVar,
				" -- ",
				this.AnswerCode,
				" --> ",
				text
			}));
			if (SurveyMsg.OutputHistory == "OutputHistory_true")
			{
				string text2 = "";
				if (SurveyHelper.Answer != "")
				{
					text2 = "          Answer : [" + SurveyHelper.Answer + "]" + Environment.NewLine;
				}
				text2 = string.Concat(new object[]
				{
					text2,
					DateTime.Now.ToString(),
					": Case=",
					this.MySurveyId,
					", SeqID=",
					this.iSeq,
					", Ver=",
					SurveyHelper.RoadMapVersion,
					", Qn=",
					this.EditVar,
					", FormName=EditAnswer",
					Environment.NewLine,
					"          Answer : [",
					text,
					"]"
				});
				this.oLogicExplain.OutputResult(text2, "History_" + SurveyHelper.SurveyID + ".Log", true);
			}
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

		private void method_1()
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			if (this.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.QDefine.LIMIT_LOGIC, ',');
				list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list.Add(surveyDetail);
							break;
						}
					}
				}
				if (this.QDefine.SHOW_LOGIC == "" && this.QDefine.IS_RANDOM == 0)
				{
					list.Sort(new Comparison<SurveyDetail>(EditAnswerSingle.Class63.instance.method_0));
				}
				this.QDetails = list;
			}
			if (this.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int j = 0; j < this.QDetails.Count<SurveyDetail>(); j++)
				{
					this.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.QDetails[j].CODE_TEXT);
				}
			}
			foreach (SurveyDetail surveyDetail2 in this.QDetails)
			{
				if (surveyDetail2.CODE != this.AnswerCode)
				{
					list.Add(surveyDetail2);
				}
			}
			this.QDetails = list;
		}

		private void txtNewAnswer_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtNewAnswer_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void btnKeyboard_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		private string MySurveyId;

		private string EditQn = "";

		private string EditVar = "";

		private string EditTitle = "";

		private SurveyDefine QDefine;

		private SurveyDefine QCircleDefineA;

		private SurveyDefine QCircleDefineB;

		private string AnswerCode = "";

		private int iSeq = 9990;

		private string CodeText = "";

		private string CircleTextA = "";

		private string CircleTextB = "";

		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		private List<SurveyDetail> QCircleDetailsA;

		private List<SurveyDetail> QCircleDetailsB;

		private BoldTitle oBoldTitle = new BoldTitle();

		public LogicEngine oLogicEngine = new LogicEngine();

		public LogicExplain oLogicExplain = new LogicExplain();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		[CompilerGenerated]
		[Serializable]
		private sealed class Class63
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly EditAnswerSingle.Class63 instance = new EditAnswerSingle.Class63();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
