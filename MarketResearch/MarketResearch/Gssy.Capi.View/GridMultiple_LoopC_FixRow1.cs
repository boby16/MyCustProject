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
	public class GridMultiple_LoopC_FixRow1 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__37_0;

			public static Comparison<SurveyDetail> _003C_003E9__37_1;

			internal int _003F300_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F301_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QMatrixMultiple oQuestion = new QMatrixMultiple();

		private classMatixButton matixButton = new classMatixButton();

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private List<Button> listButton = new List<Button>();

		private List<Button> listAllButton = new List<Button>();

		private string CodeOfNone = _003F487_003F._003F488_003F(" ļȢ\u033fФԻغܧ࠳ऩਰଫలശฮ");

		private string CodeOfQnNone = _003F487_003F._003F488_003F("'ĵȩ\u0336ЫԲرܮ");

		private string CodeOfGroupNone = _003F487_003F._003F488_003F("'ĳȩ\u0330ЫԲضܮ");

		private bool IsFixNone;

		private string GroupOfFixNone = _003F487_003F._003F488_003F(".");

		private string GroupOfFixNormal = _003F487_003F._003F488_003F(".");

		private string BackgroudColor = _003F487_003F._003F488_003F("*Ļȴ\u0340уՂم\u0744ࡇ");

		private int iNoOfInterval = 9999;

		private HorizontalAlignment CL_Label_HorizontalAlignment = HorizontalAlignment.Right;

		private VerticalAlignment CL_Label_VerticalAlignment = VerticalAlignment.Center;

		private int CL_Width;

		private HorizontalAlignment TR_Label_HorizontalAlignment = HorizontalAlignment.Center;

		private VerticalAlignment TR_Label_VerticalAlignment = VerticalAlignment.Bottom;

		private string TR_Show = _003F487_003F._003F488_003F("");

		private bool is_TR_Show;

		private int PageLoaded;

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

		internal ScrollViewer ScrollHorizontal;

		internal RowDefinition GridTop;

		internal RowDefinition GridBottom;

		internal ColumnDefinition GridLeft;

		internal ColumnDefinition GridRight;

		internal Grid GridTopLeft;

		internal TextBlock GridTopLeftText;

		internal Grid GridTopRight;

		internal ScrollViewer ScrollVertical;

		internal Grid GridBottomLeft;

		internal Grid GridBottomRight;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public GridMultiple_LoopC_FixRow1()
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
				if (_003F7_003F._003C_003E9__37_0 == null)
				{
					_003F7_003F._003C_003E9__37_0 = _003F7_003F._003C_003E9._003F300_003F;
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
				if (_003F7_003F._003C_003E9__37_1 == null)
				{
					_003F7_003F._003C_003E9__37_1 = _003F7_003F._003C_003E9._003F301_003F;
				}
				((List<SurveyDetail>)/*Error near IL_104d: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_104d: Stack underflow*/);
				oQuestion.QDetails = list6;
			}
			if (oQuestion.QDefine.FIX_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array4 = oLogicEngine.aryCode(oQuestion.QDefine.FIX_LOGIC, ',');
				for (int m = 0; m < array4.Count(); m++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array4[m])
							{
								listFix.Add(array4[m]);
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
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array5 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int n = 0; n < array5.Count(); n++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array5[n])
							{
								listPreSet.Add(array5[n]);
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
				for (int num4 = 0; num4 < oQuestion.QDetails.Count(); num4++)
				{
					oQuestion.QDetails[num4].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[num4].CODE_TEXT);
				}
			}
			else if (oQuestion.QDefine.SHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string _003F375_003F2 = list[0];
				string[] array6 = oLogicEngine.aryCode(_003F375_003F2, ',');
				List<SurveyDetail> list7 = new List<SurveyDetail>();
				for (int num5 = 0; num5 < array6.Count(); num5++)
				{
					enumerator2 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail current6 = enumerator2.Current;
							if (current6.CODE == array6[num5].ToString())
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
			double num6 = 0.0;
			if (Button_Width < 2.0)
			{
				num6 = base.ActualWidth - 63.0;
				if (CL_Width > 0)
				{
					num6 -= (double)CL_Width;
				}
				if (Button_Width == 1.0 || oQuestion.QCircleDetails.Count > 7)
				{
					if (CL_Width == 0 && (oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(oQuestion.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))))
					{
						CL_Width = (int)(num6 / 14.0 * 4.0);
						num6 = num6 / 14.0 * 10.0 - 8.0;
					}
				}
				else if (CL_Width == 0 && (oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(oQuestion.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))))
				{
					CL_Width = (int)(num6 / 16.0 * 4.0);
					num6 = num6 / 16.0 * 10.0 - 8.0;
				}
				else
				{
					num6 = num6 / 12.0 * 10.0 - 8.0;
				}
				Button_Width = (num6 - (double)((oQuestion.QCircleDetails.Count - 1) * 4) - 43.0) / (double)oQuestion.QCircleDetails.Count;
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
			PageLoaded = 1;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0133: Incompatible stack heights: 0 vs 1
			//IL_013e: Incompatible stack heights: 0 vs 1
			//IL_0280: Incompatible stack heights: 0 vs 1
			//IL_0281: Incompatible stack heights: 0 vs 1
			//IL_02c4: Incompatible stack heights: 0 vs 1
			//IL_02c5: Incompatible stack heights: 0 vs 1
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0520: Incompatible stack heights: 0 vs 1
			//IL_0536: Incompatible stack heights: 0 vs 1
			//IL_0538: Incompatible stack heights: 0 vs 1
			if (PageLoaded == 2)
			{
				if (CL_Width > 0)
				{
					GridTopLeft.Width = (double)CL_Width;
					GridBottomLeft.Width = (double)CL_Width;
				}
				else
				{
					GridTopLeft.Width = GridBottomLeft.ActualWidth;
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				PageLoaded++;
			}
			if (PageLoaded == 1)
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
					double num = (double)/*Error near IL_013f: Stack underflow*/;
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
				bool flag = (byte)/*Error near IL_0282: Stack underflow*/ != 0;
				if (oQuestion.QCircleDefine.CONTROL_TYPE != 1)
				{
					bool flag3 = oQuestion.QCircleDefine.CONTROL_TYPE == 2;
				}
				if (((/*Error near IL_02c7: Stack underflow*/ | flag) ? 1 : 0) != 0)
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
								((TextBlock)/*Error near IL_053d: Stack underflow*/).Text = (string)/*Error near IL_053d: Stack underflow*/;
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
				}
				GridTopRight.Width = GridBottomRight.ActualWidth;
				string a = oQuestion.QCircleDefine.CONTROL_MASK.Trim().ToUpper();
				if (a != _003F487_003F._003F488_003F("W") && a != _003F487_003F._003F488_003F("I"))
				{
					a = _003F487_003F._003F488_003F("W");
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
				PageLoaded++;
			}
		}

		private void _003F28_003F()
		{
			//IL_01c5: Incompatible stack heights: 0 vs 1
			//IL_01cf: Incompatible stack heights: 0 vs 1
			//IL_026c: Incompatible stack heights: 0 vs 1
			//IL_0276: Incompatible stack heights: 0 vs 1
			//IL_06dd: Incompatible stack heights: 0 vs 1
			//IL_06de: Incompatible stack heights: 0 vs 1
			//IL_084f: Incompatible stack heights: 0 vs 1
			//IL_085f: Incompatible stack heights: 0 vs 1
			//IL_0860: Incompatible stack heights: 0 vs 1
			//IL_0945: Incompatible stack heights: 0 vs 1
			//IL_0956: Incompatible stack heights: 0 vs 1
			//IL_0958: Incompatible stack heights: 0 vs 1
			//IL_0da5: Incompatible stack heights: 0 vs 1
			//IL_0daf: Incompatible stack heights: 0 vs 1
			//IL_0e44: Incompatible stack heights: 0 vs 1
			//IL_0e4e: Incompatible stack heights: 0 vs 1
			//IL_0f1a: Incompatible stack heights: 0 vs 1
			//IL_0f24: Incompatible stack heights: 0 vs 1
			//IL_1169: Incompatible stack heights: 0 vs 1
			//IL_1179: Incompatible stack heights: 0 vs 1
			//IL_117a: Incompatible stack heights: 0 vs 1
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = oLogicEngine;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style3 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			List<SurveyDetail>.Enumerator enumerator;
			if (listFix.Count > 0)
			{
				enumerator = oQuestion.QDetails.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SurveyDetail current = enumerator.Current;
						if (listFix.Contains(current.CODE))
						{
							if (!IsFixNone && CodeOfQnNone.Contains(_003F487_003F._003F488_003F(".") + current.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
							{
								IsFixNone = true;
							}
							if (CodeOfGroupNone.Contains(_003F487_003F._003F488_003F(".") + current.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
							{
								if (!(current.PARENT_CODE == _003F487_003F._003F488_003F("")))
								{
									string pARENT_CODE = current.PARENT_CODE;
								}
								else
								{
									_003F487_003F._003F488_003F(";ĸ");
								}
								string str = (string)/*Error near IL_01d1: Stack underflow*/;
								GroupOfFixNone = GroupOfFixNone + str + _003F487_003F._003F488_003F(".");
							}
							if (!CodeOfNone.Contains(_003F487_003F._003F488_003F(".") + current.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
							{
								if (!(current.PARENT_CODE == _003F487_003F._003F488_003F("")))
								{
									string pARENT_CODE2 = current.PARENT_CODE;
								}
								else
								{
									_003F487_003F._003F488_003F(";ĸ");
								}
								string str2 = (string)/*Error near IL_0278: Stack underflow*/;
								GroupOfFixNormal = GroupOfFixNormal + str2 + _003F487_003F._003F488_003F(".");
							}
						}
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
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
			Border border = new Border();
			Border border2 = new Border();
			int num2 = 0;
			int num3 = 0;
			string b = _003F487_003F._003F488_003F("");
			int num4 = 1;
			enumerator = oQuestion.QCircleDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					string cODE = current2.CODE;
					string cODE_TEXT = current2.CODE_TEXT;
					classMultipleAnswers classMultipleAnswers = new classMultipleAnswers();
					if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
					{
						string _003F285_003F = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + cODE;
						foreach (SurveyAnswer item in oQuestion.ReadAnswerByQuestionName(MySurveyId, _003F285_003F, SurveyHelper.SurveySequence))
						{
							classMultipleAnswers.Answers.Add(item.CODE);
						}
					}
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
							num3 = current2.RANDOM_SET;
						}
						else if (num3 != current2.RANDOM_SET)
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
							num3 = current2.RANDOM_SET;
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
						bool flag5 = oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("");
					}
					bool flag2 = (byte)/*Error near IL_06e0: Stack underflow*/ != 0;
					List<SurveyDetail>.Enumerator enumerator3;
					if ((oQuestion.QCircleDefine.CONTROL_TYPE != 1) | flag2)
					{
						is_TR_Show = true;
						string text2 = _003F487_003F._003F488_003F("");
						if (flag2 && current2.PARENT_CODE != b)
						{
							enumerator3 = oQuestion.QCircleGroupDetails.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									SurveyDetail current4 = enumerator3.Current;
									if (current4.CODE == current2.PARENT_CODE)
									{
										text2 = current4.CODE_TEXT;
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
						}
						if (flag2 && current2.PARENT_CODE == b)
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
							Thickness borderThickness = new Thickness((double)/*Error near IL_0861: Stack underflow*/);
							((Border)/*Error near IL_086b: Stack underflow*/).BorderThickness = borderThickness;
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
							((TextBlock)/*Error near IL_095d: Stack underflow*/).Text = (string)/*Error near IL_095d: Stack underflow*/;
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
						b = current2.PARENT_CODE;
					}
					gridBottomRight.ColumnDefinitions.Add(new ColumnDefinition
					{
						Width = GridLength.Auto
					});
					if (num2 == 0)
					{
						enumerator3 = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail current11 = enumerator3.Current;
								gridBottomRight.RowDefinitions.Add(new RowDefinition
								{
									Height = GridLength.Auto
								});
							}
						}
						finally
						{
							((IDisposable)enumerator3).Dispose();
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
					string[] _003F113_003F = oLogicEngine.aryCode(current2.EXTEND_4, ',');
					List<string> list = new List<string>(listFix);
					bool flag3 = IsFixNone;
					string text3 = GroupOfFixNone;
					string text4 = GroupOfFixNormal;
					if (current2.EXTEND_5 != _003F487_003F._003F488_003F(""))
					{
						string[] array = oLogicEngine.aryCode(current2.EXTEND_5, ',');
						for (int i = 0; i < array.Count(); i++)
						{
							enumerator3 = oQuestion.QDetails.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									if (enumerator3.Current.CODE == array[i])
									{
										list.Add(array[i]);
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
						}
						if (list.Count() > 0)
						{
							enumerator3 = oQuestion.QDetails.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									SurveyDetail current5 = enumerator3.Current;
									if (list.Contains(current5.CODE))
									{
										if (!flag3 && CodeOfQnNone.Contains(_003F487_003F._003F488_003F(".") + current5.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
										{
											flag3 = true;
										}
										if (CodeOfGroupNone.Contains(_003F487_003F._003F488_003F(".") + current5.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
										{
											if (!(current5.PARENT_CODE == _003F487_003F._003F488_003F("")))
											{
												string pARENT_CODE3 = current5.PARENT_CODE;
											}
											else
											{
												_003F487_003F._003F488_003F(";ĸ");
											}
											string str3 = (string)/*Error near IL_0db1: Stack underflow*/;
											text3 = text3 + str3 + _003F487_003F._003F488_003F(".");
										}
										if (!CodeOfNone.Contains(_003F487_003F._003F488_003F(".") + current5.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
										{
											if (!(current5.PARENT_CODE == _003F487_003F._003F488_003F("")))
											{
												string pARENT_CODE4 = current5.PARENT_CODE;
											}
											else
											{
												_003F487_003F._003F488_003F(";ĸ");
											}
											string str4 = (string)/*Error near IL_0e50: Stack underflow*/;
											text4 = text4 + str4 + _003F487_003F._003F488_003F(".");
										}
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
						}
					}
					listButton = new List<Button>();
					classListButton classListButton = new classListButton();
					matixButton.Attributes.Add(classListButton);
					int num5 = 0;
					enumerator3 = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator3.MoveNext())
						{
							SurveyDetail current6 = enumerator3.Current;
							if (!(current6.PARENT_CODE == _003F487_003F._003F488_003F("")))
							{
								string pARENT_CODE5 = current6.PARENT_CODE;
							}
							else
							{
								_003F487_003F._003F488_003F(";ĸ");
							}
							string text5 = (string)/*Error near IL_0f26: Stack underflow*/;
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
							button.Name = _003F487_003F._003F488_003F("`Ş") + num2.ToString() + _003F487_003F._003F488_003F("^") + current6.CODE;
							if (oQuestion.QCircleDefine.CONTROL_TYPE == 0)
							{
								button.Content = current6.CODE_TEXT;
							}
							else if (oQuestion.QCircleDefine.CONTROL_TYPE == 1)
							{
								button.Content = current2.CODE_TEXT;
							}
							else if (oQuestion.QCircleDefine.CONTROL_TYPE == 2)
							{
								button.Content = current6.EXTEND_1;
							}
							button.Margin = new Thickness(0.0);
							if (!classMultipleAnswers.Answers.Contains(current6.CODE))
							{
							}
							((FrameworkElement)/*Error near IL_117f: Stack underflow*/).Style = (Style)/*Error near IL_117f: Stack underflow*/;
							if (flag)
							{
								button.Opacity = 0.85;
							}
							button.Tag = (current6.IS_OTHER + _003F487_003F._003F488_003F("!")).Substring(0, 2) + _003F487_003F._003F488_003F("F") + text5;
							button.FontSize = (double)Button_FontSize;
							button.MinWidth = Button_Width;
							button.MinHeight = (double)Button_Height;
							wrapPanel.Children.Add(button);
							matixButton.Attributes[num2].Buttons.Add(button);
							if (current2.EXTEND_4 != _003F487_003F._003F488_003F("") && oFunc.StringInArray(current6.CODE, _003F113_003F, true) == _003F487_003F._003F488_003F(""))
							{
								wrapPanel.Visibility = Visibility.Collapsed;
							}
							if (wrapPanel.Visibility == Visibility.Visible)
							{
								if (current6.EXTEND_2 != _003F487_003F._003F488_003F(""))
								{
									wrapPanel.Visibility = Visibility.Collapsed;
									if (oBoldTitle.ParaToList(current6.EXTEND_2, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
									{
										wrapPanel.Visibility = Visibility.Visible;
									}
								}
								if (current6.EXTEND_3 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(current6.EXTEND_3, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
								{
									wrapPanel.Visibility = Visibility.Collapsed;
								}
							}
							bool flag4 = false;
							if (wrapPanel.Visibility == Visibility.Visible && list.Count > 0)
							{
								if (list.Contains(current6.CODE))
								{
									flag4 = true;
								}
								else if (flag3 || text3.Contains(_003F487_003F._003F488_003F(".") + text5 + _003F487_003F._003F488_003F(".")))
								{
									wrapPanel.Visibility = Visibility.Collapsed;
								}
								else if (!list.Contains(current6.CODE) && CodeOfQnNone.Contains(_003F487_003F._003F488_003F(".") + current6.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
								{
									wrapPanel.Visibility = Visibility.Collapsed;
								}
								else if (text4.Contains(_003F487_003F._003F488_003F(".") + text5 + _003F487_003F._003F488_003F(".")) && CodeOfGroupNone.Contains(_003F487_003F._003F488_003F(".") + current6.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
								{
									wrapPanel.Visibility = Visibility.Collapsed;
								}
							}
							if (wrapPanel.Visibility == Visibility.Visible)
							{
								if (flag4)
								{
									button.Style = style;
									button.Opacity = 0.5;
									classMultipleAnswers.Answers.Add(current6.CODE);
								}
								else
								{
									button.Click += _003F29_003F;
									listAllButton.Add(button);
									if (!CodeOfNone.Contains(_003F487_003F._003F488_003F(".") + current6.IS_OTHER.ToString() + _003F487_003F._003F488_003F(".")))
									{
										listButton.Add(button);
									}
								}
							}
							num5++;
						}
					}
					finally
					{
						((IDisposable)enumerator3).Dispose();
					}
					oQuestion.SelectedCodes.Add(classMultipleAnswers);
					int num6 = 0;
					List<Button>.Enumerator enumerator4;
					if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))) && SurveyHelper.NavOperation != _003F487_003F._003F488_003F("FŢɡ\u036a"))
					{
						string eXTEND_ = current2.EXTEND_6;
						if (eXTEND_ != _003F487_003F._003F488_003F(""))
						{
							string[] array2 = oLogicEngine.aryCode(eXTEND_, ',');
							for (int j = 0; j < array2.Count(); j++)
							{
								enumerator4 = classListButton.Buttons.GetEnumerator();
								try
								{
									while (enumerator4.MoveNext())
									{
										Button current7 = enumerator4.Current;
										if (current7.Name == _003F487_003F._003F488_003F("`Ş") + num2.ToString() + _003F487_003F._003F488_003F("^") + array2[j])
										{
											num6++;
											_003F29_003F(current7, new RoutedEventArgs());
											break;
										}
									}
								}
								finally
								{
									((IDisposable)enumerator4).Dispose();
								}
							}
						}
						foreach (string item2 in listPreSet)
						{
							enumerator4 = listButton.GetEnumerator();
							try
							{
								while (enumerator4.MoveNext())
								{
									Button current9 = enumerator4.Current;
									if (current9.Name == _003F487_003F._003F488_003F("`Ş") + num2.ToString() + _003F487_003F._003F488_003F("^") + item2)
									{
										num6++;
										_003F29_003F(current9, new RoutedEventArgs());
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator4).Dispose();
							}
						}
					}
					if (num6 == 0 && oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
					{
						_003F29_003F(listButton[0], new RoutedEventArgs());
					}
					if (SurveyHelper.AutoFill)
					{
						listButton = autoFill.MultiButton(oQuestion.QDefine, listButton, listAllButton, 0);
						enumerator4 = listButton.GetEnumerator();
						try
						{
							while (enumerator4.MoveNext())
							{
								Button current10 = enumerator4.Current;
								if (current10.Style == style2)
								{
									_003F29_003F(current10, null);
								}
							}
						}
						finally
						{
							((IDisposable)enumerator4).Dispose();
						}
					}
					num2++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			//IL_012c: Incompatible stack heights: 0 vs 1
			//IL_012d: Incompatible stack heights: 0 vs 1
			//IL_0315: Incompatible stack heights: 0 vs 1
			//IL_0316: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Button button = (Button)_003F347_003F;
			string _003F90_003F = button.Name.Substring(2);
			List<string> list = oFunc.StringToList(_003F90_003F, _003F487_003F._003F488_003F("^"));
			_003F90_003F = list[1];
			int index = oFunc.StringToInt(list[0]);
			string text = (string)button.Tag;
			string a = text.Substring(3);
			text = _003F487_003F._003F488_003F(".") + text.Substring(0, 2).Trim() + _003F487_003F._003F488_003F(".");
			int num = 0;
			if (CodeOfQnNone.Contains(text))
			{
				num = 1;
			}
			else if (CodeOfGroupNone.Contains(text))
			{
				num = 2;
			}
			if (button.Style != style)
			{
			}
			int num2 = (int)/*Error near IL_012f: Stack underflow*/;
			int num3 = 0;
			if (num2 == 0)
			{
				switch (num)
				{
				case 1:
					oQuestion.SelectedCodes[index].Answers.Clear();
					num3 = 1;
					break;
				case 2:
					num3 = 3;
					break;
				default:
					num3 = 2;
					break;
				}
				oQuestion.SelectedCodes[index].Answers.Add(_003F90_003F);
				button.Style = style;
			}
			else if (num == 1)
			{
				num2 = 2;
			}
			else
			{
				oQuestion.SelectedCodes[index].Answers.Remove(_003F90_003F);
				button.Style = style2;
				num2 = 2;
			}
			if (num2 < 2)
			{
				bool flag = true;
				bool flag2 = true;
				foreach (Button button2 in matixButton.Attributes[index].Buttons)
				{
					string _003F90_003F2 = button2.Name.Substring(2);
					_003F90_003F2 = oFunc.StringToList(_003F90_003F2, _003F487_003F._003F488_003F("^"))[1];
					string text2 = (string)button2.Tag;
					string b = text2.Substring(3);
					text2 = _003F487_003F._003F488_003F(".") + text2.Substring(0, 2).Trim() + _003F487_003F._003F488_003F(".");
					if (button2.Opacity != 0.5)
					{
					}
					if ((int)/*Error near IL_0325: Stack underflow*/ == 0 && !(_003F90_003F2 == _003F90_003F))
					{
						switch (num3)
						{
						case 1:
							button2.Style = style2;
							break;
						case 2:
						case 3:
							if (flag && CodeOfQnNone.Contains(text2) && button2.Style == style)
							{
								button2.Style = style2;
								oQuestion.SelectedCodes[index].Answers.Remove(_003F90_003F2);
								flag = false;
							}
							if (flag2 && a == b && CodeOfGroupNone.Contains(text2) && button2.Style == style)
							{
								button2.Style = style2;
								oQuestion.SelectedCodes[index].Answers.Remove(_003F90_003F2);
								flag2 = false;
							}
							break;
						}
						if (num3 == 3 && a == b)
						{
							button2.Style = style2;
							oQuestion.SelectedCodes[index].Answers.Remove(_003F90_003F2);
						}
					}
				}
			}
		}

		private bool _003F87_003F()
		{
			int num = 0;
			foreach (classMultipleAnswers selectedCode in oQuestion.SelectedCodes)
			{
				if (selectedCode.Answers.Count == 0 && GridBottomRight.ColumnDefinitions[num].ActualWidth > 3.0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					matixButton.Attributes[num].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				if (oQuestion.QDefine.MIN_COUNT != 0 && GridBottomRight.ColumnDefinitions[num].ActualWidth > 3.0 && selectedCode.Answers.Count < oQuestion.QDefine.MIN_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgMAless, oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					matixButton.Attributes[num].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				if (oQuestion.QDefine.MAX_COUNT != 0 && selectedCode.Answers.Count > oQuestion.QDefine.MAX_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					matixButton.Attributes[num].Buttons[0].Focus();
					matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				num++;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			int num = 0;
			foreach (classMultipleAnswers selectedCode in oQuestion.SelectedCodes)
			{
				string str = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				VEAnswer vEAnswer = new VEAnswer();
				for (int i = 0; i < selectedCode.Answers.Count; i++)
				{
					vEAnswer = new VEAnswer();
					vEAnswer.QUESTION_NAME = str + _003F487_003F._003F488_003F("]ŀ") + (i + 1).ToString();
					vEAnswer.CODE = selectedCode.Answers[i].ToString();
					SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer.CODE;
				}
				str = oQuestion.CircleQuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = str;
				vEAnswer.CODE = oQuestion.QCircleDetails[num].CODE;
				list.Add(vEAnswer);
				num++;
			}
			SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
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
				string content = ((GridMultiple_LoopC_FixRow1)/*Error near IL_004b: Stack underflow*/).btnNav_Content;
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
			//IL_0038: Incompatible stack heights: 0 vs 1
			//IL_003d: Incompatible stack heights: 1 vs 0
			//IL_0042: Incompatible stack heights: 0 vs 2
			//IL_0048: Incompatible stack heights: 0 vs 1
			//IL_004d: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_002c: Stack underflow*/).Substring((int)/*Error near IL_002c: Stack underflow*/, length);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_0074: Incompatible stack heights: 0 vs 1
			//IL_0079: Incompatible stack heights: 1 vs 0
			//IL_007e: Incompatible stack heights: 0 vs 2
			//IL_0084: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
			int num = _003F365_003F;
			if (num == -9999)
			{
				num = _003F362_003F.Length;
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
			if (num2 + num > _003F362_003F.Length)
			{
				int num3 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, (int)/*Error near IL_0090: Stack underflow*/);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Incompatible stack heights: 0 vs 1
			//IL_0038: Incompatible stack heights: 1 vs 0
			//IL_003d: Incompatible stack heights: 0 vs 1
			//IL_0049: Incompatible stack heights: 0 vs 2
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int startIndex;
			if (num <= _003F362_003F.Length)
			{
				int length = _003F362_003F.Length;
				startIndex = /*Error near IL_001c: Stack underflow*/- /*Error near IL_001c: Stack underflow*/;
			}
			else
			{
				startIndex = 0;
			}
			return ((string)/*Error near IL_0027: Stack underflow*/).Substring(startIndex);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0016ſɄ\u0345ьԚ\u0670ݓࡁख़ਔ\u0b4d\u0c42\u0d41๛ཅ၇ᅍ\u1249ፒᐊᕒᙊᝇᡖᤏ\u1a78\u1b6cᱴ\u1d78ṶὯ⁵Ⅼ≾⍦⑹╱♌❾⡾⥿⩿⭭ⱒ\u2d6a\u2e62⽲ほㅧ㉰㌷㐫㕼㙢㝯㡭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_002d:
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
				((GridMultiple_LoopC_FixRow1)_003F350_003F).Loaded += _003F80_003F;
				((GridMultiple_LoopC_FixRow1)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				ScrollHorizontal = (ScrollViewer)_003F350_003F;
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
				GridTopRight = (Grid)_003F350_003F;
				break;
			case 13:
				ScrollVertical = (ScrollViewer)_003F350_003F;
				break;
			case 14:
				GridBottomLeft = (Grid)_003F350_003F;
				break;
			case 15:
				GridBottomRight = (Grid)_003F350_003F;
				break;
			case 16:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 17:
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
