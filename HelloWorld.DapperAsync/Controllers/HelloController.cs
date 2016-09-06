using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloWorld.DapperAsync.Controllers
{
    [RoutePrefix("api")]
    public class HelloController : ApiController
    {
        [HttpGet]
        [Route("hello/world")]
        public async Task<IHttpActionResult> World()
        {
            IEnumerable<DTO> data;
            using (var conn = GetConn())
            {
                await conn.OpenAsync();
                data = await conn.QueryAsync<DTO>("Patient_Select", new { Parm1 = DateTime.Now, Parm2 = Guid.NewGuid().ToString() }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return Ok(data);
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(ConfigurationManager.AppSettings["conn"]);
        }
    }

    class DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
    }
}
