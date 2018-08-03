using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gssy.Capi.View
{
	public class PicDefine : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMultiple oQuestion = new QMultiple();

		private List<Button> listBtnNormal = new List<Button>();

		private List<Rectangle> listRtgNormal = new List<Rectangle>();

		private int CurrentSelect = -1;

		private double UnSelImgOpacity = 0.3;

		private double SelImgOpacity = 0.8;

		private Style SelBtnStyle;

		private Style UnSelBtnStyle;

		private double DelBtnOpacity = 0.2;

		private double NormalBtnOpacity = 1.0;

		private Canvas CanvasRtg;

		private Image Picture;

		private double BmpWidth;

		private double BmpHeight;

		private double ParaX = 1.0;

		private string MouseStatus = _003F487_003F._003F488_003F("");

		private bool ChangeLeft;

		private bool ChangeRight;

		private bool ChangeTop;

		private bool ChangeBottom;

		private Point p_MouseLeftDown;

		private Point p_Rectangle;

		private double Width_MouseLeftDown;

		private double Height_MouseLeftDown;

		private bool PageLoaded;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal ScrollViewer scrollmain;

		internal Grid g;

		internal ScrollViewer scrollcode;

		internal WrapPanel WrapCode;

		internal WrapPanel AddCode;

		internal TextBox Code;

		internal TextBox CodeText;

		internal Button btnMoveTop;

		internal Button btnMoveBottom;

		internal Button btnMoveUp;

		internal Button btnMoveDown;

		internal Button btnEdit;

		internal Button btnCancel;

		internal Button btnNav;

		internal Button btnAdd;

		internal Button btnDel;

		private bool _contentLoaded;

		public PicDefine()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			btnNav.Content = btnNav_Content;
			SelBtnStyle = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			UnSelBtnStyle = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
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
			qUESTION_TITLE = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"))[0];
			if (qUESTION_TITLE == _003F487_003F._003F488_003F(""))
			{
				txtQuestionTitle.Height = 0.0;
				txtQuestionTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			string text = _003F487_003F._003F488_003F("");
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
					foreach (SurveyDetail qCircleDetail in oQuestion.QCircleDetails)
					{
						if (qCircleDetail.CODE == text2)
						{
							text = qCircleDetail.EXTEND_1;
							break;
						}
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
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				image.SetValue(Grid.ColumnProperty, 0);
				image.SetValue(Grid.RowProperty, 0);
				image.HorizontalAlignment = HorizontalAlignment.Left;
				image.VerticalAlignment = VerticalAlignment.Top;
				BitmapImage bitmapImage = new BitmapImage();
				try
				{
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					image.Width = (double)bitmapImage.PixelWidth;
					image.Height = (double)bitmapImage.PixelHeight;
					image.MouseLeftButtonUp += _003F124_003F;
					image.MouseMove += _003F123_003F;
					g.Children.Add(image);
					Picture = image;
					BmpHeight = image.Height;
					BmpWidth = image.Width;
				}
				catch (Exception)
				{
				}
				CanvasRtg = new Canvas();
				g.Children.Add(CanvasRtg);
			}
			_003F28_003F();
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_015d: Expected O, but got Unknown
			//IL_018f: Incompatible stack heights: 0 vs 1
			//IL_0199: Incompatible stack heights: 0 vs 1
			//IL_01ed: Incompatible stack heights: 0 vs 1
			//IL_01fb: Incompatible stack heights: 0 vs 1
			//IL_0200: Incompatible stack heights: 1 vs 0
			//IL_0205: Incompatible stack heights: 0 vs 1
			//IL_0213: Incompatible stack heights: 0 vs 1
			//IL_0218: Incompatible stack heights: 1 vs 0
			if (PageLoaded)
			{
				Canvas canvasRtg = CanvasRtg;
				Image picture = Picture;
				Point point = ((UIElement)/*Error near IL_001f: Stack underflow*/).TranslatePoint(default(Point), (UIElement)picture);
				ParaX = Picture.ActualHeight / BmpHeight;
				for (int i = 0; i < oQuestion.QDetails.Count; i++)
				{
					Rectangle rectangle = listRtgNormal[i];
					SurveyDetail surveyDetail = oQuestion.QDetails[i];
					double num = oFunc.StringToDouble(surveyDetail.EXTEND_1) - point.X;
					double num2 = oFunc.StringToDouble(surveyDetail.EXTEND_2);
					double num3 = oFunc.StringToDouble(surveyDetail.EXTEND_3) - point.X;
					double num4 = oFunc.StringToDouble(surveyDetail.EXTEND_4);
					if (num < 0.0)
					{
						num = 0.0;
					}
					if (num2 < 0.0)
					{
						num2 = 0.0;
					}
					if (num3 < 0.0)
					{
						num3 = 0.0;
					}
					if (num4 < 0.0)
					{
						num4 = 0.0;
					}
					double num5 = num3 - num + 1.0;
					double num6 = num4 - num2 + 1.0;
					/*Error near IL_011b: Stack underflow*/;
					double width;
					if (num5 > 1.0)
					{
						width = num5;
					}
					((FrameworkElement)/*Error near IL_0138: Stack underflow*/).Width = width;
					/*Error near IL_0138: Stack underflow*/;
					double height;
					if (num6 > 1.0)
					{
						height = num6;
					}
					((FrameworkElement)/*Error near IL_0155: Stack underflow*/).Height = height;
					Canvas.SetLeft((UIElement)/*Error near IL_0155: Stack underflow*/, num);
					Canvas.SetTop((UIElement)/*Error near IL_0164: Stack underflow*/, num2);
				}
				PageLoaded = false;
			}
		}

		private void _003F28_003F()
		{
			WrapPanel wrapCode = WrapCode;
			Canvas canvasRtg = CanvasRtg;
			int num = 0;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				double num2 = oFunc.StringToDouble(qDetail.EXTEND_1);
				double num3 = oFunc.StringToDouble(qDetail.EXTEND_2);
				double num4 = oFunc.StringToDouble(qDetail.EXTEND_3);
				double num5 = oFunc.StringToDouble(qDetail.EXTEND_4);
				if (num2 < 0.0)
				{
					num2 = 0.0;
				}
				if (num3 < 0.0)
				{
					num3 = 0.0;
				}
				if (num4 < 0.0)
				{
					num4 = 0.0;
				}
				if (num5 < 0.0)
				{
					num5 = 0.0;
				}
				double num6 = num5 - num3 + 1.0;
				if (num6 <= 1.0)
				{
					num6 = 50.0;
				}
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Tag = num;
				button.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				button.Style = UnSelBtnStyle;
				button.Click += _003F29_003F;
				button.FontSize = 16.0;
				button.MinWidth = 210.0;
				button.MinHeight = 30.0;
				wrapCode.Children.Add(button);
				listBtnNormal.Add(button);
				Rectangle rectangle = new Rectangle();
				rectangle.Name = _003F487_003F._003F488_003F("pŞ") + qDetail.CODE;
				rectangle.Tag = num;
				rectangle.Fill = Brushes.White;
				rectangle.Stroke = Brushes.Gray;
				rectangle.StrokeThickness = 1.0;
				rectangle.Opacity = UnSelImgOpacity;
				rectangle.MouseLeftButtonDown += _003F122_003F;
				rectangle.MouseLeftButtonUp += _003F124_003F;
				rectangle.MouseMove += _003F123_003F;
				Canvas.SetLeft(rectangle, num2);
				Canvas.SetTop(rectangle, num3);
				canvasRtg.Children.Add(rectangle);
				listRtgNormal.Add(rectangle);
				num++;
			}
		}

		private void _003F122_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			//IL_01f1: Incompatible stack heights: 0 vs 2
			//IL_01f6: Incompatible stack heights: 0 vs 1
			//IL_0204: Incompatible stack heights: 0 vs 1
			//IL_0209: Incompatible stack heights: 1 vs 0
			//IL_020e: Incompatible stack heights: 0 vs 1
			//IL_021c: Incompatible stack heights: 0 vs 1
			//IL_0221: Incompatible stack heights: 1 vs 0
			//IL_0231: Incompatible stack heights: 0 vs 1
			//IL_023c: Incompatible stack heights: 0 vs 1
			//IL_024c: Incompatible stack heights: 0 vs 1
			//IL_0272: Incompatible stack heights: 0 vs 2
			//IL_0282: Incompatible stack heights: 0 vs 1
			Rectangle rectangle = (Rectangle)_003F347_003F;
			CurrentSelect = (int)rectangle.Tag;
			if (!(MouseStatus == _003F487_003F._003F488_003F("")))
			{
				return;
			}
			Image picture = ((PicDefine)/*Error near IL_0037: Stack underflow*/).Picture;
			Point position = ((MouseEventArgs)/*Error near IL_003c: Stack underflow*/).GetPosition((IInputElement)picture);
			Point position2 = _003F348_003F.GetPosition(rectangle);
			p_Rectangle = rectangle.TranslatePoint(default(Point), Picture);
			p_MouseLeftDown = position;
			Width_MouseLeftDown = rectangle.Width;
			Height_MouseLeftDown = rectangle.Height;
			ChangeLeft = (position2.X <= 5.0);
			if (rectangle.Width - position2.X <= 5.0)
			{
				bool flag = !ChangeLeft;
			}
			((PicDefine)/*Error near IL_00c2: Stack underflow*/).ChangeRight = false;
			ChangeTop = (position2.Y <= 5.0);
			if (rectangle.Height - position2.Y <= 5.0)
			{
				bool flag2 = !ChangeTop;
			}
			((PicDefine)/*Error near IL_0105: Stack underflow*/).ChangeBottom = false;
			if (!ChangeLeft)
			{
				bool changeRight = ChangeRight;
				if ((int)/*Error near IL_0236: Stack underflow*/ == 0 && !((PicDefine)/*Error near IL_011a: Stack underflow*/).ChangeTop)
				{
					bool changeBottom = ChangeBottom;
					if ((int)/*Error near IL_0251: Stack underflow*/ == 0)
					{
						MouseStatus = _003F487_003F._003F488_003F("IŬɴ\u0364");
						rectangle.StrokeThickness = 1.0;
						goto IL_0167;
					}
				}
			}
			MouseStatus = _003F487_003F._003F488_003F("Eŭɥ\u036dѥդ");
			rectangle.StrokeThickness = 5.0;
			goto IL_0167;
			IL_0167:
			for (int i = 0; i < listBtnNormal.Count(); i++)
			{
				Button button = listBtnNormal[i];
				Rectangle element = listRtgNormal[i];
				button.Style = UnSelBtnStyle;
				if (button.Opacity == NormalBtnOpacity)
				{
					double unSelImgOpacity = UnSelImgOpacity;
					((UIElement)/*Error near IL_01ac: Stack underflow*/).Opacity = (double)/*Error near IL_01ac: Stack underflow*/;
					Panel.SetZIndex(element, i + 1);
				}
			}
			List<Button> listBtnNormal2 = listBtnNormal;
			int currentSelect = CurrentSelect;
			((List<Button>)/*Error near IL_01da: Stack underflow*/)[currentSelect].Style = SelBtnStyle;
			listBtnNormal[CurrentSelect].BringIntoView();
			listRtgNormal[CurrentSelect].Opacity = SelImgOpacity;
			Panel.SetZIndex(listRtgNormal[CurrentSelect], listBtnNormal.Count + 2);
			listRtgNormal[CurrentSelect].BringIntoView();
		}

		private void _003F123_003F(object _003F347_003F, MouseEventArgs _003F348_003F)
		{
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected F8, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected F8, but got Unknown
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Expected F8, but got Unknown
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Expected F8, but got Unknown
			//IL_0241: Unknown result type (might be due to invalid IL or missing references)
			//IL_0246: Expected F8, but got Unknown
			//IL_025a: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Expected F8, but got Unknown
			//IL_0275: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Expected F8, but got Unknown
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Expected F8, but got Unknown
			//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d5: Expected F8, but got Unknown
			//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Expected F8, but got Unknown
			//IL_030c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Expected F8, but got Unknown
			//IL_0396: Incompatible stack heights: 0 vs 1
			//IL_039b: Invalid comparison between Unknown and I4
			//IL_03b0: Incompatible stack heights: 0 vs 2
			//IL_03c6: Incompatible stack heights: 0 vs 2
			//IL_03dd: Incompatible stack heights: 0 vs 2
			//IL_040c: Incompatible stack heights: 0 vs 2
			//IL_0425: Incompatible stack heights: 0 vs 2
			//IL_0446: Incompatible stack heights: 0 vs 3
			//IL_045b: Incompatible stack heights: 0 vs 1
			//IL_046e: Incompatible stack heights: 0 vs 1
			//IL_0481: Incompatible stack heights: 0 vs 2
			//IL_0497: Incompatible stack heights: 0 vs 2
			//IL_04bc: Incompatible stack heights: 0 vs 3
			//IL_04c7: Incompatible stack heights: 0 vs 1
			//IL_04d2: Incompatible stack heights: 0 vs 1
			//IL_04ea: Incompatible stack heights: 0 vs 1
			//IL_04ef: Invalid comparison between Unknown and F8
			//IL_04fd: Incompatible stack heights: 0 vs 2
			//IL_0509: Incompatible stack heights: 0 vs 2
			//IL_0526: Incompatible stack heights: 0 vs 3
			//IL_0539: Incompatible stack heights: 0 vs 3
			//IL_054f: Incompatible stack heights: 0 vs 2
			//IL_0568: Incompatible stack heights: 0 vs 2
			//IL_057b: Incompatible stack heights: 0 vs 2
			//IL_0593: Incompatible stack heights: 0 vs 3
			//IL_05b9: Incompatible stack heights: 0 vs 2
			//IL_05c6: Incompatible stack heights: 0 vs 2
			//IL_05df: Incompatible stack heights: 0 vs 1
			//IL_05ec: Incompatible stack heights: 0 vs 2
			//IL_05fd: Incompatible stack heights: 0 vs 1
			//IL_0602: Invalid comparison between Unknown and F8
			//IL_0613: Incompatible stack heights: 0 vs 2
			//IL_0620: Incompatible stack heights: 0 vs 2
			//IL_0634: Incompatible stack heights: 0 vs 2
			//IL_0644: Incompatible stack heights: 0 vs 1
			double num = 15.0;
			double num2 = 15.0;
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				int currentSelect2 = CurrentSelect;
				if ((int)/*Error near IL_039b: Stack underflow*/ > -1)
				{
					string mouseStatus = MouseStatus;
					_003F487_003F._003F488_003F("");
					if ((string)/*Error near IL_002a: Stack underflow*/ != (string)/*Error near IL_002a: Stack underflow*/)
					{
						List<Rectangle> listRtgNormal2 = listRtgNormal;
						int currentSelect3 = CurrentSelect;
						Rectangle rectangle = ((List<Rectangle>)/*Error near IL_0034: Stack underflow*/)[(int)/*Error near IL_0034: Stack underflow*/];
						Point position = _003F348_003F.GetPosition(Picture);
						double num3 = position.X - p_MouseLeftDown.X;
						double num4 = position.Y - p_MouseLeftDown.Y;
						if (MouseStatus == _003F487_003F._003F488_003F("IŬɴ\u0364"))
						{
							double x2 = p_Rectangle.X;
							double num5 = (double)(/*Error near IL_0087: Stack underflow*/ + /*Error near IL_0087: Stack underflow*/);
							double num6 = p_Rectangle.Y + num4;
							if (!(num5 < 0.0))
							{
								if (!(num5 + rectangle.Width > Picture.Width))
								{
									Canvas.SetLeft(rectangle, p_Rectangle.X + num3);
								}
								else
								{
									Image picture = Picture;
									Canvas.SetLeft(length: ((FrameworkElement)/*Error near IL_00cc: Stack underflow*/).Width - rectangle.Width, element: (UIElement)/*Error near IL_00d8: Stack underflow*/);
								}
							}
							else
							{
								Canvas.SetLeft(rectangle, 0.0);
							}
							if (num6 < 0.0)
							{
								Canvas.SetTop((UIElement)/*Error near IL_0106: Stack underflow*/, (double)/*Error near IL_0106: Stack underflow*/);
							}
							else if (num6 + rectangle.Height > Picture.Height)
							{
								double height5 = Picture.Height;
								double height6 = rectangle.Height;
								_003F val = /*Error near IL_0125: Stack underflow*/- /*Error near IL_0125: Stack underflow*/;
								Canvas.SetTop((UIElement)/*Error near IL_012a: Stack underflow*/, (double)val);
							}
							else
							{
								Canvas.SetTop(rectangle, p_Rectangle.Y + num4);
							}
							rectangle.StrokeThickness = 1.0;
						}
						else
						{
							rectangle.StrokeThickness = 5.0;
							if (ChangeLeft)
							{
								ref Point reference = ref p_Rectangle;
								double num7 = ((Point)/*Error near IL_0172: Stack underflow*/).X + Width_MouseLeftDown;
								if (position.X < num7)
								{
									double num9 = Width_MouseLeftDown - num3;
									if (Math.Abs((double)/*Error near IL_018e: Stack underflow*/) > num)
									{
										double x3 = position.X;
										if (/*Error near IL_0486: Stack underflow*/ < /*Error near IL_0486: Stack underflow*/)
										{
											double x4 = p_Rectangle.X;
											_003F val2 = /*Error near IL_019c: Stack underflow*/+ num3;
											Canvas.SetLeft((UIElement)/*Error near IL_01a1: Stack underflow*/, (double)val2);
										}
										if (position.X > num7)
										{
											Canvas.SetLeft(rectangle, num7);
										}
										if (position.X < num7)
										{
											double width_MouseLeftDown = Width_MouseLeftDown;
											double width = Math.Abs((double)(/*Error near IL_01be: Stack underflow*/ - /*Error near IL_01be: Stack underflow*/));
											((FrameworkElement)/*Error near IL_01c8: Stack underflow*/).Width = width;
										}
										if (position.X > num7)
										{
											double width2 = Math.Abs(num3 - Width_MouseLeftDown);
											((FrameworkElement)/*Error near IL_01e9: Stack underflow*/).Width = width2;
										}
									}
								}
							}
							if (ChangeRight)
							{
								double x = ((PicDefine)/*Error near IL_01f9: Stack underflow*/).p_Rectangle.X;
								if (position.X > x)
								{
									Math.Abs(num3 + Width_MouseLeftDown);
									if (!((double)/*Error near IL_04ef: Stack underflow*/ <= num))
									{
										double x5 = position.X;
										if (/*Error near IL_0502: Stack underflow*/ < /*Error near IL_0502: Stack underflow*/)
										{
											Canvas.SetLeft(length: ((PicDefine)/*Error near IL_021e: Stack underflow*/).p_Rectangle.X + num3 + Width_MouseLeftDown, element: (UIElement)/*Error near IL_0232: Stack underflow*/);
										}
										if (position.X < x)
										{
											double x6 = position.X;
											double x7 = p_Rectangle.X;
											double width3 = Math.Abs((double)(/*Error near IL_0241: Stack underflow*/ - /*Error near IL_0241: Stack underflow*/));
											((FrameworkElement)/*Error near IL_024b: Stack underflow*/).Width = width3;
										}
										if (position.X > x)
										{
											double width_MouseLeftDown2 = Width_MouseLeftDown;
											double width4 = Math.Abs((double)(/*Error near IL_025a: Stack underflow*/ + /*Error near IL_025a: Stack underflow*/));
											((FrameworkElement)/*Error near IL_0264: Stack underflow*/).Width = width4;
										}
									}
								}
							}
							if (ChangeTop)
							{
								double y2 = p_Rectangle.Y;
								double height_MouseLeftDown = ((PicDefine)/*Error near IL_0274: Stack underflow*/).Height_MouseLeftDown;
								double num8 = /*Error near IL_0275: Stack underflow*/+ height_MouseLeftDown;
								if (position.Y < num8)
								{
									Math.Abs(Height_MouseLeftDown - num4);
									if (/*Error near IL_056d: Stack underflow*/ > /*Error near IL_056d: Stack underflow*/)
									{
										double y3 = position.Y;
										if (/*Error near IL_0580: Stack underflow*/ < /*Error near IL_0580: Stack underflow*/)
										{
											double y4 = p_Rectangle.Y;
											_003F val3 = /*Error near IL_0290: Stack underflow*/+ /*Error near IL_0290: Stack underflow*/;
											Canvas.SetTop((UIElement)/*Error near IL_0295: Stack underflow*/, (double)val3);
										}
										if (position.Y > num8)
										{
											Canvas.SetTop(rectangle, num8);
										}
										if (position.Y < num8)
										{
											double num10 = Height_MouseLeftDown - num4;
											double height = Math.Abs((double)/*Error near IL_02b6: Stack underflow*/);
											((FrameworkElement)/*Error near IL_02bb: Stack underflow*/).Height = height;
										}
										if (position.Y > num8)
										{
											double height_MouseLeftDown2 = Height_MouseLeftDown;
											double height2 = Math.Abs(/*Error near IL_02d0: Stack underflow*/ - height_MouseLeftDown2);
											((FrameworkElement)/*Error near IL_02da: Stack underflow*/).Height = height2;
										}
									}
								}
							}
							if (ChangeBottom)
							{
								double y = p_Rectangle.Y;
								if (((Point)/*Error near IL_02ea: Stack underflow*/).Y > y)
								{
									double height_MouseLeftDown3 = ((PicDefine)/*Error near IL_02f6: Stack underflow*/).Height_MouseLeftDown;
									if (Math.Abs(/*Error near IL_02f7: Stack underflow*/ + height_MouseLeftDown3) > num2)
									{
										double y5 = position.Y;
										if (!((double)/*Error near IL_0602: Stack underflow*/ >= y))
										{
											double y6 = p_Rectangle.Y;
											_003F val4 = /*Error near IL_030c: Stack underflow*/+ num4 + Height_MouseLeftDown;
											Canvas.SetTop((UIElement)/*Error near IL_0318: Stack underflow*/, (double)val4);
										}
										if (position.Y < y)
										{
											double height3 = Math.Abs(((Point)/*Error near IL_032b: Stack underflow*/).Y - p_Rectangle.Y);
											((FrameworkElement)/*Error near IL_0341: Stack underflow*/).Height = height3;
										}
										if (position.Y > y)
										{
											double num11 = num4 + Height_MouseLeftDown;
											double height4 = Math.Abs((double)/*Error near IL_0354: Stack underflow*/);
											((FrameworkElement)/*Error near IL_0359: Stack underflow*/).Height = height4;
										}
									}
								}
							}
						}
						return;
					}
				}
			}
			MouseStatus = _003F487_003F._003F488_003F("");
			if (CurrentSelect > -1)
			{
				List<Rectangle> listRtgNormal3 = listRtgNormal;
				int currentSelect = CurrentSelect;
				((List<Rectangle>)/*Error near IL_0381: Stack underflow*/)[currentSelect].StrokeThickness = 1.0;
			}
		}

		private void _003F124_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			//IL_0022: Incompatible stack heights: 0 vs 1
			if (CurrentSelect > -1)
			{
				List<Rectangle> listRtgNormal2 = listRtgNormal;
				int currentSelect = CurrentSelect;
				((List<Rectangle>)/*Error near IL_002c: Stack underflow*/)[currentSelect].StrokeThickness = 1.0;
			}
			MouseStatus = _003F487_003F._003F488_003F("");
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00e3: Incompatible stack heights: 0 vs 2
			//IL_00f9: Incompatible stack heights: 0 vs 2
			//IL_0113: Incompatible stack heights: 0 vs 2
			Button button = (Button)_003F347_003F;
			CurrentSelect = (int)button.Tag;
			for (int i = 0; i < listBtnNormal.Count(); i++)
			{
				Button button2 = listBtnNormal[i];
				Rectangle element = listRtgNormal[i];
				button2.Style = UnSelBtnStyle;
				if (button2.Opacity == NormalBtnOpacity)
				{
					double unSelImgOpacity = ((PicDefine)/*Error near IL_0059: Stack underflow*/).UnSelImgOpacity;
					((UIElement)/*Error near IL_005e: Stack underflow*/).Opacity = unSelImgOpacity;
					Panel.SetZIndex(element, i + 1);
				}
			}
			List<Button> listBtnNormal2 = listBtnNormal;
			int currentSelect = CurrentSelect;
			if (((List<Button>)/*Error near IL_0081: Stack underflow*/)[(int)/*Error near IL_0081: Stack underflow*/].Opacity == NormalBtnOpacity)
			{
				Button btnDel2 = btnDel;
				_003F487_003F._003F488_003F("判靧鈋魸");
				((ContentControl)/*Error near IL_0096: Stack underflow*/).Content = (object)/*Error near IL_0096: Stack underflow*/;
			}
			else
			{
				btnDel.Content = _003F487_003F._003F488_003F("恦堎鈋魸");
			}
			listBtnNormal[CurrentSelect].Style = SelBtnStyle;
			listRtgNormal[CurrentSelect].Opacity = SelImgOpacity;
			Panel.SetZIndex(listRtgNormal[CurrentSelect], listBtnNormal.Count + 2);
			listRtgNormal[CurrentSelect].BringIntoView();
		}

		private void _003F125_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0047: Incompatible stack heights: 0 vs 1
			//IL_005d: Incompatible stack heights: 0 vs 2
			if (CurrentSelect > -1)
			{
				List<Button> listBtnNormal2 = listBtnNormal;
				int currentSelect = CurrentSelect;
				if (((List<Button>)/*Error near IL_0017: Stack underflow*/)[currentSelect].Opacity == NormalBtnOpacity)
				{
					List<Button> listBtnNormal3 = listBtnNormal;
					int currentSelect2 = CurrentSelect;
					((List<Button>)/*Error near IL_002c: Stack underflow*/)[(int)/*Error near IL_002c: Stack underflow*/].Opacity = DelBtnOpacity;
					listRtgNormal[CurrentSelect].Visibility = Visibility.Collapsed;
					btnDel.Content = _003F487_003F._003F488_003F("恦堎鈋魸");
				}
				else
				{
					listBtnNormal[CurrentSelect].Opacity = NormalBtnOpacity;
					listRtgNormal[CurrentSelect].Visibility = Visibility.Visible;
					listRtgNormal[CurrentSelect].Opacity = SelImgOpacity;
					Panel.SetZIndex(listRtgNormal[CurrentSelect], listBtnNormal.Count + 2);
					btnDel.Content = SurveyMsg.MsgOptionDelete;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F126_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (btnAdd.Content.ToString() == SurveyMsg.MsgOptionAdd)
			{
				btnAdd.Content = SurveyMsg.MsgOptionSave;
				AddCode.Visibility = Visibility.Visible;
				btnCancel.Visibility = Visibility.Visible;
				btnEdit.Visibility = Visibility.Hidden;
				Code.Focus();
			}
			else if (Code.Text.Trim() == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillCode, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else if (CodeText.Text.Trim() == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				foreach (SurveyDetail qDetail in oQuestion.QDetails)
				{
					if (qDetail.CODE == Code.Text.Trim())
					{
						MessageBox.Show(SurveyMsg.MsgFillCodeRepeat, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
				}
				btnAdd.Content = SurveyMsg.MsgOptionAdd;
				AddCode.Visibility = Visibility.Collapsed;
				btnCancel.Visibility = Visibility.Hidden;
				btnEdit.Visibility = Visibility.Visible;
				if (CurrentSelect > -1)
				{
					listRtgNormal[CurrentSelect].Opacity = UnSelImgOpacity;
					listBtnNormal[CurrentSelect].Style = UnSelBtnStyle;
				}
				CurrentSelect = listRtgNormal.Count();
				SurveyDetail surveyDetail = new SurveyDetail();
				surveyDetail.DETAIL_ID = oQuestion.QDefine.DETAIL_ID;
				surveyDetail.CODE = Code.Text;
				surveyDetail.CODE_TEXT = CodeText.Text;
				surveyDetail.INNER_ORDER = CurrentSelect + 1;
				surveyDetail.RANDOM_BASE = CurrentSelect + 1;
				oQuestion.QDetails.Add(surveyDetail);
				Rectangle rectangle = new Rectangle();
				rectangle.Name = _003F487_003F._003F488_003F("pŞ") + surveyDetail.CODE;
				rectangle.Tag = CurrentSelect;
				rectangle.Fill = Brushes.White;
				rectangle.Stroke = Brushes.Gray;
				rectangle.StrokeThickness = 1.0;
				rectangle.Opacity = SelImgOpacity;
				rectangle.Width = 100.0;
				rectangle.Height = 50.0;
				rectangle.MouseLeftButtonDown += _003F122_003F;
				rectangle.MouseLeftButtonUp += _003F124_003F;
				rectangle.MouseMove += _003F123_003F;
				Canvas.SetLeft(rectangle, 0.0);
				Canvas.SetTop(rectangle, 0.0);
				CanvasRtg.Children.Add(rectangle);
				listRtgNormal.Add(rectangle);
				rectangle.BringIntoView();
				_003F133_003F(CurrentSelect);
			}
		}

		private void _003F127_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_015e: Incompatible stack heights: 0 vs 1
			//IL_018f: Incompatible stack heights: 0 vs 4
			//IL_01a6: Incompatible stack heights: 0 vs 4
			//IL_01cc: Incompatible stack heights: 0 vs 2
			//IL_01e0: Incompatible stack heights: 0 vs 2
			//IL_0200: Incompatible stack heights: 0 vs 1
			if (CurrentSelect > -1)
			{
				Button btnEdit2 = btnEdit;
				if (((ContentControl)/*Error near IL_0011: Stack underflow*/).Content.ToString() == SurveyMsg.MsgOptionModify)
				{
					btnEdit.Content = SurveyMsg.MsgOptionSaveModify;
					btnAdd.Visibility = Visibility.Hidden;
					Code.Text = oQuestion.QDetails[CurrentSelect].CODE;
					CodeText.Text = oQuestion.QDetails[CurrentSelect].CODE_TEXT;
					AddCode.Visibility = Visibility.Visible;
					btnCancel.Visibility = Visibility.Visible;
					Code.Focus();
				}
				else if (Code.Text.Trim() == _003F487_003F._003F488_003F(""))
				{
					string msgFirstFillCode = SurveyMsg.MsgFirstFillCode;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_00cb: Stack underflow*/, (string)/*Error near IL_00cb: Stack underflow*/, (MessageBoxButton)/*Error near IL_00cb: Stack underflow*/, (MessageBoxImage)/*Error near IL_00cb: Stack underflow*/);
				}
				else if (CodeText.Text.Trim() == _003F487_003F._003F488_003F(""))
				{
					string msgFirstFillText = SurveyMsg.MsgFirstFillText;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_00f6: Stack underflow*/, (string)/*Error near IL_00f6: Stack underflow*/, (MessageBoxButton)/*Error near IL_00f6: Stack underflow*/, (MessageBoxImage)/*Error near IL_00f6: Stack underflow*/);
				}
				else
				{
					for (int i = 0; i < listBtnNormal.Count(); i++)
					{
						if (i != CurrentSelect)
						{
							string cODE = oQuestion.QDetails[i].CODE;
							string b = ((PicDefine)/*Error near IL_0110: Stack underflow*/).Code.Text.Trim();
							if ((string)/*Error near IL_011f: Stack underflow*/ == b)
							{
								string msgFillCodeRepeat = SurveyMsg.MsgFillCodeRepeat;
								string msgCaption3 = SurveyMsg.MsgCaption;
								MessageBox.Show((string)/*Error near IL_012c: Stack underflow*/, (string)/*Error near IL_012c: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
								return;
							}
						}
					}
					btnEdit.Content = SurveyMsg.MsgOptionModify;
					Button btnAdd2 = btnAdd;
					((UIElement)/*Error near IL_0149: Stack underflow*/).Visibility = Visibility.Visible;
					AddCode.Visibility = Visibility.Collapsed;
					btnCancel.Visibility = Visibility.Hidden;
					oQuestion.QDetails[CurrentSelect].CODE = Code.Text.Trim();
					oQuestion.QDetails[CurrentSelect].CODE_TEXT = CodeText.Text.Trim();
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			btnEdit.Content = SurveyMsg.MsgOptionModify;
			btnAdd.Content = SurveyMsg.MsgOptionAdd;
			btnEdit.Visibility = Visibility.Visible;
			btnAdd.Visibility = Visibility.Visible;
			AddCode.Visibility = Visibility.Collapsed;
			btnCancel.Visibility = Visibility.Hidden;
		}

		private void _003F129_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c5: Incompatible stack heights: 0 vs 1
			//IL_00ca: Invalid comparison between Unknown and I4
			//IL_00d7: Incompatible stack heights: 0 vs 2
			//IL_00ee: Incompatible stack heights: 0 vs 3
			//IL_0104: Incompatible stack heights: 0 vs 1
			//IL_013a: Incompatible stack heights: 0 vs 1
			if (CurrentSelect > -1)
			{
				int currentSelect = CurrentSelect;
				if ((int)/*Error near IL_00ca: Stack underflow*/ > 0)
				{
					List<Rectangle> list = new List<Rectangle>();
					Rectangle item = ((PicDefine)/*Error near IL_0017: Stack underflow*/).listRtgNormal[CurrentSelect];
					((List<Rectangle>)/*Error near IL_0027: Stack underflow*/).Add(item);
					for (int i = 0; i < listRtgNormal.Count(); i++)
					{
						if (i != CurrentSelect)
						{
							List<Rectangle> listRtgNormal2 = listRtgNormal;
							Rectangle item2 = ((List<Rectangle>)/*Error near IL_003f: Stack underflow*/)[(int)/*Error near IL_003f: Stack underflow*/];
							((List<Rectangle>)/*Error near IL_0044: Stack underflow*/).Add(item2);
						}
					}
					listRtgNormal = list;
					new List<SurveyDetail>();
					List<SurveyDetail> list2 = (List<SurveyDetail>)/*Error near IL_005a: Stack underflow*/;
					list2.Add(oQuestion.QDetails[CurrentSelect]);
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						if (j != CurrentSelect)
						{
							list2.Add(oQuestion.QDetails[j]);
						}
					}
					QMultiple oQuestion2 = oQuestion;
					((QMultiple)/*Error near IL_00a9: Stack underflow*/).QDetails = list2;
					_003F133_003F(0);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F130_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c1: Incompatible stack heights: 0 vs 1
			//IL_00c6: Invalid comparison between Unknown and I4
			//IL_00ea: Incompatible stack heights: 0 vs 3
			//IL_00fc: Incompatible stack heights: 0 vs 3
			//IL_012e: Incompatible stack heights: 0 vs 2
			if (CurrentSelect > -1)
			{
				int currentSelect2 = CurrentSelect;
				int num = listBtnNormal.Count() - 1;
				if ((int)/*Error near IL_00c6: Stack underflow*/ < num)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < listRtgNormal.Count(); i++)
					{
						if (i != CurrentSelect)
						{
							List<Rectangle> listRtgNormal2 = listRtgNormal;
							Rectangle item = ((List<Rectangle>)/*Error near IL_0034: Stack underflow*/)[(int)/*Error near IL_0034: Stack underflow*/];
							((List<Rectangle>)/*Error near IL_0039: Stack underflow*/).Add(item);
						}
					}
					List<Rectangle> listRtgNormal3 = listRtgNormal;
					int currentSelect = ((PicDefine)/*Error near IL_0053: Stack underflow*/).CurrentSelect;
					Rectangle item2 = ((List<Rectangle>)/*Error near IL_0058: Stack underflow*/)[currentSelect];
					((List<Rectangle>)/*Error near IL_005d: Stack underflow*/).Add(item2);
					listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						if (j != CurrentSelect)
						{
							list2.Add(oQuestion.QDetails[j]);
						}
					}
					SurveyDetail item3 = ((PicDefine)/*Error near IL_009c: Stack underflow*/).oQuestion.QDetails[CurrentSelect];
					((List<SurveyDetail>)/*Error near IL_0133: Stack underflow*/).Add(item3);
					oQuestion.QDetails = list2;
					_003F133_003F(listBtnNormal.Count - 1);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F131_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected I4, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected I4, but got Unknown
			//IL_011f: Incompatible stack heights: 0 vs 1
			//IL_0124: Invalid comparison between Unknown and I4
			//IL_0148: Incompatible stack heights: 0 vs 3
			//IL_015f: Incompatible stack heights: 0 vs 3
			//IL_018e: Incompatible stack heights: 0 vs 2
			//IL_01ab: Incompatible stack heights: 0 vs 4
			//IL_01c0: Incompatible stack heights: 0 vs 1
			if (CurrentSelect > -1)
			{
				int currentSelect2 = CurrentSelect;
				if ((int)/*Error near IL_0124: Stack underflow*/ > 0)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < listRtgNormal.Count(); i++)
					{
						if (i == CurrentSelect - 1)
						{
							List<Rectangle> listRtgNormal2 = listRtgNormal;
							int currentSelect = ((PicDefine)/*Error near IL_002a: Stack underflow*/).CurrentSelect;
							Rectangle item = ((List<Rectangle>)/*Error near IL_002f: Stack underflow*/)[currentSelect];
							((List<Rectangle>)/*Error near IL_0034: Stack underflow*/).Add(item);
						}
						else if (i == CurrentSelect)
						{
							List<Rectangle> listRtgNormal3 = listRtgNormal;
							_003F val = /*Error near IL_0047: Stack underflow*/- 1;
							Rectangle item2 = ((List<Rectangle>)/*Error near IL_004c: Stack underflow*/)[(int)val];
							((List<Rectangle>)/*Error near IL_0051: Stack underflow*/).Add(item2);
						}
						else
						{
							list.Add(listRtgNormal[i]);
						}
					}
					listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						if (j == CurrentSelect - 1)
						{
							SurveyDetail item3 = ((PicDefine)/*Error near IL_0095: Stack underflow*/).oQuestion.QDetails[CurrentSelect];
							((List<SurveyDetail>)/*Error near IL_00aa: Stack underflow*/).Add(item3);
						}
						else if (j == CurrentSelect)
						{
							List<SurveyDetail> qDetail = oQuestion.QDetails;
							_003F val2 = /*Error near IL_00bc: Stack underflow*/- /*Error near IL_00bc: Stack underflow*/;
							SurveyDetail item4 = ((List<SurveyDetail>)/*Error near IL_00c1: Stack underflow*/)[(int)val2];
							((List<SurveyDetail>)/*Error near IL_00c6: Stack underflow*/).Add(item4);
						}
						else
						{
							list2.Add(oQuestion.QDetails[j]);
						}
					}
					QMultiple oQuestion2 = oQuestion;
					((QMultiple)/*Error near IL_0102: Stack underflow*/).QDetails = list2;
					_003F133_003F(CurrentSelect - 1);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F132_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected I4, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected I4, but got Unknown
			//IL_012b: Incompatible stack heights: 0 vs 1
			//IL_0130: Invalid comparison between Unknown and I4
			//IL_0154: Incompatible stack heights: 0 vs 3
			//IL_016b: Incompatible stack heights: 0 vs 3
			//IL_019a: Incompatible stack heights: 0 vs 2
			//IL_01b7: Incompatible stack heights: 0 vs 4
			//IL_01cc: Incompatible stack heights: 0 vs 1
			if (CurrentSelect > -1)
			{
				int currentSelect2 = CurrentSelect;
				int num = listBtnNormal.Count() - 1;
				if ((int)/*Error near IL_0130: Stack underflow*/ < num)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < listRtgNormal.Count(); i++)
					{
						if (i == CurrentSelect + 1)
						{
							List<Rectangle> listRtgNormal2 = listRtgNormal;
							int currentSelect = ((PicDefine)/*Error near IL_0036: Stack underflow*/).CurrentSelect;
							Rectangle item = ((List<Rectangle>)/*Error near IL_003b: Stack underflow*/)[currentSelect];
							((List<Rectangle>)/*Error near IL_0040: Stack underflow*/).Add(item);
						}
						else if (i == CurrentSelect)
						{
							List<Rectangle> listRtgNormal3 = listRtgNormal;
							_003F val = /*Error near IL_0053: Stack underflow*/+ 1;
							Rectangle item2 = ((List<Rectangle>)/*Error near IL_0058: Stack underflow*/)[(int)val];
							((List<Rectangle>)/*Error near IL_005d: Stack underflow*/).Add(item2);
						}
						else
						{
							list.Add(listRtgNormal[i]);
						}
					}
					listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						if (j == CurrentSelect + 1)
						{
							SurveyDetail item3 = ((PicDefine)/*Error near IL_00a1: Stack underflow*/).oQuestion.QDetails[CurrentSelect];
							((List<SurveyDetail>)/*Error near IL_00b6: Stack underflow*/).Add(item3);
						}
						else if (j == CurrentSelect)
						{
							List<SurveyDetail> qDetail = oQuestion.QDetails;
							_003F val2 = /*Error near IL_00c8: Stack underflow*/+ /*Error near IL_00c8: Stack underflow*/;
							SurveyDetail item4 = ((List<SurveyDetail>)/*Error near IL_00cd: Stack underflow*/)[(int)val2];
							((List<SurveyDetail>)/*Error near IL_00d2: Stack underflow*/).Add(item4);
						}
						else
						{
							list2.Add(oQuestion.QDetails[j]);
						}
					}
					QMultiple oQuestion2 = oQuestion;
					((QMultiple)/*Error near IL_010e: Stack underflow*/).QDetails = list2;
					_003F133_003F(CurrentSelect + 1);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F133_003F(int _003F373_003F)
		{
			//IL_00c7: Incompatible stack heights: 0 vs 1
			//IL_00dc: Incompatible stack heights: 0 vs 1
			//IL_00e2: Incompatible stack heights: 0 vs 1
			WrapPanel wrapCode = WrapCode;
			wrapCode.Children.Clear();
			listBtnNormal = new List<Button>();
			int num = 0;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Tag = num;
				button.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				if (num != _003F373_003F)
				{
					Style unSelBtnStyle = UnSelBtnStyle;
				}
				else
				{
					Style selBtnStyle = SelBtnStyle;
				}
				((FrameworkElement)/*Error near IL_00e7: Stack underflow*/).Style = (Style)/*Error near IL_00e7: Stack underflow*/;
				if (listRtgNormal[num].Visibility == Visibility.Collapsed)
				{
					button.Opacity = DelBtnOpacity;
				}
				button.Click += _003F29_003F;
				button.FontSize = 16.0;
				button.MinWidth = 210.0;
				button.MinHeight = 30.0;
				wrapCode.Children.Add(button);
				listBtnNormal.Add(button);
				listRtgNormal[num].Tag = num;
				num++;
			}
			CurrentSelect = _003F373_003F;
		}

		private bool _003F87_003F()
		{
			return false;
		}

		private void _003F89_003F()
		{
			string text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("KŒɴ\u0360ѲՎ\u0655ݵ\u087b९\u0a64ୠ\u0c54ൎ\u0e6c\u0f6eၮᅨበጪᑠᕱᙷ");
			try
			{
				oFunc.WriteStringAppendToFile(_003F487_003F._003F488_003F(""), text, _003F487_003F._003F488_003F("Cţɣ\u0365Ѷծٵ"));
				oFunc.WriteStringAppendToFile(oQuestion.QDefine.QUESTION_NAME + _003F487_003F._003F488_003F("(ĪȦ貖嗾惵鏶\uf81b") + DateTime.Now.ToString(), text, _003F487_003F._003F488_003F("Cţɣ\u0365Ѷծٵ"));
				for (int i = 0; i < listRtgNormal.Count; i++)
				{
					Rectangle rectangle = listRtgNormal[i];
					if (rectangle.Visibility == Visibility.Visible)
					{
						SurveyDetail surveyDetail = oQuestion.QDetails[i];
						Point point = rectangle.TranslatePoint(default(Point), Picture);
						int num = Convert.ToInt32(point.X);
						int num2 = Convert.ToInt32(point.Y);
						int num3 = num + Convert.ToInt32(rectangle.Width) - 1;
						int num4 = num2 + Convert.ToInt32(rectangle.Height) - 1;
						string text2 = surveyDetail.DETAIL_ID + _003F487_003F._003F488_003F("-") + surveyDetail.CODE + _003F487_003F._003F488_003F("-") + surveyDetail.CODE_TEXT + _003F487_003F._003F488_003F("-") + (i + 1).ToString() + _003F487_003F._003F488_003F("-") + surveyDetail.PARENT_CODE + _003F487_003F._003F488_003F("-") + surveyDetail.IS_OTHER.ToString() + _003F487_003F._003F488_003F("-") + (i + 1).ToString() + _003F487_003F._003F488_003F("-") + surveyDetail.RANDOM_SET.ToString() + _003F487_003F._003F488_003F("-") + surveyDetail.RANDOM_FIX.ToString() + _003F487_003F._003F488_003F("-");
						text2 = text2 + num.ToString() + _003F487_003F._003F488_003F("-") + num2.ToString() + _003F487_003F._003F488_003F("-") + num3.ToString() + _003F487_003F._003F488_003F("-") + num4.ToString();
						oFunc.WriteStringAppendToFile(text2, text, _003F487_003F._003F488_003F("Cţɣ\u0365Ѷծٵ"));
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgErrorWriteFile, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0056: Incompatible stack heights: 0 vs 1
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				((PicDefine)/*Error near IL_005b: Stack underflow*/).btnNav.Content = btnNav_Content;
			}
			else
			{
				_003F89_003F();
				oPageNav.NextPage(MyNav, base.NavigationService);
				btnNav.Content = btnNav_Content;
			}
			return;
			IL_0046:
			goto IL_0020;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a7e᭤ᱯᵯṯὯ\u2061Ⅹ≣⌫⑼╢♯❭"), UriKind.Relative);
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
				((PicDefine)_003F350_003F).Loaded += _003F80_003F;
				((PicDefine)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 4:
				g = (Grid)_003F350_003F;
				break;
			case 5:
				scrollcode = (ScrollViewer)_003F350_003F;
				break;
			case 6:
				WrapCode = (WrapPanel)_003F350_003F;
				break;
			case 7:
				AddCode = (WrapPanel)_003F350_003F;
				break;
			case 8:
				Code = (TextBox)_003F350_003F;
				break;
			case 9:
				CodeText = (TextBox)_003F350_003F;
				break;
			case 10:
				btnMoveTop = (Button)_003F350_003F;
				btnMoveTop.Click += _003F129_003F;
				break;
			case 11:
				btnMoveBottom = (Button)_003F350_003F;
				btnMoveBottom.Click += _003F130_003F;
				break;
			case 12:
				btnMoveUp = (Button)_003F350_003F;
				btnMoveUp.Click += _003F131_003F;
				break;
			case 13:
				btnMoveDown = (Button)_003F350_003F;
				btnMoveDown.Click += _003F132_003F;
				break;
			case 14:
				btnEdit = (Button)_003F350_003F;
				btnEdit.Click += _003F127_003F;
				break;
			case 15:
				btnCancel = (Button)_003F350_003F;
				btnCancel.Click += _003F128_003F;
				break;
			case 16:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			case 17:
				btnAdd = (Button)_003F350_003F;
				btnAdd.Click += _003F126_003F;
				break;
			case 18:
				btnDel = (Button)_003F350_003F;
				btnDel.Click += _003F125_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
