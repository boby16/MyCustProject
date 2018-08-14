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
	// Token: 0x0200004F RID: 79
	public partial class SurveyCode : Page
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0008F224 File Offset: 0x0008D424
		public SurveyCode()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0008F298 File Offset: 0x0008D498
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			this.CityCode = SurveyHelper.SurveyCity;
			this.SurveyId_City_Length = this.CityCode.Length;
			this.brushOK = this.btnNav.Foreground;
			int num = SurveyMsg.VersionID.IndexOf('v');
			string text = global::GClass0.smethod_0("");
			if (num > -1)
			{
				text = this.oFunc.LEFT(SurveyMsg.VersionID, num);
				if (text == global::GClass0.smethod_0("歠帍灉"))
				{
					this.txtVersion.Visibility = Visibility.Collapsed;
				}
				else
				{
					SurveyHelper.TestVersion = true;
					if (this.oFunc.RIGHT(text, 1) == global::GClass0.smethod_0("牉"))
					{
						text += global::GClass0.smethod_0("札");
					}
					this.txtVersion.Text = text;
				}
			}
			this.oQuestion.Init(this.CurPageId, 0);
			SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
			this.txtCITY.Text = surveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("KŮɲͼѐզٺݵ"));
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
			this.txtFill.Text = global::GClass0.smethod_0("");
			this.SurveyIdBegin = surveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("^ŹɹͼѬձَ݂ࡇॡ੤୫౯"));
			this.SurveyIdEnd = surveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("Xſɻ;Ѣտٌ݀ࡆ६੥"));
			if (this.SurveyIdBegin == global::GClass0.smethod_0("") || this.SurveyIdBegin == null)
			{
				this.SurveyIdBegin = this.CityCode + SurveyMsg.SurveyIDBegin;
				if (SurveyMsg.AllowClearCaseNumber == global::GClass0.smethod_0("XŴɻ͹Ѣ՗ٿݷࡰॢੌ୯౾൩ๅཿၤᅪቢ፴ᑚᕰᙱ᝷ᡤ"))
				{
					this.SurveyIdEnd = this.CityCode + SurveyMsg.SurveyIDEnd;
				}
				else
				{
					this.SurveyIdEnd = this.CityCode + SurveyMsg.SurveyIDBegin;
				}
			}
			this.SurveyIdBegin = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.SurveyIdBegin, SurveyMsg.SurveyId_Length);
			this.SurveyIdEnd = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.SurveyIdEnd, SurveyMsg.SurveyId_Length);
			this.txtMsg.Text = string.Format(SurveyMsg.MsgFrmCodeRange, this.SurveyIdBegin, this.SurveyIdEnd);
			string text2 = this.method_9();
			text2 = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + text2, SurveyMsg.SurveyId_Length);
			string a = global::GClass0.smethod_0("");
			if (text2 != global::GClass0.smethod_0("") && text2 != this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ"), SurveyMsg.SurveyId_Length))
			{
				a = this.method_11(text2, SurveyMsg.CITY_Length);
			}
			string b = global::GClass0.smethod_0("");
			if (this.SurveyIdBegin != global::GClass0.smethod_0(""))
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (text2 == global::GClass0.smethod_0(""))
				{
					text2 = this.SurveyIdBegin;
					this.btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, text2);
					this.btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, text2);
				}
				else
				{
					this.btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, text2);
					text2 = (Convert.ToInt32(text2) + 1).ToString();
					text2 = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + text2, SurveyMsg.SurveyId_Length);
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
				SurveyHelper.Survey_Status = global::GClass0.smethod_0("");
				this.MyStatus = 3;
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000037B8 File Offset: 0x000019B8
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

		// Token: 0x06000526 RID: 1318 RVA: 0x0008F770 File Offset: 0x0008D970
		private void method_1()
		{
			string text = this.txtFill.Text;
			text = text.Trim();
			this.oQuestion.FillText = text;
			if (this.oQuestion.FillText == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtFill.Focus();
				this.method_8();
				return;
			}
			this.oQuestion.FillText = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.oQuestion.FillText, SurveyMsg.SurveyId_Length);
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
					this.txtFill.Text = global::GClass0.smethod_0("");
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
			this.MySurveyId = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.MySurveyId, SurveyMsg.SurveyId_Length);
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
							SurveyHelper.Survey_Status = global::GClass0.smethod_0("EŤɪ͠ѧխ");
							this.method_10(this.txtFill.Text);
							this.method_5(50.0);
							SurveyHelper.NavLoad = 1;
							this.NavLoadPage();
							goto IL_61B;
						}
						this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
						this.txtFill.Text = global::GClass0.smethod_0("");
						this.txtFill.Focus();
						this.MyStatus = 1;
						this.txtMsgBar.Text = global::GClass0.smethod_0("");
						this.method_5(1.0);
						this.method_8();
						return;
					}
				}
				if (!MessageBox.Show(SurveyMsg.MsgHaveFinished, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
				{
					this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
					this.txtFill.Text = global::GClass0.smethod_0("");
					this.txtFill.Focus();
					this.MyStatus = 1;
					this.txtMsgBar.Text = global::GClass0.smethod_0("");
					this.method_5(1.0);
					this.method_8();
					return;
				}
				this.method_10(this.txtFill.Text);
				this.txtMsgBar.Text = SurveyMsg.MsgFrmQueryID;
				this.method_5(60.0);
				string uriString = global::GClass0.smethod_0("\\Ŋɉ͂ВԈ؉݄ࡔ॓੎ୈృൾ๪ུၳᅵሠጵᐴᔻᘹᝃ᡽᥶ᩥᬾ᱃ᵺṼύ⁩ⅲ≛⍼⑭╵♿✫⡼⥢⩯⭭");
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
				SurveyHelper.NavCurPage = global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸");
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

		// Token: 0x06000527 RID: 1319 RVA: 0x0008FDAC File Offset: 0x0008DFAC
		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
			if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
			{
				uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
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
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0008FEDC File Offset: 0x0008E0DC
		public void NavLoadPage()
		{
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.LoadPage(this.MySurveyId, roadMapVersion);
			this.MySurveyId = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.MySurveyId, SurveyMsg.SurveyId_Length);
			SurveyHelper.SurveyID = this.MySurveyId;
			SurveyHelper.SurveyCity = this.MyNav.Survey.CITY_ID;
			SurveyHelper.CircleACount = this.MyNav.Survey.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = this.MyNav.Survey.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = this.MyNav.Survey.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = this.MyNav.Survey.CIRCLE_B_CURRENT;
			this.oSurvey.UpdateSurveyLastTime(this.MySurveyId);
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
			if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
			{
				uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
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
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavGoBackTimes = 0;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000900D4 File Offset: 0x0008E2D4
		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnBack.Foreground != this.brushOK)
			{
				return;
			}
			this.MySurveyId = global::GClass0.smethod_0("");
			this.MyStatus = 1;
			this.txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
			this.txtFill.Text = global::GClass0.smethod_0("");
			this.txtFill.Focus();
			this.btnBack.Visibility = Visibility.Collapsed;
			this.btnBack.Width = 5.0;
			this.btnAuto.Visibility = Visibility.Visible;
			this.btnLast.Visibility = Visibility.Visible;
			this.method_8();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0009017C File Offset: 0x0008E37C
		private void txtFill_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
			TextBox textBox = sender as TextBox;
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				if (textBox.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.Decimal)
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
				if (textBox.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.OemPeriod)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00090250 File Offset: 0x0008E450
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

		// Token: 0x0600052C RID: 1324 RVA: 0x000902D4 File Offset: 0x0008E4D4
		private void method_3()
		{
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(global::GClass0.smethod_0("#Ġȡ̦ЧԤإܪࠫ॑ੱୱ౧൶ะ停䉃襸巭獤ᐷᔴᘵ᜺ᠻᤸᨹᬾ᰿ᴼ") + Environment.NewLine + global::GClass0.smethod_0("录午驱̣иԡ") + this.CurPageId + Environment.NewLine + global::GClass0.smethod_0("额勳ȣ̸С") + this.oQuestion.QuestionName + Environment.NewLine + global::GClass0.smethod_0("*īȨ̩Юԯجܭ࠲म煙捄ఫഷิ်༵ᄻሸጹᐾᔿᘼ") + Environment.NewLine + global::GClass0.smethod_0("塮签ȣ̸С") + this.oQuestion.FillText + Environment.NewLine + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00090380 File Offset: 0x0008E580
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
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, global::GClass0.smethod_0("GŊɖ͘"), surveyCity);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, global::GClass0.smethod_0("WŅɚ͇Ѭզ٤"), this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ")));
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

		// Token: 0x0600052E RID: 1326 RVA: 0x00090504 File Offset: 0x0008E704
		private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			double double_ = Convert.ToDouble(e.ProgressPercentage);
			this.method_5(double_);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000037E8 File Offset: 0x000019E8
		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.method_5(100.0);
			this.txtMsgBar.Text = SurveyMsg.MsgFrmCodeCreate;
			this.btnNav.IsEnabled = true;
			this.method_2();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00090524 File Offset: 0x0008E724
		private void method_4()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000381B File Offset: 0x00001A1B
		private void method_5(double double_0)
		{
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				double_0
			});
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00090574 File Offset: 0x0008E774
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

		// Token: 0x06000533 RID: 1331 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00090610 File Offset: 0x0008E810
		private void timer_Tick(object sender, EventArgs e)
		{
			this.txtDate.Text = SurveyMsg.MsgFrmCodeNow + DateTime.Now.ToString();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00090640 File Offset: 0x0008E840
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

		// Token: 0x06000537 RID: 1335 RVA: 0x000906A4 File Offset: 0x0008E8A4
		private void btnAuto_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnAuto.Foreground != this.brushOK)
			{
				return;
			}
			this.method_7();
			this.SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			this.MySurveyId = this.method_9();
			string a = global::GClass0.smethod_0("");
			if (this.MySurveyId != global::GClass0.smethod_0(""))
			{
				a = this.method_11(this.MySurveyId, SurveyMsg.CITY_Length);
			}
			string b = global::GClass0.smethod_0("");
			if (this.SurveyIdBegin != global::GClass0.smethod_0(""))
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (this.MySurveyId == global::GClass0.smethod_0(""))
				{
					this.MySurveyId = this.SurveyIdBegin;
				}
				else
				{
					this.MySurveyId = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + (Convert.ToInt32(this.MySurveyId) + 1).ToString(), SurveyMsg.SurveyId_Length);
				}
			}
			else
			{
				this.MySurveyId = this.SurveyIdBegin;
			}
			this.MySurveyId = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.MySurveyId, SurveyMsg.SurveyId_Length);
			this.txtFill.Text = this.MySurveyId;
			this.MyStatus = 3;
			this.method_1();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00090800 File Offset: 0x0008EA00
		private void btnLast_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnLast.Foreground != this.brushOK)
			{
				return;
			}
			this.method_7();
			this.SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			this.MySurveyId = this.method_9();
			string a = global::GClass0.smethod_0("");
			if (this.MySurveyId != global::GClass0.smethod_0(""))
			{
				a = this.method_11(this.MySurveyId, SurveyMsg.CITY_Length);
			}
			string b = global::GClass0.smethod_0("");
			if (this.SurveyIdBegin != global::GClass0.smethod_0(""))
			{
				b = this.method_11(this.SurveyIdBegin, SurveyMsg.CITY_Length);
			}
			if (a == b)
			{
				if (this.MySurveyId == global::GClass0.smethod_0(""))
				{
					this.MySurveyId = this.SurveyIdBegin;
				}
			}
			else
			{
				this.MySurveyId = this.SurveyIdBegin;
			}
			this.MySurveyId = this.method_13(global::GClass0.smethod_0("=ļȻ̺йԸطܶ࠵ऴਲ਼ଲఱ") + this.MySurveyId, SurveyMsg.SurveyId_Length);
			this.txtFill.Text = this.MySurveyId;
			this.MyStatus = 3;
			this.method_1();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00090924 File Offset: 0x0008EB24
		private void method_7()
		{
			this.btnAuto.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnLast.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnOK.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnNav.Foreground = new SolidColorBrush(Colors.Gray);
			this.btnExit.Foreground = new SolidColorBrush(Colors.Gray);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0009099C File Offset: 0x0008EB9C
		private void method_8()
		{
			this.btnAuto.Foreground = this.brushOK;
			this.btnLast.Foreground = this.brushOK;
			this.btnOK.Foreground = this.brushOK;
			this.btnNav.Foreground = this.brushOK;
			this.btnExit.Foreground = this.brushOK;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00003857 File Offset: 0x00001A57
		private string method_9()
		{
			return this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("@Ūɹͽћղٴݳࡡॺੋ୥"));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000386E File Offset: 0x00001A6E
		private void method_10(string string_0)
		{
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("@Ūɹͽћղٴݳࡡॺੋ୥"), string_0);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600053F RID: 1343 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_13(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0400098E RID: 2446
		private string MySurveyId;

		// Token: 0x0400098F RID: 2447
		private string CurPageId;

		// Token: 0x04000990 RID: 2448
		private string CityCode;

		// Token: 0x04000991 RID: 2449
		private Brush brushOK;

		// Token: 0x04000992 RID: 2450
		private NavBase MyNav = new NavBase();

		// Token: 0x04000993 RID: 2451
		private QFill oQuestion = new QFill();

		// Token: 0x04000994 RID: 2452
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x04000995 RID: 2453
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x04000996 RID: 2454
		private UDPX oFunc = new UDPX();

		// Token: 0x04000997 RID: 2455
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000998 RID: 2456
		private int MyStatus;

		// Token: 0x04000999 RID: 2457
		private int SurveyId_Length;

		// Token: 0x0400099A RID: 2458
		private int SurveyId_City_Length;

		// Token: 0x0400099B RID: 2459
		private int SurveyId_Number_Length;

		// Token: 0x0400099C RID: 2460
		private RandomBiz oRandom = new RandomBiz();

		// Token: 0x0400099D RID: 2461
		private bool IsRandomOK;

		// Token: 0x0400099E RID: 2462
		private BackgroundWorker bw;

		// Token: 0x0400099F RID: 2463
		private string SurveyIdBegin;

		// Token: 0x040009A0 RID: 2464
		private string SurveyIdEnd;

		// Token: 0x040009A1 RID: 2465
		private DispatcherTimer timer = new DispatcherTimer();
	}
}
