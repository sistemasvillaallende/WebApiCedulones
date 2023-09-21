using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public interface ICedulonesTasaServices
    {
        public long EmitoCedulonTasa(int cir, int sec, int man, int par, int p_h,
            string vencimiento, decimal monto_cedulon, List<Entities.VCtasctes> Listadeuda,
            int nroProc, int tipoCedulon, string periodo);

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasa(long nroCedulon);
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasaAnual(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetalleCedulonTasa(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetalleCedulonTasaAnual(long nroCedulon);
    }
}
