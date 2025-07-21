using BIL.IServices;
using DAL.Models;

namespace BIL.IService
{
    public interface ICandidateService : IGenericService<Candidate>
    {
        IEnumerable<Candidate> Search(string keyword);
    }
}
