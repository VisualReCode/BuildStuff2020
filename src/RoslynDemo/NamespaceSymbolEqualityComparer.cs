using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace RoslynDemo
{
    internal class NamespaceSymbolEqualityComparer : IEqualityComparer<INamespaceSymbol>
    {
        public static readonly NamespaceSymbolEqualityComparer Instance = new();
        
        public bool Equals(INamespaceSymbol? x, INamespaceSymbol? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Name == y.Name && Instance.Equals(x.ContainingNamespace, y.ContainingNamespace);
        }

        public int GetHashCode(INamespaceSymbol obj)
        {
            if (obj.IsGlobalNamespace) return 0;
            const int mod = 16777619;
            return obj.Name.GetHashCode() * mod + Instance.GetHashCode(obj.ContainingNamespace);
        }
    }
}