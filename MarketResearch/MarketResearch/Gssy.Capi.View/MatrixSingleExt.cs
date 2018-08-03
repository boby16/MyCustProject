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

namespace Gssy.Capi.View
{
	public class MatrixSingleExt : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__27_0;

			public static Comparison<SurveyDetail> _003C_003E9__27_1;

			internal int _003F308_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F309_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMatrixSingle oQuestion = new QMatrixSingle();

		private List<Button> listButton = new List<Button>();

		private int AutoFillButton = -1;

		private List<WrapPanel> wrapSingle = new List<WrapPanel>();

		private List<string> listOther = new List<string>();

		private string LastClickCode = _003F487_003F._003F488_003F("");

		private int SameClickCount;

		private bool SameClickCheck = true;

		private string BackgroudColor = _003F487_003F._003F488_003F("*Ļȴ\u0340уՂم\u0744ࡇ");

		private int iNoOfInterval = 9999;

		private string CL_TA = _003F487_003F._003F488_003F("S");

		private string CL_VA = _003F487_003F._003F488_003F("B");

		private int CL_Width;

		private double CR_Width;

		private bool PageLoaded;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal WrapPanel wrapContent;

		internal Grid GridTitle;

		internal ColumnDefinition TitleLeft;

		internal ColumnDefinition TitleRight;

		internal TextBlock OptionTitleHeader;

		internal ColumnDefinition ExtColumn;

		internal TextBlock txt1;

		internal TextBlock txt3;

		internal TextBlock txt4;

		internal TextBlock txt5;

		internal TextBlock txt6;

		internal TextBlock txt7;

		internal TextBlock txt9;

		internal ScrollViewer ScrollContent;

		internal Grid GridContent;

		internal ColumnDefinition GridLeft;

		internal ColumnDefinition GridRight;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public MatrixSingleExt()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_03f6: Incompatible stack heights: 0 vs 1
			//IL_03fd: Incompatible stack heights: 0 vs 1
			//IL_043c: Incompatible stack heights: 0 vs 1
			//IL_0443: Incompatible stack heights: 0 vs 1
			//IL_0972: Incompatible stack heights: 0 vs 2
			//IL_0989: Incompatible stack heights: 0 vs 1
			//IL_0b8f: Incompatible stack heights: 0 vs 2
			//IL_0ba6: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0, true);
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
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("C"))
			{
				if (list.Count <= 1)
				{
					string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
				}
				else
				{
					string text2 = list[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_03fe: Stack underflow*/;
			}
			else
			{
				if (list.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text3 = list[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0444: Stack underflow*/;
			}
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			string text = oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			if (text != _003F487_003F._003F488_003F(""))
			{
				CL_TA = _003F93_003F(text, 1);
				if (_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(CL_TA))
				{
					CL_TA = _003F487_003F._003F488_003F("S");
					if (text != _003F487_003F._003F488_003F(""))
					{
						CL_Width = Convert.ToInt32(text);
					}
				}
				else
				{
					text = _003F94_003F(text, 1, -9999);
					CL_VA = _003F93_003F(text, 1);
					if (_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(CL_VA))
					{
						if (CL_TA != _003F487_003F._003F488_003F("U") && CL_TA != _003F487_003F._003F488_003F("C"))
						{
							CL_VA = _003F487_003F._003F488_003F("B");
						}
						if (text != _003F487_003F._003F488_003F(""))
						{
							CL_Width = Convert.ToInt32(text);
						}
					}
					else if (_003F94_003F(text, 1, -9999) != _003F487_003F._003F488_003F(""))
					{
						CL_Width = Convert.ToInt32(_003F94_003F(text, 1, -9999));
					}
				}
				text = CL_TA + CL_VA;
				if (text.Contains(_003F487_003F._003F488_003F("M")))
				{
					CL_TA = _003F487_003F._003F488_003F("M");
				}
				else if (text.Contains(_003F487_003F._003F488_003F("S")))
				{
					CL_TA = _003F487_003F._003F488_003F("S");
				}
				if (text.Contains(_003F487_003F._003F488_003F("U")))
				{
					CL_VA = _003F487_003F._003F488_003F("U");
				}
				else if (text.Contains(_003F487_003F._003F488_003F("C")))
				{
					CL_VA = _003F487_003F._003F488_003F("C");
				}
				if (text.Contains(_003F487_003F._003F488_003F("B")) && (text.Contains(_003F487_003F._003F488_003F("U")) || text.Contains(_003F487_003F._003F488_003F("C"))))
				{
					CL_TA = _003F487_003F._003F488_003F("B");
				}
				if (text.Contains(_003F487_003F._003F488_003F("B")) && (text.Contains(_003F487_003F._003F488_003F("S")) || text.Contains(_003F487_003F._003F488_003F("M"))))
				{
					CL_VA = _003F487_003F._003F488_003F("B");
				}
				if (text == _003F487_003F._003F488_003F("B") || text == _003F487_003F._003F488_003F("Ał"))
				{
					CL_TA = _003F487_003F._003F488_003F("B");
					CL_VA = _003F487_003F._003F488_003F("B");
				}
			}
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == array[i].ToString())
							{
								list2.Add(current);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__27_0 == null)
				{
					_003F7_003F._003C_003E9__27_0 = _003F7_003F._003C_003E9._003F308_003F;
				}
				((List<SurveyDetail>)/*Error near IL_098e: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_098e: Stack underflow*/);
				oQuestion.QDetails = list2;
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQuestion.QDetails.Count(); j++)
				{
					oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails(1);
			}
			if (oQuestion.QCircleDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count(); k++)
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array2[k].ToString())
							{
								list3.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__27_1 == null)
				{
					_003F7_003F._003C_003E9__27_1 = _003F7_003F._003C_003E9._003F309_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0bab: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0bab: Stack underflow*/);
				oQuestion.QCircleDetails = list3;
			}
			if (oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int l = 0; l < oQuestion.QCircleDetails.Count(); l++)
				{
					oQuestion.QCircleDetails[l].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QCircleDetails[l].CODE_TEXT);
				}
			}
			if (oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails(2);
			}
			else if (oQuestion.QCircleDefine.SHOW_LOGIC != _003F487_003F._003F488_003F("") && oQuestion.QCircleDefine.IS_RANDOM == 0)
			{
				string sHOW_LOGIC = oQuestion.QCircleDefine.SHOW_LOGIC;
				string[] array3 = oLogicEngine.aryCode(sHOW_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int m = 0; m < array3.Count(); m++)
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == array3[m].ToString())
							{
								list4.Add(current3);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				oQuestion.QCircleDetails = list4;
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
			if (Button_Width < 2.0)
			{
				CR_Width = base.ActualWidth - 63.0;
				if (CL_Width > 0)
				{
					CR_Width -= (double)CL_Width;
				}
				if (Button_Width == 1.0 || oQuestion.QDetails.Count > 7)
				{
					if (CL_Width == 0)
					{
						CL_Width = (int)(CR_Width / 14.0 * 4.0);
					}
					CR_Width = CR_Width / 14.0 * 10.0 - 8.0;
				}
				else
				{
					if (CL_Width == 0)
					{
						CL_Width = (int)(CR_Width / 16.0 * 4.0);
					}
					CR_Width = CR_Width / 16.0 * 10.0 - 8.0;
				}
				Button_Width = (CR_Width - (double)((oQuestion.QDetails.Count - 1) * 4) - 43.0) / (double)oQuestion.QDetails.Count;
				if (Button_Width < 20.0)
				{
					Button_Width = 20.0;
				}
			}
			_003F28_003F();
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current4 = enumerator.Current;
					if (current4.IS_OTHER > 0)
					{
						listOther.Add(current4.CODE);
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			oBoldTitle.SetTextBlock(OptionTitleHeader, oQuestion.QCircleDefine.QUESTION_CONTENT, 0, _003F487_003F._003F488_003F(""), true);
			int num = listOther.Count();
			int num2 = oQuestion.QDetails.Count() - num;
			double value = 10.0 * (double)num / (double)num2;
			ExtColumn.Width = new GridLength(value, GridUnitType.Star);
			txt1.Text = _003F487_003F._003F488_003F("");
			txt3.Text = _003F487_003F._003F488_003F("");
			txt4.Text = _003F487_003F._003F488_003F("");
			txt5.Text = _003F487_003F._003F488_003F("");
			txt6.Text = _003F487_003F._003F488_003F("");
			txt7.Text = _003F487_003F._003F488_003F("");
			txt9.Text = _003F487_003F._003F488_003F("");
			string nOTE = oQuestion.QDefine.NOTE;
			if (nOTE == _003F487_003F._003F488_003F(""))
			{
				txt1.Height = 0.0;
				txt3.Height = 0.0;
				txt4.Height = 0.0;
				txt5.Height = 0.0;
				txt6.Height = 0.0;
				txt7.Height = 0.0;
				txt9.Height = 0.0;
			}
			else
			{
				list = new List<string>(nOTE.Split(new string[1]
				{
					_003F487_003F._003F488_003F("-Į")
				}, StringSplitOptions.RemoveEmptyEntries));
				int num3 = oBoldTitle.TakeFontSize(list[0]);
				list[0] = oBoldTitle.TakeText(list[0]);
				if (list.Count == 1)
				{
					txt5.Text = list[0];
				}
				else if (list.Count == 2)
				{
					txt1.Text = list[0];
					txt9.Text = list[1];
				}
				else if (list.Count == 3)
				{
					txt1.Text = list[0];
					txt5.Text = list[1];
					txt9.Text = list[2];
				}
				else if (list.Count == 4)
				{
					txt1.Text = list[0];
					txt4.Text = list[1];
					txt6.Text = list[2];
					txt9.Text = list[3];
				}
				else if (list.Count == 5)
				{
					txt1.Text = list[0];
					txt3.Text = list[1];
					txt5.Text = list[2];
					txt7.Text = list[3];
					txt9.Text = list[4];
				}
				if (num3 != 0)
				{
					txt1.FontSize = (double)num3;
					txt3.FontSize = (double)num3;
					txt4.FontSize = (double)num3;
					txt5.FontSize = (double)num3;
					txt6.FontSize = (double)num3;
					txt7.FontSize = (double)num3;
					txt9.FontSize = (double)num3;
				}
				else if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					txt1.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt3.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt4.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt5.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt6.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt7.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
					txt9.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				}
			}
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
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0063: Incompatible stack heights: 0 vs 2
			//IL_0074: Incompatible stack heights: 0 vs 2
			//IL_0080: Incompatible stack heights: 0 vs 1
			//IL_0086: Incompatible stack heights: 0 vs 1
			if (!PageLoaded)
			{
				return;
			}
			int cL_Width = CL_Width;
			if (/*Error near IL_0068: Stack underflow*/ > /*Error near IL_0068: Stack underflow*/)
			{
				ColumnDefinition titleLeft = TitleLeft;
				GridLength width = new GridLength((double)((MatrixSingleExt)/*Error near IL_0015: Stack underflow*/).CL_Width);
				((ColumnDefinition)/*Error near IL_0020: Stack underflow*/).Width = width;
				GridLeft.Width = new GridLength((double)CL_Width);
			}
			if (ScrollContent.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
			{
				goto IL_0085;
			}
			goto IL_0086;
			IL_004d:
			goto IL_0085;
			IL_0086:
			int num = (int)/*Error near IL_0087: Stack underflow*/;
			GridTitle.Margin = new Thickness(0.0, 0.0, (double)num, 0.0);
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			PageLoaded = false;
			return;
			IL_0085:
			goto IL_0086;
		}

		private void _003F28_003F()
		{
			//IL_0785: Incompatible stack heights: 0 vs 1
			//IL_0795: Incompatible stack heights: 0 vs 1
			//IL_0796: Incompatible stack heights: 0 vs 1
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = oLogicEngine;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style3 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (CL_TA == _003F487_003F._003F488_003F("B"))
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (CL_TA == _003F487_003F._003F488_003F("M"))
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (CL_VA == _003F487_003F._003F488_003F("U"))
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (CL_VA == _003F487_003F._003F488_003F("C"))
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			Grid gridContent = GridContent;
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
			int num2 = 0;
			int num3 = 0;
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
				gridContent.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto
				});
				Border border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
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
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroudColor));
				}
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 0);
				gridContent.Children.Add(border);
				WrapPanel wrapPanel = new WrapPanel();
				wrapPanel.VerticalAlignment = verticalAlignment;
				wrapPanel.HorizontalAlignment = horizontalAlignment;
				border.Child = wrapPanel;
				TextBlock textBlock = new TextBlock();
				textBlock.Text = cODE_TEXT;
				textBlock.Style = style3;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
				textBlock.VerticalAlignment = verticalAlignment;
				if (oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
				{
					textBlock.FontSize = (double)oQuestion.QCircleDefine.CONTROL_FONTSIZE;
				}
				wrapPanel.Children.Add(textBlock);
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 1);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroudColor));
				}
				gridContent.Children.Add(border);
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapSingle.Add(wrapPanel2);
				wrapPanel2.Orientation = Orientation.Horizontal;
				wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
				wrapPanel2.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
				wrapPanel2.Name = _003F487_003F._003F488_003F("uœ") + cODE;
				wrapPanel2.Tag = cODE;
				border.Child = wrapPanel2;
				oQuestion.SelectedCode.Add(text2);
				listButton = new List<Button>();
				foreach (SurveyDetail qDetail in oQuestion.QDetails)
				{
					Button button = new Button();
					button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
					button.Content = qDetail.CODE_TEXT;
					button.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
					if (!(qDetail.CODE == text2))
					{
					}
					((FrameworkElement)/*Error near IL_079b: Stack underflow*/).Style = (Style)/*Error near IL_079b: Stack underflow*/;
					if (flag)
					{
						button.Opacity = 0.85;
					}
					button.Tag = num2;
					button.Click += _003F29_003F;
					button.FontSize = (double)Button_FontSize;
					button.MinWidth = Button_Width;
					button.MinHeight = (double)Button_Height;
					wrapPanel2.Children.Add(button);
					if (qDetail.IS_OTHER == 2)
					{
						button.Visibility = Visibility.Hidden;
						if (qDetail.EXTEND_2 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(qDetail.EXTEND_2, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
						{
							button.Visibility = Visibility.Visible;
						}
					}
					else if (qDetail.IS_OTHER == 3)
					{
						if (qDetail.EXTEND_3 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(qDetail.EXTEND_3, _003F487_003F._003F488_003F("-Į")).Contains(cODE))
						{
							button.Visibility = Visibility.Hidden;
						}
					}
					else
					{
						listButton.Add(button);
					}
				}
				int num4 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))) && SurveyHelper.NavOperation != _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string eXTEND_ = qCircleDetail.EXTEND_4;
					if (eXTEND_ != _003F487_003F._003F488_003F(""))
					{
						string[] array = oLogicEngine.aryCode(eXTEND_, ',');
						for (int i = 0; i < array.Count(); i++)
						{
							foreach (Button item in listButton)
							{
								if (item.Name == _003F487_003F._003F488_003F("`Ş") + array[i])
								{
									num4 = 1;
									_003F29_003F(item, new RoutedEventArgs());
									break;
								}
							}
						}
					}
				}
				if (num4 == 0 && oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
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
					if (button2 != null && num4 == 0)
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
				WrapPanel wrapPanel = wrapSingle[index];
				oQuestion.SelectedCode[index] = text;
				foreach (Button child in wrapPanel.Children)
				{
					string a = child.Name.Substring(2);
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
					if (enumerator.Current == _003F487_003F._003F488_003F(""))
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						wrapSingle[num].Focus();
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
				string text = _003F487_003F._003F488_003F("");
				int num3 = 0;
				num = 0;
				enumerator = oQuestion.SelectedCode.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						if (text == current)
						{
							num3++;
						}
						else
						{
							if (num3 >= num2 && !listOther.Contains(text))
							{
								num--;
								wrapSingle[num].Focus();
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
							num3 = 1;
							text = current;
						}
						num++;
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				if (num3 >= num2 && !listOther.Contains(text))
				{
					num--;
					wrapSingle[num].Focus();
					string text3 = string.Format(SurveyMsg.MsgMXSA_info1, oQuestion.QCircleDetails[num].CODE_TEXT, num3);
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
					if (dictionary[key2] >= num4 && !listOther.Contains(key2))
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
						string text4 = string.Format(SurveyMsg.MsgMXSA_info2, arg, dictionary[key2]);
						if (oQuestion.QCircleDefine.MIN_COUNT > 0)
						{
							MessageBox.Show(text4, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return true;
						}
						text4 = text4 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
						if (MessageBox.Show(text4, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
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
						if (!listOther.Contains(current5))
						{
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
					wrapSingle[index].Focus();
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
					wrapSingle[index].Focus();
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
			//IL_0052: Incompatible stack heights: 0 vs 2
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				PageNav oPageNav2 = oPageNav;
				QMatrixSingle oQuestion2 = oQuestion;
				int pAGE_COUNT_DOWN = ((QMatrixSingle)/*Error near IL_0057: Stack underflow*/).QDefine.PAGE_COUNT_DOWN;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_006e: Stack underflow*/).PageDataLog(pAGE_COUNT_DOWN, _003F370_003F, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00bd: Incompatible stack heights: 0 vs 1
			//IL_00f1: Incompatible stack heights: 0 vs 4
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
				string content = btnNav_Content;
				((ContentControl)/*Error near IL_0051: Stack underflow*/).Content = content;
				return;
			}
			List<VEAnswer> list = _003F88_003F();
			oLogicEngine.PageAnswer = list;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_008c;
			IL_008c:
			_003F89_003F(list);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_00f6: Stack underflow*/, (string)/*Error near IL_00f6: Stack underflow*/, (MessageBoxButton)/*Error near IL_00f6: Stack underflow*/, (MessageBoxImage)/*Error near IL_00f6: Stack underflow*/);
			}
			MyNav.PageAnswer = list;
			oPageNav.NextPage(MyNav, base.NavigationService);
			btnNav.Content = btnNav_Content;
			return;
			IL_00d4:
			goto IL_008c;
			IL_00a8:
			goto IL_0020;
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_0095: Incompatible stack heights: 0 vs 1
			//IL_009a: Incompatible stack heights: 1 vs 0
			//IL_00a5: Incompatible stack heights: 0 vs 1
			//IL_00aa: Incompatible stack heights: 1 vs 0
			//IL_00b5: Incompatible stack heights: 0 vs 1
			//IL_00ba: Incompatible stack heights: 1 vs 0
			//IL_00c5: Incompatible stack heights: 0 vs 1
			//IL_00ca: Incompatible stack heights: 1 vs 0
			//IL_00d5: Incompatible stack heights: 0 vs 1
			//IL_00dc: Incompatible stack heights: 0 vs 1
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
			if (_003F364_003F > _003F362_003F.Length)
			{
				int num6 = _003F362_003F.Length - 1;
			}
			num = (int)/*Error near IL_00dd: Stack underflow*/;
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
			//IL_0076: Incompatible stack heights: 0 vs 1
			//IL_007b: Incompatible stack heights: 1 vs 0
			//IL_0080: Incompatible stack heights: 0 vs 2
			//IL_0086: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 1 vs 0
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
			int length2;
			if (num2 + num > _003F362_003F.Length)
			{
				length2 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, length2);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0047: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_004c;
			}
			int length = _003F362_003F.Length;
			int startIndex = /*Error near IL_001d: Stack underflow*/- num;
			goto IL_004d;
			IL_004c:
			startIndex = 0;
			goto IL_004d;
			IL_004d:
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
			IL_0022:
			goto IL_004c;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0001Ūɟ\u0358ѓԇ٫\u0746ࡖ\u094cਟ\u0b40\u0c4d\u0d4c๐\u0f70ၰᅸቲ፯ᐵᕯᙱ\u1772ᡡ\u193a\u1a79\u1b72ᱦ\u1d63ṹί⁽Ⅴ≢⍬⑦╬♭❿⡲⤫⩼⭢Ɐ\u2d6d"), UriKind.Relative);
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
				((MatrixSingleExt)_003F350_003F).Loaded += _003F80_003F;
				((MatrixSingleExt)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				GridTitle = (Grid)_003F350_003F;
				break;
			case 6:
				TitleLeft = (ColumnDefinition)_003F350_003F;
				break;
			case 7:
				TitleRight = (ColumnDefinition)_003F350_003F;
				break;
			case 8:
				OptionTitleHeader = (TextBlock)_003F350_003F;
				break;
			case 9:
				ExtColumn = (ColumnDefinition)_003F350_003F;
				break;
			case 10:
				txt1 = (TextBlock)_003F350_003F;
				break;
			case 11:
				txt3 = (TextBlock)_003F350_003F;
				break;
			case 12:
				txt4 = (TextBlock)_003F350_003F;
				break;
			case 13:
				txt5 = (TextBlock)_003F350_003F;
				break;
			case 14:
				txt6 = (TextBlock)_003F350_003F;
				break;
			case 15:
				txt7 = (TextBlock)_003F350_003F;
				break;
			case 16:
				txt9 = (TextBlock)_003F350_003F;
				break;
			case 17:
				ScrollContent = (ScrollViewer)_003F350_003F;
				break;
			case 18:
				GridContent = (Grid)_003F350_003F;
				break;
			case 19:
				GridLeft = (ColumnDefinition)_003F350_003F;
				break;
			case 20:
				GridRight = (ColumnDefinition)_003F350_003F;
				break;
			case 21:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 22:
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
