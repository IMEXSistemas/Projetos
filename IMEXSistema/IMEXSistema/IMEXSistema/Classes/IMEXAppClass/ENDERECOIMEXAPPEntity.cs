using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class ENDERECOIMEXAPPEntity
    {
        public int? IDENDERECO { get; set; }
        public string STENDERECO { get; set; }
        public string XCEP { get; set; }
        public string XENDERECO { get; set; }
        public int CNUMERO { get; set; }
        public string XCOMPLEMENTO { get; set; }
        public string XBAIRRO { get; set; }
        public string XCIDADE { get; set; }
        public string XESTADO { get; set; }
        public int? IDCLIENTE { get; set; }
        public int? IDREPRESENTADA { get; set; }
        public string IDASPNETUSERSINCLUSAO { get; set; }
        public string XLATITUDE { get; set; }
        public string XLONGITUDE { get; set; }
        public int IDEMPRESA { get; set; }
        public DateTime? DTULTIMAALTERACAO { get; set; }
        public string IDASPNETUSERS { get; set; }
        public DateTime? DTCADASTRO { get; set; }
        public Boolean? STPRINCIPAL { get; set; }
        public string XMEUID { get; set; }
        public int? IDTRANSPORTADORA { get; set; }
   }
}
