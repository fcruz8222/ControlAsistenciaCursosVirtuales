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
            string conexion = "Data Source=DESKTOP-79POPTI\\SQLEXPRESS;Initial Catalog=ControlAsistenciaCursosVirtuales;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "SELECT IdMaestro, Nombre, Area, Status FROM Maestros WHERE Status = 'Activo'";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvMaestrosActivos.DataSource = dt;
                gvMaestrosActivos.DataBind();
            }
        }
        // se creo el metodo para cargar y poder modificar o eliminar los maestros activos
        private void CargarMaestrosEditar()
        {
            string conexion = "Data Source=DESKTOP-79POPTI\\SQLEXPRESS;Initial Catalog=ControlAsistenciaCursosVirtuales;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "SELECT IdMaestro, Nombre, Area, Status FROM Maestros";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvMaestrosEditar.DataSource = dt;
                gvMaestrosEditar.DataBind();
            }
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

            string nombre = ((TextBox)row.Cells[1].Controls[0]).Text;
            string area = ((TextBox)row.Cells[2].Controls[0]).Text;
            string status = ((TextBox)row.Cells[3].Controls[0]).Text;

            string conexion = "Data Source=DESKTOP-79POPTI\\SQLEXPRESS;Initial Catalog=ControlAsistenciaCursosVirtuales;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "UPDATE Maestros SET Nombre=@Nombre, Area=@Area, Status=@Status WHERE IdMaestro=@IdMaestro";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Area", area);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@IdMaestro", idMaestro);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvMaestrosEditar.EditIndex = -1;
            CargarMaestrosEditar();
        }

        protected void gvMaestrosEditar_RowDeleting(object sender, GridViewDeleteEventArgs e)
            //este evento elimina cuando selecionamos un registro y le damos eliminar
        {
            int idMaestro = Convert.ToInt32(gvMaestrosEditar.DataKeys[e.RowIndex].Value);

            string conexion = "Data Source=DESKTOP-79POPTI\\SQLEXPRESS;Initial Catalog=ControlAsistenciaCursosVirtuales;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "DELETE FROM Maestros WHERE IdMaestro=@IdMaestro";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@IdMaestro", idMaestro);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            CargarMaestrosEditar();
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
            string conexion = "Data Source=DESKTOP-79POPTI\\SQLEXPRESS;Initial Catalog=ControlAsistenciaCursosVirtuales;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(conexion))
            {
                string query = "INSERT INTO Maestros (Nombre, Area, Status) VALUES (@Nombre, @Area, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Area", txtArea.Text);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtNombre.Text = "";
            txtArea.Text = "";
            ddlStatus.SelectedIndex = 0;
        }

        
            
        protected void gvMaestrosEditar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}