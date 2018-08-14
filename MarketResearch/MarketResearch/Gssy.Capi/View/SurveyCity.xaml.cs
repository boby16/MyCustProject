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
	// Token: 0x0200004E RID: 78
	public partial class SurveyCity : Page
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0008E5AC File Offset: 0x0008C7AC
		public SurveyCity()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0008E628 File Offset: 0x0008C828
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0, true);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
						this.oFunc.TimeSpanToString(timeSpan_, global::GClass0.smethod_0("B"), global::GClass0.smethod_0("kůɲ"))
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

		// Token: 0x06000519 RID: 1305 RVA: 0x0008E9F4 File Offset: 0x0008CBF4
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

		// Token: 0x0600051A RID: 1306 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapPanel1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
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

		// Token: 0x0600051B RID: 1307 RVA: 0x0008ED18 File Offset: 0x0008CF18
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
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

		// Token: 0x0600051C RID: 1308 RVA: 0x00003784 File Offset: 0x00001984
		private bool method_4()
		{
			if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0008EE08 File Offset: 0x0008D008
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			return list;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0008EE74 File Offset: 0x0008D074
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.method_4())
			{
				return;
			}
			List<VEAnswer> list = this.method_5();
			SurveyHelper.SurveyCity = this.oQuestion.SelectedCode;
			new SurveyConfigBiz().Save(global::GClass0.smethod_0("KŮɲͼѐզٺݵ"), this.SelectedText);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.method_6();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0008EEE8 File Offset: 0x0008D0E8
		private void method_6()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
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
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0008F000 File Offset: 0x0008D200
		private bool method_7()
		{
			bool result = true;
			string text = global::GClass0.smethod_0("");
			bool flag = true;
			while (flag)
			{
				flag = false;
				if (SurveyHelper.AutoDo_listCity.Count > SurveyHelper.AutoDo_CityOrder && SurveyHelper.AutoDo_Total > SurveyHelper.AutoDo_Count)
				{
					string text2 = SurveyHelper.AutoDo_listCity[SurveyHelper.AutoDo_CityOrder];
					string str = this.oFunc.FillString((SurveyHelper.AutoDo_StartOrder + SurveyHelper.AutoDo_Count).ToString(), global::GClass0.smethod_0("1"), SurveyMsg.Order_Length, true);
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
						text = global::GClass0.smethod_0("");
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

		// Token: 0x04000979 RID: 2425
		private string MySurveyId = global::GClass0.smethod_0("");

		// Token: 0x0400097A RID: 2426
		private string CurPageId;

		// Token: 0x0400097B RID: 2427
		private NavBase MyNav = new NavBase();

		// Token: 0x0400097C RID: 2428
		private UDPX oFunc = new UDPX();

		// Token: 0x0400097D RID: 2429
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400097E RID: 2430
		private QSingle oQuestion = new QSingle();

		// Token: 0x0400097F RID: 2431
		private string SelectedText = global::GClass0.smethod_0("");

		// Token: 0x04000980 RID: 2432
		private List<Button> listButton = new List<Button>();

		// Token: 0x04000981 RID: 2433
		private bool PageLoaded;

		// Token: 0x04000982 RID: 2434
		private int Button_Type;

		// Token: 0x04000983 RID: 2435
		private int Button_Height;

		// Token: 0x04000984 RID: 2436
		private int Button_Width;

		// Token: 0x04000985 RID: 2437
		private int Button_FontSize;

		// Token: 0x04000986 RID: 2438
		private double w_Height;

		// Token: 0x04000987 RID: 2439
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		[Serializable]
		private sealed class Class59
		{
			// Token: 0x0600079B RID: 1947 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D39 RID: 3385
			public static readonly SurveyCity.Class59 instance = new SurveyCity.Class59();

			// Token: 0x04000D3A RID: 3386
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
