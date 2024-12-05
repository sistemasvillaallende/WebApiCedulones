using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public interface ICedulonesCreditoServices
    {
        public long EmitoCedulonCredito(int legajo, string vencimiento,
            decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc);

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonCredito(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonCredito(long nroCedulon);
    }
}
