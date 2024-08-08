using Mono.Cecil;
using NetArchTest.Rules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.ArchitecturalTests.CustomRules
{
    internal class RepositoryInterfaceIsInAggregateNamespace : ICustomRule
    {
        public bool MeetsRule(TypeDefinition type)
        {
            string lastNamespacePart = type.Namespace.Split('.').LastOrDefault() ?? "";
            string aggregateName = type.Name.Replace("Repository", "").Substring(1);

            return lastNamespacePart.StartsWith(aggregateName);     }
    }
}
