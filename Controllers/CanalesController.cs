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
    
    
    public class CanalesController : Controller
    {

        [Route("api/Canales/1/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{montoComision}")]
        public ActionResult GetFactura(string tipoRespuesta,string numeroCuenta, string numeroComprobante, string motivoContable, string montoComision)
        {
            try
            {
                string respuestaXml="";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (tipoRespuesta=="1") {
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                    {
                        
                        
                        DateTime fecha = DateTime.Now;
                        string llave = numeroCuenta + fecha.ToString("hhmmssfff")+numeroComprobante;
                        string tipoTransaccion = "1205";
                        string origen = "SISCOA WEB";
                        exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[montoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "','" + motivoContable + "','" + montoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                        List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                        datos = CONSULTA(llave);
                        while (datos.Count == 0)
                        {
                            datos = CONSULTA(llave);
                        }
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();

                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12,'0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');
                    }
                    return Ok(respuestaXml);
                }
                if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                {
                   
                    DateTime fecha = DateTime.Now;
                    string tipoTransaccion = "1205";
                    string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                    
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[montoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "','" + motivoContable + "','" + montoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(llave);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(llave);
                    }
                    var consulta = (from c in datos
                                    select c).ToList();

                    respuestaJson = consulta;
                }
                return Ok(respuestaJson);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/2/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}")]
        public ActionResult GetDebito(string tipoRespuesta,string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision)
        {
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (tipoRespuesta == "1")
                {
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null)
                    {
                       
                        DateTime fecha = DateTime.Now;
                        string tipoTransaccion = "1500";
                        string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                       
                        string origen = "SISCOA WEB";
                        exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                        List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                        datos = CONSULTA(llave);
                        while (datos.Count == 0)
                        {
                            datos = CONSULTA(llave);
                        }
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();

                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12,'0') + consulta.saldoDisponible.ToString().PadLeft(12,'0');
                    }
                    return Ok(respuestaXml);
                }
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null)
                {

                    DateTime fecha = DateTime.Now;
                    string tipoTransaccion = "1500";
                    string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                    
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                   datos = CONSULTA(llave);
                   while(datos.Count==0)
                    {
                        datos = CONSULTA(llave);
                    }
                    var consulta = (from c in datos
                                    select c).ToList();

                    respuestaJson = consulta;
                }
                return Ok(respuestaJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
       //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
        [Route("api/Canales/3/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}")]
        public ActionResult GetCredito(string tipoRespuesta, string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision)
        {
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (tipoRespuesta == "1")
                {
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null)
                    {
                      
                        DateTime fecha = DateTime.Now;
                        string tipoTransaccion = "1901";
                        string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                        
                        string origen = "SISCOA WEB";
                        exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                        List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                        datos = CONSULTA(llave);
                        while (datos.Count == 0)
                        {
                            datos = CONSULTA(llave);
                        }
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();

                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12,'0') + consulta.saldoDisponible.ToString().PadLeft(12,'0');
                    }
                    return Ok(respuestaXml);
                }
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null)
                {
                    
                    DateTime fecha = DateTime.Now;
                    string tipoTransaccion = "1901";
                    string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                    
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(llave);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(llave);
                    }
                    var consulta = (from c in datos
                                    select c).ToList();

                    respuestaJson = consulta;
                }
                return Ok(respuestaJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/4/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}/{transaccionCredito}/{cuentaCredito}/{comisionCredito}")]
        public ActionResult GetTransaccion(string tipoRespuesta,string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision, string transaccionCredito, string cuentaCredito, string comisionCredito)
        {
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (tipoRespuesta == "1")
                {
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null && transaccionCredito != null && cuentaCredito != null && comisionCredito != null)
                    {
                        
                        DateTime fecha = DateTime.Now;
                        string tipoTransaccion = "1902";
                        string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                       
                        string origen = "SISCOA WEB";
                        exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[transaccionCredito],[cuentaCredito],[comisionCredito],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + transaccionCredito + "','" + cuentaCredito + "','" + comisionCredito + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                        List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                        datos = CONSULTA(llave);
                        while (datos.Count == 0)
                        {
                            datos = CONSULTA(llave);
                        }
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();

                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12,'0') + consulta.saldoDisponible.ToString().PadLeft(12,'0');
                    }
                    return Ok(respuestaXml);
                }
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && monto != null && montoComision != null && motivoComision != null && transaccionCredito != null && cuentaCredito != null && comisionCredito != null)
                {
                    
                    DateTime fecha = DateTime.Now;
                    string tipoTransaccion = "1902";
                    string llave = numeroCuenta + fecha.ToString("hhmmssfff") +numeroComprobante+ tipoTransaccion;
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[numeroComprobante],[motivoContable],[MONTO],[montoComision],[motivoComision],[transaccionCredito],[cuentaCredito],[comisionCredito],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE])VALUES( '" + numeroCuenta + "','" +numeroComprobante + "'," + motivoContable + ",'" + monto + "','" + montoComision + "','" + motivoComision + "','" + transaccionCredito + "','" + cuentaCredito + "','" + comisionCredito + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(llave);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(llave);
                    }
                    var consulta = (from c in datos
                                    select c).ToList();

                    respuestaJson = consulta;
                }
                return Ok(respuestaJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/5/{numeroPrestamo}/{monto}/{cuentaDebito}/{motivoDebito}")]
        public ActionResult GetPrestamo(string numeroPrestamo, string monto, string cuentaDebito, string motivoDebito)
        {
            try
            {
                List<CanalesElectronicos> respuesta = new List<CanalesElectronicos>();
                if (numeroPrestamo != null && monto != null && cuentaDebito != null && motivoDebito != null)
                {
                   
                    DateTime fecha = DateTime.Now;
                    
                    string tipoTransaccion = "3500";
                    string transaccionPagoPrestamo = "3501";
                    string llave = numeroPrestamo + fecha.ToString("hhmmssfff") + tipoTransaccion+transaccionPagoPrestamo;
                    string origen = "WERSERVICE";
                    exquery("INSERT INTO [COLCANAL]( [numeroCuenta],[motivoContable],[monto],[fechaRegistro],[tipoTransaccion],[ORIGEN],[LLAVE],[numeroPrestamo],[transaccionPagoPrestamo])" +
                        "VALUES( '" + cuentaDebito + "','" + motivoDebito + "','" + monto + "','" + fecha + "','" + tipoTransaccion + "','" + origen + "','" + llave + "','" + numeroPrestamo + "','" + transaccionPagoPrestamo + "')");
                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();

                    datos = CONSULTA(llave);
                    while (datos.Count == 0)
                    {
                        datos = CONSULTA(llave);
                    }
                    var consulta = (from c in datos
                                    select c).ToList();

                    respuesta = consulta;
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
            public int ID { get; set; }
            public string numeroCuenta { get; set; }
            public string numeroComprobante { get; set; }
            public string motivoContable { get; set; }
            public string montoComision { get; set; }
            public string RES_COL { get; set; }
            public string? codigoError { get; set; }
            public string? mensajeCola { get; set; }
            public decimal? saldoContable { get; set; }
            public decimal? saldoDisponible { get; set; }
            public string numeroPrestamo { get; set; }
            public string transaccionPagoPrestamo { get; set; }

        }

        public List<CanalesElectronicos> CONSULTA(string llave)
        {
            List<CanalesElectronicos> respuesta = new List<CanalesElectronicos>();
            using (var db = new SqlConnection("data source=COA-LACT\\SQLSERVER2014;initial catalog=CanalesElectronicos;integrated security=True;MultipleActiveResultSets=True;"))
            {
                db.Open();
                var query = "SELECT codigoError,mensajeCola,saldoContable,saldoDisponible FROM COLCANAL where LLAVE='"+llave+"'";
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) { 
                            respuesta.Add(new CanalesElectronicos()
                            {
                                codigoError = reader.GetString(0),
                                mensajeCola = reader.GetString(1),
                                saldoContable = reader.GetDecimal(2),
                                saldoDisponible= reader.GetDecimal(3)

                            });
                            }
                        }
                    }
                }

            }

            return respuesta;
        }

        public void exquery(string sql)
        {

            using (var db = new SqlConnection("data source=COA-LACT\\SQLSERVER2014;initial catalog=CanalesElectronicos;integrated security=True;MultipleActiveResultSets=True;"))
            {
                db.Open();
                var query = sql;
                using (SqlCommand command = new SqlCommand(query, db))
                {
                    command.ExecuteNonQuery();
                }

            }
        }

    }
}
