using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Utils
{
    public static class StringHelper
    {
        public static string GerarSlug(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto)) return "";

            var partes = nomeCompleto.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Pega o primeiro e o último nome
            string primeiro = partes[0];
            string ultimo = partes.Length > 1 ? partes[^1] : "";

            string resultado = (primeiro + ultimo).ToLower();

            // Remove acentos
            return new string(resultado
                .Normalize(System.Text.NormalizationForm.FormD)
                .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
                            System.Globalization.UnicodeCategory.NonSpacingMark)
                .ToArray()).Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
