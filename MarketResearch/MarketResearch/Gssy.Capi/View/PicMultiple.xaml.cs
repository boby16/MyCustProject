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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000027 RID: 39
	public partial class PicMultiple : Page
	{
		// Token: 0x0600024E RID: 590 RVA: 0x00049C6C File Offset: 0x00047E6C
		public PicMultiple()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00049D58 File Offset: 0x00047F58
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
					list4.Sort(new Comparison<SurveyDetail>(PicMultiple.Class26.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.FIX_LOGIC != global::GClass0.smethod_0(""))
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
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
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
				if (this.IsFixOther)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
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
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
				}
				if (this.listButton.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			bool flag = false;
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
					if (this.oFunc.MID(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ"))
					{
						if (!this.listFix.Contains(surveyAnswer.CODE))
						{
							this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
							flag = (this.method_4(surveyAnswer.CODE) || flag);
						}
					}
					else if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉") && surveyAnswer.CODE != global::GClass0.smethod_0(""))
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

		// Token: 0x06000250 RID: 592 RVA: 0x0004B100 File Offset: 0x00049300
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

		// Token: 0x06000251 RID: 593 RVA: 0x0004B360 File Offset: 0x00049560
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
					string item = (this.oFunc.LEFT(code_TEXT, 1) == global::GClass0.smethod_0("\"")) ? this.oFunc.MID(code_TEXT, 1, -9999) : surveyDetail2.CODE;
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
						rectangle.Name = global::GClass0.smethod_0("`Ş") + surveyDetail2.CODE;
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

		// Token: 0x06000252 RID: 594 RVA: 0x0004B76C File Offset: 0x0004996C
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
					if (this.oFunc.LEFT(this.oQuestion.QDetails[index2].CODE_TEXT, 1) == global::GClass0.smethod_0("\""))
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
					if (this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
					}
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0004BB14 File Offset: 0x00049D14
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

		// Token: 0x06000254 RID: 596 RVA: 0x0004BC68 File Offset: 0x00049E68
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

		// Token: 0x06000255 RID: 597 RVA: 0x0004BD50 File Offset: 0x00049F50
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
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			return false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0004BEFC File Offset: 0x0004A0FC
		private List<VEAnswer> method_7()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			foreach (string item in this.listFix)
			{
				this.oQuestion.SelectedValues.Add(item);
			}
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer2.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			return list;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
		private void method_8(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0004C14C File Offset: 0x0004A34C
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

		// Token: 0x06000259 RID: 601 RVA: 0x0004C244 File Offset: 0x0004A444
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

		// Token: 0x0600025A RID: 602 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400048C RID: 1164
		private string MySurveyId;

		// Token: 0x0400048D RID: 1165
		private string CurPageId;

		// Token: 0x0400048E RID: 1166
		private NavBase MyNav = new NavBase();

		// Token: 0x0400048F RID: 1167
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000490 RID: 1168
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000491 RID: 1169
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000492 RID: 1170
		private UDPX oFunc = new UDPX();

		// Token: 0x04000493 RID: 1171
		private QMultiple oQuestion = new QMultiple();

		// Token: 0x04000494 RID: 1172
		private bool ExistTextFill;

		// Token: 0x04000495 RID: 1173
		private List<string> listPreSet = new List<string>();

		// Token: 0x04000496 RID: 1174
		private List<string> listFix = new List<string>();

		// Token: 0x04000497 RID: 1175
		private List<Rectangle> listBtnFix = new List<Rectangle>();

		// Token: 0x04000498 RID: 1176
		private List<SurveyDetail> listDetailFix = new List<SurveyDetail>();

		// Token: 0x04000499 RID: 1177
		private List<Rectangle> listBtnNormal = new List<Rectangle>();

		// Token: 0x0400049A RID: 1178
		private List<SurveyDetail> listDetailNormal = new List<SurveyDetail>();

		// Token: 0x0400049B RID: 1179
		private bool IsFixOther;

		// Token: 0x0400049C RID: 1180
		private bool IsFixNone;

		// Token: 0x0400049D RID: 1181
		private List<Rectangle> listButton = new List<Rectangle>();

		// Token: 0x0400049E RID: 1182
		private double SelImgOpacity = 0.8;

		// Token: 0x0400049F RID: 1183
		private double UnSelImgOpacity;

		// Token: 0x040004A0 RID: 1184
		private double FixImgOpacity = 0.5;

		// Token: 0x040004A1 RID: 1185
		private Canvas canvas;

		// Token: 0x040004A2 RID: 1186
		private Image Picture;

		// Token: 0x040004A3 RID: 1187
		private double BmpHeight;

		// Token: 0x040004A4 RID: 1188
		private double ParaX = 1.0;

		// Token: 0x040004A5 RID: 1189
		private bool PageLoaded;

		// Token: 0x040004A6 RID: 1190
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040004A7 RID: 1191
		private int SecondsWait;

		// Token: 0x040004A8 RID: 1192
		private int SecondsCountDown;

		// Token: 0x040004A9 RID: 1193
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		[Serializable]
		private sealed class Class26
		{
			// Token: 0x06000730 RID: 1840 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CE9 RID: 3305
			public static readonly PicMultiple.Class26 instance = new PicMultiple.Class26();

			// Token: 0x04000CEA RID: 3306
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
