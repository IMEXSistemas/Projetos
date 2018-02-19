using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class PEDIDOVENDAITEMIMEXAPPEntity
    {
        public int? IDPEDIDOVENDAITEM { get; set; } //	 INTEGER
        public int? IDPEDIDOVENDA { get; set; } //INTEGER
        public int? IDTABELAPRECO { get; set; } //INTEGER
        public int? IDPRODUTO { get; set; } //INTEGER
        public decimal? VUNITARIOVENDA { get; set; } //DECIMAL NUMBER
        public decimal? VUNITARIOVENDACOMIMPOSTOS { get; set; } //DECIMAL NUMBER
        public decimal? PDESCONTO { get; set; } //DECIMAL NUMBER
        public decimal? VDESCONTO { get; set; } //DECIMAL NUMBER
        public decimal? VSUBTOTAL { get; set; } //DECIMAL NUMBER
        public decimal? VSUBTOTALSEMIMPOSTOS { get; set; } //DECIMAL NUMBER
        public string STCOMISSAO { get; set; } //STRING
        public decimal? PCOMISSAO { get; set; } //DECIMAL NUMBER
        public decimal? VCOMISSAO { get; set; } //DECIMAL NUMBER
        public decimal? VCUSTO { get; set; } //DECIMAL NUMBER
        public string XINFADICIONAIS { get; set; } //STRING
        public int? IDEMPRESA { get; set; } //INTEGER
        public string XMEUID { get; set; } //STRING
        public decimal? VVENDA { get; set; } //DECIMAL NUMBER
        public decimal? PIPIVENDA { get; set; } //DECIMAL NUMBER
        public decimal? PSTVENDA { get; set; } //DECIMAL NUMBER
        public decimal? PCOMISSAOORIGINAL { get; set; } //	DECIMAL NUMBER
        //public PRODUTO	 { get; set; } //PRODUTODATAMODEL
        public DateTime? DTULTIMAALTERACAO { get; set; } //	DATE
        public decimal? VQTDITEM { get; set; } //DECIMAL NUMBER
        public decimal? VSALDOAENTREGAR { get; set; } //DECIMAL NUMBER
        public int? STENTREGA { get; set; } //BYTE
        public int? IDGRADECOR { get; set; } //INTEGER
        public int? IDGRADETAMANHO { get; set; } //INTEGER
        public int? IDITEMAGRUPAMENTO { get; set; } //	INTEGER
        public string XCODCSTPIS { get; set; } //STRING
        public string XCODCSTCOFINS { get; set; } //STRING
        public decimal? DALIQOPICMSST { get; set; } //DECIMAL NUMBER
        public string XCODCSTIPI { get; set; } //STRING
        public string XCODCSTICMS { get; set; } //STRING
        public string XORIGEM { get; set; } //STRING
        public string XCFOP { get; set; } //STRING
        public decimal? VTABELAORIGEM { get; set; } //DECIMAL NUMBER
        public string STCOMISSAOINICIAL { get; set; } //STRING
        public Boolean STDESCONTAIPICOMISSAO { get; set; } //	BOOLEAN
        public Boolean STDESCONTASTCOMISSAO { get; set; } //	BOOLEAN
        //        public OBJCOR	 { get; set; } //GRADECORDATAMODEL
        //       public OBJTAMANHO	 { get; set; } //GRADETAMANHODATAMODEL
        //       public OBJTABELAPRECO	 { get; set; } //TABELAPRECO
        //       public OBJCOFINS { get; set; } // PedidoVendaItemCofins
}
}
