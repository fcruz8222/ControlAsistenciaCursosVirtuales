<%@ Page Title="Administrar Usuarios" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs"
    Inherits="ControlAsistenciaCursosVirtuales.Pages.Usuarios.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="card" style="max-width:900px;">
    
    <h2 class="card-title">Administración de Usuarios</h2>

    <div class="toolbar">

        <div class="toolbar-left">
            <asp:Button ID="btnNuevo"
                runat="server"
                Text="➕ Crear Usuario"
                CssClass="btn btn-sm"
                OnClick="btnNuevo_Click"
                CausesValidation="false"/>
        </div>

    </div>

    <div style="margin-top:16px;">

        <asp:GridView ID="gvUsuarios"
            runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="CodigoUsuario"
            CssClass="table"
            OnRowCommand="gvUsuarios_RowCommand">

            <Columns>

                <asp:BoundField DataField="CodigoUsuario" HeaderText="ID"/>

                <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario"/>

                <asp:BoundField DataField="Rol" HeaderText="Rol"/>

                <asp:TemplateField HeaderText="Activo">
                    <ItemTemplate>
                        <span class='badge <%# Eval("Status").ToString()=="ACTIVO" ? "activo" : "inactivo" %>'>
                             <%# Eval("Status") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnEditar"
                            runat="server"
                            Text="Editar"
                            CssClass="btn btn-sm btn-primary"
                            CommandName="EDITAR"
                            CommandArgument='<%# Eval("CodigoUsuario") %>'
                            CausesValidation="false"/>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <asp:Label ID="lblGridMsg" runat="server" CssClass="error" Visible="false"/>

    </div>

</div>


<!-- MODAL -->

<div id="usuarioModal" class="modal-overlay">

<div class="modal-box">

    <div class="modal-header">

        <span class="modal-title">
            <asp:Label ID="lblModalTitle" runat="server" Text="Usuario"></asp:Label>
        </span>

        <span class="modal-close" onclick="cerrarUsuarioModal()">×</span>

    </div>

    <asp:HiddenField ID="hfUsuarioID" runat="server"/>

    <div class="field">
        <label>Usuario</label>
        <asp:TextBox ID="txtUsuario" runat="server"/>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtUsuario"
                        ErrorMessage="Ingrese Usuario"
                        CssClass="error"
                        Display="Dynamic" />
    </div>

    <div class="field">
        <label>Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server"/>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtNombre"
                        ErrorMessage="Ingrese Nombre"
                        CssClass="error"
                        Display="Dynamic" />
    </div>

    <div class="field">
        <label>Contraseña</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtNombre"
                        ErrorMessage="Ingrese Contraseña"
                        CssClass="error"
                        Display="Dynamic" />
    </div>

    <div class="field">
        <label>Rol</label>
        <asp:DropDownList ID="ddlRol" runat="server">
            <asp:ListItem Value="Maestro">Maestro</asp:ListItem>
            <asp:ListItem Value="Estudiante">Estudiante</asp:ListItem>
            <asp:ListItem Value="Admin">Admin</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="field">
        <label>Activo</label>
        <asp:CheckBox ID="chkActivo" runat="server"/>
    </div>

    <asp:Label ID="lblModalMsg" runat="server" CssClass="error" Visible="false"/>

    <div class="modal-footer">

        <div>
            <asp:Button ID="btnGuardar"
                runat="server"
                Text="Guardar"
                CssClass="btn btn-sm"
                OnClick="btnGuardar_Click"/>
        </div>

        <div>
            <button type="button"
                class="btn btn-sm btn-secondary"
                onclick="cerrarUsuarioModal()">
                Cancelar
            </button>
        </div>

    </div>

</div>
</div>


<script>

function abrirUsuarioModal(){
document.getElementById("usuarioModal").style.display="flex";
}

function cerrarUsuarioModal(){
document.getElementById("usuarioModal").style.display="none";
}

</script>

</asp:Content>