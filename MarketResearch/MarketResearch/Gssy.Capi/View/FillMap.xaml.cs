using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.Entities.BaiduJson;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class FillMap : Page
	{
		public FillMap()
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill.MaxLength = this.oQuestion.QDefine.CONTROL_TYPE;
			}
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, "", true);
				if (list2.Count > 1)
				{
					string_ = list2[1];
					this.oBoldTitle.SetTextBlock(this.txtAfter, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, "", true);
				}
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != "")
			{
				this.txtFill.Text = this.oLogicEngine.stringResult(this.oQuestion.QDefine.PRESET_LOGIC);
				this.txtFill.SelectAll();
			}
			this.txtFill.Focus();
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.txtFill.Text == "")
				{
					this.txtFill.Text = autoFill.Fill(this.oQuestion.QDefine);
				}
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.SecondsCountDown = 2;
					this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
					this.timer.Tick += this.timer_Tick_1;
					this.timer.Start();
				}
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
					{
					}
				}
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.txtFill.Text != "" && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				this.lng = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + "_MapLng");
				this.lat = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + "_MapLat");
			}
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0 && !SurveyHelper.AutoFill)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.Gray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = 1;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded == 2)
			{
				if (this.txtFill.Text != "")
				{
					this.SecondsCountDownSearch = 500;
					this.timerSearch.Interval = TimeSpan.FromMilliseconds(100.0);
					this.timerSearch.Tick += this.timerSearch_Tick;
					this.timerSearch.Start();
				}
				this.PageLoaded++;
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			}
			if (this.PageLoaded == 1)
			{
				if (this.oQuestion.QDefine.DETAIL_ID == "")
				{
					this.strCity = this.oLogicEngine.stringResult("#[" + this.oQuestion.QDefine.PARENT_CODE + "]");
				}
				else
				{
					this.strCity = this.oLogicEngine.stringResult(string.Concat(new string[]
					{
						"$CODETEXT(",
						this.oQuestion.QDefine.DETAIL_ID,
						":",
						this.oQuestion.QDefine.PARENT_CODE,
						")"
					}));
				}
				this.method_12((int)this.scrollNote.ActualWidth, (int)this.RowNote.ActualHeight);
				this.PageLoaded++;
			}
		}

		private void txtFill_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSearch_Click(sender, e);
			}
		}

		private bool method_2()
		{
			string text = this.txtFill.Text.Trim();
			if (this.txtFill.IsEnabled)
			{
				if (text == "")
				{
					System.Windows.MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill.Focus();
					return true;
				}
				text = this.oQuestion.ConvertText(text, this.oQuestion.QDefine.CONTROL_MASK);
				this.txtFill.Text = text;
			}
			if (!this.SearchClick)
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotSearch, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.method_13();
			if ((this.lng == "" || this.lat == "") && System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgNoLngLat, text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
			{
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = text;
			return false;
		}

		private List<VEAnswer> method_3()
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

		private void method_4()
		{
			string text = this.method_11();
			if (text == "")
			{
				text = SurveyMsg.MsgCaptureFail;
			}
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + "_MapPic", text, DateTime.Now);
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + "_MapLng", this.lng, DateTime.Now);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + "_MapLat", this.lat, DateTime.Now);
		}

		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_2())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_3();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_4();
			if (SurveyHelper.Debug)
			{
				System.Windows.MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

		private string method_5(string string_0, int int_0, int int_1 = -9999)
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

		private string method_6(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_7(string string_0, int int_0, int int_1 = -9999)
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

		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_9(string string_0)
		{
			if (string_0 == "")
			{
				return 0;
			}
			if (string_0 == "0")
			{
				return 0;
			}
			if (string_0 == "-0")
			{
				return 0;
			}
			if (!this.method_10(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_10(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		public string CleanString(string string_0)
		{
			return string_0.Replace("　", " ").Trim().Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Replace("＠", "@").Replace("０", "0").Replace("１", "1").Replace("２", "2").Replace("３", "3").Replace("４", "4").Replace("５", "5").Replace("６", "6").Replace("７", "7").Replace("８", "8").Replace("９", "9").Replace("Ａ", "A").Replace("Ｂ", "B").Replace("Ｃ", "C").Replace("Ｄ", "D").Replace("Ｅ", "E").Replace("Ｆ", "F").Replace("Ｇ", "G").Replace("Ｈ", "H").Replace("Ｉ", "I").Replace("Ｊ", "J").Replace("Ｋ", "K").Replace("Ｌ", "L").Replace("Ｍ", "M").Replace("Ｎ", "N").Replace("Ｏ", "O").Replace("Ｐ", "P").Replace("Ｑ", "Q").Replace("Ｒ", "R").Replace("Ｓ", "S").Replace("Ｔ", "T").Replace("Ｕ", "U").Replace("Ｖ", "V").Replace("Ｗ", "W").Replace("Ｘ", "X").Replace("Ｙ", "Y").Replace("Ｚ", "Z").Replace("ａ", "a").Replace("Ｂ", "B").Replace("Ｃ", "C").Replace("Ｄ", "D").Replace("Ｅ", "E").Replace("Ｆ", "F").Replace("Ｇ", "G").Replace("Ｈ", "H").Replace("Ｉ", "I").Replace("Ｊ", "J").Replace("Ｋ", "K").Replace("Ｌ", "L").Replace("Ｍ", "M").Replace("Ｎ", "N").Replace("Ｏ", "O").Replace("Ｐ", "P").Replace("Ｑ", "Q").Replace("Ｒ", "R").Replace("Ｓ", "S").Replace("Ｔ", "T").Replace("Ｕ", "U").Replace("Ｖ", "V").Replace("Ｗ", "W").Replace("Ｘ", "X").Replace("Ｙ", "Y").Replace("ｚ", "z");
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

		private string method_11()
		{
			string text = this.MySurveyId + "_" + this.CurPageId + ".jpg";
			string result = text;
			text = Directory.GetCurrentDirectory() + "\\Photo\\" + text;
			if (!new ScreenCapture().Capture(text, (int)SurveyHelper.Screen_LeftTop))
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotCapture, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return "";
			}
			return result;
		}

		private void c_webBrowser_NewWindow(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		private void method_12(int int_0 = 1280, int int_1 = 600)
		{
			string urlString = Environment.CurrentDirectory + "/Data/intl/FillMap.html";
			this.c_webBrowser.Width = int_0;
			this.c_webBrowser.Height = int_1;
			this.c_webBrowser.ScriptErrorsSuppressed = true;
			this.c_webBrowser.Navigate(urlString);
		}

		private void method_13()
		{
			this.lng = this.c_webBrowser.Document.All["newlng"].GetAttribute("value");
			this.lat = this.c_webBrowser.Document.All["newlat"].GetAttribute("value");
		}

		private void btnSearch_Click(object sender = null, RoutedEventArgs e = null)
		{
			string text = "18";
			if (this.oQuestion.QDefine.NOTE != "")
			{
				text = this.oQuestion.QDefine.NOTE;
			}
			this.txtFill.Text = this.oQuestion.ConvertText(this.txtFill.Text, this.oQuestion.QDefine.CONTROL_MASK);
			string text2 = this.txtFill.Text;
			if (text2.Trim() == "")
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return;
			}
			this.SearchClick = true;
			JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(this.strCity, this.strCity + text2);
			if (geocodingFromAddress.status == 0)
			{
				this.PanelConnet.Visibility = Visibility.Collapsed;
				this.scrollNote.Visibility = Visibility.Visible;
				this.c_webBrowser.Document.InvokeScript("remove_overlay");
				this.c_webBrowser.Document.InvokeScript("AddMovePoint", new object[]
				{
					geocodingFromAddress.result.location.lng,
					geocodingFromAddress.result.location.lat,
					text2,
					text
				});
				return;
			}
			this.PanelConnet.Visibility = Visibility.Visible;
			this.scrollNote.Visibility = Visibility.Collapsed;
			System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, this.strCity + "：" + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.txtFill.Focus();
		}

		private void timerSearch_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDownSearch != 0)
			{
				this.SecondsCountDownSearch -= 100;
				return;
			}
			this.timerSearch.Stop();
			if (!(this.lng == "") && !(this.lat == ""))
			{
				this.method_14();
				return;
			}
			this.btnSearch_Click(null, null);
		}

		private void method_14()
		{
			string text = "18";
			if (this.oQuestion.QDefine.NOTE != "")
			{
				text = this.oQuestion.QDefine.NOTE;
			}
			string text2 = this.txtFill.Text;
			this.SearchClick = true;
			JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(this.strCity, this.strCity + text2);
			if (geocodingFromAddress.status == 0)
			{
				this.PanelConnet.Visibility = Visibility.Collapsed;
				this.scrollNote.Visibility = Visibility.Visible;
				this.c_webBrowser.Document.InvokeScript("remove_overlay");
				this.c_webBrowser.Document.InvokeScript("AddMovePoint", new object[]
				{
					this.lng,
					this.lat,
					text2,
					text
				});
				return;
			}
			this.PanelConnet.Visibility = Visibility.Visible;
			this.scrollNote.Visibility = Visibility.Collapsed;
			System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, this.strCity + "：" + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.txtFill.Focus();
		}

		private void timer_Tick_1(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.timer.Stop();
				this.btnNav_Click(null, null);
				return;
			}
			this.SecondsCountDown--;
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QFill oQuestion = new QFill();

		private int PageLoaded;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private DispatcherTimer timerSearch = new DispatcherTimer();

		private int SecondsCountDownSearch;

		private string strCity = "";

		private string lng = "";

		private string lat = "";

		private bool SearchClick;
	}
}
