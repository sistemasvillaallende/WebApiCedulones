using Microsoft.AspNetCore.Mvc;
using WSCedulones.Entities;
using WSCedulones.Services;

namespace WSCedulones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TasaController : Controller
    {
        private ICedulonesTasaServices _ITasaServices;
        public TasaController(ICedulonesTasaServices TasaService)
        {
            _ITasaServices = TasaService;
        }
        [HttpPost]
        public IActionResult EmitoCedulonTasa(CEDULON_INSERT_TASA oCedulon)
        {
            try
            {       
                var nro_cedulon = _ITasaServices.EmitoCedulonTasa(oCedulon.cir,
                    oCedulon.sec, oCedulon.man, oCedulon.par, oCedulon.p_h,
                    oCedulon.vencimiento, oCedulon.monto_cedulon,
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
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasa(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonTasa = _ITasaServices.getCabeceraPrintCedulonTasa(nroCedulon);
                return CabeceraPrintCedulonTasa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public CEDULON_PRINT_CABECERA getCabeceraPrintCedulonTasaAnual(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonTasaAnual = _ITasaServices.getCabeceraPrintCedulonTasaAnual(nroCedulon);
                return CabeceraPrintCedulonTasaAnual;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonTasa(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonTasa = _ITasaServices.getDetalleCedulonTasa(nroCedulon);
                return CabeceraPrintCedulonTasa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public List<CEDULON_PRINT_DETALLE> getDetallePrintCedulonTasaAnual(long nroCedulon)
        {
            try
            {
                var CabeceraPrintCedulonTasaAnual = _ITasaServices.getDetalleCedulonTasaAnual(nroCedulon);
                return CabeceraPrintCedulonTasaAnual;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
