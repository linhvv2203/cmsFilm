<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Log.aspx.cs" Inherits="SYS_User_Log" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title>VTC</title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
				<LINK href="../Common/style.css" type="text/css" rel="stylesheet">
					<script language="javascript" src="../common/common.js"></script>
					<LINK href="../common/calendar-win2k-2.css" type="text/css" rel="stylesheet">
						<script language="javascript" src="../common/common.js"></script>
						<SCRIPT src="../common/calendar.js" type="text/javascript"></SCRIPT>
						<SCRIPT src="../common/calendar-en.js" type="text/javascript"></SCRIPT>
						<script>
					function chg_menu_view(obj)
					{
						if (document.getElementById(obj).className == "menuExpanded") {
							document.getElementById(obj).className = "menuCollapsed";
							document.getElementById(obj+"_List").className = "menuListHide";
							setTimeout('document.getElementById("'+obj+'_List").className="hide";',125); 
						}
						else if (document.getElementById(obj).className == "menuCollapsed") {
							document.getElementById(obj).className = "menuExpanded";
							document.getElementById(obj+"_List").className = "menuListHide";
							setTimeout('document.getElementById("'+obj+'_List").className="menuList";',125);
						}
						mod_itm_list();
					}
					function mod_itm_list()
					{
						if (!document.all) {
						document.getElementById("td0_List").style.width = "178px";
						document.getElementById("td1_List").style.width = "178px";
						document.getElementById("td2_List").style.width = "178px";
						document.getElementById("td3_List").style.width = "178px";	 
						}
					}
					var oldLink = null;

				function selected(cal, date) {
				cal.sel.value = date; // just update the date in the input field.
				cal.callCloseHandler();
				}

				function closeHandler(cal) {
				cal.hide();                        // hide the calendar
				}

				function showCalendar(id, format) {
				var el = document.getElementById(id);
				if (calendar != null) {
					// we already have some calendar created
					calendar.hide();                 // so we hide it first.
				} else {
					// first-time call, create the calendar.
					var cal = new Calendar(false, null, selected, closeHandler);
					calendar = cal;                  // remember it in the global var
					cal.setRange(1900, 2070);        // min/max year allowed.
					cal.create();
				}
				calendar.setDateFormat(format);    // set the specified date format
				calendar.parseDate(el.value);      // try to parse the text in field
				calendar.sel = el;                 // inform it what input field we use
				calendar.showAtElement(el);        // show the calendar below it

				return false;
				}

				var MINUTE = 60 * 1000;
				var HOUR = 60 * MINUTE;
				var DAY = 24 * HOUR;
				var WEEK = 7 * DAY;

				function isDisabled(date) {
				var today = new Date();
				return (Math.abs(date.getTime() - today.getTime()) / DAY) > 10;
				}
						
                        if (document.all)
                        {     
                        document.onkeydown = function ()
                        {     var key_enter= 13; // 13 = Enter   
                              if (key_enter==event.keyCode)
                             {
                                 event.keyCode=0;
                             document.getElementById('cmdSearch').click();          
                             return false;
                             } 
                            }
                        }
					</script>
	</HEAD>
	<body  leftMargin="0" topMargin="0">
		<form id="Form1" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
			        	<tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">Thống kê log người dùng.</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						</tr>
						<tr>
							<td width="100%" bgColor="#000000" valign="middle" style="height: 1px">
							</td>
						</tr>
					<TR>
						<TD class="RadWWrapperBodyCenter" width="100%" >
							<TABLE id="ConfigTable" width="100%">
								<TR>
									<TD class="formLabel">
                                        &nbsp;<asp:TextBox ID="txtDateStart" runat="server"></asp:TextBox><input id="Button2"
                                            onclick="return showCalendar('<% =txtDateStart.ClientID %>', 'dd/mm/y');" type="button"
                                            value="[ ... ]" /><asp:TextBox ID="txtDateEnd" runat="server"></asp:TextBox><input
                                                onclick="return showCalendar('<% =txtDateEnd.ClientID %>', 'dd/mm/y');" type="button"
                                                value="[ ... ]" />
                                        Search By:
                                        <asp:dropdownlist id="ddlTypeSearch" runat="server" Width="96px"></asp:dropdownlist>
                                        Keyword:
										<asp:textbox id="txtKeyword" runat="server" Width="232px"></asp:textbox>
										<asp:button id="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Search" OnClick="cmdSearch_Click"  ></asp:button>
									</TD>
								</TR>
								<TR>
									<TD ><asp:datagrid id="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" BorderStyle="Inset"
											BorderColor="DarkGray" AllowPaging="True"  CellPadding="1" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left"
											HeaderStyle-BackColor="#99ccff" AlternatingItemStyle-BackColor="lightgray" PageSize="15">
											<SelectedItemStyle BackColor="#FFC0C0" Height="20"></SelectedItemStyle>
											<EditItemStyle></EditItemStyle>
											<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
											<ItemStyle></ItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#d2d7da"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<div align="center"><input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)"></div>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox ID="chkAllDelete" runat="server" />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False"></asp:BoundColumn>
											</Columns>
											<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<TABLE id="TABLE1" width="100%">
								<TR>
									<TD class="formLabel" height="6">
										&nbsp;<asp:label id="lblTotalRecords" runat="server" ForeColor="#004000" Font-Bold="True"></asp:label>
										</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TBODY></table>
		</form>
	</body>
</HTML>


