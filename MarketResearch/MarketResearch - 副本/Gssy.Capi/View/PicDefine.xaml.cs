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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	public partial class PicDefine : Page
	{
		public PicDefine()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.btnNav.Content = this.btnNav_Content;
			this.SelBtnStyle = (Style)base.FindResource("SelBtnStyle");
			this.UnSelBtnStyle = (Style)base.FindResource("UnSelBtnStyle");
			this.oQuestion.Init(this.CurPageId, 0, false);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != "")
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == "B")
				{
					this.MyNav.GroupCodeB = this.oQuestion.QDefine.GROUP_CODEB;
					this.MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					this.MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				List<VEAnswer> list = new List<VEAnswer>();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.MyNav.GroupCodeA,
					CODE = this.MyNav.CircleACode,
					CODE_TEXT = this.MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = this.MyNav.CircleACode;
				SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
				if (this.MyNav.GroupLevel == "B")
				{
					list.Add(new VEAnswer
					{
						QUESTION_NAME = this.MyNav.GroupCodeB,
						CODE = this.MyNav.CircleBCode,
						CODE_TEXT = this.MyNav.CircleCodeTextB
					});
					SurveyHelper.CircleBCode = this.MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = this.MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
				}
			}
			else
			{
				SurveyHelper.CircleACode = "";
				SurveyHelper.CircleACodeText = "";
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = "";
				SurveyHelper.CircleBCodeText = "";
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				this.MyNav.GroupCodeA = "";
				this.MyNav.CircleACurrent = 0;
				this.MyNav.CircleACount = 0;
				this.MyNav.GroupCodeB = "";
				this.MyNav.CircleBCurrent = 0;
				this.MyNav.CircleBCount = 0;
			}
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != "")
			{
				this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add("");
			if (show_LOGIC != "")
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, "//");
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string text = this.oQuestion.QDefine.QUESTION_TITLE;
			text = this.oBoldTitle.ParaToList(text, "//")[0];
			if (text == "")
			{
				this.txtQuestionTitle.Height = 0.0;
				this.txtQuestionTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, text, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			}
			string text2 = "";
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				text2 = this.oQuestion.QDefine.CONTROL_TOOLTIP;
			}
			else if (this.oQuestion.QDefine.GROUP_LEVEL != "" && this.oQuestion.QDefine.CONTROL_MASK != "")
			{
				this.oQuestion.InitCircle();
				string text3 = "";
				if (this.MyNav.GroupLevel == "A")
				{
					text3 = this.MyNav.CircleACode;
				}
				if (this.MyNav.GroupLevel == "B")
				{
					text3 = this.MyNav.CircleBCode;
				}
				if (text3 != "")
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail.CODE == text3)
						{
							text2 = surveyDetail.EXTEND_1;
							break;
						}
					}
				}
			}
			if (text2 != "")
			{
				if (this.oFunc.LEFT(text2, 1) == "#")
				{
					text2 = this.oFunc.MID(text2, 1, -9999);
				}
				string text4 = Environment.CurrentDirectory + "\\Media\\" + text2;
				if (!File.Exists(text4))
				{
					MessageBox.Show(string.Concat(new string[]
					{
						this.oQuestion.QuestionName,
						"题所需要的图片文件缺失！请和督导联系！",
						Environment.NewLine,
						Environment.NewLine,
						"所需图片：",
						text4,
						Environment.NewLine,
						Environment.NewLine,
						"解决方法：",
						Environment.NewLine,
						"先退出CAPI程序，然后放置正确的图片文件，再重新启动程序继续访问。"
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
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
					bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					image.Width = (double)bitmapImage.PixelWidth;
					image.Height = (double)bitmapImage.PixelHeight;
					image.MouseLeftButtonUp += this.method_5;
					image.MouseMove += this.method_4;
					this.g.Children.Add(image);
					this.Picture = image;
					this.BmpHeight = image.Height;
					this.BmpWidth = image.Width;
				}
				catch (Exception)
				{
				}
				this.CanvasRtg = new Canvas();
				this.g.Children.Add(this.CanvasRtg);
			}
			this.method_2();
			this.PageLoaded = true;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				Point point = this.CanvasRtg.TranslatePoint(default(Point), this.Picture);
				this.ParaX = this.Picture.ActualHeight / this.BmpHeight;
				for (int i = 0; i < this.oQuestion.QDetails.Count; i++)
				{
					Rectangle rectangle = this.listRtgNormal[i];
					SurveyDetail surveyDetail = this.oQuestion.QDetails[i];
					double num = this.oFunc.StringToDouble(surveyDetail.EXTEND_1) - point.X;
					double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2);
					double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3) - point.X;
					double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4);
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
					rectangle.Width = ((num5 > 1.0) ? num5 : 50.0);
					rectangle.Height = ((num6 > 1.0) ? num6 : 50.0);
					Canvas.SetLeft(rectangle, num);
					Canvas.SetTop(rectangle, num2);
				}
				this.PageLoaded = false;
			}
		}

		private void method_2()
		{
			WrapPanel wrapCode = this.WrapCode;
			Canvas canvasRtg = this.CanvasRtg;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_1);
				double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2);
				double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3);
				double num5 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4);
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
				}
				if (num5 < 0.0)
				{
					num5 = 0.0;
				}
				double num6 = num5 - num3 + 1.0;
				if (num6 <= 1.0)
				{
				}
				Button button = new Button();
				button.Name = "b_" + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Tag = num;
				button.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				button.Style = this.UnSelBtnStyle;
				button.Click += this.method_6;
				button.FontSize = 16.0;
				button.MinWidth = 210.0;
				button.MinHeight = 30.0;
				wrapCode.Children.Add(button);
				this.listBtnNormal.Add(button);
				Rectangle rectangle = new Rectangle();
				rectangle.Name = "r_" + surveyDetail.CODE;
				rectangle.Tag = num;
				rectangle.Fill = Brushes.White;
				rectangle.Stroke = Brushes.Gray;
				rectangle.StrokeThickness = 1.0;
				rectangle.Opacity = this.UnSelImgOpacity;
				rectangle.MouseLeftButtonDown += this.method_3;
				rectangle.MouseLeftButtonUp += this.method_5;
				rectangle.MouseMove += this.method_4;
				Canvas.SetLeft(rectangle, num2);
				Canvas.SetTop(rectangle, num3);
				canvasRtg.Children.Add(rectangle);
				this.listRtgNormal.Add(rectangle);
				num++;
			}
		}

		private void method_3(object sender, MouseButtonEventArgs e)
		{
			Rectangle rectangle = (Rectangle)sender;
			this.CurrentSelect = (int)rectangle.Tag;
			if (this.MouseStatus == "")
			{
				Point position = e.GetPosition(this.Picture);
				Point position2 = e.GetPosition(rectangle);
				this.p_Rectangle = rectangle.TranslatePoint(default(Point), this.Picture);
				this.p_MouseLeftDown = position;
				this.Width_MouseLeftDown = rectangle.Width;
				this.Height_MouseLeftDown = rectangle.Height;
				this.ChangeLeft = (position2.X <= 5.0);
				this.ChangeRight = (rectangle.Width - position2.X <= 5.0 && !this.ChangeLeft);
				this.ChangeTop = (position2.Y <= 5.0);
				this.ChangeBottom = (rectangle.Height - position2.Y <= 5.0 && !this.ChangeTop);
				if (!this.ChangeLeft && !this.ChangeRight && !this.ChangeTop && !this.ChangeBottom)
				{
					this.MouseStatus = "Move";
					rectangle.StrokeThickness = 1.0;
				}
				else
				{
					this.MouseStatus = "Change";
					rectangle.StrokeThickness = 5.0;
				}
				for (int i = 0; i < this.listBtnNormal.Count<Button>(); i++)
				{
					Button button = this.listBtnNormal[i];
					Rectangle rectangle2 = this.listRtgNormal[i];
					button.Style = this.UnSelBtnStyle;
					if (button.Opacity == this.NormalBtnOpacity)
					{
						rectangle2.Opacity = this.UnSelImgOpacity;
						Panel.SetZIndex(rectangle2, i + 1);
					}
				}
				this.listBtnNormal[this.CurrentSelect].Style = this.SelBtnStyle;
				this.listBtnNormal[this.CurrentSelect].BringIntoView();
				this.listRtgNormal[this.CurrentSelect].Opacity = this.SelImgOpacity;
				Panel.SetZIndex(this.listRtgNormal[this.CurrentSelect], this.listBtnNormal.Count + 2);
				this.listRtgNormal[this.CurrentSelect].BringIntoView();
			}
		}

		private void method_4(object sender, MouseEventArgs e)
		{
			double num = 15.0;
			double num2 = 15.0;
			if (Mouse.LeftButton == MouseButtonState.Pressed && this.CurrentSelect > -1 && this.MouseStatus != "")
			{
				Rectangle rectangle = this.listRtgNormal[this.CurrentSelect];
				Point position = e.GetPosition(this.Picture);
				double num3 = position.X - this.p_MouseLeftDown.X;
				double num4 = position.Y - this.p_MouseLeftDown.Y;
				if (this.MouseStatus == "Move")
				{
					double num5 = this.p_Rectangle.X + num3;
					double num6 = this.p_Rectangle.Y + num4;
					if (num5 < 0.0)
					{
						Canvas.SetLeft(rectangle, 0.0);
					}
					else if (num5 + rectangle.Width > this.Picture.Width)
					{
						Canvas.SetLeft(rectangle, this.Picture.Width - rectangle.Width);
					}
					else
					{
						Canvas.SetLeft(rectangle, this.p_Rectangle.X + num3);
					}
					if (num6 < 0.0)
					{
						Canvas.SetTop(rectangle, 0.0);
					}
					else if (num6 + rectangle.Height > this.Picture.Height)
					{
						Canvas.SetTop(rectangle, this.Picture.Height - rectangle.Height);
					}
					else
					{
						Canvas.SetTop(rectangle, this.p_Rectangle.Y + num4);
					}
					rectangle.StrokeThickness = 1.0;
					return;
				}
				rectangle.StrokeThickness = 5.0;
				if (this.ChangeLeft)
				{
					double num7 = this.p_Rectangle.X + this.Width_MouseLeftDown;
					if (position.X < num7 && Math.Abs(this.Width_MouseLeftDown - num3) > num)
					{
						if (position.X < num7)
						{
							Canvas.SetLeft(rectangle, this.p_Rectangle.X + num3);
						}
						if (position.X > num7)
						{
							Canvas.SetLeft(rectangle, num7);
						}
						if (position.X < num7)
						{
							rectangle.Width = Math.Abs(this.Width_MouseLeftDown - num3);
						}
						if (position.X > num7)
						{
							rectangle.Width = Math.Abs(num3 - this.Width_MouseLeftDown);
						}
					}
				}
				if (this.ChangeRight)
				{
					double x = this.p_Rectangle.X;
					if (position.X > x && Math.Abs(num3 + this.Width_MouseLeftDown) > num)
					{
						if (position.X < x)
						{
							Canvas.SetLeft(rectangle, this.p_Rectangle.X + num3 + this.Width_MouseLeftDown);
						}
						if (position.X < x)
						{
							rectangle.Width = Math.Abs(position.X - this.p_Rectangle.X);
						}
						if (position.X > x)
						{
							rectangle.Width = Math.Abs(num3 + this.Width_MouseLeftDown);
						}
					}
				}
				if (this.ChangeTop)
				{
					double num8 = this.p_Rectangle.Y + this.Height_MouseLeftDown;
					if (position.Y < num8 && Math.Abs(this.Height_MouseLeftDown - num4) > num2)
					{
						if (position.Y < num8)
						{
							Canvas.SetTop(rectangle, this.p_Rectangle.Y + num4);
						}
						if (position.Y > num8)
						{
							Canvas.SetTop(rectangle, num8);
						}
						if (position.Y < num8)
						{
							rectangle.Height = Math.Abs(this.Height_MouseLeftDown - num4);
						}
						if (position.Y > num8)
						{
							rectangle.Height = Math.Abs(num4 - this.Height_MouseLeftDown);
						}
					}
				}
				if (this.ChangeBottom)
				{
					double y = this.p_Rectangle.Y;
					if (position.Y > y && Math.Abs(num4 + this.Height_MouseLeftDown) > num2)
					{
						if (position.Y < y)
						{
							Canvas.SetTop(rectangle, this.p_Rectangle.Y + num4 + this.Height_MouseLeftDown);
						}
						if (position.Y < y)
						{
							rectangle.Height = Math.Abs(position.Y - this.p_Rectangle.Y);
						}
						if (position.Y > y)
						{
							rectangle.Height = Math.Abs(num4 + this.Height_MouseLeftDown);
							return;
						}
					}
				}
			}
			else
			{
				this.MouseStatus = "";
				if (this.CurrentSelect > -1)
				{
					this.listRtgNormal[this.CurrentSelect].StrokeThickness = 1.0;
				}
			}
		}

		private void method_5(object sender, MouseButtonEventArgs e)
		{
			if (this.CurrentSelect > -1)
			{
				this.listRtgNormal[this.CurrentSelect].StrokeThickness = 1.0;
			}
			this.MouseStatus = "";
		}

		private void method_6(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			this.CurrentSelect = (int)button.Tag;
			for (int i = 0; i < this.listBtnNormal.Count<Button>(); i++)
			{
				Button button2 = this.listBtnNormal[i];
				Rectangle rectangle = this.listRtgNormal[i];
				button2.Style = this.UnSelBtnStyle;
				if (button2.Opacity == this.NormalBtnOpacity)
				{
					rectangle.Opacity = this.UnSelImgOpacity;
					Panel.SetZIndex(rectangle, i + 1);
				}
			}
			if (this.listBtnNormal[this.CurrentSelect].Opacity == this.NormalBtnOpacity)
			{
				this.btnDel.Content = "删除选项";
			}
			else
			{
				this.btnDel.Content = "恢复选项";
			}
			this.listBtnNormal[this.CurrentSelect].Style = this.SelBtnStyle;
			this.listRtgNormal[this.CurrentSelect].Opacity = this.SelImgOpacity;
			Panel.SetZIndex(this.listRtgNormal[this.CurrentSelect], this.listBtnNormal.Count + 2);
			this.listRtgNormal[this.CurrentSelect].BringIntoView();
		}

		private void btnDel_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect <= -1)
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.listBtnNormal[this.CurrentSelect].Opacity == this.NormalBtnOpacity)
			{
				this.listBtnNormal[this.CurrentSelect].Opacity = this.DelBtnOpacity;
				this.listRtgNormal[this.CurrentSelect].Visibility = Visibility.Collapsed;
				this.btnDel.Content = "恢复选项";
				return;
			}
			this.listBtnNormal[this.CurrentSelect].Opacity = this.NormalBtnOpacity;
			this.listRtgNormal[this.CurrentSelect].Visibility = Visibility.Visible;
			this.listRtgNormal[this.CurrentSelect].Opacity = this.SelImgOpacity;
			Panel.SetZIndex(this.listRtgNormal[this.CurrentSelect], this.listBtnNormal.Count + 2);
			this.btnDel.Content = SurveyMsg.MsgOptionDelete;
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnAdd.Content.ToString() == SurveyMsg.MsgOptionAdd)
			{
				this.btnAdd.Content = SurveyMsg.MsgOptionSave;
				this.AddCode.Visibility = Visibility.Visible;
				this.btnCancel.Visibility = Visibility.Visible;
				this.btnEdit.Visibility = Visibility.Hidden;
				this.Code.Focus();
				return;
			}
			if (this.Code.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillCode, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.CodeText.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CODE == this.Code.Text.Trim())
					{
						MessageBox.Show(SurveyMsg.MsgFillCodeRepeat, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
				}
			}
			this.btnAdd.Content = SurveyMsg.MsgOptionAdd;
			this.AddCode.Visibility = Visibility.Collapsed;
			this.btnCancel.Visibility = Visibility.Hidden;
			this.btnEdit.Visibility = Visibility.Visible;
			if (this.CurrentSelect > -1)
			{
				this.listRtgNormal[this.CurrentSelect].Opacity = this.UnSelImgOpacity;
				this.listBtnNormal[this.CurrentSelect].Style = this.UnSelBtnStyle;
			}
			this.CurrentSelect = this.listRtgNormal.Count<Rectangle>();
			SurveyDetail surveyDetail = new SurveyDetail();
			surveyDetail.DETAIL_ID = this.oQuestion.QDefine.DETAIL_ID;
			surveyDetail.CODE = this.Code.Text;
			surveyDetail.CODE_TEXT = this.CodeText.Text;
			surveyDetail.INNER_ORDER = this.CurrentSelect + 1;
			surveyDetail.RANDOM_BASE = this.CurrentSelect + 1;
			this.oQuestion.QDetails.Add(surveyDetail);
			Rectangle rectangle = new Rectangle();
			rectangle.Name = "r_" + surveyDetail.CODE;
			rectangle.Tag = this.CurrentSelect;
			rectangle.Fill = Brushes.White;
			rectangle.Stroke = Brushes.Gray;
			rectangle.StrokeThickness = 1.0;
			rectangle.Opacity = this.SelImgOpacity;
			rectangle.Width = 100.0;
			rectangle.Height = 50.0;
			rectangle.MouseLeftButtonDown += this.method_3;
			rectangle.MouseLeftButtonUp += this.method_5;
			rectangle.MouseMove += this.method_4;
			Canvas.SetLeft(rectangle, 0.0);
			Canvas.SetTop(rectangle, 0.0);
			this.CanvasRtg.Children.Add(rectangle);
			this.listRtgNormal.Add(rectangle);
			rectangle.BringIntoView();
			this.method_7(this.CurrentSelect);
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect <= -1)
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.btnEdit.Content.ToString() == SurveyMsg.MsgOptionModify)
			{
				this.btnEdit.Content = SurveyMsg.MsgOptionSaveModify;
				this.btnAdd.Visibility = Visibility.Hidden;
				this.Code.Text = this.oQuestion.QDetails[this.CurrentSelect].CODE;
				this.CodeText.Text = this.oQuestion.QDetails[this.CurrentSelect].CODE_TEXT;
				this.AddCode.Visibility = Visibility.Visible;
				this.btnCancel.Visibility = Visibility.Visible;
				this.Code.Focus();
				return;
			}
			if (this.Code.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillCode, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.CodeText.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgFirstFillText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			for (int i = 0; i < this.listBtnNormal.Count<Button>(); i++)
			{
				if (i != this.CurrentSelect && this.oQuestion.QDetails[i].CODE == this.Code.Text.Trim())
				{
					MessageBox.Show(SurveyMsg.MsgFillCodeRepeat, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
			}
			this.btnEdit.Content = SurveyMsg.MsgOptionModify;
			this.btnAdd.Visibility = Visibility.Visible;
			this.AddCode.Visibility = Visibility.Collapsed;
			this.btnCancel.Visibility = Visibility.Hidden;
			this.oQuestion.QDetails[this.CurrentSelect].CODE = this.Code.Text.Trim();
			this.oQuestion.QDetails[this.CurrentSelect].CODE_TEXT = this.CodeText.Text.Trim();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.btnEdit.Content = SurveyMsg.MsgOptionModify;
			this.btnAdd.Content = SurveyMsg.MsgOptionAdd;
			this.btnEdit.Visibility = Visibility.Visible;
			this.btnAdd.Visibility = Visibility.Visible;
			this.AddCode.Visibility = Visibility.Collapsed;
			this.btnCancel.Visibility = Visibility.Hidden;
		}

		private void btnMoveTop_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect > -1)
			{
				if (this.CurrentSelect > 0)
				{
					List<Rectangle> list = new List<Rectangle>();
					list.Add(this.listRtgNormal[this.CurrentSelect]);
					for (int i = 0; i < this.listRtgNormal.Count<Rectangle>(); i++)
					{
						if (i != this.CurrentSelect)
						{
							list.Add(this.listRtgNormal[i]);
						}
					}
					this.listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					list2.Add(this.oQuestion.QDetails[this.CurrentSelect]);
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						if (j != this.CurrentSelect)
						{
							list2.Add(this.oQuestion.QDetails[j]);
						}
					}
					this.oQuestion.QDetails = list2;
					this.method_7(0);
					return;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void btnMoveBottom_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect > -1)
			{
				if (this.CurrentSelect < this.listBtnNormal.Count<Button>() - 1)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < this.listRtgNormal.Count<Rectangle>(); i++)
					{
						if (i != this.CurrentSelect)
						{
							list.Add(this.listRtgNormal[i]);
						}
					}
					list.Add(this.listRtgNormal[this.CurrentSelect]);
					this.listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						if (j != this.CurrentSelect)
						{
							list2.Add(this.oQuestion.QDetails[j]);
						}
					}
					list2.Add(this.oQuestion.QDetails[this.CurrentSelect]);
					this.oQuestion.QDetails = list2;
					this.method_7(this.listBtnNormal.Count - 1);
					return;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void btnMoveUp_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect > -1)
			{
				if (this.CurrentSelect > 0)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < this.listRtgNormal.Count<Rectangle>(); i++)
					{
						if (i == this.CurrentSelect - 1)
						{
							list.Add(this.listRtgNormal[this.CurrentSelect]);
						}
						else if (i == this.CurrentSelect)
						{
							list.Add(this.listRtgNormal[i - 1]);
						}
						else
						{
							list.Add(this.listRtgNormal[i]);
						}
					}
					this.listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						if (j == this.CurrentSelect - 1)
						{
							list2.Add(this.oQuestion.QDetails[this.CurrentSelect]);
						}
						else if (j == this.CurrentSelect)
						{
							list2.Add(this.oQuestion.QDetails[j - 1]);
						}
						else
						{
							list2.Add(this.oQuestion.QDetails[j]);
						}
					}
					this.oQuestion.QDetails = list2;
					this.method_7(this.CurrentSelect - 1);
					return;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void btnMoveDown_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentSelect > -1)
			{
				if (this.CurrentSelect < this.listBtnNormal.Count<Button>() - 1)
				{
					List<Rectangle> list = new List<Rectangle>();
					for (int i = 0; i < this.listRtgNormal.Count<Rectangle>(); i++)
					{
						if (i == this.CurrentSelect + 1)
						{
							list.Add(this.listRtgNormal[this.CurrentSelect]);
						}
						else if (i == this.CurrentSelect)
						{
							list.Add(this.listRtgNormal[i + 1]);
						}
						else
						{
							list.Add(this.listRtgNormal[i]);
						}
					}
					this.listRtgNormal = list;
					List<SurveyDetail> list2 = new List<SurveyDetail>();
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						if (j == this.CurrentSelect + 1)
						{
							list2.Add(this.oQuestion.QDetails[this.CurrentSelect]);
						}
						else if (j == this.CurrentSelect)
						{
							list2.Add(this.oQuestion.QDetails[j + 1]);
						}
						else
						{
							list2.Add(this.oQuestion.QDetails[j]);
						}
					}
					this.oQuestion.QDetails = list2;
					this.method_7(this.CurrentSelect + 1);
					return;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void method_7(int int_0)
		{
			WrapPanel wrapCode = this.WrapCode;
			wrapCode.Children.Clear();
			this.listBtnNormal = new List<Button>();
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = "b_" + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Tag = num;
				button.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
				button.Style = ((num == int_0) ? this.SelBtnStyle : this.UnSelBtnStyle);
				if (this.listRtgNormal[num].Visibility == Visibility.Collapsed)
				{
					button.Opacity = this.DelBtnOpacity;
				}
				button.Click += this.method_6;
				button.FontSize = 16.0;
				button.MinWidth = 210.0;
				button.MinHeight = 30.0;
				wrapCode.Children.Add(button);
				this.listBtnNormal.Add(button);
				this.listRtgNormal[num].Tag = num;
				num++;
			}
			this.CurrentSelect = int_0;
		}

		private bool method_8()
		{
			return false;
		}

		private void method_9()
		{
			string text = Environment.CurrentDirectory +"\\Data\\Detail_Define.csv";
			try
			{
				this.oFunc.WriteStringAppendToFile("", text, "Default");
				this.oFunc.WriteStringAppendToFile(this.oQuestion.QDefine.QUESTION_NAME + " - 输出时间：" + DateTime.Now.ToString(), text, "Default");
				for (int i = 0; i < this.listRtgNormal.Count; i++)
				{
					Rectangle rectangle = this.listRtgNormal[i];
					if (rectangle.Visibility == Visibility.Visible)
					{
						SurveyDetail surveyDetail = this.oQuestion.QDetails[i];
						Point point = rectangle.TranslatePoint(default(Point), this.Picture);
						int num = Convert.ToInt32(point.X);
						int num2 = Convert.ToInt32(point.Y);
						int num3 = num + Convert.ToInt32(rectangle.Width) - 1;
						int num4 = num2 + Convert.ToInt32(rectangle.Height) - 1;
						string text2 = string.Concat(new string[]
						{
							surveyDetail.DETAIL_ID,
							",",
							surveyDetail.CODE,
							",",
							surveyDetail.CODE_TEXT,
							",",
							(i + 1).ToString(),
							",",
							surveyDetail.PARENT_CODE,
							",",
							surveyDetail.IS_OTHER.ToString(),
							",",
							(i + 1).ToString(),
							",",
							surveyDetail.RANDOM_SET.ToString(),
							",",
							surveyDetail.RANDOM_FIX.ToString(),
							","
						});
						text2 = string.Concat(new string[]
						{
							text2,
							num.ToString(),
							",",
							num2.ToString(),
							",",
							num3.ToString(),
							",",
							num4.ToString()
						});
						this.oFunc.WriteStringAppendToFile(text2, text, "Default");
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgErrorWriteFile, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_8())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_9();
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

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

		private string MouseStatus = "";

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
	}
}
