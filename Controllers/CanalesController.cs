using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSCanales.Controllers
{

    public class CanalesController : ControllerBase
    {
        public static string conexionDes = "Data Source = COA-LACT\\SQLSERVER2014;Initial Catalog=CanalesElectronicos;integrated security=True;";
        public static string conexionCert = "Data Source = DESA-CANAL02\\COABANK02; Initial Catalog = canalesElectronicos; User ID=sa;Password=Coabank2020*;";

        [Route("api/Canales/1/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{montoComision}/{cldoc}/{ip}/{usuario}/{origenBankingly}")]
        [Route("api/Canales/1/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{montoComision}")]
        public ActionResult GetFactura(string tipoRespuesta, string numeroCuenta, string numeroComprobante, string motivoContable, string montoComision, string? cldoc, string? usuario, string? ip ,string? origenBankingly)
        {
            if ( cldoc==null && usuario==null && ip==null && origenBankingly==null)
                {
                cldoc = "0";
                usuario = "0"; 
                ip = "0";
                origenBankingly = "0";
            }
            DateTime fecha = DateTime.Now;
            string llave = numeroCuenta + fecha.ToString("hhmmssfff") + numeroComprobante;
            string tipoTransaccion = "1205";

            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
              
                    if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                    {
                    var parametros = new DynamicParameters();
                    parametros.Add("@canalesTipo", '1', DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroCuenta", numeroCuenta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroComprobante", numeroComprobante, DbType.String, ParameterDirection.Input);
                    parametros.Add("@motivoContable", motivoContable, DbType.String, ParameterDirection.Input);
                    parametros.Add("@montoComision", Convert.ToDecimal(montoComision), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@tipoTransaccion", tipoTransaccion, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origen", tipoRespuesta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cldoc", cldoc, DbType.String, ParameterDirection.Input);
                    parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                    parametros.Add("@ip", ip, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origenBankingly", origenBankingly, DbType.String, ParameterDirection.Input);

                    
                    TRXCANALES("TRXCANALES_ADD", parametros);
                    var param_con = new DynamicParameters();
                    param_con.Add("@llave",llave,DbType.String,ParameterDirection.Input);
                    param_con.Add("@codigoError", dbType: DbType.String,direction: ParameterDirection.Output,size:500);
                    param_con.Add("@mensajeCola", dbType: DbType.String,direction: ParameterDirection.Output,size:500);
                    param_con.Add("@saldoContable", dbType: DbType.String,direction: ParameterDirection.Output,size:500);
                    param_con.Add("@saldoDisponible", dbType: DbType.String,direction: ParameterDirection.Output,size:500);

                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();
                    datos =TRXCANALES("TRXCANALES_CON", param_con);
                    while (datos.SingleOrDefault().codigoError == null)
                        {
                            datos = TRXCANALES("TRXCANALES_CON", param_con);

                    }
                      
                    if (tipoRespuesta == "1")
                    {
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();
                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12, '0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');

                        return Ok(respuestaXml);
                    }
                    else if (tipoRespuesta == "2")
                    {
                        var consulta = (from c in datos
                                        select c).ToList();

                        respuestaJson = consulta;
                        return Ok(respuestaJson);
                    }
                  
                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
      
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/2/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}/{cldoc}/{ip}/{usuario}/{origenBankingly}")]
        [Route("api/Canales/2/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}")]
        public ActionResult GetDebito(string tipoRespuesta, string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision, string? cldoc, string? usuario, string? ip, string? origenBankingly)
        {
            if (cldoc == null && usuario == null && ip == null && origenBankingly == null)
            {
                cldoc = "0";
                usuario = "0";
                ip = "0";
                origenBankingly = "0";
            }
            DateTime fecha = DateTime.Now;
            string llave = numeroCuenta + fecha.ToString("hhmmssfff") + numeroComprobante;
            string tipoTransaccion = "1500";
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@canalesTipo", '2', DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroCuenta", numeroCuenta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroComprobante", numeroComprobante, DbType.String, ParameterDirection.Input);
                    parametros.Add("@motivoContable", motivoContable, DbType.String, ParameterDirection.Input);
                    parametros.Add("@monto", Convert.ToDecimal(monto), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@montoComision", Convert.ToDecimal(montoComision), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@motivoComision", motivoComision, DbType.String, ParameterDirection.Input);
                    parametros.Add("@tipoTransaccion", tipoTransaccion, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origen", tipoRespuesta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cldoc", cldoc, DbType.String, ParameterDirection.Input);
                    parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                    parametros.Add("@ip", ip, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origenBankingly", origenBankingly, DbType.String, ParameterDirection.Input);

                    TRXCANALES("TRXCANALES_ADD", parametros);
                    var param_con = new DynamicParameters();
                    param_con.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    param_con.Add("@codigoError", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@mensajeCola", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoContable", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoDisponible", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();
                    datos = TRXCANALES("TRXCANALES_CON", param_con);
                    while (datos.SingleOrDefault().codigoError == null)
                    {
                        datos = TRXCANALES("TRXCANALES_CON", param_con);

                    }

                    if (tipoRespuesta == "1")
                    {
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();
                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12, '0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');

                        return Ok(respuestaXml);
                    }
                    else if (tipoRespuesta == "2")
                    {
                        var consulta = (from c in datos
                                        select c).ToList();

                        respuestaJson = consulta;
                        return Ok(respuestaJson);
                    }

                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
        [Route("api/Canales/3/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}/{cldoc}/{usuario}/{ip}/{origenBankingly}")]
        [Route("api/Canales/3/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}")]
        public ActionResult GetCredito(string tipoRespuesta, string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision, string? cldoc, string? usuario, string? ip, string? origenBankingly)
        {
            
            if (cldoc == null && usuario == null && ip == null && origenBankingly == null)
            {
                cldoc = "0";
                usuario = "0";
                ip = "0";
                origenBankingly = "0";
            }
            DateTime fecha = DateTime.Now;
            string llave = numeroCuenta + fecha.ToString("hhmmssfff") + numeroComprobante;
            string tipoTransaccion = "1901";
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@canalesTipo", '3', DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroCuenta", numeroCuenta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroComprobante", numeroComprobante, DbType.String, ParameterDirection.Input);
                    parametros.Add("@motivoContable", motivoContable, DbType.String, ParameterDirection.Input);
                    parametros.Add("@monto", Convert.ToDecimal(monto), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@montoComision", Convert.ToDecimal(montoComision), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@motivoComision", motivoComision, DbType.String, ParameterDirection.Input);
                    parametros.Add("@tipoTransaccion", tipoTransaccion, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origen", tipoRespuesta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cldoc", cldoc, DbType.String, ParameterDirection.Input);
                    parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                    parametros.Add("@ip", ip, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origenBankingly", origenBankingly, DbType.String, ParameterDirection.Input);

                    TRXCANALES("TRXCANALES_ADD", parametros);

                    var param_con = new DynamicParameters();
                    param_con.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    param_con.Add("@codigoError", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@mensajeCola", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoContable", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoDisponible", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();
                    datos = TRXCANALES("TRXCANALES_CON", param_con);
                    while (datos.SingleOrDefault().codigoError == null)
                    {
                        datos = TRXCANALES("TRXCANALES_CON", param_con);

                    }
                    if (tipoRespuesta == "1")
                    {
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();
                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12, '0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');

                        return Ok(respuestaXml);
                    }
                    else if (tipoRespuesta == "2")
                    {
                        var consulta = (from c in datos
                                        select c).ToList();

                        respuestaJson = consulta;
                        return Ok(respuestaJson);
                    }

                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/4/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}/{transaccionCredito}/{cuentaCredito}/{comisionCredito}/{cldoc}/{ip}/{usuario}/{origenBankingly}")]
        [Route("api/Canales/4/{tipoRespuesta}/{numeroCuenta}/{numeroComprobante}/{motivoContable}/{monto}/{montoComision}/{motivoComision}/{transaccionCredito}/{cuentaCredito}/{comisionCredito}")]
        public ActionResult GetTransaccion(string tipoRespuesta, string numeroCuenta, string numeroComprobante, string motivoContable, string monto, string montoComision, string motivoComision, string transaccionCredito, string cuentaCredito, string comisionCredito, string? cldoc, string? usuario, string? ip, string? origenBankingly)
        {
            if (cldoc == null && usuario == null && ip == null && origenBankingly == null)
            {
                cldoc = "0";
                usuario = "0";
                ip = "0";
                origenBankingly = "0";
            }
            DateTime fecha = DateTime.Now;
            string llave = numeroCuenta + fecha.ToString("hhmmssfff") + numeroComprobante;
            string tipoTransaccion = "1902";
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (numeroCuenta != null && numeroComprobante != null && motivoContable != null && montoComision != null)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@canalesTipo", '4', DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroCuenta", numeroCuenta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroComprobante", numeroComprobante, DbType.String, ParameterDirection.Input);
                    parametros.Add("@motivoContable", motivoContable, DbType.String, ParameterDirection.Input);
                    parametros.Add("@monto", Convert.ToDecimal(monto), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@montoComision", Convert.ToDecimal(montoComision), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@motivoComision", motivoComision, DbType.String, ParameterDirection.Input);
                    parametros.Add("@transaccionCredito",transaccionCredito, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cuentaCredito", cuentaCredito, DbType.String, ParameterDirection.Input);
                    parametros.Add("@comisionCredito", comisionCredito, DbType.String, ParameterDirection.Input);
                    parametros.Add("@tipoTransaccion", tipoTransaccion, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origen", tipoRespuesta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cldoc", cldoc, DbType.String, ParameterDirection.Input);
                    parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                    parametros.Add("@ip", ip, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origenBankingly", origenBankingly, DbType.String, ParameterDirection.Input);

                    TRXCANALES("TRXCANALES_ADD", parametros);

                    var param_con = new DynamicParameters();
                    param_con.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    param_con.Add("@codigoError", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@mensajeCola", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoContable", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoDisponible", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();
                    datos = TRXCANALES("TRXCANALES_CON", param_con);
                    while (datos.SingleOrDefault().codigoError == null)
                    {
                        datos = TRXCANALES("TRXCANALES_CON", param_con);

                    }

                    if (tipoRespuesta == "1")
                    {
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();
                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12, '0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');

                        return Ok(respuestaXml);
                    }
                    else if (tipoRespuesta == "2")
                    {
                        var consulta = (from c in datos
                                        select c).ToList();

                        respuestaJson = consulta;
                        return Ok(respuestaJson);
                    }

                }
                return Ok("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [Route("api/Canales/5/{tipoRespuesta}/{numeroPrestamo}/{monto}/{cuentaDebito}/{motivoDebito}/{cldoc}/{usuario}/{ip}/{origenBankingly}")]
        [Route("api/Canales/5/{tipoRespuesta}/{numeroPrestamo}/{monto}/{cuentaDebito}/{motivoDebito}")]
        public ActionResult GetPrestamo(string tipoRespuesta,string numeroPrestamo, string monto, string cuentaDebito, string motivoDebito, string? cldoc, string? usuario, string? ip, string? origenBankingly)
        {

            if (cldoc == null && usuario == null && ip == null && origenBankingly == null)
            {
                cldoc = "0";
                usuario = "0";
                ip = "0";
                origenBankingly = "0";
            }
            DateTime fecha = DateTime.Now;
            string tipoTransaccion = "3500";
            string transaccionPagoPrestamo = "3501";
            string llave = numeroPrestamo + fecha.ToString("hhmmssfff") + tipoTransaccion+transaccionPagoPrestamo;
            try
            {
                string respuestaXml = "";
                List<CanalesElectronicos> respuestaJson = new List<CanalesElectronicos>();
                if (numeroPrestamo != null && monto != null && cuentaDebito != null && motivoDebito != null)
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@canalesTipo", '5', DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroCuenta", cuentaDebito, DbType.String, ParameterDirection.Input);
                    parametros.Add("@motivoContable", motivoDebito, DbType.String, ParameterDirection.Input);
                    parametros.Add("@monto", Convert.ToDecimal(monto), DbType.Decimal, ParameterDirection.Input);
                    parametros.Add("@tipoTransaccion", tipoTransaccion, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origen", tipoRespuesta, DbType.String, ParameterDirection.Input);
                    parametros.Add("@numeroPrestamo", numeroPrestamo, DbType.String, ParameterDirection.Input);
                    parametros.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    parametros.Add("@transaccionPagoPrestamo",transaccionPagoPrestamo, DbType.String, ParameterDirection.Input);
                    parametros.Add("@cldoc", cldoc, DbType.String, ParameterDirection.Input);
                    parametros.Add("@usuario", usuario, DbType.String, ParameterDirection.Input);
                    parametros.Add("@ip", ip, DbType.String, ParameterDirection.Input);
                    parametros.Add("@origenBankingly", origenBankingly, DbType.String, ParameterDirection.Input);

                    TRXCANALES("TRXCANALES_ADD", parametros);

                    var param_con = new DynamicParameters();
                    param_con.Add("@llave", llave, DbType.String, ParameterDirection.Input);
                    param_con.Add("@codigoError", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@mensajeCola", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoContable", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    param_con.Add("@saldoDisponible", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    List<CanalesElectronicos> datos = new List<CanalesElectronicos>();
                    datos = TRXCANALES("TRXCANALES_CON", param_con);
                    while (datos.SingleOrDefault().codigoError == null)
                    {
                        datos = TRXCANALES("TRXCANALES_CON", param_con);

                    }

                    if (tipoRespuesta == "1")
                    {
                        var consulta = (from c in datos
                                        select c).SingleOrDefault();
                        respuestaXml = consulta.codigoError + consulta.mensajeCola + consulta.saldoContable.ToString().PadLeft(12, '0') + consulta.saldoDisponible.ToString().PadLeft(12, '0');

                        return Ok(respuestaXml);
                    }
                    else if (tipoRespuesta == "2")
                    {
                        var consulta = (from c in datos
                                        select c).ToList();

                        respuestaJson = consulta;
                        return Ok(respuestaJson);
                    }

                }
                return Ok("Error");
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
            public string? saldoContable { get; set; }
            public string? saldoDisponible { get; set; }
            public string numeroPrestamo { get; set; }
            public string transaccionPagoPrestamo { get; set; }

        }

        
        public IDbConnection GetConnection()
        {

            var conn = new SqlConnection(conexionCert);
            return conn;
        }

        public List<CanalesElectronicos> TRXCANALES(string procedimiento,DynamicParameters parametros)
        {
            List<CanalesElectronicos> respuesta = new List<CanalesElectronicos>();
            var conn = this.GetConnection();
            object result = null;
            try
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        var query = procedimiento;

                        result = SqlMapper.Query(conn, query, param: parametros, commandType: CommandType.StoredProcedure);
                        if (parametros.ParameterNames.Count()==5)
                        {
                            var codigoError = parametros.Get<string>("codigoError");
                            var mensajeCola = parametros.Get<string>("mensajeCola");
                            var saldoContable = parametros.Get<string>("saldoContable");
                            var saldoDisponible = parametros.Get<string>("saldoDisponible");


                            respuesta.Add(new CanalesElectronicos()
                            {
                                codigoError = codigoError,
                                mensajeCola = mensajeCola,
                                saldoContable =saldoContable,
                                saldoDisponible = saldoDisponible

                            });
                            return respuesta;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                return respuesta;
            }
            catch (Exception e)
            {

                throw e;
            }


        }
    }

}
