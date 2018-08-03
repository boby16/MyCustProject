using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
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

namespace Gssy.Capi.View
{
	public class SurveyCity : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__16_0;

			internal int _003F298_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId = _003F487_003F._003F488_003F("");

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QSingle oQuestion = new QSingle();

		private string SelectedText = _003F487_003F._003F488_003F("");

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private SurveyBiz oSurvey = new SurveyBiz();

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal Button btnNav;

		private bool _contentLoaded;

		public SurveyCity()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00ba: Incompatible stack heights: 0 vs 1
			//IL_00c1: Incompatible stack heights: 0 vs 1
			//IL_0206: Incompatible stack heights: 0 vs 2
			//IL_021d: Incompatible stack heights: 0 vs 1
			//IL_0264: Incompatible stack heights: 0 vs 1
			//IL_0283: Incompatible stack heights: 0 vs 1
			//IL_0288: Incompatible stack heights: 0 vs 1
			//IL_02e0: Incompatible stack heights: 0 vs 1
			//IL_02ff: Incompatible stack heights: 0 vs 1
			//IL_0304: Incompatible stack heights: 0 vs 1
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			oQuestion.Init(CurPageId, 0, true);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text = list[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_00c2: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = new LogicEngine
				{
					SurveyID = MySurveyId
				}.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					foreach (SurveyDetail qDetail in oQuestion.QDetails)
					{
						if (qDetail.CODE == array[i].ToString())
						{
							list2.Add(qDetail);
							break;
						}
					}
				}
				if (_003F7_003F._003C_003E9__16_0 == null)
				{
					_003F7_003F._003C_003E9__16_0 = _003F7_003F._003C_003E9._003F298_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0222: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0222: Stack underflow*/);
				oQuestion.QDetails = list2;
			}
			Button_Type = oQuestion.QDefine.CONTROL_TYPE;
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				int cONTROL_FONTSIZE = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			else
			{
				int btnFontSize = SurveyHelper.BtnFontSize;
			}
			((SurveyCity)/*Error near IL_028d: Stack underflow*/).Button_FontSize = (int)/*Error near IL_028d: Stack underflow*/;
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_FontSize = Math.Abs(Button_FontSize);
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((SurveyCity)/*Error near IL_0309: Stack underflow*/).Button_Height = (int)/*Error near IL_0309: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440;
				}
				else
				{
					Button_Width = SurveyHelper.BtnWidth;
				}
			}
			else
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			_003F28_003F();
			if (SurveyHelper.AutoDo)
			{
				if (_003F181_003F())
				{
					PageLoaded = false;
					TimeSpan _003F134_003F = DateTime.Now - SurveyHelper.AutoDo_Start;
					MessageBox.Show(string.Format(SurveyMsg.MsgAutoDo_Finish, SurveyHelper.AutoDo_Exist.ToString(), SurveyHelper.AutoDo_Create.ToString(), SurveyHelper.AutoDo_Filled.ToString(), oFunc.TimeSpanToString(_003F134_003F, _003F487_003F._003F488_003F("B"), _003F487_003F._003F488_003F("kůɲ"))), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					Application.Current.Shutdown();
					return;
				}
				Button button = new AutoFill().FindButton(listButton, SurveyHelper.SurveyCity);
				if (button != null)
				{
					_003F29_003F(button, new RoutedEventArgs());
					_003F58_003F(this, _003F348_003F);
				}
			}
			else if (listButton.Count == 1)
			{
				_003F29_003F(listButton[0], new RoutedEventArgs());
			}
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_01d5: Incompatible stack heights: 0 vs 1
			//IL_01e6: Incompatible stack heights: 0 vs 2
			//IL_0214: Incompatible stack heights: 0 vs 2
			//IL_0219: Incompatible stack heights: 0 vs 1
			//IL_0225: Incompatible stack heights: 0 vs 1
			//IL_022a: Incompatible stack heights: 1 vs 0
			//IL_0235: Incompatible stack heights: 0 vs 1
			//IL_0255: Incompatible stack heights: 0 vs 2
			if (!PageLoaded)
			{
				return;
			}
			WrapPanel wrapPanel = wrapPanel1;
			ScrollViewer scrollViewer = scrollmain;
			if (Button_Type < 1)
			{
				int button_Type = Button_Type;
				if ((int)/*Error near IL_01da: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_01eb: Stack underflow*/ == /*Error near IL_01eb: Stack underflow*/)
					{
						Button_Type = 2;
						PageLoaded = false;
					}
					else
					{
						int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(Button_Height + 15));
						int num2 = Convert.ToInt32((double)(oQuestion.QDetails.Count / num) + 0.99999999);
						int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - oQuestion.QDetails.Count) / num2);
						w_Height = wrapPanel.Height;
						wrapPanel.Height = (double)((num - num3) * (Button_Height + 15));
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						Button_Type = -1;
					}
				}
				else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Collapsed)
				{
					Button_Type = 4;
					PageLoaded = false;
				}
				else
				{
					wrapPanel.Height = w_Height;
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					wrapPanel.Orientation = Orientation.Horizontal;
					Button_Type = 1;
					PageLoaded = false;
				}
				return;
			}
			if (Button_Type > 20)
			{
				((ScrollViewer)/*Error near IL_0120: Stack underflow*/).VerticalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_0120: Stack underflow*/;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				wrapPanel.Orientation = Orientation.Vertical;
				double actualHeight;
				if ((double)Button_Type > scrollViewer.ActualHeight)
				{
					actualHeight = scrollViewer.ActualHeight;
				}
				else
				{
					double num4 = (double)Button_Type;
				}
				((FrameworkElement)/*Error near IL_0151: Stack underflow*/).Height = actualHeight;
				PageLoaded = false;
				return;
			}
			if (Button_Type == 2 || ((SurveyCity)/*Error near IL_016a: Stack underflow*/).Button_Type == 4)
			{
				wrapPanel.Orientation = Orientation.Vertical;
			}
			else
			{
				wrapPanel.Orientation = Orientation.Horizontal;
			}
			if (Button_Type != 3)
			{
				int button_Type2 = Button_Type;
				if (/*Error near IL_025a: Stack underflow*/ != /*Error near IL_025a: Stack underflow*/)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					goto IL_026a;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_026a;
			IL_026a:
			PageLoaded = false;
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapPanel1;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.CODE;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = (double)Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
				listButton.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00da: Incompatible stack heights: 0 vs 1
			//IL_00ea: Incompatible stack heights: 0 vs 1
			//IL_00eb: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string text = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				oQuestion.SelectedCode = text;
				SelectedText = (string)button.Content;
				foreach (Button child in wrapPanel1.Children)
				{
					string a = (string)child.Tag;
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_00f0: Stack underflow*/).Style = (Style)/*Error near IL_00f0: Stack underflow*/;
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_0043: Incompatible stack heights: 0 vs 3
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				string msgNotSelected = SurveyMsg.MsgNotSelected;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0026: Stack underflow*/, (string)/*Error near IL_0026: Stack underflow*/, (MessageBoxButton)/*Error near IL_0026: Stack underflow*/, MessageBoxImage.Hand);
				return true;
			}
			goto IL_0043;
			IL_0029:
			goto IL_0043;
			IL_0043:
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> result = new List<VEAnswer>
			{
				new VEAnswer
				{
					QUESTION_NAME = oQuestion.QuestionName,
					CODE = oQuestion.SelectedCode
				}
			};
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			return result;
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0073: Incompatible stack heights: 0 vs 4
			if (_003F87_003F())
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			List<VEAnswer> list = _003F88_003F();
			SurveyHelper.SurveyCity = oQuestion.SelectedCode;
			new SurveyConfigBiz().Save(_003F487_003F._003F488_003F("KŮɲͼѐզٺݵ"), SelectedText);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_004b: Stack underflow*/, (string)/*Error near IL_004b: Stack underflow*/, (MessageBoxButton)/*Error near IL_004b: Stack underflow*/, (MessageBoxImage)/*Error near IL_004b: Stack underflow*/);
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
			return;
			IL_0056:
			goto IL_000b;
		}

		private void _003F81_003F()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.PageStartTime = SurveyHelper.PageStartTime;
			MyNav.RecordFileName = SurveyHelper.RecordFileName;
			MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			MyNav.NextPage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
			if (!(MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName))
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			else
			{
				base.NavigationService.Refresh();
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavLoad = 0;
		}

		private bool _003F181_003F()
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected I4, but got Unknown
			//IL_010b: Incompatible stack heights: 0 vs 2
			//IL_0136: Incompatible stack heights: 0 vs 2
			//IL_0151: Incompatible stack heights: 0 vs 2
			//IL_016d: Incompatible stack heights: 0 vs 2
			//IL_017e: Incompatible stack heights: 0 vs 1
			bool result = true;
			string text = _003F487_003F._003F488_003F("");
			bool flag = true;
			goto IL_00ec;
			IL_00ec:
			while (flag)
			{
				flag = false;
				if (SurveyHelper.AutoDo_listCity.Count > SurveyHelper.AutoDo_CityOrder)
				{
					int autoDo_Total = SurveyHelper.AutoDo_Total;
					int autoDo_Count = SurveyHelper.AutoDo_Count;
					if (/*Error near IL_0110: Stack underflow*/ > /*Error near IL_0110: Stack underflow*/)
					{
						string text2 = SurveyHelper.AutoDo_listCity[SurveyHelper.AutoDo_CityOrder];
						string str = oFunc.FillString((SurveyHelper.AutoDo_StartOrder + SurveyHelper.AutoDo_Count).ToString(), _003F487_003F._003F488_003F("1"), SurveyMsg.Order_Length, true);
						text = text2 + str;
						flag = oSurvey.ExistSurvey(text);
						if (flag)
						{
							SurveyBiz oSurvey2 = oSurvey;
							((SurveyBiz)/*Error near IL_0081: Stack underflow*/).GetBySurveyId((string)/*Error near IL_0081: Stack underflow*/);
							if (oSurvey.MySurvey.IS_FINISH != 1)
							{
								int iS_FINISH = oSurvey.MySurvey.IS_FINISH;
								if (/*Error near IL_0156: Stack underflow*/ != /*Error near IL_0156: Stack underflow*/)
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							int autoDo_Exist = SurveyHelper.AutoDo_Exist;
							SurveyHelper.AutoDo_Exist = /*Error near IL_00a3: Stack underflow*/+ /*Error near IL_00a3: Stack underflow*/;
							text = _003F487_003F._003F488_003F("");
							SurveyHelper.AutoDo_Count++;
							if (SurveyHelper.AutoDo_Total <= SurveyHelper.AutoDo_Count)
							{
								int num = SurveyHelper.AutoDo_CityOrder + 1;
								SurveyHelper.AutoDo_CityOrder = (int)/*Error near IL_00d3: Stack underflow*/;
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
			}
			return result;
			IL_018a:
			goto IL_00ec;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7c᭻᱿ᵺṮέ\u206aⅡ≳⍿\u242b╼♢❯⡭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0016:
			goto IL_001b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((SurveyCity)_003F350_003F).Loaded += _003F80_003F;
				((SurveyCity)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 5:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 6:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
