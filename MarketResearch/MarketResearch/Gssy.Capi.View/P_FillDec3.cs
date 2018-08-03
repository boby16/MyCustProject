using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_FillDec3 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__22_0;

			internal int _003F318_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QBase oQuestion = new QBase();

		private QFill oQFill1 = new QFill();

		private QFill oQFill2 = new QFill();

		private QFill oQFill3 = new QFill();

		private string SelectedValue;

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private bool PageEntry;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal RowDefinition RowNote;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal WrapPanel wrapFill;

		internal WrapPanel wrapFill1;

		internal TextBlock txtBefore1;

		internal TextBox txtFill1;

		internal TextBlock txtAfter1;

		internal WrapPanel wrapFill2;

		internal TextBlock txtBefore2;

		internal TextBox txtFill2;

		internal TextBlock txtAfter2;

		internal WrapPanel wrapFill3;

		internal TextBlock txtBefore3;

		internal TextBox txtFill3;

		internal TextBlock txtAfter3;

		internal TextBlock txtQuestionNote;

		internal ScrollViewer scrollNote;

		internal StackPanel NoteArea;

		internal WrapPanel wrapButton;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_FillDec3()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0573: Incompatible stack heights: 0 vs 1
			//IL_057a: Incompatible stack heights: 0 vs 1
			//IL_0653: Incompatible stack heights: 0 vs 1
			//IL_065a: Incompatible stack heights: 0 vs 1
			//IL_0877: Incompatible stack heights: 0 vs 1
			//IL_087e: Incompatible stack heights: 0 vs 1
			//IL_0a9b: Incompatible stack heights: 0 vs 1
			//IL_0aa2: Incompatible stack heights: 0 vs 1
			//IL_1409: Incompatible stack heights: 0 vs 2
			//IL_1420: Incompatible stack heights: 0 vs 1
			//IL_1a75: Incompatible stack heights: 0 vs 1
			//IL_1a7c: Incompatible stack heights: 0 vs 1
			//IL_1aad: Incompatible stack heights: 0 vs 1
			//IL_1ab4: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			oQFill1.Init(CurPageId, 1);
			oQFill2.Init(CurPageId, 2);
			oQFill3.Init(CurPageId, 3);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					MyNav.GroupCodeB = oQuestion.QDefine.GROUP_CODEB;
					MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				MyNav.GetCircleInfo(MySurveyId);
				oQuestion.QuestionName += MyNav.QName_Add;
				oQFill1.QuestionName += MyNav.QName_Add;
				oQFill2.QuestionName += MyNav.QName_Add;
				oQFill3.QuestionName += MyNav.QName_Add;
				List<VEAnswer> list = new List<VEAnswer>();
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = MyNav.GroupCodeA;
				vEAnswer.CODE = MyNav.CircleACode;
				vEAnswer.CODE_TEXT = MyNav.CircleCodeTextA;
				list.Add(vEAnswer);
				SurveyHelper.CircleACode = MyNav.CircleACode;
				SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				SurveyHelper.CircleACount = MyNav.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					VEAnswer vEAnswer2 = new VEAnswer();
					vEAnswer2.QUESTION_NAME = MyNav.GroupCodeB;
					vEAnswer2.CODE = MyNav.CircleBCode;
					vEAnswer2.CODE_TEXT = MyNav.CircleCodeTextB;
					list.Add(vEAnswer2);
					SurveyHelper.CircleBCode = MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = MyNav.CircleBCount;
				}
			}
			else
			{
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
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list2.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text5 = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_057b: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQFill1.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill1.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore1, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text6 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_065b: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter1, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQFill1.QDefine.CONTROL_TYPE > 0)
			{
				txtFill1.MaxLength = oQFill1.QDefine.CONTROL_TYPE;
				txtFill1.Width = (double)oQFill1.QDefine.CONTROL_TYPE * txtFill1.FontSize * Math.Pow(0.955, (double)oQFill1.QDefine.CONTROL_TYPE);
			}
			if (oQFill1.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill1.Height = (double)oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill1.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill1.Width = (double)oQFill1.QDefine.CONTROL_WIDTH;
			}
			if (oQFill1.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill1.FontSize = (double)oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQFill2.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill2.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore2, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text7 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_087f: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter2, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQFill2.QDefine.CONTROL_TYPE > 0)
			{
				txtFill2.MaxLength = oQFill2.QDefine.CONTROL_TYPE;
				txtFill2.Width = (double)oQFill2.QDefine.CONTROL_TYPE * txtFill2.FontSize * Math.Pow(0.955, (double)oQFill2.QDefine.CONTROL_TYPE);
			}
			if (oQFill2.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill2.Height = (double)oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill2.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill2.Width = (double)oQFill2.QDefine.CONTROL_WIDTH;
			}
			if (oQFill2.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill2.FontSize = (double)oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQFill3.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill3.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore3, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text8 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0aa3: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter3, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQFill3.QDefine.CONTROL_TYPE > 0)
			{
				txtFill3.MaxLength = oQFill3.QDefine.CONTROL_TYPE;
				txtFill3.Width = (double)oQFill3.QDefine.CONTROL_TYPE * txtFill3.FontSize * Math.Pow(0.955, (double)oQFill3.QDefine.CONTROL_TYPE);
			}
			if (oQFill3.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill3.Height = (double)oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill3.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill3.Width = (double)oQFill3.QDefine.CONTROL_WIDTH;
			}
			if (oQFill3.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill3.FontSize = (double)oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == _003F487_003F._003F488_003F("W"))
			{
				wrapFill.Orientation = Orientation.Vertical;
			}
			if (oQFill1.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill1.Text = oLogicEngine.stringResult(oQFill1.QDefine.PRESET_LOGIC);
				txtFill1.SelectAll();
			}
			if (oQFill2.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill2.Text = oLogicEngine.stringResult(oQFill2.QDefine.PRESET_LOGIC);
				txtFill2.SelectAll();
			}
			if (oQFill3.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill3.Text = oLogicEngine.stringResult(oQFill3.QDefine.PRESET_LOGIC);
				txtFill3.SelectAll();
			}
			txtFill1.Focus();
			List<SurveyDetail>.Enumerator enumerator;
			if (oQFill1.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill1.QDefine.NOTE;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					string text = _003F487_003F._003F488_003F("");
					string text2 = _003F487_003F._003F488_003F("");
					int num = list2[1].IndexOf(_003F487_003F._003F488_003F("?"));
					if (num > 0)
					{
						text = _003F94_003F(list2[1], num + 1, -9999);
						text2 = _003F92_003F(list2[1], 1, num - 1);
						num = _003F96_003F(text2);
					}
					else
					{
						text = list2[1];
					}
					if (oQFill1.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && num > 0)
					{
						oQFill1.InitCircle();
						string text3 = _003F487_003F._003F488_003F("");
						if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
						{
							text3 = MyNav.CircleACode;
						}
						if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
						{
							text3 = MyNav.CircleBCode;
						}
						if (text3 != _003F487_003F._003F488_003F(""))
						{
							enumerator = oQFill1.QCircleDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current = enumerator.Current;
									if (current.CODE == text3)
									{
										text = current.EXTEND_1;
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
						}
					}
					if (text != _003F487_003F._003F488_003F(""))
					{
						string text4 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
						if (_003F93_003F(text, 1) == _003F487_003F._003F488_003F("\""))
						{
							text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + _003F94_003F(text, 1, -9999);
						}
						else if (!File.Exists(text4))
						{
							text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							image.Height = (double)num;
						}
						else if (text2 == _003F487_003F._003F488_003F("\""))
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						}
						else
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						}
						image.Stretch = Stretch.Uniform;
						image.Margin = new Thickness(0.0, 10.0, 20.0, 10.0);
						image.SetValue(Grid.ColumnProperty, 0);
						image.SetValue(Grid.RowProperty, 0);
						image.HorizontalAlignment = HorizontalAlignment.Center;
						image.VerticalAlignment = VerticalAlignment.Center;
						try
						{
							BitmapImage bitmapImage = new BitmapImage();
							bitmapImage.BeginInit();
							bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							scrollNote.Content = image;
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (oQuestion.QDefine.DETAIL_ID != _003F487_003F._003F488_003F(""))
			{
				if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
				{
					oLogicEngine.SurveyID = MySurveyId;
					if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
					{
						oLogicEngine.CircleACode = SurveyHelper.CircleACode;
						oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
						oLogicEngine.CircleACount = SurveyHelper.CircleACount;
						oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
						oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
						oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
						oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
						oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
					}
					string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int i = 0; i < array.Count(); i++)
					{
						enumerator = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current2 = enumerator.Current;
								if (current2.CODE == array[i].ToString())
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
					if (_003F7_003F._003C_003E9__22_0 == null)
					{
						_003F7_003F._003C_003E9__22_0 = _003F7_003F._003C_003E9._003F318_003F;
					}
					((List<SurveyDetail>)/*Error near IL_1425: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_1425: Stack underflow*/);
					oQuestion.QDetails = list3;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				Button_Height = SurveyHelper.BtnHeight;
				Button_FontSize = SurveyHelper.BtnFontSize;
				Button_Width = (double)SurveyHelper.BtnWidth;
				switch (oQuestion.QDefine.CONTROL_TYPE)
				{
				case 1:
					Button_Type = oQuestion.QDefine.CONTROL_TYPE;
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
					break;
				case 2:
					Button_Width = 440.0;
					Button_Type = oQuestion.QDefine.CONTROL_TYPE;
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
					break;
				case 20:
					Button_Height = SurveyHelper.BtnMediumHeight;
					Button_FontSize = SurveyHelper.BtnMediumFontSize;
					Button_Width = (double)SurveyHelper.BtnMediumWidth;
					break;
				case 30:
					Button_Height = SurveyHelper.BtnSmallHeight;
					Button_FontSize = SurveyHelper.BtnSmallFontSize;
					Button_Width = (double)SurveyHelper.BtnSmallWidth;
					break;
				}
				_003F28_003F();
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				if (txtFill1.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill1.Text = autoFill.FillDec(oQFill1.QDefine);
				}
				if (txtFill2.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill2.Text = autoFill.FillDec(oQFill2.QDefine);
				}
				if (txtFill3.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill3.Text = autoFill.FillDec(oQFill3.QDefine);
				}
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
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
				else if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && txtFill1.Text != _003F487_003F._003F488_003F("") && txtFill2.Text != _003F487_003F._003F488_003F("") && txtFill3.Text != _003F487_003F._003F488_003F("") && !SurveyHelper.AutoFill)
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			else
			{
				txtFill1.Text = oQFill1.ReadAnswerByQuestionName(MySurveyId, oQFill1.QuestionName);
				txtFill2.Text = oQFill2.ReadAnswerByQuestionName(MySurveyId, oQFill2.QuestionName);
				txtFill3.Text = oQFill3.ReadAnswerByQuestionName(MySurveyId, oQFill3.QuestionName);
				foreach (Button child in wrapButton.Children)
				{
					list2 = oBoldTitle.ParaToList((string)child.Tag, _003F487_003F._003F488_003F("\u007f"));
					string b = list2[0];
					if (list2.Count <= 1)
					{
						_003F487_003F._003F488_003F("");
					}
					else
					{
						string text9 = list2[1];
					}
					string b2 = (string)/*Error near IL_1a7e: Stack underflow*/;
					if (list2.Count <= 2)
					{
						_003F487_003F._003F488_003F("");
					}
					else
					{
						string text10 = list2[2];
					}
					string b3 = (string)/*Error near IL_1ab6: Stack underflow*/;
					if (txtFill1.Text == b && txtFill2.Text == b2 && txtFill3.Text == b3)
					{
						child.Style = style;
						txtFill1.Background = Brushes.LightGray;
						txtFill1.Foreground = Brushes.LightGray;
						txtFill2.Background = Brushes.LightGray;
						txtFill2.Foreground = Brushes.LightGray;
						txtFill3.Background = Brushes.LightGray;
						txtFill3.Foreground = Brushes.LightGray;
						txtFill1.IsEnabled = false;
						txtFill2.IsEnabled = false;
						txtFill3.IsEnabled = false;
						break;
					}
				}
			}
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
			txtFill1.SelectAll();
			txtFill1.Focus();
			PageLoaded = true;
			PageEntry = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0067: Incompatible stack heights: 0 vs 1
			//IL_0077: Incompatible stack heights: 0 vs 1
			//IL_0089: Incompatible stack heights: 0 vs 1
			//IL_0098: Incompatible stack heights: 0 vs 1
			//IL_009e: Incompatible stack heights: 0 vs 1
			//IL_00a4: Incompatible stack heights: 0 vs 1
			if (!PageLoaded)
			{
				return;
			}
			WrapPanel wrapButton2 = wrapButton;
			WrapPanel wrapPanel = (WrapPanel)/*Error near IL_000c: Stack underflow*/;
			if (Button_Type != 0)
			{
				if (Button_Type == 2)
				{
					goto IL_00a3;
				}
				goto IL_00a4;
			}
			ScrollViewer scrollNote2 = scrollNote;
			if (((ScrollViewer)/*Error near IL_001c: Stack underflow*/).ComputedVerticalScrollBarVisibility != Visibility.Collapsed)
			{
				wrapPanel.Orientation = Orientation.Horizontal;
				Button_Type = 1;
			}
			else
			{
				wrapPanel.Orientation = Orientation.Vertical;
				((P_FillDec3)/*Error near IL_0028: Stack underflow*/).Button_Type = 2;
			}
			goto IL_00a9;
			IL_00a3:
			goto IL_00a4;
			IL_0052:
			goto IL_00a3;
			IL_00a4:
			((WrapPanel)/*Error near IL_00a9: Stack underflow*/).Orientation = (Orientation)/*Error near IL_00a9: Stack underflow*/;
			goto IL_00a9;
			IL_00a9:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			PageLoaded = false;
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapButton;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.EXTEND_1 + _003F487_003F._003F488_003F("\u007f") + qDetail.EXTEND_2 + _003F487_003F._003F488_003F("\u007f") + qDetail.EXTEND_3;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_008c: Incompatible stack heights: 0 vs 1
			//IL_0093: Incompatible stack heights: 0 vs 1
			//IL_00c4: Incompatible stack heights: 0 vs 1
			//IL_00cb: Incompatible stack heights: 0 vs 1
			//IL_024e: Incompatible stack heights: 0 vs 1
			//IL_025e: Incompatible stack heights: 0 vs 1
			//IL_025f: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			List<string> list = oBoldTitle.ParaToList((string)button.Tag, _003F487_003F._003F488_003F("\u007f"));
			string text = list[0];
			if (list.Count <= 1)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				string text4 = list[1];
			}
			string text2 = (string)/*Error near IL_0095: Stack underflow*/;
			if (list.Count <= 2)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				string text5 = list[2];
			}
			string text3 = (string)/*Error near IL_00cd: Stack underflow*/;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (txtFill1.IsEnabled)
				{
					txtFill1.Tag = txtFill1.Text;
					txtFill1.Background = Brushes.LightGray;
					txtFill1.Foreground = Brushes.LightGray;
					txtFill1.IsEnabled = false;
					txtFill2.Tag = txtFill2.Text;
					txtFill2.Background = Brushes.LightGray;
					txtFill2.Foreground = Brushes.LightGray;
					txtFill2.IsEnabled = false;
					txtFill3.Tag = txtFill3.Text;
					txtFill3.Background = Brushes.LightGray;
					txtFill3.Foreground = Brushes.LightGray;
					txtFill3.IsEnabled = false;
				}
				txtFill1.Text = text;
				txtFill2.Text = text2;
				txtFill3.Text = text3;
				foreach (Button child in wrapButton.Children)
				{
					if (child.Name == button.Name)
					{
					}
					((FrameworkElement)/*Error near IL_0264: Stack underflow*/).Style = (Style)/*Error near IL_0264: Stack underflow*/;
				}
			}
			else
			{
				txtFill1.Text = (string)txtFill1.Tag;
				txtFill1.IsEnabled = true;
				txtFill1.Background = Brushes.White;
				txtFill1.Foreground = Brushes.Black;
				txtFill2.Text = (string)txtFill2.Tag;
				txtFill2.IsEnabled = true;
				txtFill2.Background = Brushes.White;
				txtFill2.Foreground = Brushes.Black;
				txtFill3.Text = (string)txtFill3.Tag;
				txtFill3.IsEnabled = true;
				txtFill3.Background = Brushes.White;
				txtFill3.Foreground = Brushes.Black;
				button.Style = style2;
				txtFill1.Focus();
			}
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_002d: Incompatible stack heights: 0 vs 1
			//IL_0039: Incompatible stack heights: 0 vs 2
			if (_003F348_003F.Key == Key.Return)
			{
				Button btnNav2 = btnNav;
				if (((UIElement)/*Error near IL_0011: Stack underflow*/).IsEnabled)
				{
					((P_FillDec3)/*Error near IL_001c: Stack underflow*/)._003F58_003F((object)/*Error near IL_001c: Stack underflow*/, (RoutedEventArgs)_003F348_003F);
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_017f: Incompatible stack heights: 0 vs 3
			//IL_0194: Incompatible stack heights: 0 vs 1
			//IL_01b3: Incompatible stack heights: 0 vs 1
			//IL_01d3: Incompatible stack heights: 0 vs 2
			//IL_01d8: Invalid comparison between Unknown and I4
			//IL_01de: Incompatible stack heights: 0 vs 1
			//IL_01f9: Incompatible stack heights: 0 vs 2
			//IL_01fe: Invalid comparison between Unknown and I4
			//IL_0209: Incompatible stack heights: 0 vs 1
			//IL_0229: Incompatible stack heights: 0 vs 2
			//IL_022e: Invalid comparison between Unknown and I4
			//IL_0236: Incompatible stack heights: 0 vs 3
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag = false;
				if (!(textBox.Text.Substring(offset, array[0].AddedLength).Trim() == _003F487_003F._003F488_003F("")))
				{
					double result = 0.0;
					flag = !double.TryParse(textBox.Text, out result);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					string text2 = textBox.Text;
					int addedLength = array[0].AddedLength;
					string text = ((string)/*Error near IL_009d: Stack underflow*/).Remove((int)/*Error near IL_009d: Stack underflow*/, addedLength);
					((TextBox)/*Error near IL_00a2: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
				else if (PageEntry)
				{
					SurveyDefine qDefine = oQuestion.QDefine;
					if (((SurveyDefine)/*Error near IL_00bb: Stack underflow*/).PAGE_COUNT_DOWN == -1)
					{
						bool flag2 = textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0343ѭկٮ\u0730");
						if ((int)/*Error near IL_01b8: Stack underflow*/ != 0)
						{
							int length = textBox.Text.Length;
							SurveyDefine qDefine2 = oQFill1.QDefine;
							int cONTROL_TYPE = ((SurveyDefine)/*Error near IL_00cb: Stack underflow*/).CONTROL_TYPE;
							if ((int)/*Error near IL_01d8: Stack underflow*/ == cONTROL_TYPE)
							{
								((P_FillDec3)/*Error near IL_00d5: Stack underflow*/).txtFill2.SelectAll();
								txtFill2.Focus();
								return;
							}
						}
						if (textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0343ѭկٮ\u0733"))
						{
							int length2 = textBox.Text.Length;
							QFill oQFill4 = oQFill2;
							int cONTROL_TYPE2 = ((QFill)/*Error near IL_0106: Stack underflow*/).QDefine.CONTROL_TYPE;
							if ((int)/*Error near IL_01fe: Stack underflow*/ == cONTROL_TYPE2)
							{
								TextBox txtFill4 = txtFill3;
								((TextBoxBase)/*Error near IL_0115: Stack underflow*/).SelectAll();
								txtFill3.Focus();
								return;
							}
						}
						if (textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0343ѭկٮ\u0732"))
						{
							int length3 = textBox.Text.Length;
							SurveyDefine qDefine3 = oQFill3.QDefine;
							int cONTROL_TYPE3 = ((SurveyDefine)/*Error near IL_0141: Stack underflow*/).CONTROL_TYPE;
							if ((int)/*Error near IL_022e: Stack underflow*/ == cONTROL_TYPE3)
							{
								((P_FillDec3)/*Error near IL_014b: Stack underflow*/)._003F58_003F((object)/*Error near IL_014b: Stack underflow*/, (RoutedEventArgs)/*Error near IL_014b: Stack underflow*/);
							}
						}
					}
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_0246: Expected I4, but got Unknown
			//IL_025b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0260: Expected I4, but got Unknown
			//IL_0923: Incompatible stack heights: 0 vs 2
			//IL_092f: Incompatible stack heights: 0 vs 2
			//IL_0946: Incompatible stack heights: 0 vs 4
			//IL_0972: Incompatible stack heights: 0 vs 2
			//IL_0977: Invalid comparison between Unknown and F8
			//IL_0993: Incompatible stack heights: 0 vs 1
			//IL_099e: Incompatible stack heights: 0 vs 1
			//IL_09a3: Invalid comparison between Unknown and F8
			//IL_09bd: Incompatible stack heights: 0 vs 2
			//IL_09cd: Incompatible stack heights: 0 vs 1
			//IL_0a05: Incompatible stack heights: 0 vs 2
			//IL_0a17: Incompatible stack heights: 0 vs 3
			//IL_0a2a: Incompatible stack heights: 0 vs 4
			//IL_0a39: Incompatible stack heights: 0 vs 1
			//IL_0a60: Incompatible stack heights: 0 vs 2
			//IL_0a65: Invalid comparison between Unknown and F8
			//IL_0a7a: Incompatible stack heights: 0 vs 2
			//IL_0a85: Incompatible stack heights: 0 vs 1
			//IL_0a8a: Invalid comparison between Unknown and F8
			//IL_0a9a: Incompatible stack heights: 0 vs 2
			//IL_0aaa: Incompatible stack heights: 0 vs 1
			//IL_0ac5: Incompatible stack heights: 0 vs 1
			//IL_0ae8: Incompatible stack heights: 0 vs 1
			//IL_0af4: Incompatible stack heights: 0 vs 2
			//IL_0b08: Incompatible stack heights: 0 vs 3
			//IL_0b1f: Incompatible stack heights: 0 vs 4
			//IL_0b36: Incompatible stack heights: 0 vs 1
			//IL_0b42: Incompatible stack heights: 0 vs 2
			//IL_0b47: Invalid comparison between Unknown and F8
			//IL_0b61: Incompatible stack heights: 0 vs 2
			//IL_0b72: Incompatible stack heights: 0 vs 2
			//IL_0b77: Invalid comparison between Unknown and F8
			//IL_0b91: Incompatible stack heights: 0 vs 2
			//IL_0ba1: Incompatible stack heights: 0 vs 1
			//IL_0bad: Incompatible stack heights: 0 vs 1
			//IL_0bc8: Incompatible stack heights: 0 vs 2
			//IL_0bd8: Incompatible stack heights: 0 vs 1
			//IL_0bed: Incompatible stack heights: 0 vs 1
			//IL_0bfd: Incompatible stack heights: 0 vs 1
			//IL_0c1b: Incompatible stack heights: 0 vs 2
			//IL_0c39: Incompatible stack heights: 0 vs 1
			//IL_0c45: Incompatible stack heights: 0 vs 2
			//IL_0c5c: Incompatible stack heights: 0 vs 4
			//IL_0c6b: Incompatible stack heights: 0 vs 1
			//IL_0c77: Incompatible stack heights: 0 vs 2
			//IL_0c8c: Incompatible stack heights: 0 vs 3
			//IL_0ca0: Incompatible stack heights: 0 vs 2
			//IL_0cac: Incompatible stack heights: 0 vs 2
			//IL_0cc1: Incompatible stack heights: 0 vs 3
			//IL_0cd0: Incompatible stack heights: 0 vs 1
			//IL_0ce3: Incompatible stack heights: 0 vs 1
			//IL_0cf8: Incompatible stack heights: 0 vs 3
			string text = txtFill1.Text;
			double num = 0.0;
			if (text.Length > 0)
			{
				int startIndex = ((string)/*Error near IL_0027: Stack underflow*/).Length - 1;
				if (((string)/*Error near IL_002f: Stack underflow*/).Substring(startIndex, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length = text.Length - 1;
					text = ((string)/*Error near IL_0050: Stack underflow*/).Substring((int)/*Error near IL_0050: Stack underflow*/, length);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_006b: Stack underflow*/, (string)/*Error near IL_006b: Stack underflow*/, (MessageBoxButton)/*Error near IL_006b: Stack underflow*/, (MessageBoxImage)/*Error near IL_006b: Stack underflow*/);
				txtFill1.SelectAll();
				txtFill1.Focus();
				return true;
			}
			if (txtFill1.IsEnabled)
			{
				num = Convert.ToDouble(text);
				if (oQFill1.QDefine.MIN_COUNT > 0)
				{
					int mIN_COUNT = oQFill1.QDefine.MIN_COUNT;
					double num2 = (double)/*Error near IL_00ac: Stack underflow*/;
					if (!((double)/*Error near IL_0977: Stack underflow*/ >= num2))
					{
						string msgFillNotSmall = SurveyMsg.MsgFillNotSmall;
						MessageBox.Show(string.Format(arg0: oQFill1.QDefine.MIN_COUNT.ToString(), format: (string)/*Error near IL_00bd: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
				if (oQFill1.QDefine.MAX_COUNT > 0)
				{
					double num3 = (double)oQFill1.QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_09a3: Stack underflow*/ <= num3))
					{
						string msgFillNotBig = SurveyMsg.MsgFillNotBig;
						int mAX_COUNT = oQFill1.QDefine.MAX_COUNT;
						MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_0110: Stack underflow*/).ToString(), format: (string)/*Error near IL_011e: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
				if (oQFill1.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					QFill oQFill4 = oQFill1;
					string text2 = ((QFill)/*Error near IL_016e: Stack underflow*/).QDefine.CONTROL_MASK;
					if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						text2 += _003F487_003F._003F488_003F(".ı");
					}
					string arg2 = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
					{
						string.Format(SurveyMsg.MsgFillFit, arg2);
						string msgCaption2 = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_01f7: Stack underflow*/, (string)/*Error near IL_01f7: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
			}
			oQFill1.FillText = text;
			text = txtFill2.Text;
			double num4 = 0.0;
			if (text.Length > 0)
			{
				int length2 = text.Length;
				_003F val = /*Error near IL_0240: Stack underflow*/- /*Error near IL_0240: Stack underflow*/;
				if (((string)/*Error near IL_0246: Stack underflow*/).Substring((int)val, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length3 = text.Length;
					_003F val2 = /*Error near IL_025b: Stack underflow*/- /*Error near IL_025b: Stack underflow*/;
					text = ((string)/*Error near IL_0260: Stack underflow*/).Substring((int)/*Error near IL_0260: Stack underflow*/, (int)val2);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill2 = SurveyMsg.MsgNotFill;
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0283: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
				txtFill2.SelectAll();
				txtFill2.Focus();
				return true;
			}
			if (txtFill2.IsEnabled)
			{
				num4 = Convert.ToDouble(text);
				if (oQFill2.QDefine.MIN_COUNT > 0)
				{
					SurveyDefine qDefine = oQFill2.QDefine;
					double num5 = (double)((SurveyDefine)/*Error near IL_02c8: Stack underflow*/).MIN_COUNT;
					if (!((double)/*Error near IL_0a65: Stack underflow*/ >= num5))
					{
						string msgFillNotSmall2 = SurveyMsg.MsgFillNotSmall;
						SurveyDefine qDefine2 = oQFill2.QDefine;
						MessageBox.Show(string.Format(arg0: ((SurveyDefine)/*Error near IL_02d3: Stack underflow*/).MIN_COUNT.ToString(), format: (string)/*Error near IL_02e1: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
				if (oQFill2.QDefine.MAX_COUNT > 0)
				{
					double num6 = (double)oQFill2.QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_0a8a: Stack underflow*/ <= num6))
					{
						string msgFillNotBig2 = SurveyMsg.MsgFillNotBig;
						QFill oQFill5 = oQFill2;
						MessageBox.Show(string.Format(arg0: ((QFill)/*Error near IL_0339: Stack underflow*/).QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_034c: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
				if (oQFill2.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					QFill oQFill6 = oQFill2;
					string text3 = ((QFill)/*Error near IL_039c: Stack underflow*/).QDefine.CONTROL_MASK;
					if (text3.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						string text5 = text3 + _003F487_003F._003F488_003F(".ı");
						text3 = (string)/*Error near IL_03bc: Stack underflow*/;
					}
					string arg3 = text3.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text3 + _003F487_003F._003F488_003F("(")))
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg3), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
			}
			oQFill2.FillText = text;
			text = txtFill3.Text;
			double num7 = 0.0;
			if (text.Length > 0)
			{
				int startIndex2 = ((string)/*Error near IL_046c: Stack underflow*/).Length - 1;
				if (((string)/*Error near IL_0474: Stack underflow*/).Substring(startIndex2, 1) == _003F487_003F._003F488_003F("/"))
				{
					int num10 = text.Length - 1;
					text = ((string)/*Error near IL_048d: Stack underflow*/).Substring((int)/*Error near IL_048d: Stack underflow*/, (int)/*Error near IL_048d: Stack underflow*/);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill3 = SurveyMsg.MsgNotFill;
				string msgCaption3 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_04a8: Stack underflow*/, (string)/*Error near IL_04a8: Stack underflow*/, (MessageBoxButton)/*Error near IL_04a8: Stack underflow*/, (MessageBoxImage)/*Error near IL_04a8: Stack underflow*/);
				txtFill3.SelectAll();
				txtFill3.Focus();
				return true;
			}
			if (txtFill3.IsEnabled)
			{
				num7 = Convert.ToDouble(text);
				QFill oQFill7 = oQFill3;
				if (((QFill)/*Error near IL_04d7: Stack underflow*/).QDefine.MIN_COUNT > 0)
				{
					double num8 = (double)((P_FillDec3)/*Error near IL_04e7: Stack underflow*/).oQFill3.QDefine.MIN_COUNT;
					if (!((double)/*Error near IL_0b47: Stack underflow*/ >= num8))
					{
						string msgFillNotSmall3 = SurveyMsg.MsgFillNotSmall;
						int mIN_COUNT2 = oQFill3.QDefine.MIN_COUNT;
						MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_04f7: Stack underflow*/).ToString(), format: (string)/*Error near IL_0505: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill3.SelectAll();
						txtFill3.Focus();
						return true;
					}
				}
				if (oQFill3.QDefine.MAX_COUNT > 0)
				{
					QFill oQFill8 = oQFill3;
					double num9 = (double)((QFill)/*Error near IL_0547: Stack underflow*/).QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_0b77: Stack underflow*/ <= num9))
					{
						string msgFillNotBig3 = SurveyMsg.MsgFillNotBig;
						int mAX_COUNT2 = oQFill3.QDefine.MAX_COUNT;
						MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_0552: Stack underflow*/).ToString(), format: (string)/*Error near IL_0560: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill3.SelectAll();
						txtFill3.Focus();
						return true;
					}
				}
				if (oQFill3.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					QFill oQFill9 = oQFill3;
					string text4 = ((QFill)/*Error near IL_05b0: Stack underflow*/).QDefine.CONTROL_MASK;
					if (text4.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						text4 = string.Concat(str1: _003F487_003F._003F488_003F(".ı"), str0: (string)/*Error near IL_05dd: Stack underflow*/);
					}
					string arg4 = text4.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text4 + _003F487_003F._003F488_003F("(")))
					{
						string.Format(SurveyMsg.MsgFillFit, arg4);
						string msgCaption4 = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_064a: Stack underflow*/, (string)/*Error near IL_064a: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill3.SelectAll();
						txtFill3.Focus();
						return true;
					}
				}
			}
			oQFill3.FillText = text;
			if (txtFill1.IsEnabled)
			{
				QBase oQuestion2 = oQuestion;
				if (((QBase)/*Error near IL_0685: Stack underflow*/).QDefine.CONTROL_MASK != _003F487_003F._003F488_003F("8"))
				{
					SurveyDefine qDefine3 = oQuestion.QDefine;
					if (!(((SurveyDefine)/*Error near IL_06a3: Stack underflow*/).CONTROL_MASK == _003F487_003F._003F488_003F("")))
					{
						QBase oQuestion3 = oQuestion;
						if (!(((QBase)/*Error near IL_06bc: Stack underflow*/).QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("1")))
						{
							if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("0"))
							{
								if (/*Error near IL_0c4a: Stack underflow*/ > /*Error near IL_0c4a: Stack underflow*/)
								{
									string msgRightNotSmallLeft = SurveyMsg.MsgRightNotSmallLeft;
									string msgCaption5 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_074b: Stack underflow*/, (string)/*Error near IL_074b: Stack underflow*/, (MessageBoxButton)/*Error near IL_074b: Stack underflow*/, (MessageBoxImage)/*Error near IL_074b: Stack underflow*/);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
								if (num4 > num7)
								{
									string msgRightNotSmallLeft2 = SurveyMsg.MsgRightNotSmallLeft;
									MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0779: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
									txtFill2.SelectAll();
									txtFill2.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3"))
							{
								if (/*Error near IL_0c7c: Stack underflow*/ <= /*Error near IL_0c7c: Stack underflow*/)
								{
									string msgRightSmallLeft = SurveyMsg.MsgRightSmallLeft;
									string msgCaption6 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_07c3: Stack underflow*/, (string)/*Error near IL_07c3: Stack underflow*/, (MessageBoxButton)/*Error near IL_07c3: Stack underflow*/, MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
								if (num4 <= num7)
								{
									string msgRightSmallLeft2 = SurveyMsg.MsgRightSmallLeft;
									string msgCaption7 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_07ec: Stack underflow*/, (string)/*Error near IL_07ec: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
									txtFill2.SelectAll();
									txtFill2.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2"))
							{
								if (/*Error near IL_0cb1: Stack underflow*/ < /*Error near IL_0cb1: Stack underflow*/)
								{
									string msgLeftNotSmallRight = SurveyMsg.MsgLeftNotSmallRight;
									string msgCaption8 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_0836: Stack underflow*/, (string)/*Error near IL_0836: Stack underflow*/, (MessageBoxButton)/*Error near IL_0836: Stack underflow*/, MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
								if (num4 < num7)
								{
									string msgLeftNotSmallRight2 = SurveyMsg.MsgLeftNotSmallRight;
									MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0864: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
									txtFill2.SelectAll();
									txtFill2.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("9"))
							{
								DateTime now = DateTime.Now;
								DateTime date = ((DateTime)/*Error near IL_08a7: Stack underflow*/).Date;
								if (Convert.ToDateTime(txtFill1.Text + _003F487_003F._003F488_003F(",") + txtFill2.Text + _003F487_003F._003F488_003F(",") + txtFill3.Text) > date)
								{
									string msgNotAfterDate = SurveyMsg.MsgNotAfterDate;
									string msgCaption9 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_0910: Stack underflow*/, (string)/*Error near IL_0910: Stack underflow*/, (MessageBoxButton)/*Error near IL_0910: Stack underflow*/, MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
							}
							goto IL_0d10;
						}
					}
					if (num >= num4)
					{
						string msgLeftSmallRight = SurveyMsg.MsgLeftSmallRight;
						string msgCaption10 = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_06e4: Stack underflow*/, (string)/*Error near IL_06e4: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
					if (num4 >= num7)
					{
						MessageBox.Show(SurveyMsg.MsgLeftSmallRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						((P_FillDec3)/*Error near IL_070a: Stack underflow*/).txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
			}
			goto IL_0d10;
			IL_0d10:
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill1.QuestionName;
			vEAnswer.CODE = oQFill1.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQFill1.QuestionName + _003F487_003F._003F488_003F("<") + oQFill1.FillText;
			vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill2.QuestionName;
			vEAnswer.CODE = oQFill2.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + oQFill2.QuestionName + _003F487_003F._003F488_003F("<") + oQFill2.FillText;
			vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill3.QuestionName;
			vEAnswer.CODE = oQFill3.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + oQFill3.QuestionName + _003F487_003F._003F488_003F("<") + oQFill3.FillText;
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			oQFill1.BeforeSave();
			oQFill1.Save(MySurveyId, SurveyHelper.SurveySequence);
			oQFill2.BeforeSave();
			oQFill2.Save(MySurveyId, SurveyHelper.SurveySequence);
			oQFill3.BeforeSave();
			oQFill3.Save(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00c0: Incompatible stack heights: 0 vs 2
			//IL_00d6: Incompatible stack heights: 0 vs 2
			//IL_00eb: Incompatible stack heights: 0 vs 2
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				Button btnNav2 = btnNav;
				string content = ((P_FillDec3)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0045: Stack underflow*/).Content = content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav3 = btnNav;
					string btnNav_Content2 = btnNav_Content;
					((ContentControl)/*Error near IL_0085: Stack underflow*/).Content = (object)/*Error near IL_0085: Stack underflow*/;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_009f: Stack underflow*/, (string)/*Error near IL_009f: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00aa:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0026: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				DispatcherTimer timer2 = timer;
				((DispatcherTimer)/*Error near IL_0010: Stack underflow*/).Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.ShowInputPanel();
			}
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
			//IL_005e: Incompatible stack heights: 0 vs 1
			//IL_0075: Incompatible stack heights: 0 vs 1
			//IL_007a: Incompatible stack heights: 1 vs 0
			//IL_007f: Incompatible stack heights: 0 vs 2
			//IL_0085: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
			int num = _003F365_003F;
			if (num == -9999)
			{
				int length2 = _003F362_003F.Length;
				num = (int)/*Error near IL_000e: Stack underflow*/;
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

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0058:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0064:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0070:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_007c:
			goto IL_004b;
			IL_004b:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQFill1.QuestionName;
			SurveyHelper.AttachPageId = CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7f᭑ᱫ\u1d65ṧὦ\u206dⅭ≤⌵\u242b╼♢❯⡭"), UriKind.Relative);
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
				((P_FillDec3)_003F350_003F).Loaded += _003F80_003F;
				((P_FillDec3)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				RowNote = (RowDefinition)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				wrapFill = (WrapPanel)_003F350_003F;
				break;
			case 6:
				wrapFill1 = (WrapPanel)_003F350_003F;
				break;
			case 7:
				txtBefore1 = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtFill1 = (TextBox)_003F350_003F;
				txtFill1.TextChanged += _003F98_003F;
				txtFill1.GotFocus += _003F91_003F;
				txtFill1.LostFocus += _003F90_003F;
				txtFill1.PreviewKeyDown += _003F86_003F;
				break;
			case 9:
				txtAfter1 = (TextBlock)_003F350_003F;
				break;
			case 10:
				wrapFill2 = (WrapPanel)_003F350_003F;
				break;
			case 11:
				txtBefore2 = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtFill2 = (TextBox)_003F350_003F;
				txtFill2.TextChanged += _003F98_003F;
				txtFill2.GotFocus += _003F91_003F;
				txtFill2.LostFocus += _003F90_003F;
				txtFill2.PreviewKeyDown += _003F86_003F;
				break;
			case 13:
				txtAfter2 = (TextBlock)_003F350_003F;
				break;
			case 14:
				wrapFill3 = (WrapPanel)_003F350_003F;
				break;
			case 15:
				txtBefore3 = (TextBlock)_003F350_003F;
				break;
			case 16:
				txtFill3 = (TextBox)_003F350_003F;
				txtFill3.TextChanged += _003F98_003F;
				txtFill3.GotFocus += _003F91_003F;
				txtFill3.LostFocus += _003F90_003F;
				txtFill3.PreviewKeyDown += _003F86_003F;
				break;
			case 17:
				txtAfter3 = (TextBlock)_003F350_003F;
				break;
			case 18:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 19:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 20:
				NoteArea = (StackPanel)_003F350_003F;
				break;
			case 21:
				wrapButton = (WrapPanel)_003F350_003F;
				break;
			case 22:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 23:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 24:
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
