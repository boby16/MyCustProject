using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class SingleQuota : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__20_0;

			internal int _003F322_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003F12_003F : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public string serviceAddress;

			public List<jquotaanswer> Myqanswer;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private TaskAwaiter<string> _003C_003Eu__2;

			private void MoveNext()
			{
				//IL_004f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0050: Expected O, but got Unknown
				int num = _003C_003E1__state;
				try
				{
					Uri requestUri = default(Uri);
					HttpClient httpClient = default(HttpClient);
					HttpContent content = default(HttpContent);
					if (num != 0 && num != 1)
					{
						requestUri = new Uri(serviceAddress);
						httpClient = new HttpClient();
						new MediaTypeHeaderValue(_003F487_003F._003F488_003F("qſɾ\u0361ѥը٫ݽ\u0861२੨ପ౮൰\u0e6d\u0f6f"));
						JsonMediaTypeFormatter val = new JsonMediaTypeFormatter();
						content = (HttpContent)new ObjectContent<List<jquotaanswer>>(Myqanswer, val)
						{
							Headers = 
							{
								ContentType = new MediaTypeHeaderValue(_003F487_003F._003F488_003F("qſɾ\u0361ѥը٫ݽ\u0861२੨ପ౮൰\u0e6d\u0f6f"))
							}
						};
					}
					try
					{
						HttpResponseMessage result;
						TaskAwaiter<HttpResponseMessage> awaiter2;
						TaskAwaiter<string> awaiter;
						switch (num)
						{
						default:
							awaiter2 = httpClient.PostAsync(requestUri, content).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0102;
						case 0:
							awaiter2 = _003C_003Eu__1;
							_003C_003Eu__1 = default(TaskAwaiter<HttpResponseMessage>);
							num = (_003C_003E1__state = -1);
							goto IL_0102;
						case 1:
							{
								awaiter = _003C_003Eu__2;
								_003C_003Eu__2 = default(TaskAwaiter<string>);
								num = (_003C_003E1__state = -1);
								break;
							}
							IL_0102:
							result = awaiter2.GetResult();
							awaiter2 = default(TaskAwaiter<HttpResponseMessage>);
							result.EnsureSuccessStatusCode();
							awaiter = result.Content.ReadAsStringAsync().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							break;
						}
						string result2 = awaiter.GetResult();
						awaiter = default(TaskAwaiter<string>);
						JsonConvert.DeserializeObject<jresponse>(result2);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine _003F484_003F)
			{
				_003C_003Et__builder.SetStateMachine(_003F484_003F);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine _003F484_003F)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(_003F484_003F);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QSingle oQuestion = new QSingle();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal Grid gridContent;

		internal ColumnDefinition PicWidth;

		internal ColumnDefinition ButtonWidth;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public SingleQuota()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_0ad8: Incompatible stack heights: 0 vs 2
			//IL_0aef: Incompatible stack heights: 0 vs 1
			//IL_0e5a: Incompatible stack heights: 0 vs 1
			//IL_0e79: Incompatible stack heights: 0 vs 1
			//IL_0e7e: Incompatible stack heights: 0 vs 1
			//IL_0ed6: Incompatible stack heights: 0 vs 1
			//IL_0ef5: Incompatible stack heights: 0 vs 1
			//IL_0efa: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
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
			List<string> list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list3[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list3.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text3 = list3[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_057e: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			string text = _003F487_003F._003F488_003F("");
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				text = oLogicEngine.Route(oQuestion.QDefine.CONTROL_TOOLTIP);
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
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == text2)
							{
								text = oLogicEngine.Route(current.EXTEND_1);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			if (text != _003F487_003F._003F488_003F(""))
			{
				string uriString = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
				Image image = new Image();
				if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("+"))
				{
					PicWidth.Width = new GridLength(1.0, GridUnitType.Star);
					ButtonWidth.Width = GridLength.Auto;
				}
				else
				{
					int num = _003F96_003F(oQuestion.QDefine.CONTROL_MASK);
					if (num > 0)
					{
						image.Width = (double)num;
					}
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 10.0, 20.0, 10.0);
				image.SetValue(Grid.ColumnProperty, 0);
				image.SetValue(Grid.RowProperty, 0);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				try
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(uriString, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					gridContent.Children.Add(image);
				}
				catch (Exception)
				{
				}
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array[i].ToString())
							{
								list4.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (oQuestion.QDefine.SHOW_LOGIC == _003F487_003F._003F488_003F("") && oQuestion.QDefine.IS_RANDOM == 0)
				{
					if (_003F7_003F._003C_003E9__20_0 == null)
					{
						_003F7_003F._003C_003E9__20_0 = _003F7_003F._003C_003E9._003F322_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0af4: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0af4: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count(); j++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								listPreSet.Add(array2[j]);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int k = 0; k < oQuestion.QDetails.Count(); k++)
				{
					oQuestion.QDetails[k].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[k].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count(); l++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == array3[l].ToString())
							{
								list5.Add(current3);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				oQuestion.QDetails = list5;
			}
			else if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails();
			}
			Button_Type = oQuestion.QDefine.CONTROL_TYPE;
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				int cONTROL_FONTSIZE = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			else
			{
				int btnFontSize = SurveyHelper.BtnFontSize;
			}
			((SingleQuota)/*Error near IL_0e83: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0e83: Stack underflow*/;
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_FontSize = Math.Abs(Button_FontSize);
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((SingleQuota)/*Error near IL_0eff: Stack underflow*/).Button_Height = (int)/*Error near IL_0eff: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440;
				}
				else
				{
					Button_Width = SurveyHelper.BtnWidth;
				}
			}
			else
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			_003F28_003F();
			if (ExistTextFill)
			{
				txtFill.Visibility = Visibility.Visible;
				if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F(""))
				{
					txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					qUESTION_TITLE = oQuestion.QDefine.NOTE;
					list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list3[0];
					oBoldTitle.SetTextBlock(txtFillTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list3.Count > 1)
					{
						qUESTION_TITLE = list3[1];
						oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					}
				}
			}
			else
			{
				txtFill.Height = 0.0;
				txtFillTitle.Height = 0.0;
				txtAfter.Height = 0.0;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				Button button = autoFill.SingleButton(oQuestion.QDefine, listButton);
				if (button != null)
				{
					if (listPreSet.Count == 0)
					{
						_003F29_003F(button, new RoutedEventArgs());
					}
					if (txtFill.IsEnabled)
					{
						txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
					}
					if (autoFill.AutoNext(oQuestion.QDefine))
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			bool flag = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else
				{
					if (listPreSet.Count > 0)
					{
						oQuestion.SelectedCode = listPreSet[0];
						foreach (Button child in wrapPanel1.Children)
						{
							if (child.Name.Substring(2) == oQuestion.SelectedCode)
							{
								child.Style = style;
								int num2 = (int)child.Tag;
								if (num2 == 1 || num2 == 3 || num2 == 5 || ((num2 == 11) | (num2 == 13)) || num2 == 14)
								{
									flag = true;
								}
								break;
							}
						}
						if (flag)
						{
							txtFill.IsEnabled = true;
							txtFill.Background = Brushes.White;
						}
					}
					if (oQuestion.QDetails.Count == 1)
					{
						if (listPreSet.Count == 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							_003F29_003F(listButton[0], new RoutedEventArgs());
						}
						if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (txtFill.IsEnabled)
							{
								txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								_003F58_003F(this, _003F348_003F);
							}
						}
					}
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
					{
						if (txtFill.IsEnabled)
						{
							txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							_003F58_003F(this, _003F348_003F);
						}
					}
				}
			}
			else
			{
				oQuestion.SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				foreach (Button child2 in wrapPanel1.Children)
				{
					if (child2.Name.Substring(2) == oQuestion.SelectedCode)
					{
						child2.Style = style;
						int num3 = (int)child2.Tag;
						if (num3 == 1 || num3 == 3 || num3 == 5 || ((num3 == 11) | (num3 == 13)) || num3 == 14)
						{
							flag = true;
						}
						break;
					}
				}
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				if (flag)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_01b6: Incompatible stack heights: 0 vs 1
			//IL_01c6: Incompatible stack heights: 0 vs 1
			//IL_01d7: Incompatible stack heights: 0 vs 2
			//IL_01ea: Incompatible stack heights: 0 vs 2
			//IL_01fd: Incompatible stack heights: 0 vs 2
			//IL_0213: Incompatible stack heights: 0 vs 1
			//IL_021f: Incompatible stack heights: 0 vs 1
			//IL_0224: Incompatible stack heights: 1 vs 0
			//IL_0235: Incompatible stack heights: 0 vs 2
			//IL_0255: Incompatible stack heights: 0 vs 2
			if (!PageLoaded)
			{
				return;
			}
			WrapPanel wrapPanel = wrapPanel1;
			ScrollViewer scrollViewer = scrollmain;
			if (((SingleQuota)/*Error near IL_0010: Stack underflow*/).Button_Type < 1)
			{
				int button_Type = Button_Type;
				if ((int)/*Error near IL_01cb: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_01dc: Stack underflow*/ == /*Error near IL_01dc: Stack underflow*/)
					{
						Button_Type = 2;
						((SingleQuota)/*Error near IL_0025: Stack underflow*/).PageLoaded = ((byte)/*Error near IL_0025: Stack underflow*/ != 0);
					}
					else
					{
						int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(Button_Height + 15));
						int num2 = Convert.ToInt32((double)(oQuestion.QDetails.Count / num) + 0.99999999);
						int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - oQuestion.QDetails.Count) / num2);
						w_Height = wrapPanel.Height;
						wrapPanel.Height = (double)((num - num3) * (Button_Height + 15));
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						Button_Type = -1;
					}
				}
				else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Collapsed)
				{
					Button_Type = 4;
					((SingleQuota)/*Error near IL_00d1: Stack underflow*/).PageLoaded = ((byte)/*Error near IL_00d1: Stack underflow*/ != 0);
				}
				else
				{
					wrapPanel.Height = w_Height;
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					wrapPanel.Orientation = Orientation.Horizontal;
					Button_Type = 1;
					PageLoaded = false;
				}
				return;
			}
			if (Button_Type > 20)
			{
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				wrapPanel.Orientation = Orientation.Vertical;
				double actualHeight;
				if ((double)Button_Type > scrollViewer.ActualHeight)
				{
					actualHeight = scrollViewer.ActualHeight;
				}
				else
				{
					double num4 = (double)Button_Type;
				}
				((FrameworkElement)/*Error near IL_0140: Stack underflow*/).Height = actualHeight;
				PageLoaded = false;
				return;
			}
			if (Button_Type != 2)
			{
				int button_Type2 = Button_Type;
				if (/*Error near IL_023a: Stack underflow*/ != /*Error near IL_023a: Stack underflow*/)
				{
					wrapPanel.Orientation = Orientation.Horizontal;
					goto IL_016c;
				}
			}
			wrapPanel.Orientation = Orientation.Vertical;
			goto IL_016c;
			IL_026a:
			PageLoaded = false;
			return;
			IL_016c:
			if (Button_Type != 3)
			{
				int button_Type3 = Button_Type;
				if (/*Error near IL_025a: Stack underflow*/ != /*Error near IL_025a: Stack underflow*/)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					goto IL_026a;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_026a;
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapPanel1;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.IS_OTHER;
				if (qDetail.IS_OTHER == 1 || qDetail.IS_OTHER == 3 || ((qDetail.IS_OTHER == 11) | (qDetail.IS_OTHER == 5)) || qDetail.IS_OTHER == 13 || qDetail.IS_OTHER == 14)
				{
					ExistTextFill = true;
				}
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = (double)Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
				listButton.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_005f: Incompatible stack heights: 0 vs 1
			//IL_0144: Incompatible stack heights: 0 vs 1
			//IL_0154: Incompatible stack heights: 0 vs 1
			//IL_0155: Incompatible stack heights: 0 vs 1
			Button obj = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			int num = (int)obj.Tag;
			string text = obj.Name.Substring(2);
			int num2 = 0;
			if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (((FrameworkElement)/*Error near IL_00c2: Stack underflow*/).Style == style)
			{
				num3 = 1;
			}
			if (num3 == 0)
			{
				oQuestion.SelectedCode = text;
				foreach (Button child in wrapPanel1.Children)
				{
					string a = child.Name.Substring(2);
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_015a: Stack underflow*/).Style = (Style)/*Error near IL_015a: Stack underflow*/;
				}
				if (num2 == 0)
				{
					txtFill.Background = Brushes.LightGray;
					txtFill.IsEnabled = false;
				}
				else
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
					if (txtFill.Text == _003F487_003F._003F488_003F(""))
					{
						txtFill.Focus();
					}
				}
			}
			if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
			{
				if (txtFill.IsEnabled)
				{
					txtFill.Focus();
				}
				else
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_00b1: Incompatible stack heights: 0 vs 1
			//IL_00cb: Incompatible stack heights: 0 vs 1
			//IL_00e0: Incompatible stack heights: 0 vs 3
			//IL_00f1: Incompatible stack heights: 0 vs 2
			//IL_0105: Incompatible stack heights: 0 vs 1
			//IL_010a: Incompatible stack heights: 1 vs 0
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return (byte)/*Error near IL_0020: Stack underflow*/ != 0;
			}
			if (txtFill.IsEnabled)
			{
				txtFill.Text.Trim();
				string b = _003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_003f: Stack underflow*/ == b)
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_004b: Stack underflow*/, (string)/*Error near IL_004b: Stack underflow*/, (MessageBoxButton)/*Error near IL_004b: Stack underflow*/, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			if (txtFill.IsEnabled)
			{
				QSingle oQuestion2 = oQuestion;
				string fillText;
				if (((SingleQuota)/*Error near IL_006f: Stack underflow*/).txtFill.IsEnabled)
				{
					fillText = txtFill.Text.Trim();
				}
				else
				{
					_003F487_003F._003F488_003F("");
				}
				((QSingle)/*Error near IL_010f: Stack underflow*/).FillText = fillText;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_00ba: Incompatible stack heights: 0 vs 3
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.SelectedCode;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				string questionName = oQuestion.QuestionName;
				string qUESTION_NAME = string.Concat(str1: _003F487_003F._003F488_003F((string)/*Error near IL_0083: Stack underflow*/), str0: (string)/*Error near IL_0088: Stack underflow*/);
				((VEAnswer)/*Error near IL_008d: Stack underflow*/).QUESTION_NAME = qUESTION_NAME;
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0160: Incompatible stack heights: 0 vs 1
			//IL_017a: Incompatible stack heights: 0 vs 2
			//IL_018b: Incompatible stack heights: 0 vs 2
			//IL_019b: Incompatible stack heights: 0 vs 1
			//IL_01bc: Incompatible stack heights: 0 vs 3
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			List<VEAnswer> list = _003F88_003F();
			oLogicEngine.PageAnswer = list;
			if (!oLogicEngine.CheckLogic(CurPageId))
			{
				int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
				if ((int)/*Error near IL_0165: Stack underflow*/ == 0)
				{
					string logic_Message = oLogicEngine.Logic_Message;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_0072: Stack underflow*/, (string)/*Error near IL_0072: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					btnNav.Content = btnNav_Content;
					return;
				}
				if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					Button btnNav2 = btnNav;
					string content = ((SingleQuota)/*Error near IL_00d0: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_00d5: Stack underflow*/).Content = content;
					return;
				}
			}
			if (SurveyMsg.FunctionQuotaManager == _003F487_003F._003F488_003F("_ŭɹ\u0375ѡսټݼࡀ॥\u0a60\u0b7a౬\u0d41\u0e6aཤ\u1068ᅯቢ፴ᑚᕰᙱ\u1777ᡤ"))
			{
				_003F164_003F();
				if ((int)/*Error near IL_01a0: Stack underflow*/ == 0)
				{
					return;
				}
			}
			goto IL_00f4;
			IL_012b:
			goto IL_0020;
			IL_00f4:
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption2 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_01c3: Stack underflow*/, (string)/*Error near IL_01c3: Stack underflow*/, (MessageBoxButton)/*Error near IL_01c3: Stack underflow*/, MessageBoxImage.Asterisk);
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
			return;
			IL_01a1:
			goto IL_00f4;
		}

		private void _003F81_003F()
		{
			//IL_01f8: Incompatible stack heights: 0 vs 4
			//IL_0212: Incompatible stack heights: 0 vs 1
			//IL_0228: Incompatible stack heights: 0 vs 2
			//IL_0247: Incompatible stack heights: 0 vs 1
			//IL_025e: Incompatible stack heights: 0 vs 2
			//IL_0273: Incompatible stack heights: 0 vs 1
			//IL_0278: Invalid comparison between Unknown and I4
			//IL_028a: Incompatible stack heights: 0 vs 2
			//IL_02a1: Incompatible stack heights: 0 vs 2
			//IL_02c5: Incompatible stack heights: 0 vs 1
			//IL_02d0: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.PageStartTime = SurveyHelper.PageStartTime;
			MyNav.RecordFileName = SurveyHelper.RecordFileName;
			MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			if (!(MyNav.GroupLevel == _003F487_003F._003F488_003F("")))
			{
				MyNav.NextCirclePage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
				SurveyHelper.CircleACount = MyNav.CircleACount;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				if (MyNav.IsLastA)
				{
					int groupPageType = MyNav.GroupPageType;
					if ((int)/*Error near IL_0217: Stack underflow*/ != 0)
					{
						int groupPageType2 = MyNav.GroupPageType;
						if (/*Error near IL_022d: Stack underflow*/ != /*Error near IL_022d: Stack underflow*/)
						{
							goto IL_00d7;
						}
					}
					SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
					SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
				}
				goto IL_00d7;
			}
			NavBase myNav = MyNav;
			string mySurveyId = MySurveyId;
			string curPageId = CurPageId;
			((NavBase)/*Error near IL_0061: Stack underflow*/).NextPage((string)/*Error near IL_0061: Stack underflow*/, (int)/*Error near IL_0061: Stack underflow*/, (string)/*Error near IL_0061: Stack underflow*/, roadMapVersion);
			goto IL_014f;
			IL_00d7:
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				int circleBCount = MyNav.CircleBCount;
				SurveyHelper.CircleBCount = (int)/*Error near IL_00fb: Stack underflow*/;
				SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
				if (MyNav.IsLastB)
				{
					int groupPageType3 = MyNav.GroupPageType;
					if (/*Error near IL_0263: Stack underflow*/ != /*Error near IL_0263: Stack underflow*/)
					{
						int groupPageType4 = MyNav.GroupPageType;
						if ((int)/*Error near IL_0278: Stack underflow*/ != 12)
						{
							int groupPageType5 = MyNav.GroupPageType;
							if (/*Error near IL_028f: Stack underflow*/ != /*Error near IL_028f: Stack underflow*/)
							{
								int groupPageType6 = MyNav.GroupPageType;
								if (/*Error near IL_02a6: Stack underflow*/ != /*Error near IL_02a6: Stack underflow*/)
								{
									goto IL_014f;
								}
							}
						}
					}
					SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
					SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
				}
			}
			goto IL_014f;
			IL_01d6:
			goto IL_02d5;
			IL_014f:
			string text = oLogicEngine.Route(MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
			{
				string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text);
				uriString = (string)/*Error near IL_01b7: Stack underflow*/;
			}
			if (!(text == SurveyHelper.CurPageName))
			{
				goto IL_02d5;
			}
			((Page)/*Error near IL_01cc: Stack underflow*/).NavigationService.Refresh();
			goto IL_02f3;
			IL_02d5:
			base.NavigationService.RemoveBackEntry();
			base.NavigationService.Navigate(new Uri(uriString));
			goto IL_02f3;
			IL_02f3:
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavLoad = 0;
		}

		private void _003F163_003F(int _003F393_003F, List<VEAnswer> _003F370_003F)
		{
			btnNav.IsEnabled = false;
			btnNav.Content = _003F487_003F._003F488_003F("歨嘢䷔塐Ы軱簈圝\u082dबਯ");
			foreach (VEAnswer item in _003F370_003F)
			{
				Logging.Data.WriteLog(MySurveyId, item.QUESTION_NAME + _003F487_003F._003F488_003F("-") + item.CODE);
			}
			Thread.Sleep(_003F393_003F);
			btnNav.Content = btnNav_Content;
			btnNav.IsEnabled = true;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_0047;
			IL_001d:
			goto IL_0047;
			IL_0047:
			SecondsCountDown--;
			btnNav.Content = SecondsCountDown.ToString();
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_0090: Incompatible stack heights: 0 vs 1
			//IL_0095: Incompatible stack heights: 1 vs 0
			//IL_00a0: Incompatible stack heights: 0 vs 1
			//IL_00a5: Incompatible stack heights: 1 vs 0
			//IL_00b0: Incompatible stack heights: 0 vs 1
			//IL_00b5: Incompatible stack heights: 1 vs 0
			//IL_00c0: Incompatible stack heights: 0 vs 1
			//IL_00c5: Incompatible stack heights: 1 vs 0
			//IL_00d0: Incompatible stack heights: 0 vs 1
			//IL_00dc: Incompatible stack heights: 0 vs 1
			int num = _003F364_003F;
			if (num == -9999)
			{
				num = _003F363_003F;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (_003F363_003F < 0)
			{
			}
			int num2 = 0;
			int num3;
			if (num2 < num)
			{
				num3 = num2;
			}
			int num4 = num3;
			int num5;
			if (num2 < num)
			{
				num5 = num;
			}
			num = num5;
			int length;
			if (num2 > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num4 = length;
			if (_003F364_003F > _003F362_003F.Length)
			{
				int num6 = _003F362_003F.Length - 1;
			}
			num = (int)/*Error near IL_00dd: Stack underflow*/;
			return _003F362_003F.Substring(num4, num - num4 + 1);
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0037: Incompatible stack heights: 0 vs 1
			//IL_003c: Incompatible stack heights: 1 vs 0
			//IL_0041: Incompatible stack heights: 0 vs 2
			//IL_0047: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, length);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_0075: Incompatible stack heights: 0 vs 1
			//IL_007a: Incompatible stack heights: 1 vs 0
			//IL_007f: Incompatible stack heights: 0 vs 2
			//IL_0085: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
			int num = _003F365_003F;
			if (num == -9999)
			{
				num = _003F362_003F.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int length;
			if (_003F363_003F > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			int num2 = length;
			if (num2 + num > _003F362_003F.Length)
			{
				int num3 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, (int)/*Error near IL_0090: Stack underflow*/);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0049: Incompatible stack heights: 0 vs 1
			//IL_004e: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num <= _003F362_003F.Length)
			{
				int num2 = _003F362_003F.Length - num;
			}
			return ((string)/*Error near IL_0026: Stack underflow*/).Substring(0);
		}

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_005d:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0069:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0075:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_0081:
			goto IL_004b;
			IL_004b:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQuestion.QuestionName;
			SurveyHelper.AttachPageId = CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		private bool _003F164_003F()
		{
			return true;
		}

		[AsyncStateMachine(typeof(_003CPostJsonAwait_003Ed__40))]
		private void _003F165_003F(List<jquotaanswer> _003F394_003F, string _003F395_003F)
		{
			_003F12_003F stateMachine = default(_003F12_003F);
			stateMachine.Myqanswer = _003F394_003F;
			stateMachine.serviceAddress = _003F395_003F;
			stateMachine._003C_003Et__builder = AsyncVoidMethodBuilder.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncVoidMethodBuilder _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
		}

		private List<jquotaanswer> _003F166_003F()
		{
			List<jquotaanswer> list = new List<jquotaanswer>();
			jquotaanswer jquotaanswer = new jquotaanswer
			{
				surveyid = _003F487_003F._003F488_003F("5ĳȲ\u0330"),
				surveyguid = _003F487_003F._003F488_003F(""),
				pageid = _003F487_003F._003F488_003F("SĲ"),
				projectid = _003F487_003F._003F488_003F("6İȶ\u0334"),
				isfinish = _003F487_003F._003F488_003F("1")
			};
			janswer item = new janswer
			{
				questionname = _003F487_003F._003F488_003F("SĲ"),
				code = _003F487_003F._003F488_003F("2")
			};
			jquotaanswer.qanswers.Add(item);
			list.Add(jquotaanswer);
			return list;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭦ᱠ\u1d6aṠὮ⁻ⅼ≧⍳⑧┫♼❢⡯⥭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0022:
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
				((SingleQuota)_003F350_003F).Loaded += _003F80_003F;
				((SingleQuota)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				gridContent = (Grid)_003F350_003F;
				break;
			case 5:
				PicWidth = (ColumnDefinition)_003F350_003F;
				break;
			case 6:
				ButtonWidth = (ColumnDefinition)_003F350_003F;
				break;
			case 7:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 8:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 9:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 10:
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 11:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 12:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 13:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 14:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 15:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0049:
			goto IL_0053;
		}
	}
}
