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
	public partial class MANUTESQUIPAMENTOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~MANUTESQUIPAMENTOProvider()
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
		
		
		public  int Save(MANUTESQUIPAMENTOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDMANUTEESQUIPAMENTO == -1)
					dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO", Entity.IDMANUTEESQUIPAMENTO);
					
					dbCommand.Parameters.AddWithValue("@DATAMANUT", Entity.DATAMANUT); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAPROXIMAMANUT", Entity.DATAPROXIMAMANUT); //Coluna 
					
					if(Entity.IDTIPOMANUTENCAO!= null)
						dbCommand.Parameters.AddWithValue("@IDTIPOMANUTENCAO", Entity.IDTIPOMANUTENCAO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTIPOMANUTENCAO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDSITUACAO!= null)
						dbCommand.Parameters.AddWithValue("@IDSITUACAO", Entity.IDSITUACAO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSITUACAO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFUNCSOLICITANTE!= null)
						dbCommand.Parameters.AddWithValue("@IDFUNCSOLICITANTE", Entity.IDFUNCSOLICITANTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFUNCSOLICITANTE", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFUNCEXECUTOR!= null)
						dbCommand.Parameters.AddWithValue("@IDFUNCEXECUTOR", Entity.IDFUNCEXECUTOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFUNCEXECUTOR", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFORNECEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", Entity.IDFORNECEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORMANUTENCAO", Entity.VALORMANUTENCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@KMMANUTENCAO", Entity.KMMANUTENCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@KMPROXMANUT", Entity.KMPROXMANUT); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					
					if(Entity.IDEQUIPAMENTO!= null)
						dbCommand.Parameters.AddWithValue("@IDEQUIPAMENTO", Entity.IDEQUIPAMENTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDEQUIPAMENTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCENTROCUSTO!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@CODREFERENCIA", Entity.CODREFERENCIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@KMATUAL", Entity.KMATUAL); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDMANUTEESQUIPAMENTO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDMANUTEESQUIPAMENTO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDMANUTEESQUIPAMENTO, DateTime DATAMANUT, DateTime DATAPROXIMAMANUT, int IDTIPOMANUTENCAO, int IDSITUACAO, int IDFUNCSOLICITANTE, int IDFUNCEXECUTOR, int IDFORNECEDOR, decimal VALORMANUTENCAO, int KMMANUTENCAO, int KMPROXMANUT, string OBSERVACAO, int IDEQUIPAMENTO, int IDCENTROCUSTO, string CODREFERENCIA, int KMATUAL)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDMANUTEESQUIPAMENTO == -1)
										dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO", IDMANUTEESQUIPAMENTO);
										
										dbCommand.Parameters.AddWithValue("@DATAMANUT", DATAMANUT); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAPROXIMAMANUT", DATAPROXIMAMANUT); //Coluna 
										
										if(IDTIPOMANUTENCAO!= null)
											dbCommand.Parameters.AddWithValue("@IDTIPOMANUTENCAO", IDTIPOMANUTENCAO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTIPOMANUTENCAO", DBNull.Value); //ForeignKey 5
										
										
										if(IDSITUACAO!= null)
											dbCommand.Parameters.AddWithValue("@IDSITUACAO", IDSITUACAO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSITUACAO", DBNull.Value); //ForeignKey 5
										
										
										if(IDFUNCSOLICITANTE!= null)
											dbCommand.Parameters.AddWithValue("@IDFUNCSOLICITANTE", IDFUNCSOLICITANTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFUNCSOLICITANTE", DBNull.Value); //ForeignKey 5
										
										
										if(IDFUNCEXECUTOR!= null)
											dbCommand.Parameters.AddWithValue("@IDFUNCEXECUTOR", IDFUNCEXECUTOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFUNCEXECUTOR", DBNull.Value); //ForeignKey 5
										
										
										if(IDFORNECEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", IDFORNECEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORNECEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORMANUTENCAO", VALORMANUTENCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@KMMANUTENCAO", KMMANUTENCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@KMPROXMANUT", KMPROXMANUT); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										
										if(IDEQUIPAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDEQUIPAMENTO", IDEQUIPAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDEQUIPAMENTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDCENTROCUSTO!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@CODREFERENCIA", CODREFERENCIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@KMATUAL", KMATUAL); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDMANUTEESQUIPAMENTO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDMANUTEESQUIPAMENTO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDMANUTEESQUIPAMENTO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO",IDMANUTEESQUIPAMENTO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDMANUTEESQUIPAMENTO;

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

		public  MANUTESQUIPAMENTOEntity Read(int IDMANUTEESQUIPAMENTO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_MANUTESQUIPAMENTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDMANUTEESQUIPAMENTO",IDMANUTEESQUIPAMENTO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				MANUTESQUIPAMENTOEntity entity = null;
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

		
		public  MANUTESQUIPAMENTOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			MANUTESQUIPAMENTOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO WHERE (";

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
						strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO  ";
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
		
		public  MANUTESQUIPAMENTOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			MANUTESQUIPAMENTOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO WHERE (";

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
						strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM MANUTESQUIPAMENTO  order by " + FieldOrder;
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

		private static MANUTESQUIPAMENTOCollection ExecuteReader(ref MANUTESQUIPAMENTOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new MANUTESQUIPAMENTOCollection();

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

		private static MANUTESQUIPAMENTOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			MANUTESQUIPAMENTOEntity entity = new MANUTESQUIPAMENTOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDMANUTEESQUIPAMENTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDMANUTEESQUIPAMENTO"));
			entity.DATAMANUT = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAMANUT"));
			entity.DATAPROXIMAMANUT = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAPROXIMAMANUT"));
			entity.IDTIPOMANUTENCAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOMANUTENCAO"));
			entity.IDSITUACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSITUACAO"));
			entity.IDFUNCSOLICITANTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFUNCSOLICITANTE"));
			entity.IDFUNCEXECUTOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFUNCEXECUTOR"));
			entity.IDFORNECEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORNECEDOR"));
			entity.VALORMANUTENCAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORMANUTENCAO"));
			entity.KMMANUTENCAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("KMMANUTENCAO"));
			entity.KMPROXMANUT = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("KMPROXMANUT"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.IDEQUIPAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDEQUIPAMENTO"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.CODREFERENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODREFERENCIA"));
			entity.KMATUAL = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("KMATUAL"));


			return entity;
		}
	}
}
