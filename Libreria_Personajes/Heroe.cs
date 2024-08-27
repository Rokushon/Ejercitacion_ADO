using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_Personajes
{
    public sealed class Heroe : Personaje 
        //sealed evita que cualquier otra clase herede de Heroe (aunque todavia si puede de heroe)
    {
        private string alianza;

        public Heroe(string nombreReal, string nombrePersonaje, string lugarDeOrigen,string alianza)
            : base(nombreReal, nombrePersonaje, lugarDeOrigen) // en la clase derivada uso base, no this
        {
            this.alianza = alianza;
        }

        public string Alianza { get => alianza;}

        protected override string MostrarDescripcion()
        {
            return $"{nombrePersonaje} es un héroe de la alianza {alianza}";
        }
    }
}
