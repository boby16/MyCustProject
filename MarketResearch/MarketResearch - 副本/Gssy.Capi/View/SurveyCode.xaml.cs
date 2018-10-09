using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;

namespace Gssy.Capi.View
{
	public partial class SurveyCode : Page
	{
		public SurveyCode()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			this.CityCode = SurveyHelper.SurveyCity;
			this.SurveyId_City_Length = this.CityCode.Length;
			this.brushOK = this.btnNav.Foreground;
			int num = SurveyMsg.VersionID.IndexOf('v');
			string text = "";
			if (num > -1)
			{
				text = this.oFunc.LEFT(SurveyMsg.VersionID, num);
				if (text == "正式版")
				{
					this.txtVersion.Visibility = Visibility.Collapsed;
				}
				else
				{
					SurveyHelper.TestVersion = true;
					if (this.oFunc.RIGHT(text, 1) == "版")
					{
						text += "本";
					}
					this.txtVersion.Text = text;
				}
			}
			this.oQuestion.Init(this.CurPageId, 0);
			SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
			this.txtCITY.Text = surveyConfigBiz.GetByCodeText("CityText");
			this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
			this.timer.Tick += this.timer_Tick;
			this.timer.Start();
			this.SurveyId_Number_Length = SurveyMsg.Order_Length;
			this.SurveyId_Length = SurveyMsg.SurveyId_Length;
			if (this.SurveyId_Length > 0)
			{
				this.txtFill.MaxLength = this.SurveyId_Length;
				if (this.SurveyId_Length > 5)
				{
					this.btnLast.Width = 350.0;
					this.txtFill.Width = 350.0;
					this.btnAuto.Width = 350.0;
				}
			}
			this.MyStatus = 1;
			this.txtQuestionTitle.Text = this.oQuestion.QDefine.QUESTION_TITLE;
			this.txtFill.Focus();
			this.txtFill.Text = "";
			this.SurveyIdBegin = surveyConfigBiz.GetByCodeText("SurveyIDBegin");
			this.SurveyIdEnd = surveyConfigBiz.GetByCodeText("SurveyIDEnd");
			if (this.SurveyIdBegin == "" || this.SurveyIdBegin == null)
			{
				this.SurveyIdBegin = this.CityCode + SurveyMsg.SurveyIDBegin;
				if (SurveyMsg.AllowClearCaseNumber == "AllowClearCaseNumber_true")
				{
					this.SurveyIdEnd = this.CityCode + SurveyMsg.SurveyIDEnd;
				}
				else
				{
					this.SurveyIdEnd = this.CityCode + SurveyMsg.SurveyIDBegin;
				}
			}
			this.SurveyIdBegin = this.method_13("0000000000000" + this.SurveyIdBegin, SurveyMsg.SurveyId_Length);
			this.SurveyIdEnd = this.method_13("0000000000000" + this.SurveyIdEnd, SurveyMsg.SurveyId_Length);
			this.txtMsg.Text = string.Format(SurveyMsg.MsgFrmCodeRange, this.SurveyIdBegin, this.SurveyIdEnd);
			string text2 = this.method_9();
			text2 = this.method_13("0000000000000" + text2, SurveyMsg.SurveyId_Length);
			string a = "";
			if (text2 != "" && text2 != this.method_13("0000000000000", SurveyMsg.SurveyId_Length))
			{
				a = this.method_11(text2, SurveyMsg.CITY_Length);
			}
			string b = "";
			if (this.SurveyIdBegin != "")
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (text2 == "")
				{
					text2 = this.SurveyIdBegin;
					this.btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, text2);
					this.btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, text2);
				}
				else
				{
					this.btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, text2);
					text2 = (Convert.ToInt32(text2) + 1).ToString();
					text2 = this.method_13("0000000000000" + text2, SurveyMsg.SurveyId_Length);
					this.btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, text2);
				}
			}
			else
			{
				this.btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, this.SurveyIdBegin);
				this.btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, this.SurveyIdBegin);
			}
			if (SurveyHelper.AutoDo)
			{
				this.txtFill.Text = SurveyHelper.SurveyID;
				SurveyHelper.Survey_Status = "";
				this.MyStatus = 3;
				this.btnNav_Click(sender, e);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnNav.Foreground != this.brushOK)
			{
				return;
			}
			if (this.btnOK.Foreground != this.brushOK)
			{
				return;
			}
			this.method_1();
		}

		private void method_1()
		{
			string text = this.txtFill.Text;
			text = text.Trim();
			this.oQuestion.FillText = text;
			if (this.oQuestion.FillText == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtFill.Focus();
				this.method_8();
				return;
			}
			this.oQuestion.FillText = this.method_13("0000000000000" + this.oQuestion.FillText, SurveyMsg.SurveyId_Length);
			this.txtFill.Text = this.oQuestion.FillText;
			if (this.MyStatus == 1 || this.MyStatus == 3)
			{
				bool flag = true;
				if (this.oQuestion.FillText.Length != this.SurveyId_Length)
				{
					this.MyStatus = 1;
					MessageBox.Show(string.Format(SurveyMsg.MsgFrmCodeLen, this.SurveyId_Length.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtFill.Focus();
					this.method_8();
					return;
				}
				if (this.oSurvey.ExistSurvey(this.oQuestion.FillText))
				{
					this.oSurvey.GetBySurveyId(this.oQuestion.FillText);
					if (this.oSurvey.MySurvey.IS_FINISH == 1 || this.oSurvey.MySurvey.IS_FINISH == 2)
					{
						flag = false;
					}
				}
				if (flag && this.oQuestion.FillText.Substring(0, this.SurveyId_City_Length) != this.CityCode)
				{
					this.MyStatus = 1;
					MessageBox.Show(string.Format(SurveyMsg.MsgFrmCodeNotSame, this.txtQuestionTitle.Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtFill.Focus();
					this.method_8();
					return;
				}
				if (flag && (Convert.ToInt32(this.oQuestion.FillText) < Convert.ToInt32(this.SurveyIdBegin) || Convert.ToInt32(this.oQuestion.FillText) > Convert.ToInt32(this.SurveyIdEnd)))
				{
					this.MyStatus = 1;
					MessageBox.Show(SurveyMsg.MsgIdOutRange, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtFill.Focus();
					this.method_8();
					return;
				}
				if (this.MyStatus == 1)
				{
					this.MySurveyId = this.oQuestion.FillText;
					this.MyStatus = 2;
					this.btnBack.Width = 160.0;
					this.btnBack.Visibility = Visibility.Visible;
					this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCodeConfirm;
					this.txtFill.Text = "";
					this.btnAuto.Visibility = Visibility.Hidden;
					this.btnLast.Visibility = Visibility.Hidden;
					this.txtFill.Focus();
					return;
				}
			}
			if (this.MyStatus == 2 && this.oQuestion.FillText != this.MySurveyId)
			{
				MessageBox.Show(SurveyMsg.MsgIdNotSame, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return;
			}
			this.method_4();
			this.method_5(20.0);
			this.MySurveyId = this.method_13("0000000000000" + this.MySurveyId, SurveyMsg.SurveyId_Length);
			SurveyHelper.SurveyID = this.MySurveyId;
			if (this.oSurvey.ExistSurvey(this.MySurveyId))
			{
				this.oSurvey.GetBySurveyId(this.MySurveyId);
				if (this.oSurvey.MySurvey.IS_FINISH != 1)
				{
					if (this.oSurvey.MySurvey.IS_FINISH != 2)
					{
						MessageBoxResult messageBoxResult = MessageBoxResult.Yes;
						if (!SurveyHelper.AutoDo)
						{
							messageBoxResult = MessageBox.Show(SurveyMsg.MsgNotFinished, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
						}
						if (messageBoxResult.Equals(MessageBoxResult.Yes))
						{
							SurveyHelper.Survey_Status = "Cancel";
							this.method_10(this.txtFill.Text);
							this.method_5(50.0);
							SurveyHelper.NavLoad = 1;
							this.NavLoadPage();
							goto IL_61B;
						}
						this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
						this.txtFill.Text = "";
						this.txtFill.Focus();
						this.MyStatus = 1;
						this.txtMsgBar.Text = "";
						this.method_5(1.0);
						this.method_8();
						return;
					}
				}
				if (!MessageBox.Show(SurveyMsg.MsgHaveFinished, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
				{
					this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
					this.txtFill.Text = "";
					this.txtFill.Focus();
					this.MyStatus = 1;
					this.txtMsgBar.Text = "";
					this.method_5(1.0);
					this.method_8();
					return;
				}
				this.method_10(this.txtFill.Text);
				this.txtMsgBar.Text = SurveyMsg.MsgFrmQueryID;
				this.method_5(60.0);
				string uriString = "pack://application:,,,/View/SurveyQuery.xaml";
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
				SurveyHelper.NavCurPage = "SurveyQuery";
			}
			else
			{
				this.method_10(this.txtFill.Text);
				this.method_7();
				this.txtMsgBar.Text = SurveyMsg.MsgNewSurvey;
				this.method_5(30.0);
				this.bw = new BackgroundWorker();
				this.bw.DoWork += this.bw_DoWork;
				this.bw.ProgressChanged += this.bw_ProgressChanged;
				this.bw.RunWorkerCompleted += this.bw_RunWorkerCompleted;
				this.bw.WorkerReportsProgress = true;
				this.bw.RunWorkerAsync();
				this.method_5(50.0);
			}
			IL_61B:
			this.method_6();
			this.timer.Stop();
		}

		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == "A")
			{
				uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			}
			if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavLoad = 0;
		}

		public void NavLoadPage()
		{
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.LoadPage(this.MySurveyId, roadMapVersion);
			this.MySurveyId = this.method_13("0000000000000" + this.MySurveyId, SurveyMsg.SurveyId_Length);
			SurveyHelper.SurveyID = this.MySurveyId;
			SurveyHelper.SurveyCity = this.MyNav.Survey.CITY_ID;
			SurveyHelper.CircleACount = this.MyNav.Survey.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = this.MyNav.Survey.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = this.MyNav.Survey.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = this.MyNav.Survey.CIRCLE_B_CURRENT;
			this.oSurvey.UpdateSurveyLastTime(this.MySurveyId);
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == "A")
			{
				uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			}
			if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = this.MyNav.Survey.SEQUENCE_ID;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
			SurveyHelper.NavLoad = 1;
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavGoBackTimes = 0;
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnBack.Foreground != this.brushOK)
			{
				return;
			}
			this.MySurveyId = "";
			this.MyStatus = 1;
			this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
			this.txtFill.Text = "";
			this.txtFill.Focus();
			this.btnBack.Visibility = Visibility.Collapsed;
			this.btnBack.Width = 5.0;
			this.btnAuto.Visibility = Visibility.Visible;
			this.btnLast.Visibility = Visibility.Visible;
			this.method_8();
		}

		private void txtFill_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
			TextBox textBox = sender as TextBox;
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				if (textBox.Text.Contains(".") && e.Key == Key.Decimal)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
			else
			{
				if (e.Key < Key.D0 || e.Key > Key.D9 || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
				{
					e.Handled = true;
					return;
				}
				if (textBox.Text.Contains(".") && e.Key == Key.OemPeriod)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
		}

		private void txtFill_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double num = 0.0;
				if (!double.TryParse(textBox.Text, out num))
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		private void method_3()
		{
			if (SurveyHelper.Debug)
			{
				MessageBox.Show("=========Debug 当前页信息==========" + Environment.NewLine 
                    + "当前页 : " + this.CurPageId + Environment.NewLine 
                    + "题号 : " + this.oQuestion.QuestionName + Environment.NewLine 
                    + "========= 答案 ==========" + Environment.NewLine 
                    + "填空 : " + this.oQuestion.FillText + Environment.NewLine 
                    + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			this.IsRandomOK = this.oRandom.RandomSurveyMain(this.MySurveyId);
			this.bw.ReportProgress(70);
			string versionID = SurveyMsg.VersionID;
			string surveyCity = SurveyHelper.SurveyCity;
			string surveyExtend = SurveyHelper.SurveyExtend1;
			string projectId = SurveyMsg.ProjectId;
			string clientId = SurveyMsg.ClientId;
			this.oSurvey.AddSurvey(this.MySurveyId, versionID, surveyCity, projectId, clientId, surveyExtend);
			this.oQuestion.FillText = this.MySurveyId;
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, "CITY", surveyCity);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, "PC_Code", this.oSurveyConfigBiz.GetByCodeText("PCCode"));
			if (!this.IsRandomOK)
			{
				MessageBox.Show(SurveyMsg.MsgErrorSysSlow, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.oRandom.DeleteRandom(this.MySurveyId);
				Thread.Sleep(1000);
				Application.Current.Shutdown();
				return;
			}
			if (this.oSurvey.GetCityCode(this.MySurveyId) != null && this.oSurvey.ExistSurvey(this.MySurveyId))
			{
				return;
			}
			MessageBox.Show(string.Format(SurveyMsg.MsgFrmCodeFailCreate, this.MySurveyId), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			Application.Current.Shutdown();
		}

		private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			double double_ = Convert.ToDouble(e.ProgressPercentage);
			this.method_5(double_);
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.method_5(100.0);
			this.txtMsgBar.Text = SurveyMsg.MsgFrmCodeCreate;
			this.btnNav.IsEnabled = true;
			this.method_2();
		}

		private void method_4()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		private void method_5(double double_0)
		{
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				double_0
			});
		}

		private void method_6()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = new RepeatBehavior(1.0);
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				100.0
			});
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

		private void timer_Tick(object sender, EventArgs e)
		{
			this.txtDate.Text = SurveyMsg.MsgFrmCodeNow + DateTime.Now.ToString();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnExit.Foreground != this.brushOK)
			{
				return;
			}
			this.method_7();
			if (MessageBox.Show(SurveyMsg.MsgConfirmExit, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
			{
				Application.Current.Shutdown();
				return;
			}
			this.method_8();
		}

		private void btnAuto_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnAuto.Foreground != this.brushOK)
			{
				return;
			}
			this.method_7();
			this.SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			this.MySurveyId = this.method_9();
			string a = "";
			if (this.MySurveyId != "")
			{
				a = this.method_11(this.MySurveyId, SurveyMsg.CITY_Length);
			}
			string b = "";
			if (this.SurveyIdBegin != "")
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (this.MySurveyId == "")
				{
					this.MySurveyId = this.SurveyIdBegin;
				}
				else
				{
					this.MySurveyId = this.method_13("0000000000000" + (Convert.ToInt32(this.MySurveyId) + 1).ToString(), SurveyMsg.SurveyId_Length);
				}
			}
			else
			{
				this.MySurveyId = this.SurveyIdBegin;
			}
			this.MySurveyId = this.method_13("0000000000000" + this.MySurveyId, SurveyMsg.SurveyId_Length);
			this.txtFill.Text = this.MySurveyId;
			this.MyStatus = 3;
			this.method_1();
		}

		private void btnLast_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnLast.Foreground != this.brushOK)
			{
				return;
			}
			this.method_7();
			this.SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			this.MySurveyId = this.method_9();
			string a = "";
			if (this.MySurveyId != "")
			{
				a = this.method_11(this.MySurveyId, SurveyMsg.CITY_Length);
			}
			string b = "";
			if (this.SurveyIdBegin != "")
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (this.MySurveyId == "")
				{
					this.MySurveyId = this.SurveyIdBegin;
				}
			}
			else
			{
				this.MySurveyId = this.SurveyIdBegin;
			}
			this.MySurveyId = this.method_13("0000000000000" + this.MySurveyId, SurveyMsg.SurveyId_Length);
			this.txtFill.Text = this.MySurveyId;
			this.MyStatus = 3;
			this.method_1();
		}

		private void method_7()
		{
			this.btnAuto.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnLast.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnOK.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnNav.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnExit.Foreground = new SolidColorBrush(Colors.Gray);
		}

		private void method_8()
		{
			this.btnAuto.Foreground = this.brushOK;
			this.btnLast.Foreground = this.brushOK;
			this.btnOK.Foreground = this.brushOK;
			this.btnNav.Foreground = this.brushOK;
			this.btnExit.Foreground = this.brushOK;
		}

		private string method_9()
		{
			return this.oSurveyConfigBiz.GetByCodeText("LastSurveyId");
		}

		private void method_10(string string_0)
		{
			this.oSurveyConfigBiz.Save("LastSurveyId", string_0);
		}

		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_12(string string_0, int int_0, int int_1 = -9999)
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

		private string method_13(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private string MySurveyId;

		private string CurPageId;

		private string CityCode;

		private Brush brushOK;

		private NavBase MyNav = new NavBase();

		private QFill oQuestion = new QFill();

		private SurveyBiz oSurvey = new SurveyBiz();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();

		private PageNav oPageNav = new PageNav();

		private int MyStatus;

		private int SurveyId_Length;

		private int SurveyId_City_Length;

		private int SurveyId_Number_Length;

		private RandomBiz oRandom = new RandomBiz();

		private bool IsRandomOK;

		private BackgroundWorker bw;

		private string SurveyIdBegin;

		private string SurveyIdEnd;

		private DispatcherTimer timer = new DispatcherTimer();
	}
}
