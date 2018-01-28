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
	public partial class CLIENTEGDProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.CupomFiscal.Default.bdgoor;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CLIENTEGDProvider()
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
		
		
		public  string Save(CLIENTEGDEntity Entity )
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CLIENTE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CLIENTE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				

	dbCommand.Parameters.AddWithValue("@CODIGO",Entity.CODIGO); //PrimaryKey 



if(Entity.NOME!= null)
	dbCommand.Parameters.AddWithValue("@NOME", Entity.NOME); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@NOME", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@FANTASIA", Entity.FANTASIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTATO", Entity.CONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@CNPJ_CNPF", Entity.CNPJ_CNPF); //Coluna 
dbCommand.Parameters.AddWithValue("@IE_RG", Entity.IE_RG); //Coluna 
dbCommand.Parameters.AddWithValue("@IM", Entity.IM); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO", Entity.ENDERECO); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMERO", Entity.NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO", Entity.COMPLEMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@BAIRRO", Entity.BAIRRO); //Coluna 
dbCommand.Parameters.AddWithValue("@CIDADE", Entity.CIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@UF", Entity.UF); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP", Entity.CEP); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_ENDERECO", Entity.COB_ENDERECO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_NUMERO", Entity.COB_NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_COMPLEMENTO", Entity.COB_COMPLEMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_BAIRRO", Entity.COB_BAIRRO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_CIDADE", Entity.COB_CIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_UF", Entity.COB_UF); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_CEP", Entity.COB_CEP); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE", Entity.TELEFONE); //Coluna 
dbCommand.Parameters.AddWithValue("@CELULAR", Entity.CELULAR); //Coluna 
dbCommand.Parameters.AddWithValue("@FAX", Entity.FAX); //Coluna 
dbCommand.Parameters.AddWithValue("@EMAIL", Entity.EMAIL); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDA", Entity.RENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@LIMITE_CREDITO", Entity.LIMITE_CREDITO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIA_DE_ACERTO", Entity.DIA_DE_ACERTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_A_RECEBER", Entity.VALOR_A_RECEBER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_EM_ATRASO", Entity.VALOR_EM_ATRASO); //Coluna 

if(Entity.CADASTRO!= null)
	dbCommand.Parameters.AddWithValue("@CADASTRO", Entity.CADASTRO); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@CADASTRO", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@ULTIMA_VENDA", Entity.ULTIMA_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@REG_SIMPLES", Entity.REG_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", Entity.OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@PARCELAS_ATRA", Entity.PARCELAS_ATRA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONVENIO", Entity.CONVENIO); //Coluna 
dbCommand.Parameters.AddWithValue("@NASCIMENTO", Entity.NASCIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PAI", Entity.PAI); //Coluna 
dbCommand.Parameters.AddWithValue("@MAE", Entity.MAE); //Coluna 
dbCommand.Parameters.AddWithValue("@NATURALIDADE", Entity.NATURALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCTRA", Entity.LOCTRA); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCAL", Entity.LOCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@PAIS_BACEN", Entity.PAIS_BACEN); //Coluna 
dbCommand.Parameters.AddWithValue("@PAIS_NOME", Entity.PAIS_NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@SITUACAO", Entity.SITUACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PROFISSAO", Entity.PROFISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL1", Entity.PERSONAL1); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL2", Entity.PERSONAL2); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL3", Entity.PERSONAL3); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL4", Entity.PERSONAL4); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL5", Entity.PERSONAL5); //Coluna 
dbCommand.Parameters.AddWithValue("@FOTO", Entity.FOTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONJUGE", Entity.CONJUGE); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = dbCommand.Parameters["@CODIGO"].Value.ToString();
				
	
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
		
		
		public string Save(string CODIGO, string NOME, string FANTASIA, string CONTATO, string CNPJ_CNPF, string IE_RG, string IM, string ENDERECO, string NUMERO, string COMPLEMENTO, string BAIRRO, string CIDADE, string UF, string CEP, string COB_ENDERECO, string COB_NUMERO, string COB_COMPLEMENTO, string COB_BAIRRO, string COB_CIDADE, string COB_UF, string COB_CEP, string TELEFONE, string CELULAR, string FAX, string EMAIL, decimal RENDA, decimal LIMITE_CREDITO, int DIA_DE_ACERTO, decimal VALOR_A_RECEBER, decimal VALOR_EM_ATRASO, DateTime? CADASTRO, DateTime ULTIMA_VENDA, string REG_SIMPLES, string OBSERVACOES, string PARCELAS_ATRA, string CONVENIO, DateTime NASCIMENTO, string PAI, string MAE, string NATURALIDADE, string LOCTRA, string LOCAL, string PAIS_BACEN, string PAIS_NOME, string SITUACAO, string PROFISSAO, string PERSONAL1, string PERSONAL2, string PERSONAL3, string PERSONAL4, string PERSONAL5, byte[] FOTO, string CONJUGE)
		{
            string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CLIENTE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_CLIENTE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;
				

	dbCommand.Parameters.AddWithValue("@CODIGO", CODIGO); //PrimaryKey 



if(NOME!= null)
	dbCommand.Parameters.AddWithValue("@NOME", NOME); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@NOME", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@FANTASIA", FANTASIA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTATO", CONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@CNPJ_CNPF", CNPJ_CNPF); //Coluna 
dbCommand.Parameters.AddWithValue("@IE_RG", IE_RG); //Coluna 
dbCommand.Parameters.AddWithValue("@IM", IM); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO", ENDERECO); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMERO", NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO", COMPLEMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@BAIRRO", BAIRRO); //Coluna 
dbCommand.Parameters.AddWithValue("@CIDADE", CIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@UF", UF); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP", CEP); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_ENDERECO", COB_ENDERECO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_NUMERO", COB_NUMERO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_COMPLEMENTO", COB_COMPLEMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_BAIRRO", COB_BAIRRO); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_CIDADE", COB_CIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_UF", COB_UF); //Coluna 
dbCommand.Parameters.AddWithValue("@COB_CEP", COB_CEP); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE", TELEFONE); //Coluna 
dbCommand.Parameters.AddWithValue("@CELULAR", CELULAR); //Coluna 
dbCommand.Parameters.AddWithValue("@FAX", FAX); //Coluna 
dbCommand.Parameters.AddWithValue("@EMAIL", EMAIL); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDA", RENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@LIMITE_CREDITO", LIMITE_CREDITO); //Coluna 
dbCommand.Parameters.AddWithValue("@DIA_DE_ACERTO", DIA_DE_ACERTO); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_A_RECEBER", VALOR_A_RECEBER); //Coluna 
dbCommand.Parameters.AddWithValue("@VALOR_EM_ATRASO", VALOR_EM_ATRASO); //Coluna 

if(CADASTRO!= null)
	dbCommand.Parameters.AddWithValue("@CADASTRO", CADASTRO); //Coluna 
else
	dbCommand.Parameters.AddWithValue("@CADASTRO", DBNull.Value); //Coluna 3

dbCommand.Parameters.AddWithValue("@ULTIMA_VENDA", ULTIMA_VENDA); //Coluna 
dbCommand.Parameters.AddWithValue("@REG_SIMPLES", REG_SIMPLES); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACOES", OBSERVACOES); //Coluna 
dbCommand.Parameters.AddWithValue("@PARCELAS_ATRA", PARCELAS_ATRA); //Coluna 
dbCommand.Parameters.AddWithValue("@CONVENIO", CONVENIO); //Coluna 
dbCommand.Parameters.AddWithValue("@NASCIMENTO", NASCIMENTO); //Coluna 
dbCommand.Parameters.AddWithValue("@PAI", PAI); //Coluna 
dbCommand.Parameters.AddWithValue("@MAE", MAE); //Coluna 
dbCommand.Parameters.AddWithValue("@NATURALIDADE", NATURALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCTRA", LOCTRA); //Coluna 
dbCommand.Parameters.AddWithValue("@LOCAL", LOCAL); //Coluna 
dbCommand.Parameters.AddWithValue("@PAIS_BACEN", PAIS_BACEN); //Coluna 
dbCommand.Parameters.AddWithValue("@PAIS_NOME", PAIS_NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@SITUACAO", SITUACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PROFISSAO", PROFISSAO); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL1", PERSONAL1); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL2", PERSONAL2); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL3", PERSONAL3); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL4", PERSONAL4); //Coluna 
dbCommand.Parameters.AddWithValue("@PERSONAL5", PERSONAL5); //Coluna 
dbCommand.Parameters.AddWithValue("@FOTO", FOTO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONJUGE", CONJUGE); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@CODIGO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result =dbCommand.Parameters["@CODIGO"].Value.ToString();			
				

	
				
	
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
		
		
		public  string Delete(string CODIGO)
		{
			string result = "0";

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CLIENTE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_CLIENTE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO",CODIGO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = CODIGO;

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

		public  CLIENTEGDEntity Read(string CODIGO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CLIENTE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_CLIENTE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@CODIGO",CODIGO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CLIENTEGDEntity entity = null;
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

		
		public  CLIENTEGDCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CLIENTEGDCollection collection = null;
			
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
						strSqlCommand += ");";

						
					}
					else
					{
						strSqlCommand = "SELECT * FROM CLIENTE  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM CLIENTE  ";
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
		
		public  CLIENTEGDCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CLIENTEGDCollection collection = null;
			
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

		private static CLIENTEGDCollection ExecuteReader(ref CLIENTEGDCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CLIENTEGDCollection();

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

		private static CLIENTEGDEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CLIENTEGDEntity entity = new CLIENTEGDEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.FANTASIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FANTASIA"));
			entity.CONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTATO"));
			entity.CNPJ_CNPF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJ_CNPF"));
			entity.IE_RG = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IE_RG"));
			entity.IM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IM"));
			entity.ENDERECO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECO"));
			entity.NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMERO"));
			entity.COMPLEMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COMPLEMENTO"));
			entity.BAIRRO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BAIRRO"));
			entity.CIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CIDADE"));
			entity.UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF"));
			entity.CEP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEP"));
			entity.COB_ENDERECO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_ENDERECO"));
			entity.COB_NUMERO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_NUMERO"));
			entity.COB_COMPLEMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_COMPLEMENTO"));
			entity.COB_BAIRRO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_BAIRRO"));
			entity.COB_CIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_CIDADE"));
			entity.COB_UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_UF"));
			entity.COB_CEP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COB_CEP"));
			entity.TELEFONE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE"));
			entity.CELULAR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CELULAR"));
			entity.FAX = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FAX"));
			entity.EMAIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMAIL"));
			entity.RENDA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("RENDA"));
			entity.LIMITE_CREDITO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("LIMITE_CREDITO"));
			entity.DIA_DE_ACERTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("DIA_DE_ACERTO"));
			entity.VALOR_A_RECEBER = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_A_RECEBER"));
			entity.VALOR_EM_ATRASO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOR_EM_ATRASO"));
			entity.CADASTRO = getData.ConvertDBValueToDateTime(DataReader, DataReader.GetOrdinal("CADASTRO"));
			entity.ULTIMA_VENDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("ULTIMA_VENDA"));
			entity.REG_SIMPLES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REG_SIMPLES"));
			entity.OBSERVACOES = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACOES"));
			entity.PARCELAS_ATRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PARCELAS_ATRA"));
			entity.CONVENIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONVENIO"));
			entity.NASCIMENTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("NASCIMENTO"));
			entity.PAI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PAI"));
			entity.MAE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MAE"));
			entity.NATURALIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NATURALIDADE"));
			entity.LOCTRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOCTRA"));
			entity.LOCAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOCAL"));
			entity.PAIS_BACEN = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PAIS_BACEN"));
			entity.PAIS_NOME = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PAIS_NOME"));
			entity.SITUACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAO"));
			entity.PROFISSAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROFISSAO"));
			entity.PERSONAL1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL1"));
			entity.PERSONAL2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL2"));
			entity.PERSONAL3 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL3"));
			entity.PERSONAL4 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL4"));
			entity.PERSONAL5 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PERSONAL5"));
			entity.CONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONJUGE"));


			return entity;
		}
	}
}
