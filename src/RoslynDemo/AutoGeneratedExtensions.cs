﻿using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynDemo
{
    public static class AutoGeneratedExtensions
    {
        public static bool IsAutoGenerated(this Document document)
        {
            var fileName = Path.GetFileNameWithoutExtension(document.FilePath.AsSpan());
            return fileName.StartsWith("TemporaryGeneratedFile_", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".designer", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".generated", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".g", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".gi", StringComparison.OrdinalIgnoreCase);
        }

        public static bool BeginsWithAutoGeneratedComment(this SyntaxNode root)
        {
            if (root.HasLeadingTrivia)
            {
                var commentTrivia = root.GetLeadingTrivia()
                    .Where(t => t.IsKind(SyntaxKind.MultiLineCommentTrivia) || t.IsKind(SyntaxKind.SingleLineCommentTrivia));
                
                foreach (var trivia in commentTrivia)
                {
                    var text = trivia.ToString().AsSpan();
                    if (text.Contains("autogenerated", StringComparison.OrdinalIgnoreCase) ||
                        text.Contains("auto-generated", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}