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
                return DateTime.ParseExact(dateFormatIso8601, new[] { "yyyyMMddTHHmmss+0100" }, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
            catch (FormatException ex)
            {
                throw new ErrorParseException("{dateFormatIso8601} est invalide", ex);
            }            
        }
    }
}