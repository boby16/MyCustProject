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
	// Token: 0x02000059 RID: 89
	public partial class EditAnswerSingle : Window
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x000997AC File Offset: 0x000979AC
		public EditAnswerSingle()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00099890 File Offset: 0x00097A90
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.MySurveyId = SurveyHelper.SurveyID;
			SurveyHelper.QueryEditQn = this.oLogicEngine.ReplaceSpecialFlag(SurveyHelper.QueryEditQn);
			List<string> list = this.oBoldTitle.ParaToList(SurveyHelper.QueryEditQn, global::GClass0.smethod_0("-Į"));
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
				this.EditVar = this.EditVar + global::GClass0.smethod_0("]œ") + list[1];
			}
			if (list.Count > 2)
			{
				this.EditVar = this.EditVar + global::GClass0.smethod_0("]œ") + list[2];
			}
			this.QDefine = this.oSurveyDefineDal.GetByName(this.EditQn);
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			if (list.Count > 1 && this.QDefine.GROUP_CODEA != global::GClass0.smethod_0(""))
			{
				this.QCircleDefineA = this.oSurveyDefineDal.GetByName(this.QDefine.GROUP_CODEA);
				if (this.QCircleDefineA.DETAIL_ID != global::GClass0.smethod_0(""))
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
			if (list.Count > 2 && this.QDefine.GROUP_CODEB != global::GClass0.smethod_0(""))
			{
				this.QCircleDefineB = this.oSurveyDefineDal.GetByName(this.QDefine.GROUP_CODEB);
				if (this.QCircleDefineB.DETAIL_ID != global::GClass0.smethod_0(""))
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
			if (this.CircleTextA != global::GClass0.smethod_0(""))
			{
				TextBlock textBlock = this.txtTitle;
				textBlock.Text = textBlock.Text + global::GClass0.smethod_0("#įȡ") + this.CircleTextA;
			}
			if (this.CircleTextB != global::GClass0.smethod_0(""))
			{
				TextBlock textBlock2 = this.txtTitle;
				textBlock2.Text = textBlock2.Text + global::GClass0.smethod_0("#įȡ") + this.CircleTextB;
			}
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(this.MySurveyId, this.EditVar);
			this.AnswerCode = one.CODE;
			this.iSeq = one.SEQUENCE_ID;
			this.txtAnswer.Text = this.AnswerCode;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				foreach (SurveyDetail surveyDetail3 in this.QDetails)
				{
					if (surveyDetail3.CODE == this.AnswerCode)
					{
						this.CodeText = surveyDetail3.CODE_TEXT;
						break;
					}
				}
				if (this.CodeText != global::GClass0.smethod_0(""))
				{
					this.txtAnswer.Text = this.AnswerCode + global::GClass0.smethod_0("）") + this.CodeText + global::GClass0.smethod_0("（");
				}
				this.method_1();
				this.ListSelect.ItemsSource = this.QDetails;
				this.ListSelect.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
				this.ListSelect.Visibility = Visibility.Visible;
				this.txtNewAnswer.IsEnabled = false;
				this.ListSelect.Focus();
				return;
			}
			this.ListSelect.Visibility = Visibility.Collapsed;
			this.txtNewAnswer.Focus();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00099DBC File Offset: 0x00097FBC
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtNewAnswer.Text;
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			this.oSurveyAnswerDal.AddOne(this.MySurveyId, this.EditVar, text, this.iSeq);
			Logging.Data.WriteLog(global::GClass0.smethod_0("逵躜櫌擮懼杂寭䣩洿牑扌濎䍞഻"), string.Concat(new string[]
			{
				this.MySurveyId,
				global::GClass0.smethod_0("$Įȯ̡"),
				this.EditVar,
				global::GClass0.smethod_0("$Įȯ̡"),
				this.AnswerCode,
				global::GClass0.smethod_0("%ĩȮ̼С"),
				text
			}));
			if (SurveyMsg.OutputHistory == global::GClass0.smethod_0("]ŤɤͿѻչلݢࡹॽ੧୵౿൚๰ཱၷᅤ"))
			{
				string text2 = global::GClass0.smethod_0("");
				if (SurveyHelper.Answer != global::GClass0.smethod_0(""))
				{
					text2 = global::GClass0.smethod_0("4ĳȲ̱аԯخܭࠬफੋ୧౻൰๣ཷဤᄹሢፚ") + SurveyHelper.Answer + global::GClass0.smethod_0("\\") + Environment.NewLine;
				}
				text2 = string.Concat(new object[]
				{
					text2,
					DateTime.Now.ToString(),
					global::GClass0.smethod_0("=ĦɆͥѰէؼ"),
					this.MySurveyId,
					global::GClass0.smethod_0("$ħɕ͠ѵՊنܼ"),
					this.iSeq,
					global::GClass0.smethod_0("*ĥɒͦѰԼ"),
					SurveyHelper.RoadMapVersion,
					global::GClass0.smethod_0(")Ĥɒͬм"),
					this.EditVar,
					global::GClass0.smethod_0("9Ĵɕͽѣսفݯࡠ३ਸ਼୏౭ൡ๳ཇၫᅷቴ፧ᑳ"),
					Environment.NewLine,
					global::GClass0.smethod_0("4ĳȲ̱аԯخܭࠬफੋ୧౻൰๣ཷဤᄹሢፚ"),
					text,
					global::GClass0.smethod_0("\\")
				});
				this.oLogicExplain.OutputResult(text2, global::GClass0.smethod_0("@Ůɵͱѫձٻݞ") + SurveyHelper.SurveyID + global::GClass0.smethod_0("*ŏɭͦ"), true);
			}
			base.Close();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00003BCD File Offset: 0x00001DCD
		private void txtNewAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnSave.IsEnabled)
			{
				this.btnSave_Click(sender, e);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00003BED File Offset: 0x00001DED
		private void ListSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.txtNewAnswer.Text = (string)this.ListSelect.SelectedValue;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00099FC4 File Offset: 0x000981C4
		private void method_1()
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			if (this.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
				if (this.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.QDefine.IS_RANDOM == 0)
				{
					list.Sort(new Comparison<SurveyDetail>(EditAnswerSingle.Class63.instance.method_0));
				}
				this.QDetails = list;
			}
			if (this.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
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

		// Token: 0x060005EC RID: 1516 RVA: 0x00002581 File Offset: 0x00000781
		private void txtNewAnswer_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtNewAnswer_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000234B File Offset: 0x0000054B
		private void btnKeyboard_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		// Token: 0x04000AB0 RID: 2736
		private string MySurveyId;

		// Token: 0x04000AB1 RID: 2737
		private string EditQn = global::GClass0.smethod_0("");

		// Token: 0x04000AB2 RID: 2738
		private string EditVar = global::GClass0.smethod_0("");

		// Token: 0x04000AB3 RID: 2739
		private string EditTitle = global::GClass0.smethod_0("");

		// Token: 0x04000AB4 RID: 2740
		private SurveyDefine QDefine;

		// Token: 0x04000AB5 RID: 2741
		private SurveyDefine QCircleDefineA;

		// Token: 0x04000AB6 RID: 2742
		private SurveyDefine QCircleDefineB;

		// Token: 0x04000AB7 RID: 2743
		private string AnswerCode = global::GClass0.smethod_0("");

		// Token: 0x04000AB8 RID: 2744
		private int iSeq = 9990;

		// Token: 0x04000AB9 RID: 2745
		private string CodeText = global::GClass0.smethod_0("");

		// Token: 0x04000ABA RID: 2746
		private string CircleTextA = global::GClass0.smethod_0("");

		// Token: 0x04000ABB RID: 2747
		private string CircleTextB = global::GClass0.smethod_0("");

		// Token: 0x04000ABC RID: 2748
		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		// Token: 0x04000ABD RID: 2749
		private List<SurveyDetail> QCircleDetailsA;

		// Token: 0x04000ABE RID: 2750
		private List<SurveyDetail> QCircleDetailsB;

		// Token: 0x04000ABF RID: 2751
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000AC0 RID: 2752
		public LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000AC1 RID: 2753
		public LogicExplain oLogicExplain = new LogicExplain();

		// Token: 0x04000AC2 RID: 2754
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000AC3 RID: 2755
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x04000AC4 RID: 2756
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x020000BE RID: 190
		[CompilerGenerated]
		[Serializable]
		private sealed class Class63
		{
			// Token: 0x060007A4 RID: 1956 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D46 RID: 3398
			public static readonly EditAnswerSingle.Class63 instance = new EditAnswerSingle.Class63();

			// Token: 0x04000D47 RID: 3399
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
