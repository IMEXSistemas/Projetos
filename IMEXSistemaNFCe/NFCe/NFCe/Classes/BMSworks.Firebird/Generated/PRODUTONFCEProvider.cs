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
	public partial class PRODUTONFCEProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PRODUTONFCEProvider()
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
		
		
		public  int Save(PRODUTONFCEEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.PRODUTONFCEID == -1)
					dbCommand.Parameters.AddWithValue("@PRODUTONFCEID", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@PRODUTONFCEID", Entity.PRODUTONFCEID);
					
					dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", Entity.CUPOMELETRONICOID); //Coluna 
					
					if(Entity.IDPRODUTO!= null)
						dbCommand.Parameters.AddWithValue("@IDPRODUTO", Entity.IDPRODUTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@QUANTIDADE", Entity.QUANTIDADE); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORUNITARIO", Entity.VALORUNITARIO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORTOTAL", Entity.VALORTOTAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALICMS", Entity.ALICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@BASEICMS", Entity.BASEICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@REDICMS", Entity.REDICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORICMS", Entity.VALORICMS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIPI", Entity.ALIPI); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORIPI", Entity.VALORIPI); //Coluna 
					
					if(Entity.IDUNIDADE!= null)
						dbCommand.Parameters.AddWithValue("@IDUNIDADE", Entity.IDUNIDADE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDUNIDADE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCFOP!= null)
						dbCommand.Parameters.AddWithValue("@IDCFOP", Entity.IDCFOP); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@ALIQPIS", Entity.ALIQPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORPIS", Entity.VALORPIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQCOFINS", Entity.ALIQCOFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORCOFINS", Entity.VALORCOFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLBASEST", Entity.VLBASEST); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLICMSST", Entity.VLICMSST); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLALIQST", Entity.VLALIQST); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLOUTROS", Entity.VLOUTROS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VLTRIBUTOAPROX", Entity.VLTRIBUTOAPROX); //Coluna 
					dbCommand.Parameters.AddWithValue("@ITEM", Entity.ITEM); //Coluna 
                    dbCommand.Parameters.AddWithValue("@IDCST", Entity.IDCST); //Coluna 



                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@PRODUTONFCEID"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@PRODUTONFCEID"].Value.ToString());
				
	
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
		
		
		public  int Save(int? PRODUTONFCEID, int CUPOMELETRONICOID, int IDPRODUTO, decimal QUANTIDADE, decimal VALORUNITARIO, decimal VALORTOTAL, decimal ALICMS, decimal BASEICMS, decimal REDICMS, decimal VALORICMS, decimal ALIPI, decimal VALORIPI, int IDUNIDADE, int IDCFOP, decimal ALIQPIS, decimal VALORPIS, decimal ALIQCOFINS, decimal VALORCOFINS, decimal VLBASEST, decimal VLICMSST, decimal VLALIQST, decimal VLOUTROS, decimal VLTRIBUTOAPROX, int ITEM, int? IDCST)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (PRODUTONFCEID == -1)
										dbCommand.Parameters.AddWithValue("@PRODUTONFCEID", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@PRODUTONFCEID", PRODUTONFCEID);
										
										dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", CUPOMELETRONICOID); //Coluna 
										
										if(IDPRODUTO!= null)
											dbCommand.Parameters.AddWithValue("@IDPRODUTO", IDPRODUTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@QUANTIDADE", QUANTIDADE); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORUNITARIO", VALORUNITARIO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORTOTAL", VALORTOTAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALICMS", ALICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@BASEICMS", BASEICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@REDICMS", REDICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORICMS", VALORICMS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIPI", ALIPI); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORIPI", VALORIPI); //Coluna 
										
										if(IDUNIDADE!= null)
											dbCommand.Parameters.AddWithValue("@IDUNIDADE", IDUNIDADE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDUNIDADE", DBNull.Value); //ForeignKey 5
										
										
										if(IDCFOP!= null)
											dbCommand.Parameters.AddWithValue("@IDCFOP", IDCFOP); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@ALIQPIS", ALIQPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORPIS", VALORPIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQCOFINS", ALIQCOFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORCOFINS", VALORCOFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLBASEST", VLBASEST); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLICMSST", VLICMSST); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLALIQST", VLALIQST); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLOUTROS", VLOUTROS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VLTRIBUTOAPROX", VLTRIBUTOAPROX); //Coluna 
										dbCommand.Parameters.AddWithValue("@ITEM", ITEM); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@IDCST", IDCST); //Coluna 



                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@PRODUTONFCEID"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@PRODUTONFCEID"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int PRODUTONFCEID)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@PRODUTONFCEID",PRODUTONFCEID); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = PRODUTONFCEID;

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

		public  PRODUTONFCEEntity Read(int PRODUTONFCEID)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTONFCE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@PRODUTONFCEID",PRODUTONFCEID); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PRODUTONFCEEntity entity = null;
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

		
		public  PRODUTONFCECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PRODUTONFCECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTONFCE WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTONFCE  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTONFCE  ";
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
		
		public  PRODUTONFCECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PRODUTONFCECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTONFCE WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTONFCE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTONFCE  order by " + FieldOrder;
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

		private static PRODUTONFCECollection ExecuteReader(ref PRODUTONFCECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PRODUTONFCECollection();

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

		private static PRODUTONFCEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PRODUTONFCEEntity entity = new PRODUTONFCEEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.PRODUTONFCEID = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("PRODUTONFCEID"));
			entity.CUPOMELETRONICOID = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CUPOMELETRONICOID"));
			entity.IDPRODUTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPRODUTO"));
			entity.QUANTIDADE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANTIDADE"));
			entity.VALORUNITARIO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORUNITARIO"));
			entity.VALORTOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTOTAL"));
			entity.ALICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALICMS"));
			entity.BASEICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASEICMS"));
			entity.REDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("REDICMS"));
			entity.VALORICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMS"));
			entity.ALIPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIPI"));
			entity.VALORIPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORIPI"));
			entity.IDUNIDADE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDUNIDADE"));
			entity.IDCFOP = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.ALIQPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQPIS"));
			entity.VALORPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPIS"));
			entity.ALIQCOFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQCOFINS"));
			entity.VALORCOFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOFINS"));
			entity.VLBASEST = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLBASEST"));
			entity.VLICMSST = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLICMSST"));
			entity.VLALIQST = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLALIQST"));
			entity.VLOUTROS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLOUTROS"));
			entity.VLTRIBUTOAPROX = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VLTRIBUTOAPROX"));
			entity.ITEM = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("ITEM"));
            entity.IDCST = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCST"));
            


            return entity;
		}
	}
}
