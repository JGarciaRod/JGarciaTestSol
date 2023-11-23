using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public int IdArea { get; set; }
        public string? NombreArea { get; set; }

        public List<Object> Areas { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (DL.JgarciaTestSolContext context=new DL.JgarciaTestSolContext())
                {
                    var query = context.Areas.FromSqlRaw("AreaDDL").ToList();
                    if(query!= null)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            Area area = new Area();
                            area.IdArea = item.IdArea;
                            area.NombreArea = item.NombreArea;

                            result.Objects.Add(area);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }
    }
}
