//Template gerado utilizando o MyGeneration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using BMSworks.Model;
using BmsSoftware;

namespace BMSworks.Firebird
{
	public partial class VERSAO2Provider
	{
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~VERSAO2Provider()
		{
			dbCnn = null;
			dbCommand = null;
			dbTransaction = null;
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

        public FbConnection GetConnectionDB(string connectionString)
        {
            FbConnection cnx = new FbConnection();
            cnx.ConnectionString = connectionString;
            return cnx;
        }
      

        public VERSAOEntity Read(int IDVERSAO, string connectionString)
        {
            FbDataReader reader = null;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Rea_VERSAO", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB(connectionString));

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Rea_VERSAO", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@IDVERSAO", IDVERSAO); //PrimaryKey


                reader = dbCommand.ExecuteReader();

                VERSAOEntity entity = null;
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
		
		private static VERSAOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			VERSAOEntity entity = new VERSAOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDVERSAO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDVERSAO"));
			entity.NUMEROVERSAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROVERSAO"));


			return entity;
		}
	}
}
