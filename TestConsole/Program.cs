using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IdentificacionCR;

namespace TestConsole
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                IdService servicio = new IdService(@"C:\driversdev\100.0.4896.60_chromedriver_win32");
                DateTime inicio = DateTime.Now;
                DateTime fin = DateTime.Now;
                string res = "";

                

                try
                {
                    //Cédula que no existe.
                    Console.WriteLine("Caso: Persona que no existe con Cédula.");
                    inicio=DateTime.Now;
                    res=servicio.ConsultaCedula("999999999");
                    fin=DateTime.Now;
                    print(inicio, fin, res);
                }
                catch (Exception caso2)
                {
                    Console.WriteLine(caso2.Message);
                }
               
                try
                {
                    Console.WriteLine("Caso: Persona que no existe con Nombre.");
                    //Persona que no existe.
                    inicio=DateTime.Now;
                    res=servicio.ConsultaNombre("Máximo", "Décimo", "Meridio");
                    fin=DateTime.Now;
                    print(inicio, fin, res);
                }
                catch (Exception caso4)
                {
                    Console.WriteLine(caso4.Message);

                }

                servicio.CerrarCliente();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Presione ENTER para cerrar");
            Console.ReadLine();
        }

        public static void print( DateTime ini, DateTime fin, string dato )
        {
            TimeSpan time = fin-ini;

            Console.WriteLine("Inicio: "+ini.ToLongTimeString());
            Console.WriteLine("Fin: "+fin.ToLongTimeString());
            Console.WriteLine("Duración: "+time.TotalSeconds+" Segundos.");
            Console.WriteLine(dato);
        }
    }
}

