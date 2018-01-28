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
using BMSworks.Firebird;


namespace BMSworks.Firebird
{
	public partial class LIS_CLIENTEProvider 
	{
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		private FbConnection dbCnn = null;
		private FbCommand dbCommand = null;
		private FbTransaction dbTransaction = null;

		~LIS_CLIENTEProvider()
		{
			dbCnn = null;
			dbCommand = null;
			dbTransaction = null;
		}

		public  FbConnection GetConnectionDB()
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
		
		

		public  LIS_CLIENTECollection ReadCollection()
		{
			FbDataReader dataReader = null;

			try
			{
				LIS_CLIENTECollection collection = null;

				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_CLIENTE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_CLIENTE", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				// Tipo do comando de banco Procedure ou SQL
				dbCommand.CommandType = CommandType.Text;

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

		public  LIS_CLIENTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						LIS_CLIENTECollection collection = null;

						string strSqlCommand = "SELECT * FROM LIS_CLIENTE WHERE (";

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

						//Verificando a existência de um transação aberta
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
					else
					{
						return this.ReadCollection();
					}
				}
				else
				{
					return this.ReadCollection();
				}
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
					if(dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
		}


public  LIS_CLIENTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro,  string FieldOrder)
		{
			FbDataReader dataReader = null;
			string strSqlCommand = String.Empty;
			LIS_CLIENTECollection collection = null;
			
			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						

						strSqlCommand = "SELECT * FROM LIS_CLIENTE WHERE (";

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
						strSqlCommand += ") order by  " + FieldOrder;

						
					}
					else
					{
						strSqlCommand = "SELECT * FROM LIS_CLIENTE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM LIS_CLIENTE order by  " + FieldOrder;
				}
				
				//Verificando a existência de um transação aberta
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
					if(dbCommand.Transaction != null)
						dbCommand.Transaction.Rollback();
					if (dbCnn.State == ConnectionState.Open)
						dbCnn.Close();
				}

				throw ex;
			}
		}


		private static LIS_CLIENTECollection ExecuteReader(ref LIS_CLIENTECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new LIS_CLIENTECollection();

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

		private static LIS_CLIENTEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			LIS_CLIENTEEntity entity = new LIS_CLIENTEEntity();

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
			entity.IDCLASSIFICACAO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCLASSIFICACAO"));
			entity.NOMECLASSIFICACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECLASSIFICACAO"));
			entity.IDTIPOREGIAO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDTIPOREGIAO"));
			entity.NOMETIPOREGIAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMETIPOREGIAO"));
			entity.IDPROFISSAOATIVIDADE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPROFISSAOATIVIDADE"));
			entity.NOMEPROFISSAOATIVIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEPROFISSAOATIVIDADE"));
			entity.IDTRANSPORTADORA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDTRANSPORTADORA"));
			entity.NOMETRANSPORTADORA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMETRANSPORTADORA"));
			entity.IDFUNCIONARIO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDFUNCIONARIO"));
			entity.NOMEFUNCIONARIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEFUNCIONARIO"));
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
			entity.COD_MUN_IBGE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("COD_MUN_IBGE"));
			entity.MUNICIPIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MUNICIPIO"));
			entity.UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF"));
			entity.COD_UF_IBGE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("COD_UF_IBGE"));
			entity.NUMEROENDER = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROENDER"));


			return entity;
		}
	}
}
	