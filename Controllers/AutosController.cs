using Microsoft.AspNetCore.Mvc;
using WSCedulones.Entities;
using WSCedulones.Services;

namespace WSCedulones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutosController : Controller
    {
        private ICedulonesAutosServices _IAutosServices;
        public AutosController(ICedulonesAutosServices AutosService)
        {
            _IAutosServices = AutosService;
        }
        [HttpPost]
        public IActionResult EmitoCedulonAuto(CEDULON_INSERT_AUTO oCedulon)
        {
            try
            {
                var nro_cedulon = _IAutosServices.EmitoCedulonVehiculo(
                    oCedulon.dominio, oCedulon.vencimiento, oCedulon.monto_cedulon,
                    oCedulon.Listadeuda, oCedulon.nroProc, 5, string.Empty);
                if (nro_cedulon == 0)
                {
                    return BadRequest(new { message = "No se pudo Confirmar el Cedulon!" });
                }
                return Ok(nro_cedulon);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAuto(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonAuto = _IAutosServices.getCabeceraPrintCedulonAuto(nroCedulon);
                return CabeceraPrintCedulonAuto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonAutoAnual(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonAuto = _IAutosServices.getCabeceraPrintCedulonAutoAnual(nroCedulon);
                return CabeceraPrintCedulonAuto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAuto(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonAuto = _IAutosServices.getDetallePrintCedulonAuto(nroCedulon);
                return CabeceraPrintCedulonAuto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonAutoAnual(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonAuto = _IAutosServices.getDetallePrintCedulonAutoAnual(nroCedulon);
                return CabeceraPrintCedulonAuto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
