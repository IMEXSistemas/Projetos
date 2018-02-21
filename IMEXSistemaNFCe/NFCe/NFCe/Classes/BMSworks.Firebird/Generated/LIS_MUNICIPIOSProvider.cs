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
	public partial class LIS_MUNICIPIOSProvider 
	{
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		private FbConnection dbCnn = null;
		private FbCommand dbCommand = null;
		private FbTransaction dbTransaction = null;

		~LIS_MUNICIPIOSProvider()
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
		
		

		public  LIS_MUNICIPIOSCollection ReadCollection()
		{
			FbDataReader dataReader = null;

			try
			{
				LIS_MUNICIPIOSCollection collection = null;

				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_MUNICIPIOS", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("SELECT * FROM LIS_MUNICIPIOS", dbCnn);
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

		public  LIS_MUNICIPIOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						LIS_MUNICIPIOSCollection collection = null;

						string strSqlCommand = "SELECT * FROM LIS_MUNICIPIOS WHERE (";

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


public  LIS_MUNICIPIOSCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro,  string FieldOrder)
		{
			FbDataReader dataReader = null;
			string strSqlCommand = String.Empty;
			LIS_MUNICIPIOSCollection collection = null;
			
			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						

						strSqlCommand = "SELECT * FROM LIS_MUNICIPIOS WHERE (";

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
						strSqlCommand = "SELECT * FROM LIS_MUNICIPIOS  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM LIS_MUNICIPIOS order by  " + FieldOrder;
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


		private static LIS_MUNICIPIOSCollection ExecuteReader(ref LIS_MUNICIPIOSCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new LIS_MUNICIPIOSCollection();

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

		private static LIS_MUNICIPIOSEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			LIS_MUNICIPIOSEntity entity = new LIS_MUNICIPIOSEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

			entity.COD_MUN_IBGE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("COD_MUN_IBGE"));
			entity.COD_UF_IBGE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("COD_UF_IBGE"));
			entity.MUNICIPIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MUNICIPIO"));
			entity.UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF"));
			entity.DESCRICAO_UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("DESCRICAO_UF"));
			entity.MUNIUF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("MUNIUF"));


			return entity;
		}
	}
}
	