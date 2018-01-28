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
	public partial class PRODUTOSPEDIDOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PRODUTOSPEDIDOProvider()
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
		
		
		public  int Save(PRODUTOSPEDIDOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDPRODPEDIDO == -1)
					dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO", Entity.IDPRODPEDIDO);
					
					
					if(Entity.IDPEDIDO!= null)
						dbCommand.Parameters.AddWithValue("@IDPEDIDO", Entity.IDPEDIDO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDPEDIDO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDPRODUTO!= null)
						dbCommand.Parameters.AddWithValue("@IDPRODUTO", Entity.IDPRODUTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@QUANTIDADE", Entity.QUANTIDADE); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORUNITARIO", Entity.VALORUNITARIO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORTOTAL", Entity.VALORTOTAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@COMISSAO", Entity.COMISSAO); //Coluna 
					
					if(Entity.IDCOR!= null)
						dbCommand.Parameters.AddWithValue("@IDCOR", Entity.IDCOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@TOTALMT", Entity.TOTALMT); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGEXIBIR", Entity.FLAGEXIBIR); //Coluna 
					dbCommand.Parameters.AddWithValue("@DADOSADICIONAIS", Entity.DADOSADICIONAIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALTURA", Entity.ALTURA); //Coluna 
					dbCommand.Parameters.AddWithValue("@LARGURA", Entity.LARGURA); //Coluna 
					
					if(Entity.IDAMBIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDAMBIENTE", Entity.IDAMBIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDAMBIENTE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDPRODUTOMASTER!= null)
						dbCommand.Parameters.AddWithValue("@IDPRODUTOMASTER", Entity.IDPRODUTOMASTER); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDPRODUTOMASTER", DBNull.Value); //ForeignKey 5

                    dbCommand.Parameters.AddWithValue("@PORCPERDAMT", Entity.PORCPERDAMT); //Coluna 
                    dbCommand.Parameters.AddWithValue("@TOTALPERDAMT", Entity.TOTALPERDAMT); //Coluna 

                   dbCommand.Parameters.AddWithValue("@BUSTO", Entity.BUSTO); //Coluna 
                    dbCommand.Parameters.AddWithValue("@QUADRIL", Entity.QUADRIL); //Coluna 
                    dbCommand.Parameters.AddWithValue("@COLARINHO", Entity.COLARINHO); //Coluna 
                    dbCommand.Parameters.AddWithValue("@MANGA", Entity.MANGA); //Coluna 
                    dbCommand.Parameters.AddWithValue("@BARRA", Entity.BARRA); //Coluna 					
                    dbCommand.Parameters.AddWithValue("@CINTURA", Entity.CINTURA); //Coluna 
                    dbCommand.Parameters.AddWithValue("@IDTAMANHO", Entity.IDTAMANHO); //Coluna 
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPRODPEDIDO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDPRODPEDIDO"].Value.ToString());
				
	
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


        public int Save(int? IDPRODPEDIDO, int IDPEDIDO, int IDPRODUTO, decimal QUANTIDADE, decimal VALORUNITARIO, decimal VALORTOTAL, decimal COMISSAO, int IDCOR, decimal TOTALMT, string FLAGEXIBIR, string DADOSADICIONAIS, decimal ALTURA, decimal LARGURA, int IDAMBIENTE, int IDPRODUTOMASTER, decimal PORCPERDAMT, decimal TOTALPERDAMT)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDPRODPEDIDO == -1)
										dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO", IDPRODPEDIDO);
										
										
										if(IDPEDIDO!= null)
											dbCommand.Parameters.AddWithValue("@IDPEDIDO", IDPEDIDO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDPEDIDO", DBNull.Value); //ForeignKey 5
										
										
										if(IDPRODUTO!= null)
											dbCommand.Parameters.AddWithValue("@IDPRODUTO", IDPRODUTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDPRODUTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@QUANTIDADE", QUANTIDADE); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORUNITARIO", VALORUNITARIO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORTOTAL", VALORTOTAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@COMISSAO", COMISSAO); //Coluna 
										
										if(IDCOR!= null)
											dbCommand.Parameters.AddWithValue("@IDCOR", IDCOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@TOTALMT", TOTALMT); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGEXIBIR", FLAGEXIBIR); //Coluna 
										dbCommand.Parameters.AddWithValue("@DADOSADICIONAIS", DADOSADICIONAIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALTURA", ALTURA); //Coluna 
										dbCommand.Parameters.AddWithValue("@LARGURA", LARGURA); //Coluna 
										
										if(IDAMBIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDAMBIENTE", IDAMBIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDAMBIENTE", DBNull.Value); //ForeignKey 5
										
										
										if(IDPRODUTOMASTER!= null)
											dbCommand.Parameters.AddWithValue("@IDPRODUTOMASTER", IDPRODUTOMASTER); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDPRODUTOMASTER", DBNull.Value); //ForeignKey 5


                                        dbCommand.Parameters.AddWithValue("@PORCPERDAMT", PORCPERDAMT); //Coluna 
                                        dbCommand.Parameters.AddWithValue("@TOTALPERDAMT", TOTALPERDAMT); //Coluna 
					
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPRODPEDIDO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDPRODPEDIDO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDPRODPEDIDO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO",IDPRODPEDIDO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDPRODPEDIDO;

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

		public  PRODUTOSPEDIDOEntity Read(int IDPRODPEDIDO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PRODUTOSPEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPRODPEDIDO",IDPRODPEDIDO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PRODUTOSPEDIDOEntity entity = null;
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

		
		public  PRODUTOSPEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PRODUTOSPEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO  ";
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
		
		public  PRODUTOSPEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PRODUTOSPEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PRODUTOSPEDIDO  order by " + FieldOrder;
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

		private static PRODUTOSPEDIDOCollection ExecuteReader(ref PRODUTOSPEDIDOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PRODUTOSPEDIDOCollection();

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

		private static PRODUTOSPEDIDOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PRODUTOSPEDIDOEntity entity = new PRODUTOSPEDIDOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDPRODPEDIDO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPRODPEDIDO"));
			entity.IDPEDIDO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPEDIDO"));
			entity.IDPRODUTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPRODUTO"));
			entity.QUANTIDADE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANTIDADE"));
			entity.VALORUNITARIO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORUNITARIO"));
			entity.VALORTOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTOTAL"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));
			entity.IDCOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCOR"));
			entity.TOTALMT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALMT"));
			entity.FLAGEXIBIR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGEXIBIR"));
			entity.DADOSADICIONAIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DADOSADICIONAIS"));
			entity.ALTURA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALTURA"));
			entity.LARGURA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("LARGURA"));
			entity.IDAMBIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDAMBIENTE"));
			entity.IDPRODUTOMASTER = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPRODUTOMASTER"));
            entity.PORCPERDAMT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCPERDAMT"));
            entity.BUSTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BUSTO"));
            entity.QUADRIL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUADRIL"));
            entity.COLARINHO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COLARINHO"));
            entity.MANGA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("MANGA"));
            entity.BARRA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BARRA"));
            entity.CINTURA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CINTURA"));
            entity.IDTAMANHO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTAMANHO"));

			return entity;
		}
	}
}


