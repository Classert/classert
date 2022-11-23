using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Collections.Immutable;
using System.Reflection;

namespace Classertion.Internal
{
    internal class ClassertTypeBuilder<T> : ITypeBuilder<T>
    {
        private static readonly Type _type = typeof(T);
        private static readonly string TypeName = $"{_type.Name}_{Guid.NewGuid():N}";

        private static Type _compiledType;

        private IClassertBuilder _classertBuilder;

        internal ClassertTypeBuilder(ClassertBuilder classertBuilder)
        {
            _classertBuilder = classertBuilder;
        }

        Type ITypeBuilder<T>.Type => _type;

        public T Build()
        {
            if (_compiledType == null)
            {
                var namespaceDeclaration = SyntaxFactory
                    .NamespaceDeclaration(SyntaxFactory.ParseName("ClassertGenerated"))
                    .AddUsings(
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_type.Namespace))
                    ).NormalizeWhitespace();

                var classDeclaration = SyntaxFactory.ClassDeclaration(TypeName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(_type.Name))
                    );

                namespaceDeclaration.AddMembers(classDeclaration);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddMembers(namespaceDeclaration);

                var references = LoadAssemblyReferences();

                var compiler = CSharpCompilation.Create(TypeName,
                    syntaxTrees: new[] { compilationUnit.SyntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                        .WithOptimizationLevel(OptimizationLevel.Release)
                        .WithOverflowChecks(true)
                        .WithUsings(
                            namespaceDeclaration.Usings.Select(u => u.Name.ToFullString())
                                .ToImmutableArray()
                        )
                );

                using var stream = new MemoryStream();
                var result = compiler.Emit(stream);

                if (!result.Success)
                {
                    throw new Exception(result.ToString());
                }

                var assembly = AppDomain.CurrentDomain.Load(stream.ToArray());
                _compiledType = assembly.GetType(TypeName);
            }

            return (T)Activator.CreateInstance(_compiledType);
        }

        private static List<MetadataReference> LoadAssemblyReferences()
        {
            var references = new List<MetadataReference>();

            references.Add(MetadataReference.CreateFromFile(_type.Assembly.Location));

            foreach (var reference in _type.Assembly.GetReferencedAssemblies())
            {
                var referenceFilePath = Path.Combine(Assembly.Load(reference).Location);
                var metadataReference = MetadataReference.CreateFromFile(referenceFilePath);

                references.Add(metadataReference);
            }

            return references;
        }
    }
}