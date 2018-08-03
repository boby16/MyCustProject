using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi
{
	public class SurveyAuto : Window, IComponentConnector
	{
		private bool IsFinish;

		private AutoAnswer oAutoAnswer = new AutoAnswer();

		internal TextBlock txtQuestionTitle;

		internal RichTextBox RTxtQInfo;

		internal RichTextBox RTxtQRun;

		internal RichTextBox RTxtQSysInfo;

		internal Button btnRun;

		internal Button btnReport;

		internal Button btnExit;

		internal Button btnTest;

		private bool _contentLoaded;

		public string SurveyId
		{
			get;
			set;
		}

		public SurveyAuto()
		{
			InitializeComponent();
		}

		private void _003F51_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0221: Incompatible stack heights: 0 vs 2
			//IL_023b: Incompatible stack heights: 0 vs 1
			RTxtQRun.AppendText(_003F487_003F._003F488_003F("刔壃儑雨坲Ԫحܬ\u082f") + Environment.NewLine);
			int num = SurveyHelper.SurveySequence;
			SurveyId = SurveyInit();
			oAutoAnswer.SurveyId = SurveyId;
			SurveyHelper.SurveyCity = _003F487_003F._003F488_003F("0");
			SurveyHelper.SurveyID = SurveyId;
			RTxtQRun.AppendText(_003F487_003F._003F488_003F("廲篌韨偲Ъԭجܯ") + SurveyId + Environment.NewLine);
			string text = SurveyHelper.SurveyFirstPage;
			string text2 = _003F487_003F._003F488_003F("");
			goto IL_01de;
			IL_01de:
			while (!IsFinish)
			{
				SurveyHelper.NavCurPage = text;
				RTxtQRun.AppendText(_003F487_003F._003F488_003F("门割驱\u0323Юԡ") + text + Environment.NewLine);
				int num2 = oAutoAnswer.PageInfo(text);
				for (int i = 0; i < num2; i++)
				{
					text2 = oAutoAnswer.GetChildByIndex(text, i);
					oAutoAnswer.QuestionInit(SurveyId, text2);
					RTxtQInfo.AppendText(oAutoAnswer.QuestionInfo());
					if (oAutoAnswer.MySurveyDefine.QUESTION_TYPE < 4)
					{
						oAutoAnswer.SetMain(SurveyId, text2, num);
						RTxtQRun.AppendText(_003F487_003F._003F488_003F("筐楋ȸ\u0321") + oAutoAnswer.MyAnswer + Environment.NewLine);
					}
				}
				AutoAnswer oAutoAnswer2 = oAutoAnswer;
				string surveyId = ((SurveyAuto)/*Error near IL_015c: Stack underflow*/).SurveyId;
				int _003F286_003F = num;
				string _003F279_003F = text;
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				text = ((AutoAnswer)/*Error near IL_0168: Stack underflow*/).NextPage(surveyId, _003F286_003F, _003F279_003F, roadMapVersion);
				RTxtQRun.AppendText(_003F487_003F._003F488_003F("闤剾驽軨焷锾覕ܣ\u082eड") + oAutoAnswer.RouteLogic + Environment.NewLine);
				num = (SurveyHelper.SurveySequence = num + 1);
				if (!(text == _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ")))
				{
					bool flag = text == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64");
					if ((int)/*Error near IL_0240: Stack underflow*/ == 0)
					{
						continue;
					}
				}
				RTxtQRun.AppendText(_003F487_003F._003F488_003F("门割驱\u0323Юԡ") + text + Environment.NewLine);
				IsFinish = true;
			}
			return;
			IL_024b:
			goto IL_01de;
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Application.Current.Shutdown();
		}

		public string SurveyInit()
		{
			string text = _003F487_003F._003F488_003F("2");
			int _003F287_003F = 5;
			string autoSurveyId = oAutoAnswer.GetAutoSurveyId(text, _003F287_003F);
			string versionID = SurveyMsg.VersionID;
			SurveyHelper.SurveyCity = text;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.SurveyExtend1 = _003F487_003F._003F488_003F("0");
			string surveyExtend = SurveyHelper.SurveyExtend1;
			oAutoAnswer.SurveyInit(autoSurveyId, versionID, surveySequence, text, surveyExtend);
			return autoSurveyId;
		}

		private void _003F52_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
		}

		private void _003F53_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\vŤɑ\u0352љԱ\u065dݼ\u086cॲਡ\u0b7a\u0c77ൺ\u0e66\u0f7aၺᅶቼ፥ᐿᕼᙻ\u177f\u187a\u196e\u1a73᭨\u1c7dᵳṩἫ⁼Ⅲ≯⍭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
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
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 2:
				RTxtQInfo = (RichTextBox)_003F350_003F;
				break;
			case 3:
				RTxtQRun = (RichTextBox)_003F350_003F;
				break;
			case 4:
				RTxtQSysInfo = (RichTextBox)_003F350_003F;
				break;
			case 5:
				btnRun = (Button)_003F350_003F;
				btnRun.Click += _003F51_003F;
				break;
			case 6:
				btnReport = (Button)_003F350_003F;
				btnReport.Click += _003F52_003F;
				break;
			case 7:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 8:
				btnTest = (Button)_003F350_003F;
				btnTest.Click += _003F53_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_002d:
			goto IL_0037;
		}
	}
}
