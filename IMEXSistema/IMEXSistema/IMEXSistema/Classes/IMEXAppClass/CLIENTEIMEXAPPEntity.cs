using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class CLIENTEIMEXAPPEntity
    {
        public int? IDCLIENTES { get; set; }//INTEGER
        public int? STJURIDICO { get; set; }	//BYTE
        public Boolean? STATIVO { get; set; }//BOOLEAN
        public string CALTERNATIVO { get; set; }	//STRING
        public string XRAZAOSOCIAL { get; set; }	//STRING
        public string XFANTASIA { get; set; }	//STRING
        public string XCPFCNPJ { get; set; }//STRING
        public string XRGIE { get; set; }	//STRING
        public int? IDRAMOATIVIDADE { get; set; }	//INTEGER
        public string STPROSPECCAO { get; set; }//STRING
        public int? QVISITACLIENTE { get; set; }//INTEGER
        public int? STPERIODOVISITACLIENTE { get; set; }  //BYTE
        public int? IDCONDICAOPAGAMENTO { get; set; }//INTEGER
        public string XANOTACAO { get; set; }   //STRING
        public int? IDTABELAPRECO { get; set; }//INTEGER
        public DateTime? DEFETIVACAO { get; set; }//DATE
        public int? IDEMPRESA { get; set; }//INTEGER
        public int? IDTRANSPORTADORA { get; set; }//INTEGER
        public string XTELEFONES { get; set; }  //STRING
        public string XEMAIL { get; set; }//STRING
        public DateTime? DTULTIMAALTERACAO { get; set; }    //DATE
        public int? IDEMPRESA_ASPNETUSERS { get; set; } //INTEGER
        public string IDASPNETUSERS { get; set; }//STRING
        public DateTime? DTCADASTRO { get; set; }//DATE
        public int? IDIMPORTACAO { get; set; }//INTEGER
        public decimal? VLIMITECREDITO	 { get; set; }//DECIMAL NUMBER
        public int? STATUALIZADO { get; set; }//BYTE
        public string XWEBSITE { get; set; }//STRING
        public Boolean? BEXIBIRANOTACAONOPEDIDO { get; set; }//BOOLEAN
        public string XMEUID { get; set; }//STRING
        public DateTime? DTNASCIMENTO { get; set; }//DATE
        public string CNAE { get; set; }//STRING
        public string IDASPNETUSERSINCLUSAO { get; set; }//STRING
        public int? IDINTEGRACAO { get; set; }//INTEGER
        public int? STCONTRIBUINTE { get; set; }//BYTE
        public string XIDMAPS { get; set; }//STRING
        public int? IDCNAE { get; set; }//INTEGER
        public int? STCLIENTEAPLICACAO { get; set; }//BYTE

    }
}
