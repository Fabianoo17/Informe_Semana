using Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Core.Helpers.Validation
{
    public class MatriculaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var matricula = value as string;

            if (string.IsNullOrEmpty(matricula))
                return true;

            var ADService = new ADService();
            return ADService.ValidaMatricula(matricula);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("O campo {0} deve ser uma matrícula válida.", name);
        }
    }
}