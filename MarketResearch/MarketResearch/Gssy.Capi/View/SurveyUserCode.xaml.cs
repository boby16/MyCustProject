using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000054 RID: 84
	public partial class SurveyUserCode : Page
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x000039E4 File Offset: 0x00001BE4
		public SurveyUserCode()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0009495C File Offset: 0x00092B5C
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0);
			string text = this.oQuestion.QDefine.QUESTION_TITLE;
			BoldTitle boldTitle = new BoldTitle();
			text = boldTitle.ReplaceTitle(text);
			boldTitle.DealBoldString(text);
			this.span1.Inlines.Clear();
			this.span2.Inlines.Clear();
			this.span3.Inlines.Clear();
			this.span4.Inlines.Clear();
			this.span5.Inlines.Clear();
			this.span6.Inlines.Clear();
			this.span7.Inlines.Clear();
			if (boldTitle.BoldCount == 0)
			{
				this.txtQuestionTitle.Text = text;
			}
			else
			{
				this.span1.Inlines.Add(new Run(boldTitle.SpanString1));
				this.span2.Inlines.Add(new Run(boldTitle.BoldString1));
				this.span3.Inlines.Add(new Run(boldTitle.SpanString2));
				this.span4.Inlines.Add(new Run(boldTitle.BoldString2));
				this.span5.Inlines.Add(new Run(boldTitle.SpanString3));
				this.span6.Inlines.Add(new Run(boldTitle.BoldString3));
				this.span7.Inlines.Add(new Run(boldTitle.SpanString4));
			}
			if (this.oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				this.txtQuestionTitle.FontSize = (double)this.oQuestion.QDefine.TITLE_FONTSIZE;
			}
			this.txtBefore.Text = SurveyHelper.SurveyCity;
			this.txtAfter.Text = this.oQuestion.QDefine.NOTE;
			if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill.MaxLength = this.oQuestion.QDefine.CONTROL_TYPE;
				this.txtFill.Width = (double)(this.oQuestion.QDefine.CONTROL_TYPE * 30);
			}
			this.txtFill.Focus();
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.txtFill.Text = autoFill.FillInt(this.oQuestion.QDefine);
				this.btnNav_Click(this, e);
			}
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				int control_TYPE = this.oQuestion.QDefine.CONTROL_TYPE;
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName)
					{
						this.txtFill.Text = this.oFunc.RIGHT(surveyAnswer.CODE, control_TYPE);
					}
				}
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00094CC8 File Offset: 0x00092EC8
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtFill.Text;
			text = text.Replace(global::GClass0.smethod_0("^"), global::GClass0.smethod_0(""));
			this.oQuestion.FillText = text;
			if (this.oQuestion.FillText == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return;
			}
			int control_TYPE = this.oQuestion.QDefine.CONTROL_TYPE;
			if (this.oQuestion.FillText.Length != control_TYPE)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgInteNum, control_TYPE.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtFill.Focus();
				return;
			}
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer veanswer = new VEAnswer();
			veanswer.QUESTION_NAME = this.oQuestion.QuestionName;
			veanswer.CODE = this.oQuestion.FillText;
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.FillText;
			list.Add(veanswer);
			this.oLogicEngine.PageAnswer = list;
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (!this.oLogicEngine.CheckLogic(this.CurPageId))
			{
				if (this.oLogicEngine.IS_ALLOW_PASS == 0)
				{
					MessageBox.Show(this.oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				if (MessageBox.Show(this.oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					return;
				}
			}
			this.oQuestion.FillText = SurveyHelper.SurveyCity + text;
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			if (!(SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤")))
			{
			}
			this.MyNav.PageAnswer = list;
			this.method_1();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00094F0C File Offset: 0x0009310C
		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == global::GClass0.smethod_0("@"))
			{
				uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), text);
			}
			if (text == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00095068 File Offset: 0x00093268
		private void txtFill_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
			TextBox textBox = sender as TextBox;
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				if (textBox.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.Decimal)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
			else
			{
				if (e.Key < Key.D0 || e.Key > Key.D9 || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
				{
					e.Handled = true;
					return;
				}
				if (textBox.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.OemPeriod)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00090250 File Offset: 0x0008E450
		private void txtFill_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double num = 0.0;
				if (!double.TryParse(textBox.Text, out num))
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		// Token: 0x04000A18 RID: 2584
		private string MySurveyId;

		// Token: 0x04000A19 RID: 2585
		private string CurPageId;

		// Token: 0x04000A1A RID: 2586
		private NavBase MyNav = new NavBase();

		// Token: 0x04000A1B RID: 2587
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000A1C RID: 2588
		private UDPX oFunc = new UDPX();

		// Token: 0x04000A1D RID: 2589
		private QFill oQuestion = new QFill();
	}
}
