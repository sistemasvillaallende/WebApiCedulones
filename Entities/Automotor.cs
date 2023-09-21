using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WSCedulones.Entities
{
    public class Automotor
    {
        public bool mExiste { get; set; }
        public bool mPlanPago { get; set; }
        public bool mProcuracion { get; set; }

        public string dominio { get; set; }
        public int nro_bad { get; set; }
        public string nombre { get; set; }
        public string nom_barrio_dom_esp { get; set; }
        public string nro_dom_esp { get; set; }
        public string nom_calle_dom_esp { get; set; }
        public string piso_dpto_esp { get; set; }
        public string ciudad_dom_esp { get; set; }
        public string cod_postal_dom_esp { get; set; }
        public string provincia_dom_esp { get; set; }
        public string marca { get; set; }
        public string anio { get; set; }
        public decimal monto_original { get; set; }
        public decimal debe { get; set; }
        public string ultimpo_periodo { get; set; }
        public string clave_pago { get; set; }
        public bool mNewRecord { get; set; }
        public DateTime fecha_alta { get; set; }
        public string CUIT { get; set; }
        public string CUIT_VECINO_DIGITAL { get; set; }

        public bool baja { get; set; }

        public int cod_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string Des_tipo_documento { get; set; }
        public bool debito_automatico { get; set; }

        public Automotor()
        {
            mExiste = false;
            mPlanPago = false;
            mProcuracion = false;

            dominio = "";
            nro_bad = 0;
            nombre = "";
            nom_barrio_dom_esp = "";
            nro_dom_esp = "";
            nom_calle_dom_esp = "";
            piso_dpto_esp = "";
            ciudad_dom_esp = "";
            cod_postal_dom_esp = "";
            provincia_dom_esp = "";
            marca = "";
            anio = "";
            monto_original = 0;
            debe = 0;
            ultimpo_periodo = "";
            clave_pago = "";
            mNewRecord = false;
            baja = false;
            CUIT = string.Empty;
            CUIT_VECINO_DIGITAL = string.Empty;
            debito_automatico = false;
        }
        public static Entities.Automotor getAutoByPk(string dominio)
        {
            Entities.Automotor oAutomotor = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT v.*, b.nombre, td.Des_tipo_documento");
            strSQL.AppendLine("FROM vehiculos v (nolock)");
            strSQL.AppendLine("join badec b on v.nro_bad=b.nro_bad");
            strSQL.AppendLine("full join TIPOS_DOCUMENTOS td ON v.cod_tipo_documento = td.Cod_tipo_documento");
            strSQL.AppendLine("where dominio = @dominio");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@dominio", dominio));


            try
            {
                cn = DALBase.GetConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oAutomotor = new Entities.Automotor();
                    if (!dr.IsDBNull(dr.GetOrdinal("dominio"))) oAutomotor.dominio = dr.GetString((dr.GetOrdinal("dominio")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) oAutomotor.nro_bad = dr.GetInt32((dr.GetOrdinal("nro_bad")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) oAutomotor.nombre = dr.GetString((dr.GetOrdinal("nombre")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_barrio_dom_esp"))) oAutomotor.nom_barrio_dom_esp = dr.GetString((dr.GetOrdinal("nom_barrio_dom_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom_esp"))) oAutomotor.nro_dom_esp = Convert.ToString(dr.GetInt32((dr.GetOrdinal("nro_dom_esp"))));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle_dom_esp"))) oAutomotor.nom_calle_dom_esp = dr.GetString((dr.GetOrdinal("nom_calle_dom_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto_esp"))) oAutomotor.piso_dpto_esp = dr.GetString((dr.GetOrdinal("piso_dpto_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("ciudad_dom_esp"))) oAutomotor.ciudad_dom_esp = dr.GetString((dr.GetOrdinal("ciudad_dom_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia_dom_esp"))) oAutomotor.provincia_dom_esp = dr.GetString((dr.GetOrdinal("provincia_dom_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal_dom_esp"))) oAutomotor.cod_postal_dom_esp = dr.GetString((dr.GetOrdinal("cod_postal_dom_esp")));
                    if (!dr.IsDBNull(dr.GetOrdinal("marca"))) oAutomotor.marca = dr.GetString((dr.GetOrdinal("marca")));
                    if (!dr.IsDBNull(dr.GetOrdinal("anio"))) oAutomotor.anio = Convert.ToString(dr.GetInt32((dr.GetOrdinal("anio"))));
                    oAutomotor.monto_original = 0;
                    oAutomotor.debe = 0;
                    if (!dr.IsDBNull(dr.GetOrdinal("per_ult"))) oAutomotor.ultimpo_periodo = dr.GetString((dr.GetOrdinal("per_ult")));
                    if (!dr.IsDBNull(dr.GetOrdinal("clave_pago"))) oAutomotor.clave_pago = dr.GetString((dr.GetOrdinal("clave_pago")));

                    if (!dr.IsDBNull(dr.GetOrdinal("baja"))) oAutomotor.baja = Convert.ToBoolean(dr.GetBoolean((dr.GetOrdinal("baja"))));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento"))) oAutomotor.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_documento"))) oAutomotor.nro_documento = dr.GetString(dr.GetOrdinal("nro_documento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Des_tipo_documento"))) oAutomotor.Des_tipo_documento = dr.GetString(dr.GetOrdinal("Des_tipo_documento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CUIT"))) oAutomotor.CUIT = dr.GetString(dr.GetOrdinal("CUIT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUIT_VECINO_DIGITAL"))) oAutomotor.CUIT_VECINO_DIGITAL = dr.GetString(dr.GetOrdinal("CUIT_VECINO_DIGITAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FECHA_ALTA"))) oAutomotor.fecha_alta = dr.GetDateTime(dr.GetOrdinal("FECHA_ALTA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("debito_automatico"))) oAutomotor.debito_automatico = dr.GetBoolean(dr.GetOrdinal("debito_automatico"));
                    oAutomotor.mExiste = true;
                    //oAutomotor.mPlanPago = GetPlanPago(oAutomotor);
                    //oAutomotor.mProcuracion = GetProcuracion(oAutomotor);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oAutomotor;
        }
    }
}
