using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class CATEGORIAPRODUTOIMEXAPPEntity
    {
        public int? IDCATEGORIA { get; set; }
        public string XCATEGORIA { get; set; }
        public int? IDCATEGORIAPAI { get; set; }
        public int IDEMPRESA { get; set; }
        public Boolean? STATIVO { get; set; }
        public DateTime? DTULTIMAALTERACAO { get; set; }
        public string IDASPNETUSERSINCLUSAO { get; set; }
        public int? IDREPRESENTACAO { get; set; }
        public string XMEUID { get; set; }
        public string XMEUIDPAI { get; set; }
        public string XPATHCAPACATALOGO { get; set; }
        public string XDETALHESCATEGORIAPARACATALOGO { get; set; }
        public string XPATHCAPACATALOGO2 { get; set; }
        public string XPATHCAPACATALOGO3 { get; set; }
        public string XPATHCAPACATALOGO4 { get; set; }
        public string XPATHCAPACATALOGO5 { get; set; }

    }
}
