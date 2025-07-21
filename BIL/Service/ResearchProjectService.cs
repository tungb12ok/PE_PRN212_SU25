using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Service
{
    public class ResearchProjectService
    {
        private ResearchProjectRepository researchProjectRepository;

        public ResearchProjectService()
        {
            researchProjectRepository = new();
        }
        //CRUD operations for ResearchProject
        public List<ResearchProject> GetList()
        {
            return researchProjectRepository.GetResearchProjects();
        }
        public List<ResearchProject> Search(string inputSearch)
        {
            return researchProjectRepository.Search(inputSearch);
        }
        public void DeleteProject(int projectId)
        {
            researchProjectRepository.DeleteProject(projectId);
        }
        public void AddProject(ResearchProject project)
        {
            researchProjectRepository.AddProject(project);
        }
        public void UpdateProject(ResearchProject project)
        {
            researchProjectRepository.UpdateProject(project);
        }
    }
}
