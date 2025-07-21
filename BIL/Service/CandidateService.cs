using BIL.IService;
using DAL.Models;

namespace BIL.Service
{
    public class CandidateService : GenericService<Candidate>, ICandidateService
    {
        public CandidateService() : base()
        {
        }

        public IEnumerable<Candidate> Search(string keyword)
        {
            return _repository.Find(x =>
                x.FullName.Contains(keyword) ||
                x.Jlptlevel.Contains(keyword) ||
                x.StudyGoal.Contains(keyword));
        }
    }
}
