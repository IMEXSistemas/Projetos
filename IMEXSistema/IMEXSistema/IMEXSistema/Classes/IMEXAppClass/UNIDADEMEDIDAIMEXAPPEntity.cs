using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class UNIDADEMEDIDAIMEXAPPEntity
    {
        public int? IDUNIDADEMEDIDA { get; set; }
        public string XUNIDADEMEDIDA { get; set; }
        public int? NCASASDECIMAIS { get; set; }
        public int IDEMPRESA { get; set; }
        public DateTime? DTULTIMAALTERACAO { get; set; }
        public Boolean? STATIVO { get; set; }
        public string XSIGLA { get; set; }
        public string IDASPNETUSERSINCLUSAO { get; set; }
        public string XMEUID { get; set; }
        public string XUSERINCLUSAO { get; set; }

    }
}
