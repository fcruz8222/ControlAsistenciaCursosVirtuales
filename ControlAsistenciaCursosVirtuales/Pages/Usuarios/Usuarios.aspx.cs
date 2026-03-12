using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Usuarios
{
    public partial class Usuarios : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null)
                Response.Redirect("~/Pages/Auth/Login.aspx");

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string sql = @"
                SELECT CodigoUsuario, NombreUsuario, Rol, Status
                FROM dbo.Usuario
                ORDER BY CodigoUsuario DESC
            ";

            gvUsuarios.DataSource = Db.Query(sql);
            gvUsuarios.DataBind();
            lblGridMsg.Text = "";
        }

        // Crear usuario
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarModal();
            lblModalTitle.Text = "Crear Usuario";
            AbrirModal();
        }

        // Acciones del Grid
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                string idUsuario = e.CommandArgument.ToString();

                CargarUsuario(idUsuario);

                lblModalTitle.Text = "Editar Usuario (ID: " + idUsuario + ")";
                AbrirModal();
            }
        }

        private void CargarUsuario(string idUsuario)
        {
            LimpiarModal();

            var dt = Db.Query("SELECT * FROM dbo.Usuario WHERE CodigoUsuario=@id",
                new SqlParameter("@id", idUsuario));

            if (dt.Rows.Count != 1)
            {
                lblGridMsg.Text = "No se encontró el usuario.";
                return;
            }

            var r = dt.Rows[0];

            hfUsuarioID.Value = r["CodigoUsuario"].ToString();
            txtUsuario.Text = r["CodigoUsuario"].ToString();
            txtNombre.Text = r["NombreUsuario"].ToString();
            txtPassword.Text = r["Contraseña"].ToString();
            ddlRol.SelectedValue = r["Rol"].ToString();
            //chkActivo.Checked = Convert.ToBoolean(r["Status"]);
            chkActivo.Checked = r["Status"].ToString() == "ACTIVO";
        }

        // Guardar
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                AbrirModal();
                return;
            }

            try
            {
                bool esEditar = !string.IsNullOrWhiteSpace(hfUsuarioID.Value);
                string passwordNueva = txtPassword.Text.Trim();

                if (esEditar)
                {
                    if (string.IsNullOrWhiteSpace(passwordNueva))
                    {
                        string sql = @"
                    UPDATE dbo.Usuario
                    SET CodigoUsuario = @CodigoUsuarioNuevo,
                        NombreUsuario = @NombreUsuario,
                        Rol = @Rol,
                        Status = @Activo
                    WHERE CodigoUsuario = @CodigoUsuarioOriginal";

                        Db.Execute(sql,
                            new SqlParameter("@CodigoUsuarioNuevo", txtUsuario.Text.Trim()),
                            new SqlParameter("@NombreUsuario", txtNombre.Text.Trim()),
                            new SqlParameter("@Rol", ddlRol.SelectedValue),
                            new SqlParameter("@Activo", chkActivo.Checked ? "ACTIVO" : "INACTIVO"),
                            new SqlParameter("@CodigoUsuarioOriginal", hfUsuarioID.Value)
                        );
                    }
                    else
                    {
                        string sql = @"
                    UPDATE dbo.Usuario
                    SET CodigoUsuario = @CodigoUsuarioNuevo,
                        NombreUsuario = @NombreUsuario,
                        Contraseña = @Contraseña,
                        Rol = @Rol,
                        Status = @Activo
                    WHERE CodigoUsuario = @CodigoUsuarioOriginal";

                        Db.Execute(sql,
                            new SqlParameter("@CodigoUsuarioNuevo", txtUsuario.Text.Trim()),
                            new SqlParameter("@NombreUsuario", txtNombre.Text.Trim()),
                            new SqlParameter("@Contraseña", txtPassword.Text.Trim()),
                            new SqlParameter("@Rol", ddlRol.SelectedValue),
                            new SqlParameter("@Activo", chkActivo.Checked ? "ACTIVO" : "INACTIVO"),
                            new SqlParameter("@CodigoUsuarioOriginal", hfUsuarioID.Value)
                        );
                    }

                    BindGrid();
                    LimpiarModal();

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "ok", "alert('Usuario actualizado correctamente');", true);
                }
                else
                {
                    string sql = @"
                INSERT INTO dbo.Usuario (CodigoUsuario, NombreUsuario, Contraseña, Rol, Status)
                VALUES (@CodigoUsuario, @NombreUsuario, @Contraseña, @Rol, @Activo)";

                    Db.Execute(sql,
                        new SqlParameter("@CodigoUsuario", txtUsuario.Text.Trim()),
                        new SqlParameter("@NombreUsuario", txtNombre.Text.Trim()),
                        new SqlParameter("@Contraseña", txtPassword.Text.Trim()),
                        new SqlParameter("@Rol", ddlRol.SelectedValue),
                        new SqlParameter("@Activo", chkActivo.Checked ? "ACTIVO" : "INACTIVO")
                    );

                    BindGrid();
                    LimpiarModal();

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "ok", "alert('Usuario creado correctamente');", true);
                }
            }
            catch (Exception ex)
            {
                AbrirModal();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "err", "alert('Ocurrió un error al guardar: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private void LimpiarModal()
        {
            hfUsuarioID.Value = "";
            txtUsuario.Text = "";
            txtNombre.Text = "";
            txtPassword.Text = "";
            ddlRol.SelectedIndex = 0;
            chkActivo.Checked = true;
            lblModalMsg.Text = "";
        }

        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
             "abrirModal", "abrirUsuarioModal();", true);
        }

    }
}