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
	public partial class CONFIGBOLETAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CONFIGBOLETAProvider()
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
		
		
		public  int Save(CONFIGBOLETAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(Entity.IDCONFIGBOLETA!= -1)
	dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA",Entity.IDCONFIGBOLETA); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOME", Entity.NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@IDBANCO", Entity.IDBANCO); //Coluna 
dbCommand.Parameters.AddWithValue("@CARTEIRA", Entity.CARTEIRA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONVENIO", Entity.CONVENIO); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPOMODALIDADE", Entity.TIPOMODALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@CODCEDENTE", Entity.CODCEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@NOMECEDENTE", Entity.NOMECEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CPFCNPJCEDENTE", Entity.CPFCNPJCEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@AGENCIA", Entity.AGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGAGENCIA", Entity.DIGAGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTA", Entity.CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGCONTA", Entity.DIGCONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIEDOC", Entity.ESPECIEDOC); //Coluna 
dbCommand.Parameters.AddWithValue("@ACEITE", Entity.ACEITE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORTAXA", Entity.VALORTAXA); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO1", Entity.INSTRUCAO1); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO2", Entity.INSTRUCAO2); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO3", Entity.INSTRUCAO3); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO4", Entity.INSTRUCAO4); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO5", Entity.INSTRUCAO5); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO6", Entity.INSTRUCAO6); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO7", Entity.INSTRUCAO7); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO8", Entity.INSTRUCAO8); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO9", Entity.INSTRUCAO9); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGCEDENTE", Entity.DIGCEDENTE); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCONFIGBOLETA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDCONFIGBOLETA"].Value.ToString());
				
	
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


        public int Save(int? IDCONFIGBOLETA, string NOME, int IDBANCO, string CARTEIRA, string CONVENIO, string TIPOMODALIDADE, int CODCEDENTE, string NOMECEDENTE, string CPFCNPJCEDENTE, string AGENCIA, string DIGAGENCIA, string CONTA, string DIGCONTA, string ESPECIEDOC, string ACEITE, decimal VALORTAXA, string OBSERVACAO, string INSTRUCAO1, string INSTRUCAO2, string INSTRUCAO3, string INSTRUCAO4, string INSTRUCAO5, string INSTRUCAO6, string INSTRUCAO7, string INSTRUCAO8, string INSTRUCAO9, string DIGCEDENTE)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(IDCONFIGBOLETA!= -1)
	dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", IDCONFIGBOLETA); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOME", NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@IDBANCO", IDBANCO); //Coluna 
dbCommand.Parameters.AddWithValue("@CARTEIRA", CARTEIRA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONVENIO", CONVENIO); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPOMODALIDADE", TIPOMODALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@CODCEDENTE", CODCEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@NOMECEDENTE", NOMECEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CPFCNPJCEDENTE", CPFCNPJCEDENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@AGENCIA", AGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGAGENCIA", DIGAGENCIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTA", CONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGCONTA", DIGCONTA); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIEDOC", ESPECIEDOC); //Coluna 
dbCommand.Parameters.AddWithValue("@ACEITE", ACEITE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORTAXA", VALORTAXA); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO1", INSTRUCAO1); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO2", INSTRUCAO2); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO3", INSTRUCAO3); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO4", INSTRUCAO4); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO5", INSTRUCAO5); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO6", INSTRUCAO6); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO7", INSTRUCAO7); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO8", INSTRUCAO8); //Coluna 
dbCommand.Parameters.AddWithValue("@INSTRUCAO9", INSTRUCAO9); //Coluna 
dbCommand.Parameters.AddWithValue("@DIGCEDENTE", DIGCEDENTE); //Coluna 
                
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCONFIGBOLETA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDCONFIGBOLETA"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDCONFIGBOLETA)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA",IDCONFIGBOLETA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDCONFIGBOLETA;

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

		public  CONFIGBOLETAEntity Read(int IDCONFIGBOLETA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CONFIGBOLETA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDCONFIGBOLETA",IDCONFIGBOLETA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CONFIGBOLETAEntity entity = null;
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

		
		public  CONFIGBOLETACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CONFIGBOLETACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CONFIGBOLETA WHERE (";

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
						strSqlCommand = "SELECT * FROM CONFIGBOLETA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CONFIGBOLETA  ";
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
		
		public  CONFIGBOLETACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CONFIGBOLETACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CONFIGBOLETA WHERE (";

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
						strSqlCommand = "SELECT * FROM CONFIGBOLETA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CONFIGBOLETA  order by " + FieldOrder;
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

		private static CONFIGBOLETACollection ExecuteReader(ref CONFIGBOLETACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CONFIGBOLETACollection();

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

		private static CONFIGBOLETAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CONFIGBOLETAEntity entity = new CONFIGBOLETAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDCONFIGBOLETA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCONFIGBOLETA"));
			entity.NOME = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOME"));
			entity.IDBANCO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDBANCO"));
			entity.CARTEIRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARTEIRA"));
			entity.CONVENIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONVENIO"));
			entity.TIPOMODALIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPOMODALIDADE"));
			entity.CODCEDENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODCEDENTE"));
			entity.NOMECEDENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECEDENTE"));
			entity.CPFCNPJCEDENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPFCNPJCEDENTE"));
			entity.AGENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("AGENCIA"));
			entity.DIGAGENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIGAGENCIA"));
			entity.CONTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTA"));
			entity.DIGCONTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIGCONTA"));
			entity.ESPECIEDOC = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIEDOC"));
			entity.ACEITE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ACEITE"));
			entity.VALORTAXA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTAXA"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.INSTRUCAO1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO1"));
			entity.INSTRUCAO2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO2"));
			entity.INSTRUCAO3 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO3"));
			entity.INSTRUCAO4 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO4"));
			entity.INSTRUCAO5 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO5"));
			entity.INSTRUCAO6 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO6"));
			entity.INSTRUCAO7 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO7"));
			entity.INSTRUCAO8 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO8"));
			entity.INSTRUCAO9 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSTRUCAO9"));
            entity.DIGCEDENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DIGCEDENTE"));


			return entity;
		}
	}
}
