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
	public partial class PRODUTOS2NFEProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PRODUTOS2NFEProvider()
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
		
		
		public  int Save(PRODUTOS2NFEEntity Entity )
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

				
if(Entity.IDPRODUTO!= -1)
	dbCommand.Parameters.AddWithValue("@IDPRODUTO",Entity.IDPRODUTO); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOMEPRODUTO", Entity.NOMEPRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CODPRODUTOFORNECEDOR", Entity.CODPRODUTOFORNECEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@CODBARRA", Entity.CODBARRA); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCALIZACAO", Entity.LOCALIZACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATACADASTRO", Entity.DATACADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDUNIDADE", Entity.IDUNIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDMARCA", Entity.IDMARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDMOEDA", Entity.IDMOEDA); //Coluna 
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
dbCommand.Parameters.AddWithValue("@ESTOQUEATUAL", Entity.ESTOQUEATUAL); //Coluna 
dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", Entity.IDGRUPOCATEGORIA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCFRETE", Entity.PORCFRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCENCARGOS", Entity.PORCENCARGOS); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCMARGEMLUCRO", Entity.PORCMARGEMLUCRO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCVENDA2", Entity.PORCVENDA2); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCVENDA3", Entity.PORCVENDA3); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO", Entity.PESO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", Entity.IDCLASSIFICACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCST", Entity.IDCST); //Coluna 
dbCommand.Parameters.AddWithValue("@NCMSH", Entity.NCMSH); //Coluna 
dbCommand.Parameters.AddWithValue("@EXTIPI", Entity.EXTIPI); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQPIS", Entity.ALIQPIS); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQCOFINS", Entity.ALIQCOFINS); //Coluna 
dbCommand.Parameters.AddWithValue("@CSTPISCONFIS", Entity.CSTPISCONFIS); //Coluna 
	
				
								
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
		
		
		public  int Save(int? IDPRODUTO, string NOMEPRODUTO, string CODPRODUTOFORNECEDOR, string CODBARRA, string LOCALIZACAO, DateTime DATACADASTRO, int IDUNIDADE, int IDMARCA, int IDMOEDA, decimal VALORCUSTOINICIAL, decimal FRETEPRODUTO, decimal ENCARGOSPRODUTOS, decimal VALORCUSTOFINAL, decimal MARGEMLUCRO, decimal VALORVENDA1, decimal VALORVENDA2, decimal VALORVENDA3, decimal COMISSAO, decimal IPI, decimal ICMS, decimal QUANTIDADEMINIMA, decimal ESTOQUEATUAL, int IDGRUPOCATEGORIA, int IDSTATUS, string OBSERVACAO, decimal PORCFRETE, decimal PORCENCARGOS, decimal PORCMARGEMLUCRO, decimal PORCVENDA2, decimal PORCVENDA3, decimal PESO, int IDCLASSIFICACAO, int IDCST, string NCMSH, string EXTIPI, decimal ALIQPIS, decimal ALIQCOFINS, string CSTPISCONFIS)
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

				
if(IDPRODUTO!= -1)
	dbCommand.Parameters.AddWithValue("@IDPRODUTO", IDPRODUTO); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOMEPRODUTO", NOMEPRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CODPRODUTOFORNECEDOR", CODPRODUTOFORNECEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@CODBARRA", CODBARRA); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCALIZACAO", LOCALIZACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATACADASTRO", DATACADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDUNIDADE", IDUNIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDMARCA", IDMARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDMOEDA", IDMOEDA); //Coluna 
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
dbCommand.Parameters.AddWithValue("@ESTOQUEATUAL", ESTOQUEATUAL); //Coluna 
dbCommand.Parameters.AddWithValue("@IDGRUPOCATEGORIA", IDGRUPOCATEGORIA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCFRETE", PORCFRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCENCARGOS", PORCENCARGOS); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCMARGEMLUCRO", PORCMARGEMLUCRO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCVENDA2", PORCVENDA2); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCVENDA3", PORCVENDA3); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO", PESO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", IDCLASSIFICACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCST", IDCST); //Coluna 
dbCommand.Parameters.AddWithValue("@NCMSH", NCMSH); //Coluna 
dbCommand.Parameters.AddWithValue("@EXTIPI", EXTIPI); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQPIS", ALIQPIS); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQCOFINS", ALIQCOFINS); //Coluna 
dbCommand.Parameters.AddWithValue("@CSTPISCONFIS", CSTPISCONFIS); //Coluna 
	
				
								
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

        public PRODUTOS2NFEEntity Read(int IDPRODUTO)
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

                PRODUTOS2NFEEntity entity = null;
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

		
		public  PRODUTOS2NFECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
            PRODUTOS2NFECollection collection = null;
			
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
		
		public  PRODUTOS2NFECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
            PRODUTOS2NFECollection collection = null;
			
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

        private static PRODUTOS2NFECollection ExecuteReader(ref PRODUTOS2NFECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
                collection = new PRODUTOS2NFECollection();

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

        private static PRODUTOS2NFEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
            PRODUTOS2NFEEntity entity = new PRODUTOS2NFEEntity();

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
			entity.ESTOQUEATUAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ESTOQUEATUAL"));
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


			return entity;
		}
	}
}
