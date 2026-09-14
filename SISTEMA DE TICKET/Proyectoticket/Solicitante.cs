using System;

namespace Proyectoticket.logica
{
    public class Solicitante : Persona
    {
        public string StrDepartamento { get; set; }
        public string StrExtension { get; set; }

        public Solicitante(string strCodigo, string strNombre, string strCorreo, string strDepartamento, string strExtension)
            : base(strCodigo, strNombre, strCorreo)
        {
            this.StrDepartamento = strDepartamento;
            this.StrExtension = strExtension;
        }

        public override string ObtenerRol()
        {
            return "Solicitante";
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($" Departamento: {StrDepartamento} | Extension: {StrExtension}");
        }
    }
}