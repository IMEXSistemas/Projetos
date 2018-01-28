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
	public partial class LIS_PRODUTOSProvider 
	{
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		private FbConnection dbCnn = null;
		private FbCommand dbCommand = null;
		private FbTransaction dbTransaction = null;

		~LIS_PRODUTOSProvider()
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
		
		

		public  LIS_PRODUTOSCollection ReadCollection()
		{
			FbDataReader dataReader = null;

			try
			{
				LIS_PRODUTOSCollection collection = null;

				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_PRODUTOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_PRODUTOS", dbCnn);
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

		public  LIS_PRODUTOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						LIS_PRODUTOSCollection collection = null;

						string strSqlCommand = "SELECT * FROM LIS_PRODUTOS WHERE (";

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


public  LIS_PRODUTOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro,  string FieldOrder)
		{
			FbDataReader dataReader = null;
			string strSqlCommand = String.Empty;
			LIS_PRODUTOSCollection collection = null;
			
			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						

						strSqlCommand = "SELECT * FROM LIS_PRODUTOS WHERE (";

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
						strSqlCommand = "SELECT * FROM LIS_PRODUTOS  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM LIS_PRODUTOS order by  " + FieldOrder;
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


		private static LIS_PRODUTOSCollection ExecuteReader(ref LIS_PRODUTOSCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new LIS_PRODUTOSCollection();

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

		private static LIS_PRODUTOSEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			LIS_PRODUTOSEntity entity = new LIS_PRODUTOSEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

						entity.IDPRODUTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPRODUTO"));
			entity.NOMEPRODUTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEPRODUTO"));
			entity.CODPRODUTOFORNECEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODPRODUTOFORNECEDOR"));
			entity.CODBARRA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODBARRA"));
			entity.LOCALIZACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("LOCALIZACAO"));
			entity.DATACADASTRO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATACADASTRO"));
			entity.IDUNIDADE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDUNIDADE"));
			entity.NOMEUNIDADE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEUNIDADE"));
			entity.IDMARCA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDMARCA"));
			entity.NOMEMARCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEMARCA"));
			entity.IDMOEDA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDMOEDA"));
			entity.VALORCUSTOINICIAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCUSTOINICIAL"));
			entity.FRETEPRODUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("FRETEPRODUTO"));
			entity.ENCARGOSPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ENCARGOSPRODUTOS"));
			entity.VALORCUSTOFINAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCUSTOFINAL"));
			entity.MARGEMLUCRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("MARGEMLUCRO"));
			entity.VALORVENDA1 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA1"));
			entity.VALORVENDA2 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA2"));
			entity.VALORVENDA3 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORVENDA3"));
			entity.COMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("COMISSAO"));
			entity.IPI = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("IPI"));
			entity.ICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ICMS"));
			entity.QUANTIDADEMINIMA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANTIDADEMINIMA"));
			entity.IDGRUPOCATEGORIA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDGRUPOCATEGORIA"));
			entity.NOMEGRUPOCATEGORIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEGRUPOCATEGORIA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.NOMESTATUS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMESTATUS"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.PORCFRETE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCFRETE"));
			entity.PORCENCARGOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCENCARGOS"));
			entity.PORCMARGEMLUCRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCMARGEMLUCRO"));
			entity.PORCVENDA2 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCVENDA2"));
			entity.PORCVENDA3 = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCVENDA3"));
			entity.PESO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESO"));
			entity.CODCLASSFISCAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODCLASSFISCAL"));
			entity.CODSITTRIBU = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODSITTRIBU"));
			entity.NCMSH = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NCMSH"));
			entity.EXTIPI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("EXTIPI"));
			entity.ALIQPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQPIS"));
			entity.ALIQCOFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQCOFINS"));
			entity.CSTPISCONFIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTPISCONFIS"));
			entity.IDLOTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDLOTE"));
			entity.DESCLOTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCLOTE"));
			entity.DATAVALIDADE = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAVALIDADE"));
			entity.CODLOTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODLOTE"));
			entity.ESTOQUEMANUAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ESTOQUEMANUAL"));
			entity.SITUACAOTRIBUTARIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAOTRIBUTARIA"));
			entity.CSTPIS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTPIS"));
			entity.CSTIPI = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CSTIPI"));
			entity.NOMEPRODUTOCOD = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEPRODUTOCOD"));
            entity.IDCSTECF = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCSTECF"));
            entity.TIPOITEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TIPOITEM"));
            entity.PORCPERDAPROD = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCPERDAPROD"));
            entity.FLAGINATIVO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGINATIVO"));
			return entity;
		}
	}
}
	