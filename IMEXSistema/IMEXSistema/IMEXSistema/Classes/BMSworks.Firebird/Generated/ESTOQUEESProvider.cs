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
	public partial class ESTOQUEESProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ESTOQUEESProvider()
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
		
		
		public  int Save(ESTOQUEESEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUEES", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUEES", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDESTOQUEES == -1)
					dbCommand.Parameters.AddWithValue("@IDESTOQUEES", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDESTOQUEES", Entity.IDESTOQUEES);
					
					
					if(Entity.IDTIPOMOVIMENTACAO!= null)
						dbCommand.Parameters.AddWithValue("@IDTIPOMOVIMENTACAO", Entity.IDTIPOMOVIMENTACAO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTIPOMOVIMENTACAO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DATAMOVIM", Entity.DATAMOVIM); //Coluna 
					
					if(Entity.IDCODMOVIMENTACAO!= null)
						dbCommand.Parameters.AddWithValue("@IDCODMOVIMENTACAO", Entity.IDCODMOVIMENTACAO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCODMOVIMENTACAO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@NDOCUMENTO", Entity.NDOCUMENTO); //Coluna 
					
					if(Entity.IDFORNECEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", Entity.IDFORNECEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@TOTALMOVIMENTACAO", Entity.TOTALMOVIMENTACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORIPI", Entity.VALORIPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORICMS", Entity.VALORICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORFRETE", Entity.VALORFRETE); //Coluna 
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCFOP!= null)
						dbCommand.Parameters.AddWithValue("@IDCFOP", Entity.IDCFOP); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@MODELONF", Entity.MODELONF); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIENF", Entity.SERIENF); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORBASEICMS", Entity.VALORBASEICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGSINTEGRA", Entity.FLAGSINTEGRA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGENERGIATELECOM", Entity.FLAGENERGIATELECOM); //Coluna 
					dbCommand.Parameters.AddWithValue("@CHAVEACESSO", Entity.CHAVEACESSO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CNPJEMISSOR", Entity.CNPJEMISSOR); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDESTOQUEES"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDESTOQUEES"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDESTOQUEES, int IDTIPOMOVIMENTACAO, DateTime DATAMOVIM, int IDCODMOVIMENTACAO, string NDOCUMENTO, int IDFORNECEDOR, decimal TOTALMOVIMENTACAO, decimal VALORIPI, decimal VALORICMS, decimal VALORFRETE, int IDCLIENTE, int IDCFOP, string MODELONF, string SERIENF, decimal VALORBASEICMS, string FLAGSINTEGRA, string FLAGENERGIATELECOM, string CHAVEACESSO, string CNPJEMISSOR)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUEES", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUEES", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDESTOQUEES == -1)
										dbCommand.Parameters.AddWithValue("@IDESTOQUEES", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDESTOQUEES", IDESTOQUEES);
										
										
										if(IDTIPOMOVIMENTACAO!= null)
											dbCommand.Parameters.AddWithValue("@IDTIPOMOVIMENTACAO", IDTIPOMOVIMENTACAO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTIPOMOVIMENTACAO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DATAMOVIM", DATAMOVIM); //Coluna 
										
										if(IDCODMOVIMENTACAO!= null)
											dbCommand.Parameters.AddWithValue("@IDCODMOVIMENTACAO", IDCODMOVIMENTACAO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCODMOVIMENTACAO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@NDOCUMENTO", NDOCUMENTO); //Coluna 
										
										if(IDFORNECEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", IDFORNECEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@TOTALMOVIMENTACAO", TOTALMOVIMENTACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORIPI", VALORIPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORICMS", VALORICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORFRETE", VALORFRETE); //Coluna 
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										
										if(IDCFOP!= null)
											dbCommand.Parameters.AddWithValue("@IDCFOP", IDCFOP); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@MODELONF", MODELONF); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIENF", SERIENF); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORBASEICMS", VALORBASEICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGSINTEGRA", FLAGSINTEGRA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGENERGIATELECOM", FLAGENERGIATELECOM); //Coluna 
										dbCommand.Parameters.AddWithValue("@CHAVEACESSO", CHAVEACESSO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CNPJEMISSOR", CNPJEMISSOR); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDESTOQUEES"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDESTOQUEES"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDESTOQUEES)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ESTOQUEES", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ESTOQUEES", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDESTOQUEES",IDESTOQUEES); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDESTOQUEES;

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

		public  ESTOQUEESEntity Read(int IDESTOQUEES)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ESTOQUEES", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ESTOQUEES", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDESTOQUEES",IDESTOQUEES); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				ESTOQUEESEntity entity = null;
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

		
		public  ESTOQUEESCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ESTOQUEESCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ESTOQUEES WHERE (";

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
						strSqlCommand = "SELECT * FROM ESTOQUEES  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ESTOQUEES  ";
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
		
		public  ESTOQUEESCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ESTOQUEESCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ESTOQUEES WHERE (";

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
						strSqlCommand = "SELECT * FROM ESTOQUEES  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ESTOQUEES  order by " + FieldOrder;
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

		private static ESTOQUEESCollection ExecuteReader(ref ESTOQUEESCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ESTOQUEESCollection();

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

		private static ESTOQUEESEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ESTOQUEESEntity entity = new ESTOQUEESEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDESTOQUEES = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDESTOQUEES"));
			entity.IDTIPOMOVIMENTACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOMOVIMENTACAO"));
			entity.DATAMOVIM = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAMOVIM"));
			entity.IDCODMOVIMENTACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCODMOVIMENTACAO"));
			entity.NDOCUMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NDOCUMENTO"));
			entity.IDFORNECEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORNECEDOR"));
			entity.TOTALMOVIMENTACAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALMOVIMENTACAO"));
			entity.VALORIPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORIPI"));
			entity.VALORICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMS"));
			entity.VALORFRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORFRETE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.IDCFOP = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.MODELONF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MODELONF"));
			entity.SERIENF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIENF"));
			entity.VALORBASEICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORBASEICMS"));
			entity.FLAGSINTEGRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSINTEGRA"));
			entity.FLAGENERGIATELECOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGENERGIATELECOM"));
			entity.CHAVEACESSO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEACESSO"));
			entity.CNPJEMISSOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJEMISSOR"));


			return entity;
		}
	}
}
