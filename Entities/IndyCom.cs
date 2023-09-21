using System.Data;
using System.Data.SqlClient;

namespace WSCedulones.Entities
{
    public class IndyCom
    {
        public bool mExiste { get; set; }
        public bool mPlanPago { get; set; }
        public bool mProcuracion { get; set; }

        public int legajo { get; set; }
        public int nro_bad { get; set; }
        public string nombre { get; set; }
        public string des_com { get; set; }
        public string nom_fantasia { get; set; }
        public int cod_tipo_per { get; set; }
        public string cod_zona { get; set; }
        public int tipo_liquidacion { get; set; }
        public string des_tipo_liquidacion { get; set; }
        public string nro_ing_bruto { get; set; }
        public string nro_cuit { get; set; }
        public string fecha_alta { get; set; }
        public bool transporte { get; set; }
        public string nom_calle_dom_esp { get; set; }
        public string nro_dom_esp { get; set; }
        public string nom_barrio_dom_esp { get; set; }
        public string piso_dpto_esp { get; set; }
        public string ciudad_dom_esp { get; set; }
        public string cod_postal_dom_esp { get; set; }
        public string provincia_dom_esp { get; set; }
        public decimal monto_original { get; set; }
        public decimal debe { get; set; }
        public string ultimpo_periodo { get; set; }
        public string clave_pago { get; set; }
        public bool mNewRecord { get; set; }
        public string CUIT_VECINO_DIGITAL { get; set; }
        public int totalRows { get; set; }

        public IndyCom()
        {
            mExiste = true;
            mPlanPago = false;
            mProcuracion = false;

            legajo = 0;
            nro_bad = 0;
            nombre = "";
            nom_fantasia = "";
            cod_tipo_per = 0;
            cod_zona = "";
            tipo_liquidacion = 0;
            des_tipo_liquidacion = "";
            nro_ing_bruto = "";
            nro_cuit = "";
            fecha_alta = "";
            transporte = false;
            nom_barrio_dom_esp = "";
            nom_calle_dom_esp = "";
            nro_dom_esp = "";
            piso_dpto_esp = "";
            ciudad_dom_esp = "";
            cod_postal_dom_esp = "";
            provincia_dom_esp = "";
            monto_original = 0;
            debe = 0;
            ultimpo_periodo = "";
            clave_pago = "";
            mNewRecord = true;
            totalRows = 0;
            CUIT_VECINO_DIGITAL = string.Empty;
        }
        public static Entities.IndyCom GetIndycomByPk(int legajo)
        {
            Entities.IndyCom oIndycom = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;

            string sql = @"SELECT i.legajo, i.nro_bad, b.nombre, i.des_com, i.nom_fantasia,
                        i.cod_tipo_per, i.cod_zona, i.cod_zona_liquidacion, tli.descripcion_tipo_liq, i.tipo_liquidacion,
                        i.nro_ing_bruto, i.nro_cuit,
                        convert(varchar(8),i.fecha_alta,104) as fecha_alta, i.transporte,
                        i.nom_barrio_dom_esp, i.nom_calle_dom_esp,
                        i.nro_dom_esp, i.piso_dpto_esp,
                        i.ciudad_dom_esp, i.cod_postal_dom_esp,
                        i.provincia_dom_esp, i.per_ult as ultimo_periodo, i.clave_pago, i.cuit_vecino_digital
                        FROM Indycom i (nolock)
                        JOIN TIPOS_LIQ_IYC  tli on
                        i.tipo_liquidacion=tli.cod_tipo_liq
                        JOIN BADEC b on
                        i.nro_bad=b.nro_bad
                        WHERE i.legajo = @legajo";


            cmd = new SqlCommand();


            cmd.Parameters.Add(new SqlParameter("@legajo", legajo));

            try
            {
                cn = DALBase.GetConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oIndycom = new Entities.IndyCom();
                    if (!dr.IsDBNull(dr.GetOrdinal("legajo"))) { oIndycom.legajo = dr.GetInt32(dr.GetOrdinal("legajo")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) { oIndycom.nro_bad = dr.GetInt32(dr.GetOrdinal("nro_bad")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) { oIndycom.nombre = dr.GetString(dr.GetOrdinal("nombre")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_fantasia"))) { oIndycom.nom_fantasia = dr.GetString(dr.GetOrdinal("nom_fantasia")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_per"))) { oIndycom.cod_tipo_per = dr.GetInt32(dr.GetOrdinal("cod_tipo_per")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_zona"))) { oIndycom.cod_zona = dr.GetString(dr.GetOrdinal("cod_zona")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("tipo_liquidacion"))) { oIndycom.tipo_liquidacion = dr.GetInt32(dr.GetOrdinal("tipo_liquidacion")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("descripcion_tipo_liq"))) { oIndycom.des_tipo_liquidacion = dr.GetString(dr.GetOrdinal("descripcion_tipo_liq")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ing_bruto")))
                    { oIndycom.nro_ing_bruto = dr.GetString(dr.GetOrdinal("nro_ing_bruto")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cuit"))) { oIndycom.nro_cuit = Convert.ToString(dr.GetString(dr.GetOrdinal("nro_cuit"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta")))
                    { oIndycom.fecha_alta = dr.GetString(dr.GetOrdinal("fecha_alta")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("transporte"))) { oIndycom.transporte = dr.GetBoolean(dr.GetOrdinal("transporte")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_barrio_dom_esp"))) { oIndycom.nom_barrio_dom_esp = dr.GetString(dr.GetOrdinal("nom_barrio_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle_dom_esp"))) { oIndycom.nom_calle_dom_esp = dr.GetString(dr.GetOrdinal("nom_calle_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom_esp"))) { oIndycom.nro_dom_esp = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_dom_esp"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto_esp"))) { oIndycom.piso_dpto_esp = Convert.ToString(dr.GetString(dr.GetOrdinal("piso_dpto_esp"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("ciudad_dom_esp"))) { oIndycom.ciudad_dom_esp = Convert.ToString(dr.GetString(dr.GetOrdinal("ciudad_dom_esp"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia_dom_esp"))) { oIndycom.provincia_dom_esp = dr.GetString(dr.GetOrdinal("provincia_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal_dom_esp"))) { oIndycom.cod_postal_dom_esp = dr.GetString(dr.GetOrdinal("cod_postal_dom_esp")); }
                    oIndycom.monto_original = 0;
                    oIndycom.debe = 0;
                    if (!dr.IsDBNull(dr.GetOrdinal("ultimo_periodo"))) { oIndycom.ultimpo_periodo = dr.GetString(dr.GetOrdinal("ultimo_periodo")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("clave_pago"))) { oIndycom.clave_pago = dr.GetString(dr.GetOrdinal("clave_pago")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("CUIT_VECINO_DIGITAL"))) { oIndycom.CUIT_VECINO_DIGITAL = dr.GetString(dr.GetOrdinal("CUIT_VECINO_DIGITAL")); }
                    oIndycom.mExiste = true;
                    //oIndycom.mPlanPago = GetPlanPago(legajo);
                    //oIndycom.mProcuracion = GetProcuracion(legajo);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oIndycom;
        }
    }
}
