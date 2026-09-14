using System;

namespace Proyectoticket.logica
{
    public abstract class Persona(string codigo, string nombre, string email)
    {
        public string StrCodigo { get; set; } = codigo;
        public string StrNombre { get; set; } = nombre;
        public string StrCorreo { get; set; } = email;
        public bool BlnActivo { get; set; } = true;

        public abstract string ObtenerRol();

        public virtual void Registrar()
        {
            Console.WriteLine($"Persona registrada: [{ObtenerRol()}] {StrNombre} ({StrCodigo})");
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"[{ObtenerRol()}] Codigo: {StrCodigo} | Nombre: {StrNombre} | Correo: {StrCorreo}");
        }
    }
}