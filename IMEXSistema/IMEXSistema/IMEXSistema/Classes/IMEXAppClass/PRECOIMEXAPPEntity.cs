using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class PRECOIMEXAPPEntity
    {
        public int? IDTABELAPRECO { get; set; }//INTEGER
        public string XNOME	{ get; set; }//STRING
        public int? STVALOR	{ get; set; }//DESCONTO = 0 ACRÉSCIMO = 1 MANUAL = 2 BYTE
        public decimal? PINDICETABELA	{ get; set; }//DECIMAL NUMBER
        public Boolean? STATIVO { get; set; }	//BOOLEAN
        public int? IDEMPRESA { get; set; }//INTEGER
        public int? STDEFAULT	{ get; set; }//BYTE
        public int? IDREPRESENTADA { get; set; }//INTEGER
        public int? STTABELAPRECO { get; set; }//BYTE
        public decimal? PDESCONTOMAXIMO	{ get; set; }//DECIMAL NUMBER
        public int? STCAMPANHAREPRESENTANTE { get; set; }	//BYTE
        public int? STCAMPANHACLIENTE { get; set; }//BYTE
        public int? STCAMPANHACLIENTEUF { get; set; }//BYTE
        public int? STCAMPANHACLIENTERAMOATIVIDADE { get; set; }	//BYTE
        public DateTime? DINICIAL { get; set; }	//DATE
        public DateTime? DFINAL	{ get; set; }//DATE
        public int? STTABELAPRECOREPRESENTACAO { get; set; }	//BYTE
        public DateTime? DTULTIMAALTERACAO { get; set; }	//DATE
        public string XMEUID { get; set; }//STRING
        public decimal? QPRODUTO { get; set; }//DECIMAL NUMBER
        public string IDASPNETUSERSINCLUSAO { get; set; }   //STRING
        public decimal? PCOMISSAO { get; set; }//DECIMAL NUMBER
        public string XTAGCLIENTE { get; set; } //STRING
        public string XUFCLIENTE { get; set; }//STRING

    }
}
