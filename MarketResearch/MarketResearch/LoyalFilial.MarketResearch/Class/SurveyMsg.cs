using System;

namespace LoyalFilial.MarketResearch.Class
{
	public static class SurveyMsg
	{
		public static string FunctionUpload = "FunctionUpload_true";

		public static string FunctionDelete = "FunctionDelete_false";

		public static string RecordIsOn = "RecordIsOn_true";

		public static string ReadIsOn = "ReadIsOn_false";

		public static string ShowToolBar = "ShowToolBar_true";

		public static string FunctionQueryEdit = "FunctionQueryEdit_false";

		public static string FunctionAttachments = "FunctionAttachments_false";

		public static string OutputHistory = "OutputHistory_true";

		public static string IsSaveSequence = "IsSaveSequence_false";

		public static string StartOne = "StartOne_false";

		public static string MsgCityRange = "不再使用这个信息";

		public static string SurveyIDBegin = "0001";

		public static string SurveyIDEnd = "9999";

		public static int SurveyId_Length = 5;

		public static int CITY_Length = 1;

		public static int CITY_Start = 1;

		public static int CITY_End = 6;

		public static int Order_Length = 4;

		public static int PcCode_Length = 2;

		public static string AllowClearCaseNumber = "AllowClearCaseNumber_false";

		public static string SetPCNumber = "SetPCNumber_true";

		public static string SurveyRangeDemoPsw = "demo";
        /// <summary>
        /// 密码
        /// </summary>
		public static string SurveyRangePsw = "zd17916";

		public static bool SurveyRangePswOk = false;

		public static int DelaySeconds = 0;

		public static string Language = "zh-cn";

		public static string MsgProjectName = "蒙牛低温品类研究";

		public static string MsgCaption = "ZD17916";

		public static string MsgCompany = "ZDO";

		public static string ProjectId = "zd17916";

		public static string ClientId = "zdo";

		public static string OSSRegion = "sz";

		public static string VersionDate = "2017-10-22";

		public static string TestVersionActiveDays = "30";

		public static string VersionActiveDays = "360";

		public static string IsCheckLicnese = "IsCheckLicnese_false";

		public static string FunctionQuotaManager = " FunctionQuotaManager_true";

		public static string QuotaManagerType = "LOCAL";

		public static string QuotaManagerHost = "localhost";

		public static string QuotaManagerPort = "9000";

		public static string QuotaManagerFileHost = "localhost";

		public static string QuotaManagerFilePort = "9999";

		public static string MsgExpire = "版本使用有效期已过, 如需正式版，请与开发者(2404769274@QQ.com)联系。";

		public static string MsgSelectDir = "请选择目录";

		public static string MsgDirNotExist = "目录不存在或错误的目录,请选择正确的目录";

		public static string MsgDirNoFile = "当前的目录没有问卷数据文件，请检查！";

		public static string MsgSpssVar = "错误在：变量名[{0}]重复定义！";

		public static string MsgCopyRight = "版权声明";

		public static string MsgQuerySurveyIdCheck = "请填写问卷编号！";

		public static string MsgQuerySurveyIdLength = "问卷编号是 {0} 位数字！";

		public static string MsgFileNotFound = "未找到文件 {0} !";

		public static string MsgExportOverwrite = "导出 Dat 文件将覆盖原有文件, 确定要导出吗？";

		public static string MsgExcelOutput = "要输出Excel问卷吗？";

		public static string MsgExcelDone = "输出完成！请到Excel子文件夹查看输出资料。";

		public static string MsgRecodeDone = "RECODE完成！请到RECODE子文件夹查看输出资料。";

		public static string MsgMTDone = " MT 完成！请到对应子文件夹查看输出资料。";

		public static string MsgExcelOutputDone = "输出问卷 {0}完成，请查询 {1}";

		public static string MsgNotRecordFile = "录音文件 {0} 或 {1} 不存在！";

		public static string MsgAttachNotSelectFile = "未选择文件！";

		public static string MsgAttachNoFile = "本题 {0} 没有附件！";

		public static string MsgNoNet = "未能连接服务器，请检查网络！";

		public static string MsgKeyExprie = "密钥文件已经失效或过期，请提供合法的密钥文件！";

		public static string MsgNoKey = string.Concat(new string[]
		{
			"在辅助工具所在的文件夹中没有找到任何 密钥文件 ！",
			Environment.NewLine,
			Environment.NewLine,
			"请把对应的密钥文件（诸如：北京.key）放到以下这个文件夹中 :",
			Environment.NewLine,
			Environment.NewLine,
			Environment.CurrentDirectory
		});

		public static string MsgJobDone = "处理完成！";

		public static string MsgOssExprie = "存储服务器已过期！";

		public static string MsgDirChange = "不能把原始数据放置在这个文件夹中，请更换文件夹！";

		public static string MsgAttachHaveSame = "同名文件 {0} 已添加！";

		public static string MsgCopyFileFail = "复制文件 {0} 失败！";

		public static string MsgDeleteFileFail = "删除文件 {0} 失败！";

		public static string MsgNoLegalExcel = "Excel 文件不是合法模板，请使用正确模板建立！";

		public static string MsgFlagOther = "需要填写文本说明";

		public static string MsgFlagNone = "排他选项";

		public static string MsgFlagOtherNone = "排他选项，并需要填写文本说明";

		public static string MsgNotFill = "请在漏答的空白处补填答案！";

		public static string MsgFillOverLength = "填写内容不能超出 250 个中文字！";

		public static string MsgNotFillNumber = "请输入合法数值！";

		public static string MsgNotSelected = "请选择答案！";

		public static string MsgNotFillOther = "选择了需要文本说明的选项，请在文本框中填写详细说明 ！";

		public static string MsgNotRunApp = "请先执行外部程序的运行！";

		public static string MsgRunAppError = "执行外部程序失败！";

		public static string MsgNotMacth = "填写数值与选择项的范围不符，请核查修正！";

		public static string MsgLegalMoney = "请输入合法的金额, 标准如: 12.38 ！";

		public static string MsgEndTerminate = "对不起，受访者不符合此次调查的要求。请访问员及时与督导取得联系";

		public static string MsgPassCheck = "请访问员或者督导向受访者再次确认！" + Environment.NewLine + 
            "选择[是]，继续下一题！";

		public static string MsgPassModify = "请向受访者确认是否需要修改之前的答案！" + Environment.NewLine 
            + "选择[是]，修改之前的答案。";

		public static string MsgPassModifyCheck = string.Concat(new string[]
		{
			"请再次向受访者确认是否改变之前的答案！",
			Environment.NewLine,
			"选择[是]，修改之前的答案。",
			Environment.NewLine,
			"选择[否]，继续下一题！"
		});

		public static string VersionText = "版本：";

		public static string VersionID = "vYYYYMMDD";

		public static string MsgOverTime = "这个程序使用的第三方授权组件的许可证已经过期！" + Environment.NewLine + Environment.NewLine 
            + "如需续费使用，请与开发者(2404769274@qq.com)联系。";

		public static string MsgErrorTime = "系统日期错误，请先调整正确！";

		public static string MsgTestOverTime = "测试版本使用有效期已过, 如需正式版，请与开发者(2404769274@qq.com)联系。";

		public static string MsgRightTitle = "版权声明";

		public static string MsgRight = string.Concat(new string[]
		{
			"      该系统的技术支持由 Loyal.Filial 提供。",
			Environment.NewLine,
			Environment.NewLine,
			"      此系统的版权属于 Loyal.Filial 成员所有。任何人未经Loyal.Filial 成员的书面许可，不能对该软件进行任何形式的逆向工程、破译以及修改任何信息。Loyal.Filial 成员对上述行为保留对其追究法律责任的权利。",
			Environment.NewLine,
			Environment.NewLine,
			" Loyal.Filial   Email：2404769274@QQ.com"
		});

		public static string MsgbtnNav_Content = "继  续";

		public static string MsgbtnNav_SaveText = "检查逻辑及答案保存中…";

		public static string MsgNotSelectedSurvey = "请选择问卷！";

		public static string MsgReadSurveyOK = "问卷资料读入已完成！";

		public static string MsgNavError = "导航错误,无法往下执行,请通知管理员检查日志！";

		public static string MsgNotBack = "未建立问卷，无法使用这个导航条的按钮返回上一题！";

		public static string MsgNoID = "未建立问卷，无法使用这个导航条跳转！";

		public static string MsgFirstPage = "这是新的访问段落的开始，不能再回退了！";

		public static string MsgFirstPageInfo = "这题是新的访问段落的开始，在【正式版】中是不能回退的！";

		public static string MsgNotGoPage = "请选择要跳转到的目标页！";

		public static string MsgNotDoIt = "无法在本页面启用这个功能！";

		public static string MsgExitBreak = "问卷 {0} 未完成,确定要退出吗？";

		public static string MsgHaveFinished = "问卷已完成, 请选择 [是] 进入问卷结果查阅, [否] 重新输入问卷编号！";

		public static string MsgNotFinished = "问卷已存在,但未完成, 是否继续？";

		public static string MsgBackTimes = "当前页已保存答案，返回上一页将导致本页数据作废, 是否继续？";

		public static string MsgNotPre = "问卷读入时，不允许马上使用返回上一页功能，需要先执行到下一页。";

		public static string MsgQueryEditNoSave = "当前问卷已修改, 确定放弃保存修改吗？";

		public static string MsgRangeMissPsw = "需要正确的配置管理密码才能修改本机问卷范围配置！";

		public static string MsgRangePswError = "配置管理密码不正确！";

		public static string MsgRangeLength = "[问卷编号] 是 {0} 位数字！";

		public static string MsgRangeOut = "[问卷编号] 超出指定范围！";

		public static string MsgRangeBegin = "[问卷开始编号] 第1位与城市不一致！";

		public static string MsgRangeEnd = "[问卷结束编号] 第1位与城市不一致！";

		public static string MsgRangeCompare = "[问卷开始编号] 不能大于 [问卷结束编号]！";

		public static string MsgRangeFirst = "[问卷开始编号] 与 [问卷结束编号] 的第1位必须相同！";

		public static string MsgRangePCCode = "[本机编号] 是 {0} 位数字！";

		public static string MsgRangePCCodeStart = "[本机编号] 第1位与城市不一致！";

		public static string MsgSetSaveOk = "保存配置完成,将回到主界面！";

		public static string MsgDeleteOk = "问卷 {0} 删除完成！";

		public static string MsgDeleteMissSurveyId = "请输入问卷编号！";

		public static string MsgDeleteNotExist = "问卷 {0} 不存在！";

		public static string MsgNoExcel = "检查到系统未安装 Office Excel 软件，请先安装！";

		public static string MsgTaptipOpen = "调出键盘";

		public static string MsgTaptipClose = "隐藏键盘";

		public static string MsgNoWebCam = "未检测到可用拍摄设备，请自行使用其它拍摄设备进行拍摄！";

		public static string MsgNoWebCamConnect = "未选择可用摄像头，请选择！";

		public static string MsgPhotoSave = "照片已保存到 {0} ！";

		public static string MsgNotTakePhoto = "未完成拍照，请先拍照！";

		public static string MsgNotRecordStart = "录音未开启，请开启！";

		public static string MsgQuestionNoRecord = "本题没有录音。";

		public static string MsgNotMedia = "未能找到对应媒体文件！";

		public static string MsgNotEdit = "本项不是可修改内容！";

		public static string MsgHaveEdit = "问卷内容已修改，需要覆盖导出Dat文件使生效！";

		public static string MsgExportDatFile = "问卷 {0} Dat文件成功导出到 {1} 文件夹中！";

		public static string MsgOutputOk = "成功重新导出所有原始数据到 {0} 文件夹中！";

		public static string MsgEditNote = "（请注意：本项已被修改!!!）";

		public static string MsgPageNoMP3 = "本题未配音。";

		public static string MsgErrorEnd = Environment.NewLine + Environment.NewLine + "请把此错误信息告知开发者(2404769274@QQ.com)！";

		public static string MsgNoKeyFile = "许可证文件未找到！";

		public static string MsgErrorKeyFile = "不是正确的许可证文件！";

		public static string MsgFinishAndUploaded = "问卷({0})已完成，并成功上传！";

		public static int MsgProgramType = 1;

		public static string MsgAutoDo_Finish = string.Concat(new string[]
		{
			"自动生成数据操作完成！",
			Environment.NewLine,
			Environment.NewLine,
			"数据产生情况：",
			Environment.NewLine,
			"原已存在问卷数：{0}",
			Environment.NewLine,
			"自动产生问卷数：{1}",
			Environment.NewLine,
			"补充之前未完数：{2}",
			Environment.NewLine,
			Environment.NewLine,
			"共耗时：{3}"
		});

		public static string MsgAutoDo_Ask = "友情提示：这次操作将自动产生 {0}份 问卷数据！" + Environment.NewLine + Environment.NewLine
            + "是否确认启动这次的自动数据生成操作？";

		public static string MsgAutoDo_AskTitle = "操作确认";

		public static string MsgAutoDo_CityCount = "（已选择：{0}）";

		public static string MsgAutoDo_NoCase = "要产生的问卷总数为 0 ！";

		public static string MsgNeedConfig = "请先到这个页面的右下角【问卷配置】中设置好这台设备的基本资料。";

		public static string MsgWrongJump = "非法的测试跳转方式：";

		public static string MsgErrorGoBack = "问卷( {0} )无法从当前页[ {1} ] 返回到RoadMap：VERSION_ID={2}，PAGE_ID={3}，FORM_NAME={4}";

		public static string MsgErrorJump = "问卷( {0} )无法直接从当前页[ {1} ]跳到：VERSION_ID={2}，PAGE_ID={3}，FORM_NAME={4}";

		public static string MsgErrorDevice = "选定音频设备有误，使用默认音频设备!";

		public static string MsgNoDevice = "系统中没有音频捕捉设备!";

		public static string MsgRecordStart = "正在录音";

		public static string MsgRecordStartInfo = "录音已启动，将在问卷结束停止！";

		public static string MsgErrorRecordStart = "录音启动失败,请检查录音设备配置!";

		public static string MsgRecordNoSurveyId = "未有问卷编号，不能启动录音！";

		public static string MsgRecordError = "无法录音";

		public static string MsgPrePageAnswer = "=========上一页答案==========";

		public static string MsgNewSurvey = "正在建立新问卷,请稍候...";

		public static string MsgErrorSysSlow = "Windows系统过慢，造成建立问卷数据准备超时！为防止因此导致访问数据丢失，请优化系统后再运行MarketResearch程序,MarketResearch程序将自动退出 ！";

		public static string MsgErrorCreateSurvey = "问卷 {0} 创建失败, 可能由于非标准的系统日期格式设置导致，请设定标准的系统日期格式！";

		public static string MsgNoDataFile = "数据未成功生成或打包失败，需重启程序尝试。";

		public static string MsgUploadSingleVersion = "单问卷版本上传OSS";

		public static string MsgEndSurveyInfo = "访问结束,非常感谢您的参与和配合!";

		public static string MsgNoSelectAttach = "未选择附件！";

		public static string MsgErrorRoadmap = "错误的RoadMap定义：";

		public static string MsgNoModifyQSet = "没有设置对应要修改的题目！请尽快反馈！";

		public static string MsgNoFunction = "功能未完成！";

		public static string MsgCurrentContent = "当前内容：";

		public static string MsgErrorRoadmapTip = "由于这是问卷开始就出现的错误，所以请先直接点“退出”，然后等MarketResearch系统管理员更新程序后，再重新访问。";

		public static string MsgConfirmEnd = "确认对表格内容已完成审核，并结束吗？";

		public static string MsgClickConfirmEnd = "点击 确认 结束!";

		public static string MsgFillNotSmall = "请填写不小于 {0} 的数值。";

		public static string MsgFillNotBig = "请填写不大于 {0} 的数值。";

		public static string MsgSelectOne = "请先选择一个选项！";

		public static string MsgLeftSmallRight = "左边数值应该小于右边数值。";

		public static string MsgLeftNotSmallRight = "左边数值不应该小于右边数值。";

		public static string MsgRightSmallLeft = "右边数值应该小于左边数值。";

		public static string MsgRightNotSmallLeft = "右边数值不应该小于左边数值。";

		public static string MsgConfirmExit = "确认要退出吗？";

		public static string MsgIdOutRange = "问卷编号超出本机范围!";

		public static string MsgIdNotSame = "对不起，问卷编号与您刚录入的编号不一致！";

		public static string MsgFillIntFit = "填写数值需符合：{0} 位数字。";

		public static string MsgFillFit = "填写数值需符合：{0} 位小数。";

		public static string MsgFillFitReplace = "位整数，";

		public static string MsgOptionDelete = "删除选项";

		public static string MsgOptionAdd = "添加选项";

		public static string MsgOptionSave = "保存选项";

		public static string MsgOptionModify = "修改选项";

		public static string MsgOptionSaveModify = "保存修改";

		public static string MsgOptionNotInList = "输入的选项不在设定的选项列表中！";

		public static string MsgPrgNotFound = "没有找到要启动的程序！";

		public static string MsgPrgNotRun = "没有成功启动程序！请检查要启动的程序是否可正常运行。";

		public static string MsgFirstFillCode = "请先填写编码！";

		public static string MsgFirstFillText = "请先填写选项文本！";

		public static string MsgFillCodeRepeat = "填写的编码和现有的选项发生重码！";

		public static string MsgFinishDeal = "处理完成!";

		public static string MsgNotAfterYM = "选择日期不能在当前年月之后！";

		public static string MsgNotAfterDate = "选择日期不能在当前日期之后！";

		public static string MsgConfirmReplace = "这个操作将使用【{0}】替换输入框中的所有内容，是否确认替换？";

		public static string MsgSelectFixAnswer = "请选择【{0}】的答案。";

		public static string MsgPointSmall = "【{0}】的评分【{1}分】不能低于其它句子的最低评分【{2}分】！";

		public static string MsgPointBig = "【{0}】的评分【{1}分】不能高于其它句子的最高评分【{2}分】！";

		public static string MsgNotSelectOther = "当选择了【{0}】时，不能同时选择其它选项！";

		public static string MsgNotOverOption = "这个项目不允许分配超过 {0} 次相同的筹码数量（有问题的筹码数：{1} )。请重新分配！";

		public static string MsgNotOverOptionWeak = "有至少 {0} 个地方分配了相同的筹码数（筹码数：{1} ）。请访问员或者督导向受访者再次确认！选择[是], 则忽略这种逻辑检查！";

		public static string MsgNeedMore = "排在“{0}”前面的数量必须比它多。请修正！";

		public static string MsgNeedMoreWeak = "排在“{0}”前面的数量应该比它多。请访问员或者督导向受访者再次确认！选择[是], 则忽略这种逻辑检查！";

		public static string MsgNotOptionZero = "所有选项的数量不应该都是 0 ！";

		public static string MsgNeedFinishNum = "请把剩余的数量 {0} 分配完毕。";

		public static string MsgNeedFinishRight = "请把分配多了的数量适当减少到正确。";

		public static string MsgErrorWriteFile = "无法写入数据到输出文件！输出文件：{0} .";

		public static string MsgCaptionError = "重要错误";

		public static string MsgNotSetDataCopy = "[{0}]页没有设置好对应的DATA COPY语句，这可能会误导后续的访问流程，请告知程序员！";

		public static string MsgNotSetRecode = "[{0}]页没有设置对应的RECODE语句，这可能会误导后续的访问流程，请告知程序员！";

		public static string MsgNotSelectClassB = "有选项只选择了第一级，没有选择第2级。请先选择！";

		public static string MsgSelectRepeat = "请不要选择重复的选项";

		public static string MsgNotSetMachine = "未设定本机编号，请到配置界面先设定。";

		public static string MsgUploadDemoFinish = "数据上传演示完成！";

		public static string MsgUploadFinish = "数据成功上传！";

		public static string MsgUpFileMoveFail = "未能把成功上传的数据从Output文件夹移到Upload文件夹中。请自行移走，以免下次重复上传！";

		public static string MsgNotFilePackError = "未有问卷数据文件或打包失败,请检查!";

		public static string MsgUpFailCheckNet = "文件上传失败,请检查网络!";

		public static string MsgUpFileMove = "转移已上传文件到 ";

		public static string MsgUpProcInfo = "正在上传文件的第 {0} 部分, {1} KB.";

		public static string MsgUploading = "正在上传问卷数据.....  ";

		public static string MsgUpPacking = "正在打包问卷数据.....  ";

		public static string MsgUpDealing = "正在开始处理文件.....  ";

		public static string MsgUpFinish = "处理完成,耗时 {0} 秒。";

		public static string MsgUpFinishDemo = "处理完成,耗时 1 秒。";

		public static string MsgWaitingSave = "正在保存,请稍候...";

		public static string MsgAttachSame = "同名文件 {0} 已添加！";

		public static string MsgAttachCopyFail = "复制文件 {0} 失败！";

		public static string MsgAttachDelFail = "删除文件 {0} 失败！";

		public static string MsgAttachNotExist = "文件 {0} 不存在！";

		public static string MsgFrmCodeRange = " 本机问卷编号范围：{0} -- {1}";

		public static string MsgFrmCodePre = "重新进入上一问卷[{0}]";

		public static string MsgFrmCodeAutoNext = "自动进入下一问卷[{0}]";

		public static string MsgFrmCodeLen = "问卷编号是 {0} 位数字！";

		public static string MsgFrmCodeNotSame = "对不起，问卷编号第1位与[{0}]不一致！";

		public static string MsgFrmCodeConfirm = "确认问卷编号";

		public static string MsgFrmCode = "问卷编号";

		public static string MsgFrmCurrentID = "当前问卷编号：";

		public static string MsgFrmQueryID = "正在查询问卷...";

		public static string MsgFrmCodeFailCreate = "问卷 {0} 创建失败, 可能由于非标准的系统日期格式设置导致，请设定标准的系统日期格式！";

		public static string MsgFrmCodeCreate = "问卷建立完成！";

		public static string MsgFrmCodeNow = "当前时间：";

		public static string MsgFrmNoCodeForSelect = "没有任何选项符合指定的选取要求，无法自动填充！";

		public static string MsgMP3Info = string.Concat(new string[]
		{
			"录音时长超过 {0} 分钟！",
			Environment.NewLine,
			"转换为MP3格式的话，可能会因资源耗竭导致等待时间过久。",
			Environment.NewLine,
			Environment.NewLine,
			"请确认是否进行转换？"
		});

		public static string MsgMP3Caption = "MP3转换确认";

		public static string MsgFromBigToSmall = "数值要求从大到小地输入。";

		public static string MsgFromSmallToBig = "数值要求从小到大地输入。";

		public static string MsgScreenCaptureDone = "屏幕截图完成。保存到文件：";

		public static string MsgScreenCaptureFail = "屏幕截图失败！无法截图到：";

		public static string MsgOrderInfo = string.Concat(new string[]
		{
			"Loyal.Filial友情建议：",
			Environment.NewLine,
			"　　为各个的访问设备设置相互独立的问卷编号段，可以有效避免问卷编号重复的问题发生。",
			Environment.NewLine,
			"　　因此建议尽可能对问卷流水序号进行系统性的分段设置！"
		});

		public static string MsgBit1 = "(问卷编号后";

		public static string MsgBit2 = "位)";

		public static string MsgBit3 = "位数字)";

		public static string NoCity ="不设置问卷编号分段";

		public static string MP3Mode1 = "禁止转换";

		public static string MP3Mode2 = "提示转换";

		public static string MsgInteNum = "请填写 访问员编号后 {0} 位数字！";

		public static string MsgPlay = "播放";

		public static string MsgContinue = "继续";

		public static string MsgPause = "暂停";

		public static string MsgStartAt = "开始位置：";

		public static string MsgNoRecordFile = "录音文件 {0} 或 {1} 不存在！";

		public static string MsgPlayTip = "点击播放";

		public static string MsgPauseTip = "点击暂停";

		public static string MsgMAless = "至少需要选择 {0} 个选项。";

		public static string MsgMAmore = "最多可以选择 {0} 个选项。";

		public static string MsgFillPrveText = "请顺序填写资料，不要跳填。";

		public static string MsgNoCamera = "没有拍照设备!";

		public static string MsgMXSA_info1 ="到【{0}】为止已经连续选择了{1}次相同的选项！";

		public static string MsgMXSA_info2 = "选项【{0}】被选择了{1}次！";

		public static string MsgMXSA_info3 = string.Concat(new string[]
		{
			"已经连续选择了同一选项{0}次！",
			Environment.NewLine,
			"请确认这种选择是否受访者真实的想法。",
			Environment.NewLine,
			Environment.NewLine,
			"是否继续监察这种连续点击操作？",
			Environment.NewLine,
			"选择[是]，再出现这种情况将会继续提醒。",
			Environment.NewLine,
			"选择[否]，不再监察这种情况。"
		});

		public static string MsgRB_info1 = "没有选择任何答案，是否确认受访者的情况不适用于这个题目？";

		public static string MsgRB_info2 = "还有选项未排序，是否确认受访者不选择剩下的选项？";

		public static string MsgCallApp_BtnCall = "启动";

		public static string MsgCallApp_BtnNav ="已启动程序";

		public static string MsgFillMode_NotJING = "请以【#题号1#题号2#】方式设置，即题号前后要有#号。";

		public static string MsgCaptureFail ="无法截屏";

		public static string MsgNotCapture = "注意：因Windows系统问题，导致无法保存该题的截图！请联系督导！";

		public static string MsgNotSearch = "请点【搜索】按钮进行定位。";

		public static string MsgMapNotFound = "地图未能定位到 {0}，请确保已经连接互联网！" + Environment.NewLine + "(错误代码：{1})";

		public static string MsgNoLngLat = "地图未能成功定位到 {0}！" + Environment.NewLine + Environment.NewLine + "请确认是否仍然【继续】到下一题？";

		public static string MsgBPTO_NotFinish = "还未完成BPTO价格测试，是否确认当前的价格情况下不会再选择任何答案？";
	}
}
