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
	public partial class SurveyAuto : Window
	{
		public string SurveyId { get; set; }

		public SurveyAuto()
		{
			this.InitializeComponent();
		}

		private void btnRun_Click(object sender, RoutedEventArgs e)
		{
			this.RTxtQRun.AppendText("初始化问卷...." + Environment.NewLine);
			int num = SurveyHelper.SurveySequence;
			this.SurveyId = this.SurveyInit();
			this.oAutoAnswer.SurveyId = this.SurveyId;
			SurveyHelper.SurveyCity = "1";
			SurveyHelper.SurveyID = this.SurveyId;
			this.RTxtQRun.AppendText("建立问卷...." + this.SurveyId + Environment.NewLine);
			string text = SurveyHelper.SurveyFirstPage;
			string string_ = "";
			while (!this.IsFinish)
			{
				SurveyHelper.NavCurPage = text;
				this.RTxtQRun.AppendText("问卷页 , " + text + Environment.NewLine);
				int num2 = this.oAutoAnswer.PageInfo(text);
				for (int i = 0; i < num2; i++)
				{
					string_ = this.oAutoAnswer.GetChildByIndex(text, i);
					this.oAutoAnswer.QuestionInit(this.SurveyId, string_);
					this.RTxtQInfo.AppendText(this.oAutoAnswer.QuestionInfo());
					if (this.oAutoAnswer.MySurveyDefine.QUESTION_TYPE < 4)
					{
						this.oAutoAnswer.SetMain(this.SurveyId, string_, num);
						this.RTxtQRun.AppendText("答案: " + this.oAutoAnswer.MyAnswer + Environment.NewLine);
					}
				}
				text = this.oAutoAnswer.NextPage(this.SurveyId, num, text, SurveyHelper.RoadMapVersion);
				this.RTxtQRun.AppendText("问卷页路由逻辑 , " + this.oAutoAnswer.RouteLogic + Environment.NewLine);
				num++;
				SurveyHelper.SurveySequence = num;
				if (text == "EndSummary" || text == "EndTerminate")
				{
					this.RTxtQRun.AppendText("问卷页 , " + text + Environment.NewLine);
					this.IsFinish = true;
				}
			}
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		public string SurveyInit()
		{
			string text = "3";
			string autoSurveyId = this.oAutoAnswer.GetAutoSurveyId(text, 5);
			string versionID = SurveyMsg.VersionID;
			SurveyHelper.SurveyCity = text;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.SurveyExtend1 = "1";
			string surveyExtend = SurveyHelper.SurveyExtend1;
			this.oAutoAnswer.SurveyInit(autoSurveyId, versionID, surveySequence, text, surveyExtend);
			return autoSurveyId;
		}

		private void btnReport_Click(object sender, RoutedEventArgs e)
		{
		}

		private void btnTest_Click(object sender, RoutedEventArgs e)
		{
		}

		private bool IsFinish;

		private AutoAnswer oAutoAnswer = new AutoAnswer();
	}
}
