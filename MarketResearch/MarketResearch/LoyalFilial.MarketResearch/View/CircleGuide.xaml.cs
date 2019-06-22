using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;

namespace LoyalFilial.MarketResearch.View
{
	public partial class CircleGuide : Page
	{
		public CircleGuide()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			QDisplay qdisplay = new QDisplay();
			qdisplay.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			string navOperation = SurveyHelper.NavOperation;
			this.MyNav.GroupLevel = qdisplay.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != "")
			{
				this.MyNav.GroupPageType = qdisplay.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = qdisplay.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (navOperation == "Back")
				{
					if (this.MyNav.GroupLevel == "A" && this.MyNav.CircleACurrent > 1)
					{
						this.MyNav.CircleACurrent = this.MyNav.CircleACurrent - 1;
					}
				}
				else if (this.MyNav.GroupLevel == "A" && this.MyNav.CircleACurrent == 0)
				{
					this.MyNav.CircleACurrent = 1;
				}
				this.MyNav.GetCircleInfo(this.MySurveyId);
			}
			string[] array = new LogicEngine
			{
				SurveyID = this.MySurveyId,
				CircleBCode = this.MyNav.CircleBCode,
				CircleACode = this.MyNav.CircleACode,
				CircleACodeText = this.MyNav.CircleCodeTextA,
				CircleACount = this.MyNav.CircleACount,
				CircleACurrent = this.MyNav.CircleACurrent,
				CircleBCodeText = this.MyNav.CircleCodeTextB,
				CircleBCount = this.MyNav.CircleBCount,
				CircleBCurrent = this.MyNav.CircleBCurrent
			}.CircleGuideLogic(this.CurPageId, 1);
			if (array.Count<string>() > 0 && array[0].ToString() != "")
			{
				new RandomBiz().RebuildCircleGuide(this.MySurveyId, qdisplay.QDefine.QUESTION_NAME, array, qdisplay.QDefine.IS_RANDOM);
			}
			int page_COUNT_DOWN = qdisplay.QDefine.PAGE_COUNT_DOWN;
			if (page_COUNT_DOWN > 0)
			{
				Thread.Sleep(page_COUNT_DOWN);
			}
			if (navOperation == "Back")
			{
				this.method_2();
				SurveyHelper.NavOperation = "Back";
				return;
			}
			this.method_1();
		}

		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
				string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
				SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
				string uriString = string.Format("pack://application:,,,/View/{0}.xaml", text);
				if (text.Substring(0, 1) == "A")
				{
					uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", text);
				}
				if (text == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
				}
				else if (this.oPageNav.FormIsOK(this.MyNav.RoadMap.FORM_NAME))
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
				}
				else
				{
					string text2 = string.Format(SurveyMsg.MsgErrorJump, new object[]
					{
						this.MySurveyId,
						this.CurPageId,
						this.MyNav.RoadMap.VERSION_ID,
						this.MyNav.RoadMap.PAGE_ID,
						this.MyNav.RoadMap.FORM_NAME
					});
					MessageBox.Show(string.Concat(new string[]
					{
						SurveyMsg.MsgErrorRoadmap,
						Environment.NewLine,
						Environment.NewLine,
						text2,
						SurveyMsg.MsgErrorEnd
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.oLogicEngine.OutputResult(text2, "MarketResearchDebug.Log");
					if (this.CurPageId == SurveyHelper.SurveyFirstPage)
					{
						MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					this.method_2();
					SurveyHelper.NavOperation = "Back";
				}
				SurveyHelper.SurveySequence = surveySequence + 1;
				SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
				SurveyHelper.NavGoBackTimes = 0;
				SurveyHelper.NavOperation = "Normal";
				SurveyHelper.NavLoad = 0;
			}
			catch (Exception)
			{
				string text3 = string.Format(SurveyMsg.MsgErrorJump, new object[]
				{
					this.MySurveyId,
					this.CurPageId,
					this.MyNav.RoadMap.VERSION_ID,
					this.MyNav.RoadMap.PAGE_ID,
					this.MyNav.RoadMap.FORM_NAME
				});
				MessageBox.Show(string.Concat(new string[]
				{
					SurveyMsg.MsgErrorRoadmap,
					Environment.NewLine,
					Environment.NewLine,
					text3,
					SurveyMsg.MsgErrorEnd
				}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.oLogicEngine.OutputResult(text3, "MarketResearchDebug.Log");
				if (this.CurPageId == SurveyHelper.SurveyFirstPage)
				{
					MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					this.method_2();
					SurveyHelper.NavOperation = "Back";
				}
			}
		}

		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(this.MySurveyId == "") && !(this.CurPageId == SurveyHelper.SurveyFirstPage))
			{
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				this.MyNav.PrePage(this.MySurveyId, surveySequence, roadMapVersion);
				SurveyHelper.CircleACount = this.MyNav.Sequence.CIRCLE_A_COUNT;
				SurveyHelper.CircleACurrent = this.MyNav.Sequence.CIRCLE_A_CURRENT;
				SurveyHelper.CircleBCount = this.MyNav.Sequence.CIRCLE_B_COUNT;
				SurveyHelper.CircleBCurrent = this.MyNav.Sequence.CIRCLE_B_CURRENT;
				string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
				if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == "A")
				{
					uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
				}
				if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
				}
				else
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
				}
				SurveyHelper.SurveySequence = surveySequence - 1;
				SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
				return;
			}
			SurveyHelper.NavOperation = "Normal";
			MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.method_1();
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		public LogicEngine oLogicEngine = new LogicEngine();
	}
}
