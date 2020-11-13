using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynDemo
{
    static class SymbolVisitorDemo
    {
        public static async Task ListAsync(Solution solution)
        {
            var visitor = new ListTypesVisitor();
            
            foreach (var project in solution.Projects)
            {
                var compilation = await project.GetCompilationAsync();

                compilation?.GlobalNamespace.Accept(visitor);
            }
        }
    }
}