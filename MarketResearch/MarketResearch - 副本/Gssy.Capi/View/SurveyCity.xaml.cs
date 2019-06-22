using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	public partial class SurveyCity : Page
	{
		public SurveyCity()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0, true);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = new LogicEngine
				{
					SurveyID = this.MySurveyId
				}.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list2.Add(surveyDetail);
							break;
						}
					}
				}
				list2.Sort(new Comparison<SurveyDetail>(SurveyCity.Class59.instance.method_0));
				this.oQuestion.QDetails = list2;
			}
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			this.Button_FontSize = ((this.oQuestion.QDefine.CONTROL_FONTSIZE == 0) ? SurveyHelper.BtnFontSize : this.oQuestion.QDefine.CONTROL_FONTSIZE);
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.Button_Height = ((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? SurveyHelper.BtnHeight : this.oQuestion.QDefine.CONTROL_HEIGHT);
			if (this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (this.Button_Type != 2)
				{
					if (this.Button_Type != 4)
					{
						this.Button_Width = SurveyHelper.BtnWidth;
						goto IL_2A7;
					}
				}
				this.Button_Width = 440;
			}
			else
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			IL_2A7:
			this.method_2();
			if (SurveyHelper.AutoDo)
			{
				if (this.method_7())
				{
					this.PageLoaded = false;
					TimeSpan timeSpan_ = DateTime.Now - SurveyHelper.AutoDo_Start;
					MessageBox.Show(string.Format(SurveyMsg.MsgAutoDo_Finish, new object[]
					{
						SurveyHelper.AutoDo_Exist.ToString(),
						SurveyHelper.AutoDo_Create.ToString(),
						SurveyHelper.AutoDo_Filled.ToString(),
						this.oFunc.TimeSpanToString(timeSpan_, "C", "hms")
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					Application.Current.Shutdown();
					return;
				}
				Button button = new AutoFill().FindButton(this.listButton, SurveyHelper.SurveyCity);
				if (button != null)
				{
					this.method_3(button, new RoutedEventArgs());
					this.btnNav_Click(this, e);
				}
			}
			else if (this.listButton.Count == 1)
			{
				this.method_3(this.listButton[0], new RoutedEventArgs());
			}
			this.PageLoaded = true;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel1;
				ScrollViewer scrollViewer = this.scrollmain;
				if (this.Button_Type < 1)
				{
					if (this.Button_Type == 0)
					{
						if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
						{
							this.Button_Type = 2;
							this.PageLoaded = false;
							return;
						}
						int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(this.Button_Height + 15));
						int num2 = Convert.ToInt32((double)(this.oQuestion.QDetails.Count / num) + 0.99999999);
						int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - this.oQuestion.QDetails.Count) / num2);
						this.w_Height = wrapPanel.Height;
						wrapPanel.Height = (double)((num - num3) * (this.Button_Height + 15));
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						this.Button_Type = -1;
						return;
					}
					else
					{
						if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Collapsed)
						{
							this.Button_Type = 4;
							this.PageLoaded = false;
							return;
						}
						wrapPanel.Height = this.w_Height;
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
						wrapPanel.Orientation = Orientation.Horizontal;
						this.Button_Type = 1;
						this.PageLoaded = false;
						return;
					}
				}
				else
				{
					if (this.Button_Type > 20)
					{
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						wrapPanel.Height = (((double)this.Button_Type > scrollViewer.ActualHeight) ? scrollViewer.ActualHeight : ((double)this.Button_Type));
						this.PageLoaded = false;
						return;
					}
					if (this.Button_Type != 2)
					{
						if (this.Button_Type != 4)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_199;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					IL_199:
					if (this.Button_Type != 3)
					{
						if (this.Button_Type != 4)
						{
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							goto IL_1CB;
						}
					}
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					IL_1CB:
					this.PageLoaded = false;
				}
			}
		}

		private void method_2()
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			WrapPanel wrapPanel = this.wrapPanel1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = "b_" + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.CODE;
				button.Click += this.method_3;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = (double)this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
				this.listButton.Add(button);
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			string text = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				this.oQuestion.SelectedCode = text;
				this.SelectedText = (string)button.Content;
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button2 = (Button)obj;
					string a = (string)button2.Tag;
					button2.Style = ((a == text) ? style : style2);
				}
			}
		}

		private bool method_4()
		{
			if (this.oQuestion.SelectedCode == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + "=" + this.oQuestion.SelectedCode;
			return list;
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.method_4())
			{
				return;
			}
			List<VEAnswer> list = this.method_5();
			SurveyHelper.SurveyCity = this.oQuestion.SelectedCode;
			new SurveyConfigBiz().Save("CityText", this.SelectedText);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.method_6();
		}

		private void method_6()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
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
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavLoad = 0;
		}

		private bool method_7()
		{
			bool result = true;
			string text = "";
			bool flag = true;
			while (flag)
			{
				flag = false;
				if (SurveyHelper.AutoDo_listCity.Count > SurveyHelper.AutoDo_CityOrder && SurveyHelper.AutoDo_Total > SurveyHelper.AutoDo_Count)
				{
					string text2 = SurveyHelper.AutoDo_listCity[SurveyHelper.AutoDo_CityOrder];
					string str = this.oFunc.FillString((SurveyHelper.AutoDo_StartOrder + SurveyHelper.AutoDo_Count).ToString(), "0", SurveyMsg.Order_Length, true);
					text = text2 + str;
					if (flag = this.oSurvey.ExistSurvey(text))
					{
						this.oSurvey.GetBySurveyId(text);
						if (this.oSurvey.MySurvey.IS_FINISH != 1 && this.oSurvey.MySurvey.IS_FINISH != 2)
						{
							flag = false;
						}
					}
					if (flag)
					{
						SurveyHelper.AutoDo_Exist++;
						text = "";
						SurveyHelper.AutoDo_Count++;
						if (SurveyHelper.AutoDo_Total <= SurveyHelper.AutoDo_Count)
						{
							SurveyHelper.AutoDo_CityOrder++;
							SurveyHelper.AutoDo_Count = 0;
						}
					}
					else
					{
						SurveyHelper.SurveyCity = text2;
						SurveyHelper.SurveyID = text;
						result = false;
					}
				}
			}
			return result;
		}

		private string MySurveyId = "";

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QSingle oQuestion = new QSingle();

		private string SelectedText = "";

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private SurveyBiz oSurvey = new SurveyBiz();

		[CompilerGenerated]
		[Serializable]
		private sealed class Class59
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly SurveyCity.Class59 instance = new SurveyCity.Class59();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
