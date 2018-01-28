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
	public partial class NOTASERVICOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~NOTASERVICOProvider()
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
		
		
		public  int Save(NOTASERVICOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTASERVICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTASERVICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDNOTASERVICO == -1)
					dbCommand.Parameters.AddWithValue("@IDNOTASERVICO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDNOTASERVICO", Entity.IDNOTASERVICO);
					
					dbCommand.Parameters.AddWithValue("@NPS", Entity.NPS); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAEMISSAO", Entity.DATAEMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CODNATUREZA", Entity.CODNATUREZA); //Coluna 
					dbCommand.Parameters.AddWithValue("@REGIMETRIBUTACAO", Entity.REGIMETRIBUTACAO); //Coluna 
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DEDUCAO", Entity.DEDUCAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PIS", Entity.PIS); //Coluna 
					dbCommand.Parameters.AddWithValue("@COFINS", Entity.COFINS); //Coluna 
					dbCommand.Parameters.AddWithValue("@INSS", Entity.INSS); //Coluna 
					dbCommand.Parameters.AddWithValue("@IMPOSTORENDA", Entity.IMPOSTORENDA); //Coluna 
					dbCommand.Parameters.AddWithValue("@CONTRIBUICAOSOCIAL", Entity.CONTRIBUICAOSOCIAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@ISS", Entity.ISS); //Coluna 
					dbCommand.Parameters.AddWithValue("@ISSRETIDO", Entity.ISSRETIDO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OUTRASRETENCOES", Entity.OUTRASRETENCOES); //Coluna 
					dbCommand.Parameters.AddWithValue("@BASECALCULO", Entity.BASECALCULO); //Coluna 
					dbCommand.Parameters.AddWithValue("@ALIQSERVICO", Entity.ALIQSERVICO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DESCONTO", Entity.DESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DESCRICAODETALSERVICO", Entity.DESCRICAODETALSERVICO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALSERVICO", Entity.TOTALSERVICO); //Coluna 
					
					if(Entity.IDCENTROCUSTO!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDFORMAPAGTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", Entity.IDFORMAPAGTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDLOCALCOBRANCA!= null)
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
					
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTASERVICO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDNOTASERVICO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDNOTASERVICO, int NPS, DateTime DATAEMISSAO, int CODNATUREZA, int REGIMETRIBUTACAO, int IDCLIENTE, decimal DEDUCAO, decimal PIS, decimal COFINS, decimal INSS, decimal IMPOSTORENDA, decimal CONTRIBUICAOSOCIAL, decimal ISS, decimal ISSRETIDO, decimal OUTRASRETENCOES, decimal BASECALCULO, decimal ALIQSERVICO, decimal DESCONTO, string OBSERVACAO, string DESCRICAODETALSERVICO, decimal TOTALSERVICO, int IDCENTROCUSTO, int IDFORMAPAGTO, int IDLOCALCOBRANCA)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTASERVICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTASERVICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDNOTASERVICO == -1)
										dbCommand.Parameters.AddWithValue("@IDNOTASERVICO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDNOTASERVICO", IDNOTASERVICO);
										
										dbCommand.Parameters.AddWithValue("@NPS", NPS); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAEMISSAO", DATAEMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CODNATUREZA", CODNATUREZA); //Coluna 
										dbCommand.Parameters.AddWithValue("@REGIMETRIBUTACAO", REGIMETRIBUTACAO); //Coluna 
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DEDUCAO", DEDUCAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PIS", PIS); //Coluna 
										dbCommand.Parameters.AddWithValue("@COFINS", COFINS); //Coluna 
										dbCommand.Parameters.AddWithValue("@INSS", INSS); //Coluna 
										dbCommand.Parameters.AddWithValue("@IMPOSTORENDA", IMPOSTORENDA); //Coluna 
										dbCommand.Parameters.AddWithValue("@CONTRIBUICAOSOCIAL", CONTRIBUICAOSOCIAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@ISS", ISS); //Coluna 
										dbCommand.Parameters.AddWithValue("@ISSRETIDO", ISSRETIDO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OUTRASRETENCOES", OUTRASRETENCOES); //Coluna 
										dbCommand.Parameters.AddWithValue("@BASECALCULO", BASECALCULO); //Coluna 
										dbCommand.Parameters.AddWithValue("@ALIQSERVICO", ALIQSERVICO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DESCONTO", DESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DESCRICAODETALSERVICO", DESCRICAODETALSERVICO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALSERVICO", TOTALSERVICO); //Coluna 
										
										if(IDCENTROCUSTO!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDFORMAPAGTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", IDFORMAPAGTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", DBNull.Value); //ForeignKey 5
										
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTASERVICO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDNOTASERVICO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDNOTASERVICO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTASERVICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTASERVICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTASERVICO",IDNOTASERVICO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDNOTASERVICO;

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

		public  NOTASERVICOEntity Read(int IDNOTASERVICO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTASERVICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTASERVICO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTASERVICO",IDNOTASERVICO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				NOTASERVICOEntity entity = null;
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

		
		public  NOTASERVICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			NOTASERVICOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTASERVICO WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTASERVICO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTASERVICO  ";
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
		
		public  NOTASERVICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			NOTASERVICOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTASERVICO WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTASERVICO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTASERVICO  order by " + FieldOrder;
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

		private static NOTASERVICOCollection ExecuteReader(ref NOTASERVICOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new NOTASERVICOCollection();

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

		private static NOTASERVICOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			NOTASERVICOEntity entity = new NOTASERVICOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDNOTASERVICO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDNOTASERVICO"));
			entity.NPS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("NPS"));
			entity.DATAEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAEMISSAO"));
			entity.CODNATUREZA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODNATUREZA"));
			entity.REGIMETRIBUTACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("REGIMETRIBUTACAO"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DEDUCAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("DEDUCAO"));
			entity.PIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS"));
			entity.COFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS"));
			entity.INSS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("INSS"));
			entity.IMPOSTORENDA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("IMPOSTORENDA"));
			entity.CONTRIBUICAOSOCIAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CONTRIBUICAOSOCIAL"));
			entity.ISS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ISS"));
			entity.ISSRETIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ISSRETIDO"));
			entity.OUTRASRETENCOES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTRASRETENCOES"));
			entity.BASECALCULO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCULO"));
			entity.ALIQSERVICO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQSERVICO"));
			entity.DESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("DESCONTO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.DESCRICAODETALSERVICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAODETALSERVICO"));
			entity.TOTALSERVICO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALSERVICO"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));


			return entity;
		}
	}
}
