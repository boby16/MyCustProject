using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.QEdit
{
	// Token: 0x02000056 RID: 86
	public partial class DebugCheck : Window
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x00097A00 File Offset: 0x00095C00
		public DebugCheck()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00097B68 File Offset: 0x00095D68
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.txtHelper.Text = this.method_1();
			this.PageId = SurveyHelper.NavCurPage;
			this.SurveyId = SurveyHelper.SurveyID;
			this.oLogicEngine.SurveyID = SurveyHelper.SurveyID;
			this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
			this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
			this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
			this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
			this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
			this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
			this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			if (SurveyHelper.SurveyID == global::GClass0.smethod_0(""))
			{
				this.TabItem3.IsEnabled = false;
				this.TabItem4.IsEnabled = false;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00097C90 File Offset: 0x00095E90
		private string method_1()
		{
			string str = global::GClass0.smethod_0("") + Environment.NewLine + global::GClass0.smethod_0("%Īǫ̈̄ЩԮدܬ࠭䜅䐎鍸睘敃ื༴ဵᄺሻጸᐹᔾᘿ᜼");
			SurveyBiz surveyBiz = new SurveyBiz();
			return str + surveyBiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00003A5D File Offset: 0x00001C5D
		private void method_2(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(1);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00097CE8 File Offset: 0x00095EE8
		private void btnQuery_Click(object sender, RoutedEventArgs e)
		{
			bool flag = true;
			if (this.Chk_Filter.IsChecked == true)
			{
				this.IsFilter = false;
			}
			else
			{
				this.IsFilter = true;
			}
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			if (this.Chk_SurveyDefine.IsChecked == true)
			{
				this.method_3();
			}
			if (this.Chk_SurveyDetail.IsChecked == true)
			{
				this.method_4();
			}
			if (this.Chk_SurveyRoadmap.IsChecked == true)
			{
				this.method_5();
			}
			if (this.Chk_SurveyLogic.IsChecked == true)
			{
				this.method_9();
			}
			if (this.Chk_S_Define.IsChecked == true)
			{
				this.method_6();
			}
			if (this.Chk_SurveyRandomBase.IsChecked == true)
			{
				this.method_7();
			}
			if (this.Chk_SurveyConfig.IsChecked == true)
			{
				this.method_10();
			}
			if (this.ChkW_SurveyMain.IsChecked == true)
			{
				this.method_13();
			}
			if (this.ChkW_SurveyAnswer.IsChecked == true)
			{
				this.method_14(1);
			}
			if (this.ChkW_SurveySequence.IsChecked == true)
			{
				this.method_15();
			}
			if (this.ChkW_SurveyRandom.IsChecked == true)
			{
				this.method_16();
			}
			if (this.ChkW_SurveyAnswerHis.IsChecked == true)
			{
				this.method_17();
			}
			if (flag)
			{
				this.txtTable.Text = SurveyMsg.MsgCurrentContent + this.TableName;
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNoFunction, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00097F50 File Offset: 0x00096150
		private void method_3()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվقݠࡢ४੬୤");
			SurveyDefineDal surveyDefineDal = new SurveyDefineDal();
			if (this.IsFilter)
			{
				this.lSurveyDefine = surveyDefineDal.GetListByPageId(this.FilterPageId);
			}
			else
			{
				this.lSurveyDefine = surveyDefineDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyDefine;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00097FAC File Offset: 0x000961AC
		private void method_4()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվقݠࡰॢ੫୭");
			SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
			if (this.IsFilter)
			{
				using (List<SurveyDefine>.Enumerator enumerator = this.lSurveyDefine.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SurveyDefine surveyDefine = enumerator.Current;
						if (surveyDefine.DETAIL_ID != global::GClass0.smethod_0(""))
						{
							this.lSurveyDetail = surveyDetailDal.GetDetails(surveyDefine.DETAIL_ID);
							break;
						}
					}
					goto IL_85;
				}
			}
			this.lSurveyDetail = surveyDetailDal.GetList();
			IL_85:
			this.DataGrid1.ItemsSource = this.lSurveyDetail;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00098060 File Offset: 0x00096260
		private void method_5()
		{
			this.TableName = global::GClass0.smethod_0("^ŹɹͼѬձٕݩࡤॠ੎ୣ౱");
			SurveyRoadMapDal surveyRoadMapDal = new SurveyRoadMapDal();
			if (this.IsFilter)
			{
				string string_ = string.Format(global::GClass0.smethod_0("CŊɂ͈я՟؊܃ࠈु੔୊౉ഃ๱པၒᅩቻ፤ᑎᕴᙻ᝽ᡕ᥶ᩦᬵᱣᵻṷὣ⁵ℯ≾⍬⑫╮♕❠⡬⤧⨻⬢Ɀⴳ⹿⼦"), this.FilterPageId);
				this.lSurveyRoadMap = surveyRoadMapDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyRoadMap = surveyRoadMapDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyRoadMap;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000980D0 File Offset: 0x000962D0
		private void method_6()
		{
			this.TableName = global::GClass0.smethod_0("[Řɂ͠Ѣժ٬ݤ");
			S_DefineDal s_DefineDal = new S_DefineDal();
			if (this.IsFilter)
			{
				string string_ = string.Format(global::GClass0.smethod_0("XŏɅ͍фՒ؅܎ࠃॄ੓୏౲ാ๎གྷၟᅿቿ፱ᑹᕳᘵᝣ᡻᥷ᩣ᭵ᰯᵾṬὫ⁮⅕≠⍬␧┻☢❿⠳⥿⨦"), this.FilterPageId);
				this.lS_Define = s_DefineDal.GetListBySql(string_);
			}
			else
			{
				this.lS_Define = s_DefineDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lS_Define;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00098140 File Offset: 0x00096340
		private void method_7()
		{
			this.TableName = global::GClass0.smethod_0("Cźɼͻѩղ٘ݨࡦॣ੩୨ెൢ๱ཤ");
			SurveyRandomBaseDal surveyRandomBaseDal = new SurveyRandomBaseDal();
			this.lSurveyRandomBase = surveyRandomBaseDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyRandomBase;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00098180 File Offset: 0x00096380
		private void method_8()
		{
			this.TableName = global::GClass0.smethod_0("UŚɎͶѯձ");
			S_JUMPDal s_JUMPDal = new S_JUMPDal();
			this.lS_Jump = s_JUMPDal.GetList();
			this.DataGrid1.ItemsSource = this.lS_Jump;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000981C0 File Offset: 0x000963C0
		private void method_9()
		{
			this.TableName = global::GClass0.smethod_0("Xſɻ;Ѣտىݫࡤ५੢");
			SurveyLogicDal surveyLogicDal = new SurveyLogicDal();
			if (this.IsFilter)
			{
				string string_ = string.Format(global::GClass0.smethod_0("]ňɀ͎щ՝؈܍ࠆृ੖ୌ౏ഁ๳ཪၬᅫቹ።ᑖᕶᙿ᝾ᡵᤵᩣ᭻ᱷᵣṵἯ⁾Ⅼ≫⍮⑕╠♬✧⠻⤢⩿⬳Ɀ⴦"), this.FilterPageId);
				this.lSurveyLogic = surveyLogicDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyLogic = surveyLogicDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyLogic;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00098230 File Offset: 0x00096430
		private void method_10()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվمݪࡪ॥੫୦");
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			this.lSurveyConfig = surveyConfigDal.GetListRead();
			this.DataGrid1.ItemsSource = this.lSurveyConfig;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00098270 File Offset: 0x00096470
		private void method_11()
		{
			this.TableName = global::GClass0.smethod_0("Yżɺͱѣռـݪࡡॵ");
			SurveyDictDal surveyDictDal = new SurveyDictDal();
			this.lSurveyDict = surveyDictDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyDict;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000982B0 File Offset: 0x000964B0
		private void method_12()
		{
			this.TableName = global::GClass0.smethod_0("Xſɻ;Ѣտِݷࡦ॰ੲ");
			SurveyUsersDal surveyUsersDal = new SurveyUsersDal();
			this.lSurveyUsers = surveyUsersDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyUsers;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000982F0 File Offset: 0x000964F0
		private void method_13()
		{
			this.TableName = global::GClass0.smethod_0("Yżɺͱѣռىݢ࡫९");
			SurveyMainDal surveyMainDal = new SurveyMainDal();
			if (this.IsFilter)
			{
				string string_ = string.Format(global::GClass0.smethod_0("\\ŋɁ͉ш՞؉܂ࠇी੗ୋ౎ം๲ཕၭᅨቸ፥ᑖᕻᙰ᝶ᠷᥡ᩽᭱ᱡᵷḱὃ⁚⅜≛⍉⑒╕♀❌⠧⤻⨢⭿ⰳ⵿⸦"), this.FilterSurveyId);
				this.lSurveyMain = surveyMainDal.GetListBySql(string_);
			}
			else
			{
				this.lSurveyMain = surveyMainDal.GetList();
			}
			this.DataGrid1.ItemsSource = this.lSurveyMain;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00098360 File Offset: 0x00096560
		private void method_14(int int_0 = 1)
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվهݫࡷॴ੧୳");
			SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
			if (this.IsFilter)
			{
				this.lSurveyAnswer = surveyAnswerDal.GetListBySequenceId(this.FilterSurveyId, SurveyHelper.SurveySequence - 1);
			}
			else
			{
				string string_ = string.Format(global::GClass0.smethod_0("&ıȿ̷вԤٯݤ࡭पਹଥత൨ด༳့ᄲሦጻ᐀ᔮᙌᝉᡘ᥎ᨛ᭍᱑ᵝṅὓ―Ⅷ≦⍠⑧╵♶❱⡤⥨⨋⬗Ⰾⵓ⸗⽛。㄄㉂㍌㑅㔀㙌㝻㡬㥩㩾㭴㱺㵽㹈㽿䁱䄴䈯䌲䐩䔠䘿䜾䠽䤬䩤䭸䱭䵭乵伦偧兽刣卫呥"), this.FilterSurveyId);
				this.lSurveyAnswer = surveyAnswerDal.GetListBySql(string_);
			}
			if (int_0 == 1)
			{
				this.DataGrid1.ItemsSource = this.lSurveyAnswer;
				return;
			}
			if (int_0 == 2)
			{
				this.DataGrid2.ItemsSource = this.lSurveyAnswer;
				return;
			}
			if (int_0 == 3)
			{
				this.DataGrid3.ItemsSource = this.lSurveyAnswer;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0009840C File Offset: 0x0009660C
		private void method_15()
		{
			this.TableName = global::GClass0.smethod_0("]Ÿɾͽѯհٛݢࡷ॰੡୭ౡ൤");
			SurveySequenceDal surveySequenceDal = new SurveySequenceDal();
			this.lSurveySequence = surveySequenceDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveySequence;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0009844C File Offset: 0x0009664C
		private void method_16()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվٔݤࡪ१੭୬");
			SurveyRandomDal surveyRandomDal = new SurveyRandomDal();
			this.lSurveyRandom = surveyRandomDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyRandom;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0009848C File Offset: 0x0009668C
		private void method_17()
		{
			this.TableName = global::GClass0.smethod_0("\\ŻɿͺѮճوݦࡴॱ੠୶ో൫๲");
			SurveyAnswerHisDal surveyAnswerHisDal = new SurveyAnswerHisDal();
			this.lSurveyAnswerHis = surveyAnswerHisDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyAnswerHis;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000984CC File Offset: 0x000966CC
		private void method_18()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվمݪࡪ॥੫୦");
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			this.lSurveyConfig = surveyConfigDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyConfig;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0009850C File Offset: 0x0009670C
		private void method_19()
		{
			this.TableName = global::GClass0.smethod_0("ZŽɵͰѠսُݭࡦ");
			SurveyLogDal surveyLogDal = new SurveyLogDal();
			this.lSurveyLog = surveyLogDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyLog;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0009854C File Offset: 0x0009674C
		private void method_20()
		{
			this.TableName = global::GClass0.smethod_0("_žɸͿѭվىݵࡰ४੭୯");
			SurveyOptionDal surveyOptionDal = new SurveyOptionDal();
			this.lSurveyOption = surveyOptionDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveyOption;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0009858C File Offset: 0x0009678C
		private void method_21()
		{
			this.TableName = global::GClass0.smethod_0("WŜɏ͕");
			S_MTDal s_MTDal = new S_MTDal();
			this.lS_MT = s_MTDal.GetList();
			this.DataGrid1.ItemsSource = this.lS_MT;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000985CC File Offset: 0x000967CC
		private void method_22()
		{
			this.TableName = global::GClass0.smethod_0("Yżɺͱѣռٗݺ࡬ॢ");
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			this.lSurveySync = surveySyncDal.GetList();
			this.DataGrid1.ItemsSource = this.lSurveySync;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00003A83 File Offset: 0x00001C83
		private void TabItem3_Initialized(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(3);
			this.txtQName.Focus();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0009860C File Offset: 0x0009680C
		private void DataGrid3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				return;
			}
			this.txtQName.Text = ((SurveyAnswer)this.DataGrid3.CurrentItem).QUESTION_NAME;
			if (this.txtLogic.Text == global::GClass0.smethod_0(""))
			{
				this.AutoAdd.IsChecked = new bool?(true);
			}
			if (this.AutoAdd.IsChecked == true)
			{
				TextBox textBox = this.txtLogic;
				textBox.Text = textBox.Text + ((SurveyAnswer)this.DataGrid3.CurrentItem).QUESTION_NAME + global::GClass0.smethod_0("!");
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000986D4 File Offset: 0x000968D4
		private void btnRunAnswer_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtQName.Text.Trim();
			if (text == global::GClass0.smethod_0(""))
			{
				this.txtQAnswer.Text = global::GClass0.smethod_0(" 諰华貖啡鶛嗵ܨ");
				this.txtQName.Focus();
				return;
			}
			SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
			string text2 = global::GClass0.smethod_0("Z") + surveyAnswerDal.GetOneCode(SurveyHelper.SurveyID, text) + global::GClass0.smethod_0("\\");
			if (text2 == global::GClass0.smethod_0("YŜ"))
			{
				text2 = global::GClass0.smethod_0("-筪ȱ̮Х曜鞙邑嫿凾刡倊岝歌渝ဨ");
			}
			this.txtQAnswer.Text = this.txtQName.Text + global::GClass0.smethod_0("#Ŀȡ") + text2;
			this.txtQName.Focus();
			this.txtQName.SelectAll();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000987AC File Offset: 0x000969AC
		private void btnRunLogic_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.oLogicEngine.boolResult(this.txtLogic.Text);
			this.txtLogicResult.Text = flag.ToString();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000987E4 File Offset: 0x000969E4
		private void btnRunText_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.strShowText(this.txtLogic.Text, true);
			if (text == global::GClass0.smethod_0("YŜ"))
			{
				text = global::GClass0.smethod_0("-筪ȱ̮Х曜鞙邑嫿凾刡倊岝歌渝ဨ");
			}
			this.txtLogicResult.Text = text;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00098834 File Offset: 0x00096A34
		private void btnRunRoute_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.Route(this.txtLogic.Text);
			if (text == global::GClass0.smethod_0("YŜ"))
			{
				text = global::GClass0.smethod_0("-筪ȱ̮Х曜鞙邑嫿凾刡倊岝歌渝ဨ");
			}
			this.txtLogicResult.Text = text;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00098884 File Offset: 0x00096A84
		private void btnRunOption_Click(object sender, RoutedEventArgs e)
		{
			string text = this.oLogicEngine.stringResult(this.txtLogic.Text);
			if (text == global::GClass0.smethod_0("YŜ"))
			{
				text = global::GClass0.smethod_0("-筪ȱ̮Х曜鞙邑嫿凾刡倊岝歌渝ဨ");
			}
			this.txtLogicResult.Text = text;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00003AB5 File Offset: 0x00001CB5
		private void txtQName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnRunAnswer_Click(sender, e);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00003AC8 File Offset: 0x00001CC8
		private void txtQName_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtQName.SelectAll();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00003AD5 File Offset: 0x00001CD5
		private void TabItem4_Initialized(object sender, EventArgs e)
		{
			this.IsFilter = false;
			this.FilterSurveyId = SurveyHelper.SurveyID;
			this.FilterPageId = SurveyHelper.NavCurPage;
			this.method_14(2);
			this.txtQuestionName.Focus();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000988D4 File Offset: 0x00096AD4
		private void DataGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				this.txtNewAnswer.Focus();
				this.txtNewAnswer.SelectAll();
				return;
			}
			this.txtQuestionName.Text = ((SurveyAnswer)this.DataGrid2.CurrentItem).QUESTION_NAME;
			this.btnGetAnswer_Click(null, null);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00098934 File Offset: 0x00096B34
		private void btnGetAnswer_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.btnGetAnswer.Opacity == this.ButtonDisable)
			{
				return;
			}
			string text = this.txtQuestionName.Text;
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtQuestionName.Focus();
				return;
			}
			string oneCode = new SurveyAnswerDal().GetOneCode(SurveyHelper.SurveyID, text);
			this.txtAnswer.Text = oneCode;
			if (this.txtAnswer.Text == global::GClass0.smethod_0(""))
			{
				this.txtAnswer.Text = global::GClass0.smethod_0("东婈唧賗鲕繘湃吏簠澷厘榼刋䇞呚");
			}
			this.txtNewAnswer.Text = oneCode;
			this.txtQuestionName.IsEnabled = false;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.txtNewAnswer.IsEnabled = true;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.btnGetAnswer.Opacity = this.ButtonDisable;
			this.btnSave.Opacity = this.ButtonEnable;
			this.btnCancel.Opacity = this.ButtonEnable;
			this.txtNewAnswer.Focus();
			this.txtNewAnswer.SelectAll();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00003B07 File Offset: 0x00001D07
		private void txtQuestionName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnGetAnswer_Click(sender, e);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000234B File Offset: 0x0000054B
		private void btnKeyboard1_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00003B1A File Offset: 0x00001D1A
		private void txtNewAnswer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00098A98 File Offset: 0x00096C98
		private void btnSave_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.btnSave.Opacity == this.ButtonDisable)
			{
				return;
			}
			string text = this.txtAnswer.Text;
			string text2 = this.txtNewAnswer.Text;
			if (text != text2)
			{
				string text3 = this.txtQuestionName.Text;
				if (text2 == global::GClass0.smethod_0("") && MessageBox.Show(global::GClass0.smethod_0("昿唩悄祷牱繟湂屑洶嬷藟䔯応鳌䀯"), SurveyMsg.MsgCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
				{
					return;
				}
				new SurveyAnswerDal().AddOne(SurveyHelper.SurveyID, text3, text2, 8888);
				this.method_14(2);
				this.method_14(3);
			}
			this.txtQuestionName.IsEnabled = true;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.txtNewAnswer.IsEnabled = false;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.btnGetAnswer.Opacity = this.ButtonEnable;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			this.txtQuestionName.Focus();
			this.txtQuestionName.SelectAll();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00098BE0 File Offset: 0x00096DE0
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.txtQuestionName.IsEnabled = true;
			this.txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			this.txtNewAnswer.IsEnabled = false;
			this.txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			this.btnGetAnswer.Opacity = this.ButtonEnable;
			this.btnSave.Opacity = this.ButtonDisable;
			this.btnCancel.Opacity = this.ButtonDisable;
			this.method_14(2);
			this.txtQuestionName.Focus();
			this.txtQuestionName.SelectAll();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00098CA0 File Offset: 0x00096EA0
		public void Refresh()
		{
			DispatcherFrame dispatcherFrame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(DebugCheck.Class62.instance.method_0), dispatcherFrame);
			Dispatcher.PushFrame(dispatcherFrame);
		}

		// Token: 0x04000A56 RID: 2646
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000A57 RID: 2647
		private string TableName;

		// Token: 0x04000A58 RID: 2648
		private bool IsFilter = true;

		// Token: 0x04000A59 RID: 2649
		private string FilterSurveyId = global::GClass0.smethod_0("");

		// Token: 0x04000A5A RID: 2650
		private string FilterPageId = global::GClass0.smethod_0("");

		// Token: 0x04000A5B RID: 2651
		private List<S_Define> lS_Define = new List<S_Define>();

		// Token: 0x04000A5C RID: 2652
		private List<SurveyRandom> lSurveyRandomBase = new List<SurveyRandom>();

		// Token: 0x04000A5D RID: 2653
		private List<S_JUMP> lS_Jump = new List<S_JUMP>();

		// Token: 0x04000A5E RID: 2654
		private List<SurveyDefine> lSurveyDefine = new List<SurveyDefine>();

		// Token: 0x04000A5F RID: 2655
		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		// Token: 0x04000A60 RID: 2656
		private List<SurveyRoadMap> lSurveyRoadMap = new List<SurveyRoadMap>();

		// Token: 0x04000A61 RID: 2657
		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		// Token: 0x04000A62 RID: 2658
		private List<SurveyConfig> lSurveyConfig = new List<SurveyConfig>();

		// Token: 0x04000A63 RID: 2659
		private List<SurveyDict> lSurveyDict = new List<SurveyDict>();

		// Token: 0x04000A64 RID: 2660
		private List<SurveyUsers> lSurveyUsers = new List<SurveyUsers>();

		// Token: 0x04000A65 RID: 2661
		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		// Token: 0x04000A66 RID: 2662
		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		// Token: 0x04000A67 RID: 2663
		private List<SurveySequence> lSurveySequence = new List<SurveySequence>();

		// Token: 0x04000A68 RID: 2664
		private List<SurveyRandom> lSurveyRandom = new List<SurveyRandom>();

		// Token: 0x04000A69 RID: 2665
		private List<SurveyAnswerHis> lSurveyAnswerHis = new List<SurveyAnswerHis>();

		// Token: 0x04000A6A RID: 2666
		private List<SurveyLog> lSurveyLog = new List<SurveyLog>();

		// Token: 0x04000A6B RID: 2667
		private List<SurveyOption> lSurveyOption = new List<SurveyOption>();

		// Token: 0x04000A6C RID: 2668
		private List<S_MT> lS_MT = new List<S_MT>();

		// Token: 0x04000A6D RID: 2669
		private List<SurveySync> lSurveySync = new List<SurveySync>();

		// Token: 0x04000A6E RID: 2670
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000A6F RID: 2671
		private string PageId = global::GClass0.smethod_0("");

		// Token: 0x04000A70 RID: 2672
		private string SurveyId = global::GClass0.smethod_0("");

		// Token: 0x04000A71 RID: 2673
		private double ButtonEnable = 1.0;

		// Token: 0x04000A72 RID: 2674
		private double ButtonDisable = 0.5;

		// Token: 0x020000BD RID: 189
		[CompilerGenerated]
		[Serializable]
		private sealed class Class62
		{
			// Token: 0x060007A1 RID: 1953 RVA: 0x000046BE File Offset: 0x000028BE
			internal object method_0(object object_0)
			{
				((DispatcherFrame)object_0).Continue = false;
				return null;
			}

			// Token: 0x04000D44 RID: 3396
			public static readonly DebugCheck.Class62 instance = new DebugCheck.Class62();

			// Token: 0x04000D45 RID: 3397
			public static DispatcherOperationCallback compare0;
		}
	}
}
