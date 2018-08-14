using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;

namespace Gssy.Capi
{
	// Token: 0x02000005 RID: 5
	public partial class SurveyAuto : Window
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002259 File Offset: 0x00000459
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002261 File Offset: 0x00000461
		public string SurveyId { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000226A File Offset: 0x0000046A
		public SurveyAuto()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00005C20 File Offset: 0x00003E20
		private void btnRun_Click(object sender, RoutedEventArgs e)
		{
			this.RTxtQRun.AppendText(global::GClass0.smethod_0("刔壃儑雨坲Ԫحܬ࠯") + Environment.NewLine);
			int num = SurveyHelper.SurveySequence;
			this.SurveyId = this.SurveyInit();
			this.oAutoAnswer.SurveyId = this.SurveyId;
			SurveyHelper.SurveyCity = global::GClass0.smethod_0("0");
			SurveyHelper.SurveyID = this.SurveyId;
			this.RTxtQRun.AppendText(global::GClass0.smethod_0("廲篌韨偲Ъԭجܯ") + this.SurveyId + Environment.NewLine);
			string text = SurveyHelper.SurveyFirstPage;
			string string_ = global::GClass0.smethod_0("");
			while (!this.IsFinish)
			{
				SurveyHelper.NavCurPage = text;
				this.RTxtQRun.AppendText(global::GClass0.smethod_0("门割驱̣Юԡ") + text + Environment.NewLine);
				int num2 = this.oAutoAnswer.PageInfo(text);
				for (int i = 0; i < num2; i++)
				{
					string_ = this.oAutoAnswer.GetChildByIndex(text, i);
					this.oAutoAnswer.QuestionInit(this.SurveyId, string_);
					this.RTxtQInfo.AppendText(this.oAutoAnswer.QuestionInfo());
					if (this.oAutoAnswer.MySurveyDefine.QUESTION_TYPE < 4)
					{
						this.oAutoAnswer.SetMain(this.SurveyId, string_, num);
						this.RTxtQRun.AppendText(global::GClass0.smethod_0("筐楋ȸ̡") + this.oAutoAnswer.MyAnswer + Environment.NewLine);
					}
				}
				text = this.oAutoAnswer.NextPage(this.SurveyId, num, text, SurveyHelper.RoadMapVersion);
				this.RTxtQRun.AppendText(global::GClass0.smethod_0("闤剾驽軨焷锾覕ܣ࠮ड") + this.oAutoAnswer.RouteLogic + Environment.NewLine);
				num++;
				SurveyHelper.SurveySequence = num;
				if (text == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || text == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤"))
				{
					this.RTxtQRun.AppendText(global::GClass0.smethod_0("门割驱̣Юԡ") + text + Environment.NewLine);
					this.IsFinish = true;
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002283 File Offset: 0x00000483
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005E38 File Offset: 0x00004038
		public string SurveyInit()
		{
			string text = global::GClass0.smethod_0("2");
			string autoSurveyId = this.oAutoAnswer.GetAutoSurveyId(text, 5);
			string versionID = SurveyMsg.VersionID;
			SurveyHelper.SurveyCity = text;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.SurveyExtend1 = global::GClass0.smethod_0("0");
			string surveyExtend = SurveyHelper.SurveyExtend1;
			this.oAutoAnswer.SurveyInit(autoSurveyId, versionID, surveySequence, text, surveyExtend);
			return autoSurveyId;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000228F File Offset: 0x0000048F
		private void btnReport_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000228F File Offset: 0x0000048F
		private void btnTest_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x04000037 RID: 55
		private bool IsFinish;

		// Token: 0x04000038 RID: 56
		private AutoAnswer oAutoAnswer = new AutoAnswer();
	}
}
