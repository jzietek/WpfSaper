using NUnit.Framework;
using Shouldly;
using System.Reflection;
using WpfSaper.ViewModels;

namespace WpfSaper.Tests.ViewModels
{
    [TestFixture]
    public class AboutWindowViewModelTests
    {
        [Test]
        public void ViewModel_ShouldProvide_ValidAssemblyDetails()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AboutWindowViewModelTests));
            AboutWindowViewModel vm = new AboutWindowViewModel(assembly);
            
            vm.Description.ShouldBe("TestDescription");
            vm.Company.ShouldBe("TestCompany");
            vm.Copyright.ShouldBe("TestCopyright");
            vm.Product.ShouldBe("TestProduct");
            vm.Title.ShouldBe("TestTitle");
            vm.Trademark.ShouldBe("TestTradeMark");
            vm.Version.ShouldBe("1.0.0.0");
        }
    }
}
