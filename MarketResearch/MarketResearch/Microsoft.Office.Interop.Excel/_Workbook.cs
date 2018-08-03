using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[ComImport]
	[CompilerGenerated]
	[Guid("000208DA-0000-0000-C000-000000000046")]
	[TypeIdentifier]
	public interface _Workbook
	{
		[DispId(298)]
		bool Saved
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[LCIDConversion(0)]
			[DispId(298)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[DispId(298)]
			[LCIDConversion(0)]
			[param: In]
			set;
		}

		[DispId(494)]
		Sheets Worksheets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[DispId(494)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		void _VtblGap1_20();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[LCIDConversion(3)]
		[DispId(277)]
		void Close([In] [MarshalAs(UnmanagedType.Struct)] object _003F475_003F = null, [In] [MarshalAs(UnmanagedType.Struct)] object _003F476_003F = null, [In] [MarshalAs(UnmanagedType.Struct)] object _003F477_003F = null);

		void _VtblGap2_76();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[LCIDConversion(1)]
		[DispId(175)]
		void SaveCopyAs([In] [MarshalAs(UnmanagedType.Struct)] object _003F476_003F = null);

		void _VtblGap3_24();
	}
}
