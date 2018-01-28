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
	public partial class RECEBERProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
        private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~RECEBERProvider()
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
		
		
		public  string Save(RECEBEREntity Entity )
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_RECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_RECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@DOCUMENTO",Entity.DOCUMENTO); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@HISTORICO", Entity.HISTORICO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CLIENTE", Entity.COD_CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@NOM_CLIENTE", Entity.NOM_CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@EMISSAO", Entity.EMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@VENCIMENTO", Entity.VENCIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DUP", Entity.VALOR_DUP); //Coluna 
dbCommand.Parameters.AddWithValue("@RECEBIMENTO", Entity.RECEBIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_REC", Entity.VALOR_REC); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_JUR", Entity.VALOR_JUR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DES", Entity.VALOR_DES); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", Entity.ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@NUM_CONTA", Entity.NUM_CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CTO_CUSTO", Entity.CTO_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORTADOR", Entity.PORTADOR); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO_DOC", Entity.TIPO_DOC); //Coluna 
dbCommand.Parameters.AddWithValue("@COMP", Entity.COMP); //Coluna 
dbCommand.Parameters.AddWithValue("@BANCO", Entity.BANCO); //Coluna 
dbCommand.Parameters.AddWithValue("@AGENCIA", Entity.AGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTA", Entity.CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CHEQUE", Entity.CHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", Entity.OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@NOSSO_NUM", Entity.NOSSO_NUM); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@DOCUMENTO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = dbCommand.Parameters["@DOCUMENTO"].Value.ToString();
				
	
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
		
		
		public string Save(string DOCUMENTO, string HISTORICO, string COD_CLIENTE, string NOM_CLIENTE, DateTime EMISSAO, DateTime VENCIMENTO, decimal VALOR_DUP, DateTime RECEBIMENTO, decimal VALOR_REC, decimal VALOR_JUR, decimal VALOR_DES, string ESPECIE, string NUM_CONTA, string CTO_CUSTO, string PORTADOR, string TIPO_DOC, string COMP, string BANCO, string AGENCIA, string CONTA, string CHEQUE, string OBSERVACOES, string NOSSO_NUM)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_RECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_RECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
	dbCommand.Parameters.AddWithValue("@DOCUMENTO", DOCUMENTO); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@HISTORICO", HISTORICO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@NOM_CLIENTE", NOM_CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@EMISSAO", EMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@VENCIMENTO", VENCIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DUP", VALOR_DUP); //Coluna 
dbCommand.Parameters.AddWithValue("@RECEBIMENTO", RECEBIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_REC", VALOR_REC); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_JUR", VALOR_JUR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DES", VALOR_DES); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@NUM_CONTA", NUM_CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CTO_CUSTO", CTO_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORTADOR", PORTADOR); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO_DOC", TIPO_DOC); //Coluna 
dbCommand.Parameters.AddWithValue("@COMP", COMP); //Coluna 
dbCommand.Parameters.AddWithValue("@BANCO", BANCO); //Coluna 
dbCommand.Parameters.AddWithValue("@AGENCIA", AGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTA", CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CHEQUE", CHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@NOSSO_NUM", NOSSO_NUM); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@DOCUMENTO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = dbCommand.Parameters["@DOCUMENTO"].Value.ToString();
				
				

	
				
	
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
		
		
		public string Delete(string DOCUMENTO)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_RECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_RECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@DOCUMENTO",DOCUMENTO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = DOCUMENTO;

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

		public  RECEBEREntity Read(string DOCUMENTO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_RECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_RECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@DOCUMENTO",DOCUMENTO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				RECEBEREntity entity = null;
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

		
		public  RECEBERCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			RECEBERCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM RECEBER WHERE (";

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
						strSqlCommand = "SELECT * FROM RECEBER  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM RECEBER  ";
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
		
		public  RECEBERCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			RECEBERCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM RECEBER WHERE (";

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
						strSqlCommand = "SELECT * FROM RECEBER  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM RECEBER  order by " + FieldOrder;
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

		private static RECEBERCollection ExecuteReader(ref RECEBERCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new RECEBERCollection();

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

		private static RECEBEREntity FillEntityObject(ref FbDataReader DataReader) 
		{
			RECEBEREntity entity = new RECEBEREntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.DOCUMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DOCUMENTO"));
			entity.HISTORICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HISTORICO"));
			entity.COD_CLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COD_CLIENTE"));
			entity.NOM_CLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOM_CLIENTE"));
			entity.EMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("EMISSAO"));
			entity.VENCIMENTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("VENCIMENTO"));
			entity.VALOR_DUP = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_DUP"));
			entity.RECEBIMENTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("RECEBIMENTO"));
			entity.VALOR_REC = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_REC"));
			entity.VALOR_JUR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_JUR"));
			entity.VALOR_DES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_DES"));
			entity.ESPECIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIE"));
			entity.NUM_CONTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUM_CONTA"));
			entity.CTO_CUSTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CTO_CUSTO"));
			entity.PORTADOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PORTADOR"));
			entity.TIPO_DOC = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPO_DOC"));
			entity.COMP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COMP"));
			entity.BANCO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BANCO"));
			entity.AGENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("AGENCIA"));
			entity.CONTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTA"));
			entity.CHEQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHEQUE"));
			entity.OBSERVACOES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACOES"));
			entity.NOSSO_NUM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOSSO_NUM"));


			return entity;
		}
	}
}
