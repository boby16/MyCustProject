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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_CmbList2 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__22_0;

			public static Comparison<SurveyDetail> _003C_003E9__22_1;

			public static Comparison<SurveyDetail> _003C_003E9__22_2;

			public static Comparison<SurveyDetail> _003C_003E9__24_0;

			internal int _003F318_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F319_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F320_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F321_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QSingle oQSingle1 = new QSingle();

		private QSingle oQSingle2 = new QSingle();

		private string Answer1 = _003F487_003F._003F488_003F("");

		private string Answer2 = _003F487_003F._003F488_003F("");

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private List<SurveyDetail> cmbList2Detail = new List<SurveyDetail>();

		internal RowDefinition RowNote;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal WrapPanel wrapFill;

		internal WrapPanel wrapFill1;

		internal TextBlock txtBefore1;

		internal ComboBox cmbSelect1;

		internal TextBlock txtAfter1;

		internal WrapPanel wrapFill2;

		internal TextBlock txtBefore2;

		internal ComboBox cmbSelect2;

		internal TextBlock txtAfter2;

		internal TextBlock txtQuestionNote;

		internal ScrollViewer scrollNote;

		internal StackPanel NoteArea;

		internal WrapPanel wrapButton;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_CmbList2()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0549: Incompatible stack heights: 0 vs 1
			//IL_0550: Incompatible stack heights: 0 vs 1
			//IL_0629: Incompatible stack heights: 0 vs 1
			//IL_0630: Incompatible stack heights: 0 vs 1
			//IL_0718: Incompatible stack heights: 0 vs 1
			//IL_071f: Incompatible stack heights: 0 vs 1
			//IL_09d2: Incompatible stack heights: 0 vs 2
			//IL_09e9: Incompatible stack heights: 0 vs 1
			//IL_0c03: Incompatible stack heights: 0 vs 2
			//IL_0c1a: Incompatible stack heights: 0 vs 1
			//IL_14be: Incompatible stack heights: 0 vs 2
			//IL_14d5: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			oQSingle1.Init(CurPageId, 1, true);
			oQSingle2.Init(CurPageId, 2, true);
			cmbList2Detail = oQSingle2.QDetails;
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
				oQSingle1.QuestionName += MyNav.QName_Add;
				oQSingle2.QuestionName += MyNav.QName_Add;
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
				string text7 = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_0551: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQSingle1.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQSingle1.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore1, qUESTION_TITLE, oQSingle1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text8 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0631: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter1, qUESTION_TITLE, oQSingle1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQSingle2.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQSingle2.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore2, qUESTION_TITLE, oQSingle1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text9 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0720: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter2, qUESTION_TITLE, oQSingle1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQSingle1.QDefine.CONTROL_HEIGHT != 0)
			{
				cmbSelect1.Height = (double)oQSingle1.QDefine.CONTROL_HEIGHT;
			}
			if (oQSingle1.QDefine.CONTROL_WIDTH != 0)
			{
				cmbSelect1.Width = (double)oQSingle1.QDefine.CONTROL_WIDTH;
			}
			if (oQSingle1.QDefine.CONTROL_FONTSIZE > 0)
			{
				cmbSelect1.FontSize = (double)oQSingle1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQSingle2.QDefine.CONTROL_HEIGHT != 0)
			{
				cmbSelect2.Height = (double)oQSingle1.QDefine.CONTROL_HEIGHT;
			}
			if (oQSingle2.QDefine.CONTROL_WIDTH != 0)
			{
				cmbSelect2.Width = (double)oQSingle2.QDefine.CONTROL_WIDTH;
			}
			if (oQSingle2.QDefine.CONTROL_FONTSIZE > 0)
			{
				cmbSelect2.FontSize = (double)oQSingle1.QDefine.CONTROL_FONTSIZE;
			}
			List<SurveyDetail>.Enumerator enumerator;
			if (oQSingle1.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQSingle1.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = oQSingle1.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == array[i].ToString())
							{
								list3.Add(current);
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
				((List<SurveyDetail>)/*Error near IL_09ee: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_09ee: Stack underflow*/);
				oQSingle1.QDetails = list3;
				if (oQSingle1.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int j = 0; j < oQSingle1.QDetails.Count(); j++)
					{
						oQSingle1.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQSingle1.QDetails[j].CODE_TEXT);
					}
				}
			}
			cmbSelect1.ItemsSource = oQSingle1.QDetails;
			cmbSelect1.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (oQSingle2.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(oQSingle2.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count(); k++)
				{
					enumerator = oQSingle2.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array2[k].ToString())
							{
								list4.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__22_1 == null)
				{
					_003F7_003F._003C_003E9__22_1 = _003F7_003F._003C_003E9._003F319_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0c1f: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0c1f: Stack underflow*/);
				oQSingle2.QDetails = list4;
				if (oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int l = 0; l < oQSingle2.QDetails.Count(); l++)
					{
						oQSingle2.QDetails[l].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQSingle2.QDetails[l].CODE_TEXT);
					}
				}
			}
			cmbSelect2.ItemsSource = oQSingle2.QDetails;
			cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == _003F487_003F._003F488_003F("W"))
			{
				wrapFill.Orientation = Orientation.Vertical;
			}
			if (oQSingle1.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				cmbSelect1.SelectedValue = oLogicEngine.stringResult(oQSingle1.QDefine.PRESET_LOGIC);
			}
			if (oQSingle2.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				cmbSelect2.SelectedValue = oLogicEngine.stringResult(oQSingle2.QDefine.PRESET_LOGIC);
			}
			else
			{
				_003F148_003F(cmbSelect1, null);
			}
			cmbSelect1.Focus();
			if (oQSingle1.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQSingle1.QDefine.NOTE;
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
					if (oQSingle1.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && num > 0)
					{
						oQSingle1.InitCircle();
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
							enumerator = oQSingle1.QCircleDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current3 = enumerator.Current;
									if (current3.CODE == text3)
									{
										text = current3.EXTEND_1;
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
					string[] array3 = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list5 = new List<SurveyDetail>();
					for (int m = 0; m < array3.Count(); m++)
					{
						enumerator = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current4 = enumerator.Current;
								if (current4.CODE == array3[m].ToString())
								{
									list5.Add(current4);
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					if (_003F7_003F._003C_003E9__22_2 == null)
					{
						_003F7_003F._003C_003E9__22_2 = _003F7_003F._003C_003E9._003F320_003F;
					}
					((List<SurveyDetail>)/*Error near IL_14da: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_14da: Stack underflow*/);
					oQuestion.QDetails = list5;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int n = 0; n < oQuestion.QDetails.Count(); n++)
					{
						oQuestion.QDetails[n].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[n].CODE_TEXT);
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
				if (cmbSelect1.SelectedValue == null)
				{
					cmbSelect1.SelectedValue = autoFill.SingleDetail(oQSingle1.QDefine, oQSingle1.QDetails).CODE;
				}
				if (cmbSelect2.SelectedValue == null)
				{
					_003F148_003F(cmbSelect1, null);
					cmbSelect2.SelectedValue = autoFill.SingleDetail(oQSingle2.QDefine, oQSingle2.QDetails).CODE;
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
				else if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && cmbSelect1.SelectedValue != _003F487_003F._003F488_003F("") && cmbSelect2.SelectedValue != _003F487_003F._003F488_003F("") && !SurveyHelper.AutoFill)
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			else
			{
				string text5 = oQSingle1.ReadAnswerByQuestionName(MySurveyId, oQSingle1.QuestionName);
				string text6 = oQSingle2.ReadAnswerByQuestionName(MySurveyId, oQSingle2.QuestionName);
				cmbSelect1.SelectedValue = text5;
				cmbSelect2.SelectedValue = text6;
				cmbSelect1.Text = oQSingle1.GetInnerCodeText(text5);
				cmbSelect2.Text = oQSingle2.GetInnerCodeText(text6);
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
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_006d: Incompatible stack heights: 0 vs 1
			//IL_0083: Incompatible stack heights: 0 vs 2
			//IL_00a3: Incompatible stack heights: 0 vs 1
			//IL_00a9: Incompatible stack heights: 0 vs 1
			//IL_00ae: Incompatible stack heights: 1 vs 0
			if (PageLoaded)
			{
				WrapPanel wrapPanel = wrapButton;
				if (((P_CmbList2)/*Error near IL_0010: Stack underflow*/).Button_Type != 0)
				{
					if (Button_Type == 2)
					{
					}
					((WrapPanel)/*Error near IL_0051: Stack underflow*/).Orientation = Orientation.Vertical;
				}
				else
				{
					Visibility computedVerticalScrollBarVisibility = scrollNote.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_0088: Stack underflow*/ == /*Error near IL_0088: Stack underflow*/)
					{
						wrapPanel.Orientation = Orientation.Vertical;
						Button_Type = 2;
					}
					else
					{
						wrapPanel.Orientation = Orientation.Horizontal;
						Button_Type = 1;
					}
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				PageLoaded = false;
			}
		}

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_01d5: Incompatible stack heights: 0 vs 2
			//IL_01ec: Incompatible stack heights: 0 vs 1
			cmbSelect2.Focus();
			if (cmbSelect1.SelectedValue == null)
			{
				Answer1 = _003F487_003F._003F488_003F("");
			}
			else
			{
				Answer1 = (string)cmbSelect1.SelectedValue;
				if (oQSingle2.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
				{
					oQSingle2.QDetails = cmbList2Detail;
					oQSingle1.SelectedCode = cmbSelect1.SelectedValue.ToString();
					List<VEAnswer> list = new List<VEAnswer>();
					VEAnswer vEAnswer = new VEAnswer();
					vEAnswer.QUESTION_NAME = oQSingle1.QuestionName;
					vEAnswer.CODE = oQSingle1.SelectedCode;
					list.Add(vEAnswer);
					oLogicEngine.PageAnswer = list;
					string[] array = oLogicEngine.aryCode(oQSingle2.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int i = 0; i < array.Count(); i++)
					{
						foreach (SurveyDetail qDetail in oQSingle2.QDetails)
						{
							if (qDetail.CODE == array[i].ToString())
							{
								list2.Add(qDetail);
								break;
							}
						}
					}
					if (_003F7_003F._003C_003E9__24_0 == null)
					{
						_003F7_003F._003C_003E9__24_0 = _003F7_003F._003C_003E9._003F321_003F;
					}
					((List<SurveyDetail>)/*Error near IL_01f1: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_01f1: Stack underflow*/);
					oQSingle2.QDetails = list2;
					if (oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
					{
						for (int j = 0; j < oQSingle2.QDetails.Count(); j++)
						{
							oQSingle2.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQSingle2.QDetails[j].CODE_TEXT);
						}
					}
					cmbSelect2.ItemsSource = null;
					cmbSelect2.ItemsSource = oQSingle2.QDetails;
					cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
					cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
				}
			}
		}

		private void _003F149_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			if (cmbSelect2.SelectedValue == null)
			{
				Answer2 = _003F487_003F._003F488_003F("");
			}
			else
			{
				Answer2 = (string)cmbSelect2.SelectedValue;
			}
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
				button.Tag = qDetail.EXTEND_1 + _003F487_003F._003F488_003F("\u007f") + qDetail.EXTEND_2;
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
			//IL_01bd: Incompatible stack heights: 0 vs 1
			//IL_01cd: Incompatible stack heights: 0 vs 1
			//IL_01ce: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			List<string> list = oBoldTitle.ParaToList((string)button.Tag, _003F487_003F._003F488_003F("\u007f"));
			string answer = list[0];
			if (list.Count <= 1)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				string text = list[1];
			}
			string answer2 = (string)/*Error near IL_0095: Stack underflow*/;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (cmbSelect1.IsEnabled)
				{
					cmbSelect1.Tag = cmbSelect1.SelectedValue;
					cmbSelect1.Background = Brushes.LightGray;
					cmbSelect1.Foreground = Brushes.LightGray;
					cmbSelect1.IsEnabled = false;
					cmbSelect2.Tag = cmbSelect2.SelectedValue;
					cmbSelect2.Background = Brushes.LightGray;
					cmbSelect2.Foreground = Brushes.LightGray;
					cmbSelect2.IsEnabled = false;
				}
				Answer1 = answer;
				Answer2 = answer2;
				foreach (Button child in wrapButton.Children)
				{
					if (child.Name == button.Name)
					{
					}
					((FrameworkElement)/*Error near IL_01d3: Stack underflow*/).Style = (Style)/*Error near IL_01d3: Stack underflow*/;
				}
			}
			else
			{
				cmbSelect1.SelectedValue = (string)cmbSelect1.Tag;
				cmbSelect1.IsEnabled = true;
				cmbSelect1.Background = Brushes.White;
				cmbSelect1.Foreground = Brushes.Black;
				cmbSelect2.SelectedValue = (string)cmbSelect2.Tag;
				cmbSelect2.IsEnabled = true;
				cmbSelect2.Background = Brushes.White;
				cmbSelect2.Foreground = Brushes.Black;
				button.Style = style2;
				Answer1 = _003F487_003F._003F488_003F("");
				Answer2 = _003F487_003F._003F488_003F("");
				cmbSelect1.Focus();
			}
		}

		private bool _003F87_003F()
		{
			//IL_025a: Incompatible stack heights: 0 vs 1
			//IL_0265: Incompatible stack heights: 0 vs 1
			//IL_0284: Incompatible stack heights: 0 vs 1
			//IL_0294: Incompatible stack heights: 0 vs 1
			//IL_02b3: Incompatible stack heights: 0 vs 1
			//IL_02c2: Incompatible stack heights: 0 vs 1
			//IL_02ea: Incompatible stack heights: 0 vs 1
			//IL_0304: Incompatible stack heights: 0 vs 1
			//IL_031b: Incompatible stack heights: 0 vs 1
			//IL_0332: Incompatible stack heights: 0 vs 4
			//IL_0363: Incompatible stack heights: 0 vs 2
			if (Answer1 == _003F487_003F._003F488_003F(""))
			{
				object selectedValue = cmbSelect1.SelectedValue;
				if ((int)/*Error near IL_025f: Stack underflow*/ == 0 || (string)((P_CmbList2)/*Error near IL_0024: Stack underflow*/).cmbSelect1.SelectedValue == _003F487_003F._003F488_003F(""))
				{
					MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					cmbSelect1.Focus();
					return true;
				}
			}
			if (Answer2 == _003F487_003F._003F488_003F(""))
			{
				object selectedValue2 = cmbSelect2.SelectedValue;
				if ((int)/*Error near IL_0289: Stack underflow*/ != 0)
				{
					ComboBox cmbSelect3 = cmbSelect2;
					if (!((string)((Selector)/*Error near IL_0087: Stack underflow*/).SelectedValue == _003F487_003F._003F488_003F("")))
					{
						goto IL_00c1;
					}
				}
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				cmbSelect2.Focus();
				return true;
			}
			goto IL_00c1;
			IL_00c1:
			if (Answer1 == _003F487_003F._003F488_003F(""))
			{
				SurveyDefine qDefine = oQuestion.QDefine;
				if (((SurveyDefine)/*Error near IL_00e0: Stack underflow*/).CONTROL_MASK == _003F487_003F._003F488_003F("9"))
				{
					DateTime now = DateTime.Now;
					DateTime date = ((DateTime)/*Error near IL_00f4: Stack underflow*/).Date;
					if (Convert.ToDateTime(cmbSelect1.SelectedValue.ToString() + _003F487_003F._003F488_003F(",") + cmbSelect2.SelectedValue.ToString() + _003F487_003F._003F488_003F(".ĲȰ")) > date)
					{
						MessageBox.Show(SurveyMsg.MsgNotAfterYM, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						cmbSelect1.Focus();
						return true;
					}
					goto IL_01fc;
				}
			}
			if (Answer1 == _003F487_003F._003F488_003F(""))
			{
				string cONTROL_MASK = oQuestion.QDefine.CONTROL_MASK;
				string b = _003F487_003F._003F488_003F("8");
				if ((string)/*Error near IL_0172: Stack underflow*/ == b)
				{
					DateTime date2 = DateTime.Now.Date;
					DateTime t = (DateTime)/*Error near IL_0178: Stack underflow*/;
					string text = t.Year.ToString();
					if (Convert.ToDateTime(text + _003F487_003F._003F488_003F(",") + cmbSelect1.SelectedValue.ToString() + _003F487_003F._003F488_003F(",") + cmbSelect2.SelectedValue.ToString()) > t)
					{
						string msgNotAfterDate = SurveyMsg.MsgNotAfterDate;
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_01ed: Stack underflow*/, (string)/*Error near IL_01ed: Stack underflow*/, (MessageBoxButton)/*Error near IL_01ed: Stack underflow*/, (MessageBoxImage)/*Error near IL_01ed: Stack underflow*/);
						cmbSelect1.Focus();
						return true;
					}
				}
			}
			goto IL_01fc;
			IL_01fc:
			if (Answer1 == _003F487_003F._003F488_003F(""))
			{
				Answer1 = (string)cmbSelect1.SelectedValue;
			}
			if (Answer2 == _003F487_003F._003F488_003F(""))
			{
				ComboBox cmbSelect4 = cmbSelect2;
				string answer = (string)((Selector)/*Error near IL_0235: Stack underflow*/).SelectedValue;
				((P_CmbList2)/*Error near IL_023f: Stack underflow*/).Answer2 = answer;
			}
			oQSingle1.SelectedCode = Answer1;
			oQSingle2.SelectedCode = Answer2;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQSingle1.QuestionName;
			vEAnswer.CODE = oQSingle1.SelectedCode;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQSingle1.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle1.SelectedCode;
			VEAnswer vEAnswer2 = new VEAnswer();
			vEAnswer2.QUESTION_NAME = oQSingle2.QuestionName;
			vEAnswer2.CODE = oQSingle2.SelectedCode;
			list.Add(vEAnswer2);
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + oQSingle2.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle2.SelectedCode;
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			oQSingle1.BeforeSave();
			oQSingle1.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle2.BeforeSave();
			oQSingle2.Save(MySurveyId, SurveyHelper.SurveySequence, false);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00b8: Incompatible stack heights: 0 vs 2
			//IL_00de: Incompatible stack heights: 0 vs 1
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
				string content = ((P_CmbList2)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0045: Stack underflow*/).Content = content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					btnNav.Content = btnNav_Content;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						MessageBox.Show(SurveyHelper.ShowPageAnswer((List<VEAnswer>)/*Error near IL_0097: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00a2:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0030: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				((P_CmbList2)/*Error near IL_0010: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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
			//IL_0096: Incompatible stack heights: 0 vs 1
			//IL_009b: Incompatible stack heights: 1 vs 0
			//IL_00a6: Incompatible stack heights: 0 vs 1
			//IL_00ab: Incompatible stack heights: 1 vs 0
			//IL_00b6: Incompatible stack heights: 0 vs 1
			//IL_00bb: Incompatible stack heights: 1 vs 0
			//IL_00c6: Incompatible stack heights: 0 vs 1
			//IL_00cb: Incompatible stack heights: 1 vs 0
			//IL_00d6: Incompatible stack heights: 0 vs 1
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
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0041: Incompatible stack heights: 1 vs 0
			//IL_0046: Incompatible stack heights: 0 vs 2
			//IL_004c: Incompatible stack heights: 0 vs 1
			//IL_0051: Incompatible stack heights: 1 vs 0
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
			//IL_0069: Incompatible stack heights: 0 vs 1
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
				num = (int)/*Error near IL_0015: Stack underflow*/;
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
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0048: Incompatible stack heights: 0 vs 2
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int startIndex;
			if (num > _003F362_003F.Length)
			{
				startIndex = 0;
			}
			else
			{
				int length = _003F362_003F.Length;
				startIndex = /*Error near IL_001c: Stack underflow*/- /*Error near IL_001c: Stack underflow*/;
			}
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
		}

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0057:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0063:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_006f:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_0080;
			IL_007b:
			goto IL_0080;
			IL_0080:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQSingle1.QuestionName;
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7f᭑ᱮ\u1d61ṩὦ\u2060ⅻ≳⌴\u242b╼♢❯⡭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0027:
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
				((P_CmbList2)_003F350_003F).Loaded += _003F80_003F;
				((P_CmbList2)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				cmbSelect1 = (ComboBox)_003F350_003F;
				cmbSelect1.SelectionChanged += _003F148_003F;
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
				cmbSelect2 = (ComboBox)_003F350_003F;
				cmbSelect2.SelectionChanged += _003F149_003F;
				break;
			case 13:
				txtAfter2 = (TextBlock)_003F350_003F;
				break;
			case 14:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 15:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 16:
				NoteArea = (StackPanel)_003F350_003F;
				break;
			case 17:
				wrapButton = (WrapPanel)_003F350_003F;
				break;
			case 18:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 19:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 20:
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
