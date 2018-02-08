using System;
using System.Collections.Generic;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class PRODUTODATAMODELIMEXAPPEntity
    {
        public int? IDPRODUTO  { get; set; }// INTEGER
        public int? IDREPRESENTADA  { get; set; }//INTEGER
        public int? IDCATEGORIA  { get; set; }//INTEGER
        public int? IDUNIDADEMEDIDA  { get; set; }//INTEGER
        public int? IDEMPRESA  { get; set; }//INTEGER
        public string CALTERNATIVO  { get; set; }//STRING
        public string XNOME { get; set; } //STRING
        public decimal? VCOMPRA  { get; set; } //DECIMAL NUMBER
        public decimal? PICMS { get; set; }//DECIMAL NUMBER
        public decimal? PIPI { get; set; }// DECIMAL NUMBER
        public decimal? PST { get; set; }// DECIMAL NUMBER
        public decimal? POUTRAS { get; set; }//DECIMAL NUMBER
        public decimal? VCUSTO  { get; set; }//DECIMAL NUMBER
        public decimal? VESTOQUEMAX { get; set; }//DECIMAL NUMBER
        public decimal? VESTOQUEMIN { get; set; }//DECIMAL NUMBER
        public string XANOTACAO { get; set; }//STRING
        public string XFABRICANTE { get; set; }//STRING
        public decimal? PCOMISSAO { get; set; }//DECIMAL NUMBER
        public Boolean? STVENDASEMESTOQUE { get; set; }//BOOLEAN
        public Boolean? STATIVO { get; set; }//BOOLEAN
        public string XFILEIMAGEPRINCIPAL { get; set; }//STRING
        public decimal? VVENDA { get; set; }//DECIMAL NUMBER
        public decimal? PLUCRO { get; set; }//DECIMAL NUMBER
        public DateTime? DTULTIMAALTERACAO { get; set; }//DATE
        public string XMEUID { get; set; }//STRING
        public string IDASPNETUSERSINCLUSAO { get; set; }// STRING
        public DateTime? DTCADASTRO { get; set; }//DATE
        public decimal? PIPIVENDA { get; set; }//DECIMAL NUMBER
        public decimal? PSTVENDA { get; set; }//DECIMAL NUMBER
        public string XNCM { get; set; }//STRING
        public string CEAN { get; set; }//STRING
        public string XCODCSTPIS { get; set; }//STRING
        public string XCODCSTCOFINS { get; set; }//STRING
        public decimal? DALIQOPICMSST { get; set; }//DECIMAL NUMBER
        public string XCODCSTIPI { get; set; }//STRING
        public string XCODCSTICMS { get; set; }//STRING
        public string XORIGEM { get; set; }//STRING
        public string XCFOP { get; set; }//STRING
        public string XCFOP_INTER { get; set; }//STRING
        public decimal? DPESOLIQ { get; set; }//DECIMAL NUMBER
        public decimal? DPESOBRUTO { get; set; }//DECIMAL NUMBER
        public string CEANEMB { get; set; }//STRING
        public string XNOMEDET { get; set; }// STRING
        public string CCEST { get; set; }//STRING
        public string CORIGEM { get; set; }//STRING
        public Boolean? STATUALIZADO { get; set; }//BOOLEAN
        public Boolean? BEXIBIRANOTACAONOPEDIDO { get; set; }// BOOLEAN
        public int? IDIMPORTACAO { get; set; }//INTEGER
        public Boolean? BEXIBIRCATALOGO { get; set; }// BOOLEAN
        public Boolean? BDESTAQUECATALOGO { get; set; }//BOOLEAN
        public string XDETALHESCATALOGO { get; set; }//STRING
        public string XTAMANHOSCATALOGO { get; set; }//STRING
        public string XINFTECNICASCATALOGO { get; set; }// STRING
        public DateTime? DTATIVACAOCATALOGO { get; set; }//DATE    

    }
}
