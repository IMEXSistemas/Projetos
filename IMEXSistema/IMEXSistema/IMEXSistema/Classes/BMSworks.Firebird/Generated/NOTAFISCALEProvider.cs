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
	public partial class NOTAFISCALEProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~NOTAFISCALEProvider()
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
		
		
		public  int Save(NOTAFISCALEEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDNOTAFISCALE == -1)
					dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE", Entity.IDNOTAFISCALE);
					
					dbCommand.Parameters.AddWithValue("@NOTAFISCALE", Entity.NOTAFISCALE); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIE", Entity.SERIE); //Coluna 
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DTSAIDA", Entity.DTSAIDA); //Coluna 
					dbCommand.Parameters.AddWithValue("@HORASAIDA", Entity.HORASAIDA); //Coluna 
					
					if(Entity.IDTIPOMOVIM!= null)
						dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", Entity.IDTIPOMOVIM); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCFOP!= null)
						dbCommand.Parameters.AddWithValue("@IDCFOP", Entity.IDCFOP); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@INSCESTSTRIB", Entity.INSCESTSTRIB); //Coluna 
					dbCommand.Parameters.AddWithValue("@BASECALCICMS", Entity.BASECALCICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORICMS", Entity.VALORICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@BASECALCICMSLSUB", Entity.BASECALCICMSLSUB); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORICMSSUB", Entity.VALORICMSSUB); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORFRETE", Entity.VALORFRETE); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORSEGURO", Entity.VALORSEGURO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OUTRADESPES", Entity.OUTRADESPES); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALIPI", Entity.TOTALIPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", Entity.TOTALPRODUTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALNOTA", Entity.TOTALNOTA); //Coluna 
					
					if(Entity.IDVENDEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", Entity.IDVENDEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", Entity.VALORCOMISSAO); //Coluna 
					
					if(Entity.IDTRANSPORTES!= null)
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", Entity.IDTRANSPORTES); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@PLACA", Entity.PLACA); //Coluna 
					dbCommand.Parameters.AddWithValue("@UFTRANSPORTE", Entity.UFTRANSPORTE); //Coluna 
					dbCommand.Parameters.AddWithValue("@QUANT", Entity.QUANT); //Coluna 
					dbCommand.Parameters.AddWithValue("@ESPECIE", Entity.ESPECIE); //Coluna 
					dbCommand.Parameters.AddWithValue("@MARCANFE", Entity.MARCANFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@NUMEROS", Entity.NUMEROS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PESOBRUTO", Entity.PESOBRUTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PESOLIQUIDO", Entity.PESOLIQUIDO); //Coluna 
					dbCommand.Parameters.AddWithValue("@INFOCOMPLEM", Entity.INFOCOMPLEM); //Coluna 
					
					if(Entity.IDCENTROCUSTO!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFORMAPAGTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", Entity.IDFORMAPAGTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDLOCALCOBRANCA!= null)
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@FRETE", Entity.FRETE); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", Entity.PORCACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", Entity.VALORACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORPIS", Entity.VALORPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORCONFINS", Entity.VALORCONFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODANTT", Entity.CODANTT); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORTOTALSERVICO", Entity.VALORTOTALSERVICO); //Coluna 
					dbCommand.Parameters.AddWithValue("@BASECALCISSQN", Entity.BASECALCISSQN); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQISSQN", Entity.ALIQISSQN); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORISSQN", Entity.VALORISSQN); //Coluna 
					dbCommand.Parameters.AddWithValue("@CHAVEACESSO", Entity.CHAVEACESSO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGARQUIVOXML", Entity.FLAGARQUIVOXML); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGASSINATURA", Entity.FLAGASSINATURA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCANCELADA", Entity.FLAGCANCELADA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGENVIADA", Entity.FLAGENVIADA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGINUTILIZADO", Entity.FLAGINUTILIZADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGVALIDADA", Entity.FLAGVALIDADA); //Coluna 
					dbCommand.Parameters.AddWithValue("@ARQUIVOLOTE", Entity.ARQUIVOLOTE); //Coluna 
					dbCommand.Parameters.AddWithValue("@RECIBONFE", Entity.RECIBONFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@SITUACAONFE", Entity.SITUACAONFE); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQCREDICMS", Entity.ALIQCREDICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORCREDICMS", Entity.VALORCREDICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGTIPOPAGAMENTO", Entity.FLAGTIPOPAGAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@NUMPEDIDO", Entity.NUMPEDIDO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGDEVOLUCAO", Entity.FLAGDEVOLUCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CHAVEDEVOLUCAO", Entity.CHAVEDEVOLUCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CNPJEMISSOR", Entity.CNPJEMISSOR); //Coluna 
                dbCommand.Parameters.AddWithValue("@INFADFISCO", Entity.INFADFISCO); //Coluna 
                



                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTAFISCALE"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDNOTAFISCALE"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDNOTAFISCALE, string NOTAFISCALE, string SERIE, int IDCLIENTE, DateTime DTEMISSAO, DateTime DTSAIDA, 
                        string HORASAIDA, int IDTIPOMOVIM, int IDCFOP, string INSCESTSTRIB, decimal BASECALCICMS, decimal VALORICMS, 
                        decimal BASECALCICMSLSUB, decimal VALORICMSSUB, decimal VALORFRETE, decimal VALORSEGURO, decimal OUTRADESPES, 
                        decimal TOTALIPI, decimal TOTALPRODUTOS, decimal TOTALNOTA, int IDVENDEDOR, decimal VALORCOMISSAO, int IDTRANSPORTES, 
                        string PLACA, string UFTRANSPORTE, decimal QUANT, string ESPECIE, string MARCANFE, string NUMEROS, decimal PESOBRUTO, 
                        decimal PESOLIQUIDO, string INFOCOMPLEM, int IDCENTROCUSTO, int IDFORMAPAGTO, int IDLOCALCOBRANCA, int IDSTATUS, 
                        decimal VALORPAGO, decimal VALORDEVEDOR, int FRETE, decimal PORCDESCONTO, decimal VALORDESCONTO, decimal PORCACRESCIMO, 
                        decimal VALORACRESCIMO, decimal VALORPIS, decimal VALORCONFINS, string CODANTT, decimal VALORTOTALSERVICO, 
                        decimal BASECALCISSQN, decimal ALIQISSQN, decimal VALORISSQN, string CHAVEACESSO, string FLAGARQUIVOXML, 
                        string FLAGASSINATURA, string FLAGCANCELADA, string FLAGENVIADA, string FLAGINUTILIZADO, string FLAGVALIDADA, 
                        string ARQUIVOLOTE, string RECIBONFE, string SITUACAONFE, decimal ALIQCREDICMS, decimal VALORCREDICMS, string FLAGTIPOPAGAMENTO, 
                        string NUMPEDIDO, string FLAGDEVOLUCAO, string CHAVEDEVOLUCAO, string CNPJEMISSOR, string INFADFISCO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDNOTAFISCALE == -1)
										dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE", IDNOTAFISCALE);
										
										dbCommand.Parameters.AddWithValue("@NOTAFISCALE", NOTAFISCALE); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //Coluna 
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DTSAIDA", DTSAIDA); //Coluna 
										dbCommand.Parameters.AddWithValue("@HORASAIDA", HORASAIDA); //Coluna 
										
										if(IDTIPOMOVIM!= null)
											dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", IDTIPOMOVIM); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", DBNull.Value); //ForeignKey 5
										
										
										if(IDCFOP!= null)
											dbCommand.Parameters.AddWithValue("@IDCFOP", IDCFOP); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@INSCESTSTRIB", INSCESTSTRIB); //Coluna 
										dbCommand.Parameters.AddWithValue("@BASECALCICMS", BASECALCICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORICMS", VALORICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@BASECALCICMSLSUB", BASECALCICMSLSUB); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORICMSSUB", VALORICMSSUB); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORFRETE", VALORFRETE); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORSEGURO", VALORSEGURO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OUTRADESPES", OUTRADESPES); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALIPI", TOTALIPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", TOTALPRODUTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALNOTA", TOTALNOTA); //Coluna 
										
										if(IDVENDEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", IDVENDEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", VALORCOMISSAO); //Coluna 
										
										if(IDTRANSPORTES!= null)
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", IDTRANSPORTES); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@PLACA", PLACA); //Coluna 
										dbCommand.Parameters.AddWithValue("@UFTRANSPORTE", UFTRANSPORTE); //Coluna 
										dbCommand.Parameters.AddWithValue("@QUANT", QUANT); //Coluna 
										dbCommand.Parameters.AddWithValue("@ESPECIE", ESPECIE); //Coluna 
										dbCommand.Parameters.AddWithValue("@MARCANFE", MARCANFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@NUMEROS", NUMEROS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PESOBRUTO", PESOBRUTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PESOLIQUIDO", PESOLIQUIDO); //Coluna 
										dbCommand.Parameters.AddWithValue("@INFOCOMPLEM", INFOCOMPLEM); //Coluna 
										
										if(IDCENTROCUSTO!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDFORMAPAGTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", IDFORMAPAGTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@FRETE", FRETE); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", PORCACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", VALORACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORPIS", VALORPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORCONFINS", VALORCONFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODANTT", CODANTT); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORTOTALSERVICO", VALORTOTALSERVICO); //Coluna 
										dbCommand.Parameters.AddWithValue("@BASECALCISSQN", BASECALCISSQN); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQISSQN", ALIQISSQN); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORISSQN", VALORISSQN); //Coluna 
										dbCommand.Parameters.AddWithValue("@CHAVEACESSO", CHAVEACESSO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGARQUIVOXML", FLAGARQUIVOXML); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGASSINATURA", FLAGASSINATURA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCANCELADA", FLAGCANCELADA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGENVIADA", FLAGENVIADA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGINUTILIZADO", FLAGINUTILIZADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGVALIDADA", FLAGVALIDADA); //Coluna 
										dbCommand.Parameters.AddWithValue("@ARQUIVOLOTE", ARQUIVOLOTE); //Coluna 
										dbCommand.Parameters.AddWithValue("@RECIBONFE", RECIBONFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@SITUACAONFE", SITUACAONFE); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQCREDICMS", ALIQCREDICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORCREDICMS", VALORCREDICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGTIPOPAGAMENTO", FLAGTIPOPAGAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@NUMPEDIDO", NUMPEDIDO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGDEVOLUCAO", FLAGDEVOLUCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CHAVEDEVOLUCAO", CHAVEDEVOLUCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CNPJEMISSOR", CNPJEMISSOR); //Coluna 
                dbCommand.Parameters.AddWithValue("@INFADFISCO", INFADFISCO); //Coluna 

                



                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTAFISCALE"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDNOTAFISCALE"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDNOTAFISCALE)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE",IDNOTAFISCALE); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDNOTAFISCALE;

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

		public  NOTAFISCALEEntity Read(int IDNOTAFISCALE)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTAFISCALE",IDNOTAFISCALE); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				NOTAFISCALEEntity entity = null;
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

		
		public  NOTAFISCALECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			NOTAFISCALECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTAFISCALE WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTAFISCALE  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTAFISCALE  ";
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
		
		public  NOTAFISCALECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			NOTAFISCALECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTAFISCALE WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTAFISCALE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTAFISCALE  order by " + FieldOrder;
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

		private static NOTAFISCALECollection ExecuteReader(ref NOTAFISCALECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new NOTAFISCALECollection();

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

		private static NOTAFISCALEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			NOTAFISCALEEntity entity = new NOTAFISCALEEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDNOTAFISCALE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDNOTAFISCALE"));
			entity.NOTAFISCALE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTAFISCALE"));
			entity.SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTSAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTSAIDA"));
			entity.HORASAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORASAIDA"));
			entity.IDTIPOMOVIM = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOMOVIM"));
			entity.IDCFOP = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.INSCESTSTRIB = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSCESTSTRIB"));
			entity.BASECALCICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCICMS"));
			entity.VALORICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMS"));
			entity.BASECALCICMSLSUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCICMSLSUB"));
			entity.VALORICMSSUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMSSUB"));
			entity.VALORFRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORFRETE"));
			entity.VALORSEGURO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORSEGURO"));
			entity.OUTRADESPES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTRADESPES"));
			entity.TOTALIPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALIPI"));
			entity.TOTALPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPRODUTOS"));
			entity.TOTALNOTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALNOTA"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.PLACA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PLACA"));
			entity.UFTRANSPORTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UFTRANSPORTE"));
			entity.QUANT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANT"));
			entity.ESPECIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIE"));
			entity.MARCANFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCANFE"));
			entity.NUMEROS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROS"));
			entity.PESOBRUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOBRUTO"));
			entity.PESOLIQUIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOLIQUIDO"));
			entity.INFOCOMPLEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INFOCOMPLEM"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.FRETE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("FRETE"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));
			entity.VALORPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPIS"));
			entity.VALORCONFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCONFINS"));
			entity.CODANTT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODANTT"));
			entity.VALORTOTALSERVICO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTOTALSERVICO"));
			entity.BASECALCISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCISSQN"));
			entity.ALIQISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQISSQN"));
			entity.VALORISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORISSQN"));
			entity.CHAVEACESSO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEACESSO"));
			entity.FLAGARQUIVOXML = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGARQUIVOXML"));
			entity.FLAGASSINATURA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGASSINATURA"));
			entity.FLAGCANCELADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCANCELADA"));
			entity.FLAGENVIADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGENVIADA"));
			entity.FLAGINUTILIZADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGINUTILIZADO"));
			entity.FLAGVALIDADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGVALIDADA"));
			entity.ARQUIVOLOTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ARQUIVOLOTE"));
			entity.RECIBONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RECIBONFE"));
			entity.SITUACAONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAONFE"));
			entity.ALIQCREDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQCREDICMS"));
			entity.VALORCREDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCREDICMS"));
			entity.FLAGTIPOPAGAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTIPOPAGAMENTO"));
			entity.NUMPEDIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMPEDIDO"));
			entity.FLAGDEVOLUCAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGDEVOLUCAO"));
			entity.CHAVEDEVOLUCAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEDEVOLUCAO"));
			entity.CNPJEMISSOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJEMISSOR"));
            entity.INFADFISCO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INFADFISCO"));           


            return entity;
		}
	}
}
