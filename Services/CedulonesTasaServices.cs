using System.Data.SqlClient;
using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public class CedulonesTasaServices : ICedulonesTasaServices
    {
        public long EmitoCedulonTasa(int cir, int sec, int man, int par, int p_h,
            string vencimiento, decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc, int tipoCedulon, string periodo)
        {
            long nro_cedulon = 0;
            try
            {
                //DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
                //DateTime f = Convert.ToDateTime(vencimiento, culturaFecArgentina);
                //vencimiento = f.ToShortDateString();

                //long ret = 0;
                Entities.Cedulones oCedulon = new Entities.Cedulones();
                Entities.Inmuebles oInmueble = Entities.Inmuebles.getInmuebleByPk(cir, sec, man, par, p_h);
                if (oInmueble != null)
                {
                    //oCedulon.nro_cedulon = nro_cedulon;
                    oCedulon.fecha_emision = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    oCedulon.subsistema = 1;
                    //TIPO_CEDULON :
                    //1-CEDULON ANUAL
                    //2-CEDULON MASIVO PERIODICO
                    //3-CEDULON MOSTRADOR
                    //4-PAGO PARCIAL
                    //5-CEDULON INTERNET
                    oCedulon.tipo_cedulon = tipoCedulon;
                    oCedulon.no_pagado = true;
                    oCedulon.activo = true;
                    oCedulon.nro_emision = 0;
                    oCedulon.circunscripcion = cir;
                    oCedulon.seccion = sec;
                    oCedulon.manzana = man;
                    oCedulon.parcela = par;
                    oCedulon.p_h = p_h;
                    //Datos Factura Cedulon, Importe//
                    oCedulon.periodo = periodo;
                    oCedulon.vencimiento_1 = null;
                    oCedulon.monto_1 = 0;
                    oCedulon.vencimiento_2 = vencimiento; 
                        //DateTime.Now.AddDays(1).ToShortDateString();
                    oCedulon.monto_2 = monto_cedulon;
                    oCedulon.contado = 0;
                    oCedulon.cheques = 0;
                    oCedulon.monto_arreglo = 0;
                    //oCedulon.nro_decreto = null;
                    /////////////////////////////////
                    oCedulon.nro_dom_esp = oInmueble.nro_dom_esp;
                    oCedulon.piso_dpto_esp = oInmueble.piso_dpto_esp;
                    oCedulon.local_esp = "";
                    oCedulon.nom_calle_dom_esp = oInmueble.nom_calle_dom_esp;
                    oCedulon.nom_barrio_dom_esp = oInmueble.nom_barrio_dom_esp;
                    oCedulon.ciudad_dom_esp = oInmueble.ciudad_dom_esp;
                    oCedulon.provincia_dom_esp = oInmueble.provincia_dom_esp;
                    oCedulon.pais_dom_esp = "";
                    oCedulon.codigo_postal_dom_esp = oInmueble.cod_postal_dom_esp;
                    oCedulon.nro_badec = oInmueble.nro_bad;
                    oCedulon.nro_contrib = 0;
                    oCedulon.nom_badec = oInmueble.nombre;
                    oCedulon.nom_calle_pf = oInmueble.nom_calle_pf;
                    oCedulon.nro_dom_pf = oInmueble.nro_dom_pf;
                    //////////////////////////////////////
                    oCedulon.mNewRecord = true;
                    oCedulon.lstDeuda = Listadeuda;                    
                    //ret = Entities.Cedulones.insert(oCedulon, nroProc);
                    //*******************************************************************//
                    //Junio 2025
                    //Uso Nuevo Metodo de InsertCedulon
                    using SqlConnection cn = DALBase.GetConnectionSIIMVA();
                    cn.Open();
                    using SqlTransaction trx = cn.BeginTransaction();
                    try
                    {
                        nro_cedulon = Cedulones.InsertCedulon(oCedulon, nroProc, cn, trx);
                        trx.Commit();
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }

                }
                return nro_cedulon;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasa(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readTasa(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasaAnual(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readTasaAnual(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetalleCedulonTasa(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readTasa(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetalleCedulonTasaAnual(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readTasaAnual(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
