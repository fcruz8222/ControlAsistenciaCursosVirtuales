using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Cursos
{
    public partial class ListarCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null) Response.Redirect("~/Pages/Auth/Login.aspx");

            if (!IsPostBack)
            {
                CargarCombos();
                BindGrid();
            }
        }

        private void CargarCombos()
        {
            // TipoCurso
            ddlTipoCurso.Items.Clear();
            ddlTipoCurso.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlTipoCurso.Items.Add(new ListItem("Virtual", "Virtual"));
            ddlTipoCurso.Items.Add(new ListItem("Presencial", "Presencial"));
            ddlTipoCurso.Items.Add(new ListItem("Mixto", "Mixto"));

            // Status
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlStatus.Items.Add(new ListItem("ACTIVO", "ACTIVO"));
            ddlStatus.Items.Add(new ListItem("INACTIVO", "INACTIVO"));

            // Maestros
            var dtM = Db.Query("SELECT IdMaestro, Nombre FROM dbo.Maestros WHERE Status='ACTIVO' ORDER BY Nombre");
            ddlMaestro.Items.Clear();
            ddlMaestro.Items.Add(new ListItem("-- Seleccione --", ""));
            foreach (DataRow r in dtM.Rows)
                ddlMaestro.Items.Add(new ListItem(r["Nombre"].ToString(), r["IdMaestro"].ToString()));
        }

        private void BindGrid()
        {
            string sql = @"
                SELECT c.IdCurso, c.TipoCurso, c.Nombre, c.Descripcion, c.IdMaestro,
                       m.Nombre AS MaestroNombre,
                       c.Status, c.FechaHora, c.DuracionHoras
                FROM dbo.Cursos c
                INNER JOIN dbo.Maestros m ON m.IdMaestro = c.IdMaestro
                ORDER BY c.IdCurso DESC;
            ";

            gvCursos.DataSource = Db.Query(sql);
            gvCursos.DataBind();
            lblGridMsg.Text = "";
        }

        // Abrir modal
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarModal();
            lblModalTitle.Text = "Crear Curso";
            AbrirModal();
        }

        // Se cargan los datos y abre el modal para editar
        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                int idCurso = int.Parse(e.CommandArgument.ToString());
                CargarCursoEnModal(idCurso);
                lblModalTitle.Text = "Editar Curso (ID: " + idCurso + ")";
                AbrirModal();
            }
        }

        private void CargarCursoEnModal(int idCurso)
        {
            LimpiarModal();

            var dt = Db.Query("SELECT * FROM dbo.Cursos WHERE IdCurso=@id",
                new SqlParameter("@id", idCurso));

            if (dt.Rows.Count != 1)
            {
                lblGridMsg.Text = "No se encontró el curso.";
                return;
            }

            var r = dt.Rows[0];

            hfIdCurso.Value = idCurso.ToString();
            ddlTipoCurso.SelectedValue = r["TipoCurso"].ToString();
            ddlStatus.SelectedValue = r["Status"].ToString();
            txtNombre.Text = r["Nombre"].ToString();
            txtDescripcion.Text = r["Descripcion"] == DBNull.Value ? "" : r["Descripcion"].ToString();
            ddlMaestro.SelectedValue = r["IdMaestro"].ToString();

            DateTime fh = (DateTime)r["FechaHora"];
            txtFecha.Text = fh.ToString("yyyy-MM-dd");
            txtHora.Text = fh.ToString("HH:mm");
            txtDuracion.Text = Convert.ToDecimal(r["DuracionHoras"]).ToString(CultureInfo.InvariantCulture);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                AbrirModal(); //Mantiene modal abierto
                return;
            }

            lblModalMsg.Text = "";

            DateTime fechaHora;
            if (!TryBuildFechaHora(txtFecha.Text, txtHora.Text, out fechaHora))
            {
                lblModalMsg.Text = "Fecha/Hora inválida.";
                AbrirModal();
                return;
            }

            decimal dur;
            if (!decimal.TryParse(txtDuracion.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out dur) &&
                !decimal.TryParse(txtDuracion.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out dur))
            {
                lblModalMsg.Text = "Duración inválida (ej: 2 o 1.5).";
                AbrirModal();
                return;
            }

            bool esEditar = !string.IsNullOrWhiteSpace(hfIdCurso.Value);
            if (esEditar)
            {
                string sql = @"
                    UPDATE dbo.Cursos
                    SET TipoCurso=@Tipo, Nombre=@Nombre, Descripcion=@Desc, IdMaestro=@IdMaestro,
                        Status=@Status, FechaHora=@FechaHora, DuracionHoras=@Dur
                    WHERE IdCurso=@IdCurso;
                ";

                Db.Execute(sql,
                    new SqlParameter("@Tipo", ddlTipoCurso.SelectedValue),
                    new SqlParameter("@Nombre", txtNombre.Text.Trim()),
                    new SqlParameter("@Desc", (object)txtDescripcion.Text.Trim() ?? DBNull.Value),
                    new SqlParameter("@IdMaestro", int.Parse(ddlMaestro.SelectedValue)),
                    new SqlParameter("@Status", ddlStatus.SelectedValue),
                    new SqlParameter("@FechaHora", fechaHora),
                    new SqlParameter("@Dur", dur),
                    new SqlParameter("@IdCurso", int.Parse(hfIdCurso.Value))
                );
            }
            else
            {
                string sql = @"
                    INSERT INTO dbo.Cursos (TipoCurso, Nombre, Descripcion, IdMaestro, Status, FechaHora, DuracionHoras)
                    VALUES (@Tipo, @Nombre, @Desc, @IdMaestro, @Status, @FechaHora, @Dur);
                ";

                Db.Execute(sql,
                    new SqlParameter("@Tipo", ddlTipoCurso.SelectedValue),
                    new SqlParameter("@Nombre", txtNombre.Text.Trim()),
                    new SqlParameter("@Desc", (object)txtDescripcion.Text.Trim() ?? DBNull.Value),
                    new SqlParameter("@IdMaestro", int.Parse(ddlMaestro.SelectedValue)),
                    new SqlParameter("@Status", ddlStatus.SelectedValue),
                    new SqlParameter("@FechaHora", fechaHora),
                    new SqlParameter("@Dur", dur)
                );
            }

            BindGrid();
            LimpiarModal();
            
        }

        private void LimpiarModal()
        {
            hfIdCurso.Value = "";
            ddlTipoCurso.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlMaestro.SelectedIndex = 0;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtFecha.Text = "";
            txtHora.Text = "";
            txtDuracion.Text = "";
            lblModalMsg.Text = "";
        }

        private void AbrirModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
             "abrirModal", "abrirCursoModal();", true);
        }

        private bool TryBuildFechaHora(string fecha, string hora, out DateTime result)
        {
            result = DateTime.MinValue;
            if (string.IsNullOrWhiteSpace(fecha) || string.IsNullOrWhiteSpace(hora)) return false;

            var s = fecha.Trim() + " " + hora.Trim();
            return DateTime.TryParseExact(s, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BindGrid(txtBuscar.Text.Trim());
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            BindGrid();
        }

        private void BindGrid(string filtro = "")
        {
            string sql = @"
        SELECT c.IdCurso, c.TipoCurso, c.Nombre, c.Descripcion,
               m.Nombre AS MaestroNombre,
               c.Status, c.FechaHora, c.DuracionHoras
        FROM dbo.Cursos c
        INNER JOIN dbo.Maestros m ON m.IdMaestro = c.IdMaestro
        WHERE (@filtro='' OR c.Nombre LIKE '%' + @filtro + '%')
        ORDER BY c.IdCurso DESC;
    ";

            gvCursos.DataSource = Db.Query(sql, new SqlParameter("@filtro", filtro));
            gvCursos.DataBind();
        }
    }
}