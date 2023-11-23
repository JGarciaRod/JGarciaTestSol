using iText.Kernel.Geom;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            BL.Empleado empleado = new BL.Empleado();
            empleado.Empleados = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5075/api/Empleado/");

                var responsTask = client.GetAsync("GetAll");
                responsTask.Wait();

                var result = responsTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BL.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        BL.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Empleado>(resultItem.ToString());
                        empleado.Empleados.Add(resultItemList);
                    }
                }
            }
            return View(empleado);
        }


        [HttpGet]
        public IActionResult Form(int? IdEmpleado)
        {
            BL.Empleado empleado = new BL.Empleado();
            empleado.Area = new BL.Area();
            empleado.Area.Areas = new List<object>();

            if (IdEmpleado != 0) //update
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5075/api/Empleado/");
                    var responsTask = client.GetAsync("GetById/" + IdEmpleado);

                    responsTask.Wait();
                    var result = responsTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<BL.Result>();
                        readTask.Wait();

                        BL.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Empleado>(readTask.Result.Object.ToString());
                        empleado = resultItemList;



                        empleado.Area = new BL.Area();
                        empleado.Area.Areas = new List<object>();

                    }
                }
                

                using (var clientArea = new HttpClient())
                {
                    clientArea.BaseAddress = new Uri("http://localhost:5075/api/Area/GetAll");
                    var responsTask = clientArea.GetAsync("GetAll");

                    responsTask.Wait();
                    var result = responsTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<BL.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            BL.Area resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Area>(resultItem.ToString());
                            empleado.Area.Areas.Add(resultItemList);
                        }

                    }
                }


            }
            else //Add
            {

                using (var clientArea = new HttpClient())
                {
                    clientArea.BaseAddress = new Uri("http://localhost:5075/api/Area/GetAll");
                    var responsTask = clientArea.GetAsync("GetAll");

                    responsTask.Wait();
                    var result = responsTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<BL.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            BL.Area resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Area>(resultItem.ToString());
                            empleado.Area.Areas.Add(resultItemList);
                        }

                    }
                }
            }
            return View(empleado);
        }

        [HttpPost]
        public IActionResult Form(BL.Empleado empleado)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5075/api/Empleado/");

                if (empleado.IdEmpleado == 0) //add
                {
                    var responsTask = client.PostAsJsonAsync<BL.Empleado>("", empleado);
                    responsTask.Wait();

                    var result = responsTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha insertado correctamente";
                    }
                    else
                    {
                        ViewBag.Error = result.ToString();
                    }
                }
                else //update
                {
                    var responsTask = client.PutAsJsonAsync<BL.Empleado>("" + empleado.IdEmpleado, empleado);
                    responsTask.Wait();

                    var result = responsTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Error = result.ToString();
                    }
                }
            }
            return PartialView("Modal");
        }


        [HttpDelete]
        public IActionResult Delete(int IdEmpleado)
        {
            BL.Empleado empleado = new BL.Empleado();
            empleado.IdEmpleado = IdEmpleado;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5075/api/Empleado/");

                var responsTask = client.DeleteAsync("" + empleado.IdEmpleado);
                responsTask.Wait();

                var result = responsTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    ViewBag.Error = result.ToString();
                }
            }
            return PartialView("Modal");
        }

    }
}
