using System;
using System.Reflection;
using Mono.Cecil;

namespace Conventional.Conventions.Cecil
{
    public class ConventionalAssemblyResolver : IAssemblyResolver
    {
        private const string FileSchemePrefix = "file:///";

        public virtual AssemblyDefinition Resolve(string fullName)
        {
            return Resolve(fullName, new ReaderParameters());
        }

        public virtual AssemblyDefinition Resolve(string fullName, ReaderParameters parameters)
        {
            if (fullName == null)
                throw new ArgumentNullException("fullName");

            return Resolve(AssemblyNameReference.Parse(fullName), parameters);
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            return Resolve(name, new ReaderParameters());
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var assembly = Assembly.Load(name.Name);

            return AssemblyDefinition.ReadAssembly(GetPathToAssembly(assembly), new ReaderParameters { AssemblyResolver = this });
        }

        private static string GetPathToAssembly(Assembly assembly)
        {
            return new Uri(assembly.CodeBase).LocalPath;
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}