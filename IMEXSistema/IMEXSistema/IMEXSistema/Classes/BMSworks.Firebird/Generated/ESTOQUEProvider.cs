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
	public partial class ESTOQUEProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~ESTOQUEProvider()
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
		
		
		public  int Save(ESTOQUEEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@CODIGO",Entity.CODIGO); //PrimaryKey
dbCommand.Parameters.AddWithValue("@BARRAS", Entity.BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", Entity.DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", Entity.UND); //Coluna 
dbCommand.Parameters.AddWithValue("@UND_COMPRA", Entity.UND_COMPRA); //Coluna 
dbCommand.Parameters.AddWithValue("@FAT_CONV", Entity.FAT_CONV); //Coluna 
dbCommand.Parameters.AddWithValue("@FAMILIA", Entity.FAMILIA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRUPO", Entity.GRUPO); //Coluna 
dbCommand.Parameters.AddWithValue("@CARACTERISTICAS", Entity.CARACTERISTICAS); //Coluna 
dbCommand.Parameters.AddWithValue("@FORNECEDOR", Entity.FORNECEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@TAMANHO", Entity.TAMANHO); //Coluna 
dbCommand.Parameters.AddWithValue("@COR", Entity.COR); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO", Entity.PESO); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", Entity.QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", Entity.GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS", Entity.GRADE_DIS); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_PEDCOM", Entity.QTD_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QTD_PEDCOM", Entity.GRADE_QTD_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_PEDCOM", Entity.GRADE_DIS_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_PEDVEN", Entity.QTD_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QTD_PEDVEN", Entity.GRADE_QTD_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_PEDVEN", Entity.GRADE_DIS_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_IDEAL", Entity.QTD_IDEAL); //Coluna 
//dbCommand.Parameters.AddWithValue("@QTD_SALDO", Entity.QTD_SALDO); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_INSPRO", Entity.QTD_INSPRO); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA_IDEAL", Entity.GRADE_QUA_IDEAL); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_IDEAL", Entity.GRADE_DIS_IDEAL); //Coluna 
dbCommand.Parameters.AddWithValue("@CUSTO_MEDIO", Entity.CUSTO_MEDIO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_CUSTO", Entity.PRECO_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@MARGEM_LUCRO", Entity.MARGEM_LUCRO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_VENDA", Entity.PRECO_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_ATACADO", Entity.PRECO_ATACADO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_DOLAR", Entity.PRECO_DOLAR); //Coluna 
dbCommand.Parameters.AddWithValue("@COMISSAO", Entity.COMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@OST", Entity.OST); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", Entity.ST); //Coluna 
dbCommand.Parameters.AddWithValue("@ELO", Entity.ELO); //Coluna 
dbCommand.Parameters.AddWithValue("@CF", Entity.CF); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI", Entity.ALIQ_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI_VENDA", Entity.ALIQ_IPI_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IPI_CODIGO", Entity.IPI_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_CODIGO", Entity.PIS_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE_NOR", Entity.PIS_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ_NOR", Entity.PIS_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE_SUB", Entity.PIS_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ_SUB", Entity.PIS_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_CODIGO", Entity.COFINS_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE_NOR", Entity.COFINS_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ_NOR", Entity.COFINS_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE_SUB", Entity.COFINS_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ_SUB", Entity.COFINS_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_CODIGO", Entity.PISE_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_BASE_NOR", Entity.PISE_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_ALIQ_NOR", Entity.PISE_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_BASE_SUB", Entity.PISE_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_ALIQ_SUB", Entity.PISE_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_CODIGO", Entity.COFINSE_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_BASE_NOR", Entity.COFINSE_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_ALIQ_NOR", Entity.COFINSE_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_BASE_SUB", Entity.COFINSE_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_ALIQ_SUB", Entity.COFINSE_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@ALTERACAO_PRECO", Entity.ALTERACAO_PRECO); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMA_COMPRA", Entity.ULTIMA_COMPRA); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMA_VENDA", Entity.ULTIMA_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_CADASTRO", Entity.DATA_CADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_FABRICANTE", Entity.COD_FABRICANTE); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_NCM", Entity.COD_NCM); //Coluna 
dbCommand.Parameters.AddWithValue("@VIDA_UTIL", Entity.VIDA_UTIL); //Coluna 
dbCommand.Parameters.AddWithValue("@VALIDADE_DIAS", Entity.VALIDADE_DIAS); //Coluna 
dbCommand.Parameters.AddWithValue("@FOTO", Entity.FOTO); //Coluna 
dbCommand.Parameters.AddWithValue("@SITUACAO", Entity.SITUACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@CON_FED_PICO", Entity.CON_FED_PICO); //Coluna 
dbCommand.Parameters.AddWithValue("@SERIE", Entity.SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@IPPT", Entity.IPPT); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO_ITEM", Entity.TIPO_ITEM); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL1", Entity.PERSONAL1); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL2", Entity.PERSONAL2); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL3", Entity.PERSONAL3); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL4", Entity.PERSONAL4); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL5", Entity.PERSONAL5); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", Entity.OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@CHAVE", Entity.CHAVE); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@CODIGO"].Value.ToString());
				
	
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
		
		
		public  int Save(string CODIGO, string BARRAS, string DESCRICAO, string UND, string UND_COMPRA, decimal FAT_CONV, string FAMILIA, string GRUPO, string CARACTERISTICAS, string FORNECEDOR, string TAMANHO, string COR, decimal PESO, decimal QTD, string GRADE_QUA, string GRADE_DIS, decimal QTD_PEDCOM, string GRADE_QTD_PEDCOM, string GRADE_DIS_PEDCOM, decimal QTD_PEDVEN, string GRADE_QTD_PEDVEN, string GRADE_DIS_PEDVEN, decimal QTD_IDEAL, decimal QTD_SALDO, decimal QTD_INSPRO, string GRADE_QUA_IDEAL, string GRADE_DIS_IDEAL, decimal CUSTO_MEDIO, decimal PRECO_CUSTO, decimal MARGEM_LUCRO, decimal PRECO_VENDA, decimal PRECO_ATACADO, decimal PRECO_DOLAR, decimal COMISSAO, string OST, string ST, string ELO, string CF, decimal ALIQ_IPI, decimal ALIQ_IPI_VENDA, string IPI_CODIGO, string PIS_CODIGO, decimal PIS_BASE_NOR, decimal PIS_ALIQ_NOR, decimal PIS_BASE_SUB, decimal PIS_ALIQ_SUB, string COFINS_CODIGO, decimal COFINS_BASE_NOR, decimal COFINS_ALIQ_NOR, decimal COFINS_BASE_SUB, decimal COFINS_ALIQ_SUB, string PISE_CODIGO, decimal PISE_BASE_NOR, decimal PISE_ALIQ_NOR, decimal PISE_BASE_SUB, decimal PISE_ALIQ_SUB, string COFINSE_CODIGO, decimal COFINSE_BASE_NOR, decimal COFINSE_ALIQ_NOR, decimal COFINSE_BASE_SUB, decimal COFINSE_ALIQ_SUB, DateTime ALTERACAO_PRECO, DateTime ULTIMA_COMPRA, DateTime ULTIMA_VENDA, DateTime DATA_CADASTRO, string COD_FABRICANTE, string COD_NCM, DateTime VIDA_UTIL, string VALIDADE_DIAS, byte[] FOTO, string SITUACAO, string CON_FED_PICO, short SERIE, string IPPT, string TIPO_ITEM, string PERSONAL1, string PERSONAL2, string PERSONAL3, string PERSONAL4, string PERSONAL5, string OBSERVACOES, string CHAVE)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_ESTOQUE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@CODIGO", CODIGO); //PrimaryKey 
dbCommand.Parameters.AddWithValue("@BARRAS", BARRAS); //Coluna 
dbCommand.Parameters.AddWithValue("@DESCRICAO", DESCRICAO); //Coluna 
dbCommand.Parameters.AddWithValue("@UND", UND); //Coluna 
dbCommand.Parameters.AddWithValue("@UND_COMPRA", UND_COMPRA); //Coluna 
dbCommand.Parameters.AddWithValue("@FAT_CONV", FAT_CONV); //Coluna 
dbCommand.Parameters.AddWithValue("@FAMILIA", FAMILIA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRUPO", GRUPO); //Coluna 
dbCommand.Parameters.AddWithValue("@CARACTERISTICAS", CARACTERISTICAS); //Coluna 
dbCommand.Parameters.AddWithValue("@FORNECEDOR", FORNECEDOR); //Coluna 
dbCommand.Parameters.AddWithValue("@TAMANHO", TAMANHO); //Coluna 
dbCommand.Parameters.AddWithValue("@COR", COR); //Coluna 
dbCommand.Parameters.AddWithValue("@PESO", PESO); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD", QTD); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA", GRADE_QUA); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS", GRADE_DIS); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_PEDCOM", QTD_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QTD_PEDCOM", GRADE_QTD_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_PEDCOM", GRADE_DIS_PEDCOM); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_PEDVEN", QTD_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QTD_PEDVEN", GRADE_QTD_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_PEDVEN", GRADE_DIS_PEDVEN); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_IDEAL", QTD_IDEAL); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_SALDO", QTD_SALDO); //Coluna 
dbCommand.Parameters.AddWithValue("@QTD_INSPRO", QTD_INSPRO); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_QUA_IDEAL", GRADE_QUA_IDEAL); //Coluna 
dbCommand.Parameters.AddWithValue("@GRADE_DIS_IDEAL", GRADE_DIS_IDEAL); //Coluna 
dbCommand.Parameters.AddWithValue("@CUSTO_MEDIO", CUSTO_MEDIO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_CUSTO", PRECO_CUSTO); //Coluna 
dbCommand.Parameters.AddWithValue("@MARGEM_LUCRO", MARGEM_LUCRO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_VENDA", PRECO_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_ATACADO", PRECO_ATACADO); //Coluna 
dbCommand.Parameters.AddWithValue("@PRECO_DOLAR", PRECO_DOLAR); //Coluna 
dbCommand.Parameters.AddWithValue("@COMISSAO", COMISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@OST", OST); //Coluna 
dbCommand.Parameters.AddWithValue("@ST", ST); //Coluna 
dbCommand.Parameters.AddWithValue("@ELO", ELO); //Coluna 
dbCommand.Parameters.AddWithValue("@CF", CF); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI", ALIQ_IPI); //Coluna 
dbCommand.Parameters.AddWithValue("@ALIQ_IPI_VENDA", ALIQ_IPI_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@IPI_CODIGO", IPI_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_CODIGO", PIS_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE_NOR", PIS_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ_NOR", PIS_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_BASE_SUB", PIS_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PIS_ALIQ_SUB", PIS_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_CODIGO", COFINS_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE_NOR", COFINS_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ_NOR", COFINS_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_BASE_SUB", COFINS_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINS_ALIQ_SUB", COFINS_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_CODIGO", PISE_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_BASE_NOR", PISE_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_ALIQ_NOR", PISE_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_BASE_SUB", PISE_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@PISE_ALIQ_SUB", PISE_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_CODIGO", COFINSE_CODIGO); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_BASE_NOR", COFINSE_BASE_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_ALIQ_NOR", COFINSE_ALIQ_NOR); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_BASE_SUB", COFINSE_BASE_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@COFINSE_ALIQ_SUB", COFINSE_ALIQ_SUB); //Coluna 
dbCommand.Parameters.AddWithValue("@ALTERACAO_PRECO", ALTERACAO_PRECO); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMA_COMPRA", ULTIMA_COMPRA); //Coluna 
dbCommand.Parameters.AddWithValue("@ULTIMA_VENDA", ULTIMA_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@DATA_CADASTRO", DATA_CADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_FABRICANTE", COD_FABRICANTE); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_NCM", COD_NCM); //Coluna 
dbCommand.Parameters.AddWithValue("@VIDA_UTIL", VIDA_UTIL); //Coluna 
dbCommand.Parameters.AddWithValue("@VALIDADE_DIAS", VALIDADE_DIAS); //Coluna 
dbCommand.Parameters.AddWithValue("@FOTO", FOTO); //Coluna 
dbCommand.Parameters.AddWithValue("@SITUACAO", SITUACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@CON_FED_PICO", CON_FED_PICO); //Coluna 
dbCommand.Parameters.AddWithValue("@SERIE", SERIE); //Coluna 
dbCommand.Parameters.AddWithValue("@IPPT", IPPT); //Coluna 
dbCommand.Parameters.AddWithValue("@TIPO_ITEM", TIPO_ITEM); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL1", PERSONAL1); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL2", PERSONAL2); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL3", PERSONAL3); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL4", PERSONAL4); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL5", PERSONAL5); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@CHAVE", CHAVE); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@CODIGO"].Value.ToString());		
				
	
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
		
		
		public  int Delete(string CODIGO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ESTOQUE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_ESTOQUE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO",CODIGO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = Convert.ToInt32(CODIGO);

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

		public  ESTOQUEEntity Read(string CODIGO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ESTOQUE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_ESTOQUE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO",CODIGO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				ESTOQUEEntity entity = null;
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

		
		public  ESTOQUECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			ESTOQUECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ESTOQUE WHERE (";

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
						strSqlCommand = "SELECT * FROM ESTOQUE  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ESTOQUE  ";
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
		
		public  ESTOQUECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			ESTOQUECollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM ESTOQUE WHERE (";

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
						strSqlCommand = "SELECT * FROM ESTOQUE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM ESTOQUE  order by " + FieldOrder;
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

		private static ESTOQUECollection ExecuteReader(ref ESTOQUECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new ESTOQUECollection();

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

		private static ESTOQUEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			ESTOQUEEntity entity = new ESTOQUEEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.BARRAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BARRAS"));
			entity.DESCRICAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO"));
			entity.UND = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UND"));
			entity.UND_COMPRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UND_COMPRA"));
			entity.FAT_CONV = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("FAT_CONV"));
			entity.FAMILIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FAMILIA"));
			entity.GRUPO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRUPO"));
			entity.CARACTERISTICAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARACTERISTICAS"));
			entity.FORNECEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FORNECEDOR"));
			entity.TAMANHO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TAMANHO"));
			entity.COR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COR"));
			entity.PESO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESO"));
			entity.QTD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD"));
			entity.GRADE_QUA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QUA"));
			entity.GRADE_DIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_DIS"));
			entity.QTD_PEDCOM = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD_PEDCOM"));
			entity.GRADE_QTD_PEDCOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QTD_PEDCOM"));
			entity.GRADE_DIS_PEDCOM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_DIS_PEDCOM"));
			entity.QTD_PEDVEN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD_PEDVEN"));
			entity.GRADE_QTD_PEDVEN = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QTD_PEDVEN"));
			entity.GRADE_DIS_PEDVEN = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_DIS_PEDVEN"));
			entity.QTD_IDEAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD_IDEAL"));
			entity.QTD_SALDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD_SALDO"));
			entity.QTD_INSPRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QTD_INSPRO"));
			entity.GRADE_QUA_IDEAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_QUA_IDEAL"));
			entity.GRADE_DIS_IDEAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("GRADE_DIS_IDEAL"));
			entity.CUSTO_MEDIO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CUSTO_MEDIO"));
			entity.PRECO_CUSTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO_CUSTO"));
			entity.MARGEM_LUCRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("MARGEM_LUCRO"));
			entity.PRECO_VENDA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO_VENDA"));
			entity.PRECO_ATACADO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO_ATACADO"));
			entity.PRECO_DOLAR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PRECO_DOLAR"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));
			entity.OST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OST"));
			entity.ST = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ST"));
			entity.ELO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ELO"));
			entity.CF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CF"));
			entity.ALIQ_IPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQ_IPI"));
			entity.ALIQ_IPI_VENDA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQ_IPI_VENDA"));
			entity.IPI_CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IPI_CODIGO"));
			entity.PIS_CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PIS_CODIGO"));
			entity.PIS_BASE_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_BASE_NOR"));
			entity.PIS_ALIQ_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_ALIQ_NOR"));
			entity.PIS_BASE_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_BASE_SUB"));
			entity.PIS_ALIQ_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PIS_ALIQ_SUB"));
			entity.COFINS_CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COFINS_CODIGO"));
			entity.COFINS_BASE_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_BASE_NOR"));
			entity.COFINS_ALIQ_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_ALIQ_NOR"));
			entity.COFINS_BASE_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_BASE_SUB"));
			entity.COFINS_ALIQ_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINS_ALIQ_SUB"));
			entity.PISE_CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PISE_CODIGO"));
			entity.PISE_BASE_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PISE_BASE_NOR"));
			entity.PISE_ALIQ_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PISE_ALIQ_NOR"));
			entity.PISE_BASE_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PISE_BASE_SUB"));
			entity.PISE_ALIQ_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PISE_ALIQ_SUB"));
			entity.COFINSE_CODIGO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COFINSE_CODIGO"));
			entity.COFINSE_BASE_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINSE_BASE_NOR"));
			entity.COFINSE_ALIQ_NOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINSE_ALIQ_NOR"));
			entity.COFINSE_BASE_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINSE_BASE_SUB"));
			entity.COFINSE_ALIQ_SUB = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COFINSE_ALIQ_SUB"));
			entity.ALTERACAO_PRECO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("ALTERACAO_PRECO"));
			entity.ULTIMA_COMPRA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("ULTIMA_COMPRA"));
			entity.ULTIMA_VENDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("ULTIMA_VENDA"));
			entity.DATA_CADASTRO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA_CADASTRO"));
			entity.COD_FABRICANTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COD_FABRICANTE"));
			entity.COD_NCM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COD_NCM"));
			entity.VIDA_UTIL = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("VIDA_UTIL"));
			entity.VALIDADE_DIAS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("VALIDADE_DIAS"));
			entity.SITUACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAO"));
			entity.CON_FED_PICO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CON_FED_PICO"));
			//entity.SERIE = getData.ConvertDBValueToShortNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IPPT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IPPT"));
			entity.TIPO_ITEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPO_ITEM"));
			entity.PERSONAL1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL1"));
			entity.PERSONAL2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL2"));
			entity.PERSONAL3 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL3"));
			entity.PERSONAL4 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL4"));
			entity.PERSONAL5 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL5"));
			entity.OBSERVACOES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACOES"));
			entity.CHAVE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVE"));


			return entity;
		}
	}
}
