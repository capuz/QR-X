using System;
using System.Web.Mvc;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using System.Collections.Generic;
using Isp.Laboratorios.Models.ViewModels;
using AutoMapper;
using Isp.Laboratorios.Models.Enums;
using Microsoft.Security.Application;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Text;
using System.Net.Mail;


namespace Isp.Laboratorios.Controllers
{
    public class MuestraObservacionesController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;

        public ActionResult Obtener(int muestraId, string muestraCodigo) //Carga la vista de Acciones
        {

            var observaciones = _db.MuestraObservaciones.ObtenerPorMuestraId(muestraId);
            ViewBag.MuestraCodigo = muestraCodigo;
            ViewBag.MuestraId = muestraId;
            ViewBag.TiposObservacionId = new SelectList(_db.TiposObservaciones.ObtenerTodo(), "Id", "Nombre");
            ViewBag.EstadoIncidenteId = new SelectList(_db.EstadosIncidentes.ObtenerTodo(), "Id", "Nombre");

            return PartialView("_ObtenerMuestraObservacionModal", observaciones);
        }
        [HttpPost]
        public ActionResult Guardar(int MuestraId, string Observacion, int TipoObservacionId, int EstadoIncidenteId) //Accion del boton Guardar
        {
            var mo = new MuestraObservacion
            {
                MuestraId = MuestraId,
                UsuarioId = ApplicationInfo.CurrentUser.Id,
                Observacion = Observacion,
                TipoObservacionId = TipoObservacionId,
                Fecha = DateTime.Now,
                EstadoIncidenteId = EstadoIncidenteId

            };
            if (EstadoIncidenteId == 1)
            {
                mo.UsuarioIdCierre = ApplicationInfo.CurrentUser.Id;
                mo.FechaCierre = DateTime.Now;

            }
            else
            {
                mo.UsuarioIdCierre = null;
                mo.FechaCierre = null;
            }
            //agregar UsuarioCierreId por si es el caso
            //agregar

            _db.MuestraObservaciones.Insertar(mo);
            _db.GuardarCambios();


            //
            var muestraCodigo = _db.Muestras.ObtenerCodigoPorId(MuestraId);
            return RedirectToAction("Obtener", new { MuestraId, muestraCodigo, EstadoIncidenteId });
        }

        [HttpPost]
        public ActionResult ActualizarEstado(int MuestraObservacionId, int EstadoIncidenteId, string Observacion, int MuestraId, bool enviarMail = true) //Accione del boton Actualizar y Enviar Email
        {
           // actualizar campos
            MuestraObservacion muestraobservacion = _db.MuestraObservaciones.ObtenerPorId(MuestraObservacionId);
            muestraobservacion.EstadoIncidenteId = EstadoIncidenteId;
            muestraobservacion.Observacion = Observacion;
            muestraobservacion.UsuarioIdCierre = ApplicationInfo.CurrentUser.Id;
            muestraobservacion.FechaCierre = DateTime.Now;
            
            if (EstadoIncidenteId == 1)
            {
                muestraobservacion.UsuarioIdCierre = ApplicationInfo.CurrentUser.Id;
                muestraobservacion.FechaCierre = DateTime.Now;
            }
            else
            {
                muestraobservacion.UsuarioIdCierre = null;
                muestraobservacion.FechaCierre = null;
            }
            _db.MuestraObservaciones.Actualizar(muestraobservacion);
            var muestraCodigo = _db.Muestras.ObtenerCodigoPorId(MuestraId);

            if (enviarMail == true)
            {
                EnviarMail(MuestraId, Observacion);
            }
            return RedirectToAction("Obtener", new { MuestraId, muestraCodigo, EstadoIncidenteId, enviarMail});

        }

        public void EnviarMail(int muestraId, string Observacion)
        {
            var muestra = _db.Muestras.ObtenerPorId(muestraId);
            var laboratorio = _db.Laboratorios.ObtenerPorMuestraId(muestraId);
            var responsableRtm = _db.Usuarios.ObtenerResponsableRtm();

            string ruta = ApplicationInfo.PlantillaIncidencia;
            System.IO.File.ReadAllText(ruta, Encoding.UTF8);
            string htmlString = System.IO.File.ReadAllText(ruta, Encoding.UTF8);

            var tokens = new Dictionary<string, string>
                                                {
                                                    {"[SistemaNombre]",ApplicationInfo.NombreSistema},
                                                    {"[UsuarioOrigen]",_usuario.Nombre + " " +_usuario.ApellidoPaterno},
                                                    {"[UsuarioDestino]",responsableRtm.Nombre + " "+responsableRtm.ApellidoPaterno},
                                                    {"[MuestraCodigo]",muestra.Codigo},
                                                    {"[LaboratorioNombre]",laboratorio.Nombre},
                                                    {"[ProcedenciaNombre]",muestra.Solicitud.Procedencia.Nombre},
                                                    {"[Comentario]",Observacion}
                                                };

          
                    var mensaje = Util.ReplaceTokens(htmlString, tokens);

                    string asunto = "Notificación de Incidencia de la Muestra :" + muestra.Codigo + " " + ApplicationInfo.NombreSistema;

                    var mail = Mail.Generar(ApplicationInfo.MailSistema, asunto, responsableRtm.CorreoElectronico, mensaje, laboratorio.Seccion.ResponsableMail, MailPriority.High);

                    Mail.Enviar(mail);
        
        }


        [HttpPost]
        public ActionResult ExportarExcel( int? MuestraId) //Accion del boton Descargar
        {

            var consultaexportar = _db.MuestraObservaciones.ObtenerPorMuestraId((int)MuestraId);

            int tamañoFuente = 8;
            int filaInicio = 8;
            DateTime fecha = DateTime.Now;
            var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet 1");

            worksheet.Cell(filaInicio, 1).Value = "Nº MUESTRA" + "\r\n";
            worksheet.Cell(filaInicio, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 1).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 1).Style.Font.Bold = true;

            worksheet.Cell(filaInicio, 2).Value = "PROFESIONAL QUE INFORMA" + "\r\n";
            worksheet.Cell(filaInicio, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 2).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 2).Style.Font.Bold = true;

            worksheet.Cell(filaInicio, 3).Value = "OBSERVACION" + "\r\n";
            worksheet.Cell(filaInicio, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 3).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 3).Style.Font.Bold = true;

            worksheet.Cell(filaInicio, 4).Value = "ESTADO" + "\r\n";
            worksheet.Cell(filaInicio, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 4).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 4).Style.Font.Bold = true;

            worksheet.Cell(filaInicio, 5).Value = "USUARIO DE CIERRE" + "\r\n";
            worksheet.Cell(filaInicio, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 5).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 5).Style.Font.Bold = true;

            worksheet.Cell(filaInicio, 6).Value = "FECHA DE CIERRE" + "\r\n";
            worksheet.Cell(filaInicio, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(filaInicio, 6).Style.Font.FontSize = tamañoFuente;
            worksheet.Cell(filaInicio, 6).Style.Font.Bold = true;

            var titulo = worksheet.Range("B2:G2");
            var tituloDepto = worksheet.Range("B3:G3");
            var fechaImpresion = worksheet.Range("D4:G4");
            var rangofechas = worksheet.Range("B6:H6");

            titulo.Row(1).Merge();
            tituloDepto.Row(1).Merge();
            fechaImpresion.Row(1).Merge();
            rangofechas.Row(1).Merge();
            titulo.Value = "SOLICITUDES EN SISTEMA";
            titulo.Style.Font.Bold = true;
            //tituloDepto.Value = "Emitido por: Oficina de Partes";
            tituloDepto.Style.Font.Bold = true;
            fechaImpresion.Value = "Fecha Emisión: " + String.Format("{0:dd-MM-yyyy}", fecha);
            //rangofechas.Value = "Fecha Inicio: " + String.Format("{0:dd-MM-yyyy}", fechainicio1) + " - " + "Fecha Final: " + String.Format("{0:dd-MM-yyyy}", fechafinal1);
            worksheet.Row(2).AdjustToContents(21.00, 21.00);
            worksheet.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Row(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Row(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            worksheet.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int contador = filaInicio + 1;

            foreach (MuestraObservacion item in consultaexportar)
            {
                worksheet.Cell(contador, 1).Value = item.MuestraId;
                worksheet.Cell(contador, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(contador, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(contador, 1).Style.Font.FontSize = tamañoFuente;

                worksheet.Cell(contador, 2).Value = item.Usuario.Nombre + item.Usuario.ApellidoPaterno + "\r\n";
                worksheet.Cell(contador, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(contador, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                worksheet.Cell(contador, 2).Style.Font.FontSize = tamañoFuente;

                worksheet.Cell(contador, 3).Value = item.Observacion + "\r\n"; // corresponde  al solicitante
                worksheet.Cell(contador, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(contador, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                worksheet.Cell(contador, 3).Style.Font.FontSize = tamañoFuente;

                worksheet.Cell(contador, 4).Value = item.EstadoIncidenteId + "\r\n";
                worksheet.Cell(contador, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(contador, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                worksheet.Cell(contador, 4).Style.Font.FontSize = tamañoFuente;

                worksheet.Cell(contador, 5).Value = item.UsuarioIdCierre + "\r\n";
                worksheet.Cell(contador, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(contador, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                worksheet.Cell(contador, 5).Style.Font.FontSize = tamañoFuente;

                worksheet.Cell(contador, 6).Value = item.FechaCierre + "\r\n";
                worksheet.Cell(contador, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(contador, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(contador, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                worksheet.Cell(contador, 6).Style.Font.FontSize = tamañoFuente;

                contador++;
            }


            worksheet.Column(1).AdjustToContents(7.29, 7.29);
            worksheet.Column(2).AdjustToContents(7.57, 7.57);
            worksheet.Column(3).AdjustToContents(13.14, 13.14);
            worksheet.Column(4).AdjustToContents(8.86, 8.86);
            worksheet.Column(5).AdjustToContents(8.14, 8.14);
            worksheet.Column(6).AdjustToContents(12.14, 12.14);


            worksheet.PageSetup.Header.Left.AddText("SISTEMA LABORATORIO");

            //string imagePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/img/logo.png");
            //worksheet.PageSetup.Header.Right.AddImage(imagePath, XLHFOccurrence.AllPages);


            //worksheet.PageSetup.Header.Center.AddText("Fecha: " + String.Format("{0:dd-MM-yyyy}", fecha));
            worksheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.PageNumber, XLHFOccurrence.AllPages);
            worksheet.PageSetup.Header.Right.AddText(" / ", XLHFOccurrence.AllPages);
            worksheet.PageSetup.Header.Right.AddText(XLHFPredefinedText.NumberOfPages, XLHFOccurrence.AllPages);

            DateTime dt = DateTime.Now;
            MemoryStream ms = new MemoryStream();
            //workbook.SaveAs("c:\\temp\\descargaxcel " + String.Format("{0:d-M-yyyy HH.mm.ss}", dt) + ".xlsx");
            workbook.SaveAs(ms);
            ms.Seek(0, SeekOrigin.Begin);

            return File(ms, @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml", "consultaexportar" + String.Format("{0:d-M-yyyy HH.mm.ss}", dt) + ".xlsx");
          
        }
    }
}