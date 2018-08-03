using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[ComImport]
	[CompilerGenerated]
	[DefaultMember("_Default")]
	[Guid("000208D5-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	public interface _Application
	{
		[DispId(572)]
		Workbooks Workbooks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[DispId(572)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		[DispId(0)]
		string _Default
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[DispId(0)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}

		void _VtblGap1_45();

		void _VtblGap2_60();
	}
}
