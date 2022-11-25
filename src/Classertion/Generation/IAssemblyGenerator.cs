using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace Classertion.Generation
{
    public interface IAssemblyGenerator
    {
        CSharpCompilation CompileAssembly<T>(IClassert<T> target, CompilationUnitSyntax compilation, ImmutableArray<string> namespaces) where T : class;

        NamespaceDeclarationSyntax GetNamespaceDeclarations<T>(IClassert<T> target) where T : class;

        IEnumerable<MetadataReference> LoadAssemblyReferenceses<T>(IClassert<T> target) where T : class;

        ClassDeclarationSyntax GetClassDeclaration<T>(IClassert<T> target) where T : class;

        ClassDeclarationSyntax ImplementatInterfaces<T>(IClassert<T> target, ClassDeclarationSyntax classDeclaration) where T : class;
    }
}