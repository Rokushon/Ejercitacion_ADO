namespace Libreria_Personajes
{
    public abstract class Personaje
    {
        protected int id;
        protected string nombreReal;
        protected string nombrePersonaje;
        protected string lugarDeOrigen;
        protected List<string> habilidades;

        private Personaje()
        {
            this.habilidades = new List<string>();
        }
        public Personaje(string nombreReal, string nombrePersonaje, string lugarDeOrigen)
            :this() //en la clase principal uso this y no base cuando llamo otro constructor
        {
            //this.id = id;
            this.nombreReal = nombreReal;
            this.nombrePersonaje = nombrePersonaje;
            this.lugarDeOrigen = lugarDeOrigen;
        }

        public int ID { get => id; set => id = value; }
        public string NombreReal { get => nombreReal;}
        public string NombrePersonaje { get => nombrePersonaje; }
        public string LugarDeOrigen { get => lugarDeOrigen;}
        public string Habilidades {
            get
            {
                string valores = string.Empty;

                for (int i = 0; i < this.habilidades.Count; i++)
                {
                    valores += this.habilidades[i];
                    if (i < this.habilidades.Count -1)
                    {
                        valores += ", ";
                    }
                }
                return valores;
            }
        }
        public string Descripcion { get => this.MostrarDescripcion(); }

        public void setHabilidad(string habilidad)
        {
            this.habilidades.Add(habilidad);
        }
        protected abstract string MostrarDescripcion();



        public override bool Equals(object obj)
        {
            bool respuesta = false;
            Personaje parametro = obj as Personaje;
            if (parametro != null) 
            {
                respuesta = (parametro.nombreReal == this.nombreReal && parametro.nombrePersonaje == this.nombrePersonaje);
            }
            return respuesta;
        }
        public void CargarHabilidadesDesdeString(string habilidades)
        {
            this.habilidades.AddRange(habilidades.Split(", "));
        }

        public override string ToString()
        {
            return $"id:{id} - nombre personaje:{nombrePersonaje} - nombre real:{nombreReal} - tipo: {this.GetType().Name}";
        }





    }
}
