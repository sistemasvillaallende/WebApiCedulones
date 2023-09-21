using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WSCedulones.Entities
{
    public class CEDULON_PRINT_DETALLE : DALBase
    {
        public string periodo { get; set; }
        public string concepto { get; set; }
        public Decimal montoPagado { get; set; }
        public Decimal montoOriginal { get; set; }
        public Decimal recargo { get; set; }
        public Decimal descInteres { get; set; }
        public decimal saldoFavor { get; set; }
        public int nro_transaccion { get; set; }

        public CEDULON_PRINT_DETALLE()
        {
            periodo = string.Empty;
            concepto = string.Empty;
            montoPagado = Decimal.Zero;
            montoOriginal = Decimal.Zero;
            recargo = Decimal.Zero;
            descInteres = Decimal.Zero;
            saldoFavor = 0;
        }
        public static List<CEDULON_PRINT_DETALLE> readAuto(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> detalleCedulonList = new List<CEDULON_PRINT_DETALLE>();
                string sql = @"SELECT b.periodo, des_concepto_inmueble =
                            ci.des_categoria,
                            b.debe - ISNULL(b.recargo,0) as monto_original,
                            ISNULL(b.recargo, 0),
                            (SELECT SUM(haber) FROM
                            CTASCTES_AUTOMOTORES C2 WHERE
                            C2.nro_transaccion = b.nro_transaccion  AND
                            C2.DOMINIO = b.DOMINIO) as 'Saldo a favor',
                            (b.debe - (SELECT SUM(haber) FROM
                            CTASCTES_AUTOMOTORES C2 WHERE
                            C2.nro_transaccion = b.nro_transaccion  AND
                            C2.DOMINIO = b.DOMINIO)) - a.monto_pagado as
                            desc_interes, a.monto_pagado
                            FROM DEUDAS_X_CEDULON3 AS a INNER
                            JOIN CTASCTES_AUTOMOTORES AS b ON a.nro_transaccion =
                            b.nro_transaccion AND
                            b.tipo_transaccion=1 AND b.pagado=0
                            JOIN CATE_DEUDA_AUTOMOTOR As ci ON
                            a.categoria_deuda=ci.cod_categoria
                            WHERE (a.nro_cedulon = @nro_cedulon) ORDER BY b.periodo";
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@nro_cedulon", nroCedulon);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon = new CEDULON_PRINT_DETALLE();
                            if (!sqlDataReader.IsDBNull(0))
                                detalleCedulon.periodo = sqlDataReader.GetString(0);
                            if (!sqlDataReader.IsDBNull(1))
                                detalleCedulon.concepto = sqlDataReader.GetString(1);
                            if (!sqlDataReader.IsDBNull(2))
                                detalleCedulon.montoOriginal = sqlDataReader.GetDecimal(2);
                            if (!sqlDataReader.IsDBNull(3))
                                detalleCedulon.recargo = sqlDataReader.GetDecimal(3);
                            if (!sqlDataReader.IsDBNull(4))
                                detalleCedulon.saldoFavor = sqlDataReader.GetDecimal(4);
                            if (!sqlDataReader.IsDBNull(5))
                                detalleCedulon.descInteres = sqlDataReader.GetDecimal(5);
                            if (!sqlDataReader.IsDBNull(6))
                                detalleCedulon.montoPagado = sqlDataReader.GetDecimal(6);
                            detalleCedulonList.Add(detalleCedulon);
                        }
                    }
                }
                return detalleCedulonList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readAutoAnual(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> lst = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder stringBuilder = new StringBuilder();
                CEDULON_PRINT_DETALLE obj;
                stringBuilder.AppendLine("SELECT d.periodo AS PERIODO, B.des_concepto_dominio, A.importe_actual, B.suma");
                stringBuilder.AppendLine("FROM DETALLE_DEUDA_AUTO_ANUAL A");
                stringBuilder.AppendLine("INNER JOIN CONCEPTOS_AUTO B ON A.cod_concepto_item = B.cod_concepto_dominio");
                stringBuilder.AppendLine("INNER JOIN DEUDAS_X_CEDULON3 C ON A.nro_transaccion = C.nro_transaccion");
                stringBuilder.AppendLine("INNER JOIN CEDULONES2 D ON D.nro_cedulon = C.nro_cedulon");
                stringBuilder.AppendLine("WHERE D.nro_cedulon = @nroCedulon");
                stringBuilder.AppendLine("ORDER BY nro_item");

                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroCedulon", (object)nroCedulon);
                    command.Connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            obj = new CEDULON_PRINT_DETALLE();
                            obj.periodo = dr.GetString(0);
                            obj.concepto = dr.GetString(1);
                            obj.montoPagado = dr.GetDecimal(2);
                            bool suma = dr.GetBoolean(3);
                            if (!suma)
                            {
                                obj.montoPagado = obj.montoPagado - (obj.montoPagado * 2);
                            }
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readCementerio(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> detalleCedulonList = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT b.periodo, des_concepto_inmueble = ci.des_categoria, b.monto_original, b.recargo,");
                stringBuilder.AppendLine("(b.MONTO_ORIGINAL + b.recargo) - a.monto_pagado as desc_interes, a.monto_pagado");
                stringBuilder.AppendLine("FROM DEUDAS_X_CEDULON3 AS a INNER");
                stringBuilder.AppendLine("JOIN CTASCTES_CEMENTERIO AS b ON a.nro_transaccion = b.nro_transaccion AND");
                stringBuilder.AppendLine("b.tipo_transaccion=1 AND b.pagado=0");
                stringBuilder.AppendLine("JOIN CATE_DEUDA_CEMENTERIO As ci ON a.categoria_deuda=ci.cod_categoria");
                stringBuilder.AppendLine("WHERE (a.nro_cedulon = @nroCedulon) ORDER BY b.periodo");
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroCedulon", (object)nroCedulon);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon = new CEDULON_PRINT_DETALLE();
                            if (!sqlDataReader.IsDBNull(0))
                                detalleCedulon.periodo = sqlDataReader.GetString(0);
                            if (!sqlDataReader.IsDBNull(1))
                                detalleCedulon.concepto = sqlDataReader.GetString(1);
                            if (!sqlDataReader.IsDBNull(2))
                                detalleCedulon.montoOriginal = sqlDataReader.GetDecimal(2);
                            if (!sqlDataReader.IsDBNull(3))
                                detalleCedulon.recargo = sqlDataReader.GetDecimal(3);
                            if (!sqlDataReader.IsDBNull(4))
                                detalleCedulon.descInteres = sqlDataReader.GetDecimal(4);
                            if (!sqlDataReader.IsDBNull(5))
                                detalleCedulon.montoPagado = sqlDataReader.GetDecimal(5);
                            detalleCedulonList.Add(detalleCedulon);
                        }
                    }
                }
                return detalleCedulonList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readTasa(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> detalleCedulonList = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT b.periodo, des_concepto_inmueble =");
                stringBuilder.AppendLine("ci.des_categoria,");
                stringBuilder.AppendLine("b.debe - ISNULL(b.recargo, 0) as monto_original,");
                stringBuilder.AppendLine("ISNULL(b.recargo, 0),");
                stringBuilder.AppendLine("(SELECT SUM(haber) FROM");
                stringBuilder.AppendLine("CTASCTES_INMUEBLES C2 WHERE");
                stringBuilder.AppendLine("C2.nro_transaccion = b.nro_transaccion  AND");
                stringBuilder.AppendLine("C2.circunscripcion = b.circunscripcion AND");
                stringBuilder.AppendLine("C2.seccion = b.seccion  AND");
                stringBuilder.AppendLine("C2.manzana = b.manzana  AND");
                stringBuilder.AppendLine("C2.parcela = b.parcela  AND");
                stringBuilder.AppendLine("C2.p_h = b.p_h) as 'Saldo a favor',");
                stringBuilder.AppendLine("(b.debe - (SELECT SUM(haber) FROM");
                stringBuilder.AppendLine("CTASCTES_INMUEBLES C2 WHERE");
                stringBuilder.AppendLine("C2.nro_transaccion = b.nro_transaccion  AND");
                stringBuilder.AppendLine("C2.circunscripcion = b.circunscripcion AND");
                stringBuilder.AppendLine("C2.seccion = b.seccion  AND");
                stringBuilder.AppendLine("C2.manzana = b.manzana  AND");
                stringBuilder.AppendLine("C2.parcela = b.parcela  AND");
                stringBuilder.AppendLine("C2.p_h = b.p_h)) - a.monto_pagado as");
                stringBuilder.AppendLine("desc_interes, a.monto_pagado");
                stringBuilder.AppendLine("FROM DEUDAS_X_CEDULON3 AS a INNER");
                stringBuilder.AppendLine("JOIN CTASCTES_INMUEBLES AS b ON a.nro_transaccion =");
                stringBuilder.AppendLine("b.nro_transaccion AND");
                stringBuilder.AppendLine("b.tipo_transaccion=1 AND b.pagado=0");
                stringBuilder.AppendLine("JOIN CATE_DEUDA_INMUEBLE As ci ON");
                stringBuilder.AppendLine("a.categoria_deuda=ci.cod_categoria");
                stringBuilder.AppendLine("WHERE (a.nro_cedulon = @nroCedulon) ORDER BY b.periodo");

                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroCedulon", (object)nroCedulon);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon = new CEDULON_PRINT_DETALLE();
                            if (!sqlDataReader.IsDBNull(0))
                                detalleCedulon.periodo = sqlDataReader.GetString(0);
                            if (!sqlDataReader.IsDBNull(1))
                                detalleCedulon.concepto = sqlDataReader.GetString(1);
                            if (!sqlDataReader.IsDBNull(2))
                                detalleCedulon.montoOriginal = sqlDataReader.GetDecimal(2);
                            if (!sqlDataReader.IsDBNull(3))
                                detalleCedulon.recargo = sqlDataReader.GetDecimal(3);
                            if (!sqlDataReader.IsDBNull(4))
                                detalleCedulon.saldoFavor = sqlDataReader.GetDecimal(4);
                            if (!sqlDataReader.IsDBNull(5))
                                detalleCedulon.descInteres = sqlDataReader.GetDecimal(5);
                            if (!sqlDataReader.IsDBNull(6))
                                detalleCedulon.montoPagado = sqlDataReader.GetDecimal(6);
                            detalleCedulonList.Add(detalleCedulon);
                        }
                    }
                }
                return detalleCedulonList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readTasaAnual(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> lst = new List<CEDULON_PRINT_DETALLE>();
                CEDULON_PRINT_DETALLE obj;
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT D.periodo AS PERIODO, B.des_concepto_inmueble, A.importe_actual, B.suma");
                sql.AppendLine("FROM DETALLE_DEUDA_INMUEBLE_ANUAL A");
                sql.AppendLine("INNER JOIN CONCEPTOS_INMUEBLE B ON A.cod_concepto_item = B.cod_concepto_inmueble");
                sql.AppendLine("INNER JOIN DEUDAS_X_CEDULON3 C ON A.nro_transaccion = C.nro_transaccion");
                sql.AppendLine("INNER JOIN CEDULONES2 D ON D.nro_cedulon = C.nro_cedulon");
                sql.AppendLine("WHERE D.nro_cedulon = @nroCedulon");
                sql.AppendLine("ORDER BY nro_item");

                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@nroCedulon", nroCedulon);
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            obj = new CEDULON_PRINT_DETALLE();
                            obj.periodo = dr.GetString(0);
                            obj.concepto = dr.GetString(1);
                            obj.montoPagado = dr.GetDecimal(2);
                            bool suma = dr.GetBoolean(3);
                            if (!suma)
                            {
                                obj.montoPagado = obj.montoPagado - (obj.montoPagado * 2);
                            }
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readIndyCom(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> detalleCedulonList = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT b.periodo, des_concepto_inmueble =");
                stringBuilder.AppendLine("ci.des_categoria,");
                stringBuilder.AppendLine("ISNULL(b.debe, 0) - ISNULL(b.recargo,0) as monto_original,");
                stringBuilder.AppendLine("ISNULL(b.recargo,0),");
                stringBuilder.AppendLine("(SELECT SUM(haber) FROM");
                stringBuilder.AppendLine("CTASCTES_INDYCOM C2 WHERE");
                stringBuilder.AppendLine("C2.nro_transaccion = b.nro_transaccion  AND");
                stringBuilder.AppendLine("C2.legajo = b.legajo) as 'Saldo a favor',");
                stringBuilder.AppendLine("(b.debe - (SELECT SUM(haber) FROM");
                stringBuilder.AppendLine("CTASCTES_INDYCOM C2 WHERE");
                stringBuilder.AppendLine("C2.nro_transaccion = b.nro_transaccion  AND");
                stringBuilder.AppendLine("C2.legajo = b.legajo)) - a.monto_pagado as");
                stringBuilder.AppendLine("desc_interes, a.monto_pagado");
                stringBuilder.AppendLine("FROM DEUDAS_X_CEDULON3 AS a INNER");
                stringBuilder.AppendLine("JOIN CTASCTES_INDYCOM AS b ON a.nro_transaccion =");
                stringBuilder.AppendLine("b.nro_transaccion AND");
                stringBuilder.AppendLine("b.tipo_transaccion=1 AND b.pagado=0");
                stringBuilder.AppendLine("JOIN CATE_DEUDA_INDYCOM As ci ON");
                stringBuilder.AppendLine("a.categoria_deuda=ci.cod_categoria");
                stringBuilder.AppendLine("WHERE (a.nro_cedulon = @nroCedulon) ORDER BY b.periodo");
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroCedulon", (object)nroCedulon);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon = new CEDULON_PRINT_DETALLE();
                            if (!sqlDataReader.IsDBNull(0))
                                detalleCedulon.periodo = sqlDataReader.GetString(0);
                            if (!sqlDataReader.IsDBNull(1))
                                detalleCedulon.concepto = sqlDataReader.GetString(1);
                            if (!sqlDataReader.IsDBNull(2))
                                detalleCedulon.montoOriginal = sqlDataReader.GetDecimal(2);
                            if (!sqlDataReader.IsDBNull(3))
                                detalleCedulon.recargo = sqlDataReader.GetDecimal(3);
                            if (!sqlDataReader.IsDBNull(4))
                                detalleCedulon.saldoFavor = sqlDataReader.GetDecimal(4);
                            if (!sqlDataReader.IsDBNull(5))
                                detalleCedulon.descInteres = sqlDataReader.GetDecimal(5);
                            if (!sqlDataReader.IsDBNull(6))
                                detalleCedulon.montoPagado = sqlDataReader.GetDecimal(6);
                            detalleCedulonList.Add(detalleCedulon);
                        }
                    }
                }
                return detalleCedulonList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readMulta(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> detalleCedulonList = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT d.nro_expediente AS nro_acta,cdm.des_categoria AS nom_multa,d.importe AS monto_2");
                stringBuilder.AppendLine("FROM DEUDAS_X_SUMARIO d");
                stringBuilder.AppendLine("LEFT JOIN CATE_DEUDA_MULTAS cdm ON cdm.cod_categoria=d.categoria_deuda");
                stringBuilder.AppendLine("LEFT JOIN CTASCTES_MULTAS cm ON cm.nro_transaccion=d.nro_transaccion");
                stringBuilder.AppendLine("JOIN deudas_x_cedulon3 DC ON DC.nro_transaccion = cm.nro_transaccion");
                stringBuilder.AppendLine("WHERE DC.NRO_CEDULON = @nroCedulon");
                stringBuilder.AppendLine("ORDER BY d.categoria_deuda");
                //stringBuilder.AppendLine("INNER JOIN CEDULONES2 c2 ON d.nro_cedulon = c2.nro_cedulon");
                //stringBuilder.AppendLine("WHERE d.nro_cedulon=@nroCedulon ORDER By s.fecha_acta_infraccion, s.nro_acta");

                //stringBuilder.AppendLine("SELECT d.nro_expediente AS nro_acta,cdm.des_categoria AS nom_multa,d.importe AS monto_2");
                //stringBuilder.AppendLine("FROM DEUDAS_X_SUMARIO d");
                //stringBuilder.AppendLine("LEFT JOIN CATE_DEUDA_MULTAS cdm ON cdm.cod_categoria=d.categoria_deuda");
                //stringBuilder.AppendLine("LEFT JOIN CTASCTES_MULTAS cm ON cm.nro_transaccion=d.nro_transaccion");
                //stringBuilder.AppendLine("JOIN deudas_x_cedulon3 DC ON DC.nro_transaccion = cm.nro_transaccion");
                //stringBuilder.AppendLine("WHERE DC.NRO_CEDULON = @nroCedulon");
                //stringBuilder.AppendLine("ORDER BY d.categoria_deuda");




                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroCedulon", (object)nroCedulon);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader1 = command.ExecuteReader();
                    if (sqlDataReader1.HasRows)
                    {
                        while (sqlDataReader1.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon1 = new CEDULON_PRINT_DETALLE();
                            CEDULON_PRINT_DETALLE detalleCedulon2 = detalleCedulon1;
                            SqlDataReader sqlDataReader2 = sqlDataReader1;
                            string str1 = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("nro_acta")).ToString();
                            detalleCedulon2.periodo = str1;
                            CEDULON_PRINT_DETALLE detalleCedulon3 = detalleCedulon1;
                            SqlDataReader sqlDataReader3 = sqlDataReader1;
                            string str2 = sqlDataReader3.GetString(sqlDataReader3.GetOrdinal("nom_multa"));
                            detalleCedulon3.concepto = str2;
                            CEDULON_PRINT_DETALLE detalleCedulon4 = detalleCedulon1;
                            SqlDataReader sqlDataReader4 = sqlDataReader1;
                            Decimal num = sqlDataReader4.GetDecimal(sqlDataReader4.GetOrdinal("monto_2"));
                            detalleCedulon4.montoPagado = num;
                            detalleCedulonList.Add(detalleCedulon1);
                        }
                    }
                }
                return detalleCedulonList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CEDULON_PRINT_DETALLE> readFacturacion(long nroCedulon)
        {
            try
            {
                List<CEDULON_PRINT_DETALLE> lst = new List<CEDULON_PRINT_DETALLE>();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT b.periodo, des_concepto_inmueble = c.des_concepto_carnet + ' ' +");
                sql.AppendLine("ISNULL(D.CLASE, ''), ci.importe_actual, ci.suma_item, ci.duracion, b.nro_transaccion");
                sql.AppendLine("FROM DEUDAS_X_CEDULON3 AS a INNER");
                sql.AppendLine("JOIN CTASCTES_FACTU AS b ON a.nro_transaccion = b.nro_transaccion AND b.pago_parcial=0 AND");
                sql.AppendLine("b.tipo_transaccion=1 and pagado = 0");
                sql.AppendLine("JOIN DETALLE_DEUDA_FACTU As ci ON a.nro_transaccion=ci.nro_transaccion");
                sql.AppendLine("FULL JOIN CONCEPTOS_FACTU c ON ci.cod_concepto_item=c.cod_concepto_carnet");
                sql.AppendLine("FULL JOIN LICENCIAS_CONDUCIR D ON CI.TIPO_LICENCIA=D.ID");
                sql.AppendLine("WHERE (a.nro_cedulon = @nro_cedulon)");
                sql.AppendLine("ORDER BY b.periodo");

                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@nro_cedulon", nroCedulon);
                    cmd.Connection.Open();

                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    bool suma = false;
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CEDULON_PRINT_DETALLE detalleCedulon = new CEDULON_PRINT_DETALLE();
                            if (!sqlDataReader.IsDBNull(0))
                                detalleCedulon.periodo = sqlDataReader.GetString(0);
                            if (!sqlDataReader.IsDBNull(1))
                                detalleCedulon.concepto = sqlDataReader.GetString(1);

                            if (!sqlDataReader.IsDBNull(3))
                                suma = sqlDataReader.GetBoolean(3);

                            if (suma)
                            {
                                if (!sqlDataReader.IsDBNull(2))
                                    detalleCedulon.montoOriginal = sqlDataReader.GetDecimal(2);
                                detalleCedulon.descInteres = 0;

                                detalleCedulon.montoPagado = detalleCedulon.montoOriginal;
                                detalleCedulon.concepto += string.Format(" por {0} Años",
                                    sqlDataReader.GetInt32(4));
                            }
                            else
                            {
                                if (!sqlDataReader.IsDBNull(2))
                                    detalleCedulon.descInteres = sqlDataReader.GetDecimal(2);

                                detalleCedulon.montoOriginal = 0;
                                detalleCedulon.montoOriginal -= detalleCedulon.descInteres;
                            }
                            if (!sqlDataReader.IsDBNull(5))
                                detalleCedulon.nro_transaccion = sqlDataReader.GetInt32(5);

                            lst.Add(detalleCedulon);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

