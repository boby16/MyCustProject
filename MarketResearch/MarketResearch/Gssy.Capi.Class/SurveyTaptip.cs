using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Gssy.Capi.Class
{
	public static class SurveyTaptip
	{
		[CompilerGenerated]
		private static class _003F21_003F
		{
			public static CallSite<Func<CallSite, Type, object, object>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

			public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

			public static CallSite<Action<CallSite, Type, object>> _003C_003Ep__3;
		}

		[CompilerGenerated]
		private static class _003F22_003F
		{
			public static CallSite<Func<CallSite, Type, object, object>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

			public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

			public static CallSite<Action<CallSite, Type, object>> _003C_003Ep__3;
		}

		private const int WM_SYSCOMMAND = 274;

		private const uint SC_CLOSE = 61536u;

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
		private static extern bool _003F294_003F(IntPtr _003F445_003F, int _003F446_003F, int _003F447_003F, int _003F448_003F);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
		private static extern bool _003F294_003F(IntPtr _003F445_003F, int _003F446_003F, uint _003F447_003F, uint _003F448_003F);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
		private static extern bool _003F294_003F(IntPtr _003F445_003F, uint _003F446_003F, IntPtr _003F447_003F, IntPtr _003F448_003F);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "FindWindow", SetLastError = true)]
		private static extern IntPtr _003F295_003F(string _003F449_003F, string _003F450_003F);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RegisterWindowMessage", SetLastError = true)]
		private static extern int _003F296_003F(string _003F451_003F);

		public static int ShowInputPanel()
		{
			//IL_006e: Incompatible stack heights: 0 vs 2
			//IL_00bd: Incompatible stack heights: 0 vs 2
			try
			{
				object arg = _003F487_003F._003F488_003F("~Ćɧ\u036aы\u0557\u0650\u0744ࡔख़ਓ୴ౘ\u0d5c\u0e4aཝ\u1071ᅯቄፇᑄᕇᙉᜆᡣ᥍ᩏᭇ᱒ᵼṲί⁾Ⅾ≴⍩⑶╾♣✶⡦⥼⩲⭠ⱴ\u2d74\u2e53⽧っㅧ㉗㍞㑨㕪㙓㝯㡵㤪㩦㭺㱤");
				if (_003F21_003F._003C_003Ep__2 == null)
				{
					_003F21_003F._003C_003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SurveyTaptip), new CSharpArgumentInfo[1]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = _003F21_003F._003C_003Ep__2.Target;
				CallSite<Func<CallSite, object, bool>> _003C_003Ep__ = _003F21_003F._003C_003Ep__2;
				if (_003F21_003F._003C_003Ep__1 == null)
				{
					_003F21_003F._003C_003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(SurveyTaptip), new CSharpArgumentInfo[1]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object> target2 = _003F21_003F._003C_003Ep__1.Target;
				CallSite<Func<CallSite, object, object>> _003C_003Ep__2 = _003F21_003F._003C_003Ep__1;
				if (_003F21_003F._003C_003Ep__0 == null)
				{
					_003F21_003F._003C_003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, _003F487_003F._003F488_003F("CŽɭͰѶղ"), null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				object arg2 = _003F21_003F._003C_003Ep__0.Target(_003F21_003F._003C_003Ep__0, typeof(File), arg);
				object arg3 = /*Error near IL_0121: Stack underflow*/((CallSite)/*Error near IL_0121: Stack underflow*/, arg2);
				if (!/*Error near IL_0126: Stack underflow*/((CallSite)/*Error near IL_0126: Stack underflow*/, arg3))
				{
					if (_003F21_003F._003C_003Ep__3 == null)
					{
						_003F21_003F._003C_003Ep__3 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, _003F487_003F._003F488_003F("VŰɢͰѵ"), null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					_003F21_003F._003C_003Ep__3.Target(_003F21_003F._003C_003Ep__3, typeof(Process), arg);
					return 2;
				}
				return -1;
			}
			catch (Exception)
			{
				return 255;
			}
		}

		public static void HideInputPanel()
		{
			IntPtr intPtr = new IntPtr(0);
			intPtr = _003F295_003F(_003F487_003F._003F488_003F("Xŀɛ\u0367ѽՓنݫ\u0860०\u0a58\u0b51౬൪\u0e67\u0f6dၶ"), null);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			goto IL_0029;
			IL_0029:
			_003F294_003F(intPtr, 274, 61536u, 0u);
			return;
			IL_003a:
			goto IL_0029;
		}

		public static int SwitchInputPanel()
		{
			//IL_00a1: Incompatible stack heights: 0 vs 2
			//IL_00f0: Incompatible stack heights: 0 vs 2
			IntPtr intPtr = new IntPtr(0);
			intPtr = _003F295_003F(_003F487_003F._003F488_003F("Xŀɛ\u0367ѽՓنݫ\u0860०\u0a58\u0b51౬൪\u0e67\u0f6dၶ"), null);
			if (intPtr == IntPtr.Zero)
			{
				try
				{
					object arg = _003F487_003F._003F488_003F("~Ćɧ\u036aы\u0557\u0650\u0744ࡔख़ਓ୴ౘ\u0d5c\u0e4aཝ\u1071ᅯቄፇᑄᕇᙉᜆᡣ᥍ᩏᭇ᱒ᵼṲί⁾Ⅾ≴⍩⑶╾♣✶⡦⥼⩲⭠ⱴ\u2d74\u2e53⽧っㅧ㉗㍞㑨㕪㙓㝯㡵㤪㩦㭺㱤");
					if (_003F22_003F._003C_003Ep__2 == null)
					{
						_003F22_003F._003C_003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SurveyTaptip), new CSharpArgumentInfo[1]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> target = _003F22_003F._003C_003Ep__2.Target;
					CallSite<Func<CallSite, object, bool>> _003C_003Ep__ = _003F22_003F._003C_003Ep__2;
					if (_003F22_003F._003C_003Ep__1 == null)
					{
						_003F22_003F._003C_003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(SurveyTaptip), new CSharpArgumentInfo[1]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> target2 = _003F22_003F._003C_003Ep__1.Target;
					CallSite<Func<CallSite, object, object>> _003C_003Ep__2 = _003F22_003F._003C_003Ep__1;
					if (_003F22_003F._003C_003Ep__0 == null)
					{
						_003F22_003F._003C_003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, _003F487_003F._003F488_003F("CŽɭͰѶղ"), null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					object arg2 = _003F22_003F._003C_003Ep__0.Target(_003F22_003F._003C_003Ep__0, typeof(File), arg);
					object arg3 = /*Error near IL_0154: Stack underflow*/((CallSite)/*Error near IL_0154: Stack underflow*/, arg2);
					if (!/*Error near IL_0159: Stack underflow*/((CallSite)/*Error near IL_0159: Stack underflow*/, arg3))
					{
						if (_003F22_003F._003C_003Ep__3 == null)
						{
							_003F22_003F._003C_003Ep__3 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, _003F487_003F._003F488_003F("VŰɢͰѵ"), null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						_003F22_003F._003C_003Ep__3.Target(_003F22_003F._003C_003Ep__3, typeof(Process), arg);
						return 1;
					}
					return -1;
				}
				catch (Exception)
				{
					return 2;
				}
			}
			_003F294_003F(intPtr, 274, 61536u, 0u);
			return 0;
		}
	}
}
