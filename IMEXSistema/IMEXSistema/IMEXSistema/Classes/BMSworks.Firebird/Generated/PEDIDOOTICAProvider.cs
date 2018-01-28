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
	public partial class PEDIDOOTICAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PEDIDOOTICAProvider()
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
		
		
		public  int Save(PEDIDOOTICAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDPEDIDOOTICA == -1)
					dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", Entity.IDPEDIDOOTICA);
					
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DTENTREGA", Entity.DTENTREGA); //Coluna 
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", Entity.PRAZOENTREGA); //Coluna 
					
					if(Entity.IDTRANSPORTES!= null)
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", Entity.IDTRANSPORTES); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDVENDEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", Entity.IDVENDEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", Entity.VALORCOMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", Entity.TOTALPRODUTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALIMPOSTOS", Entity.TOTALIMPOSTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", Entity.PORCACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", Entity.VALORACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALPEDIDO", Entity.TOTALPEDIDO); //Coluna 
					
					if(Entity.IDFORMAPAGAMENTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", Entity.IDFORMAPAGAMENTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
					
					if(Entity.IDLOCALCOBRANCA!= null)
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCENTROCUSTO!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDMEDICO!= null)
						dbCommand.Parameters.AddWithValue("@IDMEDICO", Entity.IDMEDICO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDMEDICO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFORNECEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", Entity.IDFORNECEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@NUMREFERENCIA", Entity.NUMREFERENCIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", Entity.FLAGORCAMENTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDOOTICA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDPEDIDOOTICA"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDPEDIDOOTICA, int IDCLIENTE, DateTime DTEMISSAO, DateTime DTENTREGA, int IDSTATUS, string PRAZOENTREGA, int IDTRANSPORTES, int IDVENDEDOR, decimal VALORCOMISSAO, string OBSERVACAO, decimal TOTALPRODUTOS, decimal TOTALIMPOSTOS, decimal PORCDESCONTO, decimal VALORDESCONTO, decimal PORCACRESCIMO, decimal VALORACRESCIMO, decimal TOTALPEDIDO, int IDFORMAPAGAMENTO, decimal VALORPAGO, decimal VALORDEVEDOR, int IDLOCALCOBRANCA, int IDCENTROCUSTO, int IDMEDICO, int IDFORNECEDOR, string NUMREFERENCIA, string FLAGORCAMENTO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDPEDIDOOTICA == -1)
										dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA", IDPEDIDOOTICA);
										
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DTENTREGA", DTENTREGA); //Coluna 
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", PRAZOENTREGA); //Coluna 
										
										if(IDTRANSPORTES!= null)
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", IDTRANSPORTES); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
										
										
										if(IDVENDEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", IDVENDEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", VALORCOMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", TOTALPRODUTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALIMPOSTOS", TOTALIMPOSTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", PORCACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", VALORACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPEDIDO", TOTALPEDIDO); //Coluna 
										
										if(IDFORMAPAGAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", IDFORMAPAGAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDCENTROCUSTO!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDMEDICO!= null)
											dbCommand.Parameters.AddWithValue("@IDMEDICO", IDMEDICO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDMEDICO", DBNull.Value); //ForeignKey 5
										
										
										if(IDFORNECEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", IDFORNECEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@NUMREFERENCIA", NUMREFERENCIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", FLAGORCAMENTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDOOTICA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDPEDIDOOTICA"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDPEDIDOOTICA)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA",IDPEDIDOOTICA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDPEDIDOOTICA;

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

		public  PEDIDOOTICAEntity Read(int IDPEDIDOOTICA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDOOTICA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDOOTICA",IDPEDIDOOTICA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PEDIDOOTICAEntity entity = null;
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

		
		public  PEDIDOOTICACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PEDIDOOTICACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDOOTICA WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDOOTICA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDOOTICA  ";
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
		
		public  PEDIDOOTICACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PEDIDOOTICACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDOOTICA WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDOOTICA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDOOTICA  order by " + FieldOrder;
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

		private static PEDIDOOTICACollection ExecuteReader(ref PEDIDOOTICACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PEDIDOOTICACollection();

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

		private static PEDIDOOTICAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PEDIDOOTICAEntity entity = new PEDIDOOTICAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDPEDIDOOTICA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPEDIDOOTICA"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTENTREGA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTENTREGA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.PRAZOENTREGA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PRAZOENTREGA"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.TOTALPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPRODUTOS"));
			entity.TOTALIMPOSTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALIMPOSTOS"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));
			entity.TOTALPEDIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPEDIDO"));
			entity.IDFORMAPAGAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGAMENTO"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.IDMEDICO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDMEDICO"));
			entity.IDFORNECEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORNECEDOR"));
			entity.NUMREFERENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMREFERENCIA"));
			entity.FLAGORCAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGORCAMENTO"));


			return entity;
		}
	}
}
