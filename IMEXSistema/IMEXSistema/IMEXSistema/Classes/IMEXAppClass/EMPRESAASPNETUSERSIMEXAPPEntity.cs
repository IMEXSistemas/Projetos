using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class EMPRESAASPNETUSERSIMEXAPPEntity
    {
        public Boolean? STATIVO { get; set; } // BOOLEAN
        public int? IDEMPRESAASPNETUSERS { get; set; } // INTEGER
        public string XNOME { get; set; } // STRING
        public string XAPELIDO { get; set; } // STRING
        public int? QVISITANOVOSCLIENTE { get; set; } // INTEGER
        public int? STPERIODONOVOSCLIENTES { get; set; } // BYTE
        public Boolean? STADMINISTRADOR { get; set; } // BOOLEAN
        public int? STACESSOTODOSCLIENTES { get; set; } // BYTE
        public decimal? VMETAFATURAMENTO { get; set; } // DECIMAL NUMBER
        public int? IDEMPRESA { get; set; } // INTEGER
        public string XEMAIL { get; set; } // 	STRING
        public string XTELEFONES { get; set; } // STRING
        public string IDASPNETUSERS { get; set; } // STRING
        public int? QVISITACLIENTESATIVOS { get; set; } // INTEGER
        public int? STPERIODOCLIENTESATIVOS { get; set; } //BYTE
        public DateTime? DTULTIMAALTERACAO { get; set; } //DATE
        public DateTime? DTINCLUSAO { get; set; } //DATE
        public string IDASPNETUSERSINCLUSAO { get; set; } //STRING
        public Boolean? BEMPRESACORRENTE { get; set; } //	BOOLEAN
        public string XMEUID { get; set; } //STRING
      //  public EMPRESA{ get; set; } //EMPRESA
        public Boolean? STVISUALIZARANKINGREPRESENTANTES { get; set; } //BOOLEAN
        public string XTKNACESSOCATALOGO { get; set; } //STRING
        public string IMUSUARIO { get; set; } //STRING
        public string IMUSUARIOCROPPED { get; set; } //STRING
        public int? UTCUSUARIO { get; set; } //integer

}
}
