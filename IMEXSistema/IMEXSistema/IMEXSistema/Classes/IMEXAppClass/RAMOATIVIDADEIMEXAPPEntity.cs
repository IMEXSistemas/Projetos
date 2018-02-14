using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class RAMOATIVIDADEIMEXAPPEntity
    {
        public int? IDRAMOATIVIDADE	{ get; set; }//INTEGER
        public string XRAMOATIVIDADE	{ get; set; }//STRING
        public string CRAMOATIVIDADE	{ get; set; }//STRING
        public int? IDEMPRESA	{ get; set; }//INTEGER
        public Boolean? STATIVO	{ get; set; }//BOOLEAN
        public DateTime? DTULTIMAALTERACAO	{ get; set; }//DATE
        public string IDASPNETUSERSINCLUSAO { get; set; }//STRING
        public string XMEUID	{ get; set; }//STRING
    }
}
