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
	public partial class FillWebCam : Page
	{
		public FillWebCam()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = (string)this.BtnPhoto.Content;
			this.oQuestion.Init(this.CurPageId, 0);
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
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				this.btnNav.Content = this.btnNav_Content;
				this.oQuestion.FillText = autoFill.Fill(this.oQuestion.QDefine) + ".jpg";
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			string question_TITLE = this.oQuestion.QDefine.QUESTION_TITLE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, question_TITLE, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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
			if (!(navOperation == "Back") && !(navOperation == "Normal"))
			{
				navOperation = "Jump";
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

		private void method_1(object sender, RoutedEventArgs e)
		{
			this.videoElement.Stop();
			this.videoElement.Close();
		}

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
				string text = string.Format("{1}_{2}_{0:yyy-MM-dd_HH-mm-ss}.jpg", DateTime.Now, this.MySurveyId, this.oQuestion.QuestionName);
				this.PhotoFileName = text;
				string text2 = Path.Combine(currentDirectory + "\\Photo", text);
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

		private void method_2(object sender, TouchEventArgs e)
		{
			this.BtnPhoto_Click(sender, e);
		}

		private void videoElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.BtnPhoto_Click(sender, e);
		}

		private void BtnViewPic_Click(object sender, RoutedEventArgs e)
		{
			this.BtnViewPic.Visibility = Visibility.Hidden;
			this.BtnPhoto.Visibility = Visibility.Visible;
			this.cmbList.Visibility = Visibility.Visible;
			this.txtDevice.Visibility = Visibility.Visible;
			this.videoElement.Play();
		}

		private bool method_3()
		{
			if (this.oQuestion.FillText == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotTakePhoto, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		private List<VEAnswer> method_4()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.FillText
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + "=" + this.oQuestion.FillText;
			return list;
		}

		private void method_5(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

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

		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
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

		private SurveyBiz oSurveyBiz = new SurveyBiz();

		private QFill oQuestion = new QFill();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private bool HaveWebCam = true;

		private int PhotoCount;

		private string PhotoFileName = "";
	}
}
