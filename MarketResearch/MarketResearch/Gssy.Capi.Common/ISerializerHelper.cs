using System;

namespace Gssy.Capi.Common
{
	// Token: 0x0200000D RID: 13
	public interface ISerializerHelper
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000071 RID: 113
		// (set) Token: 0x06000072 RID: 114
		object SerializeObject { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		string FilePath { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		string FileName { get; set; }

		// Token: 0x06000077 RID: 119
		void Serialize<T>();

		// Token: 0x06000078 RID: 120
		T Deserialize<T>();
	}
}
