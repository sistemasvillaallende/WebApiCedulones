using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WSCedulones.Entities
{
    public class CEDULON_PRINT_CABECERA_CREDITO : DALBase
    {
        public int nroCedulon { get; set; }
        public string denominacion { get; set; }
        public string detalle { get; set; }
        public string nombre { get; set; }
        public DateTime vencimiento { get; set; }
        public decimal montoPagar { get; set; }
        public string CUIT { get; set; }
        public int legajo { get; set; }
        public string domicilio { get; set; }
        public int cantidadCuotas { get; set; }
        public decimal presupuesto { get; set; }
        public int cantidadCuotas { get; set; }
        public decimal presupuesto { get; set; }

        public CEDULON_PRINT_CABECERA_CREDITO()
        {
            nroCedulon = 0;
            denominacion = string.Empty;
            vencimiento = DateTime.Now;
            montoPagar = 0;
            detalle = string.Empty;
            nombre = string.Empty;
            CUIT = string.Empty;
            legajo = 0;
            domicilio = string.Empty;
            cantidadCuotas = 0;
            presupuesto = 0;
        }


        public static CEDULON_PRINT_CABECERA_CREDITO readCredito(long nroCedulon)
        {
            try
            {
                CEDULON_PRINT_CABECERA_CREDITO obj = null;
                string sql =
                @"SELECT 
                    nro_cedulon,  
                    i.id_credito_materiales, 
                    vencimiento_2, 
                    monto_2,
                    ISNULL(b.cuit, ' - ') AS CUIT,
                    b.APELLIDO + ', ' + b.NOMBRE AS NOMBRE, 
                    i.legajo, 
                    i.domicilio,
	                i.presupuesto,
	                i.cant_cuotas AS cantidadCuotas
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
                            obj = new CEDULON_PRINT_CABECERA_CREDITO();
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
                            if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                                obj.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));
                            if (!dr.IsDBNull(dr.GetOrdinal("domicilio")))
                                obj.domicilio = dr.GetString(dr.GetOrdinal("domicilio"));
                            if (!dr.IsDBNull(dr.GetOrdinal("presupuesto")))
                                obj.presupuesto = dr.GetDecimal(dr.GetOrdinal("presupuesto"));
                            if (!dr.IsDBNull(dr.GetOrdinal("cantidadCuotas")))
                                obj.cantidadCuotas = dr.GetInt32(dr.GetOrdinal("cantidadCuotas"));
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
