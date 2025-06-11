using System.Data.SqlClient;
using WSCedulones.Entities;

namespace WSCedulones.Services
{
    public interface ICedulonesAutosServices
    {
        public long EmitoCedulonVehiculo(string dominio, string vencimiento, decimal monto_cedulon,
List<Entities.VCtasctes> Listadeuda, int nroProc, int tipoCedulon, string periodo);

        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAuto(long nroCedulon);
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAutoAnual(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAuto(long nroCedulon);
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAutoAnual(long nroCedulon);
    }
}
