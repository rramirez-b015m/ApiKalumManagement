using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace KalumManagement.Helpers
{
    public class NoExpedienteAttribute : ValidationAttribute
    {
           protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if(value.ToString().Contains("-"))
            {
                value = value.ToString().ToUpper();
                int guion = value.ToString().IndexOf("-");
                string exp = value.ToString().Substring(0,guion);
                string numero = value.ToString().Substring(guion+1, value.ToString().Length -4);
                if(!exp.ToUpper().Equals("EXP") || !Information.IsNumeric(numero))
                {
                    return new ValidationResult("El numero de expediente no contiene la nomenclatura adecuada");
                }


            }
            else 
            {
                return new ValidationResult("El numero de expediente no contiene un '-' ");
            }

            return ValidationResult.Success;


        }
    }
}