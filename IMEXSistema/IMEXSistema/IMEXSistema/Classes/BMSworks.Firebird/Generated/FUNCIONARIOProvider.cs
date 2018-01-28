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
	public partial class FUNCIONARIOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~FUNCIONARIOProvider()
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
		
		
		public  int Save(FUNCIONARIOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDFUNCIONARIO == -1)
					dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", Entity.IDFUNCIONARIO);
					
					dbCommand.Parameters.AddWithValue("@NOME", Entity.NOME); //Coluna 
					dbCommand.Parameters.AddWithValue("@COMISSAO", Entity.COMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAADMISSAO", Entity.DATAADMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@SALARIO", Entity.SALARIO); //Coluna 
					
					if(Entity.CODSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@CODSTATUS", Entity.CODSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@CODSTATUS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@FUNCAO", Entity.FUNCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DEPARTAMENTO", Entity.DEPARTAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@SETOR", Entity.SETOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@CARTEIRATRABALHO", Entity.CARTEIRATRABALHO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CARTEIRAMOTORISTA", Entity.CARTEIRAMOTORISTA); //Coluna 
					dbCommand.Parameters.AddWithValue("@CPF", Entity.CPF); //Coluna 
					dbCommand.Parameters.AddWithValue("@RG", Entity.RG); //Coluna 
					dbCommand.Parameters.AddWithValue("@ENDERECO", Entity.ENDERECO); //Coluna 
					dbCommand.Parameters.AddWithValue("@BAIRRO", Entity.BAIRRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CIDADE", Entity.CIDADE); //Coluna 
					dbCommand.Parameters.AddWithValue("@CEP", Entity.CEP); //Coluna 
					dbCommand.Parameters.AddWithValue("@UF", Entity.UF); //Coluna 
					dbCommand.Parameters.AddWithValue("@TELEFONE1", Entity.TELEFONE1); //Coluna 
					dbCommand.Parameters.AddWithValue("@TELEFONE2", Entity.TELEFONE2); //Coluna 
					dbCommand.Parameters.AddWithValue("@EMAIL", Entity.EMAIL); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DTANIVERSARIO", Entity.DTANIVERSARIO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGEXIBIRAGENDA", Entity.FLAGEXIBIRAGENDA); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDFUNCIONARIO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDFUNCIONARIO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDFUNCIONARIO, string NOME, decimal COMISSAO, DateTime DATAADMISSAO, decimal SALARIO, int CODSTATUS, string FUNCAO, string DEPARTAMENTO, string SETOR, string CARTEIRATRABALHO, string CARTEIRAMOTORISTA, string CPF, string RG, string ENDERECO, string BAIRRO, string CIDADE, string CEP, string UF, string TELEFONE1, string TELEFONE2, string EMAIL, string OBSERVACAO, DateTime DTANIVERSARIO, string FLAGEXIBIRAGENDA)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDFUNCIONARIO == -1)
										dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", IDFUNCIONARIO);
										
										dbCommand.Parameters.AddWithValue("@NOME", NOME); //Coluna 
										dbCommand.Parameters.AddWithValue("@COMISSAO", COMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAADMISSAO", DATAADMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@SALARIO", SALARIO); //Coluna 
										
										if(CODSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@CODSTATUS", CODSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@CODSTATUS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@FUNCAO", FUNCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DEPARTAMENTO", DEPARTAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@SETOR", SETOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@CARTEIRATRABALHO", CARTEIRATRABALHO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CARTEIRAMOTORISTA", CARTEIRAMOTORISTA); //Coluna 
										dbCommand.Parameters.AddWithValue("@CPF", CPF); //Coluna 
										dbCommand.Parameters.AddWithValue("@RG", RG); //Coluna 
										dbCommand.Parameters.AddWithValue("@ENDERECO", ENDERECO); //Coluna 
										dbCommand.Parameters.AddWithValue("@BAIRRO", BAIRRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CIDADE", CIDADE); //Coluna 
										dbCommand.Parameters.AddWithValue("@CEP", CEP); //Coluna 
										dbCommand.Parameters.AddWithValue("@UF", UF); //Coluna 
										dbCommand.Parameters.AddWithValue("@TELEFONE1", TELEFONE1); //Coluna 
										dbCommand.Parameters.AddWithValue("@TELEFONE2", TELEFONE2); //Coluna 
										dbCommand.Parameters.AddWithValue("@EMAIL", EMAIL); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DTANIVERSARIO", DTANIVERSARIO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGEXIBIRAGENDA", FLAGEXIBIRAGENDA); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDFUNCIONARIO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDFUNCIONARIO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDFUNCIONARIO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO",IDFUNCIONARIO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDFUNCIONARIO;

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

		public  FUNCIONARIOEntity Read(int IDFUNCIONARIO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_FUNCIONARIO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO",IDFUNCIONARIO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				FUNCIONARIOEntity entity = null;
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

		
		public  FUNCIONARIOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			FUNCIONARIOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM FUNCIONARIO WHERE (";

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
						strSqlCommand = "SELECT * FROM FUNCIONARIO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM FUNCIONARIO  ";
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
		
		public  FUNCIONARIOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			FUNCIONARIOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM FUNCIONARIO WHERE (";

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
						strSqlCommand = "SELECT * FROM FUNCIONARIO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM FUNCIONARIO  order by " + FieldOrder;
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

		private static FUNCIONARIOCollection ExecuteReader(ref FUNCIONARIOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new FUNCIONARIOCollection();

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

		private static FUNCIONARIOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			FUNCIONARIOEntity entity = new FUNCIONARIOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDFUNCIONARIO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDFUNCIONARIO"));
			entity.NOME = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOME"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));
			entity.DATAADMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAADMISSAO"));
			entity.SALARIO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("SALARIO"));
			entity.CODSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODSTATUS"));
			entity.FUNCAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FUNCAO"));
			entity.DEPARTAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DEPARTAMENTO"));
			entity.SETOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SETOR"));
			entity.CARTEIRATRABALHO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARTEIRATRABALHO"));
			entity.CARTEIRAMOTORISTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARTEIRAMOTORISTA"));
			entity.CPF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPF"));
			entity.RG = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RG"));
			entity.ENDERECO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECO"));
			entity.BAIRRO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BAIRRO"));
			entity.CIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CIDADE"));
			entity.CEP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEP"));
			entity.UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF"));
			entity.TELEFONE1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE1"));
			entity.TELEFONE2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE2"));
			entity.EMAIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMAIL"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.DTANIVERSARIO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTANIVERSARIO"));
			entity.FLAGEXIBIRAGENDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGEXIBIRAGENDA"));


			return entity;
		}
	}
}
