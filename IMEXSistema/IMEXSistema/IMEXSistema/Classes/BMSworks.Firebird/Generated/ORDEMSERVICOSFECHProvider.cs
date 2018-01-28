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
	public partial class ORDEMSERVICOSFECHProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ORDEMSERVICOSFECHProvider()
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
		
		
		public  int Save(ORDEMSERVICOSFECHEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDORDEMSERVICO == -1)
					dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO", Entity.IDORDEMSERVICO);
					
					dbCommand.Parameters.AddWithValue("@DATAEMISSAO", Entity.DATAEMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORORCAMENTO", Entity.VALORORCAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", Entity.PRAZOENTREGA); //Coluna 
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFUNCIONARIO!= null)
						dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", Entity.IDFUNCIONARIO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALITEMSERVICO", Entity.TOTALITEMSERVICO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALITEMPECA", Entity.TOTALITEMPECA); //Coluna 
					dbCommand.Parameters.AddWithValue("@MAOOBRA", Entity.MAOOBRA); //Coluna 
					dbCommand.Parameters.AddWithValue("@OUTROVALOR", Entity.OUTROVALOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALFECHOS", Entity.TOTALFECHOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@GARANTIAVECTO", Entity.GARANTIAVECTO); //Coluna 
					
					if(Entity.IDFORMAPAGAMENTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", Entity.IDFORMAPAGAMENTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@CONTATO", Entity.CONTATO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", Entity.FLAGORCAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCCOMISSAO", Entity.PORCCOMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLCOMISSAO", Entity.VLCOMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGTELABLOQUEADA", Entity.FLAGTELABLOQUEADA); //Coluna 
					dbCommand.Parameters.AddWithValue("@PLACA", Entity.PLACA); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PROBLEMAINFORMADO", Entity.PROBLEMAINFORMADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PROBLEMACONSTATADO", Entity.PROBLEMACONSTATADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERVICOEXECUTADO", Entity.SERVICOEXECUTADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@EQUIPAMENTO", Entity.EQUIPAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@MODELO", Entity.MODELO); //Coluna 
					dbCommand.Parameters.AddWithValue("@MARCA", Entity.MARCA); //Coluna 
					dbCommand.Parameters.AddWithValue("@ACESSORIOS", Entity.ACESSORIOS); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDORDEMSERVICO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDORDEMSERVICO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDORDEMSERVICO, DateTime DATAEMISSAO, decimal VALORORCAMENTO, int PRAZOENTREGA, int IDSTATUS, int IDFUNCIONARIO, string OBSERVACAO, decimal TOTALITEMSERVICO, decimal TOTALITEMPECA, decimal MAOOBRA, decimal OUTROVALOR, decimal TOTALFECHOS, DateTime GARANTIAVECTO, int IDFORMAPAGAMENTO, int IDCLIENTE, string CONTATO, decimal VALORPAGO, decimal VALORDEVEDOR, string FLAGORCAMENTO, decimal PORCCOMISSAO, decimal VLCOMISSAO, string FLAGTELABLOQUEADA, string PLACA, decimal PORCDESCONTO, decimal VALORDESCONTO, string PROBLEMAINFORMADO, string PROBLEMACONSTATADO, string SERVICOEXECUTADO, string EQUIPAMENTO, string MODELO, string MARCA, string ACESSORIOS)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDORDEMSERVICO == -1)
										dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO", IDORDEMSERVICO);
										
										dbCommand.Parameters.AddWithValue("@DATAEMISSAO", DATAEMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORORCAMENTO", VALORORCAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", PRAZOENTREGA); //Coluna 
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										
										if(IDFUNCIONARIO!= null)
											dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", IDFUNCIONARIO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALITEMSERVICO", TOTALITEMSERVICO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALITEMPECA", TOTALITEMPECA); //Coluna 
										dbCommand.Parameters.AddWithValue("@MAOOBRA", MAOOBRA); //Coluna 
										dbCommand.Parameters.AddWithValue("@OUTROVALOR", OUTROVALOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALFECHOS", TOTALFECHOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@GARANTIAVECTO", GARANTIAVECTO); //Coluna 
										
										if(IDFORMAPAGAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", IDFORMAPAGAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@CONTATO", CONTATO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", FLAGORCAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCCOMISSAO", PORCCOMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLCOMISSAO", VLCOMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGTELABLOQUEADA", FLAGTELABLOQUEADA); //Coluna 
										dbCommand.Parameters.AddWithValue("@PLACA", PLACA); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PROBLEMAINFORMADO", PROBLEMAINFORMADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PROBLEMACONSTATADO", PROBLEMACONSTATADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERVICOEXECUTADO", SERVICOEXECUTADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@EQUIPAMENTO", EQUIPAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@MODELO", MODELO); //Coluna 
										dbCommand.Parameters.AddWithValue("@MARCA", MARCA); //Coluna 
										dbCommand.Parameters.AddWithValue("@ACESSORIOS", ACESSORIOS); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDORDEMSERVICO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDORDEMSERVICO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDORDEMSERVICO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO",IDORDEMSERVICO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDORDEMSERVICO;

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

		public  ORDEMSERVICOSFECHEntity Read(int IDORDEMSERVICO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ORDEMSERVICOSFECH", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDORDEMSERVICO",IDORDEMSERVICO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				ORDEMSERVICOSFECHEntity entity = null;
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

		
		public  ORDEMSERVICOSFECHCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ORDEMSERVICOSFECHCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH WHERE (";

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
						strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH  ";
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
		
		public  ORDEMSERVICOSFECHCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ORDEMSERVICOSFECHCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH WHERE (";

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
						strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ORDEMSERVICOSFECH  order by " + FieldOrder;
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

		private static ORDEMSERVICOSFECHCollection ExecuteReader(ref ORDEMSERVICOSFECHCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ORDEMSERVICOSFECHCollection();

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

		private static ORDEMSERVICOSFECHEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ORDEMSERVICOSFECHEntity entity = new ORDEMSERVICOSFECHEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDORDEMSERVICO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDORDEMSERVICO"));
			entity.DATAEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAEMISSAO"));
			entity.VALORORCAMENTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORORCAMENTO"));
			entity.PRAZOENTREGA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("PRAZOENTREGA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.IDFUNCIONARIO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFUNCIONARIO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.TOTALITEMSERVICO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALITEMSERVICO"));
			entity.TOTALITEMPECA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALITEMPECA"));
			entity.MAOOBRA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("MAOOBRA"));
			entity.OUTROVALOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTROVALOR"));
			entity.TOTALFECHOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALFECHOS"));
			entity.GARANTIAVECTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("GARANTIAVECTO"));
			entity.IDFORMAPAGAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGAMENTO"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.CONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTATO"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.FLAGORCAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGORCAMENTO"));
			entity.PORCCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCCOMISSAO"));
			entity.VLCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLCOMISSAO"));
			entity.FLAGTELABLOQUEADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTELABLOQUEADA"));
			entity.PLACA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PLACA"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PROBLEMAINFORMADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROBLEMAINFORMADO"));
			entity.PROBLEMACONSTATADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROBLEMACONSTATADO"));
			entity.SERVICOEXECUTADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERVICOEXECUTADO"));
			entity.EQUIPAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EQUIPAMENTO"));
			entity.MODELO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MODELO"));
			entity.MARCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCA"));
			entity.ACESSORIOS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ACESSORIOS"));


			return entity;
		}
	}
}
