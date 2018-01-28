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
	public partial class EMPRESAEMISSORAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~EMPRESAEMISSORAProvider()
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
		
		
		public  int Save(EMPRESAEMISSORAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDEMPRESAEMISSORA == -1)
					dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA", Entity.IDEMPRESAEMISSORA);
					
					dbCommand.Parameters.AddWithValue("@RAZAOSOCIAL", Entity.RAZAOSOCIAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@NOMEFANTASIA", Entity.NOMEFANTASIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@TELEFONE", Entity.TELEFONE); //Coluna 
					dbCommand.Parameters.AddWithValue("@CNPJ", Entity.CNPJ); //Coluna 
					dbCommand.Parameters.AddWithValue("@IE", Entity.IE); //Coluna 
					dbCommand.Parameters.AddWithValue("@EMAIL", Entity.EMAIL); //Coluna 
					dbCommand.Parameters.AddWithValue("@ENDERECO", Entity.ENDERECO); //Coluna 
					dbCommand.Parameters.AddWithValue("@NUMERO", Entity.NUMERO); //Coluna 
					dbCommand.Parameters.AddWithValue("@COMPLEMENTO", Entity.COMPLEMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@BAIRRO", Entity.BAIRRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CEP", Entity.CEP); //Coluna 
					dbCommand.Parameters.AddWithValue("@IMUNICIPAL", Entity.IMUNICIPAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@CRT", Entity.CRT); //Coluna 
					dbCommand.Parameters.AddWithValue("@IEST", Entity.IEST); //Coluna 
					dbCommand.Parameters.AddWithValue("@CNAE", Entity.CNAE); //Coluna 
					dbCommand.Parameters.AddWithValue("@NOMECERTIFICADO", Entity.NOMECERTIFICADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIACERTIFICADO", Entity.SERIACERTIFICADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALIDADECERTIFICADO", Entity.VALIDADECERTIFICADO); //Coluna 
					
					if(Entity.COD_MUN_IBGE!= null)
						dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", Entity.COD_MUN_IBGE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", DBNull.Value); //ForeignKey 5
					
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDEMPRESAEMISSORA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDEMPRESAEMISSORA"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDEMPRESAEMISSORA, string RAZAOSOCIAL, string NOMEFANTASIA, string TELEFONE, string CNPJ, string IE, string EMAIL, string ENDERECO, string NUMERO, string COMPLEMENTO, string BAIRRO, string CEP, string IMUNICIPAL, string CRT, string IEST, string CNAE, string NOMECERTIFICADO, string SERIACERTIFICADO, string VALIDADECERTIFICADO, int COD_MUN_IBGE)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDEMPRESAEMISSORA == -1)
										dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA", IDEMPRESAEMISSORA);
										
										dbCommand.Parameters.AddWithValue("@RAZAOSOCIAL", RAZAOSOCIAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@NOMEFANTASIA", NOMEFANTASIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@TELEFONE", TELEFONE); //Coluna 
										dbCommand.Parameters.AddWithValue("@CNPJ", CNPJ); //Coluna 
										dbCommand.Parameters.AddWithValue("@IE", IE); //Coluna 
										dbCommand.Parameters.AddWithValue("@EMAIL", EMAIL); //Coluna 
										dbCommand.Parameters.AddWithValue("@ENDERECO", ENDERECO); //Coluna 
										dbCommand.Parameters.AddWithValue("@NUMERO", NUMERO); //Coluna 
										dbCommand.Parameters.AddWithValue("@COMPLEMENTO", COMPLEMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@BAIRRO", BAIRRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CEP", CEP); //Coluna 
										dbCommand.Parameters.AddWithValue("@IMUNICIPAL", IMUNICIPAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@CRT", CRT); //Coluna 
										dbCommand.Parameters.AddWithValue("@IEST", IEST); //Coluna 
										dbCommand.Parameters.AddWithValue("@CNAE", CNAE); //Coluna 
										dbCommand.Parameters.AddWithValue("@NOMECERTIFICADO", NOMECERTIFICADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIACERTIFICADO", SERIACERTIFICADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALIDADECERTIFICADO", VALIDADECERTIFICADO); //Coluna 
										
										if(COD_MUN_IBGE!= null)
											dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", COD_MUN_IBGE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", DBNull.Value); //ForeignKey 5
										
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDEMPRESAEMISSORA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDEMPRESAEMISSORA"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDEMPRESAEMISSORA)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA",IDEMPRESAEMISSORA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDEMPRESAEMISSORA;

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

		public  EMPRESAEMISSORAEntity Read(int IDEMPRESAEMISSORA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_EMPRESAEMISSORA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDEMPRESAEMISSORA",IDEMPRESAEMISSORA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				EMPRESAEMISSORAEntity entity = null;
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

		
		public  EMPRESAEMISSORACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			EMPRESAEMISSORACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM EMPRESAEMISSORA WHERE (";

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
						strSqlCommand = "SELECT * FROM EMPRESAEMISSORA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM EMPRESAEMISSORA  ";
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
		
		public  EMPRESAEMISSORACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			EMPRESAEMISSORACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM EMPRESAEMISSORA WHERE (";

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
						strSqlCommand = "SELECT * FROM EMPRESAEMISSORA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM EMPRESAEMISSORA  order by " + FieldOrder;
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

		private static EMPRESAEMISSORACollection ExecuteReader(ref EMPRESAEMISSORACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new EMPRESAEMISSORACollection();

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

		private static EMPRESAEMISSORAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			EMPRESAEMISSORAEntity entity = new EMPRESAEMISSORAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDEMPRESAEMISSORA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDEMPRESAEMISSORA"));
			entity.RAZAOSOCIAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RAZAOSOCIAL"));
			entity.NOMEFANTASIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEFANTASIA"));
			entity.TELEFONE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE"));
			entity.CNPJ = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJ"));
			entity.IE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IE"));
			entity.EMAIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMAIL"));
			entity.ENDERECO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECO"));
			entity.NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMERO"));
			entity.COMPLEMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COMPLEMENTO"));
			entity.BAIRRO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BAIRRO"));
			entity.CEP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEP"));
			entity.IMUNICIPAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IMUNICIPAL"));
			entity.CRT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CRT"));
			entity.IEST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IEST"));
			entity.CNAE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNAE"));
			entity.NOMECERTIFICADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECERTIFICADO"));
			entity.SERIACERTIFICADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIACERTIFICADO"));
			entity.VALIDADECERTIFICADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("VALIDADECERTIFICADO"));
			entity.COD_MUN_IBGE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("COD_MUN_IBGE"));


			return entity;
		}
	}
}
