using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.QEdit
{
	public class DebugCheck : Window, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static DispatcherOperationCallback _003C_003E9__71_0;

			internal object _003F333_003F(object _003F485_003F)
			{
				((DispatcherFrame)_003F485_003F).Continue = false;
				return null;
			}
		}

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private string TableName;

		private bool IsFilter = true;

		private string FilterSurveyId = _003F487_003F._003F488_003F("");

		private string FilterPageId = _003F487_003F._003F488_003F("");

		private List<S_Define> lS_Define = new List<S_Define>();

		private List<SurveyRandom> lSurveyRandomBase = new List<SurveyRandom>();

		private List<S_JUMP> lS_Jump = new List<S_JUMP>();

		private List<SurveyDefine> lSurveyDefine = new List<SurveyDefine>();

		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		private List<SurveyRoadMap> lSurveyRoadMap = new List<SurveyRoadMap>();

		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		private List<SurveyConfig> lSurveyConfig = new List<SurveyConfig>();

		private List<SurveyDict> lSurveyDict = new List<SurveyDict>();

		private List<SurveyUsers> lSurveyUsers = new List<SurveyUsers>();

		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		private List<SurveySequence> lSurveySequence = new List<SurveySequence>();

		private List<SurveyRandom> lSurveyRandom = new List<SurveyRandom>();

		private List<SurveyAnswerHis> lSurveyAnswerHis = new List<SurveyAnswerHis>();

		private List<SurveyLog> lSurveyLog = new List<SurveyLog>();

		private List<SurveyOption> lSurveyOption = new List<SurveyOption>();

		private List<S_MT> lS_MT = new List<S_MT>();

		private List<SurveySync> lSurveySync = new List<SurveySync>();

		private LogicEngine oLogicEngine = new LogicEngine();

		private string PageId = _003F487_003F._003F488_003F("");

		private string SurveyId = _003F487_003F._003F488_003F("");

		private double ButtonEnable = 1.0;

		private double ButtonDisable = 0.5;

		internal TextBox txtHelper;

		internal CheckBox Chk_Filter;

		internal RadioButton Chk_SurveyDefine;

		internal RadioButton Chk_SurveyDetail;

		internal RadioButton Chk_SurveyRoadmap;

		internal RadioButton Chk_SurveyLogic;

		internal RadioButton Chk_S_Define;

		internal RadioButton Chk_SurveyRandomBase;

		internal RadioButton Chk_SurveyConfig;

		internal RadioButton ChkW_SurveyMain;

		internal RadioButton ChkW_SurveyAnswer;

		internal RadioButton ChkW_SurveySequence;

		internal RadioButton ChkW_SurveyRandom;

		internal RadioButton ChkW_SurveyAnswerHis;

		internal TextBlock txtTable;

		internal Button btnQuery;

		internal DataGrid DataGrid1;

		internal TabItem TabItem3;

		internal DataGrid DataGrid3;

		internal TextBox txtQName;

		internal Button btnRunAnswer;

		internal Button btnKeyboard;

		internal TextBox txtQAnswer;

		internal CheckBox AutoAdd;

		internal TextBox txtLogic;

		internal Button btnRunText;

		internal Button btnRunLogic;

		internal Button btnRunRoute;

		internal Button btnRunOption;

		internal TextBox txtLogicResult;

		internal TabItem TabItem4;

		internal DataGrid DataGrid2;

		internal Button btnKeyboard1;

		internal TextBox txtQuestionName;

		internal Button btnGetAnswer;

		internal TextBlock txtAnswerTitle;

		internal TextBox txtAnswer;

		internal TextBlock txtNewAnswerTitle;

		internal TextBox txtNewAnswer;

		internal Button btnSave;

		internal Button btnCancel;

		private bool _contentLoaded;

		public DebugCheck()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0126: Incompatible stack heights: 0 vs 1
			base.Topmost = true;
			Hide();
			Show();
			txtHelper.Text = _003F218_003F();
			PageId = SurveyHelper.NavCurPage;
			SurveyId = SurveyHelper.SurveyID;
			oLogicEngine.SurveyID = SurveyHelper.SurveyID;
			oLogicEngine.CircleACode = SurveyHelper.CircleACode;
			oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
			oLogicEngine.CircleACount = SurveyHelper.CircleACount;
			oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
			oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
			oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
			oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			btnSave.Opacity = ButtonDisable;
			btnCancel.Opacity = ButtonDisable;
			if (SurveyHelper.SurveyID == _003F487_003F._003F488_003F(""))
			{
				TabItem3.IsEnabled = false;
				((DebugCheck)/*Error near IL_010a: Stack underflow*/).TabItem4.IsEnabled = false;
			}
		}

		private string _003F218_003F()
		{
			string str = _003F487_003F._003F488_003F("") + Environment.NewLine + _003F487_003F._003F488_003F("%Īȫ\u0328ЩԮدܬ\u082d䜅䐎鍸睘敃\u0e37༴\u1035ᄺሻጸᐹᔾᘿ\u173c");
			SurveyBiz surveyBiz = new SurveyBiz();
			return str + surveyBiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo();
		}

		private void _003F219_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			IsFilter = false;
			FilterSurveyId = SurveyHelper.SurveyID;
			FilterPageId = SurveyHelper.NavCurPage;
			_003F231_003F(1);
		}

		private void _003F201_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_027e: Incompatible stack heights: 0 vs 1
			//IL_0284: Incompatible stack heights: 0 vs 1
			//IL_0289: Incompatible stack heights: 1 vs 0
			//IL_02aa: Incompatible stack heights: 0 vs 1
			//IL_02af: Incompatible stack heights: 1 vs 0
			//IL_02ca: Incompatible stack heights: 0 vs 1
			//IL_02cf: Incompatible stack heights: 1 vs 0
			//IL_02ea: Incompatible stack heights: 0 vs 1
			//IL_02ef: Incompatible stack heights: 1 vs 0
			//IL_030a: Incompatible stack heights: 0 vs 1
			//IL_030f: Incompatible stack heights: 1 vs 0
			//IL_032a: Incompatible stack heights: 0 vs 1
			//IL_032f: Incompatible stack heights: 1 vs 0
			//IL_034a: Incompatible stack heights: 0 vs 1
			//IL_034f: Incompatible stack heights: 1 vs 0
			//IL_036a: Incompatible stack heights: 0 vs 1
			//IL_036f: Incompatible stack heights: 1 vs 0
			//IL_038a: Incompatible stack heights: 0 vs 1
			//IL_038f: Incompatible stack heights: 1 vs 0
			//IL_03aa: Incompatible stack heights: 0 vs 1
			//IL_03af: Incompatible stack heights: 1 vs 0
			//IL_03cb: Incompatible stack heights: 0 vs 1
			//IL_03d0: Incompatible stack heights: 1 vs 0
			//IL_03eb: Incompatible stack heights: 0 vs 1
			//IL_03f0: Incompatible stack heights: 1 vs 0
			//IL_040b: Incompatible stack heights: 0 vs 1
			//IL_0410: Incompatible stack heights: 1 vs 0
			//IL_0435: Incompatible stack heights: 0 vs 2
			bool? isChecked = Chk_Filter.IsChecked;
			bool flag = true;
			bool hasValue;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue = isChecked.HasValue;
			}
			if (!hasValue)
			{
				IsFilter = true;
			}
			else
			{
				IsFilter = false;
			}
			FilterSurveyId = SurveyHelper.SurveyID;
			FilterPageId = SurveyHelper.NavCurPage;
			isChecked = Chk_SurveyDefine.IsChecked;
			flag = true;
			bool hasValue2;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue2 = isChecked.HasValue;
			}
			if (hasValue2)
			{
				_003F220_003F();
			}
			isChecked = Chk_SurveyDetail.IsChecked;
			flag = true;
			bool hasValue3;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue3 = isChecked.HasValue;
			}
			if (hasValue3)
			{
				_003F221_003F();
			}
			isChecked = Chk_SurveyRoadmap.IsChecked;
			flag = true;
			bool hasValue4;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue4 = isChecked.HasValue;
			}
			if (hasValue4)
			{
				_003F222_003F();
			}
			isChecked = Chk_SurveyLogic.IsChecked;
			flag = true;
			bool hasValue5;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue5 = isChecked.HasValue;
			}
			if (hasValue5)
			{
				_003F226_003F();
			}
			isChecked = Chk_S_Define.IsChecked;
			flag = true;
			bool hasValue6;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue6 = isChecked.HasValue;
			}
			if (hasValue6)
			{
				_003F223_003F();
			}
			isChecked = Chk_SurveyRandomBase.IsChecked;
			flag = true;
			bool hasValue7;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue7 = isChecked.HasValue;
			}
			if (hasValue7)
			{
				_003F224_003F();
			}
			isChecked = Chk_SurveyConfig.IsChecked;
			flag = true;
			bool hasValue8;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue8 = isChecked.HasValue;
			}
			if (hasValue8)
			{
				_003F227_003F();
			}
			isChecked = ChkW_SurveyMain.IsChecked;
			flag = true;
			bool hasValue9;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue9 = isChecked.HasValue;
			}
			if (hasValue9)
			{
				_003F230_003F();
			}
			isChecked = ChkW_SurveyAnswer.IsChecked;
			flag = true;
			bool hasValue10;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue10 = isChecked.HasValue;
			}
			if (hasValue10)
			{
				_003F231_003F(1);
			}
			isChecked = ChkW_SurveySequence.IsChecked;
			flag = true;
			bool hasValue11;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue11 = isChecked.HasValue;
			}
			if (hasValue11)
			{
				_003F232_003F();
			}
			isChecked = ChkW_SurveyRandom.IsChecked;
			flag = true;
			bool hasValue12;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue12 = isChecked.HasValue;
			}
			if (hasValue12)
			{
				_003F233_003F();
			}
			isChecked = ChkW_SurveyAnswerHis.IsChecked;
			flag = true;
			bool hasValue13;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue13 = isChecked.HasValue;
			}
			if (hasValue13)
			{
				_003F234_003F();
			}
			if ((int)/*Error near IL_0425: Stack underflow*/ != 0)
			{
				TextBlock txtTable2 = txtTable;
				string msgCurrentContent = SurveyMsg.MsgCurrentContent;
				string text = string.Concat(str1: TableName, str0: (string)/*Error near IL_026f: Stack underflow*/);
				((TextBlock)/*Error near IL_0274: Stack underflow*/).Text = text;
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgNoFunction, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void _003F220_003F()
		{
			//IL_0048: Incompatible stack heights: 0 vs 2
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվقݠ\u0862४੬\u0b64");
			SurveyDefineDal surveyDefineDal = new SurveyDefineDal();
			if (IsFilter)
			{
				surveyDefineDal.GetListByPageId(FilterPageId);
				((DebugCheck)/*Error near IL_0026: Stack underflow*/).lSurveyDefine = (List<SurveyDefine>)/*Error near IL_0026: Stack underflow*/;
			}
			else
			{
				lSurveyDefine = surveyDefineDal.GetList();
			}
			DataGrid1.ItemsSource = lSurveyDefine;
		}

		private void _003F221_003F()
		{
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվقݠ\u0870\u0962੫୭");
			SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
			if (IsFilter)
			{
				foreach (SurveyDefine item in lSurveyDefine)
				{
					if (item.DETAIL_ID != _003F487_003F._003F488_003F(""))
					{
						lSurveyDetail = surveyDetailDal.GetDetails(item.DETAIL_ID);
						break;
					}
				}
			}
			else
			{
				lSurveyDetail = surveyDetailDal.GetList();
			}
			DataGrid1.ItemsSource = lSurveyDetail;
		}

		private void _003F222_003F()
		{
			//IL_0059: Incompatible stack heights: 0 vs 1
			TableName = _003F487_003F._003F488_003F("^ŹɹͼѬձ\u0655ݩ\u0864ॠ\u0a4e\u0b63\u0c71");
			SurveyRoadMapDal surveyRoadMapDal = new SurveyRoadMapDal();
			if (IsFilter)
			{
				string.Format(_003F487_003F._003F488_003F("CŊɂ\u0348я՟؊܃ࠈ\u0941\u0a54\u0b4a\u0c49\u0d03\u0e71པၒᅩቻ፤ᑎᕴᙻ\u177dᡕ\u1976\u1a66\u1b35ᱣᵻṷὣ⁵ℯ≾⍬⑫╮♕❠⡬⤧⨻⬢Ɀⴳ\u2e7f⼦"), FilterPageId);
				string _003F10_003F = (string)/*Error near IL_0022: Stack underflow*/;
				lSurveyRoadMap = surveyRoadMapDal.GetListBySql(_003F10_003F);
			}
			else
			{
				lSurveyRoadMap = surveyRoadMapDal.GetList();
			}
			DataGrid1.ItemsSource = lSurveyRoadMap;
		}

		private void _003F223_003F()
		{
			//IL_0059: Incompatible stack heights: 0 vs 1
			TableName = _003F487_003F._003F488_003F("[Řɂ\u0360Ѣժ٬ݤ");
			S_DefineDal s_DefineDal = new S_DefineDal();
			if (IsFilter)
			{
				string.Format(_003F487_003F._003F488_003F("XŏɅ\u034dфՒ\u0605\u070eࠃ\u0944\u0a53\u0b4f\u0c72\u0d3e\u0e4eགྷ\u105fᅿቿ፱ᑹᕳᘵᝣ\u187b\u1977\u1a63᭵\u1c2fᵾṬὫ\u206e⅕≠⍬\u2427┻☢❿⠳⥿⨦"), FilterPageId);
				string _003F10_003F = (string)/*Error near IL_0022: Stack underflow*/;
				lS_Define = s_DefineDal.GetListBySql(_003F10_003F);
			}
			else
			{
				lS_Define = s_DefineDal.GetList();
			}
			DataGrid1.ItemsSource = lS_Define;
		}

		private void _003F224_003F()
		{
			TableName = _003F487_003F._003F488_003F("Cźɼͻѩղ\u0658ݨ\u0866\u0963੩୨\u0c46\u0d62\u0e71ཤ");
			SurveyRandomBaseDal surveyRandomBaseDal = new SurveyRandomBaseDal();
			lSurveyRandomBase = surveyRandomBaseDal.GetList();
			DataGrid1.ItemsSource = lSurveyRandomBase;
		}

		private void _003F225_003F()
		{
			TableName = _003F487_003F._003F488_003F("UŚɎͶѯձ");
			S_JUMPDal s_JUMPDal = new S_JUMPDal();
			lS_Jump = s_JUMPDal.GetList();
			DataGrid1.ItemsSource = lS_Jump;
		}

		private void _003F226_003F()
		{
			//IL_0059: Incompatible stack heights: 0 vs 1
			TableName = _003F487_003F._003F488_003F("Xſɻ;Ѣտىݫ\u0864५\u0a62");
			SurveyLogicDal surveyLogicDal = new SurveyLogicDal();
			if (IsFilter)
			{
				string.Format(_003F487_003F._003F488_003F("]ňɀ\u034eщ՝؈܍ࠆ\u0943\u0a56\u0b4c\u0c4f\u0d01\u0e73ཪ\u106cᅫቹ።ᑖᕶᙿ\u177eᡵ\u1935\u1a63᭻ᱷ\u1d63ṵἯ⁾Ⅼ≫⍮\u2455╠♬✧⠻⤢⩿⬳Ɀ\u2d26"), FilterPageId);
				string _003F10_003F = (string)/*Error near IL_0022: Stack underflow*/;
				lSurveyLogic = surveyLogicDal.GetListBySql(_003F10_003F);
			}
			else
			{
				lSurveyLogic = surveyLogicDal.GetList();
			}
			DataGrid1.ItemsSource = lSurveyLogic;
		}

		private void _003F227_003F()
		{
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվمݪ\u086a॥੫୦");
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			lSurveyConfig = surveyConfigDal.GetListRead();
			DataGrid1.ItemsSource = lSurveyConfig;
		}

		private void _003F228_003F()
		{
			TableName = _003F487_003F._003F488_003F("Yżɺͱѣռ\u0640ݪ\u0861ॵ");
			SurveyDictDal surveyDictDal = new SurveyDictDal();
			lSurveyDict = surveyDictDal.GetList();
			DataGrid1.ItemsSource = lSurveyDict;
		}

		private void _003F229_003F()
		{
			TableName = _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0650ݷ\u0866॰ੲ");
			SurveyUsersDal surveyUsersDal = new SurveyUsersDal();
			lSurveyUsers = surveyUsersDal.GetList();
			DataGrid1.ItemsSource = lSurveyUsers;
		}

		private void _003F230_003F()
		{
			//IL_0059: Incompatible stack heights: 0 vs 1
			TableName = _003F487_003F._003F488_003F("Yżɺͱѣռىݢ\u086b९");
			SurveyMainDal surveyMainDal = new SurveyMainDal();
			if (IsFilter)
			{
				string.Format(_003F487_003F._003F488_003F("\\ŋɁ\u0349ш՞؉܂ࠇ\u0940\u0a57\u0b4b\u0c4e\u0d02\u0e72ཕ\u106dᅨቸ፥ᑖᕻᙰ\u1776ᠷᥡ\u1a7d\u1b71ᱡᵷḱὃ⁚⅜≛⍉\u2452╕♀❌⠧⤻⨢⭿ⰳ\u2d7f⸦"), FilterSurveyId);
				string _003F10_003F = (string)/*Error near IL_0022: Stack underflow*/;
				lSurveyMain = surveyMainDal.GetListBySql(_003F10_003F);
			}
			else
			{
				lSurveyMain = surveyMainDal.GetList();
			}
			DataGrid1.ItemsSource = lSurveyMain;
		}

		private void _003F231_003F(int _003F406_003F = 1)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected I4, but got Unknown
			//IL_0098: Incompatible stack heights: 0 vs 4
			//IL_00b3: Incompatible stack heights: 0 vs 2
			//IL_00c3: Incompatible stack heights: 0 vs 1
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվهݫ\u0877ॴ੧୳");
			SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
			if (IsFilter)
			{
				string filterSurveyId = FilterSurveyId;
				int surveySequence = SurveyHelper.SurveySequence;
				_003F val = /*Error near IL_0023: Stack underflow*/- 1;
				List<SurveyAnswer> listBySequenceId = ((SurveyAnswerDal)/*Error near IL_0028: Stack underflow*/).GetListBySequenceId((string)/*Error near IL_0028: Stack underflow*/, (int)val);
				((DebugCheck)/*Error near IL_002d: Stack underflow*/).lSurveyAnswer = listBySequenceId;
			}
			else
			{
				string _003F10_003F = string.Format(_003F487_003F._003F488_003F("&ıȿ\u0337вԤٯݤ\u086dपਹଥత൨ด༳\u1037ᄲሦጻ᐀ᔮᙌᝉᡘ᥎\u1a1b\u1b4d᱑\u1d5dṅὓ―Ⅷ≦⍠⑧╵♶❱⡤⥨⨋⬗Ⰾⵓ⸗⽛。\u3104㉂㍌㑅㔀㙌㝻㡬㥩㩾㭴㱺㵽㹈㽿䁱䄴䈯䌲䐩䔠䘿䜾䠽䤬䩤䭸䱭䵭乵伦偧兽刣卫呥"), FilterSurveyId);
				lSurveyAnswer = surveyAnswerDal.GetListBySql(_003F10_003F);
			}
			switch (_003F406_003F)
			{
			case 1:
			{
				DataGrid dataGrid = DataGrid1;
				List<SurveyAnswer> lSurveyAnswer2 = lSurveyAnswer;
				((ItemsControl)/*Error near IL_0061: Stack underflow*/).ItemsSource = (IEnumerable)/*Error near IL_0061: Stack underflow*/;
				break;
			}
			case 2:
			{
				DataGrid dataGrid2 = DataGrid2;
				List<SurveyAnswer> itemsSource = lSurveyAnswer;
				((ItemsControl)/*Error near IL_0074: Stack underflow*/).ItemsSource = itemsSource;
				break;
			}
			case 3:
				DataGrid3.ItemsSource = lSurveyAnswer;
				break;
			}
		}

		private void _003F232_003F()
		{
			TableName = _003F487_003F._003F488_003F("]Ÿɾͽѯհ\u065bݢ\u0877॰\u0a61୭ౡ\u0d64");
			SurveySequenceDal surveySequenceDal = new SurveySequenceDal();
			lSurveySequence = surveySequenceDal.GetList();
			DataGrid1.ItemsSource = lSurveySequence;
		}

		private void _003F233_003F()
		{
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվ\u0654ݤ\u086a१੭୬");
			SurveyRandomDal surveyRandomDal = new SurveyRandomDal();
			lSurveyRandom = surveyRandomDal.GetList();
			DataGrid1.ItemsSource = lSurveyRandom;
		}

		private void _003F234_003F()
		{
			TableName = _003F487_003F._003F488_003F("\\Żɿ\u037aѮճوݦ\u0874\u0971\u0a60୶\u0c4b൫\u0e72");
			SurveyAnswerHisDal surveyAnswerHisDal = new SurveyAnswerHisDal();
			lSurveyAnswerHis = surveyAnswerHisDal.GetList();
			DataGrid1.ItemsSource = lSurveyAnswerHis;
		}

		private void _003F235_003F()
		{
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվمݪ\u086a॥੫୦");
			SurveyConfigDal surveyConfigDal = new SurveyConfigDal();
			lSurveyConfig = surveyConfigDal.GetList();
			DataGrid1.ItemsSource = lSurveyConfig;
		}

		private void _003F236_003F()
		{
			TableName = _003F487_003F._003F488_003F("ZŽɵͰѠս\u064fݭ\u0866");
			SurveyLogDal surveyLogDal = new SurveyLogDal();
			lSurveyLog = surveyLogDal.GetList();
			DataGrid1.ItemsSource = lSurveyLog;
		}

		private void _003F237_003F()
		{
			TableName = _003F487_003F._003F488_003F("_žɸͿѭվىݵ\u0870४੭୯");
			SurveyOptionDal surveyOptionDal = new SurveyOptionDal();
			lSurveyOption = surveyOptionDal.GetList();
			DataGrid1.ItemsSource = lSurveyOption;
		}

		private void _003F238_003F()
		{
			TableName = _003F487_003F._003F488_003F("WŜɏ\u0355");
			S_MTDal s_MTDal = new S_MTDal();
			lS_MT = s_MTDal.GetList();
			DataGrid1.ItemsSource = lS_MT;
		}

		private void _003F239_003F()
		{
			TableName = _003F487_003F._003F488_003F("Yżɺͱѣռ\u0657ݺ\u086c\u0962");
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			lSurveySync = surveySyncDal.GetList();
			DataGrid1.ItemsSource = lSurveySync;
		}

		private void _003F240_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			IsFilter = false;
			FilterSurveyId = SurveyHelper.SurveyID;
			FilterPageId = SurveyHelper.NavCurPage;
			_003F231_003F(3);
			txtQName.Focus();
		}

		private void _003F241_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			//IL_00ac: Incompatible stack heights: 0 vs 2
			//IL_00b7: Incompatible stack heights: 0 vs 1
			//IL_00bc: Incompatible stack heights: 1 vs 0
			//IL_00d2: Incompatible stack heights: 0 vs 2
			if (btnGetAnswer.Opacity == ButtonDisable)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			txtQName.Text = ((SurveyAnswer)DataGrid3.CurrentItem).QUESTION_NAME;
			if (txtLogic.Text == _003F487_003F._003F488_003F(""))
			{
				CheckBox autoAdd = AutoAdd;
				new bool?(true);
				((ToggleButton)/*Error near IL_005a: Stack underflow*/).IsChecked = (bool?)/*Error near IL_005a: Stack underflow*/;
			}
			bool? isChecked = AutoAdd.IsChecked;
			bool flag = true;
			bool hasValue;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue = isChecked.HasValue;
			}
			if (hasValue)
			{
				string text2 = txtLogic.Text;
				string text = string.Concat(str1: ((SurveyAnswer)DataGrid3.CurrentItem).QUESTION_NAME, str2: _003F487_003F._003F488_003F("!"), str0: (string)/*Error near IL_00f6: Stack underflow*/);
				((TextBox)/*Error near IL_00fb: Stack underflow*/).Text = text;
			}
			return;
			IL_0091:
			goto IL_0016;
		}

		private void _003F242_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string text = txtQName.Text.Trim();
			if (text == _003F487_003F._003F488_003F(""))
			{
				txtQAnswer.Text = _003F487_003F._003F488_003F(" 諰华貖啡鶛嗵ܨ");
				txtQName.Focus();
			}
			else
			{
				SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
				string text2 = _003F487_003F._003F488_003F("Z") + surveyAnswerDal.GetOneCode(SurveyHelper.SurveyID, text) + _003F487_003F._003F488_003F("\\");
				if (text2 == _003F487_003F._003F488_003F("YŜ"))
				{
					text2 = _003F487_003F._003F488_003F("-筪ȱ\u032eХ曜鞙\uf810邑嫿凾刡倊岝歌渝ဨ");
				}
				txtQAnswer.Text = txtQName.Text + _003F487_003F._003F488_003F("#Ŀȡ") + text2;
				txtQName.Focus();
				txtQName.SelectAll();
			}
		}

		private void _003F243_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			bool flag = oLogicEngine.boolResult(txtLogic.Text);
			txtLogicResult.Text = flag.ToString();
		}

		private void _003F244_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string text = oLogicEngine.strShowText(txtLogic.Text, true);
			if (text == _003F487_003F._003F488_003F("YŜ"))
			{
				text = _003F487_003F._003F488_003F("-筪ȱ\u032eХ曜鞙\uf810邑嫿凾刡倊岝歌渝ဨ");
			}
			txtLogicResult.Text = text;
		}

		private void _003F245_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string text = oLogicEngine.Route(txtLogic.Text);
			if (text == _003F487_003F._003F488_003F("YŜ"))
			{
				text = _003F487_003F._003F488_003F("-筪ȱ\u032eХ曜鞙\uf810邑嫿凾刡倊岝歌渝ဨ");
			}
			txtLogicResult.Text = text;
		}

		private void _003F246_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string text = oLogicEngine.stringResult(txtLogic.Text);
			if (text == _003F487_003F._003F488_003F("YŜ"))
			{
				text = _003F487_003F._003F488_003F("-筪ȱ\u032eХ曜鞙\uf810邑嫿凾刡倊岝歌渝ဨ");
			}
			txtLogicResult.Text = text;
		}

		private void _003F247_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if (_003F348_003F.Key == Key.Return)
			{
				_003F242_003F(_003F347_003F, _003F348_003F);
			}
		}

		private void _003F248_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtQName.SelectAll();
		}

		private void _003F249_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			IsFilter = false;
			FilterSurveyId = SurveyHelper.SurveyID;
			FilterPageId = SurveyHelper.NavCurPage;
			_003F231_003F(2);
			txtQuestionName.Focus();
		}

		private void _003F250_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			//IL_0037: Incompatible stack heights: 0 vs 1
			if (btnGetAnswer.Opacity == ButtonDisable)
			{
				txtNewAnswer.Focus();
				((DebugCheck)/*Error near IL_001b: Stack underflow*/).txtNewAnswer.SelectAll();
			}
			else
			{
				txtQuestionName.Text = ((SurveyAnswer)DataGrid2.CurrentItem).QUESTION_NAME;
				_003F251_003F(null, null);
			}
		}

		private void _003F251_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00ad: Incompatible stack heights: 0 vs 4
			//IL_00c2: Incompatible stack heights: 0 vs 2
			if (btnGetAnswer.Opacity == ButtonDisable)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			string text = txtQuestionName.Text;
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_003c: Stack underflow*/, (string)/*Error near IL_003c: Stack underflow*/, (MessageBoxButton)/*Error near IL_003c: Stack underflow*/, (MessageBoxImage)/*Error near IL_003c: Stack underflow*/);
				txtQuestionName.Focus();
			}
			else
			{
				string oneCode = new SurveyAnswerDal().GetOneCode(SurveyHelper.SurveyID, text);
				txtAnswer.Text = oneCode;
				if (txtAnswer.Text == _003F487_003F._003F488_003F(""))
				{
					TextBox txtAnswer2 = txtAnswer;
					string text2 = _003F487_003F._003F488_003F((string)/*Error near IL_00c7: Stack underflow*/);
					((TextBox)/*Error near IL_00cc: Stack underflow*/).Text = text2;
				}
				txtNewAnswer.Text = oneCode;
				txtQuestionName.IsEnabled = false;
				txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
				txtNewAnswer.IsEnabled = true;
				txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
				btnGetAnswer.Opacity = ButtonDisable;
				btnSave.Opacity = ButtonEnable;
				btnCancel.Opacity = ButtonEnable;
				txtNewAnswer.Focus();
				txtNewAnswer.SelectAll();
			}
			return;
			IL_0091:
			goto IL_0016;
		}

		private void _003F252_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if (_003F348_003F.Key == Key.Return)
			{
				_003F251_003F(_003F347_003F, _003F348_003F);
			}
		}

		private void _003F253_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyTaptip.ShowInputPanel();
		}

		private void _003F254_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if (_003F348_003F.Key == Key.Return)
			{
				_003F211_003F(_003F347_003F, _003F348_003F);
			}
		}

		private void _003F211_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_009a: Incompatible stack heights: 0 vs 2
			if (btnSave.Opacity == ButtonDisable)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			string text = txtAnswer.Text;
			string text2 = txtNewAnswer.Text;
			string text3;
			if (text != text2)
			{
				text3 = txtQuestionName.Text;
				if (text2 == _003F487_003F._003F488_003F(""))
				{
					_003F487_003F._003F488_003F("昿唩悄祷牱繟湂屑洶嬷藟䔯応鳌䀯\uf01e");
					string msgCaption = SurveyMsg.MsgCaption;
					if (MessageBox.Show((string)/*Error near IL_0055: Stack underflow*/, (string)/*Error near IL_0055: Stack underflow*/, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
					{
						return;
					}
				}
				goto IL_00a5;
			}
			goto IL_00c9;
			IL_0066:
			goto IL_0016;
			IL_00a0:
			goto IL_00a5;
			IL_00a5:
			new SurveyAnswerDal().AddOne(SurveyHelper.SurveyID, text3, text2, 8888);
			_003F231_003F(2);
			_003F231_003F(3);
			goto IL_00c9;
			IL_00c9:
			txtQuestionName.IsEnabled = true;
			txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			txtNewAnswer.IsEnabled = false;
			txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			btnGetAnswer.Opacity = ButtonEnable;
			btnSave.Opacity = ButtonDisable;
			btnCancel.Opacity = ButtonDisable;
			txtQuestionName.Focus();
			txtQuestionName.SelectAll();
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtQuestionName.IsEnabled = true;
			txtQuestionName.Background = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			txtNewAnswer.IsEnabled = false;
			txtNewAnswer.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
			btnGetAnswer.Opacity = ButtonEnable;
			btnSave.Opacity = ButtonDisable;
			btnCancel.Opacity = ButtonDisable;
			_003F231_003F(2);
			txtQuestionName.Focus();
			txtQuestionName.SelectAll();
		}

		public void Refresh()
		{
			//IL_0026: Incompatible stack heights: 0 vs 3
			//IL_003d: Incompatible stack heights: 0 vs 2
			DispatcherFrame dispatcherFrame = new DispatcherFrame();
			Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
			if (_003F7_003F._003C_003E9__71_0 == null)
			{
				new DispatcherOperationCallback(_003F7_003F._003C_003E9._003F333_003F);
				_003F7_003F._003C_003E9__71_0 = (DispatcherOperationCallback)/*Error near IL_001c: Stack underflow*/;
			}
			((Dispatcher)/*Error near IL_0043: Stack underflow*/).BeginInvoke((DispatcherPriority)/*Error near IL_0043: Stack underflow*/, (Delegate)/*Error near IL_0043: Stack underflow*/, (object)dispatcherFrame);
			Dispatcher.PushFrame(dispatcherFrame);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕤᙱ\u1777\u187bᥥᨿ\u1b6bᱫᵯṹὬ\u2069Ⅱ≭⍤⑭┫♼❢⡯⥭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0017:
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
				((DebugCheck)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtHelper = (TextBox)_003F350_003F;
				break;
			case 3:
				((TabItem)_003F350_003F).Initialized += _003F219_003F;
				break;
			case 4:
				Chk_Filter = (CheckBox)_003F350_003F;
				break;
			case 5:
				Chk_SurveyDefine = (RadioButton)_003F350_003F;
				break;
			case 6:
				Chk_SurveyDetail = (RadioButton)_003F350_003F;
				break;
			case 7:
				Chk_SurveyRoadmap = (RadioButton)_003F350_003F;
				break;
			case 8:
				Chk_SurveyLogic = (RadioButton)_003F350_003F;
				break;
			case 9:
				Chk_S_Define = (RadioButton)_003F350_003F;
				break;
			case 10:
				Chk_SurveyRandomBase = (RadioButton)_003F350_003F;
				break;
			case 11:
				Chk_SurveyConfig = (RadioButton)_003F350_003F;
				break;
			case 12:
				ChkW_SurveyMain = (RadioButton)_003F350_003F;
				break;
			case 13:
				ChkW_SurveyAnswer = (RadioButton)_003F350_003F;
				break;
			case 14:
				ChkW_SurveySequence = (RadioButton)_003F350_003F;
				break;
			case 15:
				ChkW_SurveyRandom = (RadioButton)_003F350_003F;
				break;
			case 16:
				ChkW_SurveyAnswerHis = (RadioButton)_003F350_003F;
				break;
			case 17:
				txtTable = (TextBlock)_003F350_003F;
				break;
			case 18:
				btnQuery = (Button)_003F350_003F;
				btnQuery.Click += _003F201_003F;
				break;
			case 19:
				DataGrid1 = (DataGrid)_003F350_003F;
				break;
			case 20:
				TabItem3 = (TabItem)_003F350_003F;
				TabItem3.Initialized += _003F240_003F;
				break;
			case 21:
				DataGrid3 = (DataGrid)_003F350_003F;
				DataGrid3.MouseDoubleClick += _003F241_003F;
				break;
			case 22:
				txtQName = (TextBox)_003F350_003F;
				txtQName.KeyDown += _003F247_003F;
				txtQName.GotFocus += _003F248_003F;
				break;
			case 23:
				btnRunAnswer = (Button)_003F350_003F;
				btnRunAnswer.Click += _003F242_003F;
				break;
			case 24:
				btnKeyboard = (Button)_003F350_003F;
				btnKeyboard.Click += _003F253_003F;
				break;
			case 25:
				txtQAnswer = (TextBox)_003F350_003F;
				break;
			case 26:
				AutoAdd = (CheckBox)_003F350_003F;
				break;
			case 27:
				txtLogic = (TextBox)_003F350_003F;
				break;
			case 28:
				btnRunText = (Button)_003F350_003F;
				btnRunText.Click += _003F244_003F;
				break;
			case 29:
				btnRunLogic = (Button)_003F350_003F;
				btnRunLogic.Click += _003F243_003F;
				break;
			case 30:
				btnRunRoute = (Button)_003F350_003F;
				btnRunRoute.Click += _003F245_003F;
				break;
			case 31:
				btnRunOption = (Button)_003F350_003F;
				btnRunOption.Click += _003F246_003F;
				break;
			case 32:
				txtLogicResult = (TextBox)_003F350_003F;
				break;
			case 33:
				TabItem4 = (TabItem)_003F350_003F;
				TabItem4.Initialized += _003F249_003F;
				break;
			case 34:
				DataGrid2 = (DataGrid)_003F350_003F;
				DataGrid2.MouseDoubleClick += _003F250_003F;
				break;
			case 35:
				btnKeyboard1 = (Button)_003F350_003F;
				btnKeyboard1.Click += _003F253_003F;
				break;
			case 36:
				txtQuestionName = (TextBox)_003F350_003F;
				txtQuestionName.KeyDown += _003F252_003F;
				break;
			case 37:
				btnGetAnswer = (Button)_003F350_003F;
				btnGetAnswer.Click += _003F251_003F;
				break;
			case 38:
				txtAnswerTitle = (TextBlock)_003F350_003F;
				break;
			case 39:
				txtAnswer = (TextBox)_003F350_003F;
				break;
			case 40:
				txtNewAnswerTitle = (TextBlock)_003F350_003F;
				break;
			case 41:
				txtNewAnswer = (TextBox)_003F350_003F;
				txtNewAnswer.KeyDown += _003F254_003F;
				break;
			case 42:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			case 43:
				btnCancel = (Button)_003F350_003F;
				btnCancel.Click += _003F128_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
