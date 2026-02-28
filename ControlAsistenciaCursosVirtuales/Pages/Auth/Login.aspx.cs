using System;
using System.Data;
using System.Data.SqlClient;

namespace ControlAsistenciaCursosVirtuales.Pages.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string CodUser = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            string sql = @"
                SELECT CodigoUsuario, NombreUsuario, Rol, Status
                FROM dbo.Usuario
                WHERE CodigoUsuario = @CodUser AND Contraseña = @Pass
            ";

            var dt = Db.Query(sql,
                new SqlParameter("@CodUser", CodUser),
                new SqlParameter("@Pass", pass)
            );

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                if (row["Status"].ToString() != "ACTIVO")
                {
                    lblMsg.Text = "Usuario inactivo.";
                    return;
                }

                Session["USER"] = row["CodigoUsuario"].ToString();
                Session["USER_NAME"] = row["NombreUsuario"].ToString();
                Session["ROLE"] = row["Rol"].ToString();

                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {
                lblMsg.Text = "Credenciales inválidas.";
            }
        }
    }
}