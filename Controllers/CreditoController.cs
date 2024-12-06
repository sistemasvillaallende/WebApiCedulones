using Microsoft.AspNetCore.Mvc;
using WSCedulones.Entities;
using WSCedulones.Services;

namespace WSCedulones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CreditoController : Controller
    {
        private ICedulonesCreditoServices _ICreditoServices;
        public CreditoController(ICedulonesCreditoServices CreditoService)
        {
            _ICreditoServices = CreditoService;
        }
        [HttpPost]
        public IActionResult EmitoCedulonCredito(CEDULON_INSERT_COMERCIO oCedulon)
        {
            try
            {
                var nro_cedulon = _ICreditoServices.EmitoCedulonCredito(
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
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonCredito(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonCredito = 
                    _ICreditoServices.getCabeceraPrintCedulonCredito(nroCedulon);
                return CabeceraPrintCedulonCredito;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonCredito(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonComercio = 
                    _ICreditoServices.getDetallePrintCedulonCredito(nroCedulon);
                return CabeceraPrintCedulonComercio;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
