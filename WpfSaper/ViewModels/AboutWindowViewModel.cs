using System;
using System.Reflection;

namespace WpfSaper.ViewModels
{
    public class AboutWindowViewModel
    {
        private readonly Assembly assembly;

        public AboutWindowViewModel(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public AboutWindowViewModel() : this(Assembly.GetExecutingAssembly())
        {
        }        

        public string Description { get { return GetAssemblyAttribute<AssemblyDescriptionAttribute>().Description; } }

        public string Company { get { return GetAssemblyAttribute<AssemblyCompanyAttribute>().Company; } }

        public string Copyright { get { return GetAssemblyAttribute<AssemblyCopyrightAttribute>().Copyright; } }

        public string Title { get { return GetAssemblyAttribute<AssemblyTitleAttribute>().Title; } }

        public string Product { get { return GetAssemblyAttribute<AssemblyProductAttribute>().Product; } }

        public string Trademark { get { return GetAssemblyAttribute<AssemblyTrademarkAttribute>().Trademark; } }        

        public string Version { get { return GetAssemblyAttribute<AssemblyFileVersionAttribute>().Version; }}

        private T GetAssemblyAttribute<T>() where T : Attribute
        {
            return (T) Attribute.GetCustomAttribute(assembly, typeof(T), false);
        }
    }
}
