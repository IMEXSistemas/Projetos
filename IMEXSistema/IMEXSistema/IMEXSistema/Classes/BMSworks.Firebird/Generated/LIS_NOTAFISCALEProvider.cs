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
	public partial class LIS_NOTAFISCALEProvider 
	{
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		private FbConnection dbCnn = null;
		private FbCommand dbCommand = null;
		private FbTransaction dbTransaction = null;

		~LIS_NOTAFISCALEProvider()
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
		
		

		public  LIS_NOTAFISCALECollection ReadCollection()
		{
			FbDataReader dataReader = null;

			try
			{
				LIS_NOTAFISCALECollection collection = null;

				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_NOTAFISCALE", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_NOTAFISCALE", dbCnn);
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

		public  LIS_NOTAFISCALECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						LIS_NOTAFISCALECollection collection = null;

						string strSqlCommand = "SELECT * FROM LIS_NOTAFISCALE WHERE (";

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


public  LIS_NOTAFISCALECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro,  string FieldOrder)
		{
			FbDataReader dataReader = null;
			string strSqlCommand = String.Empty;
			LIS_NOTAFISCALECollection collection = null;
			
			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						

						strSqlCommand = "SELECT * FROM LIS_NOTAFISCALE WHERE (";

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
						strSqlCommand = "SELECT * FROM LIS_NOTAFISCALE  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM LIS_NOTAFISCALE order by  " + FieldOrder;
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


		private static LIS_NOTAFISCALECollection ExecuteReader(ref LIS_NOTAFISCALECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new LIS_NOTAFISCALECollection();

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

		private static LIS_NOTAFISCALEEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			LIS_NOTAFISCALEEntity entity = new LIS_NOTAFISCALEEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

						entity.IDNOTAFISCALE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDNOTAFISCALE"));
			entity.NFISCALE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NFISCALE"));
			entity.SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.NOMECLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTSAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTSAIDA"));
			entity.HORASAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORASAIDA"));
			entity.IDTIPOMOVIM = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDTIPOMOVIM"));
			entity.NOMEMOVIMESTOQUE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEMOVIMESTOQUE"));
			entity.IDCFOP = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.DESCCFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCCFOP"));
			entity.CODCFOP = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODCFOP"));
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
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.NOMEVENDEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.NOMETRANSPORTADORA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMETRANSPORTADORA"));
			entity.PLACA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PLACA"));
			entity.UFTRANSPORTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UFTRANSPORTE"));
			entity.QUANT = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANT"));
			entity.ESPECIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ESPECIE"));
			entity.MARCANFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MARCANFE"));
			entity.NUMEROS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROS"));
			entity.PESOBRUTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOBRUTO"));
			entity.PESOLIQUIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PESOLIQUIDO"));
			entity.INFOCOMPLEM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("INFOCOMPLEM"));
			entity.IDCENTROCUSTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCENTROCUSTO"));
			entity.CENTROCUSTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CENTROCUSTO"));
			entity.DESCCENTROCUSTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCCENTROCUSTO"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.NOMEFORMAPAGTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEFORMAPAGTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.NOMELOCCOBRANCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMELOCCOBRANCA"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.NOMESTATUS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMESTATUS"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.FRETE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("FRETE"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));
			entity.VALORPIS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPIS"));
			entity.VALORCONFINS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCONFINS"));
			entity.CODANTT = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CODANTT"));
			entity.VALORTOTALSERVICO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTOTALSERVICO"));
			entity.BASECALCISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("BASECALCISSQN"));
			entity.ALIQISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQISSQN"));
			entity.VALORISSQN = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORISSQN"));
			entity.CHAVEACESSO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEACESSO"));
			entity.FLAGARQUIVOXML = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGARQUIVOXML"));
			entity.FLAGASSINATURA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGASSINATURA"));
			entity.FLAGCANCELADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCANCELADA"));
			entity.FLAGENVIADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGENVIADA"));
			entity.FLAGINUTILIZADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGINUTILIZADO"));
			entity.FLAGVALIDADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGVALIDADA"));
			entity.ARQUIVOLOTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ARQUIVOLOTE"));
			entity.RECIBONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("RECIBONFE"));
			entity.SITUACAONFE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SITUACAONFE"));
			entity.ALIQCREDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("ALIQCREDICMS"));
			entity.VALORCREDICMS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCREDICMS"));
			entity.FLAGTIPOPAGAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTIPOPAGAMENTO"));
			entity.CPF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPF"));
			entity.CNPJ = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJ"));
			entity.NUMPEDIDO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMPEDIDO"));
			entity.COD_MUN_IBGE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("COD_MUN_IBGE"));
			entity.CNPJEMISSOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJEMISSOR"));


			return entity;
		}
	}
}
	