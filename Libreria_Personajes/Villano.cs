using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_Personajes
{
    public sealed class Villano : Personaje
    {
        private string planMalvado;

        public Villano(string nombreReal, string nombrePersonaje, string lugarDeOrigen, string planMalvado)
            : base(nombreReal, nombrePersonaje, lugarDeOrigen) // en la clase derivada uso base, no this
        {
            this.planMalvado = planMalvado;
        }

        public string PlanMalvado { get => planMalvado;}

        protected override string MostrarDescripcion()
        {
            return $"{nombrePersonaje} es un villano con un plan malvado de: {planMalvado}";
        }
        public override string ToString() 
        {
            return base.ToString() + $" con el plan malvado: {planMalvado}";
        }
    }
}
