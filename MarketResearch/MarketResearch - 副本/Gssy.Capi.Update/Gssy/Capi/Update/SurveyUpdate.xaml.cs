﻿using System;
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
			this.method_5(GClass0.smethod_0("NŻɴͿЫՇ٢ݲࡨ"));
			this.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead(GClass0.smethod_0("_ŭɵ͵Ѭի٭݋ࡅ"));
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText(GClass0.smethod_0("VņɇͬѦդ"));
			this.txtPCCode.Text = ((byCodeText == null) ? GClass0.smethod_0("9昮覽籬п") : byCodeText);
			this.txtMsg.Text = GClass0.smethod_0("彔卋砎嶋癋戮ة") + this.VersionID + GClass0.smethod_0("(");
			this.txtProjectName.Text = SurveyMsg.MsgProjectName + GClass0.smethod_0(".襠䰅旷憲Ԩ");
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(GClass0.smethod_0("迁伽擢暥惙䩏娔䠋膖翙禾尥窈眀傅絁眤䱰盥磑畡伏") + Environment.NewLine + Environment.NewLine + GClass0.smethod_0("昿唩穠袩泌拮塼懽涸ॄੇ୕్眈傍"), GClass0.smethod_0("牎昩擰暳籬躥"), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
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
				this.bw.ReportProgress(90, GClass0.smethod_0("朤臰跒悮指垨偠ܫ菱懅淡瑒狞"));
				MessageBox.Show(GClass0.smethod_0("朤臰跒悮指垨偠菱懅淡瑒狞"), GClass0.smethod_0("牌是擶暱"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			if (!flag)
			{
				this.bw.ReportProgress(90, GClass0.smethod_0("孑冠攊冧剭壶规思"));
				MessageBox.Show(GClass0.smethod_0("孑冠攊冧剭壶规思"), GClass0.smethod_0("牌是擶暱"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			string str = "";
			int num = this.VersionID.ToLower().IndexOf(GClass0.smethod_0("w"));
			if (num > -1)
			{
				str = this.VersionID.Substring(0, num);
			}
			else if (this.VersionID.IndexOf(GClass0.smethod_0("浈諗灉")) > -1)
			{
				str = GClass0.smethod_0("浈諗灉");
			}
			else if (this.VersionID.IndexOf(GClass0.smethod_0("漗砸灉")) > -1)
			{
				str = GClass0.smethod_0("漗砸灉");
			}
			else if (this.VersionID.IndexOf(GClass0.smethod_0("歠帍灉")) > -1)
			{
				str = GClass0.smethod_0("歠帍灉");
			}
			else if (this.VersionID.IndexOf(GClass0.smethod_0("辆厫灉")) > -1)
			{
				str = GClass0.smethod_0("辆厫灉");
			}
			ObjectListing objectListing = ossClient.ListObjects(alioss.bucketNameUpdate, alioss.bucketDirUpdate + GClass0.smethod_0(".") + str);
			if (objectListing.ObjectSummaries.Count<OssObjectSummary>() == 0)
			{
				this.bw.ReportProgress(90, GClass0.smethod_0("朣昁灏搪拱悴掄䧴"));
				MessageBox.Show(GClass0.smethod_0("朣昁灏搪拱悴掄䧴"), GClass0.smethod_0("牌是擶暱"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
				if (!(ossObjectSummary.Key == alioss.bucketDirUpdate + GClass0.smethod_0(".")))
				{
					text = ossObjectSummary.Key.Replace(alioss.bucketDirUpdate + GClass0.smethod_0("."), "");
					text4 = text.Replace(GClass0.smethod_0("*űɣͳ"), "").ToLower();
					key = ossObjectSummary.Key;
					if (string.Compare(text4, text3) > 0)
					{
						text3 = text4;
					}
				}
			}
			if (text3 == text2)
			{
				this.bw.ReportProgress(90, GClass0.smethod_0("彙卄忺緈戩戅掴畋漮"));
				MessageBox.Show(GClass0.smethod_0("彙卄忺緈戩戅掴畋漮"), GClass0.smethod_0("牌是擶暱"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			string text5 = Environment.CurrentDirectory + GClass0.smethod_0("VōɧͰѨթ٫ݢࡦढ़");
			if (!Directory.Exists(text5))
			{
				Directory.CreateDirectory(text5);
			}
			if (!Directory.Exists(text5))
			{
				this.bw.ReportProgress(90, GClass0.smethod_0("曱撴嬲踧ﬀ"));
				MessageBox.Show(string.Concat(new string[]
				{
					GClass0.smethod_0("曤撿灆搡懺杋鄊躈續沀䓰刼䈉噛太"),
					Environment.NewLine,
					Environment.NewLine,
					GClass0.smethod_0("详嘸擻暾噀蓦蹇龎奁埲烌䗣䈎梃䃵嘻"),
					Environment.NewLine,
					text5
				}), GClass0.smethod_0("曰撳嬳踤"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
				stopwatch.Stop();
				return;
			}
			RarFile rarFile = new RarFile();
			string path = text5 + text;
			this.bw.ReportProgress(40, string.Format(GClass0.smethod_0("丂蹵枀䷰Хտسݿࠡ"), text));
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
			GetObjectRequest getObjectRequest = new GetObjectRequest(alioss.bucketNameUpdate, key);
			ossClient.GetObject(getObjectRequest, fileStream);
			fileStream.Close();
			this.bw.ReportProgress(80, string.Format(GClass0.smethod_0("觪劃枀䷰Хտسݿࠡ"), text));
			this.strRarFile = path;
			this.strRarOutputFolder = Environment.CurrentDirectory + GClass0.smethod_0("]");
			rarFile.Extract(this.strRarFile, this.strRarOutputFolder, this.strRarOutputFolder, this.strRarPassword);
			this.bw.ReportProgress(95, GClass0.smethod_0("牎昩擰暳䨸福") + text3);
			this.bw.ReportProgress(95, string.Format(GClass0.smethod_0("夊甋妀愛ﬆ蔞揾ܧࡽव੹ଣ痐㴃"), stopwatch.Elapsed.TotalSeconds.ToString(GClass0.smethod_0("Dĳ"))));
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
			this.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead(GClass0.smethod_0("_ŭɵ͵Ѭի٭݋ࡅ"));
			this.method_2(100.0);
			this.btnSave.IsEnabled = true;
			this.method_3();
			MessageBox.Show(string.Concat(new string[]
			{
				GClass0.smethod_0("夁甂妏愒ﬀ"),
				Environment.NewLine,
				Environment.NewLine,
				GClass0.smethod_0("盫卉灋搮﬛"),
				this.VersionID
			}), GClass0.smethod_0("牌是擶暱"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
				GClass0.smethod_0("\u0001Āȿ̾нԼ跾篡盆羜梗氹椺渕笢༲ၖᄾቜጠᑞᔢᙒᜤᠩᤠ吪䷻ᰬᴤ緓備ဃ"),
				Environment.NewLine,
				Environment.NewLine,
				GClass0.smethod_0("NōɌ͋ъՉ洌箜皹翡砬氠值䏯เ༘ၰᄎቲገᑴᔀᙶ᝷穆䴍研籚ⱐ厪儅凵䝤徂∋⍥␙╧☛❩⠟⥫⩤䥓砚寅怦록뮆拒촰紶듇滀뷝롘盀뛮뉸痈獧打愿䦫퀨ᔼΈ㤠琫㴝췹ূᯬۋ⼝טͷˀ⹏缝偙儳剏匵呉唷噁圹堶㬅์êመ틡활ᄵ⿓ᑔ㧵㉽὿૝㢌⟾ᲀీ㸫崃"),
				Environment.NewLine,
				Environment.NewLine,
				GClass0.smethod_0("\ašȋͷЍձ؏ݹ࠱ाਵୟ౳൳๷ཹှᄶስጴᑖᕿᙰ᝹ᡣᩪ᭿ᱸᵳṪὦ⁇⅗≔〉①╭♬")
			}), GClass0.smethod_0("牌晀嫲攏"), MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

		private string strRarPassword = GClass0.smethod_0("Nśɔ͟Ыէ٢ݲࡨ");

		private string VersionID = "";

		private BackgroundWorker bw;
	}
}
