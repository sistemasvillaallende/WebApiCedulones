using Microsoft.AspNetCore.Mvc;
using WSCedulones.Entities;
using WSCedulones.Services;

namespace WSCedulones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ComercioController : Controller
    {
        private ICedulonesIyCServives _IComercioServices;
        public ComercioController(ICedulonesIyCServives ComercioService)
        {
            _IComercioServices = ComercioService;
        }
        [HttpPost]
        public IActionResult EmitoCedulonComercio(CEDULON_INSERT_COMERCIO oCedulon)
        {
            try
            {
                var nro_cedulon = _IComercioServices.EmitoCedulonComercio(
                    oCedulon.legajo, oCedulon.vencimiento, oCedulon.monto_cedulon,
                    oCedulon.Listadeuda, oCedulon.nroProc);
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
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonComercio(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonComercio = 
                    _IComercioServices.getCabeceraPrintCedulonComercio(nroCedulon);
                return CabeceraPrintCedulonComercio;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonComercio(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonComercio = 
                    _IComercioServices.getDetallePrintCedulonComercio(nroCedulon);
                return CabeceraPrintCedulonComercio;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
