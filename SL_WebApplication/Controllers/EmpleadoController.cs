using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            BL.Result result = new BL.Result();
            result = BL.Empleado.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdEmpleado?}")]
        public IActionResult GetById(int IdEmpleado)
        {
            BL.Result result = new BL.Result(); 
            result = BL.Empleado.GetById(IdEmpleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(BL.Empleado empleado)
        {
            BL.Result result = new BL.Result();
            result = BL.Empleado.Add(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPut]
        [Route("{IdEmpleado?}")]
        public IActionResult Update(int IdEmpleado, [FromBody] BL.Empleado empleado)
        {
            empleado.IdEmpleado = IdEmpleado;

            BL.Result result = new BL.Result();
            result = BL.Empleado.Update(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("{IdEmpleado?}")]
        public IActionResult Dell(int IdEmpleada)
        {
            BL.Result result = new BL.Result();

            result = BL.Empleado.Delete(IdEmpleada);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

    }
}
