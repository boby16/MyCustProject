using Aliyun.OpenServices.OpenStorageService;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class SurveyCloud : Window, IComponentConnector
	{
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private string strRarSource = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Tňɳͱєնٶݝ");

		private string strRarOutputFolder = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TŒɶ\u0369ѫբ٦ݝ");

		private string strRarFileName = _003F487_003F._003F488_003F("");

		private string strRarPassword = _003F487_003F._003F488_003F("Nśɔ\u035fЫէ٢ݲ\u0868");

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;

		internal TextBlock txtLastFile;

		internal TextBlock txtPCCode;

		internal TextBlock txtMsg;

		internal TextBlock txtMsgBar;

		internal ProgressBar progressBar1;

		internal Button btnExit;

		internal Button btnSave;

		private bool _contentLoaded;

		public SurveyCloud()
		{
			InitializeComponent();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new CStart().Show();
			Close();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ"));
			txtPCCode.Text = byCodeText;
			txtLastFile.Text = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("BŬɿͿџչ٤ݨ\u0867ॡ\u0a42୪౮\u0d64"));
			txtMsg.Text = _003F487_003F._003F488_003F("");
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0225: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Expected O, but got Unknown
			//IL_0550: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Expected O, but got Unknown
			string text = txtPCCode.Text;
			if (text == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSetMachine, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				btnSave.IsEnabled = true;
			}
			else if (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("漗砸灉")) > -1 || SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("@Ŧɯ\u036e")) > -1)
			{
				_003F189_003F(100.0);
				txtMsg.Text = SurveyMsg.MsgUpDealing;
				TextBlock textBlock = txtMsg;
				textBlock.Text = textBlock.Text + Environment.NewLine + SurveyMsg.MsgUpPacking;
				TextBlock textBlock2 = txtMsg;
				textBlock2.Text = textBlock2.Text + Environment.NewLine + SurveyMsg.MsgUploading;
				TextBlock textBlock3 = txtMsg;
				textBlock3.Text = textBlock3.Text + Environment.NewLine + string.Format(SurveyMsg.MsgUpFinishDemo);
				MessageBox.Show(SurveyMsg.MsgUploadDemoFinish, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				btnSave.IsEnabled = true;
			}
			else
			{
				bool flag = true;
				alioss alioss = new alioss(_003F267_003F: (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("浈諗灉")) > -1 || SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("漗砸灉")) > -1 || SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("@Ŧɯ\u036e")) > -1) ? true : false, _003F266_003F: SurveyMsg.OSSRegion, _003F268_003F: SurveyMsg.ProjectId);
				_003F188_003F();
				_003F189_003F(20.0);
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				txtMsg.Text = SurveyMsg.MsgUpDealing;
				btnSave.IsEnabled = false;
				OssClient val = new OssClient(alioss.endpoint, alioss.accessId, alioss.accessKey);
				bool flag2 = false;
				try
				{
					foreach (Bucket item in val.ListBuckets())
					{
						if (item.get_Name() == alioss.bucketName)
						{
							flag2 = true;
							break;
						}
					}
				}
				catch (Exception)
				{
					MessageBox.Show(SurveyMsg.MsgNoNet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					btnSave.IsEnabled = true;
					return;
				}
				if (!flag2)
				{
					TextBlock textBlock4 = txtMsg;
					textBlock4.Text = textBlock4.Text + Environment.NewLine + SurveyMsg.MsgOssExprie;
					MessageBox.Show(SurveyMsg.MsgOssExprie, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					btnSave.IsEnabled = true;
				}
				else
				{
					int num = 100;
					string text2 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("[Žɠ\u0364ѫխ\u065bݢ\u0877॰\u0a61୭ౡ\u0d64"));
					if (text2 == null)
					{
						text2 = _003F487_003F._003F488_003F("");
					}
					if (text2 != _003F487_003F._003F488_003F(""))
					{
						num = int.Parse(text2);
					}
					for (int i = num; i < 999; i++)
					{
						if (val.ListObjects(alioss.bucketName, alioss.bucketDir + _003F487_003F._003F488_003F(".") + i.ToString()).get_ObjectSummaries().Count() < 70)
						{
							text2 = i.ToString();
							if (i > num)
							{
								oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("[Žɠ\u0364ѫխ\u065bݢ\u0877॰\u0a61୭ౡ\u0d64"), text2);
							}
							break;
						}
					}
					double _003F346_003F = 30.0;
					_003F189_003F(_003F346_003F);
					TextBlock textBlock5 = txtMsg;
					textBlock5.Text = textBlock5.Text + Environment.NewLine + SurveyMsg.MsgUpPacking;
					strRarFileName = string.Format(_003F487_003F._003F488_003F("`Īɤ\u0347Ѭԧ٨\u074b\u0868ठਫଢ଼\u0c42൪\u0e69ན၃ᅂቤ፥ᑴᕵᙸᜪᡱᥣ\u1a73"), text2, text, DateTime.Now);
					RarFile rarFile = new RarFile();
					if (!rarFile.Compress(strRarSource + _003F487_003F._003F488_003F("/Īɧ\u0363ѵ"), strRarOutputFolder, strRarFileName, strRarPassword, true, true))
					{
						MessageBox.Show(SurveyMsg.MsgNotFilePackError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						btnSave.IsEnabled = true;
					}
					else
					{
						_003F346_003F = 50.0;
						_003F189_003F(_003F346_003F);
						TextBlock textBlock6 = txtMsg;
						textBlock6.Text = textBlock6.Text + Environment.NewLine + SurveyMsg.MsgUploading;
						string text3 = alioss.bucketDir + _003F487_003F._003F488_003F(".") + strRarFileName;
						ObjectMetadata val2 = new ObjectMetadata();
						val2.get_UserMetadata().Add(_003F487_003F._003F488_003F("Yżɺͱѣռ\u0640ݢ\u0876ॠ"), strRarFileName);
						string text4 = strRarOutputFolder + strRarFileName;
						bool flag3 = false;
						if (new FileInfo(text4).Length > 1024 * alioss.BigFilePartSize)
						{
							flag3 = true;
						}
						try
						{
							if (flag3)
							{
								_003F212_003F(val, text4, text3, alioss.bucketName, alioss.BigFilePartSize);
							}
							else
							{
								using (FileStream fileStream = File.Open(text4, FileMode.Open))
								{
									val.PutObject(alioss.bucketName, text3, (Stream)fileStream, val2);
								}
							}
							oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("BŬɿͿџչ٤ݨ\u0867ॡ\u0a42୪౮\u0d64"), strRarFileName);
						}
						catch (Exception)
						{
							MessageBox.Show(SurveyMsg.MsgUpFailCheckNet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
							btnSave.IsEnabled = true;
							return;
						}
						string text5 = strRarOutputFolder + strRarFileName.Replace(_003F487_003F._003F488_003F("*űɣͳ"), _003F487_003F._003F488_003F("]")).Substring(text2.Length + 1);
						if (rarFile.Extract(text4, strRarOutputFolder, text5, strRarPassword))
						{
							TextBlock textBlock7 = txtMsg;
							textBlock7.Text = textBlock7.Text + Environment.NewLine + SurveyMsg.MsgUploadFinish + Environment.NewLine + SurveyMsg.MsgUpFileMove + text5;
							string searchPattern = _003F487_003F._003F488_003F("UįȪ\u0367ѣյ");
							foreach (FileInfo item2 in new DirectoryInfo(Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŉɰͰѓշٵ")).EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly))
							{
								item2.Delete();
							}
						}
						else
						{
							TextBlock textBlock7 = txtMsg;
							textBlock7.Text = textBlock7.Text + Environment.NewLine + SurveyMsg.MsgUploadFinish + Environment.NewLine + SurveyMsg.MsgUpFileMoveFail;
						}
						stopwatch.Stop();
						btnSave.IsEnabled = true;
						_003F346_003F = 100.0;
						_003F189_003F(_003F346_003F);
						TextBlock textBlock8 = txtMsg;
						textBlock8.Text = textBlock8.Text + Environment.NewLine + string.Format(SurveyMsg.MsgUpFinish, stopwatch.Elapsed.TotalSeconds.ToString(_003F487_003F._003F488_003F("Dĳ")));
						_003F190_003F();
						MessageBox.Show(SurveyMsg.MsgFinishDeal, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
				}
			}
		}

		private void _003F212_003F(OssClient _003F399_003F, string _003F400_003F, string _003F401_003F, string _003F402_003F, int _003F403_003F)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Expected O, but got Unknown
			//IL_00e6: Incompatible stack heights: 0 vs 1
			//IL_00e8: Incompatible stack heights: 0 vs 1
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Expected O, but got Unknown
			InitiateMultipartUploadRequest val = new InitiateMultipartUploadRequest(_003F402_003F, _003F401_003F);
			InitiateMultipartUploadResult val2 = _003F399_003F.InitiateMultipartUpload(val);
			int num = 1024 * _003F403_003F;
			FileInfo fileInfo = new FileInfo(_003F400_003F);
			int num2 = (int)(fileInfo.Length / num);
			if (fileInfo.Length % num != 0L)
			{
				num2++;
			}
			List<PartETag> list = new List<PartETag>();
			for (int i = 0; i < num2; i++)
			{
				txtMsgBar.Text = string.Format(SurveyMsg.MsgUpProcInfo, (i + 1).ToString(), ((i + 1) * _003F403_003F).ToString());
				FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open);
				long num4 = fileStream.Position = num * i;
				if (num >= fileInfo.Length - num4)
				{
					long num5 = fileInfo.Length - num4;
				}
				long value = (long)/*Error near IL_00ea: Stack underflow*/;
				UploadPartRequest val3 = new UploadPartRequest(_003F402_003F, _003F401_003F, val2.get_UploadId());
				val3.set_InputStream((Stream)fileStream);
				val3.set_PartSize((long?)value);
				val3.set_PartNumber((int?)(i + 1));
				UploadPartResult val4 = _003F399_003F.UploadPart(val3);
				list.Add(val4.get_PartETag());
				fileStream.Close();
			}
			txtMsgBar.Text = _003F487_003F._003F488_003F("");
			CompleteMultipartUploadRequest val5 = new CompleteMultipartUploadRequest(_003F402_003F, _003F401_003F, val2.get_UploadId());
			foreach (PartETag item in list)
			{
				val5.get_PartETags().Add(item);
			}
			_003F399_003F.CompleteMultipartUpload(val5);
		}

		private void _003F188_003F()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		private void _003F189_003F(double _003F346_003F)
		{
			progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar1.SetValue), DispatcherPriority.Background, RangeBase.ValueProperty, _003F346_003F);
		}

		private void _003F190_003F()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = new RepeatBehavior(1.0);
			progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
			progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar1.SetValue), DispatcherPriority.Background, RangeBase.ValueProperty, 100.0);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭺\u1c7cᵻṩὲ\u2069Ⅵ≧⍲③┫♼❢⡯⥭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0016:
			goto IL_001b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((SurveyCloud)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtLastFile = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtPCCode = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtMsg = (TextBlock)_003F350_003F;
				break;
			case 5:
				txtMsgBar = (TextBlock)_003F350_003F;
				break;
			case 6:
				progressBar1 = (ProgressBar)_003F350_003F;
				break;
			case 7:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 8:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
