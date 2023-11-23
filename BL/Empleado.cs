using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public int? IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPeterno { get; set; }
        public string? ApellidoMaterno { get; set; }

        public BL.Area? Area { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public float? Sueldo { get; set; }

        public List<object> Empleados { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context =  new DL.JgarciaTestSolContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpladoGetAll").ToList();

                    if(query!= null)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            Empleado empleado = new Empleado();
                            empleado.Area = new Area();
                            

                            empleado.IdEmpleado = item.IdEmpleado;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPeterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.Area.IdArea = item.IdArea.Value;
                            empleado.FechaNacimiento = item.FechaNacimiento.Value;
                            empleado.Sueldo = (float?)item.Sueldo;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result GetById(int IdEmpleado)
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context = new DL.JgarciaTestSolContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").AsEnumerable().SingleOrDefault();

                    result.Object = new List<object>();

                    if(query!= null)
                    {
                        Empleado empleado = new Empleado();
                        empleado.Area = new Area();

                        empleado.IdEmpleado = query.IdEmpleado;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPeterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Area.IdArea = query.IdArea.Value;
                        empleado.FechaNacimiento = query.FechaNacimiento.Value;
                        empleado.Sueldo = (float?)query.Sueldo;

                        result.Object = empleado;

                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result Add(Empleado empleado)
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context = new DL.JgarciaTestSolContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}','{empleado.ApellidoPeterno}','{empleado.ApellidoMaterno}','{empleado.Area.IdArea}','{empleado.FechaNacimiento}','{empleado.Sueldo}' ");

                    if ( rowAffected > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result Update(Empleado empleado)
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context = new DL.JgarciaTestSolContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.IdEmpleado}','{empleado.Nombre}','{empleado.ApellidoPeterno}','{empleado.ApellidoMaterno}','{empleado.Area.IdArea}','{empleado.FechaNacimiento}','{empleado.Sueldo}' ");
                    if ( rowAffected > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }

        public static Result Delete(int IdEmpleado)
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context =  new DL.JgarciaTestSolContext())
                {
                    int rowAffected = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{IdEmpleado}'");

                    if( rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}