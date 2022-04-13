using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentificacionCR
{
    public class Relaciones
    {
        string _cita = "";
        string _cedula_conyuge = "";
        string _nombre_conyugue = "";
        string _cc_conyugue = "";       
        string _lugar = "";
        string _fecha = "";
        string _tipo = "";
        string _extranjero = "";
        string _fallecido = "";
        string _marginal = "";

        public string Cita { get => _cita; set => _cita=value; }
        public string Cedula_conyuge { get => _cedula_conyuge; set => _cedula_conyuge=value; }
        public string Nombre_conyugue { get => _nombre_conyugue; set => _nombre_conyugue=value; }
        public string CC_conyugue { get => _cc_conyugue; set => _cc_conyugue=value; }
        public string Lugar { get => _lugar; set => _lugar=value; }
        public string Fecha { get => _fecha; set => _fecha=value; }
        public string Tipo { get => _tipo; set => _tipo=value; }
        public string Extranjero { get => _extranjero; set => _extranjero=value; }
        public string Fallecido { get => _fallecido; set => _fallecido=value; }
        public string Marginal { get => _marginal; set => _marginal=value; }
    }
}
