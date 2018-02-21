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
	public partial class CUPOMELETRONICOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CUPOMELETRONICOProvider()
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
		
		
		public  int Save(CUPOMELETRONICOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.CUPOMELETRONICOID == -1)
					dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", Entity.CUPOMELETRONICOID);
					
					dbCommand.Parameters.AddWithValue("@NUMERONFCE", Entity.NUMERONFCE); //Coluna 
					dbCommand.Parameters.AddWithValue("@SERIE", Entity.SERIE); //Coluna 
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DTSAIDA", Entity.DTSAIDA); //Coluna 
					dbCommand.Parameters.AddWithValue("@HORASAIDA", Entity.HORASAIDA); //Coluna 
					
					if(Entity.IDCFOP!= null)
						dbCommand.Parameters.AddWithValue("@IDCFOP", Entity.IDCFOP); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@TOTALNOTA", Entity.TOTALNOTA); //Coluna 
					
					if(Entity.IDVENDEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", Entity.IDVENDEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORTROCO", Entity.VALORTROCO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CHAVEACESSO", Entity.CHAVEACESSO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					
					if(Entity.IDSTATUSNFCE!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUSNFCE", Entity.IDSTATUSNFCE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUSNFCE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDTIPOPAGAMENTO!= null)
						dbCommand.Parameters.AddWithValue("@IDTIPOPAGAMENTO", Entity.IDTIPOPAGAMENTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTIPOPAGAMENTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDLOCALCOBRANCA!= null)
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFORMAPAGTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", Entity.IDFORMAPAGTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORFINAL", Entity.VALORFINAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGENVIADO", Entity.FLAGENVIADO); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGCONTINGENCIA", Entity.FLAGCONTINGENCIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@STRQRCODE", Entity.STRQRCODE); //Coluna 
					dbCommand.Parameters.AddWithValue("@PROTOCOLO", Entity.PROTOCOLO); //Coluna 
					dbCommand.Parameters.AddWithValue("@AMBIENTE", Entity.AMBIENTE); //Coluna 
					dbCommand.Parameters.AddWithValue("@PROTOCOLOCANCEL", Entity.PROTOCOLOCANCEL); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODOPERADORACARTAO", Entity.CODOPERADORACARTAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@NOMEOPERADORACARTAO", Entity.NOMEOPERADORACARTAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@NUMEROAUTORIZACARTAO", Entity.NUMEROAUTORIZACARTAO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CUPOMELETRONICOID"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@CUPOMELETRONICOID"].Value.ToString());
				
	
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
		
		
		public  int Save(int? CUPOMELETRONICOID, int NUMERONFCE, string SERIE, int IDCLIENTE, DateTime DTEMISSAO, DateTime DTSAIDA, string HORASAIDA, int IDCFOP, decimal TOTALNOTA, int IDVENDEDOR, decimal VALORPAGO, decimal VALORDEVEDOR, decimal VALORTROCO, string CHAVEACESSO, string OBSERVACAO, int IDSTATUSNFCE, int IDTIPOPAGAMENTO, int IDLOCALCOBRANCA, int IDFORMAPAGTO, decimal VALORFINAL, decimal PORCDESCONTO, decimal VALORDESCONTO, string FLAGENVIADO, string FLAGCONTINGENCIA, string STRQRCODE, string PROTOCOLO, string AMBIENTE, string PROTOCOLOCANCEL, int CODOPERADORACARTAO, string NOMEOPERADORACARTAO, string NUMEROAUTORIZACARTAO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (CUPOMELETRONICOID == -1)
										dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID", CUPOMELETRONICOID);
										
										dbCommand.Parameters.AddWithValue("@NUMERONFCE", NUMERONFCE); //Coluna 
										dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //Coluna 
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DTSAIDA", DTSAIDA); //Coluna 
										dbCommand.Parameters.AddWithValue("@HORASAIDA", HORASAIDA); //Coluna 
										
										if(IDCFOP!= null)
											dbCommand.Parameters.AddWithValue("@IDCFOP", IDCFOP); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCFOP", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@TOTALNOTA", TOTALNOTA); //Coluna 
										
										if(IDVENDEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", IDVENDEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORTROCO", VALORTROCO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CHAVEACESSO", CHAVEACESSO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										
										if(IDSTATUSNFCE!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUSNFCE", IDSTATUSNFCE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUSNFCE", DBNull.Value); //ForeignKey 5
										
										
										if(IDTIPOPAGAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDTIPOPAGAMENTO", IDTIPOPAGAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTIPOPAGAMENTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDFORMAPAGTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", IDFORMAPAGTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORFINAL", VALORFINAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGENVIADO", FLAGENVIADO); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGCONTINGENCIA", FLAGCONTINGENCIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@STRQRCODE", STRQRCODE); //Coluna 
										dbCommand.Parameters.AddWithValue("@PROTOCOLO", PROTOCOLO); //Coluna 
										dbCommand.Parameters.AddWithValue("@AMBIENTE", AMBIENTE); //Coluna 
										dbCommand.Parameters.AddWithValue("@PROTOCOLOCANCEL", PROTOCOLOCANCEL); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODOPERADORACARTAO", CODOPERADORACARTAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@NOMEOPERADORACARTAO", NOMEOPERADORACARTAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@NUMEROAUTORIZACARTAO", NUMEROAUTORIZACARTAO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CUPOMELETRONICOID"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@CUPOMELETRONICOID"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int CUPOMELETRONICOID)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID",CUPOMELETRONICOID); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = CUPOMELETRONICOID;

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

		public  CUPOMELETRONICOEntity Read(int CUPOMELETRONICOID)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CUPOMELETRONICOID",CUPOMELETRONICOID); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CUPOMELETRONICOEntity entity = null;
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

		
		public  CUPOMELETRONICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CUPOMELETRONICOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CUPOMELETRONICO WHERE (";

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
						strSqlCommand = "SELECT * FROM CUPOMELETRONICO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CUPOMELETRONICO  ";
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
		
		public  CUPOMELETRONICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CUPOMELETRONICOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CUPOMELETRONICO WHERE (";

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
						strSqlCommand = "SELECT * FROM CUPOMELETRONICO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CUPOMELETRONICO  order by " + FieldOrder;
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

		private static CUPOMELETRONICOCollection ExecuteReader(ref CUPOMELETRONICOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CUPOMELETRONICOCollection();

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

		private static CUPOMELETRONICOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CUPOMELETRONICOEntity entity = new CUPOMELETRONICOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.CUPOMELETRONICOID = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CUPOMELETRONICOID"));
			entity.NUMERONFCE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("NUMERONFCE"));
			entity.SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTSAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTSAIDA"));
			entity.HORASAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORASAIDA"));
			entity.IDCFOP = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.TOTALNOTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALNOTA"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.VALORTROCO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTROCO"));
			entity.CHAVEACESSO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEACESSO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.IDSTATUSNFCE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUSNFCE"));
			entity.IDTIPOPAGAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOPAGAMENTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.VALORFINAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORFINAL"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.FLAGENVIADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGENVIADO"));
			entity.FLAGCONTINGENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCONTINGENCIA"));
			entity.STRQRCODE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("STRQRCODE"));
			entity.PROTOCOLO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROTOCOLO"));
			entity.AMBIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("AMBIENTE"));
			entity.PROTOCOLOCANCEL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROTOCOLOCANCEL"));
			entity.CODOPERADORACARTAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODOPERADORACARTAO"));
			entity.NOMEOPERADORACARTAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEOPERADORACARTAO"));
			entity.NUMEROAUTORIZACARTAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROAUTORIZACARTAO"));


			return entity;
		}
	}
}
