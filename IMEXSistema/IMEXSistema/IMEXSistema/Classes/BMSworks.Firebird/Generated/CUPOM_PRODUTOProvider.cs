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
	public partial class CUPOM_PRODUTOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
        private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdFast;
		
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CUPOM_PRODUTOProvider()
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
		
		
		public  int Save(CUPOM_PRODUTOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

							
			if(Entity.CODIGO_EMPRESA!= -1)
				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA",Entity.CODIGO_EMPRESA); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA", DBNull.Value); //PrimaryKey 
			
			
			if(Entity.CODIGO_CUPOM!= -1)
				dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM",Entity.CODIGO_CUPOM); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM", DBNull.Value); //PrimaryKey 
			
			
			if(Entity.CUPOM_ITEM!= -1)
				dbCommand.Parameters.AddWithValue("@CUPOM_ITEM",Entity.CUPOM_ITEM); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CUPOM_ITEM", DBNull.Value); //PrimaryKey 
			
			dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", Entity.CODIGO_PRODUTO); //ForeignKey 
			dbCommand.Parameters.AddWithValue("@REFERENCIA_PRODUTO", Entity.REFERENCIA_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@NOME_PRODUTO", Entity.NOME_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@QTDADE_PRODUTO", Entity.QTDADE_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALOR_UNITARIO", Entity.VALOR_UNITARIO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", Entity.VALOR_TOTAL); //Coluna 
			dbCommand.Parameters.AddWithValue("@UNIDADE_PRODUTO", Entity.UNIDADE_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@CSTICMS_PRODUTO", Entity.CSTICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTAICMS_PRODUTO", Entity.ALIQUOTAICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@CFOP_PRODUTO", Entity.CFOP_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@BASEICMS_PRODUTO", Entity.BASEICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALORICMS_PRODUTO", Entity.VALORICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTASUBST_PRODUTO", Entity.ALIQUOTASUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALORSUBST_PRODUTO", Entity.VALORSUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@BASESUBST_PRODUTO", Entity.BASESUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTARED_PRODUTO", Entity.ALIQUOTARED_PRODUTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO_EMPRESA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@CODIGO_EMPRESA"].Value.ToString());
				
	
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
		
		
		public  int Save(int? CODIGO_EMPRESA, int? CODIGO_CUPOM, int? CUPOM_ITEM, int? CODIGO_PRODUTO, string REFERENCIA_PRODUTO, string NOME_PRODUTO, decimal QTDADE_PRODUTO, decimal VALOR_UNITARIO, decimal VALOR_TOTAL, string UNIDADE_PRODUTO, string CSTICMS_PRODUTO, decimal ALIQUOTAICMS_PRODUTO, string CFOP_PRODUTO, decimal BASEICMS_PRODUTO, decimal VALORICMS_PRODUTO, decimal ALIQUOTASUBST_PRODUTO, decimal VALORSUBST_PRODUTO, decimal BASESUBST_PRODUTO, decimal ALIQUOTARED_PRODUTO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

							
			if(CODIGO_EMPRESA!= -1)
				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA", CODIGO_EMPRESA); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA", DBNull.Value); //PrimaryKey 
			
			
			if(CODIGO_CUPOM!= -1)
				dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM", CODIGO_CUPOM); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM", DBNull.Value); //PrimaryKey 
			
			
			if(CUPOM_ITEM!= -1)
				dbCommand.Parameters.AddWithValue("@CUPOM_ITEM", CUPOM_ITEM); //PrimaryKey 
			else
				dbCommand.Parameters.AddWithValue("@CUPOM_ITEM", DBNull.Value); //PrimaryKey 
			
			dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", CODIGO_PRODUTO); //ForeignKey 
			dbCommand.Parameters.AddWithValue("@REFERENCIA_PRODUTO", REFERENCIA_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@NOME_PRODUTO", NOME_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@QTDADE_PRODUTO", QTDADE_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALOR_UNITARIO", VALOR_UNITARIO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", VALOR_TOTAL); //Coluna 
			dbCommand.Parameters.AddWithValue("@UNIDADE_PRODUTO", UNIDADE_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@CSTICMS_PRODUTO", CSTICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTAICMS_PRODUTO", ALIQUOTAICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@CFOP_PRODUTO", CFOP_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@BASEICMS_PRODUTO", BASEICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALORICMS_PRODUTO", VALORICMS_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTASUBST_PRODUTO", ALIQUOTASUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@VALORSUBST_PRODUTO", VALORSUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@BASESUBST_PRODUTO", BASESUBST_PRODUTO); //Coluna 
			dbCommand.Parameters.AddWithValue("@ALIQUOTARED_PRODUTO", ALIQUOTARED_PRODUTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO_EMPRESA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@CODIGO_EMPRESA"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int CODIGO_EMPRESA, int CODIGO_CUPOM, int CUPOM_ITEM)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA",CODIGO_EMPRESA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM",CODIGO_CUPOM); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CUPOM_ITEM",CUPOM_ITEM); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = CODIGO_EMPRESA;

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

		public  CUPOM_PRODUTOEntity Read(int CODIGO_EMPRESA, int CODIGO_CUPOM, int CUPOM_ITEM)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CUPOM_PRODUTO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA",CODIGO_EMPRESA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CODIGO_CUPOM",CODIGO_CUPOM); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CUPOM_ITEM",CUPOM_ITEM); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CUPOM_PRODUTOEntity entity = null;
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

		
		public  CUPOM_PRODUTOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CUPOM_PRODUTOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CUPOM_PRODUTO WHERE (";

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
						strSqlCommand = "SELECT * FROM CUPOM_PRODUTO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CUPOM_PRODUTO  ";
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
		
		public  CUPOM_PRODUTOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CUPOM_PRODUTOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CUPOM_PRODUTO WHERE (";

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
						strSqlCommand = "SELECT * FROM CUPOM_PRODUTO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CUPOM_PRODUTO  order by " + FieldOrder;
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

		private static CUPOM_PRODUTOCollection ExecuteReader(ref CUPOM_PRODUTOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CUPOM_PRODUTOCollection();

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

		private static CUPOM_PRODUTOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CUPOM_PRODUTOEntity entity = new CUPOM_PRODUTOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.CODIGO_EMPRESA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_EMPRESA"));
			entity.CODIGO_CUPOM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_CUPOM"));
			entity.CUPOM_ITEM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CUPOM_ITEM"));
			entity.CODIGO_PRODUTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_PRODUTO"));
			entity.REFERENCIA_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REFERENCIA_PRODUTO"));
			entity.NOME_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOME_PRODUTO"));
			entity.QTDADE_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTDADE_PRODUTO"));
			entity.VALOR_UNITARIO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_UNITARIO"));
			entity.VALOR_TOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOTAL"));
			entity.UNIDADE_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UNIDADE_PRODUTO"));
			entity.CSTICMS_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTICMS_PRODUTO"));
			entity.ALIQUOTAICMS_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQUOTAICMS_PRODUTO"));
			entity.CFOP_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CFOP_PRODUTO"));
			entity.BASEICMS_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASEICMS_PRODUTO"));
			entity.VALORICMS_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMS_PRODUTO"));
			entity.ALIQUOTASUBST_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQUOTASUBST_PRODUTO"));
			entity.VALORSUBST_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORSUBST_PRODUTO"));
			entity.BASESUBST_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASESUBST_PRODUTO"));
			entity.ALIQUOTARED_PRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQUOTARED_PRODUTO"));


			return entity;
		}
	}
}
