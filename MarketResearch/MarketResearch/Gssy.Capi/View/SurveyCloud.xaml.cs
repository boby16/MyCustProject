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
	// Token: 0x02000051 RID: 81
	public partial class SurveyCloud : Window
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00092238 File Offset: 0x00090438
		public SurveyCloud()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000020CF File Offset: 0x000002CF
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000922B8 File Offset: 0x000904B8
		private void method_0(object sender, RoutedEventArgs e)
		{
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ"));
			this.txtPCCode.Text = byCodeText;
			this.txtLastFile.Text = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("BŬɿͿџչ٤ݨࡧॡੂ୪౮൤"));
			this.txtMsg.Text = global::GClass0.smethod_0("");
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0009231C File Offset: 0x0009051C
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtPCCode.Text;
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSetMachine, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.btnSave.IsEnabled = true;
				return;
			}
			if (SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) <= -1 && SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) <= -1)
			{
				bool bool_ = SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) > -1 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) > -1 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) > -1;
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
				string text2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("[Žɠͤѫխٛݢࡷ॰੡୭ౡ൤"));
				if (text2 == null)
				{
					text2 = global::GClass0.smethod_0("");
				}
				if (text2 != global::GClass0.smethod_0(""))
				{
					num = int.Parse(text2);
				}
				int i = num;
				while (i < 999)
				{
					if (ossClient.ListObjects(alioss.bucketName, alioss.bucketDir + global::GClass0.smethod_0(".") + i.ToString()).ObjectSummaries.Count<OssObjectSummary>() >= 70)
					{
						i++;
					}
					else
					{
						text2 = i.ToString();
						if (i > num)
						{
							this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("[Žɠͤѫխٛݢࡷ॰੡୭ౡ൤"), text2);
						}
						IL_297:
						double double_ = 30.0;
						this.method_3(double_);
						TextBlock textBlock2 = this.txtMsg;
						textBlock2.Text = textBlock2.Text + Environment.NewLine + SurveyMsg.MsgUpPacking;
						this.strRarFileName = string.Format(global::GClass0.smethod_0("`Īɤ͇Ѭԧ٨݋ࡨठਫଢ଼ూ൪๩ན၃ᅂቤ፥ᑴᕵᙸᜪᡱᥣᩳ"), text2, text, DateTime.Now);
						RarFile rarFile = new RarFile();
						if (!rarFile.Compress(this.strRarSource + global::GClass0.smethod_0("/Īɧͣѵ"), this.strRarOutputFolder, this.strRarFileName, this.strRarPassword, true, true))
						{
							MessageBox.Show(SurveyMsg.MsgNotFilePackError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
							this.btnSave.IsEnabled = true;
							return;
						}
						double_ = 50.0;
						this.method_3(double_);
						TextBlock textBlock3 = this.txtMsg;
						textBlock3.Text = textBlock3.Text + Environment.NewLine + SurveyMsg.MsgUploading;
						string text3 = alioss.bucketDir + global::GClass0.smethod_0(".") + this.strRarFileName;
						ObjectMetadata objectMetadata = new ObjectMetadata();
						objectMetadata.UserMetadata.Add(global::GClass0.smethod_0("Yżɺͱѣռـݢࡶॠ"), this.strRarFileName);
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
							this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("BŬɿͿџչ٤ݨࡧॡੂ୪౮൤"), this.strRarFileName);
						}
						catch (Exception)
						{
							MessageBox.Show(SurveyMsg.MsgUpFailCheckNet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
							this.btnSave.IsEnabled = true;
							return;
						}
						string text5 = this.strRarOutputFolder + this.strRarFileName.Replace(global::GClass0.smethod_0("*űɣͳ"), global::GClass0.smethod_0("]")).Substring(text2.Length + 1);
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
							string searchPattern = global::GClass0.smethod_0("UįȪͧѣյ");
							using (IEnumerator<FileInfo> enumerator2 = new DirectoryInfo(Environment.CurrentDirectory + global::GClass0.smethod_0("[ŉɰͰѓշٵ")).EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly).GetEnumerator())
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
						textBlock5.Text = textBlock5.Text + Environment.NewLine + string.Format(SurveyMsg.MsgUpFinish, stopwatch.Elapsed.TotalSeconds.ToString(global::GClass0.smethod_0("Dĳ")));
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

		// Token: 0x06000569 RID: 1385 RVA: 0x00092A4C File Offset: 0x00090C4C
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
			this.txtMsgBar.Text = global::GClass0.smethod_0("");
			CompleteMultipartUploadRequest completeMultipartUploadRequest = new CompleteMultipartUploadRequest(string_2, string_1, initiateMultipartUploadResult.UploadId);
			foreach (PartETag item in list)
			{
				completeMultipartUploadRequest.PartETags.Add(item);
			}
			ossClient_0.CompleteMultipartUpload(completeMultipartUploadRequest);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00092BF4 File Offset: 0x00090DF4
		private void method_2()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000038D2 File Offset: 0x00001AD2
		private void method_3(double double_0)
		{
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				double_0
			});
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00092C44 File Offset: 0x00090E44
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

		// Token: 0x040009D7 RID: 2519
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x040009D8 RID: 2520
		private string strRarSource = Environment.CurrentDirectory + global::GClass0.smethod_0("Tňɳͱєնٶݝ");

		// Token: 0x040009D9 RID: 2521
		private string strRarOutputFolder = Environment.CurrentDirectory + global::GClass0.smethod_0("TŒɶͩѫբ٦ݝ");

		// Token: 0x040009DA RID: 2522
		private string strRarFileName = global::GClass0.smethod_0("");

		// Token: 0x040009DB RID: 2523
		private string strRarPassword = global::GClass0.smethod_0("Nśɔ͟Ыէ٢ݲࡨ");

		// Token: 0x040009DC RID: 2524
		private bool strRarExcludeRootFolder;

		// Token: 0x040009DD RID: 2525
		private bool strRarIncludeSubFolder = true;
	}
}
