using System.Globalization;
using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public class CedulonesCreditoServices : ICedulonesCreditoServices
    {

        public long EmitoCedulonCredito(int legajo, string vencimiento,
            decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc)
        {
            long ret = 0;
            try
            {
                Entities.Cedulones oCedulon = new Entities.Cedulones();
                Entities.Credito oCredito = Entities.Credito.GetCreditoByPk(legajo);
                if (oCredito != null)
                {

                    // DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
                    // DateTime f = Convert.ToDateTime(vencimiento, culturaFecArgentina);
                    oCedulon.fecha_emision = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    oCedulon.subsistema = 7;
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
                    oCedulon.vencimiento_1 = vencimiento ;
                    oCedulon.monto_1 = 0;
                    //oCedulon.vencimiento_2 = vencimiento;
                    oCedulon.vencimiento_2 =  DateTime.Now.AddDays(10).ToString("dd/MM/yyyy hh:mm:ss");
                    oCedulon.monto_2 = monto_cedulon;
                    oCedulon.contado = 0;
                    oCedulon.cheques = 0;
                    oCedulon.monto_arreglo = 0;
                    oCedulon.nro_decreto = "";
                    //////////////////////////////////
                    //Domicilio postal////////////////////
                    oCedulon.nro_dom_esp = oCredito.nro_dom_esp;
                    oCedulon.piso_dpto_esp = oCredito.piso_dpto_esp;
                    oCedulon.local_esp = "";
                    oCedulon.nom_calle_dom_esp = oCredito.nom_calle_dom_esp;
                    oCedulon.nom_barrio_dom_esp = oCredito.nom_barrio_dom_esp;
                    oCedulon.ciudad_dom_esp = oCredito.ciudad_dom_esp;
                    oCedulon.provincia_dom_esp = oCredito.provincia_dom_esp;
                    oCedulon.pais_dom_esp = "";
                    oCedulon.codigo_postal_dom_esp = oCredito.cod_postal_dom_esp.ToString();
                    oCedulon.nro_badec = oCredito.nro_bad;
                    oCedulon.nro_contrib = 0;
                    oCedulon.nom_badec = oCredito.nombre;
                    oCedulon.legajo = oCredito.legajo;
                    //////////////////////////////////////
                    oCedulon.mNewRecord = true;
                    oCedulon.lstDeuda = Listadeuda;
                    ret = Entities.Cedulones.insert(oCedulon, nroProc);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Error al generar el cedulon!, transaction rolled back.");
            }
            return ret;
        }


        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonCredito(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_CABECERA.readCredito(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonCredito(long nroCedulon)
        {
            try
            {
                return Entities.CEDULON_PRINT_DETALLE.readCredito(nroCedulon);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
