using System;
using System.Collections.Generic;

namespace Proyectoticket.logica
{
    public class Ticket
    {
        public int IntNumero { get; set; }
        public string StrTitulo { get; set; }
        public string StrDescripcion { get; set; }
        public string StrCategoria { get; set; }
        public string StrPrioridad { get; set; }
        public string StrEstado { get; set; }
        public bool BlnEscalado { get; set; }
        public Solicitante ObjSolicitante { get; set; }
        public Tecnico? ObjTecnicoAsignado { get; set; }
        public Bitacora ObjBitacora { get; set; }
        public List<string> LstErrores { get; set; }

        public Ticket(int intNumero, string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante)
        {
            this.IntNumero = intNumero;
            this.StrTitulo = strTitulo;
            this.StrDescripcion = strDescripcion;
            this.StrCategoria = strCategoria;
            this.StrPrioridad = strPrioridad;
            this.ObjSolicitante = objSolicitante;
            this.StrEstado = "Abierto";
            this.BlnEscalado = false;
            this.ObjBitacora = new Bitacora();
            this.LstErrores = new List<string>();
            ObjBitacora.RegistrarEvento("Ticket creado");
        }

        public void AsignarTecnico(Tecnico objTecnico)
        {
            if (objTecnico == null)
            {
                throw new InvalidOperationException("No se puede asignar un tecnico nulo.");
            }

            if (!objTecnico.PuedeAtender(StrCategoria))
            {
                throw new InvalidOperationException("El tecnico no esta disponible o no atiende esta categoria.");
            }

            ObjTecnicoAsignado = objTecnico;
            objTecnico.AumentarCarga();
            StrEstado = "Asignado";
            ObjBitacora.RegistrarEvento($"Asignado a {objTecnico.StrNombre}");
        }

        public void RegistrarError(string strTipo, string strDescripcion, string strImpacto)
        {
            string strError = $"{strTipo}: {strDescripcion} | Impacto: {strImpacto}";
            LstErrores.Add(strError);
            ObjBitacora.RegistrarEvento("Error registrado - " + strError);

            if (strImpacto.Equals("Alto", StringComparison.OrdinalIgnoreCase) ||
                strImpacto.Equals("Critico", StringComparison.OrdinalIgnoreCase))
            {
                Escalar("Error de alto impacto");
            }
        }

        public void Resolver(string strSolucion)
        {
            if (ObjTecnicoAsignado == null || StrEstado != "Asignado")
            {
                throw new InvalidOperationException("Solo se puede resolver un ticket asignado.");
            }

            StrEstado = "Resuelto";
            ObjBitacora.RegistrarEvento("Solucion registrada: " + strSolucion);
        }

        public void Cerrar()
        {
            if (StrEstado != "Resuelto")
            {
                throw new InvalidOperationException("Solo se puede cerrar un ticket resuelto.");
            }

            StrEstado = "Cerrado";
            ObjTecnicoAsignado?.LiberarCarga();
            ObjBitacora.RegistrarEvento("Ticket cerrado");
        }

        public void Escalar(string strMotivo)
        {
            BlnEscalado = true;
            if (!StrPrioridad.Equals("Critica", StringComparison.OrdinalIgnoreCase))
            {
                StrPrioridad = "Critica";
            }
            ObjBitacora.RegistrarEvento("Ticket escalado: " + strMotivo);
        }

        public void MostrarResumen()
        {
            Console.WriteLine($"#{IntNumero} | {StrTitulo} | Estado: {StrEstado} | Prioridad: {StrPrioridad} | Categoria: {StrCategoria}");
            Console.WriteLine($" Solicitante: {ObjSolicitante.StrNombre}");
            Console.WriteLine($" Tecnico: {(ObjTecnicoAsignado != null ? ObjTecnicoAsignado.StrNombre : "Sin asignar")} | Escalado: {BlnEscalado}");
        }

        public void MostrarBitacora()
        {
            ObjBitacora.MostrarBitacora(IntNumero);
        }
    }
}