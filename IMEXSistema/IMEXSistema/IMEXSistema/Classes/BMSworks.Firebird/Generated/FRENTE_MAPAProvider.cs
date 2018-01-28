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
	public partial class FRENTE_MAPAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
        private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.PATHRECEPDIGISAT;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~FRENTE_MAPAProvider()
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
		
		
		public  int Save(FRENTE_MAPAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@N_SERIE", Entity.N_SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@N_CAIXA", Entity.N_CAIXA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTADOR_REDUCAO", Entity.CONTADOR_REDUCAO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTADOR_REINICIO", Entity.CONTADOR_REINICIO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA", Entity.DATA); //Coluna 
dbCommand.Parameters.AddWithValue("@PRIMEIRO_CUPOM", Entity.PRIMEIRO_CUPOM); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMO_CUPOM", Entity.ULTIMO_CUPOM); //Coluna 
dbCommand.Parameters.AddWithValue("@VENDA_BRUTA", Entity.VENDA_BRUTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_INICIO_DIA", Entity.VALOR_INICIO_DIA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FINAL_DIA", Entity.VALOR_FINAL_DIA); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBF", Entity.TRIBF); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBI", Entity.TRIBI); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBN", Entity.TRIBN); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBA", Entity.TRIBA); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBD", Entity.TRIBD); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBC", Entity.TRIBC); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBS", Entity.TRIBS); //Coluna 
dbCommand.Parameters.AddWithValue("@T0700", Entity.T0700); //Coluna 
dbCommand.Parameters.AddWithValue("@T1200", Entity.T1200); //Coluna 
dbCommand.Parameters.AddWithValue("@T1800", Entity.T1800); //Coluna 
dbCommand.Parameters.AddWithValue("@T2500", Entity.T2500); //Coluna 
dbCommand.Parameters.AddWithValue("@TRICS", Entity.TRICS); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIAS", Entity.TRIAS); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIDS", Entity.TRIDS); //Coluna 
dbCommand.Parameters.AddWithValue("@SINCRONIZADO", Entity.SINCRONIZADO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters[""].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters[""].Value.ToString());
				
	
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
		
		
		public  int Save(string N_SERIE, string N_CAIXA, string CONTADOR_REDUCAO, string CONTADOR_REINICIO, DateTime DATA, string PRIMEIRO_CUPOM, string ULTIMO_CUPOM, decimal VENDA_BRUTA, decimal VALOR_INICIO_DIA, decimal VALOR_FINAL_DIA, decimal TRIBF, decimal TRIBI, decimal TRIBN, decimal TRIBA, decimal TRIBD, decimal TRIBC, decimal TRIBS, decimal T0700, decimal T1200, decimal T1800, decimal T2500, decimal TRICS, decimal TRIAS, decimal TRIDS, short SINCRONIZADO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@N_SERIE", N_SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@N_CAIXA", N_CAIXA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTADOR_REDUCAO", CONTADOR_REDUCAO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTADOR_REINICIO", CONTADOR_REINICIO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA", DATA); //Coluna 
dbCommand.Parameters.AddWithValue("@PRIMEIRO_CUPOM", PRIMEIRO_CUPOM); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMO_CUPOM", ULTIMO_CUPOM); //Coluna 
dbCommand.Parameters.AddWithValue("@VENDA_BRUTA", VENDA_BRUTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_INICIO_DIA", VALOR_INICIO_DIA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FINAL_DIA", VALOR_FINAL_DIA); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBF", TRIBF); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBI", TRIBI); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBN", TRIBN); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBA", TRIBA); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBD", TRIBD); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBC", TRIBC); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBS", TRIBS); //Coluna 
dbCommand.Parameters.AddWithValue("@T0700", T0700); //Coluna 
dbCommand.Parameters.AddWithValue("@T1200", T1200); //Coluna 
dbCommand.Parameters.AddWithValue("@T1800", T1800); //Coluna 
dbCommand.Parameters.AddWithValue("@T2500", T2500); //Coluna 
dbCommand.Parameters.AddWithValue("@TRICS", TRICS); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIAS", TRIAS); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIDS", TRIDS); //Coluna 
dbCommand.Parameters.AddWithValue("@SINCRONIZADO", SINCRONIZADO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters[""].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters[""].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete()
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = -1;

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

		public  FRENTE_MAPAEntity Read()
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_FRENTE_MAPA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

				reader = dbCommand.ExecuteReader();

				FRENTE_MAPAEntity entity = null;
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

		
		public  FRENTE_MAPACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			FRENTE_MAPACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM FRENTE_MAPA WHERE (";

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
						strSqlCommand = "SELECT * FROM FRENTE_MAPA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM FRENTE_MAPA  ";
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
		
		public  FRENTE_MAPACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			FRENTE_MAPACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM FRENTE_MAPA WHERE (";

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
						strSqlCommand = "SELECT * FROM FRENTE_MAPA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM FRENTE_MAPA  order by " + FieldOrder;
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

		private static FRENTE_MAPACollection ExecuteReader(ref FRENTE_MAPACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new FRENTE_MAPACollection();

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

		private static FRENTE_MAPAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			FRENTE_MAPAEntity entity = new FRENTE_MAPAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.N_SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("N_SERIE"));
			entity.N_CAIXA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("N_CAIXA"));
			entity.CONTADOR_REDUCAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTADOR_REDUCAO"));
			entity.CONTADOR_REINICIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTADOR_REINICIO"));
			entity.DATA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA"));
			entity.PRIMEIRO_CUPOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PRIMEIRO_CUPOM"));
			entity.ULTIMO_CUPOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ULTIMO_CUPOM"));
			entity.VENDA_BRUTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VENDA_BRUTA"));
			entity.VALOR_INICIO_DIA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_INICIO_DIA"));
			entity.VALOR_FINAL_DIA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_FINAL_DIA"));
			entity.TRIBF = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBF"));
			entity.TRIBI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBI"));
			entity.TRIBN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBN"));
			entity.TRIBA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBA"));
			entity.TRIBD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBD"));
			entity.TRIBC = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBC"));
			entity.TRIBS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIBS"));
			entity.T0700 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("T0700"));
			entity.T1200 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("T1200"));
			entity.T1800 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("T1800"));
			entity.T2500 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("T2500"));
			entity.TRICS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRICS"));
			entity.TRIAS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIAS"));
			entity.TRIDS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TRIDS"));
			entity.SINCRONIZADO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("SINCRONIZADO"));


			return entity;
		}
	}
}
