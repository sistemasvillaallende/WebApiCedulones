using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WSCedulones.Entities
{
    public class CEDULON_PRINT_CABECERA : DALBase
    {
        public int nroCedulon { get; set; }
        public string denominacion { get; set; }
        public string detalle { get; set; }
        public string nombre { get; set; }
        public DateTime vencimiento { get; set; }
        public decimal montoPagar { get; set; }
        public string CUIT { get; set; }
        public CEDULON_PRINT_CABECERA()
        {
            nroCedulon = 0;
            denominacion = string.Empty;
            vencimiento = DateTime.Now;
            montoPagar = 0;
            detalle = string.Empty;
            nombre = string.Empty;
            CUIT = string.Empty;
        }
        //OK
        public static CEDULON_PRINT_CABECERA readAuto(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT nro_cedulon, v.DOMINIO, v.MARCA, vencimiento_2, monto_2, v.nombre, ISNULL(v.CUIT, ' - ') as CUIT
                FROM CEDULONES2 c2 INNER JOIN VEHICULOS v ON c2.dominio = v.DOMINIO
                WHERE subsistema=4 AND (nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion = dr.GetString(dr.GetOrdinal("DOMINIO"));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            obj.detalle = dr.GetString(dr.GetOrdinal("MARCA"));
                            obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CEDULON_PRINT_CABECERA readAutoAnual(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                //SELECT nro_cedulon, v.DOMINIO, v.MARCA, vencimiento_1, monto_1, v.nombre, ISNULL(v.CUIT, ' - ') as CUIT
                //FROM CEDULONES2 c2 INNER JOIN VEHICULOS v ON c2.dominio = v.DOMINIO
                //WHERE subsistema=4 AND (nro_cedulon = @nroCedulon)
                SELECT nro_cedulon, v.DOMINIO, v.MARCA, 
                vencimiento = 
                CASE
                WHEN CAST(GETDATE() AS date) <= CAST(vencimiento_1 AS date) THEN vencimiento_1
                ELSE vencimiento_2
                END,
                monto=
                CASE
                WHEN CAST(GETDATE() AS date) <= CAST(vencimiento_1 AS date) THEN monto_1
                ELSE monto_2
                END,
                v.nombre, ISNULL(v.CUIT, ' - ') as CUIT
                FROM CEDULONES2 c2 INNER JOIN VEHICULOS v ON c2.dominio = v.DOMINIO
                WHERE subsistema=4 AND (nro_cedulon = @nroCedulon)";
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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion = dr.GetString(dr.GetOrdinal("DOMINIO"));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto"));
                            obj.detalle = dr.GetString(dr.GetOrdinal("MARCA"));
                            obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CEDULON_PRINT_CABECERA readCementerio(int nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT nro_cedulon, i.Tipo, i.Manzana, i.Lote, i.parcela,
                i.Nivel, vencimiento_2, monto_2, i.Nom_titular1 + ' - ' + i.Nom_titular2 AS NOMBRE, ISNULL(i.Cuit, ' - ') AS CUIT,
                c.NOM_CALLE + ' ' + CONVERT(VARCHAR(100), i.Nro_dom_esp) + ' - ' +  b.NOM_BARRIO AS direccion
                FROM CEDULONES2 c2 INNER JOIN CEMENTERIO i ON c2.tipo_cem = i.Tipo AND
                c2.manzana_cem = i.Manzana AND c2.lote_cem = i.Lote AND c2.parcela_cem = i.parcela AND c2.nivel_cem = i.Nivel
                FULL JOIN BARRIOS b ON i.Cod_barrio_dom_esp = b.COD_BARRIO
                FULL JOIN CALLES c ON c.COD_CALLE = i.Cod_calle_dom_esp
                WHERE subsistema=5 AND (nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            if (!dr.IsDBNull(dr.GetOrdinal("nro_cedulon")))
                                obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            if (!dr.IsDBNull(dr.GetOrdinal("Tipo")))
                                obj.denominacion = string.Format("Tipo: {0} - Manzana: {1} - Lote: {2} - Parcela: {3} - Nivel: {4}",
                                dr.GetString(dr.GetOrdinal("Tipo")), dr.GetInt32(dr.GetOrdinal("Manzana")),
                                dr.GetInt32(dr.GetOrdinal("Lote")), dr.GetInt32(dr.GetOrdinal("parcela")),
                                    dr.GetInt32(dr.GetOrdinal("Nivel")));
                            if (!dr.IsDBNull(dr.GetOrdinal("vencimiento_2")))
                                obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("monto_2")))
                                obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("direccion")))
                                obj.detalle = dr.GetString(dr.GetOrdinal("direccion"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                                obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CUIT")))
                                obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //OK
        public static CEDULON_PRINT_CABECERA readTasa(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT nro_cedulon, i.circunscripcion, i.seccion, i.manzana, i.parcela,
                i.p_h, vencimiento_2, monto_2, i.Nombre, ISNULL(i.cuil, ' - ') AS CUIT,
                c.NOM_CALLE + ' ' + CONVERT(VARCHAR(100), i.nro_dom_pf) + ' - ' +  b.NOM_BARRIO AS direccion
                FROM CEDULONES2 c2 INNER JOIN INMUEBLES i ON c2.circunscripcion = i.circunscripcion AND
                c2.seccion = i.seccion AND c2.manzana = i.manzana AND c2.parcela = i.parcela AND c2.p_h = i.p_h
                INNER JOIN BARRIOS b ON i.cod_barrio = b.COD_BARRIO
                INNER JOIN CALLES c ON c.COD_CALLE = i.cod_calle_pf
                WHERE subsistema=1 AND (nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion =
                                Utils.armoDenominacion(
                                    dr.GetInt32(dr.GetOrdinal("circunscripcion")),
                                    dr.GetInt32(dr.GetOrdinal("seccion")),
                                    dr.GetInt32(dr.GetOrdinal("manzana")),
                                    dr.GetInt32(dr.GetOrdinal("parcela")),
                                    dr.GetInt32(dr.GetOrdinal("p_h")));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            obj.detalle = dr.GetString(dr.GetOrdinal("direccion"));
                            obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //OK
        public static CEDULON_PRINT_CABECERA readTasaAnual(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT nro_cedulon, i.circunscripcion, i.seccion, i.manzana, i.parcela,
                i.p_h,
                vencimiento = 
                CASE
                WHEN CAST(GETDATE() AS date) <= CAST(vencimiento_1 AS date) THEN vencimiento_1
                ELSE vencimiento_2
                END,
                monto=
                CASE
                WHEN CAST(GETDATE() AS date) <= CAST(vencimiento_1 AS date) THEN monto_1
                ELSE monto_2
                END,
                i.Nombre, ISNULL(i.cuil, ' - ') AS CUIT,
                c.NOM_CALLE + ' ' + CONVERT(VARCHAR(100), i.nro_dom_pf) + ' - ' +  b.NOM_BARRIO AS direccion
                FROM CEDULONES2 c2 INNER JOIN INMUEBLES i ON c2.circunscripcion = i.circunscripcion AND
                c2.seccion = i.seccion AND c2.manzana = i.manzana AND c2.parcela = i.parcela AND c2.p_h = i.p_h
                INNER JOIN BARRIOS b ON i.cod_barrio = b.COD_BARRIO
                INNER JOIN CALLES c ON c.COD_CALLE = i.cod_calle_pf
                WHERE subsistema=1 AND (nro_cedulon = @nroCedulon)";


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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion =
                                Utils.armoDenominacion(
                                    dr.GetInt32(dr.GetOrdinal("circunscripcion")),
                                    dr.GetInt32(dr.GetOrdinal("seccion")),
                                    dr.GetInt32(dr.GetOrdinal("manzana")),
                                    dr.GetInt32(dr.GetOrdinal("parcela")),
                                    dr.GetInt32(dr.GetOrdinal("p_h")));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto"));
                            obj.detalle = dr.GetString(dr.GetOrdinal("direccion"));
                            obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //OK
        public static CEDULON_PRINT_CABECERA readIndyCom(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql =
                @"SELECT 
                    nro_cedulon, 
                    i.legajo, 
                    vencimiento_2, 
                    monto_2, 
                    i.nom_fantasia,
                    ISNULL(i.nro_cuit, ' - ') AS CUIT, 
                    b.NOMBRE
                FROM CEDULONES2 c2 
                    INNER JOIN INDYCOM i ON c2.legajo = i.legajo
                    INNER JOIN VECINO_DIGITAL b ON i.nro_cuit = b.CUIT
                WHERE c2.subsistema=3 AND (nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            if (!dr.IsDBNull(dr.GetOrdinal("nro_cedulon")))
                                obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                                obj.denominacion = dr.GetInt32(dr.GetOrdinal("legajo")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("vencimiento_2")))
                                obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("monto_2")))
                                obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nom_fantasia")))
                                obj.detalle = dr.GetString(dr.GetOrdinal("nom_fantasia"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                                obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CUIT")))
                                obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //OK
        public static CEDULON_PRINT_CABECERA readFacturacion(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT c.nro_cedulon,
                c.vencimiento_2, c.monto_2, F.NOMBRE, ISNULL(F.CUIT, ' - ') AS CUIT, cat.des_categoria, f.observaciones
                FROM CEDULONES2 c
                join DEUDAS_X_CEDULON3 B ON B.nro_cedulon = C.nro_cedulon
                JOIN FACTURACION F ON B.nro_transaccion = F.nro_transaccion
                JOIN CATE_DEUDA_FACTU cat ON cat.cod_categoria = f.categoria_deuda
                WHERE c.subsistema=2 AND (c.nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));

                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            //obj.denominacion = dr.GetString(dr.GetOrdinal("des_categoria"));
                            obj.denominacion = "LICENCIA DE CONDUCIR";
                            obj.detalle = dr.GetString(dr.GetOrdinal("observaciones"));
                            obj.nombre = dr.GetString(dr.GetOrdinal("NOMBRE"));
                            obj.CUIT = dr.GetInt64(dr.GetOrdinal("CUIT")).ToString();
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //OK
        public static CEDULON_PRINT_CABECERA readMultaTasa(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
        string sql = @"
                SELECT nro_cedulon, c.circunscripcion, c.seccion, c.manzana, c.parcela,
                c.p_h, c.vencimiento_2, c.monto_2, b.NOMBRE, ISNULL(B.CUIT, ' - ') AS CUIT
                FROM CEDULONES2 c
                join Badec b on
                c.nro_badec = b.nro_bad
                WHERE c.tipo_cedulon=5 AND c.subsistema=6 AND (c.nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion =
                                Utils.armoDenominacion(
                                    dr.GetInt32(dr.GetOrdinal("circunscripcion")),
                                    dr.GetInt32(dr.GetOrdinal("seccion")),
                                    dr.GetInt32(dr.GetOrdinal("manzana")),
                                    dr.GetInt32(dr.GetOrdinal("parcela")),
                                    dr.GetInt32(dr.GetOrdinal("p_h")));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));

                            obj.nombre = dr.GetString(dr.GetOrdinal("NOMBRE"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CEDULON_PRINT_CABECERA readMulta(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql = @"
                SELECT DISTINCT c.nro_cedulon, A.CLAVE_SUBSISTEMA AS DOMINIO,
                c.vencimiento_2, c.monto_2, b.NOMBRE, ISNULL(B.CUIT, ' - ') AS CUIT
                FROM CEDULONES2 c
                join Badec b on
                c.nro_badec = b.nro_bad
                INNER JOIN SUMARIOS A ON A.DOMINIO=c.dominio
                WHERE c.tipo_cedulon=5 AND c.subsistema=6 AND (c.nro_cedulon = @nroCedulon)";

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
                            obj = new CEDULON_PRINT_CABECERA();
                            obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            obj.denominacion = dr.GetString(dr.GetOrdinal("dominio"));
                            obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));

                            obj.nombre = dr.GetString(dr.GetOrdinal("NOMBRE"));
                            obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


                public static CEDULON_PRINT_CABECERA readCredito(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA obj = null;
                string sql =
                    @"SELECT 
                    nro_cedulon,  
                    i.id_credito_materiales, 
                    vencimiento_2, 
                    monto_2,
                    ISNULL(b.cuit, ' - ') AS CUIT,
                    b.APELLIDO + ', ' + b.NOMBRE AS NOMBRE, 
                    i.legajo, 
                    i.domicilio
                FROM CEDULONES2 c2 
                    INNER JOIN CM_CREDITO_MATERIALES i ON c2.id_credito = i.id_credito_materiales 
                    INNER JOIN VECINO_DIGITAL b ON i.cuit_solicitante = b.CUIT
                WHERE c2.subsistema=7 AND (nro_cedulon = @nroCedulon)";


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
                            obj = new CEDULON_PRINT_CABECERA();
                            if (!dr.IsDBNull(dr.GetOrdinal("nro_cedulon")))
                                obj.nroCedulon = dr.GetInt32(dr.GetOrdinal("nro_cedulon"));
                            if (!dr.IsDBNull(dr.GetOrdinal("id_credito_materiales")))
                                obj.denominacion = dr.GetInt32(dr.GetOrdinal("id_credito_materiales")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("vencimiento_2")))
                                obj.vencimiento = dr.GetDateTime(dr.GetOrdinal("vencimiento_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("monto_2")))
                                obj.montoPagar = dr.GetDecimal(dr.GetOrdinal("monto_2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                                obj.nombre = dr.GetString(dr.GetOrdinal("nombre"));
                            if (!dr.IsDBNull(dr.GetOrdinal("CUIT")))
                                obj.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
