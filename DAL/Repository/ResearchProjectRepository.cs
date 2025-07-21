using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ResearchProjectRepository
    {
        private Su25researchDbContext _context;

        public ResearchProjectRepository()
        {
            _context = new();
        }
        //CRUD operations for ResearchProject
        public List <ResearchProject> GetResearchProjects()
        {
            return _context.ResearchProjects.Include(x => x.LeadResearcher).ToList();
        }
        public List<ResearchProject> Search(string inputSearch)
        {
            return _context.ResearchProjects
                            .Include(p => p.LeadResearcher)
                            .Where(p =>
                                p.ProjectTitle.Contains(inputSearch) ||
                                p.ResearchField.Contains(inputSearch))
                            .ToList();
        }
        public void DeleteProject(int projectId)
        {
            var project = _context.ResearchProjects.Find(projectId);
            if (project != null)
            {
                _context.ResearchProjects.Remove(project);
                _context.SaveChanges();
            }
        }
        public void AddProject(ResearchProject project)
        {
            project.ProjectId = _context.ResearchProjects.Max(x => x.ProjectId) + 1;
            _context.ResearchProjects.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProject(ResearchProject project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
