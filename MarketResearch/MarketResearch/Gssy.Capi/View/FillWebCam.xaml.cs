using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using WPFMediaKit.DirectShow.Controls;

namespace Gssy.Capi.View
{
	// Token: 0x02000015 RID: 21
	public partial class FillWebCam : Page
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00014408 File Offset: 0x00012608
		public FillWebCam()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00014490 File Offset: 0x00012690
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = (string)this.BtnPhoto.Content;
			this.oQuestion.Init(this.CurPageId, 0);
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
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				this.btnNav.Content = this.btnNav_Content;
				this.oQuestion.FillText = autoFill.Fill(this.oQuestion.QDefine) + global::GClass0.smethod_0("*ũɲͦ");
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			string question_TITLE = this.oQuestion.QDefine.QUESTION_TITLE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, question_TITLE, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (this.cmbList.Items.Count > 0)
			{
				this.cmbList.SelectedIndex = 0;
				this.BtnPhoto.Visibility = Visibility.Visible;
			}
			else
			{
				this.HaveWebCam = false;
			}
			if (!this.HaveWebCam)
			{
				this.PhotoFileName = SurveyMsg.MsgNoCamera;
				this.stkWebCam.Visibility = Visibility.Hidden;
				this.BtnViewPic.Visibility = Visibility.Hidden;
				this.BtnPhoto.Visibility = Visibility.Hidden;
				this.cmbList.Visibility = Visibility.Hidden;
				this.txtDevice.Visibility = Visibility.Hidden;
				MessageBox.Show(SurveyMsg.MsgNoWebCam, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.btnNav.Content = this.btnNav_Content;
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")) && !(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
			{
				navOperation = global::GClass0.smethod_0("NŶɯͱ");
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
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
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000028A1 File Offset: 0x00000AA1
		private void method_1(object sender, RoutedEventArgs e)
		{
			this.videoElement.Stop();
			this.videoElement.Close();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00014B04 File Offset: 0x00012D04
		private void BtnPhoto_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.BtnPhoto.Visibility == Visibility.Visible)
			{
				if (this.cmbList.SelectedValue == null)
				{
					MessageBox.Show(SurveyMsg.MsgNoWebCamConnect, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				this.videoElement.Stop();
				string currentDirectory = Environment.CurrentDirectory;
				string text = string.Format(global::GClass0.smethod_0("YĐɝ̀ѥԯ١݄ࡡऩਢ୮౯൬ูཞၟᄼቴ፫ᑑᕅᙄᜦᡧᥤᨥ᭴ᱵᵸḪὩ⁲Ⅶ"), DateTime.Now, this.MySurveyId, this.oQuestion.QuestionName);
				this.PhotoFileName = text;
				string text2 = Path.Combine(currentDirectory + global::GClass0.smethod_0("ZŕɬͬѶծ"), text);
				RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)this.videoElement.ActualWidth, (int)this.videoElement.ActualHeight, 96.0, 96.0, PixelFormats.Default);
				renderTargetBitmap.Render(this.videoElement);
				JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
				FileStream fileStream = new FileStream(text2, FileMode.Create);
				jpegBitmapEncoder.Save(fileStream);
				fileStream.Close();
				this.PhotoCount++;
				MessageBox.Show(string.Format(SurveyMsg.MsgPhotoSave, text2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.BtnViewPic.Visibility = Visibility.Visible;
				this.BtnPhoto.Visibility = Visibility.Hidden;
				this.cmbList.Visibility = Visibility.Hidden;
				this.txtDevice.Visibility = Visibility.Hidden;
				this.btnNav.Content = this.btnNav_Content;
				this.oQuestion.FillText = this.PhotoFileName;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000028B9 File Offset: 0x00000AB9
		private void method_2(object sender, TouchEventArgs e)
		{
			this.BtnPhoto_Click(sender, e);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000028B9 File Offset: 0x00000AB9
		private void videoElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.BtnPhoto_Click(sender, e);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000028C3 File Offset: 0x00000AC3
		private void BtnViewPic_Click(object sender, RoutedEventArgs e)
		{
			this.BtnViewPic.Visibility = Visibility.Hidden;
			this.BtnPhoto.Visibility = Visibility.Visible;
			this.cmbList.Visibility = Visibility.Visible;
			this.txtDevice.Visibility = Visibility.Visible;
			this.videoElement.Play();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002900 File Offset: 0x00000B00
		private bool method_3()
		{
			if (this.oQuestion.FillText == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotTakePhoto, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00014C80 File Offset: 0x00012E80
		private List<VEAnswer> method_4()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.FillText
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.FillText;
			return list;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002934 File Offset: 0x00000B34
		private void method_5(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00014CEC File Offset: 0x00012EEC
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content == (string)this.BtnPhoto.Content)
			{
				this.BtnPhoto_Click(null, null);
				return;
			}
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_3())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_4();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_5(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
			this.videoElement.Stop();
			this.videoElement.Close();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00014E28 File Offset: 0x00013028
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

		// Token: 0x0600011B RID: 283 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_6(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x0600011C RID: 284 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_8(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x0600011E RID: 286 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002957 File Offset: 0x00000B57
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000166 RID: 358
		private string MySurveyId;

		// Token: 0x04000167 RID: 359
		private string CurPageId;

		// Token: 0x04000168 RID: 360
		private NavBase MyNav = new NavBase();

		// Token: 0x04000169 RID: 361
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400016A RID: 362
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400016B RID: 363
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400016C RID: 364
		private SurveyBiz oSurveyBiz = new SurveyBiz();

		// Token: 0x0400016D RID: 365
		private QFill oQuestion = new QFill();

		// Token: 0x0400016E RID: 366
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400016F RID: 367
		private int SecondsWait;

		// Token: 0x04000170 RID: 368
		private int SecondsCountDown;

		// Token: 0x04000171 RID: 369
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x04000172 RID: 370
		private bool HaveWebCam = true;

		// Token: 0x04000173 RID: 371
		private int PhotoCount;

		// Token: 0x04000174 RID: 372
		private string PhotoFileName = global::GClass0.smethod_0("");
	}
}
