//Template gerado utilizando o MyGeneration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using BMSworks.Model;
using BmsSoftware;

namespace BMSworks.Firebird
{
	public partial class DUPLICATARECEBERProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~DUPLICATARECEBERProvider()
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
		
		
		public  int Save(DUPLICATARECEBEREntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(Entity.IDDUPLICATARECEBER!= -1)
	dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER",Entity.IDDUPLICATARECEBER); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NUMERO", Entity.NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAEMISSAO", Entity.DATAEMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAVECTO", Entity.DATAVECTO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAPAGTO", Entity.DATAPAGTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPODUPLICATA", Entity.IDTIPODUPLICATA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDUPLICATA", Entity.VALORDUPLICATA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORMULTA", Entity.VALORMULTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORJUROS", Entity.VALORJUROS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@NOTAFISCAL", Entity.NOTAFISCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@NCHEQUE", Entity.NCHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@DIASATRASO", Entity.DIASATRASO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAATJUROS", Entity.DATAATJUROS); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", Entity.IDFUNCIONARIO); //Coluna 
dbCommand.Parameters.AddWithValue("@COMISSAO", Entity.COMISSAO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDDUPLICATARECEBER"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDDUPLICATARECEBER"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDDUPLICATARECEBER, string NUMERO, int IDCLIENTE, int IDCENTROCUSTO, DateTime DATAEMISSAO, DateTime DATAVECTO, DateTime DATAPAGTO, int IDTIPODUPLICATA, decimal VALORDUPLICATA, decimal VALORDESCONTO, decimal VALORMULTA, decimal VALORPAGO, decimal VALORJUROS, decimal VALORDEVEDOR, string NOTAFISCAL, string NCHEQUE, int IDLOCALCOBRANCA, string OBSERVACAO, int IDSTATUS, int DIASATRASO, DateTime DATAATJUROS, int IDFUNCIONARIO, decimal COMISSAO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(IDDUPLICATARECEBER!= -1)
	dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER", IDDUPLICATARECEBER); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NUMERO", NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAEMISSAO", DATAEMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAVECTO", DATAVECTO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAPAGTO", DATAPAGTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPODUPLICATA", IDTIPODUPLICATA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDUPLICATA", VALORDUPLICATA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORMULTA", VALORMULTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORJUROS", VALORJUROS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@NOTAFISCAL", NOTAFISCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@NCHEQUE", NCHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@DIASATRASO", DIASATRASO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAATJUROS", DATAATJUROS); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", IDFUNCIONARIO); //Coluna 
dbCommand.Parameters.AddWithValue("@COMISSAO", COMISSAO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDDUPLICATARECEBER"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDDUPLICATARECEBER"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDDUPLICATARECEBER)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER",IDDUPLICATARECEBER); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDDUPLICATARECEBER;

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

		public  DUPLICATARECEBEREntity Read(int IDDUPLICATARECEBER)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_DUPLICATARECEBER", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDDUPLICATARECEBER",IDDUPLICATARECEBER); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				DUPLICATARECEBEREntity entity = null;
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

		
		public  DUPLICATARECEBERCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			DUPLICATARECEBERCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM DUPLICATARECEBER WHERE (";

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
						strSqlCommand = "SELECT * FROM DUPLICATARECEBER  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM DUPLICATARECEBER  ";
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
		
		public  DUPLICATARECEBERCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			DUPLICATARECEBERCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM DUPLICATARECEBER WHERE (";

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
						strSqlCommand = "SELECT * FROM DUPLICATARECEBER  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM DUPLICATARECEBER  order by " + FieldOrder;
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

		private static DUPLICATARECEBERCollection ExecuteReader(ref DUPLICATARECEBERCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new DUPLICATARECEBERCollection();

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

		private static DUPLICATARECEBEREntity FillEntityObject(ref FbDataReader DataReader) 
		{
			DUPLICATARECEBEREntity entity = new DUPLICATARECEBEREntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDDUPLICATARECEBER = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDDUPLICATARECEBER"));
			entity.NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMERO"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.DATAEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAEMISSAO"));
			entity.DATAVECTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAVECTO"));
			entity.DATAPAGTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAPAGTO"));
			entity.IDTIPODUPLICATA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPODUPLICATA"));
			entity.VALORDUPLICATA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDUPLICATA"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.VALORMULTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORMULTA"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORJUROS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORJUROS"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.NOTAFISCAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTAFISCAL"));
			entity.NCHEQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NCHEQUE"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.DIASATRASO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("DIASATRASO"));
			entity.DATAATJUROS = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAATJUROS"));
			entity.IDFUNCIONARIO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFUNCIONARIO"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));


			return entity;
		}
	}
}
