using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Isp.Laboratorios.Infrastructure.ValidationAtributtes
{
    public class RutValido : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var rutSinPuntos = value.ToString().Replace(".", string.Empty);
            var rut = int.Parse(rutSinPuntos.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
            var digitoVerificador = rutSinPuntos.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];

            var contador = 2;
            var acumulador = 0;

            while (rut != 0)
            {
                var multiplo = (rut % 10) * contador;
                acumulador = acumulador + multiplo;
                rut = rut / 10;
                contador = contador + 1;
                if (contador == 8)
                    contador = 2;
            }

            var digito = 11 - (acumulador % 11);
            var rutDigito = digito.ToString(CultureInfo.InvariantCulture);
            if (digito == 10)
                rutDigito = "K";
            if (digito == 11)
                rutDigito = "0";

            return String.Equals(rutDigito, digitoVerificador, StringComparison.CurrentCultureIgnoreCase);
        }
    } 
}