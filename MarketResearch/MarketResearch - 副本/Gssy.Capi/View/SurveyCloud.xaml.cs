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
using Aliyun.OpenServices.OpenStorageService;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;

namespace Gssy.Capi.View
{
	public partial class SurveyCloud : Window
	{
		public SurveyCloud()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText("PCCode");
			this.txtPCCode.Text = byCodeText;
			this.txtLastFile.Text = this.oSurveyConfigBiz.GetByCodeText("LastUploadFile");
			this.txtMsg.Text = "";
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtPCCode.Text;
			if (text == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSetMachine, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.btnSave.IsEnabled = true;
				return;
			}
			if (SurveyMsg.VersionID.IndexOf("演示版") <= -1 && SurveyMsg.VersionID.IndexOf("Demo") <= -1)
			{
				bool bool_ = SurveyMsg.VersionID.IndexOf("测试版") > -1 || SurveyMsg.VersionID.IndexOf("演示版") > -1 || SurveyMsg.VersionID.IndexOf("Demo") > -1;
				alioss alioss = new alioss(SurveyMsg.OSSRegion, bool_, SurveyMsg.ProjectId);
				this.method_2();
				this.method_3(20.0);
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				this.txtMsg.Text = SurveyMsg.MsgUpDealing;
				this.btnSave.IsEnabled = false;
				OssClient ossClient = new OssClient(alioss.endpoint, alioss.accessId, alioss.accessKey);
				bool flag = false;
				try
				{
					using (IEnumerator<Bucket> enumerator = ossClient.ListBuckets().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Name == alioss.bucketName)
							{
								flag = true;
								break;
							}
						}
					}
				}
				catch (Exception)
				{
					MessageBox.Show(SurveyMsg.MsgNoNet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.btnSave.IsEnabled = true;
					return;
				}
				if (!flag)
				{
					TextBlock textBlock = this.txtMsg;
					textBlock.Text = textBlock.Text + Environment.NewLine + SurveyMsg.MsgOssExprie;
					MessageBox.Show(SurveyMsg.MsgOssExprie, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.btnSave.IsEnabled = true;
					return;
				}
				int num = 100;
				string text2 = this.oSurveyConfigBiz.GetByCodeText("UploadSequence");
				if (text2 == null)
				{
					text2 = "";
				}
				if (text2 != "")
				{
					num = int.Parse(text2);
				}
				int i = num;
				while (i < 999)
				{
					if (ossClient.ListObjects(alioss.bucketName, alioss.bucketDir + "/" + i.ToString()).ObjectSummaries.Count<OssObjectSummary>() >= 70)
					{
						i++;
					}
					else
					{
						text2 = i.ToString();
						if (i > num)
						{
							this.oSurveyConfigBiz.Save("UploadSequence", text2);
						}
						IL_297:
						double double_ = 30.0;
						this.method_3(double_);
						TextBlock textBlock2 = this.txtMsg;
						textBlock2.Text = textBlock2.Text + Environment.NewLine + SurveyMsg.MsgUpPacking;
						this.strRarFileName = string.Format("{0}_{1}_{2:MMdd_HHmmss}.rar", text2, text, DateTime.Now);
						RarFile rarFile = new RarFile();
						if (!rarFile.Compress(this.strRarSource + "*.dat", this.strRarOutputFolder, this.strRarFileName, this.strRarPassword, true, true))
						{
							MessageBox.Show(SurveyMsg.MsgNotFilePackError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
							this.btnSave.IsEnabled = true;
							return;
						}
						double_ = 50.0;
						this.method_3(double_);
						TextBlock textBlock3 = this.txtMsg;
						textBlock3.Text = textBlock3.Text + Environment.NewLine + SurveyMsg.MsgUploading;
						string text3 = alioss.bucketDir + "/" + this.strRarFileName;
						ObjectMetadata objectMetadata = new ObjectMetadata();
						objectMetadata.UserMetadata.Add("SurveyData", this.strRarFileName);
						string text4 = this.strRarOutputFolder + this.strRarFileName;
						bool flag2 = false;
						if (new FileInfo(text4).Length > (long)(1024 * alioss.BigFilePartSize))
						{
							flag2 = true;
						}
						try
						{
							if (flag2)
							{
								this.method_1(ossClient, text4, text3, alioss.bucketName, alioss.BigFilePartSize);
							}
							else
							{
								using (FileStream fileStream = File.Open(text4, FileMode.Open))
								{
									ossClient.PutObject(alioss.bucketName, text3, fileStream, objectMetadata);
								}
							}
							this.oSurveyConfigBiz.Save("LastUploadFile", this.strRarFileName);
						}
						catch (Exception)
						{
							MessageBox.Show(SurveyMsg.MsgUpFailCheckNet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
							this.btnSave.IsEnabled = true;
							return;
						}
						string text5 = this.strRarOutputFolder + this.strRarFileName.Replace(".rar", "\\").Substring(text2.Length + 1);
						TextBlock textBlock4;
						if (rarFile.Extract(text4, this.strRarOutputFolder, text5, this.strRarPassword))
						{
							textBlock4 = this.txtMsg;
							textBlock4.Text = string.Concat(new string[]
							{
								textBlock4.Text,
								Environment.NewLine,
								SurveyMsg.MsgUploadFinish,
								Environment.NewLine,
								SurveyMsg.MsgUpFileMove,
								text5
							});
							string searchPattern = "S*.dat";
							using (IEnumerator<FileInfo> enumerator2 = new DirectoryInfo(Environment.CurrentDirectory + "\\OutPut").EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									FileInfo fileInfo = enumerator2.Current;
									fileInfo.Delete();
								}
							}
						}
						textBlock4 = this.txtMsg;
						textBlock4.Text = string.Concat(new string[]
						{
							textBlock4.Text,
							Environment.NewLine,
							SurveyMsg.MsgUploadFinish,
							Environment.NewLine,
							SurveyMsg.MsgUpFileMoveFail
						});
						stopwatch.Stop();
						this.btnSave.IsEnabled = true;
						double_ = 100.0;
						this.method_3(double_);
						TextBlock textBlock5 = this.txtMsg;
						textBlock5.Text = textBlock5.Text + Environment.NewLine + string.Format(SurveyMsg.MsgUpFinish, stopwatch.Elapsed.TotalSeconds.ToString("F2"));
						this.method_4();
						MessageBox.Show(SurveyMsg.MsgFinishDeal, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						return;
					}
				}
			}
			this.method_3(100.0);
			this.txtMsg.Text = SurveyMsg.MsgUpDealing;
			TextBlock textBlock6 = this.txtMsg;
			textBlock6.Text = textBlock6.Text + Environment.NewLine + SurveyMsg.MsgUpPacking;
			TextBlock textBlock7 = this.txtMsg;
			textBlock7.Text = textBlock7.Text + Environment.NewLine + SurveyMsg.MsgUploading;
			TextBlock textBlock8 = this.txtMsg;
			textBlock8.Text = textBlock8.Text + Environment.NewLine + string.Format(SurveyMsg.MsgUpFinishDemo, new object[0]);
			MessageBox.Show(SurveyMsg.MsgUploadDemoFinish, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.btnSave.IsEnabled = true;
		}

		private void method_1(OssClient ossClient_0, string string_0, string string_1, string string_2, int int_0)
		{
			InitiateMultipartUploadRequest initiateMultipartUploadRequest = new InitiateMultipartUploadRequest(string_2, string_1);
			InitiateMultipartUploadResult initiateMultipartUploadResult = ossClient_0.InitiateMultipartUpload(initiateMultipartUploadRequest);
			int num = 1024 * int_0;
			FileInfo fileInfo = new FileInfo(string_0);
			int num2 = (int)(fileInfo.Length / (long)num);
			if (fileInfo.Length % (long)num != 0L)
			{
				num2++;
			}
			List<PartETag> list = new List<PartETag>();
			for (int i = 0; i < num2; i++)
			{
				this.txtMsgBar.Text = string.Format(SurveyMsg.MsgUpProcInfo, (i + 1).ToString(), ((i + 1) * int_0).ToString());
				FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open);
				long num3 = (long)(num * i);
				fileStream.Position = num3;
				long value = ((long)num < fileInfo.Length - num3) ? ((long)num) : (fileInfo.Length - num3);
				UploadPartResult uploadPartResult = ossClient_0.UploadPart(new UploadPartRequest(string_2, string_1, initiateMultipartUploadResult.UploadId)
				{
					InputStream = fileStream,
					PartSize = new long?(value),
					PartNumber = new int?(i + 1)
				});
				list.Add(uploadPartResult.PartETag);
				fileStream.Close();
			}
			this.txtMsgBar.Text = "";
			CompleteMultipartUploadRequest completeMultipartUploadRequest = new CompleteMultipartUploadRequest(string_2, string_1, initiateMultipartUploadResult.UploadId);
			foreach (PartETag item in list)
			{
				completeMultipartUploadRequest.PartETags.Add(item);
			}
			ossClient_0.CompleteMultipartUpload(completeMultipartUploadRequest);
		}

		private void method_2()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		private void method_3(double double_0)
		{
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				double_0
			});
		}

		private void method_4()
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

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private string strRarSource = Environment.CurrentDirectory + "\\OutPut\\";

		private string strRarOutputFolder = Environment.CurrentDirectory + "\\Upload\\";

		private string strRarFileName = "";

		private string strRarPassword = "GSSY.capi";

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;
	}
}
