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
    public partial class ESTOQUELOTEProvider
    {
        //String de conexão recuperada do Web.Config
        //String de conexão recuperada do Web.Config
        private static readonly string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;

        private FbConnection dbCnn = null;
        private FbCommand dbCommand = null;
        private FbTransaction dbTransaction = null;

        ~ESTOQUELOTEProvider()
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

        public void CommitTransaction()
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

        public void RollbackTransaction()
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


        public int Save(ESTOQUELOTEEntity Entity)
        {
            int result = 0;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Sav_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Sav_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                //PrimaryKey com valor igual a null, indica um novo registro, 
                //o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
                if (Entity.IDESTOQUELOTE == -1)
                    dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", DBNull.Value);
                else
                    dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", Entity.IDESTOQUELOTE);

                dbCommand.Parameters.AddWithValue("@QUANTIDADE", Entity.QUANTIDADE); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDLOTE", Entity.IDLOTE); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDPRODUTO", Entity.IDPRODUTO); //Coluna 
                dbCommand.Parameters.AddWithValue("@NUMERODOC", Entity.NUMERODOC); //Coluna 
                dbCommand.Parameters.AddWithValue("@DATA", Entity.DATA); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGTIPO", Entity.FLAGTIPO); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGATIVO", Entity.FLAGATIVO); //Coluna 
                dbCommand.Parameters.AddWithValue("@OBSERVACAO", Entity.OBSERVACAO); //Coluna                 


                //Retorno da Procedure
                FbParameter returnValue;
                returnValue = dbCommand.CreateParameter();

                dbCommand.Parameters["@IDESTOQUELOTE"].Direction = ParameterDirection.InputOutput;


                //Executando consulta
                dbCommand.ExecuteNonQuery();

                result = int.Parse(dbCommand.Parameters["@IDESTOQUELOTE"].Value.ToString());


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


        public int Save(int? IDESTOQUELOTE, decimal? QUANTIDADE, int? IDLOTE, int? IDPRODUTO, string NUMERODOC,
                         DateTime? DATA, string FLAGTIPO, string FLAGATIVO, string OBSERVACAO)
        {
            int result = 0;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Sav_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Sav_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                //PrimaryKey com valor igual a null, indica um novo registro, 
                //o valor da chave será fornecido pelo banco. Qualquer outro valor indicará edição do registro.
                if (IDESTOQUELOTE == -1)
                    dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", DBNull.Value);
                else
                    dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", IDESTOQUELOTE);

                dbCommand.Parameters.AddWithValue("@QUANTIDADE", QUANTIDADE); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDLOTE", IDLOTE); //Coluna 
                dbCommand.Parameters.AddWithValue("@IDPRODUTO", IDPRODUTO); //Coluna 
                dbCommand.Parameters.AddWithValue("@NUMERODOC", NUMERODOC); //Coluna 
                dbCommand.Parameters.AddWithValue("@DATA", DATA); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGTIPO", FLAGTIPO); //Coluna 
                dbCommand.Parameters.AddWithValue("@FLAGATIVO", FLAGATIVO); //Coluna 
                dbCommand.Parameters.AddWithValue("@OBSERVACAO", OBSERVACAO); //Coluna 


                //Retorno da Procedure
                FbParameter returnValue;
                returnValue = dbCommand.CreateParameter();

                dbCommand.Parameters["@IDESTOQUELOTE"].Direction = ParameterDirection.InputOutput;

                //Executando consulta
                dbCommand.ExecuteNonQuery();


                result = int.Parse(dbCommand.Parameters["@IDESTOQUELOTE"].Value.ToString());

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


        public int Delete(int IDESTOQUELOTE)
        {
            int result = 0;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Del_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Del_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);

                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", IDESTOQUELOTE); //PrimaryKey



                //Executando consulta
                dbCommand.ExecuteNonQuery();

                result = IDESTOQUELOTE;

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

        public ESTOQUELOTEEntity Read(int IDESTOQUELOTE)
        {
            FbDataReader reader = null;

            try
            {
                //Verificando a existência de um transação aberta
                if (dbTransaction != null)
                {
                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Rea_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = ((FbTransaction)(dbTransaction));
                }
                else
                {
                    if (dbCnn == null)
                        dbCnn = ((FbConnection)GetConnectionDB());

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand("Rea_ESTOQUELOTE", dbCnn);
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
                }

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.AddWithValue("@IDESTOQUELOTE", IDESTOQUELOTE); //PrimaryKey


                reader = dbCommand.ExecuteReader();

                ESTOQUELOTEEntity entity = null;
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


        public ESTOQUELOTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro)
        {
            FbDataReader dataReader = null;
            ESTOQUELOTECollection collection = null;

            string strSqlCommand = String.Empty;

            try
            {
                if (RowsFiltro != null)
                {
                    if (RowsFiltro.Count > 0)
                    {
                        strSqlCommand = "SELECT * FROM ESTOQUELOTE WHERE (";

                        ArrayList _rowsFiltro = new ArrayList();
                        RowsFiltro.ForEach(delegate (RowsFiltro i)
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
                                    if (item[3].ToUpper() != "LIKE")
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
                        strSqlCommand = "SELECT * FROM ESTOQUELOTE  ";
                    }
                }
                else
                {
                    strSqlCommand = "SELECT * FROM ESTOQUELOTE  ";
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
                    if (dbCnn == null)
                        dbCnn = new FbConnection(connectionString);

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand(strSqlCommand, dbCnn);
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
                }


                collection = ExecuteReader(ref collection, ref dataReader, dbCommand);

                if (dataReader != null)
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

        public ESTOQUELOTECollection ReadCollectionByParameter(List<RowsFiltro> RowsFiltro, string FieldOrder)
        {
            FbDataReader dataReader = null;
            ESTOQUELOTECollection collection = null;

            string strSqlCommand = String.Empty;

            try
            {
                if (RowsFiltro != null)
                {
                    if (RowsFiltro.Count > 0)
                    {
                        strSqlCommand = "SELECT * FROM ESTOQUELOTE WHERE (";

                        ArrayList _rowsFiltro = new ArrayList();
                        RowsFiltro.ForEach(delegate (RowsFiltro i)
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
                                    if (item[3].ToUpper() != "LIKE")
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
                        strSqlCommand = "SELECT * FROM ESTOQUELOTE  order by  " + FieldOrder;
                    }
                }
                else
                {
                    strSqlCommand = "SELECT * FROM ESTOQUELOTE  order by " + FieldOrder;
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
                    if (dbCnn == null)
                        dbCnn = new FbConnection(connectionString);

                    if (dbCnn.State == ConnectionState.Closed)
                        dbCnn.Open();

                    dbCommand = new FbCommand(strSqlCommand, dbCnn);
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.Transaction = dbCnn.BeginTransaction(IsolationLevel.ReadCommitted);
                }


                collection = ExecuteReader(ref collection, ref dataReader, dbCommand);

                if (dataReader != null)
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

        private static ESTOQUELOTECollection ExecuteReader(ref ESTOQUELOTECollection collection, ref FbDataReader dataReader, FbCommand dbCommand)
        {
            using (dataReader = dbCommand.ExecuteReader())
            {
                collection = new ESTOQUELOTECollection();

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

        private static ESTOQUELOTEEntity FillEntityObject(ref FbDataReader DataReader)
        {
            ESTOQUELOTEEntity entity = new ESTOQUELOTEEntity();

            FirebirdGetDbData getData = new FirebirdGetDbData();

            entity.IDESTOQUELOTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDESTOQUELOTE"));
            entity.QUANTIDADE = getData.ConvertDBValueToDecimalNullable(DataReader, DataReader.GetOrdinal("QUANTIDADE"));
            entity.IDLOTE = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDLOTE"));
            entity.IDPRODUTO = getData.ConvertDBValueToInt32(DataReader, DataReader.GetOrdinal("IDPRODUTO"));
            entity.NUMERODOC = getData.ConvertDBValueToString(DataReader, DataReader.GetOrdinal("NUMERODOC"));
            entity.DATA = getData.ConvertDBValueToDateTimeNullable(DataReader, DataReader.GetOrdinal("DATA"));
            entity.FLAGTIPO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGTIPO"));
            entity.FLAGATIVO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGATIVO"));
            entity.FLAGATIVO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("FLAGATIVO"));
            entity.OBSERVACAO = getData.ConvertDBValueToStringNullable(DataReader, DataReader.GetOrdinal("OBSERVACAO"));


            return entity;
        }
    }
}
