    using System;
using System.Collections.Generic;

namespace Proyectoticket.logica
{
    public class Bitacora
    {
        public List<string> LstEntradas { get; set; } = new List<string>();

        public void RegistrarEvento(string strEvento)
        {
            LstEntradas.Add($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {strEvento}");
        }

        public void MostrarBitacora(int intNumeroTicket)
        {
            Console.WriteLine($"\nBitacora del ticket #{intNumeroTicket}");
            foreach (string strEntrada in LstEntradas)
            {
                Console.WriteLine("- " + strEntrada);
            }
        }
    }
}