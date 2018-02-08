using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class CONDICAOPAGAMENTOIMEXAPPEntity
    {
        public int? IDCONDICAPAGAMENTO { get; set; }
        public string XCONDICAOPAGAMENTO { get; set; }
        public string XFORMULA{ get; set; }
        public int? NPARCELAS { get; set; }
        public Boolean STBAIXA { get; set; }
        public int? IDEMPRESA { get; set; }
        public Boolean? STATIVO { get; set; }
        public DateTime? DTULTIMAALTERACAO { get; set; }
        public string XMEUID { get; set; }
        public string IDASPNETUSERSINCLUSAO { get; set; }
        public int? NDIAFAVORAVEL { get; set; }
    }
}
