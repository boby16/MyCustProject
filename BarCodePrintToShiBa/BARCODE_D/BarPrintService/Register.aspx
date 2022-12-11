<%@ Page language="c#" Codebehind="Register.aspx.cs" AutoEventWireup="false" Inherits="Sunlike.Web.DRP.Register" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server"  >
		<title>
			产品注册
		</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Register" method="post" runat="server">
			<table id="Table1" borderColor="#000000" cellSpacing="0" cellPadding="2" width="500" align="center"
				borderColorLight="#ffffff" border="0">
				<tr>
					<td height="24">
						<fieldset style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; WIDTH: 100%; PADDING-TOP: 4px"><LEGEND align="left"><asp:label id="lblTitle" tabIndex="5" runat="server">产品注册</asp:label></LEGEND>
							<table id="Table2" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblRegKey" tabIndex="2" runat="server">注册序号</asp:label>:</td>
									<td width="400" height="24">&nbsp;
									    <asp:TextBox id="txtRegKey" tabIndex="3" runat="server"></asp:TextBox></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td height="24">
						<fieldset style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 4px; WIDTH: 100%; PADDING-TOP: 4px"><LEGEND align="left"><asp:label id="lblProxy" runat="server">代理服务器</asp:label></LEGEND>
							<table id="Table3" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
							    <tr>
							        <td colspan="2">
                                        &nbsp;</td>
							    </tr>
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblServer" tabIndex="2" runat="server">服务器</asp:label>:</td>
									<td width="400" height="24">&nbsp;
										<asp:textbox id="txtServer" runat="server" Width="380px"></asp:textbox></td>
								</tr>
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblPort" tabIndex="2" runat="server">端口</asp:label>:</td>
									<td width="400" height="24">&nbsp;
										<asp:textbox id="txtPort" runat="server" Width="380px">8080</asp:textbox></td>
								</tr>
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblUser" tabIndex="2" runat="server">用户名</asp:label>:</td>
									<td width="400" height="24">&nbsp;
										<asp:textbox id="txtUser" runat="server" Width="380px"></asp:textbox></td>
								</tr>
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblPswd" tabIndex="2" runat="server">密码</asp:label>:</td>
									<td width="400" height="24">&nbsp;
										<asp:textbox id="txtPswd" runat="server" Width="380px" TextMode="Password"></asp:textbox></td>
								</tr>
								<tr>
									<td align="right" width="100" height="24">&nbsp;
										<asp:label id="lblDomain" tabIndex="2" runat="server">域</asp:label>:</td>
									<td width="400" height="24">&nbsp;
										<asp:textbox id="txtDomain" runat="server" Width="380px"></asp:textbox></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td height="24">
						<div align="center"><asp:label id="lblAlert" tabIndex="4" runat="server" ForeColor="#C00000" Font-Bold="True"></asp:label></div>
					</td>
				</tr>
				<tr>
					<td height="24"><div align="center">
							<asp:Button id="btnSubmit" runat="server" Text="注册" CssClass="button"></asp:Button></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
