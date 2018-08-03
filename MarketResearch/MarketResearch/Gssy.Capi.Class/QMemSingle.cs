using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Class
{
	public class QMemSingle : QSingle
	{
		[CompilerGenerated]
		private sealed class _003F20_003F
		{
			public string PageId;

			public int CombineIndex;

			internal bool _003F339_003F(SurveyDefine _003F483_003F)
			{
				//IL_002c: Incompatible stack heights: 0 vs 1
				//IL_0033: Invalid comparison between Unknown and I4
				if (_003F483_003F.PAGE_ID == PageId)
				{
					int cOMBINE_INDEX = _003F483_003F.COMBINE_INDEX;
					int combineIndex = CombineIndex;
					return (int)/*Error near IL_0033: Stack underflow*/ == combineIndex;
				}
				return false;
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Func<SurveyDetail, int> _003C_003E9__1_1;

			public static Func<SurveyDetail, int> _003C_003E9__1_3;

			internal int _003F340_003F(SurveyDetail _003F390_003F)
			{
				return _003F390_003F.INNER_ORDER;
			}

			internal int _003F341_003F(SurveyDetail _003F390_003F)
			{
				return _003F390_003F.INNER_ORDER;
			}
		}

		public void Init(string _003F441_003F, int _003F442_003F, List<SurveyDefine> _003F443_003F, List<SurveyDetail> _003F444_003F)
		{
			_003F20_003F _003F20_003F = new _003F20_003F();
			_003F20_003F.PageId = _003F441_003F;
			_003F20_003F.CombineIndex = _003F442_003F;
			base.QInitDateTime = DateTime.Now;
			List<SurveyDefine> list = _003F443_003F.Where(_003F20_003F._003F339_003F).ToList();
			base.QDefine = list[0];
			base.QuestionName = base.QDefine.QUESTION_NAME;
		}

		public unsafe void InitDetailID(string _003F441_003F, int _003F442_003F, List<SurveyDefine> _003F443_003F, List<SurveyDetail> _003F444_003F)
		{
			//IL_0048: Expected O, but got Unknown
			//IL_009f: Incompatible stack heights: 0 vs 1
			//IL_00ae: Incompatible stack heights: 0 vs 2
			//IL_00c4: Incompatible stack heights: 0 vs 1
			//IL_00d0: Incompatible stack heights: 0 vs 2
			//IL_00e8: Incompatible stack heights: 0 vs 2
			//IL_00fe: Incompatible stack heights: 0 vs 3
			//IL_0109: Incompatible stack heights: 2 vs 1
			if (base.QDefine.PARENT_CODE == _003F487_003F._003F488_003F("") || (int)/*Error near IL_00a4: Stack underflow*/ == 0)
			{
				_003F444_003F.Where(_003F292_003F);
				if (_003F7_003F._003C_003E9__1_1 == null)
				{
					new Func<SurveyDetail, int>(_003F7_003F._003C_003E9._003F340_003F);
					_003F7_003F._003C_003E9__1_1 = (Func<SurveyDetail, int>)/*Error near IL_0042: Stack underflow*/;
				}
				IOrderedEnumerable<SurveyDetail> source = ((IEnumerable<SurveyDetail>)/*Error near IL_004d: Stack underflow*/).OrderBy((Func<SurveyDetail, int>)/*Error near IL_004d: Stack underflow*/);
				base.QDetails = source.ToList();
			}
			else if (!(base.QDefine.PARENT_CODE == _003F487_003F._003F488_003F("Cşɋ\u0345юՋق")) && /*Error near IL_00d5: Stack underflow*/> /*Error near IL_00d5: Stack underflow*/)
			{
				new Func<SurveyDetail, bool>(_003F293_003F);
				IEnumerable<SurveyDetail> source2 = ((IEnumerable<SurveyDetail>)/*Error near IL_0084: Stack underflow*/).Where((Func<SurveyDetail, bool>)/*Error near IL_0084: Stack underflow*/);
				Func<SurveyDetail, int> _003C_003E9__1_ = _003F7_003F._003C_003E9__1_3;
				if (_003C_003E9__1_ == null)
				{
					_003F7_003F _003C_003E = _003F7_003F._003C_003E9;
					_003F7_003F._003C_003E9__1_3 = new Func<SurveyDetail, int>((object)/*Error near IL_0103: Stack underflow*/, (IntPtr)(void*)/*Error near IL_0103: Stack underflow*/);
				}
				IOrderedEnumerable<SurveyDetail> source3 = source2.OrderBy(_003C_003E9__1_);
				base.QDetails = source3.ToList();
			}
		}

		[CompilerGenerated]
		private bool _003F292_003F(SurveyDetail _003F390_003F)
		{
			return _003F390_003F.DETAIL_ID == base.QDefine.DETAIL_ID;
		}

		[CompilerGenerated]
		private bool _003F293_003F(SurveyDetail _003F390_003F)
		{
			//IL_0031: Incompatible stack heights: 0 vs 1
			if (_003F390_003F.DETAIL_ID == base.QDefine.DETAIL_ID)
			{
				string pARENT_CODE2 = _003F390_003F.PARENT_CODE;
				string pARENT_CODE = base.QDefine.PARENT_CODE;
				return (string)/*Error near IL_0040: Stack underflow*/ == pARENT_CODE;
			}
			return false;
		}
	}
}
