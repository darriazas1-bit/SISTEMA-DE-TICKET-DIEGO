using System;

namespace Proyectoticket.logica
{
    public class Tecnico : Persona
    {
        public string StrEspecialidad { get; set; }
        public int IntCargaActual { get; set; }
        public int IntCapacidadMaxima { get; set; }

        public Tecnico(string strCodigo, string strNombre, string strCorreo, string strEspecialidad, int intCapacidadMaxima)
            : base(strCodigo, strNombre, strCorreo)
        {
            this.StrEspecialidad = strEspecialidad;
            this.IntCargaActual = 0;
            this.IntCapacidadMaxima = intCapacidadMaxima;
        }

        public override string ObtenerRol()
        {
            return "Tecnico";
        }

        public bool EstaDisponible()
        {
            return BlnActivo && IntCargaActual < IntCapacidadMaxima;
        }

        public bool PuedeAtender(string strCategoria)
        {
            return EstaDisponible() &&
                   (StrEspecialidad.Equals(strCategoria, StringComparison.OrdinalIgnoreCase) ||
                    StrEspecialidad.Equals("General", StringComparison.OrdinalIgnoreCase));
        }

        public void AumentarCarga()
        {
            if (IntCargaActual < IntCapacidadMaxima)
            {
                IntCargaActual++;
            }
        }

        public void LiberarCarga()
        {
            if (IntCargaActual > 0)
            {
                IntCargaActual--;
            }
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($" Especialidad: {StrEspecialidad} | Carga: {IntCargaActual}/{IntCapacidadMaxima}");
        }
    }
}