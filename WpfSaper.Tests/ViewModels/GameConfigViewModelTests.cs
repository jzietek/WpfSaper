using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.ViewModels;

namespace WpfSaper.Tests.ViewModels
{
    [TestFixture]
    class GameConfigViewModelTests
    {
        [Test]
        public void GivenCustomDifficulty_ShouldExpandExpander()
        {
            //Arrange
            GameConfigViewModel vm = new GameConfigViewModel();
            vm.IsCustomSelected.ShouldBeFalse();
            vm.IsExpanded.ShouldBeFalse();

            //Act
            vm.IsCustomSelected = true;

            //Assert
            vm.IsExpanded.ShouldBeTrue();
            vm.IsEasySelected.ShouldBeFalse();
            vm.IsHardSelected.ShouldBeFalse();
            vm.IsMediumSelected.ShouldBeFalse();
        }        
    }
}
