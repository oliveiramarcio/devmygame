using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;

namespace Aplicacao.IoC
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return resolver.GetAll(serviceType);
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;

            this.AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }

        private void AddBindings()
        {
            this.kernel.Bind(x =>
            {
                x.FromThisAssembly()
               .SelectAllClasses()
               .BindDefaultInterface();
            });

            this.kernel.Bind(x =>
            {
                x.FromAssembliesMatching("Infra.dll")
               .SelectAllClasses()
               .BindDefaultInterface();
            });
        }
    }
}