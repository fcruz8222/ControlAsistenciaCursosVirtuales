<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Login.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Auth.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
</head>
<body class="login-body">
    <form id="form1" runat="server">

        <div class="login-wrapper">
            <div class="login-box">

                <h2 class="login-title">Inicio de Sesión</h2>

                <div class="form-group">
                    <label>Usuario</label>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="input" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtUser"
                        ErrorMessage="Ingrese usuario"
                        CssClass="error"
                        Display="Dynamic" />
                </div>

                <div class="form-group">
                    <label>Contraseña</label>
                    <asp:TextBox ID="txtPass" runat="server"
                        TextMode="Password"
                        CssClass="input" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtPass"
                        ErrorMessage="Ingrese contraseña"
                        CssClass="error"
                        Display="Dynamic" />
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="error" />

                <asp:Button ID="btnLogin"
                    runat="server"
                    Text="Iniciar sesión"
                    CssClass="login-btn"
                    OnClick="btnLogin_Click" />

            </div>
        </div>

    </form>
</body>
</html>