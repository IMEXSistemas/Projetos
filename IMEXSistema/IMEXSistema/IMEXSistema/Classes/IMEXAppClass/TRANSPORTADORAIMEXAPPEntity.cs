using System;
using System.Collections.Generic;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.IMEXAppClass
{
    public class TRANSPORTADORAIMEXAPPEntity
    {

        public int? IDTRANSPORTADORA { get; set; }
        public string XRAZAOSOCIAL{ get; set; }
        public string XFANTASIA { get; set; }
        public string XCNPJ { get; set; }
        public string XIE { get; set; }
        public string XANOTACAO { get; set; }
        public string XEMAILS { get; set; }
        public string XTELEFONES { get; set; }
        public int IDEMPRESA { get; set; }
        public Boolean STATIVO { get; set; }
        public DateTime? DTULTIMAALTERACAO { get; set; }
        public string XMEUID { get; set; }
        public string IDASPNETUSERSINCLUSAO { get; set; }
        public int IDINTEGRACAO { get; set; }
        public int STATUALIZADO { get; set; }
        public List<ENDERECOIMEXAPPEntity> ENDERECO { get; set; }
    }
   
}
