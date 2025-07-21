using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Service
{
    public class ResearcherService
    {
        //Bước 1: tạo biến private
        private ResearcherRepository _researcherRepository;
        //Bước 2: Dựng Contructor
        public ResearcherService()
        {
            _researcherRepository = new ();
        }
        //Bước 3: Viết các hàm xử lí dữ liệu
        public List<Researcher> GetResearchers()
        {
            return _researcherRepository.GetResearchers();
        }
    }
}
