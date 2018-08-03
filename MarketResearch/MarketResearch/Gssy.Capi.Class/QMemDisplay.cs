using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Class
{
	public class QMemDisplay : QDisplay
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

		public void Init(string _003F397_003F, string _003F441_003F, int _003F442_003F, List<SurveyDefine> _003F443_003F)
		{
			_003F20_003F _003F20_003F = new _003F20_003F();
			_003F20_003F.PageId = _003F441_003F;
			_003F20_003F.CombineIndex = _003F442_003F;
			List<SurveyDefine> list = _003F443_003F.Where(_003F20_003F._003F339_003F).ToList();
			base.QDefine = list[0];
		}
	}
}
