using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace RoslynDemo
{
    public class ListTypesVisitor : SymbolVisitor
    {
        private readonly HashSet<INamedTypeSymbol> _seen = new HashSet<INamedTypeSymbol>(NamedTypeSymbolEqualityComparer.Instance);
        
        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (symbol.DeclaringSyntaxReferences.Length == 0) return;
            
            foreach (var namespaceOrTypeSymbol in symbol.GetMembers())
            {
                namespaceOrTypeSymbol.Accept(this);
            }
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.DeclaringSyntaxReferences.Length == 0) return;

            if (!_seen.Add(symbol)) return;

            Console.WriteLine($"{symbol.ContainingNamespace}.{symbol.Name}");
            
            foreach (var namedTypeSymbol in symbol.GetMembers())
            {
                namedTypeSymbol.Accept(this);
            }
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (!symbol.IsImplicitlyDeclared)
            {
                Console.WriteLine($"  {symbol.Name}");
            }
        }
    }
}