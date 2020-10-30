using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Timers;

namespace WSCanales.Controllers
{
    
    [Produces("application/json")]
    public class CanalesController : Controller
    {

        [Route("api/Canales/1/{cuenta}/{numcom}/{motcon}/{montcom}")]
        public ActionResult GetFactura(string cuenta, string numcom, string motcon, string montcom)
        {
            try
            {
                string respuesta="";
                if (cuenta!=null && numcom != null && motcon != null && montcom != null)
                {
                    long id = getid();
                    long newid = id + 1;
                    DateTime fecha = DateTime.Now;
                    string tipo = "1205";
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]([ID],[NUMCUENTA],[NUMCOM],[MOTCON],[MONTCOM],[FECHA_REGISTRO],[TIPO],[ORIGEN])VALUES("+newid+",'"+cuenta+ "','" + numcom + "','" + motcon + "','" + montcom + "','" + fecha + "','" + tipo + "','" + origen+ "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(newid);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(newid);
                    }
                    var consulta = (from c in datos
                                    select c).Single();

                    respuesta = consulta.CODERRO+consulta.CLINOMB+consulta.SALCON.ToString()+consulta.SALDIS.ToString();
                }  
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("api/Canales/2/{cuenta}/{numcom}/{motcon}/{monto}/{montcom}/{motcom}")]
        public ActionResult GetDebito(string cuenta, string numcom, string motcon, string monto, string montcom, string motcom)
        {
            try
            {
                string respuesta = "";
                if (cuenta != null && numcom != null && motcon != null && monto != null && montcom != null && motcom != null)
                {
                    long id = getid();
                    long newid = id + 1;
                    DateTime fecha = DateTime.Now;
                    string tipo = "1500";
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]([ID],[NUMCUENTA],[NUMCOM],[MOTCON],[MONTO],[MONTCOM],[MOTCOM],[FECHA_REGISTRO],[TIPO],[ORIGEN])VALUES(" + newid + ",'" + cuenta + "','" + numcom + "'," + motcon + ",'" + monto + "','" + montcom + "','" + motcom + "','" + fecha + "','" + tipo + "','" + origen + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                   datos = CONSULTA(newid);
                   while(datos.Count==0)
                    {
                        datos = CONSULTA(newid);
                    }
                    var consulta = (from c in datos
                                    select c).Single();

                    respuesta = consulta.CODERRO + consulta.CLINOMB + consulta.SALCON.ToString() + consulta.SALDIS.ToString();
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("api/Canales/3/{cuenta}/{numcom}/{motcon}/{monto}/{montcom}/{motcom}")]
        public ActionResult GetCredito(string cuenta, string numcom, string motcon, string monto, string montcom, string motcom)
        {
            try
            {
                string respuesta = "";
                if (cuenta != null && numcom != null && motcon != null && monto != null && montcom != null && motcom != null)
                {
                    long id = getid();
                    long newid = id + 1;
                    DateTime fecha = DateTime.Now;
                    string tipo = "1901";
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]([ID],[NUMCUENTA],[NUMCOM],[MOTCON],[MONTO],[MONTCOM],[MOTCOM],[FECHA_REGISTRO],[TIPO],[ORIGEN])VALUES(" + newid + ",'" + cuenta + "','" + numcom + "'," + motcon + ",'" + monto + "','" + montcom + "','" + motcom + "','" + fecha + "','" + tipo + "','" + origen + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(newid);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(newid);
                    }
                    var consulta = (from c in datos
                                    select c).Single();

                    respuesta = consulta.CODERRO + consulta.CLINOMB + consulta.SALCON.ToString() + consulta.SALDIS.ToString();
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("api/Canales/4/{cuenta}/{numcom}/{motcon}/{monto}/{montcom}/{motcom}/{transcred}/{cuecred}/{comcred}")]
        public ActionResult GetTransaccion(string cuenta, string numcom, string motcon, string monto, string montcom, string motcom, string transcred, string cuecred, string comcred)
        {
            try
            {
                string respuesta = "";
                if (cuenta != null && numcom != null && motcon != null && monto != null && montcom != null && motcom != null && transcred != null && cuecred != null && comcred != null)
                {
                    long id = getid();
                    long newid = id + 1;
                    DateTime fecha = DateTime.Now;
                    string tipo = "1902";
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]([ID],[NUMCUENTA],[NUMCOM],[MOTCON],[MONTO],[MONTCOM],[MOTCOM],[TRANSCRED],[CUECRED],[COMCRED],[FECHA_REGISTRO],[TIPO],[ORIGEN])VALUES(" + newid + ",'" + cuenta + "','" + numcom + "'," + motcon + ",'" + monto + "','" + montcom + "','" + motcom + "','" + transcred + "','" + cuecred + "','" + comcred + "','" + fecha + "','" + tipo + "','" + origen + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(newid);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(newid);
                    }
                    var consulta = (from c in datos
                                    select c).Single();

                    respuesta = consulta.CODERRO + consulta.CLINOMB + consulta.SALCON.ToString() + consulta.SALDIS.ToString();
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class CanalesElectronicos
        {
            public long ID { get; set; }
            public string NUMCUENTA { get; set; }
            public string NUMCOM { get; set; }
            public string MOTCON { get; set; }
            public string MONTCOM { get; set; }
            public string RES_COL { get; set; }
            public string? CODERRO { get; set; }
            public string? CLINOMB { get; set; }
            public decimal? SALCON { get; set; }
            public decimal? SALDIS { get; set; }


        }

        public List<CanalesElectronicos> CONSULTA(long ID)
        {
            List<CanalesElectronicos> respuesta = new List<CanalesElectronicos>();
            using (var db = new SqlConnection("data source=100-TI01\\SQLEXPRESS;initial catalog=CanalesElectronicos;integrated security=True;MultipleActiveResultSets=True;"))
            {
                db.Open();
                var query = "SELECT CODERRO,CLINOMB,SALCON,SALDIS FROM COLCANAL where ID="+ID;
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) { 
                            respuesta.Add(new CanalesElectronicos()
                            {
                                CODERRO = reader.GetString(0),
                                CLINOMB = reader.GetString(1),
                                SALCON = reader.GetDecimal(2),
                                SALDIS= reader.GetDecimal(3)

                            });
                            }
                        }
                    }
                }

            }

            return respuesta;
        }

        public long getid()
        {
            long respuesta = 0;
            using (var db = new SqlConnection("data source=100-TI01\\SQLEXPRESS;initial catalog=CanalesElectronicos;integrated security=True;MultipleActiveResultSets=True;"))
            {
                db.Open();
                var query = "SELECT TOP(1)ID FROM COLCANAL order by ID DESC";
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetInt64(0);
                        }
                    }
                }

            }

            return respuesta;
        }


        public void exquery(string sql)
        {

            using (var db = new SqlConnection("data source=100-TI01\\SQLEXPRESS;initial catalog=CanalesElectronicos;integrated security=True;MultipleActiveResultSets=True;"))
            {
                db.Open();
                var query = sql;
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    command.ExecuteNonQuery();
                }

            }
        }


        // GET: api/Canales/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Canales
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Canales/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
