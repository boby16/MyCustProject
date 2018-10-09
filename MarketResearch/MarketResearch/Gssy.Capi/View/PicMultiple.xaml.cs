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
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class PicMultiple : Page
	{
		public PicMultiple()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
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
			List<string> list3 = this.oBoldTitle.ParaToList(text, "//");
			text = list3[0];
			if (text == "")
			{
				this.txtQuestionTitle.Height = 0.0;
				this.txtQuestionTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, text, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			}
			text = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			if (text == "")
			{
				this.txtCircleTitle.Height = 0.0;
				this.txtCircleTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.oBoldTitle.SetTextBlock(this.txtCircleTitle, text, 0, "", true);
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
						"先退出MarketResearch程序，然后放置正确的图片文件，再重新启动程序继续访问。"
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				Image image = new Image();
				if (this.oQuestion.QDefine.CONTROL_MASK == "#")
				{
					this.scrollmain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					this.scrollmain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				}
				else if (!(this.oQuestion.QDefine.CONTROL_MASK == "*") && !(this.oQuestion.QDefine.CONTROL_MASK.Trim() == "") && this.oQuestion.QDefine.CONTROL_MASK != null)
				{
					this.scrollmain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					this.scrollmain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					int num = this.oFunc.StringToInt(this.oQuestion.QDefine.CONTROL_MASK);
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
					bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					if (this.oQuestion.QDefine.CONTROL_MASK == "#")
					{
						image.Width = (double)bitmapImage.PixelWidth;
					}
					this.g.Children.Add(image);
					this.Picture = image;
					this.BmpHeight = (double)bitmapImage.PixelHeight;
				}
				catch (Exception)
				{
				}
				this.canvas = new Canvas();
				this.canvas.Name = "canvas";
				this.g.Children.Add(this.canvas);
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail2);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == "" && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(PicMultiple.Class26.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.FIX_LOGIC != "")
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.FIX_LOGIC, ',');
				for (int j = 0; j < array2.Count<string>(); j++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								this.listFix.Add(array2[j]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != "")
			{
				string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int k = 0; k < array3.Count<string>(); k++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array3[k])
							{
								this.listPreSet.Add(array3[k]);
								break;
							}
						}
					}
				}
			}
			this.method_2();
			if (!this.ExistTextFill && !this.IsFixOther)
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			else
			{
				this.txtFill.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE == "")
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					text = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(text, "//");
					text = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, text, 0, "", true);
					if (list3.Count > 1)
					{
						text = list3[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, text, 0, "", true);
					}
				}
				if (this.IsFixOther)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.listButton = autoFill.MultiRectangle(this.oQuestion.QDefine, this.listButton, 0);
				foreach (Rectangle sender2 in this.listButton)
				{
					this.method_3(sender2, null);
				}
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, "");
				}
				if (this.listButton.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			bool flag = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
					{
					}
				}
				else
				{
					foreach (string text5 in this.listPreSet)
					{
						if (!this.listFix.Contains(text5))
						{
							this.oQuestion.SelectedValues.Add(text5);
							flag = (this.method_4(text5) || flag);
						}
					}
					if (flag)
					{
						this.txtFill.IsEnabled = true;
						this.txtFill.Background = Brushes.White;
					}
					if (this.oQuestion.QDetails.Count == 1 || this.listBtnNormal.Count == 0)
					{
						if (this.listBtnNormal.Count > 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && this.listBtnNormal[0].Opacity == this.UnSelImgOpacity)
						{
							this.method_3(this.listBtnNormal[0], null);
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (this.txtFill.IsEnabled)
							{
								this.txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								this.btnNav_Click(this, e);
							}
						}
					}
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedValues.Count > 0)
					{
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							this.btnNav_Click(this, e);
						}
					}
				}
			}
			else
			{
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (this.oFunc.MID(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_A").Length) == this.oQuestion.QuestionName + "_A")
					{
						if (!this.listFix.Contains(surveyAnswer.CODE))
						{
							this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
							flag = (this.method_4(surveyAnswer.CODE) || flag);
						}
					}
					else if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + "_OTH" && surveyAnswer.CODE != "")
					{
						this.txtFill.Text = surveyAnswer.CODE;
					}
				}
				if (flag)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.Gray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = true;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				this.ParaX = this.Picture.ActualHeight / this.BmpHeight;
				Point point = this.canvas.TranslatePoint(default(Point), this.Picture);
				for (int i = 0; i < this.listBtnFix.Count; i++)
				{
					Rectangle rectangle = this.listBtnFix[i];
					SurveyDetail surveyDetail = this.listDetailFix[i];
					double num = this.oFunc.StringToDouble(surveyDetail.EXTEND_1) * this.ParaX - point.X;
					double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2) * this.ParaX - point.Y;
					double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3) * this.ParaX - point.X;
					double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4) * this.ParaX - point.Y;
					rectangle.Width = num3 - num + 1.0;
					rectangle.Height = num4 - num2 + 1.0;
					Canvas.SetLeft(rectangle, num);
					Canvas.SetTop(rectangle, num2);
				}
				for (int j = 0; j < this.listBtnNormal.Count; j++)
				{
					Rectangle rectangle2 = this.listBtnNormal[j];
					SurveyDetail surveyDetail2 = this.listDetailNormal[j];
					double num5 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_1) * this.ParaX - point.X;
					double num6 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_2) * this.ParaX - point.Y;
					double num7 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_3) * this.ParaX - point.X;
					double num8 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_4) * this.ParaX - point.Y;
					rectangle2.Width = num7 - num5 + 1.0;
					rectangle2.Height = num8 - num6 + 1.0;
					Canvas.SetLeft(rectangle2, num5);
					Canvas.SetTop(rectangle2, num6);
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		private void method_2()
		{
			Canvas canvas = this.canvas;
			if (this.listFix.Count > 0)
			{
				foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
				{
					if (this.listFix.Contains(surveyDetail.CODE))
					{
						if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 11 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 13 || surveyDetail.IS_OTHER == 14)
						{
							this.IsFixOther = true;
						}
						if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 13 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 4 || surveyDetail.IS_OTHER == 14)
						{
							this.IsFixNone = true;
						}
					}
				}
			}
			int num = 0;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
			{
				double num2 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_1);
				double num3 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_2);
				double num4 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_3);
				double num5 = this.oFunc.StringToDouble(surveyDetail2.EXTEND_4);
				if (num4 - num2 > 0.0 && num5 - num3 > 0.0)
				{
					bool flag = false;
					if (surveyDetail2.IS_OTHER == 1 || surveyDetail2.IS_OTHER == 3 || (surveyDetail2.IS_OTHER == 11 | surveyDetail2.IS_OTHER == 5) || surveyDetail2.IS_OTHER == 13 || surveyDetail2.IS_OTHER == 14)
					{
						flag = true;
					}
					if (flag)
					{
						this.ExistTextFill = true;
					}
					bool flag2 = false;
					if (surveyDetail2.IS_OTHER == 2 || surveyDetail2.IS_OTHER == 3 || (surveyDetail2.IS_OTHER == 13 | surveyDetail2.IS_OTHER == 4) || surveyDetail2.IS_OTHER == 5 || surveyDetail2.IS_OTHER == 14)
					{
						flag2 = true;
					}
					string code_TEXT = surveyDetail2.CODE_TEXT;
					string item = (this.oFunc.LEFT(code_TEXT, 1) == "#") ? this.oFunc.MID(code_TEXT, 1, -9999) : surveyDetail2.CODE;
					bool flag3 = this.listFix.Contains(item);
					bool flag4 = true;
					if (!flag3)
					{
						if (this.IsFixNone)
						{
							flag4 = false;
						}
						else if (flag2 && this.listFix.Count > 0)
						{
							flag4 = false;
						}
					}
					if (flag4)
					{
						Rectangle rectangle = new Rectangle();
						rectangle.Name = "b_" + surveyDetail2.CODE;
						rectangle.Fill = Brushes.White;
						rectangle.Stroke = Brushes.LightBlue;
						rectangle.StrokeThickness = 1.0;
						rectangle.Opacity = (flag3 ? this.FixImgOpacity : this.UnSelImgOpacity);
						rectangle.Tag = num;
						if (!flag3)
						{
							if (this.oQuestion.QDefine.CONTROL_TYPE == 0)
							{
								rectangle.MouseLeftButtonUp += this.method_3;
							}
							else
							{
								rectangle.MouseLeftButtonUp += this.method_5;
							}
						}
						canvas.Children.Add(rectangle);
						if (flag3)
						{
							this.listBtnFix.Add(rectangle);
							this.listDetailFix.Add(surveyDetail2);
						}
						else
						{
							this.listBtnNormal.Add(rectangle);
							this.listDetailNormal.Add(surveyDetail2);
							if (!flag2)
							{
								this.listButton.Add(rectangle);
							}
						}
					}
				}
				num++;
			}
		}

		private void method_3(object sender, MouseButtonEventArgs e = null)
		{
			Rectangle rectangle = (Rectangle)sender;
			int index = (int)rectangle.Tag;
			int is_OTHER = this.oQuestion.QDetails[index].IS_OTHER;
			string text = this.oQuestion.QDetails[index].CODE;
			if (this.oFunc.LEFT(this.oQuestion.QDetails[index].CODE_TEXT, 1) == "#")
			{
				text = this.oFunc.MID(this.oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
			}
			int num = 0;
			if (is_OTHER == 2 || is_OTHER == 3 || is_OTHER == 5 || is_OTHER == 13 || is_OTHER == 4 || is_OTHER == 14)
			{
				num = 1;
			}
			int num2 = 0;
			if (rectangle.Opacity == this.SelImgOpacity)
			{
				num2 = 1;
			}
			int num3;
			if (num2 == 0)
			{
				if (num == 1)
				{
					this.oQuestion.SelectedValues.Clear();
					num3 = 1;
				}
				else
				{
					num3 = 2;
				}
				if (!this.oQuestion.SelectedValues.Contains(text))
				{
					this.oQuestion.SelectedValues.Add(text);
				}
				rectangle.Opacity = this.SelImgOpacity;
			}
			else if (num == 1)
			{
				num3 = 0;
			}
			else
			{
				this.oQuestion.SelectedValues.Remove(text);
				rectangle.Opacity = this.UnSelImgOpacity;
				num3 = 3;
			}
			if (num3 > 0)
			{
				bool flag = true;
				foreach (Rectangle rectangle2 in this.listBtnNormal)
				{
					int index2 = (int)rectangle2.Tag;
					int is_OTHER2 = this.oQuestion.QDetails[index2].IS_OTHER;
					string text2 = this.oQuestion.QDetails[index2].CODE;
					if (this.oFunc.LEFT(this.oQuestion.QDetails[index2].CODE_TEXT, 1) == "#")
					{
						text2 = this.oFunc.MID(this.oQuestion.QDetails[index2].CODE_TEXT, 1, -9999);
					}
					if (text2 == text)
					{
						rectangle2.Opacity = ((num2 == 0) ? this.SelImgOpacity : this.UnSelImgOpacity);
					}
					else if (num3 == 1)
					{
						rectangle2.Opacity = this.UnSelImgOpacity;
					}
					else if (num3 == 2 && (is_OTHER2 == 2 || is_OTHER2 == 3 || is_OTHER2 == 5 || is_OTHER2 == 13 || is_OTHER2 == 4 || is_OTHER2 == 14) && rectangle2.Opacity == this.SelImgOpacity)
					{
						rectangle2.Opacity = this.UnSelImgOpacity;
						if (this.oQuestion.SelectedValues.Contains(text2))
						{
							this.oQuestion.SelectedValues.Remove(text2);
						}
					}
					if (!this.IsFixOther && flag && rectangle2.Opacity == this.SelImgOpacity && (is_OTHER2 == 1 || is_OTHER2 == 3 || is_OTHER2 == 5 || is_OTHER2 == 11 || is_OTHER2 == 13 || is_OTHER2 == 14))
					{
						flag = false;
					}
				}
				if (!this.IsFixOther)
				{
					if (flag)
					{
						this.txtFill.Background = Brushes.LightGray;
						this.txtFill.IsEnabled = false;
						return;
					}
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					if (this.txtFill.Text == "")
					{
						this.txtFill.Focus();
					}
				}
			}
		}

		private bool method_4(string string_0)
		{
			bool result = false;
			foreach (Rectangle rectangle in this.listBtnNormal)
			{
				int index = (int)rectangle.Tag;
				string a = this.oQuestion.QDetails[index].CODE;
				if (this.oFunc.LEFT(this.oQuestion.QDetails[index].CODE_TEXT, 1) == "#")
				{
					a = this.oFunc.MID(this.oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
				}
				if (a == string_0)
				{
					if (rectangle.Opacity == this.UnSelImgOpacity)
					{
						rectangle.Opacity = this.SelImgOpacity;
						int is_OTHER = this.oQuestion.QDetails[index].IS_OTHER;
						if (is_OTHER == 1 || is_OTHER == 3 || is_OTHER == 5 || (is_OTHER == 11 | is_OTHER == 13) || is_OTHER == 14)
						{
							result = true;
						}
					}
					else
					{
						rectangle.Opacity = this.UnSelImgOpacity;
					}
				}
			}
			return result;
		}

		private void method_5(object sender, MouseButtonEventArgs e)
		{
			Point position = e.GetPosition(this.Picture);
			double x = position.X;
			double y = position.Y;
			for (int i = 0; i < this.listBtnNormal.Count; i++)
			{
				Rectangle sender2 = this.listBtnNormal[i];
				SurveyDetail surveyDetail = this.listDetailNormal[i];
				double num = this.oFunc.StringToDouble(surveyDetail.EXTEND_1) * this.ParaX;
				double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2) * this.ParaX;
				double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3) * this.ParaX;
				double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4) * this.ParaX;
				if (num <= x && num2 <= y && num3 >= x && num4 >= y)
				{
					this.method_3(sender2, e);
				}
			}
		}

		private bool method_6()
		{
			if (this.listFix.Count == 0 && this.oQuestion.SelectedValues.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MIN_COUNT != 0 && this.listFix.Count + this.oQuestion.SelectedValues.Count < this.oQuestion.QDefine.MIN_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAless, this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MAX_COUNT != 0 && this.listFix.Count + this.oQuestion.SelectedValues.Count > this.oQuestion.QDefine.MAX_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : "");
			return false;
		}

		private List<VEAnswer> method_7()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			foreach (string item in this.listFix)
			{
				this.oQuestion.SelectedValues.Add(item);
			}
			SurveyHelper.Answer = "";
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + "_A" + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer.QUESTION_NAME,
					"=",
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != "")
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH";
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer2.QUESTION_NAME,
					"=",
					this.oQuestion.FillText
				});
			}
			return list;
		}

		private void method_8(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_6())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_7();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_8(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.timer.Stop();
				this.btnNav.Foreground = Brushes.Black;
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
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

		[CompilerGenerated]
		[Serializable]
		private sealed class Class26
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly PicMultiple.Class26 instance = new PicMultiple.Class26();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
