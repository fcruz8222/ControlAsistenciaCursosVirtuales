using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistenciaCursosVirtuales.Pages.Maestros
{
    
    public partial class RegistroMaestros : System.Web.UI.Page
    {
        //este codigo es para mantener ocultatos paeles 
        private void OcultarPaneles()
        {
            pnlNuevo.Visible = false;
            pnlLista.Visible = false;
            pnlEditarEliminar.Visible = false;

        }
        //se creoel metodo para cargar los maestros activos en el panel maetsros
        private void CargarMaestrosActivos()
        {
            string query = "SELECT IdMaestro, Nombre, Area, Status FROM Maestros WHERE Status = 'Activo'";

            DataTable dt = Db.Query(query);

            gvMaestrosActivos.DataSource = dt;
            gvMaestrosActivos.DataBind();
        }
        // se creo el metodo para cargar y poder modificar o eliminar los maestros activos
        private void CargarMaestrosEditar()
        {
            string query = "SELECT IdMaestro, Nombre, Area, Status FROM Maestros";

            DataTable dt = Db.Query(query);

            gvMaestrosEditar.DataSource = dt;
            gvMaestrosEditar.DataBind();
        }
        //metodo para editar registros se activa al dar click en editar
        protected void gvMaestrosEditar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMaestrosEditar.EditIndex = e.NewEditIndex;
            CargarMaestrosEditar();
        }
        // este evento canncela la modificacion de editar algun registro
        protected void gvMaestrosEditar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMaestrosEditar.EditIndex = -1;
            CargarMaestrosEditar();
        }


        protected void gvMaestrosEditar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        // este evento guarda los cambios cuando se modifico algun registro


        {
            int idMaestro = Convert.ToInt32(gvMaestrosEditar.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvMaestrosEditar.Rows[e.RowIndex];

            string nombre = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string area = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string status = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();

            string query = @"UPDATE Maestros
                     SET Nombre = @Nombre,
                         Area = @Area,
                         Status = @Status
                     WHERE IdMaestro = @IdMaestro";

            Db.Execute(query,
                new SqlParameter("@Nombre", nombre),
                new SqlParameter("@Area", area),
                new SqlParameter("@Status", status),
                new SqlParameter("@IdMaestro", idMaestro)
            );

            gvMaestrosEditar.EditIndex = -1;
            CargarMaestrosEditar();
            CargarMaestrosActivos();
        }
       

        protected void gvMaestrosEditar_RowDeleting(object sender, GridViewDeleteEventArgs e)
       
            
            //este evento elimina cuando selecionamos un registro y le damos eliminar

        {

            int idMaestro = Convert.ToInt32(gvMaestrosEditar.DataKeys[e.RowIndex].Value);

            string query = "DELETE FROM Maestros WHERE IdMaestro = @IdMaestro";

            Db.Execute(query,
                new SqlParameter("@IdMaestro", idMaestro)
            );

            CargarMaestrosEditar();
            CargarMaestrosActivos();
        }
      //se agrego este m,etodo de doble validacion para confirmar la eliminacion de un registro 
        protected void gvMaestrosEditar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (Control control in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
                {
                    LinkButton btn = control as LinkButton;

                    if (btn != null && btn.CommandName == "Delete")
                    {
                        btn.OnClientClick = "return confirm('¿Realmente deseas eliminar este maestro?');";
                    }
                }
            }
        }
        //**************************************
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnNuevoMaestro_Click(object sender, EventArgs e)
        //se creo este evento en en boton nuevo mastro para hacer vicible el formulaio crear nuevo maestro
          
        {
            OcultarPaneles();
            pnlNuevo.Visible = true;
        }

        protected void btnListaMaestros_Click(object sender, EventArgs e)
        //se creo este evento en en boton lista maestros para mostrar el listado de maestros
        {
            OcultarPaneles();
            pnlLista.Visible = true;
            CargarMaestrosActivos();
        }

        protected void btnModificarEliminar_Click(object sender, EventArgs e)
        //se creo este evento en en boton modificar o eliminar para al dar click muestre los maestros activos y se puiedan eliminar o modificar
        {
            OcultarPaneles();
            pnlEditarEliminar.Visible = true;
            CargarMaestrosEditar();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Maestros (Nombre, Area, Status) VALUES (@Nombre, @Area, @Status)";

            Db.Execute(query,
                new SqlParameter("@Nombre", txtNombre.Text.Trim()),
                new SqlParameter("@Area", txtArea.Text.Trim()),
                new SqlParameter("@Status", ddlStatus.SelectedValue)
            );

            txtNombre.Text = "";
            txtArea.Text = "";
            ddlStatus.SelectedIndex = 0;

            CargarMaestrosActivos();
            CargarMaestrosEditar();
        }



        protected void gvMaestrosEditar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}