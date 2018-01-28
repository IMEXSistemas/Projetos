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
	public partial class NOTAFISCALProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~NOTAFISCALProvider()
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
		
		
		public  int Save(NOTAFISCALEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(Entity.IDNOTAFISCAL!= -1)
	dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL",Entity.IDNOTAFISCAL); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOTAFISCAL", Entity.NOTAFISCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@SERIE", Entity.SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DTSAIDA", Entity.DTSAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORASAIDA", Entity.HORASAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", Entity.IDTIPOMOVIM); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCFOP", Entity.IDCFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@INSCESTSTRIB", Entity.INSCESTSTRIB); //Coluna 
dbCommand.Parameters.AddWithValue("@BASECALCICMS", Entity.BASECALCICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORICMS", Entity.VALORICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASECALCICMSLSUB", Entity.BASECALCICMSLSUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORICMSSUB", Entity.VALORICMSSUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORFRETE", Entity.VALORFRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORSEGURO", Entity.VALORSEGURO); //Coluna 
dbCommand.Parameters.AddWithValue("@OUTRADESPES", Entity.OUTRADESPES); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALIPI", Entity.TOTALIPI); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", Entity.TOTALPRODUTOS); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALNOTA", Entity.TOTALNOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDVENDEDOR", Entity.IDVENDEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", Entity.VALORCOMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", Entity.IDTRANSPORTES); //Coluna 
dbCommand.Parameters.AddWithValue("@PLACA", Entity.PLACA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDUF", Entity.IDUF); //Coluna 
dbCommand.Parameters.AddWithValue("@QUANT", Entity.QUANT); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", Entity.ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@MARCA", Entity.MARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMEROS", Entity.NUMEROS); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOBRUTO", Entity.PESOBRUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOLIQUIDO", Entity.PESOLIQUIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@INFOCOMPLEM", Entity.INFOCOMPLEM); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", Entity.IDCENTROCUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", Entity.IDFORMAPAGTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE", Entity.FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", Entity.PORCACRESCIMO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", Entity.VALORACRESCIMO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTAFISCAL"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDNOTAFISCAL"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDNOTAFISCAL, string NOTAFISCAL, string SERIE, int IDCLIENTE, DateTime DTEMISSAO, DateTime DTSAIDA, string HORASAIDA, int IDTIPOMOVIM, int IDCFOP, string INSCESTSTRIB, decimal BASECALCICMS, decimal VALORICMS, decimal BASECALCICMSLSUB, decimal VALORICMSSUB, decimal VALORFRETE, decimal VALORSEGURO, decimal OUTRADESPES, decimal TOTALIPI, decimal TOTALPRODUTOS, decimal TOTALNOTA, int IDVENDEDOR, decimal VALORCOMISSAO, int IDTRANSPORTES, string PLACA, int IDUF, decimal QUANT, string ESPECIE, string MARCA, string NUMEROS, decimal PESOBRUTO, decimal PESOLIQUIDO, string INFOCOMPLEM, int IDCENTROCUSTO, int IDFORMAPAGTO, int IDLOCALCOBRANCA, int IDSTATUS, decimal VALORPAGO, decimal VALORDEVEDOR, int FRETE, decimal PORCDESCONTO, decimal VALORDESCONTO, decimal PORCACRESCIMO, decimal VALORACRESCIMO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				
if(IDNOTAFISCAL!= -1)
	dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL", IDNOTAFISCAL); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOTAFISCAL", NOTAFISCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@DTSAIDA", DTSAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORASAIDA", HORASAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPOMOVIM", IDTIPOMOVIM); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCFOP", IDCFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@INSCESTSTRIB", INSCESTSTRIB); //Coluna 
dbCommand.Parameters.AddWithValue("@BASECALCICMS", BASECALCICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORICMS", VALORICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASECALCICMSLSUB", BASECALCICMSLSUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORICMSSUB", VALORICMSSUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORFRETE", VALORFRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORSEGURO", VALORSEGURO); //Coluna 
dbCommand.Parameters.AddWithValue("@OUTRADESPES", OUTRADESPES); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALIPI", TOTALIPI); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", TOTALPRODUTOS); //Coluna 
dbCommand.Parameters.AddWithValue("@TOTALNOTA", TOTALNOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDVENDEDOR", IDVENDEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", VALORCOMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", IDTRANSPORTES); //Coluna 
dbCommand.Parameters.AddWithValue("@PLACA", PLACA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDUF", IDUF); //Coluna 
dbCommand.Parameters.AddWithValue("@QUANT", QUANT); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@MARCA", MARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMEROS", NUMEROS); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOBRUTO", PESOBRUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESOLIQUIDO", PESOLIQUIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@INFOCOMPLEM", INFOCOMPLEM); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCENTROCUSTO", IDCENTROCUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFORMAPAGTO", IDFORMAPAGTO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE", FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", PORCACRESCIMO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", VALORACRESCIMO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDNOTAFISCAL"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDNOTAFISCAL"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDNOTAFISCAL)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL",IDNOTAFISCAL); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDNOTAFISCAL;

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

		public  NOTAFISCALEntity Read(int IDNOTAFISCAL)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_NOTAFISCAL", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDNOTAFISCAL",IDNOTAFISCAL); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				NOTAFISCALEntity entity = null;
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

		
		public  NOTAFISCALCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			NOTAFISCALCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTAFISCAL WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTAFISCAL  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTAFISCAL  ";
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
		
		public  NOTAFISCALCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			NOTAFISCALCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM NOTAFISCAL WHERE (";

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
						strSqlCommand = "SELECT * FROM NOTAFISCAL  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM NOTAFISCAL  order by " + FieldOrder;
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

		private static NOTAFISCALCollection ExecuteReader(ref NOTAFISCALCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new NOTAFISCALCollection();

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

		private static NOTAFISCALEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			NOTAFISCALEntity entity = new NOTAFISCALEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDNOTAFISCAL = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDNOTAFISCAL"));
			entity.NOTAFISCAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTAFISCAL"));
			entity.SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTSAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTSAIDA"));
			entity.HORASAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORASAIDA"));
			entity.IDTIPOMOVIM = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOMOVIM"));
			entity.IDCFOP = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.INSCESTSTRIB = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INSCESTSTRIB"));
			entity.BASECALCICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCICMS"));
			entity.VALORICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMS"));
			entity.BASECALCICMSLSUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCICMSLSUB"));
			entity.VALORICMSSUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORICMSSUB"));
			entity.VALORFRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORFRETE"));
			entity.VALORSEGURO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORSEGURO"));
			entity.OUTRADESPES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTRADESPES"));
			entity.TOTALIPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALIPI"));
			entity.TOTALPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPRODUTOS"));
			entity.TOTALNOTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALNOTA"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.PLACA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PLACA"));
			entity.IDUF = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDUF"));
			entity.QUANT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANT"));
			entity.ESPECIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIE"));
			entity.MARCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCA"));
			entity.NUMEROS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROS"));
			entity.PESOBRUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOBRUTO"));
			entity.PESOLIQUIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOLIQUIDO"));
			entity.INFOCOMPLEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INFOCOMPLEM"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.FRETE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("FRETE"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));


			return entity;
		}
	}
}
