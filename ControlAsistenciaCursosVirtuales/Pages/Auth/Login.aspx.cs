using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // solo para poder validar el login, aun no hemos terminado la base de datos
            if (txtUser.Text.Trim().ToLower() == "admin" && txtPass.Text == "1234")
            {
                Session["USER"] = "admin";
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
            {
                lblMsg.Text = "Credenciales inválidas";
            }
        }
    }
}
