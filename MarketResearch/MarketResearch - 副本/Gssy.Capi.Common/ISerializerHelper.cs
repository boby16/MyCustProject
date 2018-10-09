using System;

namespace Gssy.Capi.Common
{
	public interface ISerializerHelper
	{
		object SerializeObject { get; set; }

		string FilePath { get; set; }

		string FileName { get; set; }

		void Serialize<T>();

		T Deserialize<T>();
	}
}
