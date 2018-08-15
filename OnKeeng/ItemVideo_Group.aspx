<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemVideo_Group.aspx.cs" Inherits="CLIP_ItemVideo_Group" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../common/common.js"></script>
			<script language="JavaScript">

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


function SelectAlldgrTemp(CheckBoxControl) {

    if (CheckBoxControl.checked == true) {
        
        var i;
        
        for (i=0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrTemp') > -1)) {
                document.forms[0].elements[i].checked = true;
            }
        }
    } 
    
    else {
        
        var i;
        
        for (i=0; i < document.forms[0].elements.length; i++) {
            
            if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrTemp') > -1)) {
                
                document.forms[0].elements[i].checked = false;
            }
        }
    }
}

		</script>
					
		<style type="text/css">
        .cssPager span
        {
	        background-color:Yellow;	
        }
		    .style4
            {
                font-family: Arial, Verdana, Tahoma, Sans-Serif;
                font-size: 13px;
                font-weight: bold;
                color: #000000;
                cursor: default;
                width: 880px;
            }
		</style>
		<LINK href="../common/calendar-win2k-2.css" type="text/css" rel="stylesheet">
		<SCRIPT src="../common/calendar.js" type="text/javascript"></SCRIPT>
		<SCRIPT src="../common/calendar-en.js" type="text/javascript"></SCRIPT>
		<script language="javascript" src="../tip/script.js" type="text/javascript"></script>
        <script language="javascript" src="../tip/javascript.js" type="text/javascript"></script>
        <script language="javascript" src="../tip/tooltips.js" type="text/javascript"></script>

	</HEAD>
	<body class="BODY" leftMargin="3" topMargin="1">
		<form id="Form1" runat="server">
			
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TBODY>
						<tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH VIDEO GROUPS</asp:label>
                                &nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						</tr>
						<tr>
							<td width="100%" bgColor="#000000" valign="middle" height="1">
							</td>
						</tr>
						<TR>
							<TD class="RadWWrapperBodyCenter" width="100%">
								<TABLE id="ConfigTable" width="100%">
								    
								    
									<TR>
									
										<TD class="style4" height="26">
										    
										    Tên Group:<asp:DropDownList ID="ddlItemgroup" runat="server" Width="254px" 
                                            CssClass="select_style" AutoPostBack=true 
                                            onselectedindexchanged="ddlItemgroup_SelectedIndexChanged" Height="20px">
                                        </asp:DropDownList>
										    
										
                                            Search: <asp:dropdownlist id="ddlgroups" runat="server" 
                                                CssClass="ComboBoxInputHover_ClassicBlue" Width="90px" 
                                                onselectedindexchanged="ddlgroups_SelectedIndexChanged" ></asp:dropdownlist>
                                            Keyword:
											<asp:textbox id="txtKeyword" runat="server" Width="159px"></asp:textbox>&nbsp;<asp:button 
                                                id="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Search" 
                                                Font-Bold="True" OnClick="cmdSearch_Click1" Height="20px" ></asp:button>
                                           </TD>
                                           <td>
                                               <center><asp:Label ID="lbError" runat="server" Text=""></asp:Label></center>
                                               </td>
                                    </tr>
                                  
                                   
								
                                       
								 </TABLE>
								 <table id="TABLE1" width="100%">
                        <tr>
                            <td>
                                <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                                    HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                                    CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                                    PagerStyle-PageButtonCount="50" PageSize="10" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                                    AutoGenerateColumns="False" >
                                    <PagerStyle CssClass="cssPager" />
                                    <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                                    <EditItemStyle></EditItemStyle>
                                    <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                                    <ItemStyle></ItemStyle>
                                    <HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#d2d7da">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <div align="center">
                                                    <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)"></div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAllDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Ảnh</HeaderTemplate>
                                            <ItemTemplate>
                                                <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>\'  >')"
                                                    onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>"
                                                    height="20px" width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    ID
                                                </p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Tên
                                                </p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                      <%--  <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Chuyên Mục</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ChuyenMuc").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        
                                       <%--  <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Tiểu Mục</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NameSubsection").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        
                                       <%-- <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Kênh</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NameChannel").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <%--<asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    IsView</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsView").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <%--<asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    IsHot</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsHot").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <%--<asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    IsHome</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsHome").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                               <p>Thời gian nhập bài</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "DatePublish").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    UserLogin</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserLogin").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Sửa</HeaderTemplate>
                                            <ItemTemplate>
                                                <a href="ItemVideo_data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemStatus").ToString())%>">
                                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%>"
                                                        src="../images/Edit.gif" height="20px" border="0" width="20px" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn>
                                        <HeaderTemplate>Xem</HeaderTemplate>
                                            <ItemTemplate>
                                            <center>
                                        <a onclick="window.open('<%=Server.HtmlEncode(VatLid.Variables.sWebRoot.ToString()) %>OnPhim/PlayDigital_Group.aspx?id_group=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id_group").ToString())%>&id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%>  ','','top=0,left=0,height=1000,width=1000,scrollbars=yes,toolbar=yes')">
                                            <Img  src="../images/button-xem.gif"  />
                                        </a>
                                        </center>
                                        </ItemTemplate>
                                        </asp:TemplateColumn>
                                         
                                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
								 
								
		<asp:label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:label>	
		<TABLE id="TABLE2" width="100%">
									<TR>
										<TD style="height: 26px">
										
								<asp:dropdownlist id="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue" AutoPostBack="True"
Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" ></asp:dropdownlist> 	
                                            &nbsp;<asp:button id="cmdAdd" runat="server" CssClass="formSubmitBtn" 
                                                Font-Bold="True" Text="Add" OnClick="cmdAdd_Click" Height="20px"   ></asp:button>
                                            <asp:button id="cmdDelete" runat="server" CssClass="formSubmitBtn" 
                                                Text="Delete" Font-Bold="True" OnClick="cmdDelete_Click" 
                                                Height="20px" Visible=true   ></asp:button>
                                            <asp:button id="cmdSetShowHome" runat="server" CssClass="formSubmitBtn" 
                                                Text="Publish" Font-Bold="True" OnClick="cmdSetShowHome_Click"  
                                                Height="20px"  ></asp:button><asp:button id="cmdRemove" runat="server" 
                                                CssClass="formSubmitBtn" Text="UnPublish" Font-Bold="True" OnClick="cmdRemove_Click" 
                                                Height="20px" ></asp:button>
                                           
                                            &nbsp;&nbsp;
                                           
                                            </TD>
									</TR>                                                                                                  
								</TABLE>
								
								<asp:HiddenField runat="server" ID="hiddenToken"/>
							</TD>
						</TR>
						
					</TBODY></table>	
			</form>					
	                         
	</body>
</HTML>


