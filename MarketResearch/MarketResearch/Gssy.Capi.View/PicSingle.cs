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
using System.Text.RegularExpressions;
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
	public class PicSingle : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__23_0;

			internal int _003F310_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QSingle oQuestion = new QSingle();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<Rectangle> listBtnNormal = new List<Rectangle>();

		private double SelImgOpacity = 0.8;

		private double UnSelImgOpacity;

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

		public PicSingle()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05c4: Incompatible stack heights: 0 vs 1
			//IL_05cb: Incompatible stack heights: 0 vs 1
			//IL_0d0c: Incompatible stack heights: 0 vs 2
			//IL_0d23: Incompatible stack heights: 0 vs 1
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
					if (_003F7_003F._003C_003E9__23_0 == null)
					{
						_003F7_003F._003C_003E9__23_0 = _003F7_003F._003C_003E9._003F310_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0d28: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0d28: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count(); j++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								listPreSet.Add(array2[j]);
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
			if (ExistTextFill)
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
				Rectangle rectangle = autoFill.SingleRectangle(oQuestion.QDefine, listBtnNormal);
				if (rectangle != null)
				{
					if (listPreSet.Count == 0)
					{
						_003F29_003F(rectangle, null);
					}
					if (txtFill.IsEnabled)
					{
						txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
					}
					if (autoFill.AutoNext(oQuestion.QDefine))
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
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
					if (listPreSet.Count > 0)
					{
						oQuestion.SelectedCode = listPreSet[0];
						if (_003F134_003F(listPreSet[0]))
						{
							txtFill.IsEnabled = true;
							txtFill.Background = Brushes.White;
						}
					}
					if (oQuestion.QDetails.Count == 1)
					{
						if (listPreSet.Count == 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
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
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
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
				oQuestion.SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				bool num2 = _003F134_003F(oQuestion.SelectedCode);
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				if (num2)
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
			//IL_014f: Incompatible stack heights: 0 vs 2
			//IL_016e: Incompatible stack heights: 0 vs 3
			if (PageLoaded)
			{
				double paraX = ((PicSingle)/*Error near IL_0010: Stack underflow*/).Picture.ActualHeight / BmpHeight;
				((PicSingle)/*Error near IL_0021: Stack underflow*/).ParaX = paraX;
				Point point = canvas.TranslatePoint(default(Point), Picture);
				for (int i = 0; i < listBtnNormal.Count; i++)
				{
					Rectangle rectangle = listBtnNormal[i];
					SurveyDetail surveyDetail = oQuestion.QDetails[i];
					double num = oFunc.StringToDouble(surveyDetail.EXTEND_1) * ParaX - point.X;
					double num2 = oFunc.StringToDouble(surveyDetail.EXTEND_2) * ParaX - point.Y;
					double num3 = oFunc.StringToDouble(surveyDetail.EXTEND_3) * ParaX - point.X;
					double num4 = oFunc.StringToDouble(surveyDetail.EXTEND_4) * ParaX - point.Y;
					rectangle.Width = num3 - num + 1.0;
					rectangle.Height = num4 - num2 + 1.0;
					Canvas.SetLeft(rectangle, num);
					Canvas.SetTop(rectangle, num2);
				}
				new SurveyBiz();
				string mySurveyId = MySurveyId;
				int surveySequence = SurveyHelper.SurveySequence;
				((SurveyBiz)/*Error near IL_013c: Stack underflow*/).ClearPageAnswer((string)/*Error near IL_013c: Stack underflow*/, (int)/*Error near IL_013c: Stack underflow*/);
				PageLoaded = false;
			}
		}

		private void _003F28_003F()
		{
			Canvas canvas = this.canvas;
			int num = 0;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				double num2 = oFunc.StringToDouble(qDetail.EXTEND_1);
				double num3 = oFunc.StringToDouble(qDetail.EXTEND_2);
				double num4 = oFunc.StringToDouble(qDetail.EXTEND_3);
				double num5 = oFunc.StringToDouble(qDetail.EXTEND_4);
				if (num4 - num2 > 0.0 && num5 - num3 > 0.0)
				{
					bool flag = false;
					if (qDetail.IS_OTHER == 1 || qDetail.IS_OTHER == 3 || ((qDetail.IS_OTHER == 11) | (qDetail.IS_OTHER == 5)) || qDetail.IS_OTHER == 13 || qDetail.IS_OTHER == 14)
					{
						flag = true;
					}
					if (flag)
					{
						ExistTextFill = true;
					}
					Rectangle rectangle = new Rectangle();
					rectangle.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
					rectangle.Fill = Brushes.White;
					rectangle.Stroke = Brushes.LightBlue;
					rectangle.StrokeThickness = 1.0;
					rectangle.Opacity = UnSelImgOpacity;
					rectangle.Tag = num;
					rectangle.MouseLeftButtonUp += _003F29_003F;
					canvas.Children.Add(rectangle);
					listBtnNormal.Add(rectangle);
				}
				num++;
			}
		}

		private void _003F29_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F = null)
		{
			//IL_0080: Incompatible stack heights: 0 vs 1
			//IL_01dc: Incompatible stack heights: 0 vs 1
			//IL_0232: Incompatible stack heights: 0 vs 1
			//IL_0238: Incompatible stack heights: 0 vs 1
			int index = (int)((Rectangle)_003F347_003F).Tag;
			int iS_OTHER = oQuestion.QDetails[index].IS_OTHER;
			string text = oQuestion.QDetails[index].CODE;
			if (oFunc.LEFT(oQuestion.QDetails[index].CODE_TEXT, 1) == _003F487_003F._003F488_003F("\""))
			{
				text = oFunc.MID(oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
			}
			int num = 0;
			if (iS_OTHER == 1 || iS_OTHER == 3 || iS_OTHER == 5 || iS_OTHER == 11 || iS_OTHER == 13 || iS_OTHER == 14)
			{
				num = 1;
			}
			int num2 = 0;
			if (((UIElement)/*Error near IL_011d: Stack underflow*/).Opacity == SelImgOpacity)
			{
				num2 = 1;
			}
			if (num2 == 0)
			{
				oQuestion.SelectedCode = text;
				foreach (Rectangle item in listBtnNormal)
				{
					int index2 = (int)item.Tag;
					string a = oQuestion.QDetails[index2].CODE;
					if (oFunc.LEFT(oQuestion.QDetails[index2].CODE_TEXT, 1) == _003F487_003F._003F488_003F("\""))
					{
						a = oFunc.MID(oQuestion.QDetails[index2].CODE_TEXT, 1, -9999);
					}
					if (!(a == text))
					{
						double unSelImgOpacity = UnSelImgOpacity;
					}
					else
					{
						double selImgOpacity = SelImgOpacity;
					}
					((UIElement)/*Error near IL_023d: Stack underflow*/).Opacity = (double)/*Error near IL_023d: Stack underflow*/;
				}
				if (num == 0)
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
			if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
			{
				if (txtFill.IsEnabled)
				{
					txtFill.Focus();
				}
				else
				{
					_003F58_003F(this, _003F348_003F);
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

		private bool _003F87_003F()
		{
			//IL_00b0: Incompatible stack heights: 0 vs 2
			//IL_00ca: Incompatible stack heights: 0 vs 1
			//IL_00e1: Incompatible stack heights: 0 vs 4
			//IL_00f7: Incompatible stack heights: 0 vs 2
			//IL_010b: Incompatible stack heights: 0 vs 1
			//IL_0110: Incompatible stack heights: 1 vs 0
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				string msgNotSelected = SurveyMsg.MsgNotSelected;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0027: Stack underflow*/, (string)/*Error near IL_0027: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (txtFill.IsEnabled)
			{
				txtFill.Text.Trim();
				string b = _003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_0049: Stack underflow*/ == b)
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_0053: Stack underflow*/, (string)/*Error near IL_0053: Stack underflow*/, (MessageBoxButton)/*Error near IL_0053: Stack underflow*/, (MessageBoxImage)/*Error near IL_0053: Stack underflow*/);
					txtFill.Focus();
					return true;
				}
			}
			if (txtFill.IsEnabled)
			{
				QSingle oQuestion2 = oQuestion;
				TextBox txtFill2 = txtFill;
				string fillText;
				if (((UIElement)/*Error near IL_0077: Stack underflow*/).IsEnabled)
				{
					fillText = txtFill.Text.Trim();
				}
				else
				{
					_003F487_003F._003F488_003F("");
				}
				((QSingle)/*Error near IL_0096: Stack underflow*/).FillText = fillText;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.SelectedCode;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				vEAnswer2.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F89_003F()
		{
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00e7: Incompatible stack heights: 0 vs 1
			//IL_00f8: Incompatible stack heights: 0 vs 2
			//IL_010e: Incompatible stack heights: 0 vs 3
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				((PicSingle)/*Error near IL_0040: Stack underflow*/).btnNav.Content = btnNav_Content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav2 = btnNav;
					string content = ((PicSingle)/*Error near IL_008b: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_0090: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00a8: Stack underflow*/, (string)/*Error near IL_00a8: Stack underflow*/, (MessageBoxButton)/*Error near IL_00a8: Stack underflow*/, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00d7:
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

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_009e: Incompatible stack heights: 0 vs 1
			//IL_00a3: Incompatible stack heights: 1 vs 0
			//IL_00ae: Incompatible stack heights: 0 vs 1
			//IL_00b3: Incompatible stack heights: 1 vs 0
			//IL_00be: Incompatible stack heights: 0 vs 1
			//IL_00c3: Incompatible stack heights: 1 vs 0
			//IL_00ce: Incompatible stack heights: 0 vs 1
			//IL_00d3: Incompatible stack heights: 1 vs 0
			//IL_00de: Incompatible stack heights: 0 vs 1
			//IL_00e3: Incompatible stack heights: 1 vs 0
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
			//IL_0031: Incompatible stack heights: 0 vs 1
			//IL_0036: Incompatible stack heights: 1 vs 0
			//IL_003b: Incompatible stack heights: 0 vs 2
			//IL_0041: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_0046;
			}
			goto IL_004c;
			IL_0046:
			int length = _003F362_003F.Length;
			goto IL_004c;
			IL_004c:
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, (int)/*Error near IL_0051: Stack underflow*/);
			IL_0021:
			goto IL_0046;
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_0058: Incompatible stack heights: 0 vs 1
			//IL_006f: Incompatible stack heights: 0 vs 1
			//IL_0074: Incompatible stack heights: 1 vs 0
			//IL_0079: Incompatible stack heights: 0 vs 2
			//IL_007f: Incompatible stack heights: 0 vs 1
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
			//IL_0037: Incompatible stack heights: 0 vs 1
			//IL_003c: Incompatible stack heights: 1 vs 0
			//IL_0041: Incompatible stack heights: 0 vs 1
			//IL_0047: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_004c;
			}
			int startIndex = ((string)/*Error near IL_0020: Stack underflow*/).Length - num;
			goto IL_004d;
			IL_004c:
			startIndex = 0;
			goto IL_004d;
			IL_004d:
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
			IL_0027:
			goto IL_004c;
		}

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0059:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0065:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0071:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_007d:
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a7e᭤ᱯ\u1d78ṣὧ\u206fⅫ≣⌫⑼╢♯❭"), UriKind.Relative);
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
				((PicSingle)_003F350_003F).Loaded += _003F80_003F;
				((PicSingle)_003F350_003F).LayoutUpdated += _003F99_003F;
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
