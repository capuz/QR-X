using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Infrastructure.Security.Authorization;
using Isp.Laboratorios.Infrastructure.Security.Encrypting;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Authentication;
using Membership = Isp.Laboratorios.Infrastructure.Security.Authentication.Membership;

namespace Isp.Laboratorios.Controllers
{
    public class CuentasController : Controller
    {
        readonly UnitOfWork _db = new UnitOfWork();

        public ActionResult IniciarSesion(Login login)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidarUsuario(login))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, false);

                    return RedirectToAction("Index", "Home");

                }
                Alert.Information(this, "Nombre de usuario o contraseña no válidos");
            }
            login.UserName = string.Empty;
            login.Password = string.Empty;
            return View(login);
        }
        [RequiresAuthorization]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("IniciarSesion");
        }
        public ActionResult DesplegarMenu()
        {
            List<Funcionalidad> funcionalidades = Membership.ObtenerFuncionalidades(ApplicationInfo.CurrentUser.Id);
            ViewBag.NombreCompleto = Membership.ObtenerNombreUsuarioCompleto();

            return View(funcionalidades);
        }
        [RequiresAuthorization]
        public ActionResult CambiarContrasenaFea()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarContrasenaFea(CambioContrasena cambioContrasena)
        {
            if (ModelState.IsValid)
            {
                var usuario = ApplicationInfo.CurrentUser;

                if (usuario.ContrasenaFEA.Equals(Sha1.Encrypt(cambioContrasena.ContrasenaActual)))
                {
                    usuario.ContrasenaFEA = Sha1.Encrypt(cambioContrasena.ContrasenaNueva);
                    _db.Usuarios.Actualizar(usuario);
                    _db.GuardarCambios();
                    Alert.Success(this, "La contraseña de Firma Electrónica se actualizó correctamente");
                    return View(new CambioContrasena());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La contraseña actual ingresada no coincide con su contraseña de Firma Electrónica");
                }
            }
            Alert.Danger(this, Alert.GetAllErrorModel(ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors)));
            return View(new CambioContrasena());

        }
        [RequiresAuthorization]
        public ActionResult CambiarContrasena()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarContrasena(CambioContrasena cambioContrasena)
        {
            if (ModelState.IsValid)
            {
                var usuario = ApplicationInfo.CurrentUser;

                if (usuario.Contrasena.Equals(Sha1.Encrypt(cambioContrasena.ContrasenaActual)))
                {
                    usuario.Contrasena = Sha1.Encrypt(cambioContrasena.ContrasenaNueva);
                    _db.Usuarios.Actualizar(usuario);
                    _db.GuardarCambios();
                    Alert.Success(this, "La contraseña se actualizó correctamente");
                    return View(new CambioContrasena());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La contraseña actual ingresada no coincide con su contraseña");
                }
            }
            Alert.Danger(this, Alert.GetAllErrorModel(ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors)));
            return View(new CambioContrasena());

        }
        public ActionResult RecuperarContrasena()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecuperarContrasena(string correo)
        {
            var usuario = _db.Usuarios.ObtenerPorCorreo(correo);

            if (usuario != null)
            {
                string ruta = ApplicationInfo.PlantillaCambioContrasena;
                System.IO.File.ReadAllText(ruta, Encoding.UTF8);
                string htmlString = System.IO.File.ReadAllText(ruta, Encoding.UTF8);
                var fechaValidez = DateTime.Now.ToShortDateString();
                var bytes = Encoding.UTF8.GetBytes(fechaValidez + usuario.CorreoElectronico);
                var base64 = Convert.ToBase64String(bytes);

                var tokens = new Dictionary<string, string>
                                                {
                                                    {"[SistemaNombre]",ApplicationInfo.NombreSistema},
                                                    {"[Usuario]", usuario.Nombre},
                                                    {"[FechaValidez]",fechaValidez},
                                                    {"[UrlCambioContrasena]",ApplicationInfo.UrlCambioContrasena +base64}
                                                };

                var mensaje = Util.ReplaceTokens(htmlString, tokens);
                string asunto = "Recuperar Contraseña Sistema: " + ApplicationInfo.NombreSistema;
                var mail = Mail.Generar(ApplicationInfo.MailSistema, asunto, usuario.CorreoElectronico, mensaje);

                if (Mail.Enviar(mail))
                {
                    Alert.Success(this, "Se ha enviado un correo electrónico con instrucciones para recuperar su contraseña", false);
                    return RedirectToAction("IniciarSesion");
                }

                Alert.Danger(this, "Ha ocurrido un error al enviar correo electrónico");
                return View();

            }
            Alert.Information(this, "El correo ingresado no se encuentra registrado");
            return View();
        }

        public ActionResult NuevaContrasena(string recover)
        {
            var nuevaContrasena = new NuevaContrasena();
            try
            {
                var bytes = Convert.FromBase64String(recover);
                var cadena = Encoding.UTF8.GetString(bytes);

                var fecha = cadena.Substring(0, 10);

                if (fecha == DateTime.Now.ToShortDateString())
                {
                    nuevaContrasena.DataEnlace = cadena.Substring(10);
                }
                else
                {
                    Alert.Information(this, "El enlace de recuperación caducó, debe realizar otra solicitud de recuperación de contraseña", false);
                    return RedirectToAction("IniciarSesion");
                }
            }
            catch (Exception)
            {
                Alert.Danger(this, "El enlace está dañado o no es válido", false);
                return RedirectToAction("IniciarSesion");
            }


            return View(nuevaContrasena);
        }

        [HttpPost]
        public ActionResult NuevaContrasena(NuevaContrasena nuevaContrasena)
        {
            if (ModelState.IsValid)
            {
                var usuario = _db.Usuarios.ObtenerPorCorreo(nuevaContrasena.Correo);
                usuario.Contrasena = Sha1.Encrypt(nuevaContrasena.ContrasenaNueva);
                _db.Usuarios.Actualizar(usuario);
                Alert.Success(this, "La contraseña se actualizó correctamente");
                return RedirectToAction("IniciarSesion");
            }

            Alert.Danger(this, Alert.GetAllErrorModel(ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors)));
            return View(nuevaContrasena);
        }
    }
}