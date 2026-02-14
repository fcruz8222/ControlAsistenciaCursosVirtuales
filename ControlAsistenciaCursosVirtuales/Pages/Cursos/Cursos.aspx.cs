using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ControlAsistenciaCursosVirtuales.Pages.Cursos
{
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null) Response.Redirect("~/Pages/Auth/Login.aspx");

            if (!IsPostBack)
            {
                var dt = new DataTable();
                dt.Columns.Add("Codigo");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Instructor");
                dt.Columns.Add("Activo");

                dt.Rows.Add("EXCEL", "Excel Básico", "Juan Pérez", "Sí");
                dt.Rows.Add("CSHARP", "Programación C#", "María López", "Sí");
                dt.Rows.Add("REDES", "Redes", "Carlos Díaz", "No");

                gvCursos.DataSource = dt;
                gvCursos.DataBind();
            }
        }
    }
}
