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
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class GridSingle_LoopC_FixCol1 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__33_0;

			public static Comparison<SurveyDetail> _003C_003E9__33_1;

			internal int _003F302_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F303_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QMatrixSingle oQuestion = new QMatrixSingle();

		private classMatixButton matixButton = new classMatixButton();

		private List<string> listPreSet = new List<string>();

		private List<Button> listButton = new List<Button>();

		private int AutoFillButton = -1;

		private string LastClickCode = _003F487_003F._003F488_003F("");

		private int SameClickCount;

		private bool SameClickCheck;

		private string BackgroudColor = _003F487_003F._003F488_003F("*Ļȴ\u0340уՂم\u0744ࡇ");

		private int iNoOfInterval = 9999;

		private HorizontalAlignment CL_Label_HorizontalAlignment = HorizontalAlignment.Right;

		private VerticalAlignment CL_Label_VerticalAlignment = VerticalAlignment.Center;

		private int CL_Width;

		private HorizontalAlignment TR_Label_HorizontalAlignment = HorizontalAlignment.Center;

		private VerticalAlignment TR_Label_VerticalAlignment = VerticalAlignment.Bottom;

		private string TR_Show = _003F487_003F._003F488_003F("");

		private bool is_TR_Show;

		private bool PageLoaded;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal WrapPanel wrapContent;

		internal ScrollViewer ScrollVertical;

		internal RowDefinition GridTop;

		internal RowDefinition GridBottom;

		internal ColumnDefinition GridLeft;

		internal ColumnDefinition GridRight;

		internal Grid GridTopLeft;

		internal TextBlock GridTopLeftText;

		internal Grid GridBottomLeft;

		internal Border borderRight;

		internal ScrollViewer ScrollHorizontal;

		internal Grid GridTopRight;

		internal Grid GridBottomRight;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public GridSingle_LoopC_FixCol1()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0535: Incompatible stack heights: 0 vs 1
			//IL_053d: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_0585: Incompatible stack heights: 0 vs 1
			//IL_066a: Incompatible stack heights: 0 vs 1
			//IL_066b: Incompatible stack heights: 0 vs 1
			//IL_06fa: Incompatible stack heights: 0 vs 1
			//IL_070a: Incompatible stack heights: 0 vs 1
			//IL_070b: Incompatible stack heights: 0 vs 1
			//IL_0b22: Incompatible stack heights: 0 vs 2
			//IL_0b39: Incompatible stack heights: 0 vs 1
			//IL_1031: Incompatible stack heights: 0 vs 2
			//IL_1048: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0, false);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				MyNav.GroupLevel = _003F487_003F._003F488_003F("@");
				MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				MyNav.GetCircleInfo(MySurveyId);
				oQuestion.QuestionName += MyNav.QName_Add;
				oQuestion.CircleQuestionName += MyNav.QName_Add;
				new List<VEAnswer>().Add(new VEAnswer
				{
					QUESTION_NAME = MyNav.GroupCodeA,
					CODE = MyNav.CircleACode,
					CODE_TEXT = MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = MyNav.CircleACode;
				SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				SurveyHelper.CircleACount = MyNav.CircleACount;
			}
			else
			{
				MyNav.GroupLevel = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				MyNav.GroupCodeA = _003F487_003F._003F488_003F("");
				MyNav.CircleACurrent = 0;
				MyNav.CircleACount = 0;
				MyNav.GroupCodeB = _003F487_003F._003F488_003F("");
				MyNav.CircleBCurrent = 0;
				MyNav.CircleBCount = 0;
			}
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			}
			string sHOW_LOGIC = oQuestion.QDefine.SHOW_LOGIC;
			List<string> list = new List<string>();
			list.Add(_003F487_003F._003F488_003F(""));
			if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				list = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
				if (list.Count > 1)
				{
					oQuestion.QDefine.DETAIL_ID = oLogicEngine.Route(list[1]);
				}
			}
			sHOW_LOGIC = oQuestion.QCircleDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(_003F487_003F._003F488_003F(""));
			if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				list2 = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
				if (list2.Count > 1)
				{
					oQuestion.QCircleDefine.DETAIL_ID = oLogicEngine.Route(list2[1]);
				}
			}
			oQuestion.InitDetailID(CurPageId, 0);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list3[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("C"))
			{
				if (list3.Count <= 1)
				{
					string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
				}
				else
				{
					string text = list3[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_053e: Stack underflow*/;
			}
			else
			{
				if (list3.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text2 = list3[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0586: Stack underflow*/;
			}
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			TR_Show = oBoldTitle.AlignmentPara(oQuestion.QCircleDefine.CONTROL_TOOLTIP.ToUpper().Trim(), ref TR_Label_HorizontalAlignment, ref TR_Label_VerticalAlignment);
			CL_Width = oFunc.StringToInt(oBoldTitle.AlignmentPara(oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim(), ref CL_Label_HorizontalAlignment, ref CL_Label_VerticalAlignment));
			if (oQuestion.QCircleDefine.CONTROL_TYPE == 0)
			{
				bool flag = oQuestion.QDefine.PARENT_CODE == _003F487_003F._003F488_003F("");
			}
			if ((int)/*Error near IL_066b: Stack underflow*/ == 0 && oQuestion.QCircleDefine.CONTROL_TYPE == 1 && oQuestion.QCircleDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))
			{
				bool flag2 = oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("");
			}
			bool _003F458_003F = (byte)/*Error near IL_070d: Stack underflow*/ != 0;
			oBoldTitle.SetTextBlock(GridTopLeftText, oQuestion.QCircleDefine.QUESTION_CONTENT, oQuestion.QCircleDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), _003F458_003F);
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F("") && oQuestion.QCircleDefine.CONTROL_TYPE == 1)
			{
				list3 = new List<string>(oQuestion.QDefine.NOTE.Split(new string[1]
				{
					_003F487_003F._003F488_003F("-Į")
				}, StringSplitOptions.RemoveEmptyEntries));
				int num = oBoldTitle.TakeFontSize(list3[0]);
				list3[0] = oBoldTitle.TakeText(list3[0]);
				Grid gridTopRight = GridTopRight;
				int num2 = 0;
				double num3 = (double)(list3.Count + 1) / 2.0 - 1.0;
				foreach (string item in list3)
				{
					gridTopRight.ColumnDefinitions.Add(new ColumnDefinition
					{
						Width = new GridLength(1.0, GridUnitType.Star)
					});
					TextBlock textBlock = new TextBlock();
					textBlock.SetValue(Grid.RowProperty, 0);
					textBlock.SetValue(Grid.ColumnProperty, num2);
					textBlock.Text = item;
					textBlock.Style = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
					textBlock.Foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(2.0, 0.0, 2.0, 5.0);
					HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
					if ((double)num2 < num3)
					{
						horizontalAlignment = HorizontalAlignment.Left;
					}
					else if ((double)num2 > num3)
					{
						horizontalAlignment = HorizontalAlignment.Right;
					}
					textBlock.HorizontalAlignment = horizontalAlignment;
					textBlock.VerticalAlignment = VerticalAlignment.Bottom;
					if (num > 0)
					{
						textBlock.FontSize = (double)num;
					}
					else if (oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
					{
						textBlock.FontSize = (double)oQuestion.QCircleDefine.CONTROL_FONTSIZE;
					}
					gridTopRight.Children.Add(textBlock);
					num2++;
				}
			}
			List<SurveyDetail>.Enumerator enumerator2;
			if (oQuestion.QCircleDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator2 = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current2 = enumerator2.Current;
							if (current2.CODE == array[i].ToString())
							{
								list4.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__33_0 == null)
				{
					_003F7_003F._003C_003E9__33_0 = _003F7_003F._003C_003E9._003F302_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0b3e: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0b3e: Stack underflow*/);
				oQuestion.QCircleDetails = list4;
			}
			if (oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQuestion.QCircleDetails.Count(); j++)
				{
					oQuestion.QCircleDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (list2[0] != _003F487_003F._003F488_003F(""))
			{
				string _003F375_003F = list2[0];
				string[] array2 = oLogicEngine.aryCode(_003F375_003F, ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count(); k++)
				{
					enumerator2 = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current3 = enumerator2.Current;
							if (current3.CODE == array2[k].ToString())
							{
								list5.Add(current3);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				oQuestion.QCircleDetails = list5;
			}
			else if (oQuestion.QCircleDefine.IS_RANDOM == 1 || oQuestion.QCircleDefine.IS_RANDOM == 3 || (oQuestion.QCircleDefine.IS_RANDOM == 2 && oQuestion.QCircleDefine.PARENT_CODE != _003F487_003F._003F488_003F("")))
			{
				if (oQuestion.QCircleDefine.IS_RANDOM == 2 && oQuestion.QCircleDefine.PARENT_CODE != _003F487_003F._003F488_003F(""))
				{
					enumerator2 = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							enumerator2.Current.RANDOM_FIX = 1;
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				else if (oQuestion.QCircleDefine.IS_RANDOM == 3 && oQuestion.QCircleDefine.PARENT_CODE != _003F487_003F._003F488_003F(""))
				{
					enumerator2 = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current4 = enumerator2.Current;
							if (current4.RANDOM_SET > 0)
							{
								current4.RANDOM_SET = -current4.RANDOM_SET;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				oQuestion.RandomDetails(2);
			}
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list6 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count(); l++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current5 = enumerator2.Current;
							if (current5.CODE == array3[l].ToString())
							{
								list6.Add(current5);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__33_1 == null)
				{
					_003F7_003F._003C_003E9__33_1 = _003F7_003F._003C_003E9._003F303_003F;
				}
				((List<SurveyDetail>)/*Error near IL_104d: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_104d: Stack underflow*/);
				oQuestion.QDetails = list6;
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))))
			{
				string[] array4 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int m = 0; m < array4.Count(); m++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array4[m])
							{
								listPreSet.Add(array4[m]);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int n = 0; n < oQuestion.QDetails.Count(); n++)
				{
					oQuestion.QDetails[n].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[n].CODE_TEXT);
				}
			}
			else if (oQuestion.QDefine.SHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string _003F375_003F2 = list[0];
				string[] array5 = oLogicEngine.aryCode(_003F375_003F2, ',');
				List<SurveyDetail> list7 = new List<SurveyDetail>();
				for (int num4 = 0; num4 < array5.Count(); num4++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current6 = enumerator2.Current;
							if (current6.CODE == array5[num4].ToString())
							{
								list7.Add(current6);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				oQuestion.QDetails = list7;
			}
			else if (oQuestion.QDefine.IS_RANDOM == 1 || oQuestion.QDefine.IS_RANDOM == 3 || (oQuestion.QDefine.IS_RANDOM == 2 && oQuestion.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("")))
			{
				if (oQuestion.QDefine.IS_RANDOM == 2 && oQuestion.QDefine.PARENT_CODE != _003F487_003F._003F488_003F(""))
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							enumerator2.Current.RANDOM_FIX = 1;
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				else if (oQuestion.QDefine.IS_RANDOM == 3 && oQuestion.QDefine.PARENT_CODE != _003F487_003F._003F488_003F(""))
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current7 = enumerator2.Current;
							if (current7.RANDOM_SET > 0)
							{
								current7.RANDOM_SET = -current7.RANDOM_SET;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				oQuestion.RandomDetails(1);
			}
			Button_Height = SurveyHelper.BtnHeight;
			Button_FontSize = SurveyHelper.BtnFontSize;
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				Button_Height = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Button_Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				Button_FontSize = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			double num5 = 0.0;
			if (Button_Width < 2.0)
			{
				num5 = base.ActualWidth - 63.0;
				if (CL_Width > 0)
				{
					num5 -= (double)CL_Width;
				}
				if (Button_Width == 1.0 || oQuestion.QCircleDetails.Count > 7)
				{
					if (CL_Width == 0 && (oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(oQuestion.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))))
					{
						CL_Width = (int)(num5 / 14.0 * 4.0);
						num5 = num5 / 14.0 * 10.0 - 8.0;
					}
				}
				else if (CL_Width == 0 && (oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(oQuestion.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))))
				{
					CL_Width = (int)(num5 / 16.0 * 4.0);
					num5 = num5 / 16.0 * 10.0 - 8.0;
				}
				else
				{
					num5 = num5 / 12.0 * 10.0 - 8.0;
				}
				Button_Width = (num5 - (double)((oQuestion.QCircleDetails.Count - 1) * 4) - 43.0) / (double)oQuestion.QCircleDetails.Count;
				if (Button_Width < 20.0)
				{
					Button_Width = 20.0;
				}
			}
			_003F28_003F();
			if (SurveyHelper.AutoFill && new AutoFill
			{
				oLogicEngine = oLogicEngine
			}.AutoNext(oQuestion.QDefine))
			{
				_003F58_003F(this, _003F348_003F);
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else if (oQuestion.QDetails.Count == 1 && oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2) && !SurveyHelper.AutoFill)
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			SecondsWait = oQuestion.QCircleDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.LightGray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
			SameClickCheck = true;
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_009a: Incompatible stack heights: 0 vs 1
			//IL_00a5: Incompatible stack heights: 0 vs 1
			//IL_01e7: Incompatible stack heights: 0 vs 1
			//IL_01e8: Incompatible stack heights: 0 vs 1
			//IL_022b: Incompatible stack heights: 0 vs 1
			//IL_022c: Incompatible stack heights: 0 vs 1
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0487: Incompatible stack heights: 0 vs 1
			//IL_049d: Incompatible stack heights: 0 vs 1
			//IL_049f: Incompatible stack heights: 0 vs 1
			if (PageLoaded)
			{
				if (GridTopLeft.ActualHeight > 0.0 || GridTopRight.ActualHeight > 0.0)
				{
					if (!(GridTopLeft.ActualHeight > GridTopRight.ActualHeight))
					{
						double actualHeight2 = GridTopRight.ActualHeight;
					}
					else
					{
						double actualHeight3 = GridTopLeft.ActualHeight;
					}
					double num = (double)/*Error near IL_00a6: Stack underflow*/;
					if ((double)oQuestion.QCircleDefine.CONTROL_HEIGHT > num)
					{
						num = (double)oQuestion.QCircleDefine.CONTROL_HEIGHT;
					}
					GridTopLeft.Height = num;
					GridTopRight.Height = num;
				}
				List<SurveyDetail>.Enumerator enumerator;
				if (is_TR_Show)
				{
					Grid gridBottomRight = GridBottomRight;
					Grid gridTopRight = GridTopRight;
					int num2 = 0;
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							gridTopRight.ColumnDefinitions[num2].Width = new GridLength(gridBottomRight.ColumnDefinitions[num2].ActualWidth);
							num2++;
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (oQuestion.QCircleDefine.CONTROL_TYPE == 0)
				{
					bool flag2 = oQuestion.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("");
				}
				bool flag = (byte)/*Error near IL_01e9: Stack underflow*/ != 0;
				if (oQuestion.QCircleDefine.CONTROL_TYPE != 1)
				{
					bool flag3 = oQuestion.QCircleDefine.CONTROL_TYPE == 2;
				}
				if (((/*Error near IL_022e: Stack underflow*/ | flag) ? 1 : 0) != 0)
				{
					Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
					Grid gridBottomRight2 = GridBottomRight;
					Grid gridBottomLeft = GridBottomLeft;
					int num3 = 0;
					Border border = new Border();
					string b = _003F487_003F._003F488_003F("");
					int num4 = 1;
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							string text = _003F487_003F._003F488_003F("");
							if (flag && current.PARENT_CODE != b)
							{
								foreach (SurveyDetail qGroupDetail in oQuestion.QGroupDetails)
								{
									if (qGroupDetail.CODE == current.PARENT_CODE)
									{
										text = qGroupDetail.CODE_TEXT;
										break;
									}
								}
							}
							Button button = matixButton.Attributes[0].Buttons[num3];
							double actualHeight = gridBottomRight2.RowDefinitions[num3].ActualHeight;
							gridBottomLeft.RowDefinitions.Add(new RowDefinition
							{
								Height = new GridLength(actualHeight)
							});
							if (flag && current.PARENT_CODE == b)
							{
								num4++;
								border.SetValue(Grid.RowSpanProperty, num4);
							}
							else
							{
								border = new Border();
								border.BorderThickness = new Thickness(1.0);
								border.BorderBrush = borderBrush;
								border.SetValue(Grid.RowProperty, num3);
								border.SetValue(Grid.ColumnProperty, 0);
								num4 = 1;
								gridBottomLeft.Children.Add(border);
								TextBlock textBlock = (TextBlock)(border.Child = new TextBlock());
								if (!flag)
								{
									string cODE_TEXT = current.CODE_TEXT;
								}
								((TextBlock)/*Error near IL_04a4: Stack underflow*/).Text = (string)/*Error near IL_04a4: Stack underflow*/;
								textBlock.Style = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
								textBlock.Foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
								textBlock.TextWrapping = TextWrapping.Wrap;
								textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
								textBlock.HorizontalAlignment = CL_Label_HorizontalAlignment;
								textBlock.VerticalAlignment = CL_Label_VerticalAlignment;
								if (oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
								{
									textBlock.FontSize = (double)oQuestion.QCircleDefine.CONTROL_FONTSIZE;
								}
							}
							if (flag)
							{
								b = current.PARENT_CODE;
							}
							num3++;
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					if (CL_Width > 0)
					{
						GridLeft.Width = new GridLength((double)CL_Width);
					}
				}
				GridTopRight.Width = GridBottomRight.ActualWidth;
				string a = oQuestion.QCircleDefine.CONTROL_MASK.Trim().ToUpper();
				if (a != _003F487_003F._003F488_003F("W") && a != _003F487_003F._003F488_003F("I"))
				{
					a = _003F487_003F._003F488_003F("I");
				}
				if (a == _003F487_003F._003F488_003F("W"))
				{
					if (ScrollVertical.ComputedVerticalScrollBarVisibility != Visibility.Collapsed)
					{
						ScrollVertical.PanningMode = PanningMode.VerticalOnly;
					}
					else
					{
						ScrollHorizontal.PanningMode = PanningMode.HorizontalOnly;
					}
				}
				else if (a == _003F487_003F._003F488_003F("I"))
				{
					if (ScrollHorizontal.ComputedHorizontalScrollBarVisibility != Visibility.Collapsed)
					{
						ScrollHorizontal.PanningMode = PanningMode.HorizontalOnly;
					}
					else
					{
						ScrollVertical.PanningMode = PanningMode.VerticalOnly;
					}
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				PageLoaded = false;
			}
		}

		private void _003F28_003F()
		{
			//IL_022a: Incompatible stack heights: 0 vs 1
			//IL_023a: Incompatible stack heights: 0 vs 1
			//IL_023b: Incompatible stack heights: 0 vs 1
			//IL_04ee: Incompatible stack heights: 0 vs 1
			//IL_04ef: Incompatible stack heights: 0 vs 1
			//IL_0660: Incompatible stack heights: 0 vs 1
			//IL_0670: Incompatible stack heights: 0 vs 1
			//IL_0671: Incompatible stack heights: 0 vs 1
			//IL_0756: Incompatible stack heights: 0 vs 1
			//IL_0767: Incompatible stack heights: 0 vs 1
			//IL_0769: Incompatible stack heights: 0 vs 1
			//IL_0bed: Incompatible stack heights: 0 vs 1
			//IL_0bfd: Incompatible stack heights: 0 vs 1
			//IL_0bfe: Incompatible stack heights: 0 vs 1
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = oLogicEngine;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style3 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Grid gridBottomRight = GridBottomRight;
			Grid gridBottomLeft = GridBottomLeft;
			Grid gridTopRight = GridTopRight;
			int num = 0;
			bool flag = false;
			string text = _003F93_003F(oQuestion.QDefine.CONTROL_MASK, 1);
			if (text == _003F487_003F._003F488_003F("\""))
			{
				num = 1;
			}
			else if (text.ToUpper() == _003F487_003F._003F488_003F("F"))
			{
				num = 2;
			}
			else if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F("") && oQuestion.QDefine.CONTROL_MASK != null)
			{
				num = 0;
				iNoOfInterval = Convert.ToInt16(oQuestion.QDefine.CONTROL_MASK.ToString());
				if (iNoOfInterval < 1)
				{
					iNoOfInterval = 9999;
				}
			}
			if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("") || oQuestion.QCircleDefine.CONTROL_TYPE != 1)
			{
				Border borderRight2 = borderRight;
				if (!(TR_Show == _003F487_003F._003F488_003F("1")))
				{
				}
				Thickness borderThickness = new Thickness((double)/*Error near IL_023c: Stack underflow*/);
				((Border)/*Error near IL_0246: Stack underflow*/).BorderThickness = borderThickness;
			}
			Border border = new Border();
			Border border2 = new Border();
			int num2 = 0;
			int num3 = 0;
			string b = _003F487_003F._003F488_003F("");
			int num4 = 1;
			foreach (SurveyDetail qCircleDetail in oQuestion.QCircleDetails)
			{
				string cODE = qCircleDetail.CODE;
				string cODE_TEXT = qCircleDetail.CODE_TEXT;
				string text2 = _003F487_003F._003F488_003F("");
				if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string _003F285_003F = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + cODE;
					text2 = oQuestion.ReadAnswerByQuestionName(MySurveyId, _003F285_003F);
				}
				oQuestion.SelectedCode.Add(text2);
				gridTopRight.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = GridLength.Auto
				});
				flag = false;
				if (num == 1)
				{
					if (oQuestion.QDefine.CONTROL_MASK.Contains(_003F487_003F._003F488_003F("\"") + cODE + _003F487_003F._003F488_003F("\"")))
					{
						flag = true;
					}
				}
				else if (num > 1)
				{
					if (num3 == 0)
					{
						num3 = qCircleDetail.RANDOM_SET;
					}
					else if (num3 != qCircleDetail.RANDOM_SET)
					{
						if (num == 2)
						{
							num = 3;
							flag = true;
						}
						else
						{
							num = 2;
						}
						num3 = qCircleDetail.RANDOM_SET;
					}
					else if (num == 3)
					{
						flag = true;
					}
				}
				else if (num2 / iNoOfInterval % 2 > 0)
				{
					flag = true;
				}
				if (oQuestion.QCircleDefine.CONTROL_TYPE == 1 && oQuestion.QCircleDefine.PARENT_CODE != _003F487_003F._003F488_003F(""))
				{
					bool flag3 = oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("");
				}
				bool flag2 = (byte)/*Error near IL_04f1: Stack underflow*/ != 0;
				List<SurveyDetail>.Enumerator enumerator2;
				if ((oQuestion.QCircleDefine.CONTROL_TYPE != 1) | flag2)
				{
					is_TR_Show = true;
					string text3 = _003F487_003F._003F488_003F("");
					if (flag2 && qCircleDetail.PARENT_CODE != b)
					{
						enumerator2 = oQuestion.QCircleGroupDetails.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								SurveyDetail current2 = enumerator2.Current;
								if (current2.CODE == qCircleDetail.PARENT_CODE)
								{
									text3 = current2.CODE_TEXT;
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
					}
					if (flag2 && qCircleDetail.PARENT_CODE == b)
					{
						num4++;
						border2.SetValue(Grid.ColumnSpanProperty, num4);
					}
					else
					{
						border2 = new Border();
						if (!(TR_Show == _003F487_003F._003F488_003F("1")))
						{
						}
						Thickness borderThickness2 = new Thickness((double)/*Error near IL_0672: Stack underflow*/);
						((Border)/*Error near IL_067c: Stack underflow*/).BorderThickness = borderThickness2;
						border2.BorderBrush = borderBrush;
						border2.SetValue(Grid.RowProperty, 0);
						border2.SetValue(Grid.ColumnProperty, num2);
						num4 = 1;
						gridTopRight.Children.Add(border2);
						if (flag2)
						{
							if (num > 1 && flag)
							{
								border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroudColor));
							}
						}
						else if (flag)
						{
							border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroudColor));
						}
						TextBlock textBlock = new TextBlock();
						if (!flag2)
						{
						}
						((TextBlock)/*Error near IL_076e: Stack underflow*/).Text = (string)/*Error near IL_076e: Stack underflow*/;
						textBlock.Style = style3;
						textBlock.Foreground = foreground;
						textBlock.TextWrapping = TextWrapping.Wrap;
						textBlock.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
						textBlock.HorizontalAlignment = TR_Label_HorizontalAlignment;
						textBlock.VerticalAlignment = TR_Label_VerticalAlignment;
						if (oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
						{
							textBlock.FontSize = (double)oQuestion.QCircleDefine.CONTROL_FONTSIZE;
						}
						border2.Child = textBlock;
					}
					b = qCircleDetail.PARENT_CODE;
				}
				gridBottomRight.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = GridLength.Auto
				});
				if (num2 == 0)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current7 = enumerator2.Current;
							gridBottomRight.RowDefinitions.Add(new RowDefinition
							{
								Height = GridLength.Auto
							});
						}
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, 0);
				border.SetValue(Grid.ColumnProperty, num2);
				border.SetValue(Grid.RowSpanProperty, oQuestion.QDetails.Count);
				gridBottomRight.Children.Add(border);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroudColor));
				}
				string[] _003F113_003F = oLogicEngine.aryCode(qCircleDetail.EXTEND_4, ',');
				listButton = new List<Button>();
				matixButton.Attributes.Add(new classListButton());
				int num5 = 0;
				enumerator2 = oQuestion.QDetails.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						SurveyDetail current3 = enumerator2.Current;
						WrapPanel wrapPanel = new WrapPanel();
						wrapPanel.VerticalAlignment = VerticalAlignment.Center;
						wrapPanel.HorizontalAlignment = HorizontalAlignment.Center;
						if (num5 == 0)
						{
							wrapPanel.Margin = new Thickness(5.0, 5.0, 5.0, 2.0);
						}
						else if (num5 == oQuestion.QDetails.Count - 1)
						{
							wrapPanel.Margin = new Thickness(5.0, 2.0, 5.0, 5.0);
						}
						else
						{
							wrapPanel.Margin = new Thickness(5.0, 2.0, 5.0, 2.0);
						}
						wrapPanel.SetValue(Grid.RowProperty, num5);
						wrapPanel.SetValue(Grid.ColumnProperty, num2);
						gridBottomRight.Children.Add(wrapPanel);
						Button button = new Button();
						button.Name = _003F487_003F._003F488_003F("`Ş") + current3.CODE;
						if (oQuestion.QCircleDefine.CONTROL_TYPE == 0)
						{
							button.Content = current3.CODE_TEXT;
						}
						else if (oQuestion.QCircleDefine.CONTROL_TYPE == 1)
						{
							button.Content = qCircleDetail.CODE_TEXT;
						}
						else if (oQuestion.QCircleDefine.CONTROL_TYPE == 2)
						{
							button.Content = current3.EXTEND_1;
						}
						button.Margin = new Thickness(0.0);
						if (!(current3.CODE == text2))
						{
						}
						((FrameworkElement)/*Error near IL_0c03: Stack underflow*/).Style = (Style)/*Error near IL_0c03: Stack underflow*/;
						if (flag)
						{
							button.Opacity = 0.85;
						}
						button.Tag = num2;
						button.Click += _003F29_003F;
						button.FontSize = (double)Button_FontSize;
						button.MinWidth = Button_Width;
						button.MinHeight = (double)Button_Height;
						wrapPanel.Children.Add(button);
						matixButton.Attributes[num2].Buttons.Add(button);
						if (qCircleDetail.EXTEND_4 != _003F487_003F._003F488_003F("") && oFunc.StringInArray(current3.CODE, _003F113_003F, true) == _003F487_003F._003F488_003F(""))
						{
							wrapPanel.Visibility = Visibility.Collapsed;
						}
						if (wrapPanel.Visibility == Visibility.Visible)
						{
							if (current3.EXTEND_2 != _003F487_003F._003F488_003F(""))
							{
								wrapPanel.Visibility = Visibility.Collapsed;
								if (oBoldTitle.ParaToList(current3.EXTEND_2, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
								{
									wrapPanel.Visibility = Visibility.Visible;
								}
							}
							if (current3.EXTEND_3 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(current3.EXTEND_3, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
							{
								wrapPanel.Visibility = Visibility.Collapsed;
							}
						}
						if (wrapPanel.Visibility == Visibility.Visible)
						{
							listButton.Add(button);
						}
						num5++;
					}
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				int num6 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))) && SurveyHelper.NavOperation != _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string eXTEND_ = qCircleDetail.EXTEND_6;
					List<Button>.Enumerator enumerator3;
					if (eXTEND_ != _003F487_003F._003F488_003F(""))
					{
						string[] array = oLogicEngine.aryCode(eXTEND_, ',');
						for (int i = 0; i < array.Count(); i++)
						{
							enumerator3 = listButton.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									Button current4 = enumerator3.Current;
									if (current4.Name == _003F487_003F._003F488_003F("`Ş") + array[i])
									{
										num6 = 1;
										_003F29_003F(current4, new RoutedEventArgs());
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
							if (num6 > 0)
							{
								break;
							}
						}
					}
					if (num6 == 0)
					{
						foreach (string item in listPreSet)
						{
							enumerator3 = listButton.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									Button current6 = enumerator3.Current;
									if (current6.Name == _003F487_003F._003F488_003F("`Ş") + item)
									{
										num6 = 1;
										_003F29_003F(current6, new RoutedEventArgs());
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
							if (num6 > 0)
							{
								break;
							}
						}
					}
				}
				if (num6 == 0 && oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
				{
					_003F29_003F(listButton[0], new RoutedEventArgs());
				}
				if (SurveyHelper.AutoFill)
				{
					Button button2;
					if (oQuestion.QDefine.CONTROL_TYPE == 0)
					{
						button2 = autoFill.SingleButton(oQuestion.QDefine, listButton);
					}
					else
					{
						if (AutoFillButton == -1)
						{
							AutoFillButton = Convert.ToInt32(oFunc.INT((double)(listButton.Count() / 2), 0, 0, 0));
						}
						button2 = listButton[AutoFillButton];
					}
					if (button2 != null && num6 == 0)
					{
						_003F29_003F(button2, new RoutedEventArgs());
					}
				}
				num2++;
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00e1: Incompatible stack heights: 0 vs 1
			//IL_00f1: Incompatible stack heights: 0 vs 1
			//IL_00f2: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Button obj = (Button)_003F347_003F;
			int index = (int)obj.Tag;
			string text = obj.Name.Substring(2);
			int num = 0;
			if (obj.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				classListButton classListButton = matixButton.Attributes[index];
				oQuestion.SelectedCode[index] = text;
				foreach (Button button in classListButton.Buttons)
				{
					string a = button.Name.Substring(2);
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_00f7: Stack underflow*/).Style = (Style)/*Error near IL_00f7: Stack underflow*/;
				}
				if (oQuestion.QDefine.MAX_COUNT > 0 && SameClickCheck && !SurveyHelper.AutoFill)
				{
					if (text == LastClickCode)
					{
						SameClickCount++;
						if (SameClickCount >= oQuestion.QDefine.MAX_COUNT && MessageBox.Show(string.Format(SurveyMsg.MsgMXSA_info3, SameClickCount.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes).Equals(MessageBoxResult.No))
						{
							SameClickCheck = false;
						}
					}
					else
					{
						LastClickCode = text;
						SameClickCount = 1;
					}
				}
			}
		}

		private bool _003F87_003F()
		{
			int num = 0;
			List<string>.Enumerator enumerator = oQuestion.SelectedCode.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == _003F487_003F._003F488_003F("") && GridBottomRight.ColumnDefinitions[num].ActualWidth > 3.0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						matixButton.Attributes[num].Buttons[0].Focus();
						matixButton.Attributes[num].Buttons[0].BringIntoView();
						return true;
					}
					num++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			if (oQuestion.QDefine.MIN_COUNT > 0 && !SurveyHelper.AutoFill)
			{
				int num2 = oQuestion.QDefine.MIN_COUNT;
				if (num2 > oQuestion.QCircleDetails.Count && oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num2 = oQuestion.QCircleDetails.Count;
				}
				string a = _003F487_003F._003F488_003F("");
				int num3 = 0;
				num = 0;
				enumerator = oQuestion.SelectedCode.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						if (a == current)
						{
							num3++;
						}
						else
						{
							if (num3 >= num2)
							{
								num--;
								matixButton.Attributes[num].Buttons[0].Focus();
								matixButton.Attributes[num].Buttons[0].BringIntoView();
								string text = string.Format(SurveyMsg.MsgMXSA_info1, oQuestion.QCircleDetails[num].CODE_TEXT, num3);
								if (oQuestion.QCircleDefine.MIN_COUNT > 0)
								{
									MessageBox.Show(text, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									return true;
								}
								text = text + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
								if (MessageBox.Show(text, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
								{
									return true;
								}
							}
							num3 = 1;
							a = current;
						}
						num++;
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				if (num3 >= num2)
				{
					num--;
					matixButton.Attributes[num].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					string text2 = string.Format(SurveyMsg.MsgMXSA_info1, oQuestion.QCircleDetails[num].CODE_TEXT, num3);
					if (oQuestion.QCircleDefine.MIN_COUNT > 0)
					{
						MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					text2 = text2 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
					if (MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			else if (oQuestion.QDefine.MIN_COUNT < 0)
			{
				int num4 = -oQuestion.QDefine.MIN_COUNT;
				if (num4 > oQuestion.QCircleDetails.Count && oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num4 = oQuestion.QCircleDetails.Count;
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				num = 0;
				enumerator = oQuestion.SelectedCode.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current2 = enumerator.Current;
						if (dictionary.ContainsKey(current2))
						{
							Dictionary<string, int> dictionary2 = dictionary;
							string key = current2;
							dictionary2[key]++;
						}
						else
						{
							dictionary.Add(current2, 1);
						}
						num++;
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				foreach (string key2 in dictionary.Keys)
				{
					if (dictionary[key2] >= num4)
					{
						string arg = _003F487_003F._003F488_003F("");
						foreach (SurveyDetail qDetail in oQuestion.QDetails)
						{
							if (qDetail.CODE == key2)
							{
								arg = qDetail.CODE_TEXT;
								break;
							}
						}
						string text3 = string.Format(SurveyMsg.MsgMXSA_info2, arg, dictionary[key2]);
						if (oQuestion.QCircleDefine.MIN_COUNT > 0)
						{
							MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return true;
						}
						text3 = text3 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
						if (MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return true;
						}
					}
				}
			}
			if (oQuestion.QDefine.CONTROL_TYPE != 0)
			{
				string b = Math.Abs(oQuestion.QDefine.CONTROL_TYPE).ToString();
				int num5 = 0;
				int num6 = 9999999;
				int num7 = 0;
				int index = 0;
				num = 0;
				enumerator = oQuestion.SelectedCode.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current5 = enumerator.Current;
						int num8 = oFunc.StringToInt(current5);
						if (oQuestion.QCircleDetails[num].CODE == b)
						{
							num5 = num8;
							index = num;
						}
						else
						{
							if (num7 < num8)
							{
								num7 = num8;
							}
							if (num6 > num8)
							{
								num6 = num8;
							}
						}
						num++;
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				if (num5 < num6 && num5 > 0)
				{
					matixButton.Attributes[index].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					if (oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, oQuestion.QCircleDetails[index].CODE_TEXT, num5.ToString(), num6.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, oQuestion.QCircleDetails[index].CODE_TEXT, num5.ToString(), num6.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
				else if (num5 > num7)
				{
					matixButton.Attributes[index].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					if (oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, oQuestion.QCircleDetails[index].CODE_TEXT, num5.ToString(), num7.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, oQuestion.QCircleDetails[index].CODE_TEXT, num5.ToString(), num7.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			int num = 0;
			foreach (string item in oQuestion.SelectedCode)
			{
				string text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = item;
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + text + _003F487_003F._003F488_003F("<") + item;
				text = oQuestion.CircleQuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = oQuestion.QCircleDetails[num].CODE;
				list.Add(vEAnswer);
				num++;
			}
			SurveyHelper.Answer = _003F94_003F(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_005e: Incompatible stack heights: 0 vs 2
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				PageNav oPageNav2 = oPageNav;
				SurveyDefine qDefine = oQuestion.QDefine;
				int pAGE_COUNT_DOWN = ((SurveyDefine)/*Error near IL_003c: Stack underflow*/).PAGE_COUNT_DOWN;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_006e: Stack underflow*/).PageDataLog(pAGE_COUNT_DOWN, _003F370_003F, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c9: Incompatible stack heights: 0 vs 2
			//IL_00d9: Incompatible stack heights: 0 vs 1
			//IL_00ee: Incompatible stack heights: 0 vs 2
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			oPageNav.Refresh();
			if (_003F87_003F())
			{
				Button btnNav2 = btnNav;
				string content = ((GridSingle_LoopC_FixCol1)/*Error near IL_004b: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0050: Stack underflow*/).Content = content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav3 = btnNav;
					string content2 = btnNav_Content;
					((ContentControl)/*Error near IL_0096: Stack underflow*/).Content = content2;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00f6: Stack underflow*/, (string)/*Error near IL_00f6: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00b3:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0036: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				Button btnNav2 = btnNav;
				SolidColorBrush black = Brushes.Black;
				((System.Windows.Controls.Control)/*Error near IL_0015: Stack underflow*/).Foreground = black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_00a0: Incompatible stack heights: 0 vs 1
			//IL_00a5: Incompatible stack heights: 1 vs 0
			//IL_00b0: Incompatible stack heights: 0 vs 1
			//IL_00b5: Incompatible stack heights: 1 vs 0
			//IL_00c0: Incompatible stack heights: 0 vs 1
			//IL_00c5: Incompatible stack heights: 1 vs 0
			//IL_00d0: Incompatible stack heights: 0 vs 1
			//IL_00d5: Incompatible stack heights: 1 vs 0
			//IL_00e0: Incompatible stack heights: 0 vs 1
			//IL_00e5: Incompatible stack heights: 1 vs 0
			int num = _003F364_003F;
			if (num == -9999)
			{
				num = _003F363_003F;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (_003F363_003F < 0)
			{
			}
			int num2 = 0;
			int num3;
			if (num2 < num)
			{
				num3 = num2;
			}
			int num4 = num3;
			int num5;
			if (num2 < num)
			{
				num5 = num;
			}
			num = num5;
			int length;
			if (num2 > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num4 = length;
			int num6;
			if (_003F364_003F > _003F362_003F.Length)
			{
				num6 = _003F362_003F.Length - 1;
			}
			num = num6;
			return _003F362_003F.Substring(num4, num - num4 + 1);
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 2
			//IL_0042: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				int length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, (int)/*Error near IL_0051: Stack underflow*/);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_0060: Incompatible stack heights: 0 vs 1
			//IL_0077: Incompatible stack heights: 0 vs 1
			//IL_007c: Incompatible stack heights: 1 vs 0
			//IL_0081: Incompatible stack heights: 0 vs 2
			//IL_0087: Incompatible stack heights: 0 vs 1
			//IL_008c: Incompatible stack heights: 1 vs 0
			int num = _003F365_003F;
			if (num == -9999)
			{
				num = ((string)/*Error near IL_0012: Stack underflow*/).Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int length;
			if (_003F363_003F > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			int num2 = length;
			int length2;
			if (num2 + num > _003F362_003F.Length)
			{
				length2 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0054: Stack underflow*/).Substring((int)/*Error near IL_0054: Stack underflow*/, length2);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0049: Incompatible stack heights: 0 vs 1
			//IL_004e: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num <= _003F362_003F.Length)
			{
				int num2 = _003F362_003F.Length - num;
			}
			return ((string)/*Error near IL_0026: Stack underflow*/).Substring(0);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0018űɆ\u0347ъԜٲݑ\u085f\u0947ਖ\u0b4f\u0c44\u0d47๙ཇ၉ᅃቋፐᐌᕔᙈᝅᡨ\u1931\u1a7a\u1b6eᱲᵾṪά⁹ⅱ≹⍱\u244c╾♾❿⡿⥭⩒⭪Ɫ\u2d72\u2e6a⽧にㄷ㈫㍼㑢㕯㙭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_001d:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((GridSingle_LoopC_FixCol1)_003F350_003F).Loaded += _003F80_003F;
				((GridSingle_LoopC_FixCol1)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				wrapContent = (WrapPanel)_003F350_003F;
				break;
			case 5:
				ScrollVertical = (ScrollViewer)_003F350_003F;
				break;
			case 6:
				GridTop = (RowDefinition)_003F350_003F;
				break;
			case 7:
				GridBottom = (RowDefinition)_003F350_003F;
				break;
			case 8:
				GridLeft = (ColumnDefinition)_003F350_003F;
				break;
			case 9:
				GridRight = (ColumnDefinition)_003F350_003F;
				break;
			case 10:
				GridTopLeft = (Grid)_003F350_003F;
				break;
			case 11:
				GridTopLeftText = (TextBlock)_003F350_003F;
				break;
			case 12:
				GridBottomLeft = (Grid)_003F350_003F;
				break;
			case 13:
				borderRight = (Border)_003F350_003F;
				break;
			case 14:
				ScrollHorizontal = (ScrollViewer)_003F350_003F;
				break;
			case 15:
				GridTopRight = (Grid)_003F350_003F;
				break;
			case 16:
				GridBottomRight = (Grid)_003F350_003F;
				break;
			case 17:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 18:
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
