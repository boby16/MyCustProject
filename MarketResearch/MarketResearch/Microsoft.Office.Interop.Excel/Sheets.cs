using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[ComImport]
	[CompilerGenerated]
	[Guid("000208D7-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	public interface Sheets : IEnumerable
	{
		[IndexerName("_Default")]
		[DispId(0)]
		object this[[In] [MarshalAs(UnmanagedType.Struct)] object _003F473_003F]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[DispId(0)]
			[return: MarshalAs(UnmanagedType.IDispatch)]
			get;
		}

		void _VtblGap1_18();
	}
}
