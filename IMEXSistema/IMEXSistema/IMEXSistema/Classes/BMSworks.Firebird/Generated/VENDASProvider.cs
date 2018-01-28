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
	public partial class VENDASProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~VENDASProvider()
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
		
		
		public  int Save(VENDASEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_VENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_VENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@NOTA",Entity.NOTA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@MODELO",Entity.MODELO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@SERIE",Entity.SERIE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@DATA_EMISSAO",Entity.DATA_EMISSAO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@LOJA",Entity.LOJA); //PrimaryKey
	dbCommand.Parameters.AddWithValue("@CAIXA",Entity.CAIXA); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@ENTRADA", Entity.ENTRADA); //Coluna 
dbCommand.Parameters.AddWithValue("@SAIDA", Entity.SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONSUMO", Entity.CONSUMO); //Coluna 
dbCommand.Parameters.AddWithValue("@SERVICO", Entity.SERVICO); //Coluna 
dbCommand.Parameters.AddWithValue("@NATUREZA", Entity.NATUREZA); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CFOP", Entity.COD_CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFOP", Entity.CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFPS", Entity.CFPS); //Coluna 
dbCommand.Parameters.AddWithValue("@CLIENTE", Entity.CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@PEDIDO", Entity.PEDIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@ORCAMENTO", Entity.ORCAMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@OS", Entity.OS); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_SAIDA", Entity.DATA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORA_SAIDA", Entity.HORA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IE_SUBSTITUTO", Entity.IE_SUBSTITUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", Entity.OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@OBS_LIV_FIS", Entity.OBS_LIV_FIS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS", Entity.BASE_CAL_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS", Entity.VALOR_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS_SUB", Entity.BASE_CAL_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS_SUB", Entity.VALOR_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_PRO", Entity.VALOR_TOT_PRO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FRETE", Entity.VALOR_FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_SEGURO", Entity.VALOR_SEGURO); //Coluna 
dbCommand.Parameters.AddWithValue("@OUTRAS_DESPESAS", Entity.OUTRAS_DESPESAS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_IPI", Entity.VALOR_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_SER", Entity.VALOR_TOT_SER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ISS", Entity.VALOR_ISS); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCE_ISS", Entity.PERCE_ISS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_IRRF", Entity.VALOR_IRRF); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_INSS", Entity.VALOR_INSS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_PIS", Entity.VALOR_PIS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_PIS_SUB", Entity.VALOR_PIS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_COFINS", Entity.VALOR_COFINS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_COFINS_SUB", Entity.VALOR_COFINS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_CS", Entity.VALOR_CS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_LIQ_SER", Entity.VALOR_LIQ_SER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_NOTA", Entity.VALOR_TOT_NOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CRED_ICM_SIMPLES", Entity.CRED_ICM_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@TRANSPORTADORA_1", Entity.TRANSPORTADORA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE_CONTA_1", Entity.FRETE_CONTA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@QUANTIDADE", Entity.QUANTIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", Entity.ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@MARCA", Entity.MARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMERO", Entity.NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO_BRUTO", Entity.PESO_BRUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO_LIQUIDO", Entity.PESO_LIQUIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@VENDEDOR", Entity.VENDEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@CUSTO_FINANCEIRO", Entity.CUSTO_FINANCEIRO); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_STATUS", Entity.NFE_STATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_CHAVE", Entity.NFE_CHAVE); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_RECENT", Entity.NFE_RECENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_PROENT", Entity.NFE_PROENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_DATHORENT", Entity.NFE_DATHORENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_PROCAN", Entity.NFE_PROCAN); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_DATHORCAN", Entity.NFE_DATHORCAN); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_AMBIENTE", Entity.NFE_AMBIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADA", Entity.CANCELADA); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@NOTA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@NOTA"].Value.ToString());
				
	
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
		
		
		public  string Save(string NOTA, string MODELO, string SERIE, DateTime DATA_EMISSAO, string LOJA, string CAIXA, string ENTRADA, string SAIDA, string CONSUMO, string SERVICO, string NATUREZA, string COD_CFOP, string CFOP, string CFPS, string CLIENTE, string PEDIDO, string ORCAMENTO, string OS, DateTime DATA_SAIDA, TimeSpan HORA_SAIDA, string IE_SUBSTITUTO, string OBSERVACOES, string OBS_LIV_FIS, decimal BASE_CAL_ICMS, decimal VALOR_ICMS, decimal BASE_CAL_ICMS_SUB, decimal VALOR_ICMS_SUB, decimal VALOR_TOT_PRO, decimal VALOR_FRETE, decimal VALOR_SEGURO, decimal OUTRAS_DESPESAS, decimal VALOR_IPI, decimal VALOR_TOT_SER, decimal VALOR_ISS, decimal PERCE_ISS, decimal VALOR_IRRF, decimal VALOR_INSS, decimal VALOR_PIS, decimal VALOR_PIS_SUB, decimal VALOR_COFINS, decimal VALOR_COFINS_SUB, decimal VALOR_CS, decimal VALOR_LIQ_SER, decimal VALOR_TOT_NOTA, decimal CRED_ICM_SIMPLES, string TRANSPORTADORA_1, string FRETE_CONTA_1, string QUANTIDADE, string ESPECIE, string MARCA, string NUMERO, string PESO_BRUTO, string PESO_LIQUIDO, string VENDEDOR, decimal CUSTO_FINANCEIRO, string NFE_STATUS, string NFE_CHAVE, string NFE_RECENT, string NFE_PROENT, DateTime NFE_DATHORENT, string NFE_PROCAN, DateTime NFE_DATHORCAN, string NFE_AMBIENTE, short CANCELADA)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_VENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_VENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;


	dbCommand.Parameters.AddWithValue("@NOTA", NOTA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@MODELO", MODELO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@DATA_EMISSAO", DATA_EMISSAO); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@LOJA", LOJA); //PrimaryKey 
	dbCommand.Parameters.AddWithValue("@CAIXA", CAIXA); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@ENTRADA", ENTRADA); //Coluna 
dbCommand.Parameters.AddWithValue("@SAIDA", SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONSUMO", CONSUMO); //Coluna 
dbCommand.Parameters.AddWithValue("@SERVICO", SERVICO); //Coluna 
dbCommand.Parameters.AddWithValue("@NATUREZA", NATUREZA); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_CFOP", COD_CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFOP", CFOP); //Coluna 
dbCommand.Parameters.AddWithValue("@CFPS", CFPS); //Coluna 
dbCommand.Parameters.AddWithValue("@CLIENTE", CLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@PEDIDO", PEDIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@ORCAMENTO", ORCAMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@OS", OS); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_SAIDA", DATA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@HORA_SAIDA", HORA_SAIDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IE_SUBSTITUTO", IE_SUBSTITUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@OBS_LIV_FIS", OBS_LIV_FIS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS", BASE_CAL_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS", VALOR_ICMS); //Coluna 
dbCommand.Parameters.AddWithValue("@BASE_CAL_ICMS_SUB", BASE_CAL_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ICMS_SUB", VALOR_ICMS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_PRO", VALOR_TOT_PRO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_FRETE", VALOR_FRETE); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_SEGURO", VALOR_SEGURO); //Coluna 
dbCommand.Parameters.AddWithValue("@OUTRAS_DESPESAS", OUTRAS_DESPESAS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_IPI", VALOR_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_SER", VALOR_TOT_SER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_ISS", VALOR_ISS); //Coluna 
dbCommand.Parameters.AddWithValue("@PERCE_ISS", PERCE_ISS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_IRRF", VALOR_IRRF); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_INSS", VALOR_INSS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_PIS", VALOR_PIS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_PIS_SUB", VALOR_PIS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_COFINS", VALOR_COFINS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_COFINS_SUB", VALOR_COFINS_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_CS", VALOR_CS); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_LIQ_SER", VALOR_LIQ_SER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_TOT_NOTA", VALOR_TOT_NOTA); //Coluna 
dbCommand.Parameters.AddWithValue("@CRED_ICM_SIMPLES", CRED_ICM_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@TRANSPORTADORA_1", TRANSPORTADORA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@FRETE_CONTA_1", FRETE_CONTA_1); //Coluna 
dbCommand.Parameters.AddWithValue("@QUANTIDADE", QUANTIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@ESPECIE", ESPECIE); //Coluna 
dbCommand.Parameters.AddWithValue("@MARCA", MARCA); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMERO", NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO_BRUTO", PESO_BRUTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO_LIQUIDO", PESO_LIQUIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@VENDEDOR", VENDEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@CUSTO_FINANCEIRO", CUSTO_FINANCEIRO); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_STATUS", NFE_STATUS); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_CHAVE", NFE_CHAVE); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_RECENT", NFE_RECENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_PROENT", NFE_PROENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_DATHORENT", NFE_DATHORENT); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_PROCAN", NFE_PROCAN); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_DATHORCAN", NFE_DATHORCAN); //Coluna 
dbCommand.Parameters.AddWithValue("@NFE_AMBIENTE", NFE_AMBIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CANCELADA", CANCELADA); //Coluna 
	
				
								
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

					dbCommand = new FbCommand("Del_VENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_VENDAS", dbCnn);
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

		public  VENDASEntity Read(string NOTA, string MODELO, string SERIE, DateTime DATA_EMISSAO, string LOJA, string CAIXA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_VENDAS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_VENDAS", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@NOTA",NOTA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@MODELO",MODELO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@SERIE",SERIE); //PrimaryKey
dbCommand.Parameters.AddWithValue("@DATA_EMISSAO",DATA_EMISSAO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@LOJA",LOJA); //PrimaryKey
dbCommand.Parameters.AddWithValue("@CAIXA",CAIXA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				VENDASEntity entity = null;
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

		
		public  VENDASCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			VENDASCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM VENDAS WHERE (";

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
						strSqlCommand = "SELECT * FROM VENDAS  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM VENDAS  ";
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
		
		public  VENDASCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			VENDASCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM VENDAS WHERE (";

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
						strSqlCommand = "SELECT * FROM VENDAS  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM VENDAS  order by " + FieldOrder;
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

		private static VENDASCollection ExecuteReader(ref VENDASCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new VENDASCollection();

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

		private static VENDASEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			VENDASEntity entity = new VENDASEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.NOTA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOTA"));
							entity.DATA_EMISSAO = getData.ConvertDBValueToDateTime(DataReader, DataReader.GetOrdinal("DATA_EMISSAO"));
			entity.ENTRADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENTRADA"));
			entity.SAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SAIDA"));
			entity.CONSUMO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONSUMO"));
			entity.SERVICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERVICO"));
			entity.NATUREZA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NATUREZA"));
			entity.COD_CFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COD_CFOP"));
			entity.CFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CFOP"));
			entity.CFPS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CFPS"));
			entity.CLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CLIENTE"));
			entity.PEDIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PEDIDO"));
			entity.ORCAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ORCAMENTO"));
			entity.OS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OS"));
			entity.DATA_SAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA_SAIDA"));
			entity.IE_SUBSTITUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IE_SUBSTITUTO"));
			entity.OBSERVACOES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACOES"));
			entity.OBS_LIV_FIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBS_LIV_FIS"));
			entity.BASE_CAL_ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASE_CAL_ICMS"));
			entity.VALOR_ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_ICMS"));
			entity.BASE_CAL_ICMS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASE_CAL_ICMS_SUB"));
			entity.VALOR_ICMS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_ICMS_SUB"));
			entity.VALOR_TOT_PRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOT_PRO"));
			entity.VALOR_FRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_FRETE"));
			entity.VALOR_SEGURO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_SEGURO"));
			entity.OUTRAS_DESPESAS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("OUTRAS_DESPESAS"));
			entity.VALOR_IPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_IPI"));
			entity.VALOR_TOT_SER = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOT_SER"));
			entity.VALOR_ISS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_ISS"));
			entity.PERCE_ISS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PERCE_ISS"));
			entity.VALOR_IRRF = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_IRRF"));
			entity.VALOR_INSS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_INSS"));
			entity.VALOR_PIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_PIS"));
			entity.VALOR_PIS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_PIS_SUB"));
			entity.VALOR_COFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_COFINS"));
			entity.VALOR_COFINS_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_COFINS_SUB"));
			entity.VALOR_CS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_CS"));
			entity.VALOR_LIQ_SER = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_LIQ_SER"));
			entity.VALOR_TOT_NOTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_TOT_NOTA"));
			entity.CRED_ICM_SIMPLES = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CRED_ICM_SIMPLES"));
			entity.TRANSPORTADORA_1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TRANSPORTADORA_1"));
			entity.FRETE_CONTA_1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FRETE_CONTA_1"));
			entity.QUANTIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("QUANTIDADE"));
			entity.ESPECIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIE"));
			entity.MARCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCA"));
			entity.NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMERO"));
			entity.PESO_BRUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PESO_BRUTO"));
			entity.PESO_LIQUIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PESO_LIQUIDO"));
			entity.VENDEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("VENDEDOR"));
			entity.CUSTO_FINANCEIRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CUSTO_FINANCEIRO"));
			entity.NFE_STATUS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_STATUS"));
			entity.NFE_CHAVE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_CHAVE"));
			entity.NFE_RECENT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_RECENT"));
			entity.NFE_PROENT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_PROENT"));
			entity.NFE_DATHORENT = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("NFE_DATHORENT"));
			entity.NFE_PROCAN = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_PROCAN"));
			entity.NFE_DATHORCAN = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("NFE_DATHORCAN"));
			entity.NFE_AMBIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFE_AMBIENTE"));
			entity.CANCELADA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CANCELADA"));


			return entity;
		}
	}
}
