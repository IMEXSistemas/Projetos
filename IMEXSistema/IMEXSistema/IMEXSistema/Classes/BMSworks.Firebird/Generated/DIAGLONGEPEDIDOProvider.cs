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
	public partial class DIAGLONGEPEDIDOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~DIAGLONGEPEDIDOProvider()
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
		
		
		public  int Save(DIAGLONGEPEDIDOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(Entity.IDDIAGLONGEPEDIDO!= -1)
	dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO",Entity.IDDIAGLONGEPEDIDO); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", Entity.IDPEDIDOOTICA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRESFERICO", Entity.DIRESFERICO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRCILINDRICO", Entity.DIRCILINDRICO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIREIXO", Entity.DIREIXO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRADICAO", Entity.DIRADICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRDNP", Entity.DIRDNP); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRACO", Entity.DIRACO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQESFERICO", Entity.ESQESFERICO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQCILINDRICO", Entity.ESQCILINDRICO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQEIXO", Entity.ESQEIXO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQADICAO", Entity.ESQADICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQDNP", Entity.ESQDNP); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQACO", Entity.ESQACO); //Coluna 
dbCommand.Parameters.AddWithValue("@LENTES", Entity.LENTES); //Coluna 
dbCommand.Parameters.AddWithValue("@ARMACAO", Entity.ARMACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DISTANCIAPUPILAR", Entity.DISTANCIAPUPILAR); //Coluna 
dbCommand.Parameters.AddWithValue("@DIREITO", Entity.DIREITO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQUERDO", Entity.ESQUERDO); //Coluna 
dbCommand.Parameters.AddWithValue("@DPA", Entity.DPA); //Coluna 
dbCommand.Parameters.AddWithValue("@MD", Entity.MD); //Coluna 
dbCommand.Parameters.AddWithValue("@MV", Entity.MV); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDDIAGLONGEPEDIDO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDDIAGLONGEPEDIDO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDDIAGLONGEPEDIDO, int IDPEDIDOOTICA, string DIRESFERICO, string DIRCILINDRICO, string DIREIXO, string DIRADICAO, string DIRDNP, string DIRACO, string ESQESFERICO, string ESQCILINDRICO, string ESQEIXO, string ESQADICAO, string ESQDNP, string ESQACO, string LENTES, string ARMACAO, string DISTANCIAPUPILAR, string DIREITO, string ESQUERDO, string DPA, string MD, string MV)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(IDDIAGLONGEPEDIDO!= -1)
	dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO", IDDIAGLONGEPEDIDO); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", IDPEDIDOOTICA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRESFERICO", DIRESFERICO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRCILINDRICO", DIRCILINDRICO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIREIXO", DIREIXO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRADICAO", DIRADICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRDNP", DIRDNP); //Coluna 
dbCommand.Parameters.AddWithValue("@DIRACO", DIRACO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQESFERICO", ESQESFERICO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQCILINDRICO", ESQCILINDRICO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQEIXO", ESQEIXO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQADICAO", ESQADICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQDNP", ESQDNP); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQACO", ESQACO); //Coluna 
dbCommand.Parameters.AddWithValue("@LENTES", LENTES); //Coluna 
dbCommand.Parameters.AddWithValue("@ARMACAO", ARMACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DISTANCIAPUPILAR", DISTANCIAPUPILAR); //Coluna 
dbCommand.Parameters.AddWithValue("@DIREITO", DIREITO); //Coluna 
dbCommand.Parameters.AddWithValue("@ESQUERDO", ESQUERDO); //Coluna 
dbCommand.Parameters.AddWithValue("@DPA", DPA); //Coluna 
dbCommand.Parameters.AddWithValue("@MD", MD); //Coluna 
dbCommand.Parameters.AddWithValue("@MV", MV); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDDIAGLONGEPEDIDO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDDIAGLONGEPEDIDO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDDIAGLONGEPEDIDO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO",IDDIAGLONGEPEDIDO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDDIAGLONGEPEDIDO;

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

		public  DIAGLONGEPEDIDOEntity Read(int IDDIAGLONGEPEDIDO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_DIAGLONGEPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDDIAGLONGEPEDIDO",IDDIAGLONGEPEDIDO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				DIAGLONGEPEDIDOEntity entity = null;
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

		
		public  DIAGLONGEPEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			DIAGLONGEPEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO  ";
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
		
		public  DIAGLONGEPEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			DIAGLONGEPEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM DIAGLONGEPEDIDO  order by " + FieldOrder;
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

		private static DIAGLONGEPEDIDOCollection ExecuteReader(ref DIAGLONGEPEDIDOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new DIAGLONGEPEDIDOCollection();

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

		private static DIAGLONGEPEDIDOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			DIAGLONGEPEDIDOEntity entity = new DIAGLONGEPEDIDOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDDIAGLONGEPEDIDO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDDIAGLONGEPEDIDO"));
			entity.IDPEDIDOOTICA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPEDIDOOTICA"));
			entity.DIRESFERICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIRESFERICO"));
			entity.DIRCILINDRICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIRCILINDRICO"));
			entity.DIREIXO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIREIXO"));
			entity.DIRADICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIRADICAO"));
			entity.DIRDNP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIRDNP"));
			entity.DIRACO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIRACO"));
			entity.ESQESFERICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQESFERICO"));
			entity.ESQCILINDRICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQCILINDRICO"));
			entity.ESQEIXO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQEIXO"));
			entity.ESQADICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQADICAO"));
			entity.ESQDNP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQDNP"));
			entity.ESQACO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQACO"));
			entity.LENTES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LENTES"));
			entity.ARMACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ARMACAO"));
			entity.DISTANCIAPUPILAR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DISTANCIAPUPILAR"));
			entity.DIREITO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIREITO"));
			entity.ESQUERDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESQUERDO"));
			entity.DPA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DPA"));
			entity.MD = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MD"));
			entity.MV = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MV"));


			return entity;
		}
	}
}
