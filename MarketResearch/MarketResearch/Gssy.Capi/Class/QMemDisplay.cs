using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	// Token: 0x02000068 RID: 104
	public class QMemDisplay : QDisplay
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0009F564 File Offset: 0x0009D764
		public void Init(string string_0, string string_1, int int_0, List<SurveyDefine> list_0)
		{
			QMemDisplay.Class69 @class = new QMemDisplay.Class69();
			@class.PageId = string_1;
			@class.CombineIndex = int_0;
			List<SurveyDefine> list = list_0.Where(new Func<SurveyDefine, bool>(@class.method_0)).ToList<SurveyDefine>();
			base.QDefine = list[0];
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class Class69
		{
			// Token: 0x060007B7 RID: 1975 RVA: 0x00004783 File Offset: 0x00002983
			internal bool method_0(SurveyDefine surveyDefine_0)
			{
				return surveyDefine_0.PAGE_ID == this.PageId && surveyDefine_0.COMBINE_INDEX == this.CombineIndex;
			}

			// Token: 0x04000D55 RID: 3413
			public string PageId;

			// Token: 0x04000D56 RID: 3414
			public int CombineIndex;
		}
	}
}
