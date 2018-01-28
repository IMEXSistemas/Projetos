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
	public partial class PRODUTOFASTProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdFast;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

        ~PRODUTOFASTProvider()
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

        public int Delete(int CODIGO_PRODUTO)
        {
            int result = 0;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("DEL_PRODUTOFAST", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("DEL_PRODUTOFAST", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", CODIGO_PRODUTO); //PrimaryKey



                //Executando consulta
                dbCommand.ExecuteNonQuery();

                result = CODIGO_PRODUTO;

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


        public int Save(PRODUTOSFASTEntity Entity)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

                    dbCommand = new FbCommand("SAV_PRODUTOFAST", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

                    dbCommand = new FbCommand("SAV_PRODUTOFAST", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;


                if (Entity.CODIGO_PRODUTO != -1)
    dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", Entity.CODIGO_PRODUTO); //PrimaryKey 
else
    dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@REFERENCIA_PRODUTO", Entity.REFERENCIA_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_FABRICANTE", Entity.CODIGO_FABRICANTE); //Coluna 
dbCommand.Parameters.AddWithValue("@NOME_PRODUTO", Entity.NOME_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@S_DESCRICAO_PRODUTO", Entity.S_DESCRICAO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO_PRODUTO", Entity.DESCRICAO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO_PRODUTO", Entity.TIPO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@UNIDADE_PRODUTO", Entity.UNIDADE_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ACS_VALORPRODUTO", Entity.ACS_VALORPRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@AGR_PRODUTOVENDA", Entity.AGR_PRODUTOVENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@ACS_QTDPRODUTO", Entity.ACS_QTDPRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IMAGEM_PRODUTO", Entity.IMAGEM_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@TESTE", Entity.TESTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_TIPOTRIBUTACAO", Entity.CODIGO_TIPOTRIBUTACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCTRIBUT_PRODUTO", Entity.PERCTRIBUT_PRODUT); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_CATEGORIA", Entity.CODIGO_CATEGORIA); //Coluna
dbCommand.Parameters.AddWithValue("@MARCA_PRODUTO", Entity.MARCA_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCALIZACAO_PRODUTO", Entity.LOCALIZACAO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ATIVO_PRODUTO", Entity.ATIVO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ID_NCM", Entity.ID_NCM); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_NCM", Entity.CODIGO_NCM); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO_NCM", Entity.DESCRICAO_NCM); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCIPI_PRODUTO", Entity.PERCIPI_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_PRODUTO", Entity.COFINS_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCPIS_PRODUTO", Entity.PERCPIS_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOBRUTO_PRODUTO", Entity.PESOBRUTO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOLIQUIDO_PRODUTO", Entity.PESOLIQUIDO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCSBRVENDA_PRODUTO", Entity.PERCSBRVENDA_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@STAPERCSBRVDA_PRODUTO", Entity.STAPERCSBRVDA_PRODUTO); //Coluna                 
dbCommand.Parameters.AddWithValue("@REPLICAR_PRODUTO", Entity.REPLICAR_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_EMPRESA", Entity.CODIGO_EMPRESA); //Coluna 
dbCommand.Parameters.AddWithValue("@LERPESO_PRODUTO", Entity.LERPESO_PRODUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CODIGO_FORNECEDOR", Entity.CODIGO_FORNECEDOR); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();

                dbCommand.Parameters["@CODIGO_PRODUTO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();

                result = int.Parse(dbCommand.Parameters["@CODIGO_PRODUTO"].Value.ToString());
				
	
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

        public PRODUTOSFASTEntity Read(int CODIGO_PRODUTO)
        {
            FbDataReader reader = null;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("REA_PRODUTOFAST", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("REA_PRODUTOFAST", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@CODIGO_PRODUTO", CODIGO_PRODUTO); //PrimaryKey


                reader = dbCommand.ExecuteReader();

                PRODUTOSFASTEntity entity = null;
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

        public PRODUTOSFASTCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
            PRODUTOSFASTCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PRODUTO WHERE (";

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
						strSqlCommand = "SELECT * FROM PRODUTO  ";
					}
				}
				else
				{
                    strSqlCommand = "SELECT * FROM PRODUTO  ";
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
		
		public  PRODUTOSFASTCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
            PRODUTOSFASTCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM CLIENTE WHERE (";

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
						strSqlCommand = "SELECT * FROM CLIENTE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CLIENTE  order by " + FieldOrder;
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

        private static PRODUTOSFASTCollection ExecuteReader(ref PRODUTOSFASTCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
                collection = new PRODUTOSFASTCollection();

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

        private static PRODUTOSFASTEntity FillEntityObject(ref FbDataReader DataReader) 
		{
            PRODUTOSFASTEntity entity = new PRODUTOSFASTEntity();

   
			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.CODIGO_PRODUTO  =  getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_PRODUTO"));
            entity.REFERENCIA_PRODUTO  = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REFERENCIA_PRODUTO"));
            entity.CODIGO_FABRICANTE   = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODIGO_FABRICANTE"));
            entity.NOME_PRODUTO       = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOME_PRODUTO"));
           // entity.S_DESCRICAO_PRODUTO   = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("S_DESCRICAO_PRODUTO"));
            entity.DESCRICAO_PRODUTO   = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO_PRODUTO"));
            entity.TIPO_PRODUTO          = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPO_PRODUTO"));
            entity.UNIDADE_PRODUTO       = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UNIDADE_PRODUTO"));
            entity.ACS_VALORPRODUTO     = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ACS_VALORPRODUTO"));
            entity.AGR_PRODUTOVENDA     = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("AGR_PRODUTOVENDA"));
            entity.ACS_QTDPRODUTO         = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ACS_QTDPRODUTO"));
        //    entity.IMAGEM_PRODUTO =      getData.ConvertDBValueToByteNullable(DataReader, DataReader.GetOrdinal("IMAGEM_PRODUTO"));
           // entity.TESTE =              getData.ConvertDBValueToByteNullable(DataReader, DataReader.GetOrdinal("TESTE"));
            entity.CODIGO_TIPOTRIBUTACAO =  getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_TIPOTRIBUTACAO"));
            entity.PERCTRIBUT_PRODUT = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PERCTRIBUT_PRODUTO"));
            entity.CODIGO_CATEGORIA     =  getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_CATEGORIA"));
            entity.MARCA_PRODUTO          = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCA_PRODUTO"));
            entity.LOCALIZACAO_PRODUTO    = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOCALIZACAO_PRODUTO"));
            entity.ATIVO_PRODUTO        = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ATIVO_PRODUTO"));
            entity.ID_NCM               = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("ID_NCM"));
            entity.CODIGO_NCM             = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODIGO_NCM"));
            entity.DESCRICAO_NCM         = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO_NCM"));
            entity.PERCIPI_PRODUTO = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PERCIPI_PRODUTO"));
            entity.COFINS_PRODUTO = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("COFINS_PRODUTO"));
            entity.PERCPIS_PRODUTO = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PERCPIS_PRODUTO"));
            entity.PESOBRUTO_PRODUTO = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PESOBRUTO_PRODUTO"));
            entity.PESOLIQUIDO_PRODUTO  =  getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PESOLIQUIDO_PRODUTO"));
            entity.PERCSBRVENDA_PRODUTO = getData.ConvertDBValueToDecimal(DataReader, DataReader.GetOrdinal("PERCSBRVENDA_PRODUTO"));
            entity.STAPERCSBRVDA_PRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("STAPERCSBRVDA_PRODUTO"));
            entity.REPLICAR_PRODUTO       = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REPLICAR_PRODUTO"));
            entity.CODIGO_EMPRESA         = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO_EMPRESA"));
            entity.LERPESO_PRODUTO        = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LERPESO_PRODUTO"));
            entity.CODIGO_FORNECEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CODIGO_FORNECEDOR"));		


			return entity;
		}
	}
}
