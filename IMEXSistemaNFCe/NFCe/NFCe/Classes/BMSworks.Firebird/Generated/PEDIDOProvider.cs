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
	public partial class PEDIDOProvider
	{
		//String de conexão recuperada do Web.Config
		//String de conexão recuperada do Web.Config
		private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
		
		private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

		~PEDIDOProvider()
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
		
		
		public  int Save(PEDIDOEntity Entity )
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				//PrimaryKey com valor igual a null, indica um novo registro, 
				//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
				if (Entity.IDPEDIDO == -1)
					dbCommand.Parameters.AddWithValue("@IDPEDIDO", DBNull.Value);
				else
					dbCommand.Parameters.AddWithValue("@IDPEDIDO", Entity.IDPEDIDO);
					
					
					if(Entity.IDCLIENTE!= null)
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", Entity.IDCLIENTE); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@DTEMISSAO", Entity.DTEMISSAO); //Coluna 
					
					if(Entity.IDSTATUS!= null)
						dbCommand.Parameters.AddWithValue("@IDSTATUS", Entity.IDSTATUS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", Entity.PRAZOENTREGA); //Coluna 
					
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
					dbCommand.Parameters.AddWithValue("@PORCDESCONTO", Entity.PORCDESCONTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@VALORDESCONTO", Entity.VALORDESCONTO); //Coluna 
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
					
					
					if(Entity.IDCENTROCUSTOS!= null)
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTOS", Entity.IDCENTROCUSTOS); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDCENTROCUSTOS", DBNull.Value); //ForeignKey 5
					
					dbCommand.Parameters.AddWithValue("@FLAGPRODIMPRESSAO", Entity.FLAGPRODIMPRESSAO); //Coluna 
					dbCommand.Parameters.AddWithValue("@PRODUTOFINAL", Entity.PRODUTOFINAL); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", Entity.FLAGORCAMENTO); //Coluna 
					dbCommand.Parameters.AddWithValue("@NREFERENCIA", Entity.NREFERENCIA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGVLMETRO", Entity.FLAGVLMETRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@OBSANEXO", Entity.OBSANEXO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAENTREGA", Entity.DATAENTREGA); //Coluna 
					dbCommand.Parameters.AddWithValue("@FLAGTELABLOQUEADA", Entity.FLAGTELABLOQUEADA); //Coluna 
					dbCommand.Parameters.AddWithValue("@TIPOPAGTODINHEIRO", Entity.TIPOPAGTODINHEIRO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TIPOPAGTOCHEQUE", Entity.TIPOPAGTOCHEQUE); //Coluna 
					dbCommand.Parameters.AddWithValue("@TIPOPAGTOCARTAODEBITO", Entity.TIPOPAGTOCARTAODEBITO); //Coluna 
					dbCommand.Parameters.AddWithValue("@TIPOPAGTOCARTAOCREDITO", Entity.TIPOPAGTOCARTAOCREDITO); //Coluna 
					dbCommand.Parameters.AddWithValue("@DATAVECTO", Entity.DATAVECTO); //Coluna 
					
					if(Entity.IDSUPERVISOR!= null)
						dbCommand.Parameters.AddWithValue("@IDSUPERVISOR", Entity.IDSUPERVISOR); //ForeignKey 
					else
						dbCommand.Parameters.AddWithValue("@IDSUPERVISOR", DBNull.Value); //ForeignKey 5

                if (Entity.IDMESA != null)
                    dbCommand.Parameters.AddWithValue("@IDMESA", Entity.IDMESA); //ForeignKey 
                else
                    dbCommand.Parameters.AddWithValue("@IDMESA", DBNull.Value); //ForeignKey 5
             

                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDO"].Direction = ParameterDirection.InputOutput;

				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = int.Parse(dbCommand.Parameters["@IDPEDIDO"].Value.ToString());
				
	
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
		
		
		public  int Save(int? IDPEDIDO, int IDCLIENTE, DateTime DTEMISSAO, int IDSTATUS, string PRAZOENTREGA, int IDTRANSPORTES, 
            int IDVENDEDOR, decimal VALORCOMISSAO, string OBSERVACAO, decimal TOTALPRODUTOS, decimal TOTALIMPOSTOS,
            decimal PORCDESCONTO, decimal VALORDESCONTO, decimal PORCACRESCIMO, decimal VALORACRESCIMO, decimal TOTALPEDIDO, 
            int IDFORMAPAGAMENTO, decimal VALORPAGO, decimal VALORDEVEDOR, int IDLOCALCOBRANCA, int IDCENTROCUSTOS, 
            string FLAGPRODIMPRESSAO, string PRODUTOFINAL, string FLAGORCAMENTO, string NREFERENCIA, string FLAGVLMETRO, 
            string OBSANEXO, DateTime DATAENTREGA, string FLAGTELABLOQUEADA, decimal TIPOPAGTODINHEIRO, decimal TIPOPAGTOCHEQUE,
            decimal TIPOPAGTOCARTAODEBITO, decimal TIPOPAGTOCARTAOCREDITO, DateTime DATAVECTO, int IDSUPERVISOR,
            int? IDMESA)
		{	
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Sav_PEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

									//PrimaryKey com valor igual a null, indica um novo registro, 
									//o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
									if (IDPEDIDO == -1)
										dbCommand.Parameters.AddWithValue("@IDPEDIDO", DBNull.Value);
									else
										dbCommand.Parameters.AddWithValue("@IDPEDIDO", IDPEDIDO);
										
										
										if(IDCLIENTE!= null)
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", IDCLIENTE); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@DTEMISSAO", DTEMISSAO); //Coluna 
										
										if(IDSTATUS!= null)
											dbCommand.Parameters.AddWithValue("@IDSTATUS", IDSTATUS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSTATUS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@PRAZOENTREGA", PRAZOENTREGA); //Coluna 
										
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
										dbCommand.Parameters.AddWithValue("@PORCDESCONTO", PORCDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDESCONTO", VALORDESCONTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PORCACRESCIMO", PORCACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORACRESCIMO", VALORACRESCIMO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TOTALPEDIDO", TOTALPEDIDO); //Coluna 
             

                if (IDFORMAPAGAMENTO!= null)
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", IDFORMAPAGAMENTO); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDFORMAPAGAMENTO", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@VALORPAGO", VALORPAGO); //Coluna 
										dbCommand.Parameters.AddWithValue("@VALORDEVEDOR", VALORDEVEDOR); //Coluna 
										
										if(IDLOCALCOBRANCA!= null)
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", IDLOCALCOBRANCA); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDLOCALCOBRANCA", DBNull.Value); //ForeignKey 5
										
										
										if(IDCENTROCUSTOS!= null)
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTOS", IDCENTROCUSTOS); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDCENTROCUSTOS", DBNull.Value); //ForeignKey 5
										
										dbCommand.Parameters.AddWithValue("@FLAGPRODIMPRESSAO", FLAGPRODIMPRESSAO); //Coluna 
										dbCommand.Parameters.AddWithValue("@PRODUTOFINAL", PRODUTOFINAL); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGORCAMENTO", FLAGORCAMENTO); //Coluna 
										dbCommand.Parameters.AddWithValue("@NREFERENCIA", NREFERENCIA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGVLMETRO", FLAGVLMETRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@OBSANEXO", OBSANEXO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAENTREGA", DATAENTREGA); //Coluna 
										dbCommand.Parameters.AddWithValue("@FLAGTELABLOQUEADA", FLAGTELABLOQUEADA); //Coluna 
										dbCommand.Parameters.AddWithValue("@TIPOPAGTODINHEIRO", TIPOPAGTODINHEIRO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TIPOPAGTOCHEQUE", TIPOPAGTOCHEQUE); //Coluna 
										dbCommand.Parameters.AddWithValue("@TIPOPAGTOCARTAODEBITO", TIPOPAGTOCARTAODEBITO); //Coluna 
										dbCommand.Parameters.AddWithValue("@TIPOPAGTOCARTAOCREDITO", TIPOPAGTOCARTAOCREDITO); //Coluna 
										dbCommand.Parameters.AddWithValue("@DATAVECTO", DATAVECTO); //Coluna 
										
										if(IDSUPERVISOR!= null)
											dbCommand.Parameters.AddWithValue("@IDSUPERVISOR", IDSUPERVISOR); //ForeignKey 
										else
											dbCommand.Parameters.AddWithValue("@IDSUPERVISOR", DBNull.Value); //ForeignKey 5


                if (IDMESA != null)
                    dbCommand.Parameters.AddWithValue("@IDMESA", IDMESA); //ForeignKey 
                else
                    dbCommand.Parameters.AddWithValue("@IDMESA", DBNull.Value); //ForeignKey 5




                //Retorno da Procedure
                FbParameter returnValue;
				returnValue = dbCommand.CreateParameter();
				
				dbCommand.Parameters["@IDPEDIDO"].Direction = ParameterDirection.InputOutput;
				
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							

				result = int.Parse(dbCommand.Parameters["@IDPEDIDO"].Value.ToString());
				
				

	
				
	
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
		
		
		public  int Delete(int IDPEDIDO)
		{
			int result = 0;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Del_PEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDO",IDPEDIDO); //PrimaryKey


		
				//Executando consulta
				dbCommand.ExecuteNonQuery();
							
			    result = IDPEDIDO;

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

		public  PEDIDOEntity Read(int IDPEDIDO)
		{
			FbDataReader reader = null;

			try
			{
				//Verificando a existência de um transação aberta
				if (dbTransaction != null)
				{
					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDO", dbCnn);
					dbCommand.Transaction = ((FbTransaction)(dbTransaction));
				}
				else
				{
					if (dbCnn == null)
						dbCnn = ((FbConnection)GetConnectionDB());

					if (dbCnn.State == ConnectionState.Closed)
						dbCnn.Open();

					dbCommand = new FbCommand("Rea_PEDIDO", dbCnn);
					dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
				}

				dbCommand.CommandType = CommandType.StoredProcedure;

				dbCommand.Parameters.AddWithValue("@IDPEDIDO",IDPEDIDO); //PrimaryKey


				reader = dbCommand.ExecuteReader();

				PEDIDOEntity entity = null;
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

		
		public  PEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
		{
			FbDataReader dataReader = null;
			PEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDO  ";
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDO  ";
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
		
		public  PEDIDOCollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
		{
			FbDataReader dataReader = null;
			PEDIDOCollection collection = null;
			
			string strSqlCommand = String.Empty;

			try
			{
				if (RowsFiltro != null)
				{
					if (RowsFiltro.Count > 0)
					{
						strSqlCommand = "SELECT * FROM PEDIDO WHERE (";

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
						strSqlCommand = "SELECT * FROM PEDIDO  order by  " + FieldOrder;
					}
				}
				else
				{
					strSqlCommand = "SELECT * FROM PEDIDO  order by " + FieldOrder;
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

		private static PEDIDOCollection ExecuteReader(ref PEDIDOCollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
		{
			using (dataReader = dbCommand.ExecuteReader())
			{
				collection = new PEDIDOCollection();

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

		private static PEDIDOEntity FillEntityObject(ref FbDataReader DataReader) 
		{
			PEDIDOEntity entity = new PEDIDOEntity();

			FirebirdGetDbData getData = new FirebirdGetDbData();

							entity.IDPEDIDO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPEDIDO"));
			entity.IDCLIENTE = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCLIENTE"));
			entity.DTEMISSAO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DTEMISSAO"));
			entity.IDSTATUS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSTATUS"));
			entity.PRAZOENTREGA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PRAZOENTREGA"));
			entity.IDTRANSPORTES = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDTRANSPORTES"));
			entity.IDVENDEDOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDVENDEDOR"));
			entity.VALORCOMISSAO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORCOMISSAO"));
			entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));
			entity.TOTALPRODUTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPRODUTOS"));
			entity.TOTALIMPOSTOS = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALIMPOSTOS"));
			entity.PORCDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCDESCONTO"));
			entity.VALORDESCONTO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDESCONTO"));
			entity.PORCACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("PORCACRESCIMO"));
			entity.VALORACRESCIMO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORACRESCIMO"));
			entity.TOTALPEDIDO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TOTALPEDIDO"));
			entity.IDFORMAPAGAMENTO = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDFORMAPAGAMENTO"));
			entity.VALORPAGO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORPAGO"));
			entity.VALORDEVEDOR = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("VALORDEVEDOR"));
			entity.IDLOCALCOBRANCA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDLOCALCOBRANCA"));
			entity.IDCENTROCUSTOS = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDCENTROCUSTOS"));
			entity.FLAGPRODIMPRESSAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGPRODIMPRESSAO"));
			entity.PRODUTOFINAL = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("PRODUTOFINAL"));
			entity.FLAGORCAMENTO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGORCAMENTO"));
			entity.NREFERENCIA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("NREFERENCIA"));
			entity.FLAGVLMETRO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGVLMETRO"));
			entity.OBSANEXO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSANEXO"));
			entity.DATAENTREGA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAENTREGA"));
			entity.FLAGTELABLOQUEADA = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTELABLOQUEADA"));
			entity.TIPOPAGTODINHEIRO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TIPOPAGTODINHEIRO"));
			entity.TIPOPAGTOCHEQUE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TIPOPAGTOCHEQUE"));
			entity.TIPOPAGTOCARTAODEBITO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TIPOPAGTOCARTAODEBITO"));
			entity.TIPOPAGTOCARTAOCREDITO = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("TIPOPAGTOCARTAOCREDITO"));
			entity.DATAVECTO = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATAVECTO"));
			entity.IDSUPERVISOR = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDSUPERVISOR"));
            entity.IDMESA = getData.ConvertDBValueToInt32Nullable(DataReader, DataReader.GetOrdinal("IDMESA"));           

            return entity;
		}
	}
}
