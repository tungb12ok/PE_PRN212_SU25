using DAL.Models;

namespace BIL.IServices
{
    public interface IMockTestService : IGenericService<MockTest>
    {
        IEnumerable<MockTest> Search(string keyword);
    }
}
