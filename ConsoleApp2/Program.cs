using System;
using Libreria_Personajes;
namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Personaje personaje = new Heroe("Batman","Bruce Wayne","Ciudad gotica","Solitario");
            Personaje personajedos = new Heroe("9", "9", "9", "9");
            personajedos.ID = 6;
            Console.WriteLine(personaje);
            Console.WriteLine(Personaje_ADO.Guardar(personaje));
            personaje = personajedos;
            Personaje_ADO.Modificar(personaje);

            Console.WriteLine(Personaje_ADO.LeerUno(personaje));
            */

            //Personaje_ADO.ModificarPorCampo("NombreReal","Jacinto","NombrePersonaje = 'Bruce Wayne'"); ya no existe
            Personaje_ADO.ModificarPorLugarDeOrigenSegunAlianza("Gotham city", "Solitario");
        }
    }
}