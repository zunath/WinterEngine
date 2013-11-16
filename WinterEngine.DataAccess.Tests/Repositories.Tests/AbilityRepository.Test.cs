using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.MockingKernel.Moq;
using Moq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using System.Data.Entity;

namespace WinterEngine.DataAccess.Tests.Repositories.Tests
{
    [TestClass]
    public class AbilityRepositoryTest
    {
        [TestMethod()]
        public void AddTest()
        {
            //This requires a reference to Entity framework. Might be better to interface.
            //var mock = new Mock<ModuleDataContext>();
            MockRepository repofactory = new MockRepository(MockBehavior.Loose);
            var mockContext = repofactory.Create<ModuleDataContext>();
            var mockSet = repofactory.Create<DbSet<Ability>>();
            mockContext.Setup(m => m.Abilities).Returns(mockSet.Object);

            Ability ability = new Ability();

            //var dbset = new System.Data.Entity.DbSet<Ability>();
            //mock.Setup(x => x.Abilities).Returns();
            //mock.Setup(x => x.Abilities.Add(ability)).Returns(ability);
            var cx = mockContext.Object;
            var repo = new AbilityRepository(cx, true);
            
            repo.Add(ability);

            mockSet.Verify(x => x.Add(ability), Times.Once());
        }

    }
}
