using Libreria_Personajes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluacion_Integradora
{
    public partial class FormModificar : Form
    {
        public Personaje personaje;
        public FormModificar(Personaje personajetraido)
        {
            InitializeComponent();
            personaje = personajetraido;

        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_tipoPersonaje.SelectedItem!.ToString() == "Heroe")
            {
                grp_alianza.Visible = true;
                grp_planMalvado.Visible = false;
            }
            else if (cmb_tipoPersonaje.SelectedItem.ToString() == "Villano")
            {
                grp_alianza.Visible = false;
                grp_planMalvado.Visible = true;
            }
        }



        private void FormModificar_Load(object sender, EventArgs e)
        {
            cmb_tipoPersonaje.DataSource = new List<string>() { "Heroe", "Villano" };
            this.lst_lugarOrigen.DataSource = new List<string>() { "Asgard", "Midgard", "Xandar", "Hala", "Skrullos", "Titan", "Tierra-616" };

            textBox2.Text = personaje.NombreReal;
            textBox1.Text = personaje.NombrePersonaje;
            lst_lugarOrigen.SelectedItem = personaje.LugarDeOrigen;

            if(personaje is Heroe)
            {
                Heroe personajeHeroe =personaje as Heroe;
                cmb_tipoPersonaje.SelectedItem = "Heroe";
                foreach (RadioButton rb in grp_alianza.Controls)
                {
                    if(personajeHeroe.Alianza.Contains(rb.Text))
                    {
                        rb.Checked = true;
                    }
                }
            }
            else
            {
                Villano personajeVillano = personaje as Villano;
                cmb_tipoPersonaje.SelectedItem = "Villano";
                rtx_planMalvado.Text = personajeVillano.PlanMalvado;
            }

            foreach (CheckBox cb in grp_habilidades.Controls) 
            {
                if (personaje.Habilidades.Contains(cb.Text))
                {
                    cb.Checked = true;
                }
            }

        }





        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            int id = personaje.ID;
            string nombreReal = textBox2.Text;
            string nombrePersonaje = textBox1.Text;
            string lugarOrigen = lst_lugarOrigen.SelectedItem!.ToString()!;

            bool control = false;
            bool listaConValores = false;

            if (cmb_tipoPersonaje.SelectedItem!.ToString() == "Heroe")
            {

                string alianza = string.Empty;

                foreach (RadioButton item in grp_alianza.Controls)
                {
                    if (item.Checked)
                    {
                        alianza = item.Text;
                        break;
                    }
                }

                this.personaje = new Heroe(nombreReal, nombrePersonaje, lugarOrigen, alianza);
                personaje.ID = id;


                foreach (CheckBox item in grp_habilidades.Controls)
                {
                    if (item.Checked)
                    {
                        this.personaje.setHabilidad(item.Text);
                        listaConValores = true;
                    }
                }

                if (!string.IsNullOrEmpty(nombreReal) && !string.IsNullOrEmpty(nombrePersonaje) &&
                    !string.IsNullOrEmpty(lugarOrigen) && !string.IsNullOrEmpty(alianza) && listaConValores)
                {
                    control = true;
                }

            }
            else if (cmb_tipoPersonaje.SelectedItem.ToString() == "Villano")
            {

                string planMalvado = rtx_planMalvado.Text;

                this.personaje = new Villano(nombreReal, nombrePersonaje, lugarOrigen, planMalvado);

                foreach (CheckBox item in grp_habilidades.Controls)
                {
                    if (item.Checked)
                    {
                        this.personaje.setHabilidad(item.Text);
                        listaConValores = true;
                    }
                }
                if (!string.IsNullOrEmpty(nombreReal) && !string.IsNullOrEmpty(nombrePersonaje)
                    && !string.IsNullOrEmpty(lugarOrigen) && !string.IsNullOrEmpty(planMalvado) && listaConValores)
                {
                    control = true;
                }

            }
            if (control)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Faltan seleccionar valores");
            }
        }



        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
