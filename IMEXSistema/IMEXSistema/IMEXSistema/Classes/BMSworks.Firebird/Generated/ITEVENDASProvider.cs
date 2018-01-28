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
	public partial class ITEVENDASProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ITEVENDASProvider()
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
		
		
		public  string Save(ITEVENDASEntity Entity )
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;


	dbCommand.Parameters.AddWithValue("@NOTA",Entity.NOTA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@MODELO",Entity.MODELO); //PrimaryKey
	dbCommand.Parameters.AddWithValue("@SERIE",Entity.SERIE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@DATA_EMISSAO",Entity.DATA_EMISSAO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@LOJA",Entity.LOJA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@CAIXA",Entity.CAIXA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@ITEM",Entity.ITEM); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@CODIGO", Entity.CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@BARRAS", Entity.BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", Entity.DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CFOP", Entity.COD_CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFOP", Entity.CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CF", Entity.CF); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", Entity.ST); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", Entity.UND); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", Entity.QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", Entity.GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_UNITA", Entity.VALOR_UNITA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", Entity.VALOR_TOTAL); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_CUSTO", Entity.VALOR_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_LISTA", Entity.VALOR_LISTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DESCO", Entity.VALOR_DESCO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FRETE", Entity.VALOR_FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_SEGUR", Entity.VALOR_SEGUR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_OUTDE", Entity.VALOR_OUTDE); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_ICMS", Entity.ALIQ_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS", Entity.BASE_CAL_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS", Entity.VALOR_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS_SUB", Entity.BASE_CAL_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS_SUB", Entity.VALOR_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_SIMPLES", Entity.BASE_CAL_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ST", Entity.PIS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE", Entity.PIS_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ", Entity.PIS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_TOT", Entity.PIS_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_BASE", Entity.PIS_SUB_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_ALIQ", Entity.PIS_SUB_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_TOT", Entity.PIS_SUB_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ST", Entity.COFINS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE", Entity.COFINS_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ", Entity.COFINS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_TOT", Entity.COFINS_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_BASE", Entity.COFINS_SUB_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_ALIQ", Entity.COFINS_SUB_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_TOT", Entity.COFINS_SUB_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI", Entity.ALIQ_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_PRO", Entity.VALOR_TOT_PRO); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADA", Entity.CANCELADA); //Coluna 
dbCommand.Parameters.AddWithValue("@LIVRE", Entity.LIVRE); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE_CONTA_1", Entity.FRETE_CONTA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_SAIDA", Entity.DATA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORA_SAIDA", Entity.HORA_SAIDA); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@NOTA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = dbCommand.Parameters["@NOTA"].Value.ToString();
				
	
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
		
		
		public  string Save(string NOTA, string MODELO, string SERIE, DateTime? DATA_EMISSAO, string LOJA, string CAIXA, int? ITEM, string CODIGO, string BARRAS, string DESCRICAO, string COD_CFOP, string CFOP, string CF, string ST, string UND, decimal QTD, string GRADE_QUA, decimal VALOR_UNITA, decimal VALOR_TOTAL, decimal VALOR_CUSTO, decimal VALOR_LISTA, decimal VALOR_DESCO, decimal VALOR_FRETE, decimal VALOR_SEGUR, decimal VALOR_OUTDE, decimal ALIQ_ICMS, decimal BASE_CAL_ICMS, decimal VALOR_ICMS, decimal BASE_CAL_ICMS_SUB, decimal VALOR_ICMS_SUB, decimal BASE_CAL_SIMPLES, string PIS_ST, decimal PIS_BASE, decimal PIS_ALIQ, decimal PIS_TOT, decimal PIS_SUB_BASE, decimal PIS_SUB_ALIQ, decimal PIS_SUB_TOT, string COFINS_ST, decimal COFINS_BASE, decimal COFINS_ALIQ, decimal COFINS_TOT, decimal COFINS_SUB_BASE, decimal COFINS_SUB_ALIQ, decimal COFINS_SUB_TOT, decimal ALIQ_IPI, decimal VALOR_TOT_PRO, short CANCELADA, short LIVRE, string FRETE_CONTA_1, DateTime DATA_SAIDA, TimeSpan HORA_SAIDA)
		{
            string result ="0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ITEVENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;
	dbCommand.Parameters.AddWithValue("@NOTA", NOTA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@MODELO", MODELO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@DATA_EMISSAO", DATA_EMISSAO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@LOJA", LOJA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@CAIXA", CAIXA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@ITEM", ITEM); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@CODIGO", CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@BARRAS", BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CFOP", COD_CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFOP", CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CF", CF); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", ST); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", UND); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_UNITA", VALOR_UNITA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOTAL", VALOR_TOTAL); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_CUSTO", VALOR_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_LISTA", VALOR_LISTA); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_DESCO", VALOR_DESCO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FRETE", VALOR_FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_SEGUR", VALOR_SEGUR); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_OUTDE", VALOR_OUTDE); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_ICMS", ALIQ_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS", BASE_CAL_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS", VALOR_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS_SUB", BASE_CAL_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS_SUB", VALOR_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_SIMPLES", BASE_CAL_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ST", PIS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE", PIS_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ", PIS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_TOT", PIS_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_BASE", PIS_SUB_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_ALIQ", PIS_SUB_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_SUB_TOT", PIS_SUB_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ST", COFINS_ST); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE", COFINS_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ", COFINS_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_TOT", COFINS_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_BASE", COFINS_SUB_BASE); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_ALIQ", COFINS_SUB_ALIQ); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_SUB_TOT", COFINS_SUB_TOT); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI", ALIQ_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_PRO", VALOR_TOT_PRO); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADA", CANCELADA); //Coluna 
dbCommand.Parameters.AddWithValue("@LIVRE", LIVRE); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE_CONTA_1", FRETE_CONTA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_SAIDA", DATA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORA_SAIDA", HORA_SAIDA); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@NOTA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = dbCommand.Parameters["@NOTA"].Value.ToString();
	
				
	
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
		
		
		public  string Delete(string NOTA)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ITEVENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ITEVENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@NOTA",NOTA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = NOTA;

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

		public  ITEVENDASEntity Read(string NOTA, string MODELO, string SERIE, DateTime DATA_EMISSAO, string LOJA, string CAIXA, int ITEM)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEVENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ITEVENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@NOTA",NOTA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@MODELO",MODELO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@SERIE",SERIE); //PrimaryKey
dbCommand.Parameters.AddWithValue("@DATA_EMISSAO",DATA_EMISSAO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@LOJA",LOJA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CAIXA",CAIXA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@ITEM",ITEM); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				ITEVENDASEntity entity = null;
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

		
		public  ITEVENDASCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ITEVENDASCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEVENDAS WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEVENDAS  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEVENDAS  ";
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
		
		public  ITEVENDASCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ITEVENDASCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ITEVENDAS WHERE (";

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
						strSqlCommand = "SELECT * FROM ITEVENDAS  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ITEVENDAS  order by " + FieldOrder;
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

		private static ITEVENDASCollection ExecuteReader(ref ITEVENDASCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ITEVENDASCollection();

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

		private static ITEVENDASEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ITEVENDASEntity entity = new ITEVENDASEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.NOTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTA"));
							entity.DATA_EMISSAO = getData.ConvertDBValueToDateTime(DataReader, DataReader.GetOrdinal("DATA_EMISSAO"));
			entity.ITEM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("ITEM"));
			entity.CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODIGO"));
			entity.BARRAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BARRAS"));
			entity.DESCRICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO"));
			entity.COD_CFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COD_CFOP"));
			entity.CFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CFOP"));
			entity.CF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CF"));
			entity.ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ST"));
			entity.UND = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UND"));
			entity.QTD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD"));
			entity.GRADE_QUA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QUA"));
			entity.VALOR_UNITA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_UNITA"));
			entity.VALOR_TOTAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOTAL"));
			entity.VALOR_CUSTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_CUSTO"));
			entity.VALOR_LISTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_LISTA"));
			entity.VALOR_DESCO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_DESCO"));
			entity.VALOR_FRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_FRETE"));
			entity.VALOR_SEGUR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_SEGUR"));
			entity.VALOR_OUTDE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_OUTDE"));
			entity.ALIQ_ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQ_ICMS"));
			entity.BASE_CAL_ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASE_CAL_ICMS"));
			entity.VALOR_ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_ICMS"));
			entity.BASE_CAL_ICMS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASE_CAL_ICMS_SUB"));
			entity.VALOR_ICMS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_ICMS_SUB"));
			entity.BASE_CAL_SIMPLES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASE_CAL_SIMPLES"));
			entity.PIS_ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PIS_ST"));
			entity.PIS_BASE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_BASE"));
			entity.PIS_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_ALIQ"));
			entity.PIS_TOT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_TOT"));
			entity.PIS_SUB_BASE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_SUB_BASE"));
			entity.PIS_SUB_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_SUB_ALIQ"));
			entity.PIS_SUB_TOT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_SUB_TOT"));
			entity.COFINS_ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COFINS_ST"));
			entity.COFINS_BASE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_BASE"));
			entity.COFINS_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_ALIQ"));
			entity.COFINS_TOT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_TOT"));
			entity.COFINS_SUB_BASE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_SUB_BASE"));
			entity.COFINS_SUB_ALIQ = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_SUB_ALIQ"));
			entity.COFINS_SUB_TOT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_SUB_TOT"));
			entity.ALIQ_IPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQ_IPI"));
			entity.VALOR_TOT_PRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOT_PRO"));
            entity.CANCELADA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CANCELADA"));
            entity.LIVRE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("LIVRE"));
			entity.FRETE_CONTA_1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FRETE_CONTA_1"));
			entity.DATA_SAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA_SAIDA"));


			return entity;
		}
	}
}
