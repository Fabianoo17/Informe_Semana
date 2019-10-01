using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Core.Helpers.Validation
{
    public class MaxLengthWithoutHtmlAttribute : ValidationAttribute
    {
        private readonly int _max;

        public MaxLengthWithoutHtmlAttribute(int max)
        {
            _max = max;
        }

        public override bool IsValid(object value)
        {
            var valueInput = value as string;

            if (string.IsNullOrEmpty(valueInput))
            {
                return true;
            }

            var parametro = @"<[^>]*>";
            var rgx = new Regex(parametro, RegexOptions.IgnoreCase);
            var newValue = rgx.Replace(valueInput, "");

            if (newValue.Length <= _max)
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("O campo {0} deve ter no máximo {1} caracteres", name, _max);
        }
    }
}