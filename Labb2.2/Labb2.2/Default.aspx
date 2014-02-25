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
        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button" runat="server" Text="Button" OnClick="Button_Click" />
    </div>

    <div>
        <asp:ListView ID="ListView" runat="server">
            <LayoutTemplate>
                <table runat="server" id="table">
                    <tr runat="server" id="itemPlaceholder" ></tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    </form>
    <%--↓ Här är javaScripten ↓--%>
    <script type="text/javascript" src="../Scripts/script.js" ></script>
    
</body>
</html>
