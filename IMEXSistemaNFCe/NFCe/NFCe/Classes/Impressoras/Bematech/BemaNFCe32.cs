using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBemaNFCe
{
    class BemaNFCe32
    {
        #region IMPORT DAS FUNÇÕES BemaNFCe32.dll

        public const string DLL_NFCe = "BemaNFCe32.dll";

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_DadosEmissor(string CNPJ, string name, string tradeName, string address, string number, string neighborhood,
            string IBGECode, string city, string UF, string CEP, string countryCode, string country, string phone,
            string stateRegistration, string stateRegistrationST, string municipalRegistration);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_DadosConsumidor(string CPF_CNPJ, string name, string address, string complement, string number, string neighborhood,
            string IBGECode, string city, string UF, string CEP, string countryCode, string country, string phone,
            string stateRegistrationIndex, string stateRegistration, string SUFRAMACode, string email);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_AbreNota(string CPF_CNPJ, string serie, string nf);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_VendeItem(string code, string EAN13, string description, string NCM, string CFOP, string unitOfMeasure,
            string quantity, string decimalsQuantity, string unitaryValue, string decimalsUnitaryValue, string grossValue,
            string incrementValue, string discountValue, string netValue, string productOrigin, string additionalInformation);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_InsereTributacaoICMS(string item, string CST_ICMS, string basisCalculationMode,
            string basisCalculationReductionPercentual, string basisCalculationValue, string tax, string taxValue,
            string ICMSSTBasisCalculationMode, string ICMSSTValueAddedMarginPercentual, string ICMSSTBasisCalculationReductionPercentual,
            string ICMSSTBasisCalculationReductionValue, string ICMSSTTax, string ICMSSTValue, string basisCalculationValueRetained,
            string ICMSValueRetained, string ICMSUnencumberedValue, string ICMSUnburdeningMotive, string incidentTaxTotalValue);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_InsereTributacaoSIMPLES(string item, string CSOSN, string basisCalculationMode,
            string basisCalculationReductionPercentual, string basisCalculationValue, string tax, string taxValue,
            string ICMSSTBasisCalculationMode, string ICMSSTValueAddedMarginPercentual, string ICMSSTBasisCalculationReductionPercentual,
            string ICMSSTBasisCalculationReductionValue, string ICMSSTTax, string ICMSSTValue, string basisCalculationValueRetained,
            string ICMSValueRetained, string creditCalculationApplicableTax, string ICMSSNCreditValue, string incidentTaxTotalValue);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_InsereTributacaoPIS(string item, string CST_PIS, string PISBasisCalculation, string PISTax,
            string PISValue, string PISQuantitySell, string PISTaxValue, string PISIncidentTaxValue);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_InsereTributacaoCOFINS(string item, string CST_COFINS, string COFINSBasisCalculation,
            string COFINSTax, string COFINSValue, string COFINSQuantitySell, string COFINSTaxValue, string COFINSIncidentTaxValue);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_AcrescimoDescontoItem(string item, string incrementTotalValue, string discountTotalValue,
            string newNetValue, string newBasisCalculation, string newTaxValue);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_CancelaItem(string item);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_EfetuaFormaPagamento(string paymentFormIndex, string value);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_CancelaFormaPagamento(string paymentSequence);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_CancelaNota();

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_FechaNota(string promotionalMessage, string changeValue, string taxValue,
            string DANFELayout, string DANFEOut, string email);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_InutilizaNota(string serie, string nf, string reason);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_StatusInutilizaNota(string serie, string nf, string SEFAZReturnCode, string protocol, string dateHourProtocol);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_ReimprimeDANFEChave(string accessKey);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_ReimprimeDANFE(string serie, string nf);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_StatusNFCe(string serie, string nf, string SEFAZReturnCode,
            string keyAccess, string protocol, string dateHourProtocol);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_StatusUltimaNFCe(string serie, string nf, string SEFAZReturnCode,
            string keyAccess, string protocol, string dateHourProtocol);

        [DllImport(DLL_NFCe)]
        public static extern int Bematech_NFCe_ImprimeTextoLivre(string filename);

        #endregion
    }
}
