<%@ Page language="c#" Codebehind="SYSSqlServerSet.aspx.cs" AutoEventWireup="false" Inherits="Sunlike.Web.DRP.SYSSqlServerSet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SYSSqlServerSet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="SYSSqlServerSet" method="post" runat="server">
			<table style="BACKGROUND-POSITION: 50% bottom; BACKGROUND-REPEAT: repeat-x" height="100%"
				cellSpacing="0" cellPadding="0" width="100%" background="IMAGES/install_05.jpg" border="0">
				<tr height="10">
					<td><IMG src="IMAGES/zh-cn/install_01.jpg"></td>
					<td style="HEIGHT: 15px" width="100%" background="IMAGES/install_02.jpg"></td>
					<td style="HEIGHT: 15px"><IMG src="IMAGES/install_03.jpg"></td>
				</tr>
				<tr vAlign="top" height="100%">
					<td style="BACKGROUND-POSITION: left bottom; BACKGROUND-REPEAT: no-repeat" align="center"
						background="IMAGES/install_04.jpg" colSpan="3">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr vAlign="top">
								<td height="50" vAlign="bottom" align="center"><IMG src="IMAGES/zh-cn/install_sql.gif"></td>
							</tr>
							<tr vAlign="top">
								<td align="center"><br>
									<table cellSpacing="0" cellPadding="0" width="550" border="0">
										<tr>
											<td align="right" height="30" width="250"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblServer" runat="server" Width="65px">服务器名</asp:label>:</td>
											<td align="left" height="30" width="300"><asp:textbox id="txtServer" runat="server" Width="130px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="right" height="30"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblServer_User" runat="server" Width="65px">用户</asp:label>:</td>
											<td align="left" height="30"><asp:textbox id="txtSqlUser" runat="server" Width="130px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="right" height="30"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblServer_Pswd" runat="server" Width="65px">密码</asp:label>:</td>
											<td align="left" height="30"><asp:textbox id="txtSqlPswd" runat="server" Width="130px" TextMode="Password"></asp:textbox><asp:textbox id="txtPwdHide" style="DISPLAY: none" runat="server"></asp:textbox></td>
										</tr>
										<tr>
										    <td></td>
											<td height="30">
												<P align="left">
													<asp:button id="btnConn" runat="server" Width="102px" Text="连接测试" CssClass="Button"></asp:button></P>
											</td>
										</tr>
										<tr>
											<td align="right" height="30"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblDataBase" runat="server" Width="65px">帐套名</asp:label>:</td>
											<td><asp:dropdownlist id="ddlCompNo" runat="server"></asp:dropdownlist>
												<asp:Button id="btnRef" runat="server" Width="60px" CssClass="button" Text="数据刷新"></asp:Button></td>
										</tr>
										<tr>
											<td colSpan="2" height="20" style="HEIGHT: 20px" align="center">
												<asp:label id="lblMsg" runat="server" ForeColor="Red" Width="500px"></asp:label>
											</td>
										</tr>
										<tr>
											<td colspan="2" align="center">
												<asp:button id="btnPri" runat="server" CssClass="button" Text="上一步"></asp:button><asp:button id="btnNext" runat="server" CssClass="button" Text="下一步"></asp:button>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
