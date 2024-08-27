

using Libreria_Personajes;

namespace Evaluacion_Integradora
{
    public partial class FormPrincipal : Form
    {

        private List<Personaje> personajes;

        public FormPrincipal()
        {
            InitializeComponent();
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.personajes = new List<Personaje>();
            Personaje superman = new Heroe("Clark Kent", "Superman", "Titan", "Guardianes de la galaxia");
            superman.setHabilidad("Fuerza Sobrehumana");
            superman.setHabilidad("Vuelo");
            personajes.Add(superman);
            refreshDGV();
        }



        private void btn_agregar_Click(object sender, EventArgs e)
        {
            FormCrear crear = new FormCrear();
            crear.ShowDialog();
            if (crear.DialogResult == DialogResult.OK)
            {
                Personaje_ADO.Guardar(crear.Personaje);
                //personajes.Add(crear.Personaje);
                refreshDGV();
            }
        }




        private void btn_modificar_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridView1.CurrentRow != null) 
            {
                Personaje personajeSeleccionado = this.dataGridView1.CurrentRow.DataBoundItem as Personaje;
                FormModificar modificar = new FormModificar(personajeSeleccionado);
                modificar.ShowDialog();
                if (modificar.DialogResult == DialogResult.OK)
                {
                    bool banderita = true;
                    for (int i = 0; i < personajes.Count; i++)
                    {
                        if (personajes[i].Equals(modificar.personaje))
                        {
                            personajes[i] = modificar.personaje;
                            banderita = false;
                            break;
                        }
                    }
                    if (banderita)
                    {
                        personajes.Add(modificar.personaje);
                    }
                    refreshDGV();
                }
            }
            */
            Personaje personajeSeleccionado = this.dataGridView1.CurrentRow.DataBoundItem as Personaje;
            FormModificar modificar = new FormModificar(personajeSeleccionado);
            modificar.ShowDialog();
            if(modificar.DialogResult == DialogResult.OK)
            {
                Personaje_ADO.Modificar(modificar.personaje);
            }

            refreshDGV();
        }



        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            /*if (dataGridView1.CurrentRow != null) 
            {
                Personaje personajeSeleccionado = this.dataGridView1.CurrentRow.DataBoundItem as Personaje;

                if (personajeSeleccionado != null)
                {
                    personajes.Remove(personajeSeleccionado);
                    refreshDGV();
                }
            }*/
            Personaje psj = dataGridView1.CurrentRow.DataBoundItem as Personaje;
            DialogResult result = MessageBox.Show($"SEGURO que desea eliminar a\n{psj}\n??¡¡ESTE PROCESO ES IRREVERSIBLE!!","ELIMINACION",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (Personaje_ADO.Eliminar(psj.ID))
                {
                    MessageBox.Show("Eliminacion exitosa");
                }
                else
                {
                    MessageBox.Show("Fallo la conexion con la base de datos");
                }
            }
            else
            {
                MessageBox.Show("Eliminacion cancelada");
            }

            refreshDGV();

        }


        private void refreshDGV()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Personaje_ADO.LeerTodos();
            //dataGridView1.DataSource = personajes;
        }
    }
}
