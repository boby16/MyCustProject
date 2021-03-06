﻿using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Gssy.Capi.Class
{
    public static class SurveyTaptip
    {
        [CompilerGenerated]
        private static class Class73
        {
            public static CallSite<Func<CallSite, Type, object, object>> _003C_003Ep__0;

            public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

            public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

            public static CallSite<Action<CallSite, Type, object>> _003C_003Ep__3;
        }

        [CompilerGenerated]
        private static class Class74
        {
            public static CallSite<Func<CallSite, Type, object, object>> _003C_003Ep__0;

            public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

            public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

            public static CallSite<Action<CallSite, Type, object>> _003C_003Ep__3;
        }

        private const int WM_SYSCOMMAND = 274;

        private const uint SC_CLOSE = 61536u;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool PostMessage(IntPtr intptr_0, int int_0, int int_1, int int_2);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
        private static extern bool PostMessage_1(IntPtr intptr_0, int int_0, uint uint_0, uint uint_1);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PostMessage", SetLastError = true)]
        private static extern bool PostMessage_2(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindow(string string_0, string string_1);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int RegisterWindowMessage(string string_0);

        public static int ShowInputPanel()
        {
            try
            {
                object arg = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (Class73._003C_003Ep__0 == null)
                {
                    Class73._003C_003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Exists", null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
                    {
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                    }));
                }
                if (!((!(dynamic)Class73._003C_003Ep__0.Target(Class73._003C_003Ep__0, typeof(File), arg)) ? true : false))
                {
                    if (Class73._003C_003Ep__3 == null)
                    {
                        Class73._003C_003Ep__3 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Start", null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                        }));
                    }
                    Class73._003C_003Ep__3.Target(Class73._003C_003Ep__3, typeof(Process), arg);
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
            intPtr = FindWindow("IPTip_Main_Window", null);
            if (!(intPtr == IntPtr.Zero))
            {
                PostMessage_1(intPtr, 274, 61536u, 0u);
            }
        }

        public static int SwitchInputPanel()
        {
            IntPtr intPtr = new IntPtr(0);
            intPtr = FindWindow("IPTip_Main_Window", null);
            if (intPtr == IntPtr.Zero)
            {
                try
                {
                    object arg = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                    if (Class74._003C_003Ep__0 == null)
                    {
                        Class74._003C_003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Exists", null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                        }));
                    }
                    if (!((!(dynamic)Class74._003C_003Ep__0.Target(Class74._003C_003Ep__0, typeof(File), arg)) ? true : false))
                    {
                        if (Class74._003C_003Ep__3 == null)
                        {
                            Class74._003C_003Ep__3 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Start", null, typeof(SurveyTaptip), new CSharpArgumentInfo[2]
                            {
                                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                            }));
                        }
                        Class74._003C_003Ep__3.Target(Class74._003C_003Ep__3, typeof(Process), arg);
                        return 1;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return 2;
                }
            }
            PostMessage_1(intPtr, 274, 61536u, 0u);
            return 0;
        }
    }
}
