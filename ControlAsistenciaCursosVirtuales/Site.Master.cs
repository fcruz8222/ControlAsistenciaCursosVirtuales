using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ControlAsistenciaCursosVirtuales
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["USER"] as string;

            if (!string.IsNullOrEmpty(user))
            {
                lblUser.Text = "Usuario: " + user;
                btnLogout.Visible = true;
            }
            else
            {
                lblUser.Text = "No autenticado";
                btnLogout.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Auth/Login.aspx");
        }
    }
}
