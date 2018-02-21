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
using BMSworks.Firebird;


namespace BMSworks.Firebird
{
	public partial class LIS_CUPOMELETRONICOProvider 
	{
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		private FbConnection dbCnn = null;
		private FbCommand dbCommand = null;
		private FbTransaction dbTransaction = null;

		~LIS_CUPOMELETRONICOProvider()
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
		
		

		public  LIS_CUPOMELETRONICOCollection ReadCollection()
		{
			FbDataReader dataReader = null;

			try
			{
				LIS_CUPOMELETRONICOCollection collection = null;

				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_CUPOMELETRONICO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_CUPOMELETRONICO", dbCnn);
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

		public  LIS_CUPOMELETRONICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						LIS_CUPOMELETRONICOCollection collection = null;

						string strSqlCommand = "SELECT * FROM LIS_CUPOMELETRONICO WHERE (";

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


public  LIS_CUPOMELETRONICOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro,  string FieldOrder)
		{
			FbDataReader dataReader = null;
			string strSqlCommand = String.Empty;
			LIS_CUPOMELETRONICOCollection collection = null;
			
			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						

						strSqlCommand = "SELECT * FROM LIS_CUPOMELETRONICO WHERE (";

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
						strSqlCommand = "SELECT * FROM LIS_CUPOMELETRONICO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM LIS_CUPOMELETRONICO order by  " + FieldOrder;
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


		private static LIS_CUPOMELETRONICOCollection ExecuteReader(ref LIS_CUPOMELETRONICOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new LIS_CUPOMELETRONICOCollection();

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

		private static LIS_CUPOMELETRONICOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			LIS_CUPOMELETRONICOEntity entity = new LIS_CUPOMELETRONICOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

						entity.CUPOMELETRONICOID = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CUPOMELETRONICOID"));
			entity.NUMERONFCE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("NUMERONFCE"));
			entity.SERIE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("SERIE"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.NOMECLIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMECLIENTE"));
			entity.CPF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CPF"));
			entity.CNPJ = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CNPJ"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.DTSAIDA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTSAIDA"));
			entity.HORASAIDA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORASAIDA"));
			entity.IDCFOP = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDCFOP"));
			entity.TOTALNOTA = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALNOTA"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.NOMEVENDEDOR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEVENDEDOR"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.VALORTROCO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORTROCO"));
			entity.CHAVEACESSO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CHAVEACESSO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.IDSTATUSNFCE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDSTATUSNFCE"));
			entity.NOMESTATUS = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMESTATUS"));
			entity.IDTIPOPAGAMENTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDTIPOPAGAMENTO"));
			entity.NOMETIPOPAGAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMETIPOPAGAMENTO"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.NOMELOCALCOBRANCA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMELOCALCOBRANCA"));
			entity.IDFORMAPAGTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDFORMAPAGTO"));
			entity.NOMEFORMAPAGTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEFORMAPAGTO"));
			entity.VALORFINAL = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORFINAL"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.FLAGCONTINGENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGCONTINGENCIA"));
			entity.AMBIENTE = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("AMBIENTE"));
			entity.PROTOCOLO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROTOCOLO"));
			entity.PROTOCOLOCANCEL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PROTOCOLOCANCEL"));
			entity.CODOPERADORACARTAO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("CODOPERADORACARTAO"));
			entity.NOMEOPERADORACARTAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMEOPERADORACARTAO"));
			entity.NUMEROAUTORIZACARTAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NUMEROAUTORIZACARTAO"));
			entity.VALOPAGODINHEIRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOPAGODINHEIRO"));
			entity.VALOPAGOCARTAOCREDITO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOPAGOCARTAOCREDITO"));
			entity.VALOPAGOVALEREFEICAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOPAGOVALEREFEICAO"));
			entity.VALORPAGOCHEQUE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGOCHEQUE"));
			entity.VALOPAGOCARTAODEBITO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOPAGOCARTAODEBITO"));
			entity.VALOPAGOOUTROS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALOPAGOOUTROS"));


			return entity;
		}
	}
}
	