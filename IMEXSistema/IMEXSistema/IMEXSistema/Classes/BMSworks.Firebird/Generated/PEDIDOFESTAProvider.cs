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
	public partial class PEDIDOFESTAProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PEDIDOFESTAProvider()
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
		
		
		public  int Save(PEDIDOFESTAEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDPEDIDOFESTA == -1)
					dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA", Entity.IDPEDIDOFESTA);
					
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDTRANSPORTES!= null)
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", Entity.IDTRANSPORTES); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDVENDEDOR!= null)
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", Entity.IDVENDEDOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", Entity.VALORCOMISSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", Entity.TOTALPRODUTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALIMPOSTOS", Entity.TOTALIMPOSTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCDESCONTOS", Entity.PORCDESCONTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTOS", Entity.VALORDESCONTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", Entity.PORCACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", Entity.VALORACRESCIMO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TOTALPEDIDO", Entity.TOTALPEDIDO); //Coluna 
					
					if(Entity.IDFORMAPAGAMENTO!= null)
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", Entity.IDFORMAPAGAMENTO); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@VALORPAGO", Entity.VALORPAGO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", Entity.VALORDEVEDOR); //Coluna 
					
					if(Entity.IDLOCALCOBRANCA!= null)
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", Entity.IDLOCALCOBRANCA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
					
					
					if(Entity.IDCENTROSCUSTOS!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROSCUSTOS", Entity.IDCENTROSCUSTOS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROSCUSTOS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@HOMENAGEADO", Entity.HOMENAGEADO); //Coluna 
					
					if(Entity.IDTEMA!= null)
						dbCommand.Parameters.AddWithValue("@IDTEMA", Entity.IDTEMA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTEMA", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@COR", Entity.COR); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAFESTA", Entity.DATAFESTA); //Coluna 
					dbCommand.Parameters.AddWithValue("@HORAINICIO", Entity.HORAINICIO); //Coluna 
					dbCommand.Parameters.AddWithValue("@HORAFIM", Entity.HORAFIM); //Coluna 
					dbCommand.Parameters.AddWithValue("@ADULTOS", Entity.ADULTOS); //Coluna 
					dbCommand.Parameters.AddWithValue("@CRIANCAS", Entity.CRIANCAS); //Coluna 
					dbCommand.Parameters.AddWithValue("@NOMESALAO", Entity.NOMESALAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TELEFONESALAO", Entity.TELEFONESALAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@ENDSALAO", Entity.ENDSALAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@BAIRROSALAO", Entity.BAIRROSALAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@CIDADESALAO", Entity.CIDADESALAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@UF", Entity.UF); //Coluna 
					dbCommand.Parameters.AddWithValue("@CONTATOSALAO", Entity.CONTATOSALAO); //Coluna 
					
					if(Entity.IDTIPOFESTA!= null)
						dbCommand.Parameters.AddWithValue("@IDTIPOFESTA", Entity.IDTIPOFESTA); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDTIPOFESTA", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", Entity.FLAGORCAMENTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDOFESTA"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDPEDIDOFESTA"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDPEDIDOFESTA, int IDCLIENTE, DateTime DTEMISSAO, int IDSTATUS, int IDTRANSPORTES, int IDVENDEDOR, decimal VALORCOMISSAO, string OBSERVACAO, decimal TOTALPRODUTOS, decimal TOTALIMPOSTOS, decimal PORCDESCONTOS, decimal VALORDESCONTOS, decimal PORCACRESCIMO, decimal VALORACRESCIMO, decimal TOTALPEDIDO, int IDFORMAPAGAMENTO, decimal VALORPAGO, decimal VALORDEVEDOR, int IDLOCALCOBRANCA, int IDCENTROSCUSTOS, string HOMENAGEADO, int IDTEMA, string COR, DateTime DATAFESTA, string HORAINICIO, string HORAFIM, int ADULTOS, int CRIANCAS, string NOMESALAO, string TELEFONESALAO, string ENDSALAO, string BAIRROSALAO, string CIDADESALAO, string UF, string CONTATOSALAO, int IDTIPOFESTA, string FLAGORCAMENTO)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDPEDIDOFESTA == -1)
										dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA", IDPEDIDOFESTA);
										
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										
										if(IDTRANSPORTES!= null)
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", IDTRANSPORTES); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTRANSPORTES", DBNull.Value); //ForeignKey 5
										
										
										if(IDVENDEDOR!= null)
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", IDVENDEDOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDVENDEDOR", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORCOMISSAO", VALORCOMISSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPRODUTOS", TOTALPRODUTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALIMPOSTOS", TOTALIMPOSTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCDESCONTOS", PORCDESCONTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTOS", VALORDESCONTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", PORCACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", VALORACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPEDIDO", TOTALPEDIDO); //Coluna 
										
										if(IDFORMAPAGAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", IDFORMAPAGAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDCENTROSCUSTOS!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROSCUSTOS", IDCENTROSCUSTOS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROSCUSTOS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@HOMENAGEADO", HOMENAGEADO); //Coluna 
										
										if(IDTEMA!= null)
											dbCommand.Parameters.AddWithValue("@IDTEMA", IDTEMA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTEMA", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@COR", COR); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAFESTA", DATAFESTA); //Coluna 
										dbCommand.Parameters.AddWithValue("@HORAINICIO", HORAINICIO); //Coluna 
										dbCommand.Parameters.AddWithValue("@HORAFIM", HORAFIM); //Coluna 
										dbCommand.Parameters.AddWithValue("@ADULTOS", ADULTOS); //Coluna 
										dbCommand.Parameters.AddWithValue("@CRIANCAS", CRIANCAS); //Coluna 
										dbCommand.Parameters.AddWithValue("@NOMESALAO", NOMESALAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TELEFONESALAO", TELEFONESALAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@ENDSALAO", ENDSALAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@BAIRROSALAO", BAIRROSALAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@CIDADESALAO", CIDADESALAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@UF", UF); //Coluna 
										dbCommand.Parameters.AddWithValue("@CONTATOSALAO", CONTATOSALAO); //Coluna 
										
										if(IDTIPOFESTA!= null)
											dbCommand.Parameters.AddWithValue("@IDTIPOFESTA", IDTIPOFESTA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDTIPOFESTA", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", FLAGORCAMENTO); //Coluna 
	
				
								
				//Retorno da Procedure
				FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDOFESTA"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDPEDIDOFESTA"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDPEDIDOFESTA)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA",IDPEDIDOFESTA); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDPEDIDOFESTA;

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

		public  PEDIDOFESTAEntity Read(int IDPEDIDOFESTA)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDOFESTA", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDOFESTA",IDPEDIDOFESTA); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PEDIDOFESTAEntity entity = null;
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

		
		public  PEDIDOFESTACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PEDIDOFESTACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDOFESTA WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDOFESTA  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDOFESTA  ";
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
		
		public  PEDIDOFESTACollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PEDIDOFESTACollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDOFESTA WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDOFESTA  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDOFESTA  order by " + FieldOrder;
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

		private static PEDIDOFESTACollection ExecuteReader(ref PEDIDOFESTACollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PEDIDOFESTACollection();

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

		private static PEDIDOFESTAEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PEDIDOFESTAEntity entity = new PEDIDOFESTAEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDPEDIDOFESTA = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPEDIDOFESTA"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.TOTALPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPRODUTOS"));
			entity.TOTALIMPOSTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALIMPOSTOS"));
			entity.PORCDESCONTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTOS"));
			entity.VALORDESCONTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTOS"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));
			entity.TOTALPEDIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPEDIDO"));
			entity.IDFORMAPAGAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGAMENTO"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDCENTROSCUSTOS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROSCUSTOS"));
			entity.HOMENAGEADO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HOMENAGEADO"));
			entity.IDTEMA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTEMA"));
			entity.COR = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("COR"));
			entity.DATAFESTA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAFESTA"));
			entity.HORAINICIO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORAINICIO"));
			entity.HORAFIM = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("HORAFIM"));
			entity.ADULTOS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("ADULTOS"));
			entity.CRIANCAS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("CRIANCAS"));
			entity.NOMESALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NOMESALAO"));
			entity.TELEFONESALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("TELEFONESALAO"));
			entity.ENDSALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("ENDSALAO"));
			entity.BAIRROSALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("BAIRROSALAO"));
			entity.CIDADESALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CIDADESALAO"));
			entity.UF = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("UF"));
			entity.CONTATOSALAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("CONTATOSALAO"));
			entity.IDTIPOFESTA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTIPOFESTA"));
			entity.FLAGORCAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGORCAMENTO"));


			return entity;
		}
	}
}
