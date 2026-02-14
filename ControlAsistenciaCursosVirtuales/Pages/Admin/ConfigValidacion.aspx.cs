using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Admin
{
    public partial class ConfigValidacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null) Response.Redirect("~/Pages/Auth/Login.aspx");

            if (!IsPostBack)
            {
                txtIpPermitida.Text = (Session["IP_PERMITIDA"] as string) ?? "";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            Session["IP_PERMITIDA"] = txtIpPermitida.Text.Trim();
            lblMsg.Text = "✅ IP permitida guardada en sesión: " + Session["IP_PERMITIDA"];
        }
    }
}
