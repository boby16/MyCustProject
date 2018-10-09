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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Aliyun.OpenServices.OpenStorageService;
using Gssy.Capi.Class;

namespace Gssy.Capi.Update
{
	public partial class SurveyUpdate : Window
	{
		public SurveyUpdate()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.Shutdown();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.method_5("Gssy.Capi");
			this.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead("VersionID");
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText("PCCode");
			this.txtPCCode.Text = ((byCodeText == null) ? "<未设置>" : byCodeText);
			this.txtMsg.Text = "当前程序版本(" + this.VersionID + ")";
			this.txtProjectName.Text = SurveyMsg.MsgProjectName + "(补丁更新)";
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("这个更新操作将会覆盖现在的程序版本，并无法恢复！" + Environment.NewLine + Environment.NewLine + "是否确认检查并更新CAPI程序？", "版本更新确认", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				this.btnSave.IsEnabled = false;
				this.bw = new BackgroundWorker();
				this.bw.DoWork += this.bw_DoWork;
				this.bw.ProgressChanged += this.bw_ProgressChanged;
				this.bw.RunWorkerCompleted += this.bw_RunWorkerCompleted;
				this.bw.WorkerReportsProgress = true;
				this.bw.RunWorkerAsync();
			}
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			alioss alioss = new alioss(SurveyMsg.OSSRegion, false, SurveyMsg.ProjectId);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this.bw.ReportProgress(20, "检查更新.....");
			OssClient ossClient = new OssClient(alioss.endpoint, alioss.accessId, alioss.accessKey);
			bool flag = false;
			try
			{
				using (IEnumerator<Bucket> enumerator = ossClient.ListBuckets().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name == alioss.bucketNameUpdate)
						{
							flag = true;
							break;
						}
					}
				}
			}
			catch (Exception)
			{
				this.bw.ReportProgress(90, "未能连接服务器,请检查网络！");
				MessageBox.Show("未能连接服务器，请检查网络！", "版本更新", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			if (!flag)
			{
				this.bw.ReportProgress(90, "存储服务器已过期！");
				MessageBox.Show("存储服务器已过期！", "版本更新", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			string str = "";
			int num = this.VersionID.ToLower().IndexOf("v");
			if (num > -1)
			{
				str = this.VersionID.Substring(0, num);
			}
			else if (this.VersionID.IndexOf("测试版") > -1)
			{
				str = "测试版";
			}
			else if (this.VersionID.IndexOf("演示版") > -1)
			{
				str = "演示版";
			}
			else if (this.VersionID.IndexOf("正式版") > -1)
			{
				str = "正式版";
			}
			else if (this.VersionID.IndexOf("辅助版") > -1)
			{
				str = "辅助版";
			}
			ObjectListing objectListing = ossClient.ListObjects(alioss.bucketNameUpdate, alioss.bucketDirUpdate + "/" + str);
			if (objectListing.ObjectSummaries.Count<OssObjectSummary>() == 0)
			{
				this.bw.ReportProgress(90, "未有版本更新文件！");
				MessageBox.Show("未有版本更新文件！", "版本更新", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			string text = "";
			string key = "";
			string text2 = this.VersionID.ToLower();
			string text3 = text2;
			string text4 = "";
			foreach (OssObjectSummary ossObjectSummary in objectListing.ObjectSummaries)
			{
				if (!(ossObjectSummary.Key == alioss.bucketDirUpdate + "/"))
				{
					text = ossObjectSummary.Key.Replace(alioss.bucketDirUpdate + "/", "");
					text4 = text.Replace(".rar", "").ToLower();
					key = ossObjectSummary.Key;
					if (string.Compare(text4, text3) > 0)
					{
						text3 = text4;
					}
				}
			}
			if (text3 == text2)
			{
				this.bw.ReportProgress(90, "当前已经是最新版本！");
				MessageBox.Show("当前已经是最新版本！", "版本更新", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			string text5 = Environment.CurrentDirectory + "\\Download\\";
			if (!Directory.Exists(text5))
			{
				Directory.CreateDirectory(text5);
			}
			if (!Directory.Exists(text5))
			{
				this.bw.ReportProgress(90, "更新失败！");
				MessageBox.Show(string.Concat(new string[]
				{
					"更新版本时所需要的文件夹不存在！",
					Environment.NewLine,
					Environment.NewLine,
					"请在更新前自行预先建立以下文件夹：",
					Environment.NewLine,
					text5
				}), "更新失败", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			RarFile rarFile = new RarFile();
			string path = text5 + text;
			this.bw.ReportProgress(40, string.Format("下载文件 {0} ", text));
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
			GetObjectRequest getObjectRequest = new GetObjectRequest(alioss.bucketNameUpdate, key);
			ossClient.GetObject(getObjectRequest, fileStream);
			fileStream.Close();
			this.bw.ReportProgress(80, string.Format("解压文件 {0} ", text));
			this.strRarFile = path;
			this.strRarOutputFolder = Environment.CurrentDirectory + "\\";
			rarFile.Extract(this.strRarFile, this.strRarOutputFolder, this.strRarOutputFolder, this.strRarPassword);
			this.bw.ReportProgress(95, "版本更新为：" + text3);
			this.bw.ReportProgress(95, string.Format("处理完成，耗时 {0} 秒。", stopwatch.Elapsed.TotalSeconds.ToString("F2")));
			stopwatch.Stop();
		}

		private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			TextBlock textBlock = this.txtMsg;
			textBlock.Text = textBlock.Text + Environment.NewLine + e.UserState.ToString();
			double double_ = Convert.ToDouble(e.ProgressPercentage);
			this.method_2(double_);
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead("VersionID");
			this.method_2(100.0);
			this.btnSave.IsEnabled = true;
			this.method_3();
			MessageBox.Show(string.Concat(new string[]
			{
				"处理完成！",
				Environment.NewLine,
				Environment.NewLine,
				"目前版本：",
				this.VersionID
			}), "版本更新", MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_1()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			this.progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		private void method_2(double double_0)
		{
			this.progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.progressBar1.SetValue), DispatcherPriority.Background, new object[]
			{
				RangeBase.ValueProperty,
				double_0
			});
		}

		private void method_3()
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

		private void method_4(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(string.Concat(new string[]
			{
				"      该系统的技术支持由 G.S.S.Y. (中国) 提供。",
				Environment.NewLine,
				Environment.NewLine,
				"      此系统的版权属于 G.S.S.Y. 成员所有。任何人未经G.S.S.Y. 成员的书面许可，不能对该软件进行任何形式的逆向工程、破译以及修改任何信息。G.S.S.Y. 成员对上述行为保留对其追究法律责任的权利。",
				Environment.NewLine,
				Environment.NewLine,
				" G.S.S.Y. (China)   Email：gssycn@QQ.com"
			}), "版权声明", MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_5(string string_0)
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName == string_0)
				{
					process.Kill();
				}
			}
		}

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private string strRarFile = "";

		private string strRarOutputFolder = "";

		private string strRarPassword = "GSSY.capi";

		private string VersionID = "";

		private BackgroundWorker bw;
	}
}
