<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroMaestros.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Maestros.RegistroMaestros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2 class="card-title text-center">Administración de Maestros</h2>
    <br />

<div style="text-align:center; margin-bottom:30px;">

    <div style="margin:8px;">
        <asp:Button ID="btnNuevoMaestro" runat="server"
        Text="Nuevo Maestro"
        CssClass="btn btn-success"
        OnClick="btnNuevoMaestro_Click"
        Width="350px" />
    </div>

    <div style="margin:8px;">
        <asp:Button ID="btnListaMaestros" runat="server"
        Text="Lista de Maestros"
        CssClass="btn btn-primary"
        OnClick="btnListaMaestros_Click"
        Width="350px" />
    </div>

    <div style="margin:8px;">
        <asp:Button ID="btnModificarEliminar" runat="server"
        Text="Modificar y Eliminar"
        CssClass="btn btn-secondary"
        OnClick="btnModificarEliminar_Click"
        Width="350px" />
    </div>

</div>
    <br>
    <asp:Panel ID="pnlNuevo" runat="server" Visible="false">

    <h2 style="font-weight:bold; font-size:26px;">Registro de Maestros</h2>

    <table style="width: 450px;">
        <tr>
            <td style="width:150px; padding:8px;">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td style="padding:8px;">
                <asp:TextBox ID="txtNombre" runat="server" Width="220px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="padding:8px;">
                <asp:Label ID="lblArea" runat="server" Text="Área"></asp:Label>
            </td>
            <td style="padding:8px;">
                <asp:TextBox ID="txtArea" runat="server" Width="220px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="padding:8px;">
                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            </td>
            <td style="padding:8px;">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem>Activo</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td></td>
            <td style="padding:8px;">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" Width="226px" />
                <br /><br />
            </td>
        </tr>
    </table>

</asp:Panel>
    <asp:Panel ID="pnlLista" runat="server" Visible="false">
        <h3>Lista de Maestros Activos</h3>
        <asp:GridView ID="gvMaestrosActivos" runat="server" AutoGenerateColumns="false"
            CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="IdMaestro" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Area" HeaderText="Área" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlEditarEliminar" runat="server" Visible="false">
        <h3>Modificar y Eliminar Maestros</h3>
        <asp:GridView ID="gvMaestrosEditar" runat="server" AutoGenerateColumns="false"
    DataKeyNames="IdMaestro"
    OnRowEditing="gvMaestrosEditar_RowEditing"
    OnRowCancelingEdit="gvMaestrosEditar_RowCancelingEdit"
    OnRowUpdating="gvMaestrosEditar_RowUpdating"
    OnRowDeleting="gvMaestrosEditar_RowDeleting"
    OnRowDataBound="gvMaestrosEditar_RowDataBound"
    CssClass="table table-bordered"
    OnSelectedIndexChanged="gvMaestrosEditar_SelectedIndexChanged">

            <Columns>
                <asp:BoundField DataField="IdMaestro" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Area" HeaderText="Área" />
                <asp:BoundField DataField="Status" HeaderText="Status" />

                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

</asp:Content>