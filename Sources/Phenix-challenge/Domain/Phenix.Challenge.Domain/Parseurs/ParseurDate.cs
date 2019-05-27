using Phenix.Challenge.Domain.Exceptions;
using System;

namespace Phenix.Challenge.Domain.Parseurs
{
    public static class ParseurDate
    {
        public static DateTime Parse(string dateFormatIso8601)
        {
            try
            {
                //14/01/2019 00:00:00
                return DateTime.ParseExact(dateFormatIso8601, new[] { "yyyyMMddTHHmmss+0100", "yyyyMMddTHHmmss+0200",
                "dd/MM/yyyy HH:mm:ss"}, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
            catch (FormatException ex)
            {
                throw new ErrorParseException("{dateFormatIso8601} est invalide", ex);
            }            
        }
    }
}