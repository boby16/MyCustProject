using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	// Token: 0x02000069 RID: 105
	public class QMemFill : QFill
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x0009F5AC File Offset: 0x0009D7AC
		public void Init(string string_0, string string_1, int int_0, List<SurveyDefine> list_0)
		{
			QMemFill.Class70 @class = new QMemFill.Class70();
			@class.PageId = string_1;
			@class.CombineIndex = int_0;
			List<SurveyDefine> list = list_0.Where(new Func<SurveyDefine, bool>(@class.method_0)).ToList<SurveyDefine>();
			base.QDefine = list[0];
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		private sealed class Class70
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x000047A8 File Offset: 0x000029A8
			internal bool method_0(SurveyDefine surveyDefine_0)
			{
				return surveyDefine_0.PAGE_ID == this.PageId && surveyDefine_0.COMBINE_INDEX == this.CombineIndex;
			}

			// Token: 0x04000D57 RID: 3415
			public string PageId;

			// Token: 0x04000D58 RID: 3416
			public int CombineIndex;
		}
	}
}
