using BIL.Service;
using DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JLPTMockTestManagement__SE180090
{
    /// <summary>
    /// Interaction logic for ResearchProjectManagementWindow.xaml
    /// </summary>
    public partial class ResearchProjectManagementWindow : Window
    {
        private ResearchProjectService _service;
        private UserAccount _roleUser;
        private ResearcherService _researcherService;
        public ResearchProjectManagementWindow(UserAccount user)
        {
            InitializeComponent();
            _service = new();
            _roleUser = user;
            _researcherService = new();
            LoadProjects();
            LoadResearchers();
        }
        private void LoadProjects()
        {
            dgProjects.ItemsSource = _service.GetList();
        }
        private void LoadResearchers()
        {
            // Giả sử bạn có một phương thức để lấy danh sách các nhà nghiên cứu
            var researchers = _researcherService.GetResearchers(); // Cần implement phương thức này trong ResearchProjectService
            cbLeadResearcher.ItemsSource = researchers;
            cbLeadResearcher.DisplayMemberPath = "FullName"; // Hiển thị tên đầy đủ của nhà nghiên cứu
            cbLeadResearcher.SelectedValuePath = "ResearcherId"; // Lưu ID của nhà nghiên cứu
        }
       

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (_roleUser.Role != 1 && _roleUser.Role != 2)
            {
                MessageBox.Show("Only Admins can create projects!", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string title = txtTitle.Text.Trim();
            string field = txtField.Text.Trim();
            string budgetText = txtBudget.Text.Trim();
            DateTime? start = dpStartDate.SelectedDate;
            DateTime? end = dpEndDate.SelectedDate;
            Researcher selectedResearcher = cbLeadResearcher.SelectedItem as Researcher;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(field) || string.IsNullOrEmpty(budgetText)
       || !start.HasValue || !end.HasValue || selectedResearcher == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chẻck Validate Title
            if (title.Length < 5 || title.Length > 100 || !System.Text.RegularExpressions.Regex.IsMatch(title, @"^([A-Z1-9][a-zA-Z0-9]*\s?)+$"))
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters.\nEach word must start with a capital letter or a digit.\nNo special characters allowed.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chrck Validate Budget
            if (!decimal.TryParse(budgetText, out decimal budget) || budget <= 0)
            {
                MessageBox.Show("Budget must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (start >= end)
            {
                MessageBox.Show("Start Date must be before End Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tạo object
            ResearchProject newProject = new()
            {
                ProjectTitle = title,
                ResearchField = field,
                Budget = budget,
                StartDate = DateOnly.FromDateTime(start.Value),
                EndDate = DateOnly.FromDateTime(end.Value),
                LeadResearcherId = selectedResearcher.ResearcherId
            };

            _service.AddProject(newProject);
            MessageBox.Show("Project created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadProjects();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_roleUser.Role != 1 && _roleUser.Role != 2)
            {
                MessageBox.Show("Only Admins can delete projects!", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dgProjects.SelectedItem is not ResearchProject selectedProject)
            {
                MessageBox.Show("Please select a project to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string title = txtTitle.Text.Trim();
            string field = txtField.Text.Trim();
            string budgetText = txtBudget.Text.Trim();
            DateTime? start = dpStartDate.SelectedDate;
            DateTime? end = dpEndDate.SelectedDate;
            Researcher selectedResearcher = cbLeadResearcher.SelectedItem as Researcher;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(field) || string.IsNullOrEmpty(budgetText)
                || !start.HasValue || !end.HasValue || selectedResearcher == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (title.Length < 5 || title.Length > 100 || !System.Text.RegularExpressions.Regex.IsMatch(title, @"^([A-Z1-9][a-zA-Z0-9]*\s?)+$"))
            {
                MessageBox.Show("ProjectTitle must be between 5 and 100 characters.\nEach word must start with a capital letter or a digit.\nNo special characters allowed.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(budgetText, out decimal budget) || budget <= 0)
            {
                MessageBox.Show("Budget must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (start >= end)
            {
                MessageBox.Show("Start Date must be before End Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Gán data mới
            selectedProject.ProjectTitle = title;
            selectedProject.ResearchField = field;
            selectedProject.Budget = budget;
            selectedProject.StartDate = DateOnly.FromDateTime(start.Value);
            selectedProject.EndDate = DateOnly.FromDateTime(end.Value);
            selectedProject.LeadResearcherId = selectedResearcher.ResearcherId;

            _service.UpdateProject(selectedProject);
            MessageBox.Show("Project updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadProjects();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_roleUser.Role != 1)
            {
                MessageBox.Show("Only Admins can delete projects!", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dgProjects.SelectedItem is ResearchProject selected)
            {
                var confirm = MessageBox.Show("Are you sure to delete this project?", "Confirm", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    _service.DeleteProject(selected.ProjectId);
                    MessageBox.Show("Deleted successfully!");
                    LoadProjects();
                }
            }
        }


        private void dgProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProjects.SelectedItem is ResearchProject selected)
            {
                txtProjectId.Text = selected.ProjectId.ToString();
                txtTitle.Text = selected.ProjectTitle;
                txtField.Text = selected.ResearchField;
                txtBudget.Text = selected.Budget.ToString();

                // Chọn đúng LeadResearcher trong ComboBox
                foreach (var item in cbLeadResearcher.Items)
                {
                    if (item is Researcher researcher && researcher.ResearcherId == selected.LeadResearcherId)
                    {
                        cbLeadResearcher.SelectedItem = researcher;
                        break;
                    }
                }
            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            var result = _service.Search(searchText);
            dgProjects.ItemsSource = result;
        }
    }
}
