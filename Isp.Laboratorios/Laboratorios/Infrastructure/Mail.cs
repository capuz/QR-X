using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Isp.Laboratorios.Infrastructure
{
    public static class Mail
    {
        public static MailMessage Generar(string de, string asunto, string destinatario, string mensaje, string copias = "", MailPriority prioridad = MailPriority.Normal)
        {
            var correo = new MailMessage();
            correo.To.Add(destinatario);
            correo.CC.Add(copias);
            correo.Subject = asunto;
            correo.Priority = prioridad;
            correo.SubjectEncoding = Encoding.UTF8;
            var htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

            htmlView.LinkedResources.Add(new LinkedResource(ApplicationInfo.RutaHeaderLogo) { ContentId = "logoIsp" });
            correo.AlternateViews.Add(htmlView);
            correo.BodyEncoding = Encoding.UTF8;
            correo.IsBodyHtml = true;
            correo.From = new MailAddress(de, ApplicationInfo.NombreSistema);

            return correo;
        }
        public static bool Enviar(MailMessage correo)
        {
            var cliente = new SmtpClient
            {
                Credentials = new NetworkCredential(ApplicationInfo.MailSistema, ConfigurationManager.AppSettings["MailPassword"]),
                Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]),
                EnableSsl = true,
                Host = ConfigurationManager.AppSettings["MailHost"]
            };
            try
            {
                cliente.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}