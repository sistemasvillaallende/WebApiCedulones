using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSCedulones.Entities
{
    public class DALBase
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                //return new SqlConnection("Data Source=10.11.15.107;Initial Catalog=SIIMVA;User ID=general");
                return new SqlConnection("Data Source=10.0.0.8;Initial Catalog=SIIMVA;User ID=general");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static SqlConnection GetConnectionSIIMVA()
        {
            try
            {
                return new SqlConnection("Data Source=10.11.15.107;Initial Catalog=SIIMVA;User ID=general");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SqlConnection GetConnection(string strDB)
        {
            try
            {
                return new SqlConnection("Data Source=10.11.15.107;Initial Catalog=" + strDB + ";User ID=general");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string FechaServer()
        {

            string fecha = "";
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection? cn = null;
            cmd = new SqlCommand();

            string strSQL = "SELECT CONVERT(VARCHAR(10), GETDATE(),103) as fecha";
            try
            {
                cn = GetConnectionSIIMVA();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    fecha = dr.GetString(dr.GetOrdinal("fecha"));
                }
            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();
            }
            return fecha;
        }
        //Cedulones2", "nro_cedulon"
        public static long GetMaxID(string tableName, string campo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT ISNULL(MAX(" + campo + "),0) as mayor ");
                sql.AppendLine("FROM " + tableName);
                using (SqlConnection con = GetConnectionSIIMVA())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@campo", campo);
                    cmd.Connection.Open();
                    return Convert.ToInt64(cmd.ExecuteScalar()) + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public static int GetNroTransaccion(SqlConnection cn, SqlTransaction trx, int subsistema)
        //{
        //    try
        //    {
        //        int nro_transaccion = 0;
        //        string strSQL = string.Empty;
        //        switch (subsistema)
        //        {
        //            case 1:
        //                break;
        //            case 2:
        //                break;
        //            case 3:
        //                break;
        //            case 4:
        //                strSQL = @"SELECT ISNULL(MAX(nro_tran_automotor),0) as nro_transaccion
        //                           FROM Numeros_Claves";
        //                break;
        //            default:
        //                break;
        //        }
        //        //
        //        SqlCommand cmd = cn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strSQL;
        //        cmd.Transaction = trx;
        //        nro_transaccion = Convert.ToInt32(cmd.ExecuteScalar());
        //        return nro_transaccion + 1;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public static int GetNroTransaccion(int subsistema)
        {
            try
            {
                int nro_transaccion = 0;
                string strSQL = string.Empty;
                switch (subsistema)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        strSQL = @"SELECT ISNULL(MAX(nro_tran_automotor),0) as nro_transaccion
                                   FROM Numeros_Claves";
                        break;
                    default:
                        break;
                }
                using (SqlConnection cn = GetConnectionSIIMVA())
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Connection.Open();
                    nro_transaccion = Convert.ToInt32(cmd.ExecuteScalar());
                    return nro_transaccion + 1;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        //public static void UpdateNroTransaccion(SqlConnection cn, SqlTransaction trx, int subsistema, int nro_transaccion)
        //{
        //    try
        //    {
        //        string strSQL = string.Empty;
        //        switch (subsistema)
        //        {
        //            case 1:
        //                break;
        //            case 2:
        //                break;
        //            case 3:
        //                break;
        //            case 4:
        //                strSQL = @"UPDATE Numeros_claves
        //                           Set nro_tran_automotor = @nro_transaccion
        //                           FROM Numeros_Claves";
        //                break;
        //            default:
        //                break;
        //        }
        //        //
        //        SqlCommand cmd = cn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strSQL;
        //        cmd.Transaction = trx;
        //        cmd.Parameters.AddWithValue("@nro_transaccion", nro_transaccion);
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public static void UpdateNroTransaccion(int subsistema, int nro_transaccion)
        {
            try
            {
                string strSQL = string.Empty;
                switch (subsistema)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        strSQL = @"UPDATE Numeros_claves
                                   SET nro_tran_automotor = @nro_transaccion
                                   FROM Numeros_Claves";
                        break;
                    default:
                        break;
                }
                //
                using (SqlConnection cn = GetConnectionSIIMVA())
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@nro_transaccion", nro_transaccion);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static long NewID(string tableName, string campo, SqlConnection cn, SqlTransaction tran)
        {
            long id = 0;

            SqlCommand cmd = cn.CreateCommand();
            cmd.Transaction = tran;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT MAX(nro_cedulon) As Mayor");
            strSQL.AppendLine("FROM " + tableName);

            // cmd.Parameters.Add(new SqlParameter("@campo", campo));
            //cmd.Parameters.Add(new SqlParameter("@table", tableName));

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    id = dr.GetInt32(0) + 1;
                dr.Close();
                return id;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error, no se pudo obtener el prox. código, " + ex.Message);
                throw ex;
                /*EventLog.WriteEntry("netLibraty - nvoCodigo ", ex.Message);*/
            }
            finally { cmd.Dispose(); }
        }



    }
}