<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="_6thWebApplication_text_with_picture.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 385px;
        }
        .auto-style2 {
            width: 412px;
        }
        .auto-style3 {
            height: 170px;
        }
        .scrolling {  
                position: absolute;  
            }  
              
            .gvWidthHight {  
                overflow: scroll;  
                height: 250px;  
                width: 570px;  
            }  
        .auto-style4 {
            overflow: scroll;
            height: 250px;
            width: 794px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <fieldset style="width: 100%;">
    <legend>Save and retrieve image from database</legend>
    <table>
    <tr><td class="auto-style2">Book Name: </td><td class="auto-style1"><asp:TextBox ID="txtBookName" runat="server"></asp:TextBox>&nbsp; <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            </td>
        <td rowspan="11" valign="top"><asp:Image ID="Image1" runat="server" Width="150px" Height="150px" /></td></tr>
 
    <tr><td class="auto-style2">Author: </td><td class="auto-style1"><asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>&nbsp; <asp:Button ID="btnUpdate" runat="server" Text="Upadate" OnClick="btnUpdate_Click" />
            </td></tr>
<tr><td class="auto-style2">Publisher: </td><td class="auto-style1"><asp:TextBox ID="txtPublisher" runat="server"></asp:TextBox></td></tr>
    <tr><td class="auto-style2">Price: </td><td class="auto-style1"><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td></tr>
    <tr><td class="auto-style2">Book Picture: </td><td class="auto-style1">
        <asp:FileUpload ID="flupBookPic" runat="server" /></td></tr>
        <tr><td class="auto-style2">
            <asp:TextBox ID="txtIDdisplay" runat="server" Width="21px"></asp:TextBox>
            </td><td class="auto-style1">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            &nbsp;<asp:Button ID="btnRetrieve" runat="server" Text="Retrieve Image" Width="104px" OnClick="btnRetrieve_Click" BackColor="#CC0000" ForeColor="White" />
            </td></tr>
        <tr><td class="auto-style2">&nbsp;</td><td class="auto-style1">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>           
            &nbsp;
            <asp:TextBox ID="txtpath" runat="server"></asp:TextBox>
            </td></tr>
        <tr><td class="auto-style2">&nbsp;</td><td class="auto-style1">
            &nbsp;</td></tr>
        <tr><td colspan="2" class="auto-style3">
            <div class = "auto-style4">  
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="798px" DataKeyNames="BookId" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged2" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="BookId" HeaderText="BookId" InsertVisible="False" ReadOnly="True" SortExpression="BookId"  HeaderStyle-Width="10px"  >
<HeaderStyle Width="10px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BookName" HeaderText="BookName" SortExpression="BookName"  HeaderStyle-Width="30px" >
<HeaderStyle Width="30px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author"  HeaderStyle-Width="30px" >
<HeaderStyle Width="30px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Publisher" HeaderText="Publisher" SortExpression="Publisher"  HeaderStyle-Width="30px" >
<HeaderStyle Width="30px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price"  HeaderStyle-Width="30px" >
                  
<HeaderStyle Width="30px"></HeaderStyle>
                    </asp:BoundField>
                  
                    
                   
                    <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("BookPic") %>' Height="20px" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                   
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle CssClass="scrolling" BackColor="#7961da" Font-Bold="True" ForeColor="White" /> 
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="Gray" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />

                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:1MyTest_DBConnectionString %>" SelectCommand="SELECT * FROM [BookDetails]"></asp:SqlDataSource>

            </td></tr>
        <tr><td class="auto-style2">&nbsp;</td><td class="auto-style1">
            &nbsp;&nbsp;</td></tr>
    </table>  
    </fieldset>


        </div>
    </form>
</body>
</html>
