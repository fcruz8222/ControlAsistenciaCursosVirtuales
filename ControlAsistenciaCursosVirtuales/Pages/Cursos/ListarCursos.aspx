<%@ Page Title="Administrar Cursos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="ListarCursos.aspx.cs"
    Inherits="ControlAsistenciaCursosVirtuales.Pages.Cursos.ListarCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card" style="max-width:1100px;">
        <h2 class="card-title">Administración de Cursos</h2>
        <div class="toolbar">

            <div class="toolbar-left">
                <asp:Button ID="btnNuevo" runat="server"
                    Text="➕ Crear Curso"
                    CssClass="btn btn-sm"
                    OnClick="btnNuevo_Click"
                    CausesValidation="false" />
            </div>

                <div class="toolbar-rigth search-group">
                    <asp:TextBox ID="txtBuscar"
                        runat="server"
                        CssClass="search"
                        placeholder="Buscar por nombre..."
                        AutoPostBack="true"
                        OnTextChanged="btnBuscar_Click" />

                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="btn btn-sm"
                        OnClick="btnBuscar_Click"
                        CausesValidation="false" />

                    <asp:Button ID="btnLimpiar"
                        runat="server"
                        Text="Limpiar"
                        CssClass="btn btn-sm btn-secondary"
                        OnClick="btnLimpiar_Click"
                        CausesValidation="false" />
                </div>

        </div>
            

        <div style="margin-top:16px;">
            <asp:GridView ID="gvCursos" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="IdCurso"
                CssClass="table"
                OnRowCommand="gvCursos_RowCommand">

                <Columns>
                    <asp:BoundField DataField="IdCurso" HeaderText="ID" />
                    <asp:BoundField DataField="TipoCurso" HeaderText="Tipo" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="MaestroNombre" HeaderText="Maestro" />
                    <asp:BoundField DataField="FechaHora" HeaderText="Fecha/Hora" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    <asp:BoundField DataField="DuracionHoras" HeaderText="Duración (h)" />
                    <asp:TemplateField HeaderText="Status">
                      <ItemTemplate>
                        <span class='badge <%# Eval("Status").ToString()=="ACTIVO" ? "activo" : "inactivo" %>'>
                          <%# Eval("Status") %>
                        </span>
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server"
                                Text="Editar"
                                CssClass="btn btn-sm btn-primary"
                                CommandName="EDITAR"
                                CommandArgument='<%# Eval("IdCurso") %>'
                                CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Label ID="lblGridMsg" runat="server" CssClass="error" />
        </div>
    </div>

 <!-- MODAL -->
<div id="cursoModal" class="modal-overlay">
    <div class="modal-box">

        <div class="modal-header">
            <span class="modal-title">
                <asp:Label ID="lblModalTitle" runat="server" Text="Curso"></asp:Label>
            </span>
            <span class="modal-close" onclick="cerrarCursoModal()">×</span>
        </div>

        <asp:HiddenField ID="hfIdCurso" runat="server" />

        <div class="field">
            <label>Tipo de Curso</label>
            <asp:DropDownList ID="ddlTipoCurso" runat="server" />
        </div>

        <div class="field">
            <label>Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" />
        </div>

        <div class="field">
            <label>Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

        <div class="field">
            <label>Maestro</label>
            <asp:DropDownList ID="ddlMaestro" runat="server" />
        </div>

        <div class="field">
            <label>Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" />
        </div>

        <div class="field">
            <label>Fecha</label>
            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" />
        </div>

        <div class="field">
            <label>Hora</label>
            <asp:TextBox ID="txtHora" runat="server" TextMode="Time" />
        </div>

        <div class="field">
            <label>Duración</label>
            <asp:TextBox ID="txtDuracion" runat="server" />
        </div>

        <asp:Label ID="lblModalMsg" runat="server" CssClass="error" />

        <div class="modal-footer">
            <div>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-sm"
                    OnClick="btnGuardar_Click" />
            </div>
            <div>
                <button type="button" class="btn btn-sm btn-secondary" onclick="cerrarCursoModal()">Cancelar</button>
            </div>
        </div>

    </div>
</div>

<script>
    function abrirCursoModal() {
        document.getElementById("cursoModal").style.display = "flex";
    }

    function cerrarCursoModal() {
        document.getElementById("cursoModal").style.display = "none";
    }
</script>

</asp:Content>