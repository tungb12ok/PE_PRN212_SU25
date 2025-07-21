using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ResearcherRepository
    {
        //Khai báo biến private
        private Su25researchDbContext _context;
        //B2: Dựng contructor
        public ResearcherRepository()
        {
            _context = new();
        }
        //Bước 3: Viết các hàm liên quan đến lấy dữ liệu
        //Get: lấy thông tin các researcher
        public List<Researcher> GetResearchers()
        {
            return _context.Researchers.ToList();
        }
    }
}