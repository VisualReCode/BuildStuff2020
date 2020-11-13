using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace RoslynDemo
{
    internal class NamedTypeSymbolEqualityComparer : IEqualityComparer<INamedTypeSymbol>
    {
        public static readonly NamedTypeSymbolEqualityComparer Instance = new();

        private NamedTypeSymbolEqualityComparer()
        {
            
        }
        
        public bool Equals(INamedTypeSymbol? x, INamedTypeSymbol? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Name == y.Name && NamespaceSymbolEqualityComparer.Instance.Equals(x.ContainingNamespace, y.ContainingNamespace);
        }

        public int GetHashCode(INamedTypeSymbol obj) =>
            HashCode.Combine(obj.Name, NamespaceSymbolEqualityComparer.Instance.GetHashCode(obj.ContainingNamespace));
    }
}