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
	public partial class PRODUTOSProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PRODUTOSProvider()
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
		
		
		public  int Save(PRODUTOSEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDPRODUTO == -1)
					dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDPRODUTO", Entity.IDPRODUTO);
					
					dbCommand.Parameters.AddWithValue("@NOMEPRODUTO", Entity.NOMEPRODUTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODPRODUTOFORNECEDOR", Entity.CODPRODUTOFORNECEDOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODBARRA", Entity.CODBARRA); //Coluna 
					dbCommand.Parameters.AddWithValue("@LOCALIZACAO", Entity.LOCALIZACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATACADASTRO", Entity.DATACADASTRO); //Coluna 
					
					if(Entity.IDUNIDADE!= null)
						dbCommand.Parameters.AddWithValue("@IDUNIDADE", Entity.IDUNIDADE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDUNIDADE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDMARCA!= null)
						dbCommand.Parameters.AddWithValue("@IDMARCA", Entity.IDMARCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDMARCA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDMOEDA!= null)
						dbCommand.Parameters.AddWithValue("@IDMOEDA", Entity.IDMOEDA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDMOEDA", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORCUSTOINICIAL", Entity.VALORCUSTOINICIAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@FRETEPRODUTO", Entity.FRETEPRODUTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@ENCARGOSPRODUTOS", Entity.ENCARGOSPRODUTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORCUSTOFINAL", Entity.VALORCUSTOFINAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@MARGEMLUCRO", Entity.MARGEMLUCRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORVENDA1", Entity.VALORVENDA1); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORVENDA2", Entity.VALORVENDA2); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORVENDA3", Entity.VALORVENDA3); //Coluna 
					dbCommand.Parameters.AddWithValue("@COMISSAO", Entity.COMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@IPI", Entity.IPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@ICMS", Entity.ICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@QUANTIDADEMINIMA", Entity.QUANTIDADEMINIMA); //Coluna 
					
					if(Entity.IDGRUPOCATEGORIA!= null)
						dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", Entity.IDGRUPOCATEGORIA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCFRETE", Entity.PORCFRETE); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCENCARGOS", Entity.PORCENCARGOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCMARGEMLUCRO", Entity.PORCMARGEMLUCRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCVENDA2", Entity.PORCVENDA2); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCVENDA3", Entity.PORCVENDA3); //Coluna 
					dbCommand.Parameters.AddWithValue("@PESO", Entity.PESO); //Coluna 
					
					if(Entity.IDCLASSIFICACAO!= null)
						dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", Entity.IDCLASSIFICACAO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCST!= null)
						dbCommand.Parameters.AddWithValue("@IDCST", Entity.IDCST); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCST", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@NCMSH", Entity.NCMSH); //Coluna 
					dbCommand.Parameters.AddWithValue("@EXTIPI", Entity.EXTIPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQPIS", Entity.ALIQPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQCOFINS", Entity.ALIQCOFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@CSTPISCONFIS", Entity.CSTPISCONFIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGDECIMALREND", Entity.FLAGDECIMALREND); //Coluna 
					dbCommand.Parameters.AddWithValue("@MULTAREND", Entity.MULTAREND); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTMT", Entity.FLAGBAIXAESTMT); //Coluna 
					
					if(Entity.IDLOTE!= null)
						dbCommand.Parameters.AddWithValue("@IDLOTE", Entity.IDLOTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@ESTOQUEMANUAL", Entity.ESTOQUEMANUAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@SITUACAOTRIBUTARIA", Entity.SITUACAOTRIBUTARIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@CSTPIS", Entity.CSTPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@CSTIPI", Entity.CSTIPI); //Coluna 
					
					if(Entity.IDCSTECF!= null)
						dbCommand.Parameters.AddWithValue("@IDCSTECF", Entity.IDCSTECF); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCSTECF", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@TIPOITEM", Entity.TIPOITEM); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCPERDAPROD", Entity.PORCPERDAPROD); //Coluna 
					dbCommand.Parameters.AddWithValue("@DADOSADICIONAIS", Entity.DADOSADICIONAIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGICMSST", Entity.FLAGICMSST); //Coluna 
                    dbCommand.Parameters.AddWithValue("@ALTURACHAPA", Entity.ALTURACHAPA); //Coluna 
                    dbCommand.Parameters.AddWithValue("@LARGURACHAPA", Entity.LARGURACHAPA); //Coluna 
                    dbCommand.Parameters.AddWithValue("@FLAGCONTROLAESTOQUE", Entity.FLAGCONTROLAESTOQUE); //Coluna 
                    dbCommand.Parameters.AddWithValue("@ENQUADRALEGALIPI", Entity.ENQUADRALEGALIPI); //Coluna 
                    dbCommand.Parameters.AddWithValue("@CEST", Entity.CEST); //Coluna 
                    dbCommand.Parameters.AddWithValue("@FLAGINATIVO", Entity.FLAGINATIVO); //Coluna 
                    dbCommand.Parameters.AddWithValue("@FLAGNAOSINTEGRASPED", Entity.FLAGNAOSINTEGRASPED); //Coluna 				
                    dbCommand.Parameters.AddWithValue("@CFOP", Entity.CFOP); //Coluna 	
                    dbCommand.Parameters.AddWithValue("@REDICMS", Entity.REDICMS); //Coluna 

                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPRODUTO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDPRODUTO"].Value.ToString());
				
	
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


        public int Save(int? IDPRODUTO, string NOMEPRODUTO, string CODPRODUTOFORNECEDOR, string CODBARRA, string LOCALIZACAO, DateTime DATACADASTRO, 
                        int IDUNIDADE, int IDMARCA, int IDMOEDA, decimal VALORCUSTOINICIAL, decimal FRETEPRODUTO, decimal ENCARGOSPRODUTOS,
                        decimal VALORCUSTOFINAL, decimal MARGEMLUCRO, decimal VALORVENDA1, decimal VALORVENDA2, decimal VALORVENDA3, decimal COMISSAO, 
                        decimal IPI, decimal ICMS, decimal QUANTIDADEMINIMA, int IDGRUPOCATEGORIA, int IDSTATUS, string OBSERVACAO, decimal PORCFRETE, 
                        decimal PORCENCARGOS, decimal PORCMARGEMLUCRO, decimal PORCVENDA2, decimal PORCVENDA3, decimal PESO, int IDCLASSIFICACAO, 
                        int IDCST, string NCMSH, string EXTIPI, decimal ALIQPIS, decimal ALIQCOFINS, string CSTPISCONFIS, string FLAGDECIMALREND, 
                        int MULTAREND, string FLAGBAIXAESTMT, int IDLOTE, decimal ESTOQUEMANUAL, string SITUACAOTRIBUTARIA, string CSTPIS, string CSTIPI, 
                        int IDCSTECF, string TIPOITEM, decimal PORCPERDAPROD, string DADOSADICIONAIS, string FLAGICMSST, decimal? ALTURACHAPA, 
                        decimal? LARGURACHAPA, string FLAGCONTROLAESTOQUE, int? ENQUADRALEGALIPI, string CEST, string FLAGINATIVO, string FLAGNAOSINTEGRASPED,
                        string CFOP, decimal? REDICMS)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDPRODUTO == -1)
										dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDPRODUTO", IDPRODUTO);
										
										dbCommand.Parameters.AddWithValue("@NOMEPRODUTO", NOMEPRODUTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODPRODUTOFORNECEDOR", CODPRODUTOFORNECEDOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODBARRA", CODBARRA); //Coluna 
										dbCommand.Parameters.AddWithValue("@LOCALIZACAO", LOCALIZACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATACADASTRO", DATACADASTRO); //Coluna 
										
										if(IDUNIDADE!= null)
											dbCommand.Parameters.AddWithValue("@IDUNIDADE", IDUNIDADE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDUNIDADE", DBNull.Value); //ForeignKey 5
										
										
										if(IDMARCA!= null)
											dbCommand.Parameters.AddWithValue("@IDMARCA", IDMARCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDMARCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDMOEDA!= null)
											dbCommand.Parameters.AddWithValue("@IDMOEDA", IDMOEDA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDMOEDA", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORCUSTOINICIAL", VALORCUSTOINICIAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@FRETEPRODUTO", FRETEPRODUTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@ENCARGOSPRODUTOS", ENCARGOSPRODUTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORCUSTOFINAL", VALORCUSTOFINAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@MARGEMLUCRO", MARGEMLUCRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORVENDA1", VALORVENDA1); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORVENDA2", VALORVENDA2); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORVENDA3", VALORVENDA3); //Coluna 
										dbCommand.Parameters.AddWithValue("@COMISSAO", COMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@IPI", IPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@ICMS", ICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@QUANTIDADEMINIMA", QUANTIDADEMINIMA); //Coluna 
										
										if(IDGRUPOCATEGORIA!= null)
											dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", IDGRUPOCATEGORIA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", DBNull.Value); //ForeignKey 5
										
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCFRETE", PORCFRETE); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCENCARGOS", PORCENCARGOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCMARGEMLUCRO", PORCMARGEMLUCRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCVENDA2", PORCVENDA2); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCVENDA3", PORCVENDA3); //Coluna 
										dbCommand.Parameters.AddWithValue("@PESO", PESO); //Coluna 
										
										if(IDCLASSIFICACAO!= null)
											dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", IDCLASSIFICACAO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", DBNull.Value); //ForeignKey 5
										
										
										if(IDCST!= null)
											dbCommand.Parameters.AddWithValue("@IDCST", IDCST); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCST", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@NCMSH", NCMSH); //Coluna 
										dbCommand.Parameters.AddWithValue("@EXTIPI", EXTIPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQPIS", ALIQPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQCOFINS", ALIQCOFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@CSTPISCONFIS", CSTPISCONFIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGDECIMALREND", FLAGDECIMALREND); //Coluna 
										dbCommand.Parameters.AddWithValue("@MULTAREND", MULTAREND); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGBAIXAESTMT", FLAGBAIXAESTMT); //Coluna 
										
										if(IDLOTE!= null)
											dbCommand.Parameters.AddWithValue("@IDLOTE", IDLOTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@ESTOQUEMANUAL", ESTOQUEMANUAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@SITUACAOTRIBUTARIA", SITUACAOTRIBUTARIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@CSTPIS", CSTPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@CSTIPI", CSTIPI); //Coluna 
										
										if(IDCSTECF!= null)
											dbCommand.Parameters.AddWithValue("@IDCSTECF", IDCSTECF); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCSTECF", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@TIPOITEM", TIPOITEM); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCPERDAPROD", PORCPERDAPROD); //Coluna 
										dbCommand.Parameters.AddWithValue("@DADOSADICIONAIS", DADOSADICIONAIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGICMSST", FLAGICMSST); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@ALTURACHAPA", ALTURACHAPA); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@LARGURACHAPA", LARGURACHAPA); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@FLAGCONTROLAESTOQUE", FLAGCONTROLAESTOQUE); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@ENQUADRALEGALIPI", ENQUADRALEGALIPI); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@CEST", CEST); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@FLAGINATIVO", FLAGINATIVO); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@FLAGNAOSINTEGRASPED", FLAGNAOSINTEGRASPED); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@CFOP", CFOP); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@REDICMS", REDICMS); //Coluna 
                








                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPRODUTO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDPRODUTO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDPRODUTO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTOS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPRODUTO",IDPRODUTO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDPRODUTO;

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

		public  PRODUTOSEntity Read(int IDPRODUTO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTOS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPRODUTO",IDPRODUTO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PRODUTOSEntity entity = null;
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

		
		public  PRODUTOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PRODUTOSCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTOS WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTOS  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTOS  ";
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
		
		public  PRODUTOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PRODUTOSCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTOS WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTOS  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTOS  order by " + FieldOrder;
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

		private static PRODUTOSCollection ExecuteReader(ref PRODUTOSCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PRODUTOSCollection();

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

		private static PRODUTOSEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PRODUTOSEntity entity = new PRODUTOSEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDPRODUTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPRODUTO"));
			entity.NOMEPRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEPRODUTO"));
			entity.CODPRODUTOFORNECEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODPRODUTOFORNECEDOR"));
			entity.CODBARRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODBARRA"));
			entity.LOCALIZACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOCALIZACAO"));
			entity.DATACADASTRO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATACADASTRO"));
			entity.IDUNIDADE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDUNIDADE"));
			entity.IDMARCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDMARCA"));
			entity.IDMOEDA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDMOEDA"));
			entity.VALORCUSTOINICIAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCUSTOINICIAL"));
			entity.FRETEPRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("FRETEPRODUTO"));
			entity.ENCARGOSPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ENCARGOSPRODUTOS"));
			entity.VALORCUSTOFINAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCUSTOFINAL"));
			entity.MARGEMLUCRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("MARGEMLUCRO"));
			entity.VALORVENDA1 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA1"));
			entity.VALORVENDA2 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA2"));
			entity.VALORVENDA3 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA3"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));
			entity.IPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("IPI"));
			entity.ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ICMS"));
			entity.QUANTIDADEMINIMA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANTIDADEMINIMA"));
			entity.IDGRUPOCATEGORIA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDGRUPOCATEGORIA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.PORCFRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCFRETE"));
			entity.PORCENCARGOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCENCARGOS"));
			entity.PORCMARGEMLUCRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCMARGEMLUCRO"));
			entity.PORCVENDA2 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCVENDA2"));
			entity.PORCVENDA3 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCVENDA3"));
			entity.PESO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESO"));
			entity.IDCLASSIFICACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLASSIFICACAO"));
			entity.IDCST = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCST"));
			entity.NCMSH = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NCMSH"));
			entity.EXTIPI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EXTIPI"));
			entity.ALIQPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQPIS"));
			entity.ALIQCOFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQCOFINS"));
			entity.CSTPISCONFIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTPISCONFIS"));
			entity.FLAGDECIMALREND = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGDECIMALREND"));
			entity.MULTAREND = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("MULTAREND"));
			entity.FLAGBAIXAESTMT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGBAIXAESTMT"));
			entity.IDLOTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOTE"));
			entity.ESTOQUEMANUAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ESTOQUEMANUAL"));
			entity.SITUACAOTRIBUTARIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAOTRIBUTARIA"));
			entity.CSTPIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTPIS"));
			entity.CSTIPI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTIPI"));
			entity.IDCSTECF = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCSTECF"));
			entity.TIPOITEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPOITEM"));
			entity.PORCPERDAPROD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCPERDAPROD"));
			entity.DADOSADICIONAIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DADOSADICIONAIS"));
			entity.FLAGICMSST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGICMSST"));
            entity.ALTURACHAPA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALTURACHAPA"));
            entity.LARGURACHAPA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("LARGURACHAPA"));
            entity.FLAGCONTROLAESTOQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCONTROLAESTOQUE"));
            entity.ENQUADRALEGALIPI = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("ENQUADRALEGALIPI"));
            entity.CEST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEST"));
            entity.FLAGINATIVO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGINATIVO"));
            entity.FLAGNAOSINTEGRASPED = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGNAOSINTEGRASPED"));
            entity.CFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CFOP"));
            entity.REDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("REDICMS"));

            return entity;
		}
	}
}
