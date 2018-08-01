using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmashMem
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<ShellView>();
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			Container.RegisterType(typeof(object), typeof(StreamView), "StreamView");
			Container.RegisterType(typeof(object), typeof(MemoryView), "MemoryView");
		}
	}
}
