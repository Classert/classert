using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace Classertion.Verification.Internal
{
    internal class ClassertTypeBuilder : ITypeBuilder
    {
        private readonly IAssemblyGenerator _assemblyGenerator;
        private readonly Action<SetupArgs>? _constructorArguments;

        internal ClassertTypeBuilder(IAssemblyGenerator generator, Action<SetupArgs>? argsProvider = null)
        {
            _assemblyGenerator = generator;
            _constructorArguments = argsProvider;
        }

        public T? BuildType<T>(IClassert<T> target) where T : class
        {
            if (target.GetCompiledType() == null)
            {
                var classDeclaration = _assemblyGenerator.GetClassDeclaration(target);
                var namespaceDeclaration = _assemblyGenerator.GetNamespaceDeclarations(target);

                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .AddMembers(namespaceDeclaration.AddMembers(classDeclaration.NormalizeWhitespace())
                    .NormalizeWhitespace());

                var namespaces = namespaceDeclaration.Usings
                    .Select(u => u.Name.ToFullString())
                    .ToImmutableArray();

                var compiler = _assemblyGenerator.CompileAssembly(target, compilationUnit, namespaces);

                using (var stream = new MemoryStream())
                {
                    var result = compiler.Emit(stream);

                    if (!result.Success)
                    {
                        throw new Exception(result.ToString());
                    }

                    var assembly = AppDomain.CurrentDomain.Load(stream.ToArray());

                    target.SetCompiledType(assembly);

                    if (target.GetCompiledType() == null)
                    {
                        throw new ApplicationException($"Failed to generate type for: '{target.TypeName}'");
                    }
                }
            }

            if (_constructorArguments != null)
            {
                var args = new SetupArgs();
                _constructorArguments.Invoke(args);

                return Activator.CreateInstance(target.GetCompiledType(), args.Args) as T;
            }

            return Activator.CreateInstance(target.GetCompiledType()) as T;
        }
    }
}