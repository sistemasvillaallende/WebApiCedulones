using System.Data;
using System.Data.SqlClient;

namespace WSCedulones.Entities
{
    public class Inmuebles
    {
        public bool mExiste { get; set; }
        public bool mPlanPago { get; set; }
        public bool mProcuracion { get; set; }

        public int cir { get; set; }
        public int sec { get; set; }
        public int man { get; set; }
        public int par { get; set; }
        public int p_h { get; set; }
        public int nro_bad { get; set; }
        public string nombre { get; set; }
        public string nom_barrio_dom_esp { get; set; }
        public string nro_dom_esp { get; set; }
        public string nom_calle_dom_esp { get; set; }
        public string piso_dpto_esp { get; set; }
        public string ciudad_dom_esp { get; set; }
        public string cod_postal_dom_esp { get; set; }
        public string provincia_dom_esp { get; set; }
        public string nom_calle_pf { get; set; }
        public string nro_dom_pf { get; set; }
        public decimal monto_original { get; set; }
        public decimal debe { get; set; }
        public string ultimpo_periodo { get; set; }
        public string clave_pago { get; set; }
        public bool mNewRecord { get; set; }
        public string estado_inmueble { get; set; }
        public int totalRows { get; set; }
        public string barrio { get; set; }
        public string CUIT { get; set; }
        public string CUIT_VECINO_DIGITAL { get; set; }
        public int id_paypertic { get; set; }

        public Inmuebles()
        {
            mExiste = false;
            mPlanPago = false;
            mProcuracion = false;

            cir = 0;
            sec = 0;
            man = 0;
            par = 0;
            p_h = 0;
            nro_bad = 0;
            nombre = "";
            nom_barrio_dom_esp = "";
            nro_dom_esp = "";
            nom_calle_dom_esp = "";
            piso_dpto_esp = "";
            ciudad_dom_esp = "";
            cod_postal_dom_esp = "";
            provincia_dom_esp = "";
            nom_calle_pf = "";
            nro_dom_pf = "";
            monto_original = 0;
            debe = 0;
            ultimpo_periodo = "";
            clave_pago = "";
            mNewRecord = false;
            totalRows = 0;
            estado_inmueble = string.Empty;
            barrio = string.Empty;
            CUIT = string.Empty;
            CUIT_VECINO_DIGITAL = string.Empty;
        }
        public static Entities.Inmuebles getInmuebleByPk(int cir, int sec, int man, int par, int p_h)
        {
            Entities.Inmuebles oInmueble = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;

            string sql = @"SELECT i.*, c.nom_calle as nom_calle_pf, ba.NOM_BARRIO
                        FROM Inmuebles i (nolock)
                        RIGHT JOIN Calles  c on c.cod_calle = i.cod_calle_pf
                        RIGHT JOIN BARRIOS  ba on ba.COD_BARRIO = i.cod_barrio
                        WHERE (i.oculto=0 or i.oculto is null)
                        AND i.circunscripcion = @cir
                        AND i.seccion = @sec
                        AND i.manzana = @man
                        AND i.parcela = @par
                        AND i.p_h = @ph";

            cmd = new SqlCommand();


            cmd.Parameters.Add(new SqlParameter("@cir", cir));
            cmd.Parameters.Add(new SqlParameter("@sec", sec));
            cmd.Parameters.Add(new SqlParameter("@man", man));
            cmd.Parameters.Add(new SqlParameter("@par", par));
            cmd.Parameters.Add(new SqlParameter("@ph", p_h));

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
                    oInmueble = new Entities.Inmuebles();

                    if (!dr.IsDBNull(dr.GetOrdinal("circunscripcion"))) { oInmueble.cir = dr.GetInt32(dr.GetOrdinal("circunscripcion")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("seccion"))) { oInmueble.sec = dr.GetInt32(dr.GetOrdinal("seccion")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("manzana"))) { oInmueble.man = dr.GetInt32(dr.GetOrdinal("manzana")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("parcela"))) { oInmueble.par = dr.GetInt32(dr.GetOrdinal("parcela")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("p_h"))) { oInmueble.p_h = dr.GetInt32(dr.GetOrdinal("p_h")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) { oInmueble.nro_bad = dr.GetInt32(dr.GetOrdinal("nro_bad")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) { oInmueble.nombre = dr.GetString(dr.GetOrdinal("nombre")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_barrio_dom_esp"))) { oInmueble.nom_barrio_dom_esp = dr.GetString(dr.GetOrdinal("nom_barrio_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom_esp"))) { oInmueble.nro_dom_esp = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_dom_esp"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle_dom_esp"))) { oInmueble.nom_calle_dom_esp = dr.GetString(dr.GetOrdinal("nom_calle_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto_esp"))) { oInmueble.piso_dpto_esp = dr.GetString(dr.GetOrdinal("piso_dpto_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("ciudad_dom_esp"))) { oInmueble.ciudad_dom_esp = dr.GetString(dr.GetOrdinal("ciudad_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia_dom_esp"))) { oInmueble.provincia_dom_esp = dr.GetString(dr.GetOrdinal("provincia_dom_esp")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal"))) { oInmueble.cod_postal_dom_esp = dr.GetString(dr.GetOrdinal("cod_postal")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle_pf"))) { oInmueble.nom_calle_pf = dr.GetString(dr.GetOrdinal("nom_calle_pf")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom_pf"))) { oInmueble.nro_dom_pf = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_dom_pf"))); }
                    if (!dr.IsDBNull(dr.GetOrdinal("NOM_BARRIO"))) { oInmueble.barrio = dr.GetString(dr.GetOrdinal("NOM_BARRIO")); }
                    oInmueble.monto_original = 0;
                    oInmueble.debe = 0;
                    if (!dr.IsDBNull(dr.GetOrdinal("ultimo_periodo"))) { oInmueble.ultimpo_periodo = dr.GetString(dr.GetOrdinal("ultimo_periodo")); }
                    if (!dr.IsDBNull(dr.GetOrdinal("clave_pago"))) { oInmueble.clave_pago = dr.GetString(dr.GetOrdinal("clave_pago")); }

                    if (!dr.IsDBNull(dr.GetOrdinal("CUIl"))) oInmueble.CUIT = dr.GetString(dr.GetOrdinal("CUIl"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUIT_VECINO_DIGITAL"))) oInmueble.CUIT_VECINO_DIGITAL = dr.GetString(dr.GetOrdinal("CUIT_VECINO_DIGITAL"));
                    oInmueble.mExiste = true;
                    //oInmueble.mPlanPago = GetPlanPago(cir, sec, man, par, p_h);
                    //oInmueble.mProcuracion = GetProcuracion(cir, sec, man, par, p_h);



                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oInmueble;
        }
    }
}
