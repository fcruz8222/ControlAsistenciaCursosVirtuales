using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Asistencia
{
    public partial class Registrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null) Response.Redirect("~/Pages/Auth/Login.aspx");

            if (!IsPostBack)
            {
                ddlCurso.Items.Clear();
                ddlCurso.Items.Add(new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
                ddlCurso.Items.Add(new System.Web.UI.WebControls.ListItem("Curso Excel Básico", "EXCEL"));
                ddlCurso.Items.Add(new System.Web.UI.WebControls.ListItem("Curso Programación C#", "CSHARP"));
                ddlCurso.Items.Add(new System.Web.UI.WebControls.ListItem("Curso Redes", "REDES"));

                txtIp.Text = GetClientIp();
            }
        }

        private string GetClientIp()
        {
 
            return Request.UserHostAddress ?? "";
        }

        protected void cvIp_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            var ipPermitida = Session["IP_PERMITIDA"] as string;

            if (string.IsNullOrWhiteSpace(ipPermitida))
            {
                args.IsValid = true;
                return;
            }

            args.IsValid = string.Equals(txtIp.Text.Trim(), ipPermitida.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            lblOk.Text = "✅ Asistencia registrada para: " + txtNombre.Text.Trim() +
                         " | Curso: " + ddlCurso.SelectedItem.Text +
                         " | IP: " + txtIp.Text.Trim();
        }
    }
}
