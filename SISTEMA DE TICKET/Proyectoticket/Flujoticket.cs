using System;

namespace Proyectoticket.logica
{
    public class FlujoTicket
    {
        public bool PuedeCambiarEstado(string strEstadoActual, string strNuevoEstado)
        {
            if (strEstadoActual == "Abierto" && strNuevoEstado == "Asignado") return true;
            if (strEstadoActual == "Asignado" && strNuevoEstado == "Resuelto") return true;
            if (strEstadoActual == "Resuelto" && strNuevoEstado == "Cerrado") return true;
            return false;
        }

        public void MostrarFlujo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== FLUJO DE ESTADOS DEL TICKET ===");
            Console.ResetColor();
            Console.WriteLine(" [Abierto] ---> [Asignado] ---> [Resuelto] ---> [Cerrado]");
        }
    }
}