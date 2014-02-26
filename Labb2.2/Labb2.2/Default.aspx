<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Labb2._2.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Labb2.2</title>
    <meta charset="utf-8" />
    <link rel="stylesheet" href="~/Css\style.css" />
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <asp:ListView ID="ListView" runat="server" 
            ItemType="Labb2._2.Model.Contact"  
            SelectMethod="ListView_GetData" 
            InsertMethod="ListView_InsertItem" 
            UpdateMethod="ListView_UpdateItem" 
            DataKeyNames="ContactID" 
            InsertItemPosition="FirstItem"> <%--Denna krävs för att Insertkontrollen ska sättas ut... --%>
            <LayoutTemplate>
                <table runat="server" id="table">
                    <tr>
                        <th>
                              Firstname
                        </th>
                        <th>
                              Lastname
                        </th>
                        <th>
                              Mail
                        </th>
                    </tr>
                        <tr id="itemPlaceholder" runat="server">                           
                    </tr>                                             
                </table>   
                <asp:DataPager ID="DataPager" runat="server" PageSize="15" >
                    <Fields>
                        <asp:NextPreviousPagerField FirstPageText="<<" ShowFirstPageButton="true" ShowNextPageButton="false" ShowPreviousPageButton="false"/>
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField LastPageText=">>" ShowLastPageButton="true" ShowFirstPageButton="false" ShowPreviousPageButton="false" />
                    </Fields>
                </asp:DataPager>                 
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#: Item.FirstName %></>
                    </td>
                    <td>
                        <%#: Item.LastName %></>
                    </td>
                    <td>
                        <%#: Item.EmailAddress %>
                    </td> 
                    <td>
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CausesValidation="false" OnClientClick='<%# String.Format("return confirm(\"Är du säker på att du vill radera?\")") %>'/>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Edit" CausesValidation="false" />
                    </td>  
                </tr>                                 
            </ItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="FirstName" Text='<%# BindItem.FirstName %>' />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="LastName" Text='<%#: BindItem.LastName %>' />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="EmailAddress" Text='<%#: BindItem.EmailAddress %>' />
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" />
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                    </td>
                </tr>                
            </InsertItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="FirstName" Text='<%#: BindItem.FirstName %>' />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="LastName" Text='<%#: BindItem.LastName %>' />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="EmailAddress" Text='<%#: BindItem.EmailAddress %>' />
                    </td>
                    <td>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Spara" />
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                    </td>
                </tr>  
            </EditItemTemplate>
        </asp:ListView>
    </div>
    </form>
    <%--↓ Här är javaScripten ↓--%>
    <script type="text/javascript" src="../Scripts/script.js" ></script>
    
</body>
</html>
