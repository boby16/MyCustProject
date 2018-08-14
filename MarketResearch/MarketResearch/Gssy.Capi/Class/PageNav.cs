using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Gssy.Capi.Class
{
    public class PageNav : Page
    {
        [Serializable]
        [CompilerGenerated]
        private sealed class Class68
        {
            public static readonly Class68 _003C_003E9 = new Class68();

            public static DispatcherOperationCallback _003C_003E9__7_0;

            internal object method_0(object object_0)
            {
                ((DispatcherFrame)object_0).Continue = false;
                return null;
            }
        }

        public LogicEngine oLogicEngine = new LogicEngine();

        private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

        private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

        public bool NextPage(NavBase navBase_0, NavigationService navigationService_0)
        {
            int surveySequence = SurveyHelper.SurveySequence;
            string roadMapVersion = SurveyHelper.RoadMapVersion;
            string surveyID = SurveyHelper.SurveyID;
            string navCurPage = SurveyHelper.NavCurPage;
            navBase_0.PageStartTime = SurveyHelper.PageStartTime;
            navBase_0.RecordFileName = SurveyHelper.RecordFileName;
            navBase_0.RecordStartTime = SurveyHelper.RecordStartTime;
            SurveyRoadMap byPageId = oSurveyRoadMapDal.GetByPageId(navCurPage, roadMapVersion);
            string text = GClass0.smethod_0("\tŪɁ\u0355хՉفݤࡗ\u0948\u0a44\u0b7aఽ\u0d5e\u0e75ཀྵၹᅵችፄᑢᕴᙦᝧᠱᥔ\u1a7d\u1b7f\u1c7aᵴṆ\u1f7e\u2067ⅹ∫⍕④╦♫❧⡧⤢");
            string value = GClass0.smethod_0("\"") + byPageId.FORM_NAME.ToUpper() + GClass0.smethod_0("\"");
            bool flag = text.ToUpper().Contains(value);
            if (SurveyHelper.AutoCapture && !flag)
            {
                string str = SurveyHelper.SurveyID + GClass0.smethod_0("^") + SurveyHelper.NavCurPage + ((SurveyHelper.CircleACode == GClass0.smethod_0("")) ? GClass0.smethod_0("") : (GClass0.smethod_0("]ŀ") + SurveyHelper.CircleACode)) + ((SurveyHelper.CircleBCode == GClass0.smethod_0("")) ? GClass0.smethod_0("") : (GClass0.smethod_0("]Ń") + SurveyHelper.CircleBCode)) + GClass0.smethod_0("*ũɲ\u0366");
                str = Directory.GetCurrentDirectory() + GClass0.smethod_0("[Ŗɭ\u036bѷխ\u065d") + str;
                if (File.Exists(str))
                {
                    str = SurveyHelper.SurveyID + GClass0.smethod_0("^") + SurveyHelper.NavCurPage + ((SurveyHelper.CircleACode == GClass0.smethod_0("")) ? GClass0.smethod_0("") : (GClass0.smethod_0("]ŀ") + SurveyHelper.CircleACode)) + ((SurveyHelper.CircleBCode == GClass0.smethod_0("")) ? GClass0.smethod_0("") : (GClass0.smethod_0("]Ń") + SurveyHelper.CircleBCode)) + GClass0.smethod_0("^") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + GClass0.smethod_0("*ũɲ\u0366");
                    str = Directory.GetCurrentDirectory() + GClass0.smethod_0("[Ŗɭ\u036bѷխ\u065d") + str;
                }
                method_0(str, (int)SurveyHelper.Screen_LeftTop);
            }
            try
            {
                if (navBase_0.GroupLevel == GClass0.smethod_0(""))
                {
                    navBase_0.NextPage(surveyID, surveySequence, navCurPage, roadMapVersion);
                }
                else
                {
                    navBase_0.NextCirclePage(surveyID, surveySequence, navCurPage, roadMapVersion);
                    SurveyHelper.CircleACount = navBase_0.CircleACount;
                    SurveyHelper.CircleACurrent = navBase_0.CircleACurrent;
                    if (navBase_0.IsLastA && (navBase_0.GroupPageType == 0 || navBase_0.GroupPageType == 2))
                    {
                        SurveyHelper.CircleACode = GClass0.smethod_0("");
                        SurveyHelper.CircleACodeText = GClass0.smethod_0("");
                    }
                    if (navBase_0.GroupLevel == GClass0.smethod_0("C"))
                    {
                        SurveyHelper.CircleBCount = navBase_0.CircleBCount;
                        SurveyHelper.CircleBCurrent = navBase_0.CircleBCurrent;
                        if (navBase_0.IsLastB && (navBase_0.GroupPageType == 10 || navBase_0.GroupPageType == 12 || navBase_0.GroupPageType == 30 || navBase_0.GroupPageType == 32))
                        {
                            SurveyHelper.CircleBCode = GClass0.smethod_0("");
                            SurveyHelper.CircleBCodeText = GClass0.smethod_0("");
                        }
                    }
                }
                string text2 = oLogicEngine.Route(navBase_0.RoadMap.FORM_NAME);
                SurveyHelper.RoadMapVersion = navBase_0.RoadMap.VERSION_ID.ToString();
                string uriString = string.Format(GClass0.smethod_0("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text2);
                if (text2.Substring(0, 1) == GClass0.smethod_0("@"))
                {
                    uriString = string.Format(GClass0.smethod_0("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text2);
                }
                if (text2 == SurveyHelper.CurPageName)
                {
                    navigationService_0.Refresh();
                }
                else
                {
                    if (!FormIsOK(text2))
                    {
                        string text3 = string.Format(SurveyMsg.MsgErrorJump, surveyID, navCurPage, navBase_0.RoadMap.VERSION_ID, navBase_0.RoadMap.PAGE_ID, navBase_0.RoadMap.FORM_NAME);
                        MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text3 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
                        oLogicEngine.OutputResult(text3, GClass0.smethod_0("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
                        return false;
                    }
                    navigationService_0.RemoveBackEntry();
                    navigationService_0.Navigate(new Uri(uriString));
                }
                SurveyHelper.SurveySequence = surveySequence + 1;
                SurveyHelper.NavCurPage = navBase_0.RoadMap.PAGE_ID;
                SurveyHelper.CurPageName = text2;
                SurveyHelper.NavGoBackTimes = 0;
                SurveyHelper.NavOperation = GClass0.smethod_0("HŪɶ\u036eѣխ");
                SurveyHelper.NavLoad = 0;
            }
            catch (Exception)
            {
                string text4 = string.Format(SurveyMsg.MsgErrorJump, surveyID, navCurPage, navBase_0.RoadMap.VERSION_ID, navBase_0.RoadMap.PAGE_ID, navBase_0.RoadMap.FORM_NAME);
                MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text4 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
                oLogicEngine.OutputResult(text4, GClass0.smethod_0("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
                return false;
            }
            return true;
        }

        public void PageDataLog(int int_0, List<VEAnswer> list_0, Button button_0, string string_0)
        {
            button_0.IsEnabled = false;
            button_0.Content = SurveyMsg.MsgWaitingSave;
            Thread.Sleep(int_0);
            button_0.Content = SurveyMsg.MsgbtnNav_Content;
            button_0.IsEnabled = true;
        }

        public bool CheckLogic(string string_0)
        {
            if (string_0 == GClass0.smethod_0("Aŉɀ\u034aћՑه\u0742") && SurveyHelper.AutoFill)
            {
                return true;
            }
            new List<SurveyLogic>();
            foreach (SurveyLogic item in oSurveyLogicDal.GetCheckLogic(string_0))
            {
                string fORMULA = item.FORMULA;
                if (oLogicEngine.boolResult(fORMULA))
                {
                    string string_ = oLogicEngine.strShowText(item.LOGIC_MESSAGE, true);
                    string_ = oLogicEngine.strShowText(string_, false);
                    string nOTE = item.NOTE;
                    switch (item.IS_ALLOW_PASS)
                    {
                        case 1:
                            if (!SurveyHelper.AutoFill && MessageBox.Show(string_ + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
                            {
                                return false;
                            }
                            break;
                        case 3:
                            if (!SurveyHelper.AutoFill)
                            {
                                MessageBoxResult messageBoxResult = MessageBox.Show(string_ + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassModifyCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNoCancel, MessageBoxImage.Asterisk, MessageBoxResult.Cancel);
                                if (messageBoxResult.Equals(MessageBoxResult.Yes))
                                {
                                    SurveyHelper.QueryEditQn = nOTE;
                                    EditAnswerSingle editAnswerSingle2 = new EditAnswerSingle();
                                    editAnswerSingle2.oLogicEngine = oLogicEngine;
                                    editAnswerSingle2.ShowDialog();
                                    return false;
                                }
                                if (messageBoxResult.Equals(MessageBoxResult.Cancel))
                                {
                                    return false;
                                }
                            }
                            break;
                        case 0:
                            MessageBox.Show(string_, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
                            return false;
                        case 2:
                            if (MessageBox.Show(string_ + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassModify, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
                            {
                                SurveyHelper.QueryEditQn = nOTE;
                                EditAnswerSingle editAnswerSingle = new EditAnswerSingle();
                                editAnswerSingle.oLogicEngine = oLogicEngine;
                                editAnswerSingle.ShowDialog();
                            }
                            return false;
                    }
                }
            }
            return true;
        }

        public bool FormIsOK(string string_0)
        {
            string b = string_0.ToUpper();
            try
            {
                foreach (TypeInfo definedType in Assembly.GetExecutingAssembly().DefinedTypes)
                {
                    if (definedType.Name.ToUpper() == b)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Refresh()
        {
            DispatcherFrame dispatcherFrame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(Class68._003C_003E9.method_0), dispatcherFrame);
            Dispatcher.PushFrame(dispatcherFrame);
        }

        private void method_0(string string_0, int int_0)
        {
            if (new ScreenCapture().Capture(string_0, int_0))
            {
                if (!SurveyHelper.AutoFill)
                {
                    MessageBox.Show(SurveyMsg.MsgScreenCaptureDone + Environment.NewLine + string_0);
                }
            }
            else
            {
                MessageBox.Show(SurveyMsg.MsgScreenCaptureFail + Environment.NewLine + string_0);
            }
        }
    }
}
