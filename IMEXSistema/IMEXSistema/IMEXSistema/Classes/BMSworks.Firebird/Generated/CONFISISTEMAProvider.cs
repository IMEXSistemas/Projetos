//Template gerado utilizando o MyGeneration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using BMSSoftware;
using BMSworks.Model;
using BmsSoftware;

namespace BMSworks.Firebird
{
	public partial class CONFISISTEMAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CONFISISTEMAProvider()
		{
			dbCnn = null;
			dbCommand = null;
			dbTransaction = null;
		}

		public FbConnection GetConnectionDB()
        {
            FbConnection cnx = new FbConnection();
            cnx.ConnectionString = connectionString;
            return cnx;
        }

		public FbTransaction GetTransaction()
        {
            return dbTransaction;
        }

		public void BeginTransaction()
        {
            try
            {
                if (dbTransaction == null)
                {
                    if (dbCnn == null)
                        dbCnn = (FbConnection)GetConnectionDB();

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbTransaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                }
            }
            catch (Exception e)
            {
                dbTransaction = null;
                throw e;
            }
        }

		 public void BeginTransaction(FbTransaction _dbTransaction)
        {
            try
            {
                if (_dbTransaction != null)
                {
                    dbTransaction = (FbTransaction)_dbTransaction;
                    dbCnn = dbTransaction.Connection;
                }
                else
                {
                    if (dbTransaction == null)
                    {
                        if (dbCnn == null)
                            dbCnn = (FbConnection)GetConnectionDB();

                        if (dbCnn.State == ConnectionState.Closed)
                            dbCnn.Open();

                        dbTransaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                        //Registrando informações da sessão
                    }
                }
            }
            catch (Exception e)
            {
                dbTransaction = null;
                throw e;
            }
        }

		 public void BeginTransaction(IsolationLevel NivelIsolamento)
        {
            try
            {
                if (dbTransaction == null)
                {
                    if (dbCnn == null)
                        dbCnn = (FbConnection)GetConnectionDB();

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbTransaction = dbCnn.BeginTransaction(NivelIsolamento);

                }
            }
            catch (Exception e)
            {
                dbTransaction = null;
                throw e;
            }
        }

		public void EndTransaction()
        {
            try
            {
                // Comita a transação
                if (dbTransaction != null)
                    if (dbTransaction.Connection != null)
                    {
                        dbTransaction.Commit();
                        dbTransaction = null;
                    }
            }
            catch (Exception e)
            {
                this.RollbackTransaction();
                throw e;
            }

            try
            {
                // Fecha a conexão
                if (dbCnn != null)
                    if (dbCnn.State != ConnectionState.Closed)
                        dbCnn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
		
		 public  void CommitTransaction()
		 {
			 try
			 {
				 if (dbTransaction != null)
				 {
					 if (dbTransaction.Connection != null)
						 dbTransaction.Commit();

					 dbTransaction = null;
				  }
			 }
			 catch (Exception e)
			 {
				 this.RollbackTransaction();
				 throw e;
			 }
		 }

		public  void RollbackTransaction()
		{
			try
			{
				if (dbTransaction != null)
					if (dbTransaction.Connection != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
						dbCnn.Close();
					}
			}
			catch (Exception e)
			{
				throw e;
			}
		}	
		
		
		public  int Save(CONFISISTEMAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDCONFIGSISTEMA == -1)
					dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA", Entity.IDCONFIGSISTEMA);
					
					dbCommand.Parameters.AddWithValue("@FLAGLOGORELATORIO", Entity.FLAGLOGORELATORIO); //Coluna 
					
					if(Entity.IDARQUIVOBINARIO1!= null)
						dbCommand.Parameters.AddWithValue("@IDARQUIVOBINARIO1", Entity.IDARQUIVOBINARIO1); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDARQUIVOBINARIO1", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCONFIGBOLETA!= null)
						dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", Entity.IDCONFIGBOLETA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@FLAGCOMPENTREGABOLETA", Entity.FLAGCOMPENTREGABOLETA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCARNEBOLETA", Entity.FLAGCARNEBOLETA); //Coluna 
					dbCommand.Parameters.AddWithValue("@PRAZOOS", Entity.PRAZOOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PRAZOORCAMENTO", Entity.PRAZOORCAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGVENDADEBITO", Entity.FLAGVENDADEBITO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGPEDBAIXAESTOQUE", Entity.FLAGPEDBAIXAESTOQUE); //Coluna 
					dbCommand.Parameters.AddWithValue("@TEMPOGARANTIA", Entity.TEMPOGARANTIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCOMISSAO", Entity.FLAGCOMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@MSGFECHOS", Entity.MSGFECHOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@MSGPEDIDO", Entity.MSGPEDIDO); //Coluna 
					dbCommand.Parameters.AddWithValue("@MSGCONSIGNACAO", Entity.MSGCONSIGNACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGFECHOSESTOQUE", Entity.FLAGFECHOSESTOQUE); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIENF", Entity.SERIENF); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGSOMAIPI", Entity.FLAGSOMAIPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGSOMASEGURO", Entity.FLAGSOMASEGURO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGJANELAS", Entity.FLAGJANELAS); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIENFE", Entity.SERIENFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGSOMAIPINFE", Entity.FLAGSOMAIPINFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGSOMASEGURANFE", Entity.FLAGSOMASEGURANFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCOMISSAONFE", Entity.FLAGCOMISSAONFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@MODELONFE", Entity.MODELONFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALISSQN", Entity.ALISSQN); //Coluna 
					dbCommand.Parameters.AddWithValue("@INSCMUNICIPAL", Entity.INSCMUNICIPAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIPIS", Entity.ALIPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALICOFINS", Entity.ALICOFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGBASEISSQN", Entity.FLAGBASEISSQN); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODMUNIBGE", Entity.CODMUNIBGE); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODUFIBGE", Entity.CODUFIBGE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGAMBIENTENFE", Entity.FLAGAMBIENTENFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIALCERTFDIGITAL", Entity.SERIALCERTFDIGITAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@NAMECERTFDIGITAL", Entity.NAMECERTFDIGITAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALIDADECERTDIGITAL", Entity.VALIDADECERTDIGITAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGLOGONFE", Entity.FLAGLOGONFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@USUARIOPROXY", Entity.USUARIOPROXY); //Coluna 
					dbCommand.Parameters.AddWithValue("@SENHAPROXY", Entity.SENHAPROXY); //Coluna 
					
					if(Entity.IDVERSAOXMLNFE!= null)
						dbCommand.Parameters.AddWithValue("@IDVERSAOXMLNFE", Entity.IDVERSAOXMLNFE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDVERSAOXMLNFE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@NOMEFANTASIA", Entity.NOMEFANTASIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@CNAE", Entity.CNAE); //Coluna 
					dbCommand.Parameters.AddWithValue("@IEST", Entity.IEST); //Coluna 
					dbCommand.Parameters.AddWithValue("@CRT", Entity.CRT); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGALIQIPICONFIS", Entity.FLAGALIQIPICONFIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORTAEMAIL", Entity.PORTAEMAIL); //Coluna 
					dbCommand.Parameters.AddWithValue("@EMAIL", Entity.EMAIL); //Coluna 
					dbCommand.Parameters.AddWithValue("@SMTP", Entity.SMTP); //Coluna 
					dbCommand.Parameters.AddWithValue("@SENHAEMAIL", Entity.SENHAEMAIL); //Coluna 
					dbCommand.Parameters.AddWithValue("@CONFSEGSSL", Entity.CONFSEGSSL); //Coluna 
					dbCommand.Parameters.AddWithValue("@HOSTPROXY", Entity.HOSTPROXY); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORTAPROXY", Entity.PORTAPROXY); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGNFESERVICOS", Entity.FLAGNFESERVICOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@NOTAFISCALINICIAL", Entity.NOTAFISCALINICIAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@MSGINICIALNFE", Entity.MSGINICIALNFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@LARGLAMINA", Entity.LARGLAMINA); //Coluna 
					dbCommand.Parameters.AddWithValue("@NIVELOTIMIZ", Entity.NIVELOTIMIZ); //Coluna 
					dbCommand.Parameters.AddWithValue("@SCHEMAXML", Entity.SCHEMAXML); //Coluna 
					dbCommand.Parameters.AddWithValue("@CASADECPRINTDANFE", Entity.CASADECPRINTDANFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGPLANOCORTE", Entity.FLAGPLANOCORTE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCODREFERENCIA", Entity.FLAGCODREFERENCIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCUPOMFISCAL", Entity.FLAGCUPOMFISCAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGPEDIDOMT", Entity.FLAGPEDIDOMT); //Coluna 
					dbCommand.Parameters.AddWithValue("@ESTOQUENEGATIVO", Entity.ESTOQUENEGATIVO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCPFCNPJPEDIDO", Entity.FLAGCPFCNPJPEDIDO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCPDIGISAT", Entity.FLAGCPDIGISAT); //Coluna 
					dbCommand.Parameters.AddWithValue("@PATHRECEPDIGISAT", Entity.PATHRECEPDIGISAT); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTOQUENF", Entity.FLAGBAIXAESTOQUENF); //Coluna 
					dbCommand.Parameters.AddWithValue("@OPERADORASMS", Entity.OPERADORASMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGLIMITECREDITO", Entity.FLAGLIMITECREDITO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGHABNFE", Entity.FLAGHABNFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGMSGFECHA", Entity.FLAGMSGFECHA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCUPOMFAST", Entity.FLAGCUPOMFAST); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLABACKUP", Entity.FLABACKUP); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCSTECF", Entity.FLAGCSTECF); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCODREFNFE", Entity.FLAGCODREFNFE); //Coluna 
                dbCommand.Parameters.AddWithValue("@TOKENIMEXAPP", Entity.TOKENIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDASPNETUSERSINCLUSAO", Entity.IDASPNETUSERSINCLUSAO); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDEMPRESAIMEXAPP", Entity.IDEMPRESAIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDREPRESIMEXAPP", Entity.IDREPRESIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGIMEXAPP", Entity.FLAGIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTOQUENFCE", Entity.FLAGBAIXAESTOQUENFCE); //Coluna 



                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCONFIGSISTEMA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDCONFIGSISTEMA"].Value.ToString());
				
	
				if (dbTransaction == null)
				{
					dbCommand.Transaction.Commit();
					dbCnn.Close();
				}
			}
			catch (Exception ex)
			{
				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}

			return result;
		}
		
		
		public  int Save(int? IDCONFIGSISTEMA, string FLAGLOGORELATORIO, int IDARQUIVOBINARIO1, int IDCONFIGBOLETA, 
            string FLAGCOMPENTREGABOLETA, string FLAGCARNEBOLETA, int PRAZOOS, int PRAZOORCAMENTO, string FLAGVENDADEBITO,
            string FLAGPEDBAIXAESTOQUE, int TEMPOGARANTIA, string FLAGCOMISSAO, string MSGFECHOS, string MSGPEDIDO, 
            string MSGCONSIGNACAO, string FLAGFECHOSESTOQUE, string SERIENF, string FLAGSOMAIPI, string FLAGSOMASEGURO,
            string FLAGJANELAS, string SERIENFE, string FLAGSOMAIPINFE, string FLAGSOMASEGURANFE, string FLAGCOMISSAONFE, 
            string MODELONFE, decimal ALISSQN, string INSCMUNICIPAL, decimal ALIPIS, decimal ALICOFINS,
            string FLAGBASEISSQN, int CODMUNIBGE, int CODUFIBGE, string FLAGAMBIENTENFE, string SERIALCERTFDIGITAL, 
            string NAMECERTFDIGITAL, string VALIDADECERTDIGITAL, string FLAGLOGONFE, string USUARIOPROXY, 
            string SENHAPROXY, int IDVERSAOXMLNFE, string NOMEFANTASIA, string CNAE, string IEST, 
            string CRT, string FLAGALIQIPICONFIS, int PORTAEMAIL, string EMAIL, string SMTP, string SENHAEMAIL,
            string CONFSEGSSL, string HOSTPROXY, string PORTAPROXY, string FLAGNFESERVICOS, string NOTAFISCALINICIAL,
            string MSGINICIALNFE, int LARGLAMINA, int NIVELOTIMIZ, string SCHEMAXML, string CASADECPRINTDANFE,
            string FLAGPLANOCORTE, string FLAGCODREFERENCIA, string FLAGCUPOMFISCAL, string FLAGPEDIDOMT, 
            string ESTOQUENEGATIVO, string FLAGCPFCNPJPEDIDO, string FLAGCPDIGISAT, string PATHRECEPDIGISAT, 
            string FLAGBAIXAESTOQUENF, string OPERADORASMS, string FLAGLIMITECREDITO, string FLAGHABNFE, string FLAGMSGFECHA, 
            string FLAGCUPOMFAST, string FLABACKUP, string FLAGCSTECF, string FLAGCODREFNFE,
            string TOKENIMEXAPP, string IDASPNETUSERSINCLUSAO, string IDEMPRESAIMEXAPP, string IDREPRESIMEXAPP,
            string FLAGIMEXAPP, string FLAGBAIXAESTOQUENFCE)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDCONFIGSISTEMA == -1)
										dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA", IDCONFIGSISTEMA);
										
										dbCommand.Parameters.AddWithValue("@FLAGLOGORELATORIO", FLAGLOGORELATORIO); //Coluna 
										
										if(IDARQUIVOBINARIO1!= null)
											dbCommand.Parameters.AddWithValue("@IDARQUIVOBINARIO1", IDARQUIVOBINARIO1); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDARQUIVOBINARIO1", DBNull.Value); //ForeignKey 5
										
										
										if(IDCONFIGBOLETA!= null)
											dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", IDCONFIGBOLETA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@FLAGCOMPENTREGABOLETA", FLAGCOMPENTREGABOLETA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCARNEBOLETA", FLAGCARNEBOLETA); //Coluna 
										dbCommand.Parameters.AddWithValue("@PRAZOOS", PRAZOOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PRAZOORCAMENTO", PRAZOORCAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGVENDADEBITO", FLAGVENDADEBITO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGPEDBAIXAESTOQUE", FLAGPEDBAIXAESTOQUE); //Coluna 
										dbCommand.Parameters.AddWithValue("@TEMPOGARANTIA", TEMPOGARANTIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCOMISSAO", FLAGCOMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@MSGFECHOS", MSGFECHOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@MSGPEDIDO", MSGPEDIDO); //Coluna 
										dbCommand.Parameters.AddWithValue("@MSGCONSIGNACAO", MSGCONSIGNACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGFECHOSESTOQUE", FLAGFECHOSESTOQUE); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIENF", SERIENF); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGSOMAIPI", FLAGSOMAIPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGSOMASEGURO", FLAGSOMASEGURO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGJANELAS", FLAGJANELAS); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIENFE", SERIENFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGSOMAIPINFE", FLAGSOMAIPINFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGSOMASEGURANFE", FLAGSOMASEGURANFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCOMISSAONFE", FLAGCOMISSAONFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@MODELONFE", MODELONFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALISSQN", ALISSQN); //Coluna 
										dbCommand.Parameters.AddWithValue("@INSCMUNICIPAL", INSCMUNICIPAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIPIS", ALIPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALICOFINS", ALICOFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGBASEISSQN", FLAGBASEISSQN); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODMUNIBGE", CODMUNIBGE); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODUFIBGE", CODUFIBGE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGAMBIENTENFE", FLAGAMBIENTENFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIALCERTFDIGITAL", SERIALCERTFDIGITAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@NAMECERTFDIGITAL", NAMECERTFDIGITAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALIDADECERTDIGITAL", VALIDADECERTDIGITAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGLOGONFE", FLAGLOGONFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@USUARIOPROXY", USUARIOPROXY); //Coluna 
										dbCommand.Parameters.AddWithValue("@SENHAPROXY", SENHAPROXY); //Coluna 
										
										if(IDVERSAOXMLNFE!= null)
											dbCommand.Parameters.AddWithValue("@IDVERSAOXMLNFE", IDVERSAOXMLNFE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDVERSAOXMLNFE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@NOMEFANTASIA", NOMEFANTASIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@CNAE", CNAE); //Coluna 
										dbCommand.Parameters.AddWithValue("@IEST", IEST); //Coluna 
										dbCommand.Parameters.AddWithValue("@CRT", CRT); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGALIQIPICONFIS", FLAGALIQIPICONFIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORTAEMAIL", PORTAEMAIL); //Coluna 
										dbCommand.Parameters.AddWithValue("@EMAIL", EMAIL); //Coluna 
										dbCommand.Parameters.AddWithValue("@SMTP", SMTP); //Coluna 
										dbCommand.Parameters.AddWithValue("@SENHAEMAIL", SENHAEMAIL); //Coluna 
										dbCommand.Parameters.AddWithValue("@CONFSEGSSL", CONFSEGSSL); //Coluna 
										dbCommand.Parameters.AddWithValue("@HOSTPROXY", HOSTPROXY); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORTAPROXY", PORTAPROXY); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGNFESERVICOS", FLAGNFESERVICOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@NOTAFISCALINICIAL", NOTAFISCALINICIAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@MSGINICIALNFE", MSGINICIALNFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@LARGLAMINA", LARGLAMINA); //Coluna 
										dbCommand.Parameters.AddWithValue("@NIVELOTIMIZ", NIVELOTIMIZ); //Coluna 
										dbCommand.Parameters.AddWithValue("@SCHEMAXML", SCHEMAXML); //Coluna 
										dbCommand.Parameters.AddWithValue("@CASADECPRINTDANFE", CASADECPRINTDANFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGPLANOCORTE", FLAGPLANOCORTE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCODREFERENCIA", FLAGCODREFERENCIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCUPOMFISCAL", FLAGCUPOMFISCAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGPEDIDOMT", FLAGPEDIDOMT); //Coluna 
										dbCommand.Parameters.AddWithValue("@ESTOQUENEGATIVO", ESTOQUENEGATIVO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCPFCNPJPEDIDO", FLAGCPFCNPJPEDIDO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCPDIGISAT", FLAGCPDIGISAT); //Coluna 
										dbCommand.Parameters.AddWithValue("@PATHRECEPDIGISAT", PATHRECEPDIGISAT); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTOQUENF", FLAGBAIXAESTOQUENF); //Coluna 
										dbCommand.Parameters.AddWithValue("@OPERADORASMS", OPERADORASMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGLIMITECREDITO", FLAGLIMITECREDITO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGHABNFE", FLAGHABNFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGMSGFECHA", FLAGMSGFECHA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCUPOMFAST", FLAGCUPOMFAST); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLABACKUP", FLABACKUP); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCSTECF", FLAGCSTECF); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCODREFNFE", FLAGCODREFNFE); //Coluna 

                dbCommand.Parameters.AddWithValue("@TOKENIMEXAPP", TOKENIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDASPNETUSERSINCLUSAO", IDASPNETUSERSINCLUSAO); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDEMPRESAIMEXAPP", IDEMPRESAIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDREPRESIMEXAPP", IDREPRESIMEXAPP); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGIMEXAPP", FLAGIMEXAPP); //Coluna                
                dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTOQUENFCE", FLAGBAIXAESTOQUENFCE); //Coluna                
                

                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCONFIGSISTEMA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDCONFIGSISTEMA"].Value.ToString());
				
				

	
				
	
				if (dbTransaction == null)
				{
					dbCommand.Transaction.Commit();
					dbCnn.Close();
				}
			}
			catch (Exception ex)
			{
				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}

			return result;
		}
		
		
		public  int Delete(int IDCONFIGSISTEMA)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA",IDCONFIGSISTEMA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDCONFIGSISTEMA;

				if (dbTransaction == null)
				{
					dbCommand.Transaction.Commit();
					dbCnn.Close();
				}
			}
			catch (Exception ex)
			{
				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
			return result;
		}

		public  CONFISISTEMAEntity Read(int IDCONFIGSISTEMA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CONFISISTEMA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDCONFIGSISTEMA",IDCONFIGSISTEMA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CONFISISTEMAEntity entity = null;
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						entity = FillEntityObject(ref reader);
					}
				}

				// Deleta reader
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}

				// Fecha conexão
				if (dbTransaction == null)
				{
					dbCommand.Transaction.Commit();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				return entity;
			}
			catch (Exception ex)
			{
				// Deleta reader
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}

				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
		}

		
		public  CONFISISTEMACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CONFISISTEMACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CONFISISTEMA WHERE (";

						ArrayList _rowsFiltro = new ArrayList();
						RowsFiltro.ForEach(delegate(RowsFiltro i)
						{
							string[] item = { i.Condicao.ToString(), i.Campo.ToString(), i.Tipo.ToString(), i.Operador.ToString(), i.Valor.ToString() };
							_rowsFiltro.Add(item);
						});

						int _count = 1;
						foreach (string[] item in _rowsFiltro)
						{
							strSqlCommand += "(" + item[1] + " " + item[3]; 
							switch (item[2])
							{
								case ("System.String"):
									if(item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int16"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int32"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int64"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Double"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Decimal"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Float"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Byte"):
										strSqlCommand += " " + item[4] + ")";
									break;
								case ("System.SByte"):
									strSqlCommand += " " + item[4] + ")";
									break;
								case ("System.Char"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.DateTime"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Guid"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Boolean"): 
									strSqlCommand += " " + item[4] + ")"; 
								break;
							}
							if (_rowsFiltro.Count > 1) 
							{
								if (_count < _rowsFiltro.Count)
								{
									strSqlCommand += " " + item[0] + " ";
								}
								_count++;
							}
						}
						strSqlCommand += ");";

						
					}
					else
					{
						strSqlCommand = "SELECT * FROM CONFISISTEMA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CONFISISTEMA  ";
				}
				
				//Verificando a existência de um transação
						if (dbTransaction != null)
						{
							if (dbCnn.State == ConnectionState.Closed)
								dbCnn.Open();

							dbCommand = new FbCommand(strSqlCommand, dbCnn);
							dbCommand.CommandType = CommandType.Text;
							dbCommand.Transaction = ((FbTransaction)(dbTransaction));
						}
						else
						{
							if(dbCnn == null)
								dbCnn = new FbConnection(connectionString);

							if (dbCnn.State == ConnectionState.Closed)
								dbCnn.Open();

							dbCommand = new FbCommand(strSqlCommand, dbCnn);
							dbCommand.CommandType = CommandType.Text;
							dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
						}


						collection = ExecuteReader(ref collection, ref dataReader, dbCommand);

						if(dataReader != null)
						{
							dataReader.Close();
							dataReader.Dispose();
						}

						if (dbTransaction == null)
						{
							dbCommand.Transaction.Commit();
							dbCnn.Close();
						}

						return collection;
				
				
				
			}
			catch (Exception ex)
			{
				// Deleta reader
				if (dataReader != null)
				{
					dataReader.Close();
					dataReader.Dispose();
				}

				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
		}
		
		public  CONFISISTEMACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CONFISISTEMACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CONFISISTEMA WHERE (";

						ArrayList _rowsFiltro = new ArrayList();
						RowsFiltro.ForEach(delegate(RowsFiltro i)
						{
							string[] item = { i.Condicao.ToString(), i.Campo.ToString(), i.Tipo.ToString(), i.Operador.ToString(), i.Valor.ToString() };
							_rowsFiltro.Add(item);
						});

						int _count = 1;
						foreach (string[] item in _rowsFiltro)
						{
							strSqlCommand += "(" + item[1] + " " + item[3]; 
							switch (item[2])
							{
								case ("System.String"):
									if(item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int16"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int32"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Int64"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Double"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Decimal"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Float"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " " + item[4] + ")";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Byte"):
										strSqlCommand += " " + item[4] + ")";
									break;
								case ("System.SByte"):
									strSqlCommand += " " + item[4] + ")";
									break;
								case ("System.Char"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.DateTime"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Guid"):
									if (item[3].ToUpper() != "LIKE")
										strSqlCommand += " '" + item[4] + "')";
									else
										strSqlCommand += " '%" + item[4] + "%')";
									break;
								case ("System.Boolean"): 
									strSqlCommand += " " + item[4] + ")"; 
								break;
							}
							if (_rowsFiltro.Count > 1) 
							{
								if (_count < _rowsFiltro.Count)
								{
									strSqlCommand += " " + item[0] + " ";
								}
								_count++;
							}
						}
						strSqlCommand += ")  order by  " + FieldOrder;

						
					}
					else
					{
						strSqlCommand = "SELECT * FROM CONFISISTEMA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CONFISISTEMA  order by " + FieldOrder;
				}
				
				//Verificando a existência de um transação
						if (dbTransaction != null)
						{
							if (dbCnn.State == ConnectionState.Closed)
								dbCnn.Open();

							dbCommand = new FbCommand(strSqlCommand, dbCnn);
							dbCommand.CommandType = CommandType.Text;
							dbCommand.Transaction = ((FbTransaction)(dbTransaction));
						}
						else
						{
							if(dbCnn == null)
								dbCnn = new FbConnection(connectionString);

							if (dbCnn.State == ConnectionState.Closed)
								dbCnn.Open();

							dbCommand = new FbCommand(strSqlCommand, dbCnn);
							dbCommand.CommandType = CommandType.Text;
							dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
						}


						collection = ExecuteReader(ref collection, ref dataReader, dbCommand);

						if(dataReader != null)
						{
							dataReader.Close();
							dataReader.Dispose();
						}

						if (dbTransaction == null)
						{
							dbCommand.Transaction.Commit();
							dbCnn.Close();
						}

						return collection;
				
				
				
			}
			catch (Exception ex)
			{
				// Deleta reader
				if (dataReader != null)
				{
					dataReader.Close();
					dataReader.Dispose();
				}

				if (dbTransaction != null)
					this.RollbackTransaction();
				else
				{
					if (dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
		}

		private static CONFISISTEMACollection ExecuteReader(ref CONFISISTEMACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CONFISISTEMACollection();

				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						collection.Add(FillEntityObject(ref dataReader));
					}
				}

				if (!(dataReader.IsClosed))
				{
					dataReader.Close();
				}
				dataReader.Dispose();
			}

			return collection;
		}

		private static CONFISISTEMAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CONFISISTEMAEntity entity = new CONFISISTEMAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDCONFIGSISTEMA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCONFIGSISTEMA"));
			entity.FLAGLOGORELATORIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGLOGORELATORIO"));
			entity.IDARQUIVOBINARIO1 = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDARQUIVOBINARIO1"));
			entity.IDCONFIGBOLETA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCONFIGBOLETA"));
			entity.FLAGCOMPENTREGABOLETA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCOMPENTREGABOLETA"));
			entity.FLAGCARNEBOLETA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCARNEBOLETA"));
			entity.PRAZOOS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("PRAZOOS"));
			entity.PRAZOORCAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("PRAZOORCAMENTO"));
			entity.FLAGVENDADEBITO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGVENDADEBITO"));
			entity.FLAGPEDBAIXAESTOQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGPEDBAIXAESTOQUE"));
			entity.TEMPOGARANTIA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("TEMPOGARANTIA"));
			entity.FLAGCOMISSAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCOMISSAO"));
			entity.MSGFECHOS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MSGFECHOS"));
			entity.MSGPEDIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MSGPEDIDO"));
			entity.MSGCONSIGNACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MSGCONSIGNACAO"));
			entity.FLAGFECHOSESTOQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGFECHOSESTOQUE"));
			entity.SERIENF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIENF"));
			entity.FLAGSOMAIPI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSOMAIPI"));
			entity.FLAGSOMASEGURO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSOMASEGURO"));
			entity.FLAGJANELAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGJANELAS"));
			entity.SERIENFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIENFE"));
			entity.FLAGSOMAIPINFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSOMAIPINFE"));
			entity.FLAGSOMASEGURANFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSOMASEGURANFE"));
			entity.FLAGCOMISSAONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCOMISSAONFE"));
			entity.MODELONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MODELONFE"));
			entity.ALISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALISSQN"));
			entity.INSCMUNICIPAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSCMUNICIPAL"));
			entity.ALIPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIPIS"));
			entity.ALICOFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALICOFINS"));
			entity.FLAGBASEISSQN = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGBASEISSQN"));
			entity.CODMUNIBGE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODMUNIBGE"));
			entity.CODUFIBGE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODUFIBGE"));
			entity.FLAGAMBIENTENFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGAMBIENTENFE"));
			entity.SERIALCERTFDIGITAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIALCERTFDIGITAL"));
			entity.NAMECERTFDIGITAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NAMECERTFDIGITAL"));
			entity.VALIDADECERTDIGITAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("VALIDADECERTDIGITAL"));
			entity.FLAGLOGONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGLOGONFE"));
			entity.USUARIOPROXY = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("USUARIOPROXY"));
			entity.SENHAPROXY = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SENHAPROXY"));
			entity.IDVERSAOXMLNFE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVERSAOXMLNFE"));
			entity.NOMEFANTASIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEFANTASIA"));
			entity.CNAE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNAE"));
			entity.IEST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IEST"));
			entity.CRT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CRT"));
			entity.FLAGALIQIPICONFIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGALIQIPICONFIS"));
			entity.PORTAEMAIL = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("PORTAEMAIL"));
			entity.EMAIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMAIL"));
			entity.SMTP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SMTP"));
			entity.SENHAEMAIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SENHAEMAIL"));
			entity.CONFSEGSSL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONFSEGSSL"));
			entity.HOSTPROXY = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HOSTPROXY"));
			entity.PORTAPROXY = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PORTAPROXY"));
			entity.FLAGNFESERVICOS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGNFESERVICOS"));
			entity.NOTAFISCALINICIAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTAFISCALINICIAL"));
			entity.MSGINICIALNFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MSGINICIALNFE"));
			entity.LARGLAMINA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("LARGLAMINA"));
			entity.NIVELOTIMIZ = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("NIVELOTIMIZ"));
			entity.SCHEMAXML = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SCHEMAXML"));
			entity.CASADECPRINTDANFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CASADECPRINTDANFE"));
			entity.FLAGPLANOCORTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGPLANOCORTE"));
			entity.FLAGCODREFERENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCODREFERENCIA"));
			entity.FLAGCUPOMFISCAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCUPOMFISCAL"));
			entity.FLAGPEDIDOMT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGPEDIDOMT"));
			entity.ESTOQUENEGATIVO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESTOQUENEGATIVO"));
			entity.FLAGCPFCNPJPEDIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCPFCNPJPEDIDO"));
			entity.FLAGCPDIGISAT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCPDIGISAT"));
			entity.PATHRECEPDIGISAT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PATHRECEPDIGISAT"));
			entity.FLAGBAIXAESTOQUENF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGBAIXAESTOQUENF"));
			entity.OPERADORASMS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OPERADORASMS"));
			entity.FLAGLIMITECREDITO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGLIMITECREDITO"));
			entity.FLAGHABNFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGHABNFE"));
			entity.FLAGMSGFECHA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGMSGFECHA"));
			entity.FLAGCUPOMFAST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCUPOMFAST"));
			entity.FLABACKUP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLABACKUP"));
			entity.FLAGCSTECF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCSTECF"));
			entity.FLAGCODREFNFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCODREFNFE"));
            entity.TOKENIMEXAPP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TOKENIMEXAPP"));
            entity.IDASPNETUSERSINCLUSAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IDASPNETUSERSINCLUSAO"));
            entity.IDEMPRESAIMEXAPP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IDEMPRESAIMEXAPP"));
            entity.IDREPRESIMEXAPP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IDREPRESIMEXAPP"));
            entity.FLAGIMEXAPP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGIMEXAPP"));
            entity.FLAGIMEXAPP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGIMEXAPP"));
            entity.FLAGBAIXAESTOQUENFCE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGBAIXAESTOQUENFCE"));
            

            return entity;
		}
	}
}
