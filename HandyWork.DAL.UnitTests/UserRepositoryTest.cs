using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandyWork.DAL.UnitTests
{
    [TestClass]
    public class UserRepositoryTest: BaseTest
    {
        [TestMethod]
        public void FindAllByQuery()
        {
            var expression1 = UnitOfWork.UserRepository.GetExpression(new UserQuery { UserNameLike = "h", IsLocked = false });
        }
    }
}
