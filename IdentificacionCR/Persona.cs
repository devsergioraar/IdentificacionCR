using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentificacionCR
{
    public class Persona
    {
        string _cedula = "";
        string _nombre_persona = "";
        string _cc_persona = "";

        string _nombre_padre_persona = "";
        string _cedula_padre_persona = "";

        string _nombre_madre_persona = "";
        string _cedula_madre_persona = "";

        DateTime _fecha_nacimiento_persona = DateTime.Now;
        string _nacionalidad_persona = "";
        string _marginal_persona = "";
        string _edad_persona = "";
        bool _fallecido = false;
        DateTime _fecha_fallecido_persona = DateTime.Now;
        

        

        public string Cedula { get => _cedula; set => _cedula=value; }
        public string Nombre_persona { get => _nombre_persona; set => _nombre_persona=value; }
        public string Cc_persona { get => _cc_persona; set => _cc_persona=value; }
        public string Nombre_padre_persona { get => _nombre_padre_persona; set => _nombre_padre_persona=value; }
        public string Cedula_padre_persona { get => _cedula_padre_persona; set => _cedula_padre_persona=value; }
        public string Nombre_madre_persona { get => _nombre_madre_persona; set => _nombre_madre_persona=value; }
        public string Cedula_madre_persona { get => _cedula_madre_persona; set => _cedula_madre_persona=value; }
        public DateTime Fecha_nacimiento_persona { get => _fecha_nacimiento_persona; set => _fecha_nacimiento_persona=value; }
        public string Nacionalidad_persona { get => _nacionalidad_persona; set => _nacionalidad_persona=value; }
        public string Marginal_persona { get => _marginal_persona; set => _marginal_persona=value; }
        public string Edad_persona { get => _edad_persona; set => _edad_persona=value; }

        public bool Fallecido { get => _fallecido; set => _fallecido=value; }
        public DateTime Fecha_fallecido_persona { get => _fecha_fallecido_persona; set => _fecha_fallecido_persona=value; }

        
    }
}
