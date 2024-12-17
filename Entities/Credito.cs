using System.Data;
using System.Data.SqlClient;

namespace WSCedulones.Entities
{
    public class Credito
    {
        public bool mExiste { get; set; }
        public bool mPlanPago { get; set; }
        public bool mProcuracion { get; set; }

        public int id_credito { get; set; }
        public int nro_bad { get; set; }
        public string nombre { get; set; }
        public string nro_cuit { get; set; }
        public string fecha_alta { get; set; }
        public string nom_calle_dom_esp { get; set; }
        public string nro_dom_esp { get; set; }
        public string nom_barrio_dom_esp { get; set; }
        public string piso_dpto_esp { get; set; }
        public string ciudad_dom_esp { get; set; }
        public int cod_postal_dom_esp { get; set; }
        public string provincia_dom_esp { get; set; }
        public decimal monto_original { get; set; }
        public decimal debe { get; set; }
        public string ultimpo_periodo { get; set; }
        public bool mNewRecord { get; set; }
        public int totalRows { get; set; }

        public Credito()
        {
            mExiste = true;
            mPlanPago = false;
            mProcuracion = false;

            id_credito = 0;
            nro_bad = 0;
            nombre = "";
            nro_cuit = "";
            fecha_alta = "";
            nom_barrio_dom_esp = "";
            nom_calle_dom_esp = "";
            nro_dom_esp = "";
            piso_dpto_esp = "";
            ciudad_dom_esp = "";
            cod_postal_dom_esp = 0;
            provincia_dom_esp = "";
            monto_original = 0;
            debe = 0;
            ultimpo_periodo = "";
            mNewRecord = true;
            totalRows = 0;
        }
        public static Entities.Credito GetCreditoByPk(int id_credito_materiales)
        {
            Entities.Credito oCredito = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;

            string sql = @"		SELECT 
                                     ccm.id_credito_materiales, 
                                     b.nro_bad, 
                                     b.nombre, 
                                     b.CUIT,
                                     CONVERT(varchar(10), ccm.fecha_alta, 104) AS fecha_alta,
                                     b.NOMBRE_BARRIO, 
                                     b.NOMBRE_CALLE,
                                     b.LOCALIDAD, 
                                     b.COD_POSTAL,
                                     b.PROVINCIA, 
                                     ccm.per_ultimo AS ultimo_periodo
                                FROM CM_CREDITO_MATERIALES ccm (nolock)
                                JOIN BADEC b 
                                    ON ccm.cuit_solicitante = b.CUIT 
                                WHERE ccm.id_credito_materiales = @id_credito_materiales;";


            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@id_credito_materiales", id_credito_materiales));

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

                    oCredito = new Entities.Credito();

                    if (!dr.IsDBNull(dr.GetOrdinal("id_credito_materiales"))) { oCredito.id_credito = dr.GetInt32(dr.GetOrdinal("id_credito_materiales")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) { oCredito.nro_bad = dr.GetInt32(dr.GetOrdinal("nro_bad")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) { oCredito.nombre = dr.GetString(dr.GetOrdinal("nombre")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("CUIT"))) { oCredito.nro_cuit = dr.GetString(dr.GetOrdinal("CUIT")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta"))) { oCredito.fecha_alta = dr.GetString(dr.GetOrdinal("fecha_alta")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_BARRIO"))) { oCredito.nom_barrio_dom_esp = dr.GetString(dr.GetOrdinal("NOMBRE_BARRIO")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_CALLE"))) { oCredito.nom_calle_dom_esp = dr.GetString(dr.GetOrdinal("NOMBRE_CALLE")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("COD_POSTAL"))) { oCredito.cod_postal_dom_esp = dr.GetInt32(dr.GetOrdinal("COD_POSTAL")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA"))) { oCredito.provincia_dom_esp = dr.GetString(dr.GetOrdinal("PROVINCIA")); }
                    oCredito.monto_original = 0;
                    oCredito.debe = 0;
                    if (!dr.IsDBNull(dr.GetOrdinal("ultimo_periodo"))) { oCredito.ultimpo_periodo = dr.GetString(dr.GetOrdinal("ultimo_periodo")); }
                    oCredito.mExiste = true;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oCredito;
        }
    }
}
