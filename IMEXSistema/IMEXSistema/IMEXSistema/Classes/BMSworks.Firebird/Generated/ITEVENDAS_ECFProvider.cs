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
	public partial class ITEVENDAS_ECFProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
        private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.PATHRECEPDIGISAT;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ITEVENDAS_ECFProvider()
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
		
		
		public  int Save(ITEVENDAS_ECFEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS_ECF", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS_ECF", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(Entity.CUPOM!= null)
	dbCommand.Parameters.AddWithValue("@CUPOM", Entity.CUPOM); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@CUPOM", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@N_CAIXA", Entity.N_CAIXA); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA", Entity.DATA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORA", Entity.HORA); //Coluna 

if(Entity.OPERADOR!= null)
	dbCommand.Parameters.AddWithValue("@OPERADOR", Entity.OPERADOR); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@OPERADOR", DBNull.Value); //Coluna 3


if(Entity.ITEM!= null)
	dbCommand.Parameters.AddWithValue("@ITEM", Entity.ITEM); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@ITEM", DBNull.Value); //Coluna 3


if(Entity.CODIGO!= null)
	dbCommand.Parameters.AddWithValue("@CODIGO", Entity.CODIGO); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@CODIGO", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@BARRAS", Entity.BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", Entity.DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", Entity.QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO", Entity.PRECO); //Coluna 
dbCommand.Parameters.AddWithValue("@TRIBUTACAO", Entity.TRIBUTACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@ICMS", Entity.ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@ISS", Entity.ISS); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", Entity.UND); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_X", Entity.GRADE_X); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_Y", Entity.GRADE_Y); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", Entity.GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_VENDIDA", Entity.GRADE_VENDIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@SERIAL", Entity.SERIAL); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCONTO", Entity.DESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@ACRESCIMO", Entity.ACRESCIMO); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTAL", Entity.TOTAL); //Coluna 
dbCommand.Parameters.AddWithValue("@OUTRAS_DESP_ACRE", Entity.OUTRAS_DESP_ACRE); //Coluna 

if(Entity.CANCELADO!= null)
	dbCommand.Parameters.AddWithValue("@CANCELADO", Entity.CANCELADO); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@CANCELADO", DBNull.Value); //Coluna 3


if(Entity.OPERADOR_SUP!= null)
	dbCommand.Parameters.AddWithValue("@OPERADOR_SUP", Entity.OPERADOR_SUP); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@OPERADOR_SUP", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@LOTE", Entity.LOTE); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO", Entity.TIPO); //Coluna 
dbCommand.Parameters.AddWithValue("@TABELA_PRECO", Entity.TABELA_PRECO); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ST", Entity.PIS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_VALOR_BC", Entity.PIS_VALOR_BC); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ", Entity.PIS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@TOT_PIS", Entity.TOT_PIS); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ST", Entity.COFINS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_VALOR_BC", Entity.COFINS_VALOR_BC); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ", Entity.COFINS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@TOT_COFINS", Entity.TOT_COFINS); //Coluna 
dbCommand.Parameters.AddWithValue("@CST_ICMS", Entity.CST_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_CUSTO", Entity.PRECO_CUSTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters[""].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters[""].Value.ToString());
				
	
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
		
		
	
		public  ITEVENDAS_ECFEntity Read()
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEVENDAS_ECF", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEVENDAS_ECF", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

				reader = dbCommand.ExecuteReader();

				ITEVENDAS_ECFEntity entity = null;
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

		
		public  ITEVENDAS_ECFCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ITEVENDAS_ECFCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEVENDAS_ECF WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEVENDAS_ECF  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEVENDAS_ECF  ";
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
		
		public  ITEVENDAS_ECFCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ITEVENDAS_ECFCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEVENDAS_ECF WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEVENDAS_ECF  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEVENDAS_ECF  order by " + FieldOrder;
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

		private static ITEVENDAS_ECFCollection ExecuteReader(ref ITEVENDAS_ECFCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ITEVENDAS_ECFCollection();

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

		private static ITEVENDAS_ECFEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ITEVENDAS_ECFEntity entity = new ITEVENDAS_ECFEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.CUPOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CUPOM"));
			entity.N_CAIXA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("N_CAIXA"));
			entity.DATA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA"));
			entity.OPERADOR = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("OPERADOR"));
			entity.ITEM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("ITEM"));
			entity.CODIGO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODIGO"));
			entity.BARRAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BARRAS"));
			entity.DESCRICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO"));
			entity.QTD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD"));
			entity.PRECO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO"));
			entity.TRIBUTACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TRIBUTACAO"));
			entity.ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ICMS"));
			entity.ISS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ISS"));
			entity.UND = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UND"));
			entity.GRADE_X = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_X"));
			entity.GRADE_Y = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_Y"));
			entity.GRADE_QUA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QUA"));
			entity.GRADE_VENDIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_VENDIDA"));
			entity.SERIAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIAL"));
			entity.DESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("DESCONTO"));
			entity.ACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ACRESCIMO"));
			entity.TOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTAL"));
			entity.OUTRAS_DESP_ACRE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTRAS_DESP_ACRE"));
            entity.CANCELADO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CANCELADO"));
			entity.OPERADOR_SUP = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("OPERADOR_SUP"));
			entity.LOTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOTE"));
            entity.TIPO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("TIPO"));
			entity.TABELA_PRECO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TABELA_PRECO"));
			entity.PIS_ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PIS_ST"));
			entity.PIS_VALOR_BC = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_VALOR_BC"));
			entity.PIS_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_ALIQ"));
			entity.TOT_PIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOT_PIS"));
			entity.COFINS_ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COFINS_ST"));
			entity.COFINS_VALOR_BC = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_VALOR_BC"));
			entity.COFINS_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_ALIQ"));
			entity.TOT_COFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOT_COFINS"));
			entity.CST_ICMS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CST_ICMS"));
			entity.PRECO_CUSTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO_CUSTO"));


			return entity;
		}
	}
}
