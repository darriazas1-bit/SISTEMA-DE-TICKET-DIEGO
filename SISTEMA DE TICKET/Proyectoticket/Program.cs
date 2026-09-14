using System;
using System.Collections.Generic;
using Proyectoticket.logica;

namespace Proyectoticket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tecnico objTecnicoSoftware = new Tecnico("T01", "Ana Lopez", "ana@empresa.com", "Software", 2);
            Tecnico objTecnicoHardware = new Tecnico("T02", "Carlos Mendez", "carlos@empresa.com", "Hardware", 2);
            Tecnico objTecnicoGeneral = new Tecnico("T03", "Maria Perez", "maria@empresa.com", "General", 3);

            Solicitante objSolicitanteContabilidad = new Solicitante("S01", "Luis Ramirez", "luis@empresa.com", "Contabilidad", "1201");
            Solicitante objSolicitanteVentas = new Solicitante("S02", "Karla Gomez", "karla@empresa.com", "Ventas", "1305");

            List<Persona> lstUsuarios = new List<Persona>()
            {
                objTecnicoSoftware,
                objTecnicoHardware,
                objTecnicoGeneral,
                objSolicitanteContabilidad,
                objSolicitanteVentas
            };

            GestorTicket objGestor = new GestorTicket();
            objGestor.LstTecnicos.Add(objTecnicoSoftware);
            objGestor.LstTecnicos.Add(objTecnicoHardware);
            objGestor.LstTecnicos.Add(objTecnicoGeneral);
            objGestor.LstSolicitantes.Add(objSolicitanteContabilidad);
            objGestor.LstSolicitantes.Add(objSolicitanteVentas);

            FlujoTicket objFlujoTicket = new FlujoTicket();

            bool blnContinuar = true;

            while (blnContinuar)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n========================================================");
                Console.WriteLine(" SISTEMA DE TICKETS DE SOPORTE TECNICO CORPORATIVO");
                Console.WriteLine("========================================================");
                Console.ResetColor();
                Console.WriteLine(" 1. Ver usuarios del sistema (polimorfismo)");
                Console.WriteLine(" 2. Crear ticket (asigna automaticamente)");
                Console.WriteLine(" 3. Ver tickets");
                Console.WriteLine(" 4. Asignar / reasignar ticket a un tecnico");
                Console.WriteLine(" 5. Registrar error en ticket");
                Console.WriteLine(" 6. Resolver ticket");
                Console.WriteLine(" 7. Escalar ticket");
                Console.WriteLine(" 8. Cerrar ticket");
                Console.WriteLine(" 9. Consultar bitacora de un ticket");
                Console.WriteLine(" 10. Generar metricas");
                Console.WriteLine(" 11. Salir");
                Console.Write("\n Seleccione una opcion (1-11): ");

                try
                {
                    string strOpcion = Console.ReadLine()?.Trim() ?? "";

                    switch (strOpcion)
                    {
                        case "1":
                            MostrarUsuariosPolimorfismo(lstUsuarios);
                            break;

                        case "2":
                            CrearTicket(objGestor);
                            break;

                        case "3":
                            objGestor.MostrarTickets();
                            break;

                        case "4":
                            AsignarTicket(objGestor);
                            break;

                        case "5":
                            RegistrarError(objGestor);
                            break;

                        case "6":
                            ResolverTicket(objGestor, objFlujoTicket);
                            break;

                        case "7":
                            EscalarTicket(objGestor);
                            break;

                        case "8":
                            CerrarTicket(objGestor, objFlujoTicket);
                            break;

                        case "9":
                            ConsultarBitacora(objGestor);
                            break;

                        case "10":
                            objGestor.GenerarResumenControl();
                            break;

                        case "11":
                            blnContinuar = false;
                            Console.WriteLine("\nGracias por utilizar el sistema de soporte.");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opcion no valida. Ingrese un numero del 1 al 11.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: " + ex.Message);
                    Console.ResetColor();
                }
            }
        }

        static void MostrarUsuariosPolimorfismo(List<Persona> lstUsuarios)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== USUARIOS DEL SISTEMA (POLIMORFISMO) ===");
            Console.ResetColor();

            foreach (Persona objPersona in lstUsuarios)
            {
                objPersona.MostrarInformacion();
                Console.WriteLine();
            }
        }

        static void CrearTicket(GestorTicket objGestor)
        {
            Console.WriteLine("\nSolicitantes disponibles:");
            for (int i = 0; i < objGestor.LstSolicitantes.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {objGestor.LstSolicitantes[i].StrNombre} - {objGestor.LstSolicitantes[i].StrDepartamento}");
            }

            Console.Write("Seleccione solicitante: ");
            int intIndice = int.Parse(Console.ReadLine() ?? "0") - 1;

            if (intIndice < 0 || intIndice >= objGestor.LstSolicitantes.Count)
            {
                throw new ArgumentOutOfRangeException("Solicitante", "Seleccion fuera de rango.");
            }

            Console.Write("Titulo del problema: ");
            string strTitulo = Console.ReadLine() ?? "";

            Console.Write("Descripcion: ");
            string strDescripcion = Console.ReadLine() ?? "";

            Console.Write("Categoria (Software/Hardware/Red/General): ");
            string strCategoria = Console.ReadLine() ?? "";

            Console.Write("Prioridad (Baja/Media/Alta/Critica): ");
            string strPrioridad = Console.ReadLine() ?? "";

            Ticket objTicket = objGestor.CrearTicket(strTitulo, strDescripcion, strCategoria, strPrioridad, objGestor.LstSolicitantes[intIndice]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTicket creado correctamente.");
            Console.ResetColor();
            objTicket.MostrarResumen();
        }

        static void AsignarTicket(GestorTicket objGestor)
        {
            Ticket objTicket = SolicitarTicket(objGestor);

            Console.WriteLine("\nTecnicos disponibles:");
            for (int i = 0; i < objGestor.LstTecnicos.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {objGestor.LstTecnicos[i].StrNombre} ({objGestor.LstTecnicos[i].StrEspecialidad})");
            }

            Console.Write("Seleccione un tecnico para reasignar: ");
            int intIndice = int.Parse(Console.ReadLine() ?? "0") - 1;

            if (intIndice < 0 || intIndice >= objGestor.LstTecnicos.Count)
            {
                throw new ArgumentOutOfRangeException("Tecnico", "Seleccion fuera de rango.");
            }

            objTicket.AsignarTecnico(objGestor.LstTecnicos[intIndice]);
            Console.WriteLine("Tecnico reasignado correctamente.");
        }

        static void RegistrarError(GestorTicket objGestor)
        {
            Ticket objTicket = SolicitarTicket(objGestor);

            Console.Write("Tipo de error (Software/Hardware/Red/Usuario): ");
            string strTipo = Console.ReadLine() ?? "";

            Console.Write("Descripcion del error: ");
            string strDescripcion = Console.ReadLine() ?? "";

            Console.Write("Impacto (Bajo/Medio/Alto/Critico): ");
            string strImpacto = Console.ReadLine() ?? "";

            objTicket.RegistrarError(strTipo, strDescripcion, strImpacto);
            Console.WriteLine("Error registrado correctamente.");
        }

        static void ResolverTicket(GestorTicket objGestor, FlujoTicket objFlujoTicket)
        {
            Ticket objTicket = SolicitarTicket(objGestor);

            if (!objFlujoTicket.PuedeCambiarEstado(objTicket.StrEstado, "Resuelto"))
            {
                throw new InvalidOperationException("El flujo no permite resolver el ticket desde el estado actual.");
            }

            Console.Write("Solucion aplicada: ");
            string strSolucion = Console.ReadLine() ?? "";

            objTicket.Resolver(strSolucion);
            Console.WriteLine("Ticket resuelto correctamente.");
        }

        static void EscalarTicket(GestorTicket objGestor)
        {
            Ticket objTicket = SolicitarTicket(objGestor);

            Console.Write("Ingrese motivo del escalamiento: ");
            string strMotivo = Console.ReadLine() ?? "";

            objTicket.Escalar(strMotivo);
            Console.WriteLine("Ticket escalado exitosamente.");
        }

        static void CerrarTicket(GestorTicket objGestor, FlujoTicket objFlujoTicket)
        {
            Ticket objTicket = SolicitarTicket(objGestor);

            if (!objFlujoTicket.PuedeCambiarEstado(objTicket.StrEstado, "Cerrado"))
            {
                throw new InvalidOperationException("El flujo no permite cerrar el ticket desde el estado actual.");
            }

            objTicket.Cerrar();
            Console.WriteLine("Ticket cerrado correctamente.");
        }

        static void ConsultarBitacora(GestorTicket objGestor)
        {
            Ticket objTicket = SolicitarTicket(objGestor);
            objTicket.MostrarBitacora();
        }

        static Ticket SolicitarTicket(GestorTicket objGestor)
        {
            Console.Write("Ingrese numero de ticket: ");
            int intNumero = int.Parse(Console.ReadLine() ?? "0");
            return objGestor.BuscarTicket(intNumero);
        }
    }
}