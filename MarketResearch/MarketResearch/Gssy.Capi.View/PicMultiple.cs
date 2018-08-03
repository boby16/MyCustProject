using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class PicMultiple : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__31_0;

			internal int _003F317_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private List<Rectangle> listBtnFix = new List<Rectangle>();

		private List<SurveyDetail> listDetailFix = new List<SurveyDetail>();

		private List<Rectangle> listBtnNormal = new List<Rectangle>();

		private List<SurveyDetail> listDetailNormal = new List<SurveyDetail>();

		private bool IsFixOther;

		private bool IsFixNone;

		private List<Rectangle> listButton = new List<Rectangle>();

		private double SelImgOpacity = 0.8;

		private double UnSelImgOpacity;

		private double FixImgOpacity = 0.5;

		private Canvas canvas;

		private Image Picture;

		private double BmpHeight;

		private double ParaX = 1.0;

		private bool PageLoaded;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollmain;

		internal Grid g;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public PicMultiple()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05c4: Incompatible stack heights: 0 vs 1
			//IL_05cb: Incompatible stack heights: 0 vs 1
			//IL_0d0c: Incompatible stack heights: 0 vs 2
			//IL_0d23: Incompatible stack heights: 0 vs 1
			//IL_13b2: Incompatible stack heights: 0 vs 1
			//IL_13b3: Incompatible stack heights: 0 vs 1
			//IL_1525: Incompatible stack heights: 0 vs 1
			//IL_1526: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0, false);
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
			string sHOW_LOGIC = oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(_003F487_003F._003F488_003F(""));
			if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				list2 = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
				if (list2.Count > 1)
				{
					oQuestion.QDefine.DETAIL_ID = oLogicEngine.Route(list2[1]);
				}
			}
			oQuestion.InitDetailID(CurPageId, 0);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list3[0];
			if (qUESTION_TITLE == _003F487_003F._003F488_003F(""))
			{
				txtQuestionTitle.Height = 0.0;
				txtQuestionTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (list3.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text4 = list3[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_05cc: Stack underflow*/;
			if (qUESTION_TITLE == _003F487_003F._003F488_003F(""))
			{
				txtCircleTitle.Height = 0.0;
				txtCircleTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			}
			string text = _003F487_003F._003F488_003F("");
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				text = oQuestion.QDefine.CONTROL_TOOLTIP;
			}
			else if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
			{
				oQuestion.InitCircle();
				string text2 = _003F487_003F._003F488_003F("");
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
				{
					text2 = MyNav.CircleACode;
				}
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					text2 = MyNav.CircleBCode;
				}
				if (text2 != _003F487_003F._003F488_003F(""))
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == text2)
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
				if (oFunc.LEFT(text, 1) == _003F487_003F._003F488_003F("\""))
				{
					text = oFunc.MID(text, 1, -9999);
				}
				string text3 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
				if (!File.Exists(text3))
				{
					MessageBox.Show(oQuestion.QuestionName + _003F487_003F._003F488_003F("颋捒锑誑犋台瑊抋䛽瘰匸\uf409蟰妊祦哸遗淹\ued00") + Environment.NewLine + Environment.NewLine + _003F487_003F._003F488_003F("扅阄哽煅\ufb1b") + text3 + Environment.NewLine + Environment.NewLine + _003F487_003F._003F488_003F("触傷枺濗\ufb1b") + Environment.NewLine + _003F487_003F._003F488_003F("兪鄡叚\u035cџՍ\u0655紐嚕\uf615笮弙椨牻敷睽暖䟯恗皈嫸\uea01䞁蛆綺䴦䢠愌䊉揢惩钼뗬ᄃ"), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				Image image = new Image();
				if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("\""))
				{
					scrollmain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollmain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				}
				else if (!(oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("+")) && !(oQuestion.QDefine.CONTROL_MASK.Trim() == _003F487_003F._003F488_003F("")) && oQuestion.QDefine.CONTROL_MASK != null)
				{
					scrollmain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollmain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					int num = oFunc.StringToInt(oQuestion.QDefine.CONTROL_MASK);
					if (num > 0)
					{
						image.Width = (double)num;
					}
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				image.SetValue(Grid.ColumnProperty, 0);
				image.SetValue(Grid.RowProperty, 0);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				BitmapImage bitmapImage = new BitmapImage();
				try
				{
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("\""))
					{
						image.Width = (double)bitmapImage.PixelWidth;
					}
					g.Children.Add(image);
					Picture = image;
					BmpHeight = (double)bitmapImage.PixelHeight;
				}
				catch (Exception)
				{
				}
				canvas = new Canvas();
				canvas.Name = _003F487_003F._003F488_003F("eŤɪ\u0375ѣղ");
				g.Children.Add(canvas);
			}
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
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
				if (oQuestion.QDefine.SHOW_LOGIC == _003F487_003F._003F488_003F("") && oQuestion.QDefine.IS_RANDOM == 0)
				{
					if (_003F7_003F._003C_003E9__31_0 == null)
					{
						_003F7_003F._003C_003E9__31_0 = _003F7_003F._003C_003E9._003F317_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0d28: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0d28: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.FIX_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.FIX_LOGIC, ',');
				for (int j = 0; j < array2.Count(); j++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								listFix.Add(array2[j]);
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
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int k = 0; k < array3.Count(); k++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array3[k])
							{
								listPreSet.Add(array3[k]);
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
			_003F28_003F();
			if (ExistTextFill || IsFixOther)
			{
				txtFill.Visibility = Visibility.Visible;
				if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F(""))
				{
					txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					qUESTION_TITLE = oQuestion.QDefine.NOTE;
					list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list3[0];
					oBoldTitle.SetTextBlock(txtFillTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list3.Count > 1)
					{
						qUESTION_TITLE = list3[1];
						oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					}
				}
				if (IsFixOther)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
				}
			}
			else
			{
				txtFill.Height = 0.0;
				txtFillTitle.Height = 0.0;
				txtAfter.Height = 0.0;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				listButton = autoFill.MultiRectangle(oQuestion.QDefine, listButton, 0);
				foreach (Rectangle item in listButton)
				{
					_003F29_003F(item, null);
				}
				if (txtFill.IsEnabled)
				{
					txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (listButton.Count > 0 && autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			bool flag = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else
				{
					foreach (string item2 in listPreSet)
					{
						if (!listFix.Contains(item2))
						{
							oQuestion.SelectedValues.Add(item2);
							if (!_003F134_003F(item2))
							{
							}
							flag = ((byte)/*Error near IL_1528: Stack underflow*/ != 0);
						}
					}
					if (flag)
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
					}
					if (oQuestion.QDetails.Count == 1 || listBtnNormal.Count == 0)
					{
						if (listBtnNormal.Count > 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && listBtnNormal[0].Opacity == UnSelImgOpacity)
						{
							_003F29_003F(listBtnNormal[0], null);
						}
						if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (txtFill.IsEnabled)
							{
								txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								_003F58_003F(this, _003F348_003F);
							}
						}
					}
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedValues.Count > 0)
					{
						if (txtFill.IsEnabled)
						{
							txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							_003F58_003F(this, _003F348_003F);
						}
					}
				}
			}
			else
			{
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer item3 in oQuestion.QAnswersRead)
				{
					if (oFunc.MID(item3.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ"))
					{
						if (!listFix.Contains(item3.CODE))
						{
							oQuestion.SelectedValues.Add(item3.CODE);
							if (!_003F134_003F(item3.CODE))
							{
							}
							flag = ((byte)/*Error near IL_13b5: Stack underflow*/ != 0);
						}
					}
					else if (ExistTextFill && item3.QUESTION_NAME == oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349") && item3.CODE != _003F487_003F._003F488_003F(""))
					{
						txtFill.Text = item3.CODE;
					}
				}
				if (flag)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
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
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0243: Incompatible stack heights: 0 vs 2
			//IL_027a: Incompatible stack heights: 0 vs 1
			if (PageLoaded)
			{
				double paraX = ((PicMultiple)/*Error near IL_0010: Stack underflow*/).Picture.ActualHeight / BmpHeight;
				((PicMultiple)/*Error near IL_0021: Stack underflow*/).ParaX = paraX;
				Point point = canvas.TranslatePoint(default(Point), Picture);
				for (int i = 0; i < listBtnFix.Count; i++)
				{
					Rectangle rectangle = listBtnFix[i];
					SurveyDetail surveyDetail = listDetailFix[i];
					double num = oFunc.StringToDouble(surveyDetail.EXTEND_1) * ParaX - point.X;
					double num2 = oFunc.StringToDouble(surveyDetail.EXTEND_2) * ParaX - point.Y;
					double num3 = oFunc.StringToDouble(surveyDetail.EXTEND_3) * ParaX - point.X;
					double num4 = oFunc.StringToDouble(surveyDetail.EXTEND_4) * ParaX - point.Y;
					rectangle.Width = num3 - num + 1.0;
					rectangle.Height = num4 - num2 + 1.0;
					Canvas.SetLeft(rectangle, num);
					Canvas.SetTop(rectangle, num2);
				}
				for (int j = 0; j < listBtnNormal.Count; j++)
				{
					Rectangle rectangle2 = listBtnNormal[j];
					SurveyDetail surveyDetail2 = listDetailNormal[j];
					double num5 = oFunc.StringToDouble(surveyDetail2.EXTEND_1) * ParaX - point.X;
					double num6 = oFunc.StringToDouble(surveyDetail2.EXTEND_2) * ParaX - point.Y;
					double num7 = oFunc.StringToDouble(surveyDetail2.EXTEND_3) * ParaX - point.X;
					double num8 = oFunc.StringToDouble(surveyDetail2.EXTEND_4) * ParaX - point.Y;
					rectangle2.Width = num7 - num5 + 1.0;
					rectangle2.Height = num8 - num6 + 1.0;
					Canvas.SetLeft(rectangle2, num5);
					Canvas.SetTop(rectangle2, num6);
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				((PicMultiple)/*Error near IL_0236: Stack underflow*/).PageLoaded = false;
			}
		}

		private void _003F28_003F()
		{
			//IL_03bf: Incompatible stack heights: 0 vs 1
			//IL_03d2: Incompatible stack heights: 0 vs 1
			//IL_04bd: Incompatible stack heights: 0 vs 1
			//IL_04d2: Incompatible stack heights: 0 vs 1
			//IL_04d8: Incompatible stack heights: 0 vs 1
			Canvas canvas = this.canvas;
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
							if (current.IS_OTHER == 1 || current.IS_OTHER == 3 || ((current.IS_OTHER == 11) | (current.IS_OTHER == 5)) || current.IS_OTHER == 13 || current.IS_OTHER == 14)
							{
								IsFixOther = true;
							}
							if (current.IS_OTHER == 2 || current.IS_OTHER == 3 || ((current.IS_OTHER == 13) | (current.IS_OTHER == 5)) || current.IS_OTHER == 4 || current.IS_OTHER == 14)
							{
								IsFixNone = true;
							}
						}
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			int num = 0;
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					double num2 = oFunc.StringToDouble(current2.EXTEND_1);
					double num3 = oFunc.StringToDouble(current2.EXTEND_2);
					double num4 = oFunc.StringToDouble(current2.EXTEND_3);
					double num5 = oFunc.StringToDouble(current2.EXTEND_4);
					if (num4 - num2 > 0.0 && num5 - num3 > 0.0)
					{
						bool flag = false;
						if (current2.IS_OTHER == 1 || current2.IS_OTHER == 3 || ((current2.IS_OTHER == 11) | (current2.IS_OTHER == 5)) || current2.IS_OTHER == 13 || current2.IS_OTHER == 14)
						{
							flag = true;
						}
						if (flag)
						{
							ExistTextFill = true;
						}
						bool flag2 = false;
						if (current2.IS_OTHER == 2 || current2.IS_OTHER == 3 || ((current2.IS_OTHER == 13) | (current2.IS_OTHER == 4)) || current2.IS_OTHER == 5 || current2.IS_OTHER == 14)
						{
							flag2 = true;
						}
						string cODE_TEXT = current2.CODE_TEXT;
						if (!(oFunc.LEFT(cODE_TEXT, 1) == _003F487_003F._003F488_003F("\"")))
						{
							string cODE = current2.CODE;
						}
						else
						{
							oFunc.MID(cODE_TEXT, 1, -9999);
						}
						string item = (string)/*Error near IL_03d4: Stack underflow*/;
						bool flag3 = listFix.Contains(item);
						bool flag4 = true;
						if (!flag3)
						{
							if (IsFixNone)
							{
								flag4 = false;
							}
							else if (flag2 && listFix.Count > 0)
							{
								flag4 = false;
							}
						}
						if (flag4)
						{
							Rectangle rectangle = new Rectangle();
							rectangle.Name = _003F487_003F._003F488_003F("`Ş") + current2.CODE;
							rectangle.Fill = Brushes.White;
							rectangle.Stroke = Brushes.LightBlue;
							rectangle.StrokeThickness = 1.0;
							if (!flag3)
							{
								double unSelImgOpacity = UnSelImgOpacity;
							}
							else
							{
								double fixImgOpacity = FixImgOpacity;
							}
							((UIElement)/*Error near IL_04dd: Stack underflow*/).Opacity = (double)/*Error near IL_04dd: Stack underflow*/;
							rectangle.Tag = num;
							if (!flag3)
							{
								if (oQuestion.QDefine.CONTROL_TYPE == 0)
								{
									rectangle.MouseLeftButtonUp += _003F29_003F;
								}
								else
								{
									rectangle.MouseLeftButtonUp += _003F120_003F;
								}
							}
							canvas.Children.Add(rectangle);
							if (flag3)
							{
								listBtnFix.Add(rectangle);
								listDetailFix.Add(current2);
							}
							else
							{
								listBtnNormal.Add(rectangle);
								listDetailNormal.Add(current2);
								if (!flag2)
								{
									listButton.Add(rectangle);
								}
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
		}

		private void _003F29_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F = null)
		{
			//IL_0323: Incompatible stack heights: 0 vs 1
			//IL_0338: Incompatible stack heights: 0 vs 1
			//IL_033e: Incompatible stack heights: 0 vs 1
			Rectangle rectangle = (Rectangle)_003F347_003F;
			int index = (int)rectangle.Tag;
			int iS_OTHER = oQuestion.QDetails[index].IS_OTHER;
			string text = oQuestion.QDetails[index].CODE;
			if (oFunc.LEFT(oQuestion.QDetails[index].CODE_TEXT, 1) == _003F487_003F._003F488_003F("\""))
			{
				text = oFunc.MID(oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
			}
			int num = 0;
			if (iS_OTHER == 2 || iS_OTHER == 3 || iS_OTHER == 5 || iS_OTHER == 13 || iS_OTHER == 4 || iS_OTHER == 14)
			{
				num = 1;
			}
			int num2 = 0;
			if (rectangle.Opacity == SelImgOpacity)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (num2 == 0)
			{
				if (num == 1)
				{
					oQuestion.SelectedValues.Clear();
					num3 = 1;
				}
				else
				{
					num3 = 2;
				}
				if (!oQuestion.SelectedValues.Contains(text))
				{
					oQuestion.SelectedValues.Add(text);
				}
				rectangle.Opacity = SelImgOpacity;
			}
			else if (num == 1)
			{
				num3 = 0;
			}
			else
			{
				oQuestion.SelectedValues.Remove(text);
				rectangle.Opacity = UnSelImgOpacity;
				num3 = 3;
			}
			if (num3 > 0)
			{
				bool flag = true;
				foreach (Rectangle item in listBtnNormal)
				{
					int index2 = (int)item.Tag;
					int iS_OTHER2 = oQuestion.QDetails[index2].IS_OTHER;
					string text2 = oQuestion.QDetails[index2].CODE;
					if (oFunc.LEFT(oQuestion.QDetails[index2].CODE_TEXT, 1) == _003F487_003F._003F488_003F("\""))
					{
						text2 = oFunc.MID(oQuestion.QDetails[index2].CODE_TEXT, 1, -9999);
					}
					if (!(text2 == text))
					{
						switch (num3)
						{
						case 1:
							item.Opacity = UnSelImgOpacity;
							break;
						case 2:
							if ((iS_OTHER2 == 2 || iS_OTHER2 == 3 || iS_OTHER2 == 5 || iS_OTHER2 == 13 || iS_OTHER2 == 4 || iS_OTHER2 == 14) && item.Opacity == SelImgOpacity)
							{
								item.Opacity = UnSelImgOpacity;
								if (oQuestion.SelectedValues.Contains(text2))
								{
									oQuestion.SelectedValues.Remove(text2);
								}
							}
							break;
						}
					}
					else
					{
						if (num2 != 0)
						{
							double unSelImgOpacity = UnSelImgOpacity;
						}
						else
						{
							double selImgOpacity = SelImgOpacity;
						}
						((UIElement)/*Error near IL_0343: Stack underflow*/).Opacity = (double)/*Error near IL_0343: Stack underflow*/;
					}
					if (!IsFixOther && flag && item.Opacity == SelImgOpacity && (iS_OTHER2 == 1 || iS_OTHER2 == 3 || iS_OTHER2 == 5 || iS_OTHER2 == 11 || iS_OTHER2 == 13 || iS_OTHER2 == 14))
					{
						flag = false;
					}
				}
				if (!IsFixOther)
				{
					if (flag)
					{
						txtFill.Background = Brushes.LightGray;
						txtFill.IsEnabled = false;
					}
					else
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
						if (txtFill.Text == _003F487_003F._003F488_003F(""))
						{
							txtFill.Focus();
						}
					}
				}
			}
		}

		private bool _003F134_003F(string _003F374_003F)
		{
			bool result = false;
			foreach (Rectangle item in listBtnNormal)
			{
				int index = (int)item.Tag;
				string a = oQuestion.QDetails[index].CODE;
				if (oFunc.LEFT(oQuestion.QDetails[index].CODE_TEXT, 1) == _003F487_003F._003F488_003F("\""))
				{
					a = oFunc.MID(oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
				}
				if (a == _003F374_003F)
				{
					if (item.Opacity == UnSelImgOpacity)
					{
						item.Opacity = SelImgOpacity;
						int iS_OTHER = oQuestion.QDetails[index].IS_OTHER;
						if (iS_OTHER == 1 || iS_OTHER == 3 || iS_OTHER == 5 || ((iS_OTHER == 11) | (iS_OTHER == 13)) || iS_OTHER == 14)
						{
							result = true;
						}
					}
					else
					{
						item.Opacity = UnSelImgOpacity;
					}
				}
			}
			return result;
		}

		private void _003F120_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			//IL_00e6: Incompatible stack heights: 0 vs 2
			//IL_00f3: Incompatible stack heights: 0 vs 2
			//IL_0100: Incompatible stack heights: 0 vs 2
			Point position = _003F348_003F.GetPosition(Picture);
			double x = position.X;
			double y = position.Y;
			int i = 0;
			goto IL_00c3;
			IL_00c3:
			for (; i < listBtnNormal.Count; i++)
			{
				Rectangle _003F347_003F2 = listBtnNormal[i];
				SurveyDetail surveyDetail = listDetailNormal[i];
				double num = oFunc.StringToDouble(surveyDetail.EXTEND_1) * ParaX;
				double num2 = oFunc.StringToDouble(surveyDetail.EXTEND_2) * ParaX;
				double num3 = oFunc.StringToDouble(surveyDetail.EXTEND_3) * ParaX;
				double num4 = oFunc.StringToDouble(surveyDetail.EXTEND_4) * ParaX;
				if (num <= x && /*Error near IL_00eb: Stack underflow*/<= /*Error near IL_00eb: Stack underflow*/&& /*Error near IL_00f8: Stack underflow*/>= /*Error near IL_00f8: Stack underflow*/&& /*Error near IL_0105: Stack underflow*/>= /*Error near IL_0105: Stack underflow*/)
				{
					_003F29_003F(_003F347_003F2, _003F348_003F);
				}
			}
			return;
			IL_0119:
			goto IL_00c3;
		}

		private bool _003F87_003F()
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Incompatible stack heights: 0 vs 1
			//IL_015d: Incompatible stack heights: 0 vs 4
			//IL_0173: Incompatible stack heights: 0 vs 2
			//IL_0178: Invalid comparison between Unknown and I4
			//IL_018d: Incompatible stack heights: 0 vs 2
			//IL_01b2: Incompatible stack heights: 0 vs 2
			//IL_01b7: Invalid comparison between Unknown and I4
			//IL_01c2: Incompatible stack heights: 0 vs 2
			//IL_01e6: Incompatible stack heights: 0 vs 2
			//IL_01fa: Incompatible stack heights: 0 vs 2
			//IL_01ff: Incompatible stack heights: 0 vs 1
			//IL_020e: Incompatible stack heights: 0 vs 1
			//IL_0218: Incompatible stack heights: 0 vs 1
			if (listFix.Count == 0)
			{
				QMultiple oQuestion2 = oQuestion;
				if (((QMultiple)/*Error near IL_0015: Stack underflow*/).SelectedValues.Count == 0)
				{
					string msgNotSelected = SurveyMsg.MsgNotSelected;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_0024: Stack underflow*/, (string)/*Error near IL_0024: Stack underflow*/, (MessageBoxButton)/*Error near IL_0024: Stack underflow*/, (MessageBoxImage)/*Error near IL_0024: Stack underflow*/);
					return true;
				}
			}
			if (oQuestion.QDefine.MIN_COUNT != 0)
			{
				int count2 = listFix.Count;
				int count = ((PicMultiple)/*Error near IL_0041: Stack underflow*/).oQuestion.SelectedValues.Count;
				if (/*Error near IL_004c: Stack underflow*/ + count < oQuestion.QDefine.MIN_COUNT)
				{
					string msgMAless = SurveyMsg.MsgMAless;
					SurveyDefine qDefine = oQuestion.QDefine;
					MessageBox.Show(string.Format(arg0: ((SurveyDefine)/*Error near IL_0066: Stack underflow*/).MIN_COUNT.ToString(), format: (string)/*Error near IL_0073: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (oQuestion.QDefine.MAX_COUNT != 0)
			{
				int count3 = listFix.Count;
				int count4 = oQuestion.SelectedValues.Count;
				if (/*Error near IL_0099: Stack underflow*/ + /*Error near IL_0099: Stack underflow*/> oQuestion.QDefine.MAX_COUNT)
				{
					string msgMAmore = SurveyMsg.MsgMAmore;
					MessageBox.Show(string.Format(arg0: ((PicMultiple)/*Error near IL_00b3: Stack underflow*/).oQuestion.QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_00ca: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (txtFill.IsEnabled)
			{
				txtFill.Text.Trim();
				_003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_00ef: Stack underflow*/ == (string)/*Error near IL_00ef: Stack underflow*/)
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_00fc: Stack underflow*/, (string)/*Error near IL_00fc: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			QMultiple oQuestion3 = oQuestion;
			if (!txtFill.IsEnabled)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				txtFill.Text.Trim();
			}
			((QMultiple)/*Error near IL_021d: Stack underflow*/).FillText = (string)/*Error near IL_021d: Stack underflow*/;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			foreach (string item in listFix)
			{
				oQuestion.SelectedValues.Add(item);
			}
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			for (int i = 0; i < oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ") + (i + 1).ToString();
				vEAnswer.CODE = oQuestion.SelectedValues[i].ToString();
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer.CODE;
			}
			SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				vEAnswer2.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_0046: Incompatible stack heights: 0 vs 1
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				PageNav oPageNav2 = oPageNav;
				int delaySeconds = SurveyMsg.DelaySeconds;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_0058: Stack underflow*/).PageDataLog(delaySeconds, _003F370_003F, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00e8: Incompatible stack heights: 0 vs 1
			//IL_00f9: Incompatible stack heights: 0 vs 2
			//IL_010f: Incompatible stack heights: 0 vs 3
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				((PicMultiple)/*Error near IL_0040: Stack underflow*/).btnNav.Content = btnNav_Content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav2 = btnNav;
					string content = ((PicMultiple)/*Error near IL_008b: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_0090: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00a9: Stack underflow*/, (string)/*Error near IL_00a9: Stack underflow*/, (MessageBoxButton)/*Error near IL_00a9: Stack underflow*/, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00d8:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0025: Incompatible stack heights: 0 vs 1
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

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQuestion.QuestionName;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a60᭦ᱭ\u1d60ṹὧ⁾Ⅰ≸⍫④┫♼❢⡯⥭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
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
				((PicMultiple)_003F350_003F).Loaded += _003F80_003F;
				((PicMultiple)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				g = (Grid)_003F350_003F;
				break;
			case 6:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 7:
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 9:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 11:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 12:
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
