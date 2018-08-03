using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi.QEdit
{
	public class EditAttachments : Window, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003F15_003F
		{
			public string strFileName;

			internal bool _003F335_003F(SurveyAttach _003F483_003F)
			{
				return _003F483_003F.ORIGINAL_NAME == strFileName;
			}
		}

		[CompilerGenerated]
		private sealed class _003F16_003F
		{
			public string strOriginalName;

			internal bool _003F336_003F(SurveyAttach _003F483_003F)
			{
				return _003F483_003F.ORIGINAL_NAME == strOriginalName;
			}
		}

		[CompilerGenerated]
		private sealed class _003F17_003F
		{
			public string strOriginalName;

			internal bool _003F337_003F(SurveyAttach _003F483_003F)
			{
				return _003F483_003F.ORIGINAL_NAME == strOriginalName;
			}
		}

		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		private List<SurveyAttach> oListSource = new List<SurveyAttach>();

		internal TextBlock txtQuestionTitle;

		internal Grid gridContent;

		internal TextBox txtAttach;

		internal Button btnSelectAttach;

		internal Button btnAddAttach;

		internal ListBox ListAttach;

		internal TextBox txtSelectedAttach;

		internal Button btnOpenAttach;

		internal Button btnRemoveAttach;

		internal Button btnExit;

		private bool _contentLoaded;

		public EditAttachments()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_003e: Incompatible stack heights: 0 vs 1
			base.Topmost = true;
			Hide();
			Show();
			if (SurveyHelper.AttachReadOnlyModel)
			{
				btnSelectAttach.Visibility = Visibility.Hidden;
				((EditAttachments)/*Error near IL_0022: Stack underflow*/).btnAddAttach.Visibility = Visibility.Hidden;
				btnRemoveAttach.Visibility = Visibility.Hidden;
			}
			txtQuestionTitle.Text = SurveyHelper.AttachQName;
			_003F113_003F();
		}

		private void _003F258_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Invalid comparison between Unknown and I4
			CommonOpenFileDialog val = new CommonOpenFileDialog();
			val.set_EnsureReadOnly(true);
			val.set_Title(SurveyMsg.MsgCaption);
			if ((int)val.ShowDialog() == 1)
			{
				txtAttach.Text = val.get_FileName();
			}
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F259_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F15_003F _003F15_003F = new _003F15_003F();
			string text = txtAttach.Text;
			if (text == _003F487_003F._003F488_003F("@ĸɝ"))
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				int num = text.LastIndexOf(_003F487_003F._003F488_003F("]"));
				text.Substring(0, num);
				_003F15_003F.strFileName = text.Substring(num + 1);
				int num2 = _003F15_003F.strFileName.LastIndexOf(_003F487_003F._003F488_003F("/"));
				string text2 = _003F15_003F.strFileName.Substring(num2 + 1);
				if (oListSource.FindIndex(_003F15_003F._003F335_003F) > -1)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgAttachSame, _003F15_003F.strFileName), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					SurveyAttach surveyAttach = new SurveyAttach();
					surveyAttach.SURVEY_ID = SurveyHelper.AttachSurveyId;
					surveyAttach.PAGE_ID = SurveyHelper.AttachPageId;
					surveyAttach.QUESTION_NAME = SurveyHelper.AttachQName;
					surveyAttach.FILE_NAME = string.Format(_003F487_003F._003F488_003F("`Īɤ\u0347Ѭԧ٨\u074b\u0868ठਫଢ଼\u0c42൪\u0e69ན၃ᅂቤ፥ᑴᕵᙸᜪ\u1878\u1931\u1a7c"), SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName, DateTime.Now, text2);
					surveyAttach.FILE_TYPE = text2;
					surveyAttach.ORIGINAL_NAME = _003F15_003F.strFileName;
					string destFileName = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TňɳͱѴնٶݝ") + surveyAttach.FILE_NAME;
					if (!Directory.Exists(Environment.CurrentDirectory + _003F487_003F._003F488_003F("TňɳͱѴնٶݝ")))
					{
						Directory.CreateDirectory(Environment.CurrentDirectory + _003F487_003F._003F488_003F("TňɳͱѴնٶݝ"));
					}
					try
					{
						File.Copy(text, destFileName, true);
					}
					catch (Exception)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgAttachCopyFail, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					oSurveyAttachDal.Add(surveyAttach);
					oListSource.Add(surveyAttach);
					ListAttach.Items.Add(_003F15_003F.strFileName);
				}
			}
		}

		private void _003F260_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F16_003F _003F16_003F = new _003F16_003F();
			_003F16_003F.strOriginalName = txtSelectedAttach.Text;
			if (_003F16_003F.strOriginalName == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			else
			{
				int num = oListSource.FindIndex(_003F16_003F._003F336_003F);
				if (num >= 0)
				{
					string fILE_NAME = oListSource[num].FILE_NAME;
					string text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Tňɳͱєնٶݝ") + fILE_NAME;
					string destFileName = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TŒɨ\u0356ѥյ٧ݝ") + fILE_NAME;
					try
					{
						if (!Directory.Exists(Environment.CurrentDirectory + _003F487_003F._003F488_003F("[œɫ\u0357Ѣմ٤")))
						{
							Directory.CreateDirectory(Environment.CurrentDirectory + _003F487_003F._003F488_003F("[œɫ\u0357Ѣմ٤"));
						}
						File.Copy(text, destFileName, true);
						File.Delete(text);
					}
					catch (Exception)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgAttachDelFail, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					oSurveyAttachDal.DeleteByQNameByFileName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName, fILE_NAME);
					oListSource.RemoveAt(num);
					ListAttach.Items.Remove(_003F16_003F.strOriginalName);
				}
			}
		}

		private void _003F261_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			txtSelectedAttach.Text = (string)ListAttach.SelectedValue;
		}

		private void _003F113_003F()
		{
			oListSource = oSurveyAttachDal.GetListByQName(SurveyHelper.AttachSurveyId, SurveyHelper.AttachQName);
			ListAttach.Items.Clear();
			foreach (SurveyAttach item in oListSource)
			{
				ListAttach.Items.Add(item.ORIGINAL_NAME);
			}
		}

		private void _003F262_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00a6: Incompatible stack heights: 0 vs 1
			//IL_00c1: Incompatible stack heights: 0 vs 1
			_003F17_003F _003F17_003F = new _003F17_003F();
			_003F17_003F.strOriginalName = txtSelectedAttach.Text;
			if (_003F17_003F.strOriginalName == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNoSelectAttach, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			int num = oListSource.FindIndex(_003F17_003F._003F337_003F);
			if (num < 0)
			{
				return;
			}
			goto IL_0052;
			IL_0052:
			string fILE_NAME = oListSource[num].FILE_NAME;
			string text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TňɳͱѴնٶݝ") + fILE_NAME;
			if (File.Exists(text))
			{
				Process.Start(text);
			}
			else
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgAttachNotExist, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			return;
			IL_00ac:
			goto IL_0052;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\0ũɞ\u035fђԄ٪\u0749ࡗ\u094fਞ\u0b47\u0c4c\u0d4f๑ཏ\u1071ᅻታ፨ᐴᕫᙼ\u177c\u187eᥢᨺ\u1b71ᱷᵻṥά⁻ⅺ≬⍯④╧♬❦⡳⥵⨫⭼Ɫ\u2d6f\u2e6d"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0017:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((EditAttachments)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				gridContent = (Grid)_003F350_003F;
				break;
			case 4:
				txtAttach = (TextBox)_003F350_003F;
				break;
			case 5:
				btnSelectAttach = (Button)_003F350_003F;
				btnSelectAttach.Click += _003F258_003F;
				break;
			case 6:
				btnAddAttach = (Button)_003F350_003F;
				btnAddAttach.Click += _003F259_003F;
				break;
			case 7:
				ListAttach = (ListBox)_003F350_003F;
				ListAttach.SelectionChanged += _003F261_003F;
				break;
			case 8:
				txtSelectedAttach = (TextBox)_003F350_003F;
				break;
			case 9:
				btnOpenAttach = (Button)_003F350_003F;
				btnOpenAttach.Click += _003F262_003F;
				break;
			case 10:
				btnRemoveAttach = (Button)_003F350_003F;
				btnRemoveAttach.Click += _003F260_003F;
				break;
			case 11:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
