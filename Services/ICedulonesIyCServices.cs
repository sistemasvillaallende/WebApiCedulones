using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public interface ICedulonesIyCServices
    {
        public long EmitoCedulonComercio(int legajo, string vencimiento,
            decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc);

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonComercio(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonComercio(long nroCedulon);
    }
}
