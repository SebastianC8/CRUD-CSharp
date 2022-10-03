using Empleados.Datos;
using Empleados.Modelo;
using System.Data;

namespace Empleados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "") {
                txtDocumento.Focus();
                MessageBox.Show("Debe ingresar un documento válido.");
            } else if (txtNombres.Text.Length < 5) {
                txtNombres.Focus();
                MessageBox.Show("Debe ingresar un nombre más largo.");
            } else {

                try {

                    Empleado empleado = new Empleado();

                    empleado.Documento = txtDocumento.Text.Trim();
                    empleado.Nombres = txtNombres.Text.Trim().ToUpper();
                    empleado.Apellidos = txtApellidos.Text.Trim().ToUpper();
                    empleado.Edad = Convert.ToInt32(txtEdad.Text.Trim());
                    empleado.Direccion = txtDireccion.Text.Trim();
                    empleado.Fecha_nacimiento = txtFechaNacimiento.Value.Year + "-" + txtFechaNacimiento.Value.Month + "-" + txtFechaNacimiento.Value.Day;

                    if (EmpleadoController.guardar(empleado)) {
                        MessageBox.Show("Registro exitoso.");
                        this.limpiarFormulario();
                        this.listar();
                    } else {
                        MessageBox.Show("Error guardando registro");
                    }

                } catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void listar()
        {
            DataTable datos = EmpleadoController.listar();

            if (datos == null) {
                MessageBox.Show("No hay información para listar");
                return;
            }

            dataGridListar.DataSource = datos.DefaultView;
        }

        private void limpiarFormulario()
        {
            txtDocumento.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            txtDireccion.Text = "";
            txtFechaNacimiento.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listar();
        }

        bool consultado = false;
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "") { 
                MessageBox.Show("Debe ingresar un documento.");
                return;
            }

            Empleado empleado = EmpleadoController.getEmpleado(txtDocumento.Text);

            if (empleado == null) {
                consultado = false;
                this.limpiarFormulario();
                MessageBox.Show("El empleado con documento " + txtDocumento.Text + " no existe.");
                return;
            }

            txtDocumento.Text = empleado.Documento;
            txtNombres.Text = empleado.Nombres;
            txtApellidos.Text = empleado.Apellidos;
            txtEdad.Text = empleado.Edad.ToString();
            txtDireccion.Text = empleado.Direccion;
            txtFechaNacimiento.Text = empleado.Fecha_nacimiento;
            consultado = true;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "") {
                txtDocumento.Focus();
                MessageBox.Show("Debe ingresar un documento válido.");
                return;
            }

            try {

                Empleado empleado = new Empleado();

                empleado.Documento = txtDocumento.Text.Trim();
                empleado.Nombres = txtNombres.Text.Trim().ToUpper();
                empleado.Apellidos = txtApellidos.Text.Trim().ToUpper();
                empleado.Edad = Convert.ToInt32(txtEdad.Text.Trim());
                empleado.Direccion = txtDireccion.Text.Trim();
                empleado.Fecha_nacimiento = txtFechaNacimiento.Value.Year + "-" + txtFechaNacimiento.Value.Month + "-" + txtFechaNacimiento.Value.Day;

                if (EmpleadoController.actualizar(empleado)) {
                    MessageBox.Show("Actualización correcta.");
                    this.limpiarFormulario();
                    this.listar();
                } else {
                    MessageBox.Show("Error actualizando registro");
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "") {
                txtDocumento.Focus();
                MessageBox.Show("Debe ingresar un documento válido.");
                return;
            }

            if (EmpleadoController.eliminar(txtDocumento.Text.Trim())) {
                MessageBox.Show("El registro se eliminó correctamente.");
                this.limpiarFormulario();
                this.listar();
            } else {
                MessageBox.Show("Error eliminando registro");
            }
        }
    }
}