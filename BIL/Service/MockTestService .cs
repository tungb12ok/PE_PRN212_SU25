using BIL.IServices;
using DAL.Models;

namespace BIL.Service
{
    public class MockTestService : GenericService<MockTest>, IMockTestService
    {
        public MockTestService() : base()
        {
        }

        public IEnumerable<MockTest> Search(string keyword)
        {
            return _repository.Find(x =>
                x.TestTitle.Contains(keyword) ||
                x.SkillArea.Contains(keyword));
        }
    }
}
