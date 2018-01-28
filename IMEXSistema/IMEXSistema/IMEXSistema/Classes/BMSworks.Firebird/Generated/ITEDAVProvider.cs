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
	public partial class ITEDAVProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ITEDAVProvider()
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
		
		
		public  string Save(ITEDAVEntity Entity )
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEDAV", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEDAV", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@NUMERO",Entity.NUMERO); //PrimaryKey 
    dbCommand.Parameters.AddWithValue("@DATA_DAV", Entity.DATA_DAV); //Coluna 
	dbCommand.Parameters.AddWithValue("@CLIENTE",Entity.CLIENTE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@ITEM",Entity.ITEM); //PrimaryKey
	
dbCommand.Parameters.AddWithValue("@CODIGO", Entity.CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@BARRAS", Entity.BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", Entity.DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", Entity.UND); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", Entity.QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", Entity.GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS", Entity.GRADE_DIS); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADO", Entity.CANCELADO); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALIZADOR", Entity.TOTALIZADOR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_UNITA", Entity.VALOR_UNITA); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCONTO", Entity.DESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ACRESCIMO", Entity.ACRESCIMO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", Entity.VALOR_TOTAL); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", Entity.ST); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQUOTA", Entity.ALIQUOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CHAVE", Entity.CHAVE); //Coluna 
dbCommand.Parameters.AddWithValue("@TITULO", Entity.TITULO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@NUMERO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();

                result = dbCommand.Parameters["@NUMERO"].Value.ToString();
				
	
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


        public string Save(string NUMERO, DateTime? DATA_DAV, string CLIENTE, int ITEM, string CODIGO, string BARRAS, string DESCRICAO, string UND, decimal QTD, string GRADE_QUA, string GRADE_DIS, short CANCELADO, string TOTALIZADOR, decimal VALOR_UNITA, decimal DESCONTO, decimal ACRESCIMO, decimal VALOR_TOTAL, string ST, string ALIQUOTA, string CHAVE, string TITULO)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEDAV", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEDAV", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@NUMERO", NUMERO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@CLIENTE", CLIENTE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@ITEM", ITEM); //PrimaryKey 

	dbCommand.Parameters.AddWithValue("@DATA_DAV", DATA_DAV); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO", CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@BARRAS", BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", UND); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS", GRADE_DIS); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADO", CANCELADO); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALIZADOR", TOTALIZADOR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_UNITA", VALOR_UNITA); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCONTO", DESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ACRESCIMO", ACRESCIMO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", VALOR_TOTAL); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", ST); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQUOTA", ALIQUOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CHAVE", CHAVE); //Coluna 
dbCommand.Parameters.AddWithValue("@TITULO", TITULO); //Coluna 

	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@NUMERO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = dbCommand.Parameters["@NUMERO"].Value.ToString();
				
				

	
				
	
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
		
		
		public  string Delete(string NUMERO, string CLIENTE, int ITEM)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ITEDAV", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ITEDAV", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@NUMERO",NUMERO); //PrimaryKey
                dbCommand.Parameters.AddWithValue("@CLIENTE",CLIENTE); //PrimaryKey
                dbCommand.Parameters.AddWithValue("@ITEM",ITEM); //PrimaryKey

		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = NUMERO;

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

		public  ITEDAVEntity Read(string NUMERO, string CLIENTE, int ITEM)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEDAV", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEDAV", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@NUMERO",NUMERO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CLIENTE",CLIENTE); //PrimaryKey
dbCommand.Parameters.AddWithValue("@ITEM",ITEM); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				ITEDAVEntity entity = null;
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

		
		public  ITEDAVCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ITEDAVCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEDAV WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEDAV  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEDAV  ";
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
		
		public  ITEDAVCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ITEDAVCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEDAV WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEDAV  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEDAV  order by " + FieldOrder;
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

		private static ITEDAVCollection ExecuteReader(ref ITEDAVCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ITEDAVCollection();

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

		private static ITEDAVEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ITEDAVEntity entity = new ITEDAVEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMERO"));
			entity.DATA_DAV = getData.ConvertDBValueToDateTime(DataReader, DataReader.GetOrdinal("DATA_DAV"));
            entity.CLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CLIENTE"));
			entity.ITEM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("ITEM"));
			entity.CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODIGO"));
			entity.BARRAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BARRAS"));
			entity.DESCRICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO"));
			entity.UND = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UND"));
			entity.QTD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD"));
			entity.GRADE_QUA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QUA"));
			entity.GRADE_DIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_DIS"));
            entity.CANCELADO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CANCELADO"));
			entity.TOTALIZADOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TOTALIZADOR"));
			entity.VALOR_UNITA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_UNITA"));
			entity.DESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("DESCONTO"));
			entity.ACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ACRESCIMO"));
			entity.VALOR_TOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOTAL"));
			entity.ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ST"));
			entity.ALIQUOTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ALIQUOTA"));
			entity.CHAVE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVE"));


			return entity;
		}
	}
}
