using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.View
{
	public partial class CircleStart : Page
	{
		public CircleStart()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			bool flag = false;
			if (SurveyHelper.CircleACurrent == 0 && this.oQuestion.QDefine.GROUP_LEVEL == "A")
			{
				flag = true;
			}
			if (SurveyHelper.CircleBCurrent == 0 && this.oQuestion.QDefine.GROUP_LEVEL == "B")
			{
				flag = true;
			}
			if (this.oQuestion.QDefine.GROUP_LEVEL == "")
			{
				flag = true;
			}
			if (flag)
			{
				this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
				if (this.MyNav.GroupLevel == "B")
				{
					this.MyNav.GroupLevel = "A";
					this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
					this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
					this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
					this.MyNav.CircleACount = SurveyHelper.CircleACount;
					this.MyNav.GetCircleInfo(this.MySurveyId);
					new List<VEAnswer>().Add(new VEAnswer
					{
						QUESTION_NAME = this.MyNav.GroupCodeA,
						CODE = this.MyNav.CircleACode,
						CODE_TEXT = this.MyNav.CircleCodeTextA
					});
					SurveyHelper.CircleACode = this.MyNav.CircleACode;
					SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
					SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
					SurveyHelper.CircleACount = this.MyNav.CircleACount;
				}
				else
				{
					this.MyNav.GroupLevel = "";
				}
				this.oLogicEngine.SurveyID = this.MySurveyId;
				if (this.MyNav.GroupLevel != "")
				{
					this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
					this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
					this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
					this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				}
				string[] array = this.oLogicEngine.CircleGuideLogic(this.CurPageId, 1);
				if (this.oQuestion.QDefine.SHOW_LOGIC != "")
				{
					string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
					string[] array2 = this.oLogicEngine.aryCode(show_LOGIC, ',');
					List<string> list = new List<string>();
					bool flag2 = this.oLogicEngine.IsExistCircleGuide(this.CurPageId);
					for (int i = 0; i < array2.Count<string>(); i++)
					{
						if (flag2)
						{
							foreach (string text in array)
							{
								if (text == array2[i].ToString())
								{
									list.Add(text);
									break;
								}
							}
						}
						else
						{
							foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
							{
								if (surveyDetail.CODE == array2[i].ToString())
								{
									list.Add(surveyDetail.CODE);
									break;
								}
							}
						}
					}
					array = list.ToArray();
					this.oQuestion.QDefine.IS_RANDOM = 0;
				}
				if (array.Count<string>() > 0 && array[0].ToString() != "")
				{
					this.oRandomBiz.RebuildCircleGuide(this.MySurveyId, this.oQuestion.QDefine.QUESTION_NAME, array, this.oQuestion.QDefine.IS_RANDOM);
				}
				else if (this.oQuestion.QDefine.IS_RANDOM > 0)
				{
					List<string> list2 = new List<string>();
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						list2.Add(surveyDetail2.CODE);
					}
					array = list2.ToArray();
					this.oRandomBiz.RebuildCircleGuide(this.MySurveyId, this.oQuestion.QDefine.QUESTION_NAME, array, this.oQuestion.QDefine.IS_RANDOM);
				}
			}
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (!(this.MyNav.GroupLevel == "A") && !(this.MyNav.GroupLevel == "B"))
			{
				SurveyHelper.CircleACode = "";
				SurveyHelper.CircleACodeText = "";
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = "";
				SurveyHelper.CircleBCodeText = "";
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
			}
			else
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == "B")
				{
					this.MyNav.GroupCodeB = this.oQuestion.QDefine.GROUP_CODEB;
					this.MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					this.MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				List<VEAnswer> list3 = new List<VEAnswer>();
				list3.Add(new VEAnswer
				{
					QUESTION_NAME = this.MyNav.GroupCodeA,
					CODE = this.MyNav.CircleACode,
					CODE_TEXT = this.MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = this.MyNav.CircleACode;
				SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
				if (this.MyNav.GroupLevel == "B")
				{
					list3.Add(new VEAnswer
					{
						QUESTION_NAME = this.MyNav.GroupCodeB,
						CODE = this.MyNav.CircleBCode,
						CODE_TEXT = this.MyNav.CircleCodeTextB
					});
					SurveyHelper.CircleBCode = this.MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = this.MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
				}
			}
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != "")
			{
				this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			int page_COUNT_DOWN = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (page_COUNT_DOWN > 0)
			{
				Thread.Sleep(page_COUNT_DOWN);
			}
			if (SurveyHelper.NavOperation == "Back")
			{
				this.method_2();
				SurveyHelper.NavOperation = "Back";
				return;
			}
			if (this.MyNav.GroupLevel != "")
			{
				string text2 = (this.MyNav.GroupLevel == "B") ? this.MyNav.CircleBCode : this.MyNav.CircleACode;
				SurveyHelper.Answer = this.oQuestion.QuestionName + "=" + text2;
				this.oQuestion.FillText = text2;
				this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			}
			this.method_1();
		}

		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				if (this.MyNav.GroupLevel == "")
				{
					this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
				}
				else
				{
					this.MyNav.NextCirclePage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
					SurveyHelper.CircleACount = this.MyNav.CircleACount;
					SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
					if (this.MyNav.IsLastA && (this.MyNav.GroupPageType == 0 || this.MyNav.GroupPageType == 2))
					{
						SurveyHelper.CircleACode = "";
						SurveyHelper.CircleACodeText = "";
					}
					if (this.MyNav.GroupLevel == "B")
					{
						SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
						SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
						if (this.MyNav.IsLastB && (this.MyNav.GroupPageType == 10 || this.MyNav.GroupPageType == 12 || this.MyNav.GroupPageType == 30 || this.MyNav.GroupPageType == 32))
						{
							SurveyHelper.CircleBCode = "";
							SurveyHelper.CircleBCodeText = "";
						}
					}
				}
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
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, surveySequence);
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

		private QFill oQuestion = new QFill();

		private RandomBiz oRandomBiz = new RandomBiz();
	}
}
