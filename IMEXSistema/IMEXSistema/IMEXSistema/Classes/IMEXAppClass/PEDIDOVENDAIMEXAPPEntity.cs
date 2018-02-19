using System;
using System.Collections.Generic;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class PEDIDOVENDAIMEXAPPEntity
    {
        public int? IDPEDIDOVENDA { get; set; }//	INTEGER
        public int? STPEDIDOVENDA { get; set; }//	STATUS DE ORÇAMENTO 0 - ABERTO 1 - CANCELADO 2 - VENDIDO BYTE
        public int? IDCLIENTES { get; set; }//INTEGER
        public string IDASPNETUSERS { get; set; }//	STRING
        public DateTime? DEMISSAO { get; set; }//DATE
        public int? IDCONDICAOPAGAMENTO { get; set; }//	INTEGER
        public int? IDTRANSPORTADORA { get; set; }//INTEGER
        public int? IDCONTATOS { get; set; }//INTEGER
        public int? IDEMPRESA { get; set; }//INTEGER
        public int? STLANCAMENTO { get; set; }//BYTE
        public string XINFADICIONAL { get; set; }//STRING
        public string XMOTIVOCANCELAMENTO { get; set; }//	STRING
        public DateTime? DTULTIMAALTERACAO { get; set; }//	DATE
        public string XMEUID { get; set; }//STRING
        public int? IDPEDIDOVENDAOFFLINE { get; set; }//	INTEGER
        public int? IDPEDIDODISPLAY { get; set; }//INTEGER
        public string IDASPNETUSERSREPRESENTANTE { get; set; }//	STRING
        public int? STENVIADOREPRESENTACAO { get; set; }//	BYTE
        public int? STENVIADOCLIENTE { get; set; }//BYTE
        public int? IDORCAMENTOORIGEM { get; set; }//INTEGER
        public int? IDPEDIDOORIGEM { get; set; }//INTEGER
        public DateTime? DTVALIDADE { get; set; }//DATE
        public int? IDREPRESENTANTEPEDIDO { get; set; }//INTEGER
        public string XREPRESENTANTE { get; set; }//STRING
        public int? IDSTATUS { get; set; }//INTEGER
       // public STATUS	{ get; set; }//STATUS
        public decimal? VSUBTOTAL { get; set; }//DECIMAL NUMBER
        public decimal? PLUCRO { get; set; }//DECIMAL NUMBER
        public decimal? VCOMISSAOTOTAL { get; set; }//DECIMAL NUMBER

        public IList<PEDIDOVENDAITEMIMEXAPPEntity> ITENS { get; set; }

        //public PEDIDOVENDAITEMIMEXAPPEntity itens { get; set; }
        //public ITENS	{ get; set; }//COLLECTION DE PEDIDOVENDAITEM
        //public IList<PEDIDOVENDAITEMIMEXAPPEntity> ITENS { get; set; }

        public int? IDCONTATO { get; set; }//INTEGER
       // public CONTATO	{ get; set; }//CONTATO
        public DateTime? DTFATURAMENTO { get; set; }//DATE
        public string XNUMNF { get; set; }//STRING
        public string XCHAVENFE { get; set; }//STRING
        public Boolean  BENCERRADOCORRETAMENTE{ get; set; }//	BOOLEAN
        public int? STATUALIZADO { get; set; }//BYTE
        public string XDISPLAYINTEGRACAO { get; set; }//STRING
        public string XCODIGOCATEGORIA { get; set; }//STRING
        public int? NCODIGOCONTACORRENTE { get; set; }//INTEGER
        public decimal? VDESCONTOPED { get; set; }//DECIMAL NUMBER
        public decimal? VFRETEPED { get; set; }//DECIMAL NUMBER
        public decimal? VOUTRASPED { get; set; }//DECIMAL NUMBER
        public decimal? VSEGUROPED { get; set; }//DECIMAL NUMBER
        public decimal? VTOTALPROD { get; set; }//DECIMAL NUMBER
        public string XOBSINTERNA { get; set; }//STRING
        public DateTime? DTPREVISTO { get; set; }//DATE
        public string STFRETEPED { get; set; }//STRING
        public int? STVINDODEINTEGRACAO { get; set; }//BYTE
        public string XNOMECONTATO { get; set; }//STRING       

    }
}
