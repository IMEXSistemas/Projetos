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
	public partial class CLIENTEProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~CLIENTEProvider()
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
		
		
		public  int Save(CLIENTEEntity Entity )
		{	
			int result = 0;

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

				
if(Entity.IDCLIENTE!= -1)
	dbCommand.Parameters.AddWithValue("@IDCLIENTE",Entity.IDCLIENTE); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOME", Entity.NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@APELIDO", Entity.APELIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTATO", Entity.CONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATACADASTRO", Entity.DATACADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE1", Entity.TELEFONE1); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE2", Entity.TELEFONE2); //Coluna 
dbCommand.Parameters.AddWithValue("@FAX", Entity.FAX); //Coluna 
dbCommand.Parameters.AddWithValue("@RAMAL", Entity.RAMAL); //Coluna 
dbCommand.Parameters.AddWithValue("@CNPJ", Entity.CNPJ); //Coluna 
dbCommand.Parameters.AddWithValue("@CPF", Entity.CPF); //Coluna 
dbCommand.Parameters.AddWithValue("@IE", Entity.IE); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO1", Entity.ENDERECO1); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO1", Entity.COMPLEMENTO1); //Coluna 
dbCommand.Parameters.AddWithValue("@BAIRRO1", Entity.BAIRRO1); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP1", Entity.CEP1); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO2", Entity.ENDERECO2); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO2", Entity.COMPLEMENTO2); //Coluna 
dbCommand.Parameters.AddWithValue("@CIDADE2", Entity.CIDADE2); //Coluna 
dbCommand.Parameters.AddWithValue("@UF2", Entity.UF2); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP2", Entity.CEP2); //Coluna 
dbCommand.Parameters.AddWithValue("@REFERENCIA1", Entity.REFERENCIA1); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEREFERENCIA1", Entity.TELEFONEREFERENCIA1); //Coluna 
dbCommand.Parameters.AddWithValue("@EMAILCLIENTE", Entity.EMAILCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATANASCIMENTOCLIENTE", Entity.DATANASCIMENTOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGSERASA", Entity.FLAGSERASA); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGSPC", Entity.FLAGSPC); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGTELECHEQUE", Entity.FLAGTELECHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGBLOQUEADO", Entity.FLAGBLOQUEADO); //Coluna 
dbCommand.Parameters.AddWithValue("@REFERENCIA2", Entity.REFERENCIA2); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEREFERENCIA2", Entity.TELEFONEREFERENCIA2); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDACLIENTE", Entity.RENDACLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CREDITOCLIENTE", Entity.CREDITOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", Entity.IDCLASSIFICACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPOREGIAO", Entity.IDTIPOREGIAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDPROFISSAOATIVIDADE", Entity.IDPROFISSAOATIVIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTRANSPORTADORA", Entity.IDTRANSPORTADORA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", Entity.IDFUNCIONARIO); //Coluna 
dbCommand.Parameters.AddWithValue("@EMPREGOCLIENTE", Entity.EMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECOEMPREGOCLIENTE", Entity.ENDERECOEMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEEMPREGOCLIENTE", Entity.TELEFONEEMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CARGOCLIENTE", Entity.CARGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@ESTADOCIVIL", Entity.ESTADOCIVIL); //Coluna 
dbCommand.Parameters.AddWithValue("@NATURALIDADE", Entity.NATURALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@CONJUGE", Entity.CONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATANASCCONJUGE", Entity.DATANASCCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@CPFCONJUGE", Entity.CPFCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@RGCONJUGE", Entity.RGCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDACONJUGE", Entity.RENDACONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@EMPREGOCONJUGE", Entity.EMPREGOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAADMISSAOCONJUGE", Entity.DATAADMISSAOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@CARGOCONJUGE", Entity.CARGOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONCONJUGE", Entity.TELEFONCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@FILIACAO", Entity.FILIACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@NOMECONTATO", Entity.NOMECONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@ATENDIDOCONTATO", Entity.ATENDIDOCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATARETORNOCONTATO", Entity.DATARETORNOCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DETALHESCONTATO", Entity.DETALHESCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", Entity.COD_MUN_IBGE); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMEROENDER", Entity.NUMEROENDER); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCLIENTE"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDCLIENTE"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDCLIENTE, string NOME, string APELIDO, string CONTATO, DateTime DATACADASTRO, string TELEFONE1, string TELEFONE2, string FAX, string RAMAL, string CNPJ, string CPF, string IE, string ENDERECO1, string COMPLEMENTO1, string BAIRRO1, string CEP1, string ENDERECO2, string COMPLEMENTO2, string CIDADE2, string UF2, string CEP2, string REFERENCIA1, string TELEFONEREFERENCIA1, string EMAILCLIENTE, DateTime DATANASCIMENTOCLIENTE, string FLAGSERASA, string FLAGSPC, string FLAGTELECHEQUE, string FLAGBLOQUEADO, string REFERENCIA2, string TELEFONEREFERENCIA2, decimal RENDACLIENTE, decimal CREDITOCLIENTE, string OBSERVACAO, int IDCLASSIFICACAO, int IDTIPOREGIAO, int IDPROFISSAOATIVIDADE, int IDTRANSPORTADORA, int IDFUNCIONARIO, string EMPREGOCLIENTE, string ENDERECOEMPREGOCLIENTE, string TELEFONEEMPREGOCLIENTE, string CARGOCLIENTE, string ESTADOCIVIL, string NATURALIDADE, string CONJUGE, DateTime DATANASCCONJUGE, string CPFCONJUGE, string RGCONJUGE, decimal RENDACONJUGE, string EMPREGOCONJUGE, DateTime DATAADMISSAOCONJUGE, string CARGOCONJUGE, string TELEFONCONJUGE, string FILIACAO, string NOMECONTATO, string ATENDIDOCONTATO, DateTime DATARETORNOCONTATO, string DETALHESCONTATO, int COD_MUN_IBGE, string NUMEROENDER)
		{	
			int result = 0;

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

				
if(IDCLIENTE!= -1)
	dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //PrimaryKey 
else
	dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //PrimaryKey 

dbCommand.Parameters.AddWithValue("@NOME", NOME); //Coluna 
dbCommand.Parameters.AddWithValue("@APELIDO", APELIDO); //Coluna 
dbCommand.Parameters.AddWithValue("@CONTATO", CONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATACADASTRO", DATACADASTRO); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE1", TELEFONE1); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONE2", TELEFONE2); //Coluna 
dbCommand.Parameters.AddWithValue("@FAX", FAX); //Coluna 
dbCommand.Parameters.AddWithValue("@RAMAL", RAMAL); //Coluna 
dbCommand.Parameters.AddWithValue("@CNPJ", CNPJ); //Coluna 
dbCommand.Parameters.AddWithValue("@CPF", CPF); //Coluna 
dbCommand.Parameters.AddWithValue("@IE", IE); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO1", ENDERECO1); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO1", COMPLEMENTO1); //Coluna 
dbCommand.Parameters.AddWithValue("@BAIRRO1", BAIRRO1); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP1", CEP1); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECO2", ENDERECO2); //Coluna 
dbCommand.Parameters.AddWithValue("@COMPLEMENTO2", COMPLEMENTO2); //Coluna 
dbCommand.Parameters.AddWithValue("@CIDADE2", CIDADE2); //Coluna 
dbCommand.Parameters.AddWithValue("@UF2", UF2); //Coluna 
dbCommand.Parameters.AddWithValue("@CEP2", CEP2); //Coluna 
dbCommand.Parameters.AddWithValue("@REFERENCIA1", REFERENCIA1); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEREFERENCIA1", TELEFONEREFERENCIA1); //Coluna 
dbCommand.Parameters.AddWithValue("@EMAILCLIENTE", EMAILCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATANASCIMENTOCLIENTE", DATANASCIMENTOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGSERASA", FLAGSERASA); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGSPC", FLAGSPC); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGTELECHEQUE", FLAGTELECHEQUE); //Coluna 
dbCommand.Parameters.AddWithValue("@FLAGBLOQUEADO", FLAGBLOQUEADO); //Coluna 
dbCommand.Parameters.AddWithValue("@REFERENCIA2", REFERENCIA2); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEREFERENCIA2", TELEFONEREFERENCIA2); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDACLIENTE", RENDACLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CREDITOCLIENTE", CREDITOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDCLASSIFICACAO", IDCLASSIFICACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTIPOREGIAO", IDTIPOREGIAO); //Coluna 
dbCommand.Parameters.AddWithValue("@IDPROFISSAOATIVIDADE", IDPROFISSAOATIVIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@IDTRANSPORTADORA", IDTRANSPORTADORA); //Coluna 
dbCommand.Parameters.AddWithValue("@IDFUNCIONARIO", IDFUNCIONARIO); //Coluna 
dbCommand.Parameters.AddWithValue("@EMPREGOCLIENTE", EMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@ENDERECOEMPREGOCLIENTE", ENDERECOEMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONEEMPREGOCLIENTE", TELEFONEEMPREGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@CARGOCLIENTE", CARGOCLIENTE); //Coluna 
dbCommand.Parameters.AddWithValue("@ESTADOCIVIL", ESTADOCIVIL); //Coluna 
dbCommand.Parameters.AddWithValue("@NATURALIDADE", NATURALIDADE); //Coluna 
dbCommand.Parameters.AddWithValue("@CONJUGE", CONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATANASCCONJUGE", DATANASCCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@CPFCONJUGE", CPFCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@RGCONJUGE", RGCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@RENDACONJUGE", RENDACONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@EMPREGOCONJUGE", EMPREGOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@DATAADMISSAOCONJUGE", DATAADMISSAOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@CARGOCONJUGE", CARGOCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@TELEFONCONJUGE", TELEFONCONJUGE); //Coluna 
dbCommand.Parameters.AddWithValue("@FILIACAO", FILIACAO); //Coluna 
dbCommand.Parameters.AddWithValue("@NOMECONTATO", NOMECONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@ATENDIDOCONTATO", ATENDIDOCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DATARETORNOCONTATO", DATARETORNOCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@DETALHESCONTATO", DETALHESCONTATO); //Coluna 
dbCommand.Parameters.AddWithValue("@COD_MUN_IBGE", COD_MUN_IBGE); //Coluna 
dbCommand.Parameters.AddWithValue("@NUMEROENDER", NUMEROENDER); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDCLIENTE"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDCLIENTE"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDCLIENTE)
		{
			int result = 0;

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

				dbCommand.Parameters.AddWithValue("@IDCLIENTE",IDCLIENTE); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDCLIENTE;

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

		public  CLIENTEEntity Read(int IDCLIENTE)
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

				dbCommand.Parameters.AddWithValue("@IDCLIENTE",IDCLIENTE); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				CLIENTEEntity entity = null;
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

		
		public  CLIENTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			CLIENTECollection collection = null;
			
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
		
		public  CLIENTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			CLIENTECollection collection = null;
			
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

		private static CLIENTECollection ExecuteReader(ref CLIENTECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new CLIENTECollection();

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

		private static CLIENTEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			CLIENTEEntity entity = new CLIENTEEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDCLIENTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.NOME = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOME"));
			entity.APELIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("APELIDO"));
			entity.CONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTATO"));
			entity.DATACADASTRO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATACADASTRO"));
			entity.TELEFONE1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE1"));
			entity.TELEFONE2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONE2"));
			entity.FAX = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FAX"));
			entity.RAMAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RAMAL"));
			entity.CNPJ = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJ"));
			entity.CPF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPF"));
			entity.IE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("IE"));
			entity.ENDERECO1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECO1"));
			entity.COMPLEMENTO1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COMPLEMENTO1"));
			entity.BAIRRO1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BAIRRO1"));
			entity.CEP1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEP1"));
			entity.ENDERECO2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECO2"));
			entity.COMPLEMENTO2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COMPLEMENTO2"));
			entity.CIDADE2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CIDADE2"));
			entity.UF2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF2"));
			entity.CEP2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CEP2"));
			entity.REFERENCIA1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REFERENCIA1"));
			entity.TELEFONEREFERENCIA1 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONEREFERENCIA1"));
			entity.EMAILCLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMAILCLIENTE"));
			entity.DATANASCIMENTOCLIENTE = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATANASCIMENTOCLIENTE"));
			entity.FLAGSERASA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSERASA"));
			entity.FLAGSPC = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGSPC"));
			entity.FLAGTELECHEQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTELECHEQUE"));
			entity.FLAGBLOQUEADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGBLOQUEADO"));
			entity.REFERENCIA2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("REFERENCIA2"));
			entity.TELEFONEREFERENCIA2 = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONEREFERENCIA2"));
			entity.RENDACLIENTE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("RENDACLIENTE"));
			entity.CREDITOCLIENTE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("CREDITOCLIENTE"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.IDCLASSIFICACAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLASSIFICACAO"));
			entity.IDTIPOREGIAO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOREGIAO"));
			entity.IDPROFISSAOATIVIDADE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDPROFISSAOATIVIDADE"));
			entity.IDTRANSPORTADORA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTADORA"));
			entity.IDFUNCIONARIO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFUNCIONARIO"));
			entity.EMPREGOCLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMPREGOCLIENTE"));
			entity.ENDERECOEMPREGOCLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDERECOEMPREGOCLIENTE"));
			entity.TELEFONEEMPREGOCLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONEEMPREGOCLIENTE"));
			entity.CARGOCLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARGOCLIENTE"));
			entity.ESTADOCIVIL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESTADOCIVIL"));
			entity.NATURALIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NATURALIDADE"));
			entity.CONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONJUGE"));
			entity.DATANASCCONJUGE = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATANASCCONJUGE"));
			entity.CPFCONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPFCONJUGE"));
			entity.RGCONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RGCONJUGE"));
			entity.RENDACONJUGE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("RENDACONJUGE"));
			entity.EMPREGOCONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EMPREGOCONJUGE"));
			entity.DATAADMISSAOCONJUGE = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAADMISSAOCONJUGE"));
			entity.CARGOCONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CARGOCONJUGE"));
			entity.TELEFONCONJUGE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONCONJUGE"));
			entity.FILIACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FILIACAO"));
			entity.NOMECONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECONTATO"));
			entity.ATENDIDOCONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ATENDIDOCONTATO"));
			entity.DATARETORNOCONTATO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATARETORNOCONTATO"));
			entity.DETALHESCONTATO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DETALHESCONTATO"));
			entity.COD_MUN_IBGE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("COD_MUN_IBGE"));
			entity.NUMEROENDER = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROENDER"));


			return entity;
		}
	}
}
