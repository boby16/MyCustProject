using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[ComImport]
	[CompilerGenerated]
	[Guid("000208DB-0000-0000-C000-000000000046")]
	[DefaultMember("_Default")]
	[TypeIdentifier]
	public interface Workbooks : IEnumerable
	{
		void _VtblGap1_3();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(181)]
		[LCIDConversion(1)]
		[return: MarshalAs(UnmanagedType.Interface)]
		Workbook Add([In] [MarshalAs(UnmanagedType.Struct)] object _003F474_003F = null);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(277)]
		[LCIDConversion(0)]
		void Close();
	}
}
