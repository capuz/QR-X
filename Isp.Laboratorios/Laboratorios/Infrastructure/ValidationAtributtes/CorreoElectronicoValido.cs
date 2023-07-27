using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Isp.Laboratorios.Infrastructure.ValidationAtributtes
{
    public class CorreoElectronicoValido : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(), @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
    }
}