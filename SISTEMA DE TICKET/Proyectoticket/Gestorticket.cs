using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyectoticket.logica
{
    public class GestorTicket
    {
        public List<Ticket> LstTickets { get; set; }
        public List<Solicitante> LstSolicitantes { get; set; }
        public List<Tecnico> LstTecnicos { get; set; }

        public GestorTicket()
        {
            LstTecnicos = new List<Tecnico>();
            LstTickets = new List<Ticket>();
            LstSolicitantes = new List<Solicitante>();
        }

        public Ticket CrearTicket(string strNombre, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            int intNumero = LstTickets.Count + 1;
            Ticket objTicket = new Ticket(intNumero, strNombre, strDescripcion, strCategoria, strPrioridad, objSolicitante);

            Tecnico? objTecnicoDisponible = LstTecnicos.FirstOrDefault(t => t.PuedeAtender(strCategoria));
            if (objTecnicoDisponible != null)
            {
                objTicket.AsignarTecnico(objTecnicoDisponible);
            }

            LstTickets.Add(objTicket);
            return objTicket;
        }

        public Ticket BuscarTicket(int intNumero)
        {
            Ticket? objTicket = LstTickets.Find(t => t.IntNumero == intNumero);
            if (objTicket == null)
            {
                throw new KeyNotFoundException($"No existe ningún ticket registrado con el numero #{intNumero}.");
            }
            return objTicket;
        }

        public void MostrarTickets()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== LISTADO DE TICKETS ===");
            Console.ResetColor();

            if (!LstTickets.Any())
            {
                Console.WriteLine("No hay tickets registrados en el sistema.");
                return;
            }

            foreach (Ticket objTicket in LstTickets)
            {
                objTicket.MostrarResumen();
                Console.WriteLine();
            }
        }

        public void GenerarResumenControl()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n========================================================");
            Console.WriteLine(" RESUMEN DE CONTROL Y ESTADÍSTICAS DEL SISTEMA (CX)");
            Console.WriteLine("========================================================");
            Console.ResetColor();

            int intTotalTickets = LstTickets.Count;
            int intAbiertos = LstTickets.Count(t => t.StrEstado == "Abierto");
            int intAsignados = LstTickets.Count(t => t.StrEstado == "Asignado");
            int intResueltos = LstTickets.Count(t => t.StrEstado == "Resuelto");
            int intCerrados = LstTickets.Count(t => t.StrEstado == "Cerrado");
            int intEscalados = LstTickets.Count(t => t.BlnEscalado);

            double dblPorcentajeResolucion = intTotalTickets > 0 ? ((double)(intResueltos + intCerrados) / intTotalTickets) * 100 : 0;

            Console.WriteLine($"• Total de Tickets Registrados: {intTotalTickets}");
            Console.WriteLine($"• Tickets Abiertos:             {intAbiertos}");
            Console.WriteLine($"• Tickets Asignados:            {intAsignados}");
            Console.WriteLine($"• Tickets Resueltos:            {intResueltos}");
            Console.WriteLine($"• Tickets Cerrados:             {intCerrados}");
            Console.WriteLine($"• Total de Tickets Escalados:   {intEscalados}");
            Console.WriteLine($"• Eficiencia de Resolución CX:   {dblPorcentajeResolucion:F2}%");
            Console.WriteLine("\n--- ESTADO DE CARGA DE TÉCNICOS ---");

            foreach (Tecnico objTecnico in LstTecnicos)
            {
                Console.WriteLine($"• Técnico: {objTecnico.StrNombre} ({objTecnico.StrEspecialidad}) | Carga Actual: {objTecnico.IntCargaActual}/{objTecnico.IntCapacidadMaxima}");
            }
        }
    }
}