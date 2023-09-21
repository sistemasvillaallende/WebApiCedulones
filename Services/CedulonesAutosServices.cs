using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public class CedulonesAutosServices : ICedulonesAutosServices
    {
        public long EmitoCedulonVehiculo(string dominio, string vencimiento, decimal monto_cedulon, List<VCtasctes> Listadeuda, int nroProc, int tipoCedulon, string periodo)
        {
            long ret = 0;
            try
            {
                Entities.Cedulones oCedulon = new Entities.Cedulones();
                Entities.Automotor oAuto = Entities.Automotor.getAutoByPk(dominio);
                if (oAuto != null)
                {
                    oCedulon.fecha_emision = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    oCedulon.subsistema = 4;
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
                    oCedulon.dominio = dominio;
                    //Datos Factura Cedulon, Importe//
                    oCedulon.periodo = periodo;
                    oCedulon.vencimiento_1 = null;
                    oCedulon.monto_1 = 0;
                    oCedulon.vencimiento_2 = vencimiento;
                    oCedulon.monto_2 = monto_cedulon;
                    oCedulon.contado = 0;
                    oCedulon.cheques = 0;
                    oCedulon.monto_arreglo = 0;
                    oCedulon.nro_decreto = "";
                    //////////////////////////////////
                    //Domicilio postal////////////////////
                    oCedulon.nro_dom_esp = oAuto.nro_dom_esp;
                    oCedulon.piso_dpto_esp = oAuto.piso_dpto_esp;
                    oCedulon.local_esp = "";
                    oCedulon.nom_calle_dom_esp = oAuto.nom_calle_dom_esp;
                    oCedulon.nom_barrio_dom_esp = oAuto.nom_barrio_dom_esp;
                    oCedulon.ciudad_dom_esp = oAuto.ciudad_dom_esp;
                    oCedulon.provincia_dom_esp = oAuto.provincia_dom_esp;
                    oCedulon.pais_dom_esp = "";
                    oCedulon.codigo_postal_dom_esp = oAuto.cod_postal_dom_esp;
                    oCedulon.nro_badec = oAuto.nro_bad;
                    oCedulon.nro_contrib = 0;
                    oCedulon.nom_badec = oAuto.nombre;
                    oCedulon.nom_calle_pf = "";
                    oCedulon.nro_dom_pf = "";
                    //////////////////////////////////////
                    oCedulon.dominio = oAuto.dominio;
                    //oCedulon.legajo = legajo;
                    //b.imprime = imprime;
                    //b.tipo_cem = "";//tipo_cem;
                    //b.manzana_cem = manzana_cem;
                    //b.lote_cem = lote_cem;
                    //b.parcela_cem = parcela_cem;
                    //b.nivel_cem = nivel_cem;

                    oCedulon.mNewRecord = true;
                    oCedulon.lstDeuda = Listadeuda;
                    ret = Entities.Cedulones.insert(oCedulon, nroProc);
                    //b.nro_cedulon = b.nro_cedulon;
                    //oCedulon. = true;
                    //i = null;
                    //return b.nro_cedulon;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Error al generar el cedulon!, transaction rolled back.");
            }
            return ret;
        }
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAuto(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readAuto(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAutoAnual(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readAutoAnual(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAuto(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readAuto(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAutoAnual(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readAutoAnual(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
