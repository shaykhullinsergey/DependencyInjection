﻿using System;
using DependencyInjection.Core;

namespace DependencyInjection
{
	public class ContainerConfig : IContainerConfig
	{
		private IContainer container;
		private readonly DependencyContainer dependencies;

		public ContainerConfig()
		{
			dependencies = new DependencyContainer(null);
		}

		internal ContainerConfig(ContainerConfig parent) 
		{
			dependencies = new DependencyContainer(parent.dependencies);
		}

		public IImplementedByBuilder<TRegister> Register<TRegister>()
			where TRegister : class =>
				new RegisterBuilder(dependencies)
					.Register<TRegister>();

		public IImplementedByBuilder<object> Register(Type register) =>
			new RegisterBuilder(dependencies)
				.Register(register);

		public IContainerConfig Scope() => new ContainerConfig(this);

		public IContainer Container => container ?? (container = new Container(dependencies));

		public void Dispose() { }
	}
}