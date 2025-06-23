using System.Data.SqlClient;
using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public class CedulonesIyCServices : ICedulonesIyCServices
    {

        public long EmitoCedulonComercio(int legajo, string vencimiento,
            decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc)
        {
            long nro_cedulon = 0;
            try
            {
                Entities.Cedulones oCedulon = new Entities.Cedulones();
                Entities.IndyCom oIndycom = Entities.IndyCom.GetIndycomByPk(legajo);
                if (oIndycom != null)
                {

                    oCedulon.fecha_emision = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    oCedulon.subsistema = 3;
                    //TIPO_CEDULON :
                    //1-CEDULON ANUAL
                    //2-CEDULON MASIVO PERIODICO
                    //3-CEDULON MOSTRADOR
                    //4-PAGO PARCIAL
                    //5-CEDULON INTERNET
                    oCedulon.tipo_cedulon = 5;
                    oCedulon.no_pagado = true;
                    oCedulon.activo = true;
                    oCedulon.nro_emision = 0;
                    //Datos Factura Cedulon, Importe//
                    oCedulon.periodo = "";
                    oCedulon.vencimiento_1 = null;
                    oCedulon.monto_1 = 0;
                    oCedulon.vencimiento_2 = vencimiento;
                    oCedulon.monto_2 = monto_cedulon;
                    oCedulon.contado = 0;
                    oCedulon.cheques = 0;
                    oCedulon.monto_arreglo = 0;
                    //oCedulon.nro_decreto = null;
                    //////////////////////////////////
                    //Domicilio postal////////////////////
                    oCedulon.nro_dom_esp = oIndycom.nro_dom_esp;
                    oCedulon.piso_dpto_esp = oIndycom.piso_dpto_esp;
                    oCedulon.local_esp = "";
                    oCedulon.nom_calle_dom_esp = oIndycom.nom_calle_dom_esp;
                    oCedulon.nom_barrio_dom_esp = oIndycom.nom_barrio_dom_esp;
                    oCedulon.ciudad_dom_esp = oIndycom.ciudad_dom_esp;
                    oCedulon.provincia_dom_esp = oIndycom.provincia_dom_esp;
                    oCedulon.pais_dom_esp = "";
                    oCedulon.codigo_postal_dom_esp = oIndycom.cod_postal_dom_esp;
                    oCedulon.nro_badec = oIndycom.nro_bad;
                    oCedulon.nro_contrib = 0;
                    oCedulon.nom_badec = oIndycom.nombre;
                    oCedulon.legajo = oIndycom.legajo;
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
            catch (Exception e)
            {
                throw new Exception(e.Message + "Error al generar el cedulon!, transaction rolled back.");
            }
        }


        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonComercio(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readIndyCom(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonComercio(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readIndyCom(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
