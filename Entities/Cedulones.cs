using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WSCedulones.Entities
{
    public class Cedulones
    {
        public long nro_cedulon { get; set; }
        public string fecha_emision { get; set; }
        public int subsistema { get; set; }
        public int tipo_cedulon { get; set; }
        public bool no_pagado { get; set; }
        public bool activo { get; set; }
        public int nro_emision { get; set; }
        public int? circunscripcion { get; set; }
        public int? seccion { get; set; }
        public int? manzana { get; set; }
        public int? parcela { get; set; }
        public int? p_h { get; set; }
        public string periodo { get; set; }
        public string? vencimiento_1 { get; set; }
        public decimal monto_1 { get; set; }
        public string? vencimiento_2 { get; set; }
        public decimal monto_2 { get; set; }
        public decimal contado { get; set; }
        public decimal cheques { get; set; }
        public decimal monto_arreglo { get; set; }
        public string? nro_decreto { get; set; }
        public string nro_dom_esp { get; set; }
        public string piso_dpto_esp { get; set; }
        public string local_esp { get; set; }
        public string nom_calle_dom_esp { get; set; }
        public string nom_barrio_dom_esp { get; set; }
        public string ciudad_dom_esp { get; set; }
        public string provincia_dom_esp { get; set; }
        public string pais_dom_esp { get; set; }
        public string codigo_postal_dom_esp { get; set; }
        public int nro_badec { get; set; }
        public int nro_contrib { get; set; }
        public string nom_badec { get; set; }
        public string nom_calle_pf { get; set; }
        public string nro_dom_pf { get; set; }
        public string? dominio { get; set; }
        public int? legajo { get; set; }
        public bool imprime { get; set; }
        public string? tipo_cem { get; set; }
        public int? manzana_cem { get; set; }
        public int? lote_cem { get; set; }
        public int? parcela_cem { get; set; }
        public int? nivel_cem { get; set; }
        public int? nro_causa { get; set; }
        public int? anio { get; set; }
        public int? nro_procuracion { get; set; }
        public bool mNewRecord { get; set; }
        public int? id_credito { get; set; }

        public List<Entities.VCtasctes> lstDeuda;

        public Cedulones()
        {
            nro_cedulon = 0;
            fecha_emision = "";
            subsistema = 0;
            tipo_cedulon = 0;
            no_pagado = false;
            activo = false;
            nro_emision = 0;
            circunscripcion = 0;
            seccion = 0;
            manzana = 0;
            parcela = 0;
            p_h = 0;
            periodo = "";
            vencimiento_1 = "";
            monto_1 = 0;
            vencimiento_2 = "";
            monto_2 = 0;
            contado = 0;
            cheques = 0;
            monto_arreglo = 0;
            nro_decreto = "";
            nro_dom_esp = "";
            piso_dpto_esp = "";
            local_esp = "";
            nom_calle_dom_esp = "";
            nom_barrio_dom_esp = "";
            ciudad_dom_esp = "";
            provincia_dom_esp = "";
            pais_dom_esp = "";
            codigo_postal_dom_esp = "";
            nro_badec = 0;
            nro_contrib = 0;
            nom_badec = "";
            nom_calle_pf = "";
            nro_dom_pf = "";
            dominio = "";
            legajo = 0;
            imprime = false;
            tipo_cem = "";
            manzana_cem = 0;
            lote_cem = 0;
            parcela_cem = 0;
            nivel_cem = 0;
            mNewRecord = false;
            lstDeuda = new List<VCtasctes>();
            nro_causa = 0;
            anio = 0;
            nro_procuracion = 0;
            id_credito = 0;
        }
        public static long insert(Entities.Cedulones oCedulon, int nroProcuracion)
        {
            long ret = 0;

            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();
            SqlTransaction tran = null;

            string fecha = "";
            try
            {
                cn = DALBase.GetConnection();
                cmd.Connection = cn;
                cmd.Connection.Open();
                tran = cn.BeginTransaction();
                cmd.Transaction = tran;

                oCedulon.nro_cedulon = DALBase.NewID("Cedulones2", "nro_cedulon", cn, tran);

                strSQL.AppendLine("INSERT INTO Cedulones2(");
                strSQL.AppendLine("nro_cedulon,");
                strSQL.AppendLine("fecha_emision,");
                strSQL.AppendLine("subsistema,");
                strSQL.AppendLine("tipo_cedulon,");
                strSQL.AppendLine("no_pagado,");
                strSQL.AppendLine("activo,");
                strSQL.AppendLine("nro_emision,");
                strSQL.AppendLine("circunscripcion,");
                strSQL.AppendLine("seccion,");
                strSQL.AppendLine("manzana,");
                strSQL.AppendLine("parcela,");
                strSQL.AppendLine("p_h,");
                strSQL.AppendLine("periodo,");
                //strSQL.AppendLine("vencimiento_1,");
                strSQL.AppendLine("monto_1,");
                strSQL.AppendLine("vencimiento_2,");
                strSQL.AppendLine("monto_2,");
                strSQL.AppendLine("contado,");
                strSQL.AppendLine("cheques,");
                strSQL.AppendLine("monto_arreglo,");
                strSQL.AppendLine("nro_dom_esp,");
                strSQL.AppendLine("piso_dpto_esp,");
                strSQL.AppendLine("local_esp,");
                strSQL.AppendLine("nom_calle_dom_esp,");
                strSQL.AppendLine("nom_barrio_dom_esp,");
                strSQL.AppendLine("ciudad_dom_esp,");
                strSQL.AppendLine("provincia_dom_esp,");
                strSQL.AppendLine("pais_dom_esp,");//, o.pais_dom_esp);
                strSQL.AppendLine("codigo_postal_dom_esp,");//;, o.codigo_postal_dom_esp);
                strSQL.AppendLine("nro_badec,");//, o.nro_badec);
                strSQL.AppendLine("nro_contrib,");//, o.nro_contrib);
                strSQL.AppendLine("nom_badec,");//, o.nom_badec);
                strSQL.AppendLine("nom_calle_pf,");//, o.nom_calle_pf);
                strSQL.AppendLine("nro_dom_pf,");//, o.nro_dom_pf);
                strSQL.AppendLine("dominio,");//, o.dominio);
                strSQL.AppendLine("legajo,");//, o.legajo);
                strSQL.AppendLine("imprime,");//, o.imprime);
                if (oCedulon.tipo_cem != string.Empty)
                    strSQL.AppendLine("tipo_cem,");//, o.tipo_cem);
                strSQL.AppendLine("manzana_cem,");//, o.manzana_cem);
                strSQL.AppendLine("lote_cem,");//, o.lote_cem);
                strSQL.AppendLine("parcela_cem,");//, o.parcela_cem);
                strSQL.AppendLine("nivel_cem,");//, o.nivel_cem);
                strSQL.AppendLine("nro_procuracion,");//, o.nivel_cem);
                strSQL.AppendLine("id_credito)");//, o.nivel_cem);
                strSQL.AppendLine("VALUES(");

                strSQL.AppendLine("@nro_cedulon,");
                strSQL.AppendLine("GETDATE(),");
                strSQL.AppendLine("@subsistema,");
                strSQL.AppendLine("@tipo_cedulon,");
                strSQL.AppendLine("@no_pagado,");
                strSQL.AppendLine("@activo,");
                strSQL.AppendLine("@nro_emision,");
                strSQL.AppendLine("@circunscripcion,");
                strSQL.AppendLine("@seccion,");
                strSQL.AppendLine("@manzana,");
                strSQL.AppendLine("@parcela,");
                strSQL.AppendLine("@p_h,");
                strSQL.AppendLine("@periodo,");
                //strSQL.AppendLine("@vencimiento_1,");
                strSQL.AppendLine("@monto_1,");
                //if (Convert.ToDateTime(oCedulon.vencimiento_2) < DateTime.Now)
                //    strSQL.AppendLine("@vencimiento_2,");
                //else
                strSQL.AppendLine("DATEADD(dd,10,GETDATE()),");
                strSQL.AppendLine("@monto_2,");
                strSQL.AppendLine("@contado,");
                strSQL.AppendLine("@cheques,");
                strSQL.AppendLine("@monto_arreglo,");
                strSQL.AppendLine("@nro_dom_esp,");
                strSQL.AppendLine("@piso_dpto_esp,");
                strSQL.AppendLine("@local_esp,");
                strSQL.AppendLine("@nom_calle_dom_esp,");
                strSQL.AppendLine("@nom_barrio_dom_esp,");
                strSQL.AppendLine("@ciudad_dom_esp,");
                strSQL.AppendLine("@provincia_dom_esp,");
                strSQL.AppendLine("@pais_dom_esp,");//, o.pais_dom_esp);
                strSQL.AppendLine("@codigo_postal_dom_esp,");//;, o.codigo_postal_dom_esp);
                strSQL.AppendLine("@nro_badec,");//, o.nro_badec);
                strSQL.AppendLine("@nro_contrib,");//, o.nro_contrib);
                strSQL.AppendLine("@nom_badec,");//, o.nom_badec);
                strSQL.AppendLine("@nom_calle_pf,");//, o.nom_calle_pf);
                strSQL.AppendLine("@nro_dom_pf,");//, o.nro_dom_pf);
                strSQL.AppendLine("@dominio,");//, o.dominio);
                strSQL.AppendLine("@legajo,");//, o.legajo);
                strSQL.AppendLine("@imprime,");//, o.imprime);
                if (oCedulon.tipo_cem != string.Empty)
                    strSQL.AppendLine("@tipo_cem,");//, o.tipo_cem);
                strSQL.AppendLine("@manzana_cem,");//, o.manzana_cem);
                strSQL.AppendLine("@lote_cem,");//, o.lote_cem);
                strSQL.AppendLine("@parcela_cem,");//, o.parcela_cem);
                strSQL.AppendLine("@nivel_cem,");//, o.nivel_cem);
                strSQL.AppendLine("@nro_procuracion,");//, o.nivel_cem);
                strSQL.AppendLine("@id_credito)");//, o.nivel_cem);
                cmd.Parameters.Add(new SqlParameter("@nro_cedulon", oCedulon.nro_cedulon));
                //cmd.Parameters.Add(new SqlParameter("@fecha_emision", oCedulon.fecha_emision));
                cmd.Parameters.Add(new SqlParameter("@subsistema", oCedulon.subsistema));
                cmd.Parameters.Add(new SqlParameter("@tipo_cedulon", oCedulon.tipo_cedulon));
                cmd.Parameters.Add(new SqlParameter("@no_pagado", oCedulon.no_pagado));
                cmd.Parameters.Add(new SqlParameter("@activo", oCedulon.activo));
                cmd.Parameters.Add(new SqlParameter("@nro_emision", oCedulon.nro_emision));
                cmd.Parameters.Add(new SqlParameter("@circunscripcion", oCedulon.circunscripcion));
                cmd.Parameters.Add(new SqlParameter("@seccion", oCedulon.seccion));
                cmd.Parameters.Add(new SqlParameter("@manzana", oCedulon.manzana));
                cmd.Parameters.Add(new SqlParameter("@parcela", oCedulon.parcela));
                cmd.Parameters.Add(new SqlParameter("@p_h", oCedulon.p_h));
                cmd.Parameters.Add(new SqlParameter("@periodo", oCedulon.periodo));
                // cmd.Parameters.Add(new SqlParameter("@vencimiento_1", oCedulon.vencimiento_1));
                cmd.Parameters.Add(new SqlParameter("@monto_1", oCedulon.monto_1));
                //if (Convert.ToDateTime(oCedulon.vencimiento_2) < DateTime.Now)
                //    cmd.Parameters.Add(new SqlParameter("@vencimiento_2", oCedulon.vencimiento_2));
                cmd.Parameters.Add(new SqlParameter("@monto_2", oCedulon.monto_2));
                cmd.Parameters.Add(new SqlParameter("@contado", oCedulon.contado));
                cmd.Parameters.Add(new SqlParameter("@cheques", oCedulon.cheques));
                cmd.Parameters.Add(new SqlParameter("@monto_arreglo", oCedulon.monto_arreglo));
                cmd.Parameters.Add(new SqlParameter("@nro_dom_esp", oCedulon.nro_dom_esp));
                cmd.Parameters.Add(new SqlParameter("@piso_dpto_esp", oCedulon.piso_dpto_esp));
                cmd.Parameters.Add(new SqlParameter("@local_esp", oCedulon.local_esp));
                cmd.Parameters.Add(new SqlParameter("@nom_calle_dom_esp", oCedulon.nom_calle_dom_esp));
                cmd.Parameters.Add(new SqlParameter("@nom_barrio_dom_esp", oCedulon.nom_barrio_dom_esp));
                cmd.Parameters.Add(new SqlParameter("@ciudad_dom_esp", oCedulon.ciudad_dom_esp));
                cmd.Parameters.Add(new SqlParameter("@provincia_dom_esp", oCedulon.provincia_dom_esp));
                cmd.Parameters.Add(new SqlParameter("@pais_dom_esp", oCedulon.pais_dom_esp));//, o.pais_dom_esp);
                cmd.Parameters.Add(new SqlParameter("@codigo_postal_dom_esp", oCedulon.codigo_postal_dom_esp));//;, o.codigo_postal_dom_esp);
                cmd.Parameters.Add(new SqlParameter("@nro_badec", oCedulon.nro_badec));//, o.nro_badec);
                cmd.Parameters.Add(new SqlParameter("@nro_contrib", oCedulon.nro_contrib));//, o.nro_contrib);
                if (oCedulon.nom_badec.Length > 40)
                    cmd.Parameters.Add(new SqlParameter("@nom_badec", oCedulon.nom_badec.Substring(0, 38)));//, o.nom_badec);
                else
                    cmd.Parameters.Add(new SqlParameter("@nom_badec", oCedulon.nom_badec));//, o.nom_badec);
                cmd.Parameters.Add(new SqlParameter("@nom_calle_pf", oCedulon.nom_calle_pf));//, o.nom_calle_pf);
                cmd.Parameters.Add(new SqlParameter("@nro_dom_pf", oCedulon.nro_dom_pf));//, o.nro_dom_pf);
                cmd.Parameters.Add(new SqlParameter("@dominio", oCedulon.dominio));//, o.dominio);
                cmd.Parameters.Add(new SqlParameter("@legajo", oCedulon.legajo));//, o.legajo);
                cmd.Parameters.Add(new SqlParameter("@imprime", oCedulon.imprime));//, o.imprime);
                if (oCedulon.tipo_cem != string.Empty)
                    cmd.Parameters.Add(new SqlParameter("@tipo_cem", oCedulon.tipo_cem));//, o.tipo_cem);
                cmd.Parameters.Add(new SqlParameter("@manzana_cem", oCedulon.manzana_cem));//, o.manzana_cem);
                cmd.Parameters.Add(new SqlParameter("@lote_cem", oCedulon.lote_cem));//, o.lote_cem);
                cmd.Parameters.Add(new SqlParameter("@parcela_cem", oCedulon.parcela_cem));//, o.parcela_cem);
                cmd.Parameters.Add(new SqlParameter("@nivel_cem", oCedulon.nivel_cem));//, o.nivel_cem);
                cmd.Parameters.Add(new SqlParameter("@nro_procuracion", nroProcuracion));//, o.nivel_cem);
                cmd.Parameters.Add(new SqlParameter("@id_credito", oCedulon.id_credito));//, o.nivel_cem);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                //tran = cn.BeginTransaction();
                //cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                int descuento = 0;

                cmd.Parameters.Add(new SqlParameter("@nro_transaccion", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@monto_pagado", SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@descuento_anticipo", descuento));
                cmd.Parameters.Add(new SqlParameter("@vencimiento_transaccion", SqlDbType.VarChar));
                //cmd.Parameters.Add(new SqlParameter("@pago_parcial", false));
                cmd.Parameters.Add(new SqlParameter("@categoria_deuda", SqlDbType.Int));
                for (int i = 0; i < oCedulon.lstDeuda.Count; i++)
                {

                    strSQL.Clear();
                    strSQL.AppendLine("INSERT INTO Deudas_x_cedulon3(");
                    strSQL.AppendLine("nro_cedulon,");
                    strSQL.AppendLine("nro_transaccion,");
                    strSQL.AppendLine("monto_pagado,");
                    strSQL.AppendLine("descuento_anticipo,");
                    strSQL.AppendLine("vencimiento_transaccion,");
                    strSQL.AppendLine("pago_parcial,");
                    strSQL.AppendLine("categoria_deuda)");
                    strSQL.AppendLine("VALUES(");
                    strSQL.AppendLine("@nro_cedulon,");
                    strSQL.AppendLine("@nro_transaccion,");
                    strSQL.AppendLine("@monto_pagado,");
                    strSQL.AppendLine("@descuento_anticipo,");
                    //strSQL.AppendLine("@vencimiento_transaccion,");
                    strSQL.AppendLine("DATEADD(dd,5,GETDATE()),");
                    strSQL.AppendLine("0,");
                    strSQL.AppendLine("@categoria_deuda)");

                    fecha = oCedulon.lstDeuda[i].fecha_vencimiento;

                    cmd.Parameters["@nro_transaccion"].Value = oCedulon.lstDeuda[i].nro_transaccion;
                    cmd.Parameters["@monto_pagado"].Value = oCedulon.lstDeuda[i].importe;
                    cmd.Parameters["@vencimiento_transaccion"].Value = oCedulon.lstDeuda[i].fecha_vencimiento;
                    cmd.Parameters["@categoria_deuda"].Value = oCedulon.lstDeuda[i].categoria_deuda;
                    cmd.CommandText = strSQL.ToString();
                    cmd.ExecuteNonQuery();

                    if (nroProcuracion > 0)
                    {
                        strSQL.Clear();
                        strSQL.AppendLine("INSERT INTO PROCURACION_X_CEDULONES");
                        strSQL.AppendLine("(nro_cedulon, nro_procuracion, nro_transaccion, subsistema,");
                        strSQL.AppendLine("monto_pagado, descuento_anticipo, categoria_deuda,");
                        strSQL.AppendLine("vencimiento_transaccion, pagado, usuario)");
                        strSQL.AppendLine("values");
                        strSQL.AppendLine("(@nro_cedulon, @nro_procuracion, @nro_transaccion, @subsistema,");
                        //strSQL.AppendLine("@monto_pagado, @descuento_anticipo, @categoria_deuda, @vencimiento_transaccion,");
                        strSQL.AppendLine("@monto_pagado, @descuento_anticipo, @categoria_deuda, DATEADD(dd,5,GETDATE()),");
                        strSQL.AppendLine("@pagado, @usuario)");

                        cmd.CommandText = strSQL.ToString();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@nro_cedulon", oCedulon.nro_cedulon);
                        cmd.Parameters.AddWithValue("@nro_procuracion", nroProcuracion);
                        cmd.Parameters.AddWithValue("@nro_transaccion",
                            oCedulon.lstDeuda[i].nro_transaccion);
                        cmd.Parameters.AddWithValue("@subsistema", oCedulon.subsistema);
                        cmd.Parameters.AddWithValue("@monto_pagado",
                            oCedulon.lstDeuda[i].importe);
                        cmd.Parameters.AddWithValue("@descuento_anticipo", 0);
                        cmd.Parameters.AddWithValue("@categoria_deuda",
                            oCedulon.lstDeuda[i].categoria_deuda);
                        cmd.Parameters.AddWithValue("@vencimiento_transaccion", oCedulon.vencimiento_2);
                        cmd.Parameters.AddWithValue("@pagado", 0);
                        cmd.Parameters.AddWithValue("@usuario", 7);

                        cmd.ExecuteNonQuery();
                    }
                }
                tran.Commit();
                ret = oCedulon.nro_cedulon;
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw new Exception(e.Message);

            }
            finally { cn.Close(); }
            return ret;
        }
        public static long InsertCedulon(Entities.Cedulones oCedulon, int nroProcuracion, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine(@"INSERT INTO Cedulones2 ( 
                  nro_cedulon, fecha_emision, subsistema, tipo_cedulon, no_pagado, activo, nro_emision, circunscripcion, seccion, 
                  manzana, parcela, p_h, periodo, vencimiento_1, monto_1, vencimiento_2, monto_2, contado, cheques, monto_arreglo,
                  nro_decreto, nro_dom_esp, piso_dpto_esp, local_esp, nom_calle_dom_esp, nom_barrio_dom_esp, ciudad_dom_esp, provincia_dom_esp, 
                  pais_dom_esp, codigo_postal_dom_esp, nro_badec, nro_contrib, nom_badec, nom_calle_pf, nro_dom_pf, dominio, 
                  legajo, imprime, tipo_cem, manzana_cem, lote_cem, parcela_cem, nivel_cem, nro_causa, anio, nro_procuracion, id_credito)
                VALUES(
                  @nro_cedulon, @fecha_emision, @subsistema, @tipo_cedulon, @no_pagado, @activo, @nro_emision, @circunscripcion, 
                  @seccion, @manzana, @parcela, @p_h, @periodo, @vencimiento_1, @monto_1, @vencimiento_2, @monto_2, 
                  @contado, @cheques, @monto_arreglo, @nro_decreto, @nro_dom_esp, @piso_dpto_esp, @local_esp, @nom_calle_dom_esp, 
                  @nom_barrio_dom_esp, @ciudad_dom_esp, @provincia_dom_esp, @pais_dom_esp, @codigo_postal_dom_esp, @nro_badec, 
                  @nro_contrib, @nom_badec, @nom_calle_pf, @nro_dom_pf, @dominio, @legajo, @imprime, @tipo_cem, @manzana_cem, 
                  @lote_cem, @parcela_cem, @nivel_cem, @nro_causa, @anio, @nro_procuracion, @id_credito)");

                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                oCedulon.nro_cedulon = DALBase.NewID("Cedulones2", "nro_cedulon", cn, trx);
                cmd.Parameters.AddWithValue("@nro_cedulon", oCedulon.nro_cedulon);
                cmd.Parameters.AddWithValue("@fecha_emision", oCedulon.fecha_emision);
                cmd.Parameters.AddWithValue("@subsistema", oCedulon.subsistema);
                cmd.Parameters.AddWithValue("@tipo_cedulon", oCedulon.tipo_cedulon);
                cmd.Parameters.AddWithValue("@no_pagado", oCedulon.no_pagado);
                cmd.Parameters.AddWithValue("@activo", oCedulon.activo);
                cmd.Parameters.AddWithValue("@nro_emision", oCedulon.nro_emision);
                cmd.Parameters.AddWithValue("@circunscripcion", oCedulon.circunscripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@seccion", oCedulon.seccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@manzana", oCedulon.manzana ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@parcela", oCedulon.parcela ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@p_h", oCedulon.p_h ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@periodo", oCedulon.periodo);
                cmd.Parameters.AddWithValue("@vencimiento_1", oCedulon.vencimiento_1 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@monto_1", oCedulon.monto_1);
                cmd.Parameters.AddWithValue("@vencimiento_2", oCedulon.vencimiento_2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@monto_2", oCedulon.monto_2);
                cmd.Parameters.AddWithValue("@contado", oCedulon.contado);
                cmd.Parameters.AddWithValue("@cheques", oCedulon.cheques);
                cmd.Parameters.AddWithValue("@monto_arreglo", oCedulon.monto_arreglo);
                cmd.Parameters.AddWithValue("@nro_decreto", oCedulon.nro_decreto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nro_dom_esp", oCedulon.nro_dom_esp);
                cmd.Parameters.AddWithValue("@piso_dpto_esp", oCedulon.piso_dpto_esp);
                cmd.Parameters.AddWithValue("@local_esp", oCedulon.local_esp);
                cmd.Parameters.AddWithValue("@nom_calle_dom_esp", oCedulon.nom_calle_dom_esp);
                cmd.Parameters.AddWithValue("@nom_barrio_dom_esp", oCedulon.nom_barrio_dom_esp);
                cmd.Parameters.AddWithValue("@ciudad_dom_esp", oCedulon.ciudad_dom_esp);
                cmd.Parameters.AddWithValue("@provincia_dom_esp", oCedulon.provincia_dom_esp);
                cmd.Parameters.AddWithValue("@pais_dom_esp", oCedulon.pais_dom_esp);
                cmd.Parameters.AddWithValue("@codigo_postal_dom_esp", oCedulon.codigo_postal_dom_esp);
                cmd.Parameters.AddWithValue("@nro_badec", oCedulon.nro_badec);
                cmd.Parameters.AddWithValue("@nro_contrib", oCedulon.nro_contrib);
                if (oCedulon.nom_badec.Length > 40)
                    cmd.Parameters.AddWithValue("@nom_badec", oCedulon.nom_badec.Substring(0, 38));
                else
                    cmd.Parameters.AddWithValue("@nom_badec", oCedulon.nom_badec); ;
                cmd.Parameters.AddWithValue("@nom_calle_pf", oCedulon.nom_calle_pf);
                cmd.Parameters.AddWithValue("@nro_dom_pf", oCedulon.nro_dom_pf);
                cmd.Parameters.AddWithValue("@dominio", oCedulon.dominio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@legajo", oCedulon.legajo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imprime", oCedulon.imprime);
                cmd.Parameters.AddWithValue("@tipo_cem", oCedulon.tipo_cem ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@manzana_cem", oCedulon.manzana_cem ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@lote_cem", oCedulon.lote_cem ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@parcela_cem", oCedulon.parcela_cem ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nivel_cem", oCedulon.nivel_cem ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nro_causa", oCedulon.nro_causa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@anio", oCedulon.anio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nro_procuracion", oCedulon.nro_procuracion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id_credito", oCedulon.id_credito ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                InsertDetalleCedulon(oCedulon, cn, trx);
                if (nroProcuracion > 0)
                    InsertProcuracion(oCedulon, nroProcuracion, cn, trx);
                return oCedulon.nro_cedulon;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw new Exception(e.Message);
            }

        }
        public static void InsertDetalleCedulon(Entities.Cedulones oCedulon, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                string strSQL = @"
                    SET NOCOUNT OFF;
                    INSERT INTO Deudas_x_cedulon3 (
                        nro_cedulon, nro_transaccion, monto_pagado, descuento_anticipo, 
                        vencimiento_transaccion, pago_parcial, categoria_deuda)
                    VALUES (
                        @nro_cedulon, @nro_transaccion, @monto_pagado, @descuento_anticipo,
                        @vencimiento_transaccion, @pago_parcial, @categoria_deuda)";

                //@vencimiento_transaccion, @pago_parcial, @categoria_deuda)";
                decimal descuento = 0;
                DateTime vencimiento;
                long nro_cedulon = oCedulon.nro_cedulon;

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Transaction = trx;
                cmd.Parameters.AddWithValue("@nro_cedulon", 0);
                cmd.Parameters.AddWithValue("@nro_transaccion", 0);
                cmd.Parameters.AddWithValue("@monto_pagado", 0);
                cmd.Parameters.AddWithValue("@descuento_anticipo", 0);
                cmd.Parameters.AddWithValue("@vencimiento_transaccion", DBNull.Value);
                cmd.Parameters.AddWithValue("@pago_parcial", false);
                cmd.Parameters.AddWithValue("@categoria_deuda", 0);
                foreach (var item in oCedulon.lstDeuda)
                {
                    cmd.Parameters.Clear();
                    // Primero se agregan los parámetros nuevamente
                    cmd.Parameters.AddWithValue("@nro_cedulon", nro_cedulon);
                    cmd.Parameters.AddWithValue("@nro_transaccion", item.nro_transaccion);
                    cmd.Parameters.AddWithValue("@monto_pagado", item.importe);
                    cmd.Parameters.AddWithValue("@descuento_anticipo", descuento);
                    cmd.Parameters.AddWithValue("@pago_parcial", item.pago_parcial);
                    cmd.Parameters.AddWithValue("@categoria_deuda", item.categoria_deuda);

                    // Manejo del vencimiento
                    if (item.categoria_deuda != 1)
                    {
                        if (!DateTime.TryParse(item.fecha_vencimiento, out vencimiento) ||
                            vencimiento <= new DateTime(1900, 1, 1))
                        {
                            vencimiento = DateTime.Now;
                        }
                    }
                    else
                    {
                        int yyyy;
                        try
                        {
                            yyyy = int.Parse(item.periodo.Substring(0, 4));
                        }
                        catch
                        {
                            yyyy = 1999;
                        }

                        vencimiento = new DateTime(yyyy, 1, 1);
                    }

                    cmd.Parameters.AddWithValue("@vencimiento_transaccion", vencimiento);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query! " + e.ToString());
                throw new Exception("Error en detalle de Cedulon! " + e.ToString());
            }
        }
        public static void InsertProcuracion(Entities.Cedulones oCedulon, int nroProcuracion, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = @" SET NOCOUNT OFF;
                            INSERT INTO PROCURACION_X_CEDULONES (
                                nro_cedulon, nro_procuracion, nro_transaccion, subsistema,
                                monto_pagado, descuento_anticipo, categoria_deuda,
                                vencimiento_transaccion, pagado, usuario)
                            VALUES (
                                @nro_cedulon, @nro_procuracion, @nro_transaccion, @subsistema,
                                @monto_pagado, @descuento_anticipo, @categoria_deuda,
                                @vencimiento_transaccion, @pagado, @usuario)";
                //decimal descuento = 0;
                long nro_cedulon = oCedulon.nro_cedulon;
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Transaction = trx;
                cmd.Parameters.AddWithValue("@nro_cedulon", 0);
                cmd.Parameters.AddWithValue("@nro_procuracion", 0);
                cmd.Parameters.AddWithValue("@nro_transaccion", 0);
                cmd.Parameters.AddWithValue("@subsistema", 0);
                cmd.Parameters.AddWithValue("@monto_pagado", 0);
                cmd.Parameters.AddWithValue("@descuento_anticipo", 0);
                cmd.Parameters.AddWithValue("@categoria_deuda", 0);
                cmd.Parameters.AddWithValue("@vencimiento_transaccion", DBNull.Value);
                cmd.Parameters.AddWithValue("@pagado", false);
                cmd.Parameters.AddWithValue("@usuario", string.Empty);
                int subsistema = oCedulon.subsistema;
                foreach (var item in oCedulon.lstDeuda)
                {
                    cmd.Parameters.AddWithValue("@nro_cedulon", nro_cedulon);
                    cmd.Parameters.AddWithValue("@nro_procuracion", nroProcuracion);
                    cmd.Parameters.AddWithValue("@nro_transaccion", item.nro_transaccion);
                    cmd.Parameters.AddWithValue("@subsistema", subsistema);
                    cmd.Parameters.AddWithValue("@monto_pagado", item.importe);
                    cmd.Parameters.AddWithValue("@descuento_anticipo", 0);
                    cmd.Parameters.AddWithValue("@categoria_deuda", item.categoria_deuda);
                    cmd.Parameters.AddWithValue("@vencimiento_transaccion", item.fecha_vencimiento);
                    cmd.Parameters.AddWithValue("@pagado", false);
                    cmd.Parameters.AddWithValue("@usuario", ""); // reemplazar por usuario real si corresponde
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query! " + e.ToString());
                throw new Exception("Error en el InsertProcuracion : " + nroProcuracion.ToString() + ' ' + e.ToString());
            }
        }



    }
}
