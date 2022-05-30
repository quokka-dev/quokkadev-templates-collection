using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.ArchitecturalTests
{
    internal static class Extensions
    {
        public static string GetOffendingTypes(this TestResult result)
        {
            var types = result.IsSuccessful ? new string[] { } : result.FailingTypeNames;
            return string.Join(", ", types);
        }
    }
}
