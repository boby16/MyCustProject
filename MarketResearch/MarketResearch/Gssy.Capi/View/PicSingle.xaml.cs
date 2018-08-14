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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000028 RID: 40
	public partial class PicSingle : Page
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0004C44C File Offset: 0x0004A64C
		public PicSingle()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0004C4F4 File Offset: 0x0004A6F4
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
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
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
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
				SurveyHelper.CircleACode = global::GClass0.smethod_0("");
				SurveyHelper.CircleACodeText = global::GClass0.smethod_0("");
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = global::GClass0.smethod_0("");
				SurveyHelper.CircleBCodeText = global::GClass0.smethod_0("");
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				this.MyNav.GroupCodeA = global::GClass0.smethod_0("");
				this.MyNav.CircleACurrent = 0;
				this.MyNav.CircleACount = 0;
				this.MyNav.GroupCodeB = global::GClass0.smethod_0("");
				this.MyNav.CircleBCurrent = 0;
				this.MyNav.CircleBCount = 0;
			}
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
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
			list2.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string text = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(text, global::GClass0.smethod_0("-Į"));
			text = list3[0];
			if (text == global::GClass0.smethod_0(""))
			{
				this.txtQuestionTitle.Height = 0.0;
				this.txtQuestionTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, text, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			}
			text = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			if (text == global::GClass0.smethod_0(""))
			{
				this.txtCircleTitle.Height = 0.0;
				this.txtCircleTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.oBoldTitle.SetTextBlock(this.txtCircleTitle, text, 0, global::GClass0.smethod_0(""), true);
			}
			string text2 = global::GClass0.smethod_0("");
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				text2 = this.oQuestion.QDefine.CONTROL_TOOLTIP;
			}
			else if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				this.oQuestion.InitCircle();
				string text3 = global::GClass0.smethod_0("");
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
				{
					text3 = this.MyNav.CircleACode;
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					text3 = this.MyNav.CircleBCode;
				}
				if (text3 != global::GClass0.smethod_0(""))
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
			if (text2 != global::GClass0.smethod_0(""))
			{
				if (this.oFunc.LEFT(text2, 1) == global::GClass0.smethod_0("\""))
				{
					text2 = this.oFunc.MID(text2, 1, -9999);
				}
				string text4 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text2;
				if (!File.Exists(text4))
				{
					MessageBox.Show(string.Concat(new string[]
					{
						this.oQuestion.QuestionName,
						global::GClass0.smethod_0("颋捒锑誑犋台瑊抋䛽瘰匸蟰妊祦哸遗淹"),
						Environment.NewLine,
						Environment.NewLine,
						global::GClass0.smethod_0("扅阄哽煅﬛"),
						text4,
						Environment.NewLine,
						Environment.NewLine,
						global::GClass0.smethod_0("触傷枺濗﬛"),
						Environment.NewLine,
						global::GClass0.smethod_0("兪鄡叚͜џՍٕ紐嚕笮弙椨牻敷睽暖䟯恗皈嫸䞁蛆綺䴦䢠愌䊉揢惩钼뗬ᄃ")
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				Image image = new Image();
				if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("\""))
				{
					this.scrollmain.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					this.scrollmain.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				}
				else if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("+")) && !(this.oQuestion.QDefine.CONTROL_MASK.Trim() == global::GClass0.smethod_0("")) && this.oQuestion.QDefine.CONTROL_MASK != null)
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
					if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("\""))
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
				this.canvas.Name = global::GClass0.smethod_0("eŤɪ͵ѣղ");
				this.g.Children.Add(this.canvas);
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(PicSingle.Class27.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count<string>(); j++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								this.listPreSet.Add(array2[j]);
								break;
							}
						}
					}
				}
			}
			this.method_2();
			if (this.ExistTextFill)
			{
				this.txtFill.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0(""))
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					text = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(text, global::GClass0.smethod_0("-Į"));
					text = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, text, 0, global::GClass0.smethod_0(""), true);
					if (list3.Count > 1)
					{
						text = list3[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, text, 0, global::GClass0.smethod_0(""), true);
					}
				}
			}
			else
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				Rectangle rectangle = autoFill.SingleRectangle(this.oQuestion.QDefine, this.listBtnNormal);
				if (rectangle != null)
				{
					if (this.listPreSet.Count == 0)
					{
						this.method_3(rectangle, null);
					}
					if (this.txtFill.IsEnabled)
					{
						this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
					}
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
				{
					if (!(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
					{
					}
				}
				else
				{
					if (this.listPreSet.Count > 0)
					{
						this.oQuestion.SelectedCode = this.listPreSet[0];
						if (this.method_4(this.listPreSet[0]))
						{
							this.txtFill.IsEnabled = true;
							this.txtFill.Background = Brushes.White;
						}
					}
					if (this.oQuestion.QDetails.Count == 1)
					{
						if (this.listPreSet.Count == 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
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
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
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
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				bool flag = this.method_4(this.oQuestion.SelectedCode);
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
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

		// Token: 0x06000261 RID: 609 RVA: 0x0004D5F4 File Offset: 0x0004B7F4
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				this.ParaX = this.Picture.ActualHeight / this.BmpHeight;
				Point point = this.canvas.TranslatePoint(default(Point), this.Picture);
				for (int i = 0; i < this.listBtnNormal.Count; i++)
				{
					Rectangle rectangle = this.listBtnNormal[i];
					SurveyDetail surveyDetail = this.oQuestion.QDetails[i];
					double num = this.oFunc.StringToDouble(surveyDetail.EXTEND_1) * this.ParaX - point.X;
					double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2) * this.ParaX - point.Y;
					double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3) * this.ParaX - point.X;
					double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4) * this.ParaX - point.Y;
					rectangle.Width = num3 - num + 1.0;
					rectangle.Height = num4 - num2 + 1.0;
					Canvas.SetLeft(rectangle, num);
					Canvas.SetTop(rectangle, num2);
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0004D758 File Offset: 0x0004B958
		private void method_2()
		{
			Canvas canvas = this.canvas;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				double num2 = this.oFunc.StringToDouble(surveyDetail.EXTEND_1);
				double num3 = this.oFunc.StringToDouble(surveyDetail.EXTEND_2);
				double num4 = this.oFunc.StringToDouble(surveyDetail.EXTEND_3);
				double num5 = this.oFunc.StringToDouble(surveyDetail.EXTEND_4);
				if (num4 - num2 > 0.0 && num5 - num3 > 0.0)
				{
					bool flag = false;
					if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 11 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 13 || surveyDetail.IS_OTHER == 14)
					{
						flag = true;
					}
					if (flag)
					{
						this.ExistTextFill = true;
					}
					Rectangle rectangle = new Rectangle();
					rectangle.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
					rectangle.Fill = Brushes.White;
					rectangle.Stroke = Brushes.LightBlue;
					rectangle.StrokeThickness = 1.0;
					rectangle.Opacity = this.UnSelImgOpacity;
					rectangle.Tag = num;
					rectangle.MouseLeftButtonUp += this.method_3;
					canvas.Children.Add(rectangle);
					this.listBtnNormal.Add(rectangle);
				}
				num++;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0004D918 File Offset: 0x0004BB18
		private void method_3(object sender, MouseButtonEventArgs e = null)
		{
			Rectangle rectangle = (Rectangle)sender;
			int index = (int)rectangle.Tag;
			int is_OTHER = this.oQuestion.QDetails[index].IS_OTHER;
			string text = this.oQuestion.QDetails[index].CODE;
			if (this.oFunc.LEFT(this.oQuestion.QDetails[index].CODE_TEXT, 1) == global::GClass0.smethod_0("\""))
			{
				text = this.oFunc.MID(this.oQuestion.QDetails[index].CODE_TEXT, 1, -9999);
			}
			int num = 0;
			if (is_OTHER == 1 || is_OTHER == 3 || is_OTHER == 5 || is_OTHER == 11 || is_OTHER == 13 || is_OTHER == 14)
			{
				num = 1;
			}
			int num2 = 0;
			if (rectangle.Opacity == this.SelImgOpacity)
			{
				num2 = 1;
			}
			if (num2 == 0)
			{
				this.oQuestion.SelectedCode = text;
				foreach (Rectangle rectangle2 in this.listBtnNormal)
				{
					int index2 = (int)rectangle2.Tag;
					string a = this.oQuestion.QDetails[index2].CODE;
					if (this.oFunc.LEFT(this.oQuestion.QDetails[index2].CODE_TEXT, 1) == global::GClass0.smethod_0("\""))
					{
						a = this.oFunc.MID(this.oQuestion.QDetails[index2].CODE_TEXT, 1, -9999);
					}
					rectangle2.Opacity = ((a == text) ? this.SelImgOpacity : this.UnSelImgOpacity);
				}
				if (num == 0)
				{
					this.txtFill.Background = Brushes.LightGray;
					this.txtFill.IsEnabled = false;
				}
				else
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					if (this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
					}
				}
			}
			if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
			{
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Focus();
					return;
				}
				this.btnNav_Click(this, e);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0004DBAC File Offset: 0x0004BDAC
		private bool method_4(string string_0)
		{
			bool result = false;
			foreach (Rectangle rectangle in this.listBtnNormal)
			{
				int index = (int)rectangle.Tag;
				string a = this.oQuestion.QDetails[index].CODE;
				if (this.oFunc.LEFT(this.oQuestion.QDetails[index].CODE_TEXT, 1) == global::GClass0.smethod_0("\""))
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

		// Token: 0x06000265 RID: 613 RVA: 0x0004DD00 File Offset: 0x0004BF00
		private bool method_5()
		{
			if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			}
			return false;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0004DDD0 File Offset: 0x0004BFD0
		private List<VEAnswer> method_6()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer.CODE = this.oQuestion.FillText;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0(".ġ"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			return list;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00002C33 File Offset: 0x00000E33
		private void method_7()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0004DEE4 File Offset: 0x0004C0E4
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_5())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_6();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_7();
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
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

		// Token: 0x0600026A RID: 618 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_8(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = int_0;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 < 0) ? 0 : int_0;
			int num3 = (num2 < num) ? num2 : num;
			int num4 = (num2 < num) ? num : num2;
			int num5 = (num2 > string_0.Length) ? string_0.Length : num2;
			num = ((int_1 > string_0.Length) ? (string_0.Length - 1) : int_1);
			return string_0.Substring(num5, num - num5 + 1);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_10(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = string_0.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 > string_0.Length) ? string_0.Length : int_0;
			return string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0004E040 File Offset: 0x0004C240
		private int method_12(string string_0)
		{
			if (string_0 == global::GClass0.smethod_0(""))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("1"))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("/ı"))
			{
				return 0;
			}
			if (!this.method_13(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_13(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00002C57 File Offset: 0x00000E57
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x040004B6 RID: 1206
		private string MySurveyId;

		// Token: 0x040004B7 RID: 1207
		private string CurPageId;

		// Token: 0x040004B8 RID: 1208
		private NavBase MyNav = new NavBase();

		// Token: 0x040004B9 RID: 1209
		private PageNav oPageNav = new PageNav();

		// Token: 0x040004BA RID: 1210
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040004BB RID: 1211
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040004BC RID: 1212
		private UDPX oFunc = new UDPX();

		// Token: 0x040004BD RID: 1213
		private QSingle oQuestion = new QSingle();

		// Token: 0x040004BE RID: 1214
		private bool ExistTextFill;

		// Token: 0x040004BF RID: 1215
		private List<string> listPreSet = new List<string>();

		// Token: 0x040004C0 RID: 1216
		private List<Rectangle> listBtnNormal = new List<Rectangle>();

		// Token: 0x040004C1 RID: 1217
		private double SelImgOpacity = 0.8;

		// Token: 0x040004C2 RID: 1218
		private double UnSelImgOpacity;

		// Token: 0x040004C3 RID: 1219
		private Canvas canvas;

		// Token: 0x040004C4 RID: 1220
		private Image Picture;

		// Token: 0x040004C5 RID: 1221
		private double BmpHeight;

		// Token: 0x040004C6 RID: 1222
		private double ParaX = 1.0;

		// Token: 0x040004C7 RID: 1223
		private bool PageLoaded;

		// Token: 0x040004C8 RID: 1224
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040004C9 RID: 1225
		private int SecondsWait;

		// Token: 0x040004CA RID: 1226
		private int SecondsCountDown;

		// Token: 0x040004CB RID: 1227
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000099 RID: 153
		[CompilerGenerated]
		[Serializable]
		private sealed class Class27
		{
			// Token: 0x06000733 RID: 1843 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CEB RID: 3307
			public static readonly PicSingle.Class27 instance = new PicSingle.Class27();

			// Token: 0x04000CEC RID: 3308
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
