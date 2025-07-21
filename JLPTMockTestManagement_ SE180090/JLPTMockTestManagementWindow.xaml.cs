using System.Windows;
using BIL.IServices;
using BIL.Service;
using DAL.Models;
using BIL.IService;
using System.Linq;

namespace JLPTMockTestManagement__SE180090
{
    public partial class JLPTMockTestManagementWindow : Window
    {
        private readonly IMockTestService _mockTestService;
        private readonly ICandidateService _candidateService;
        private readonly Jlptaccount _currentUser;

        public JLPTMockTestManagementWindow(Jlptaccount user)
        {
            _mockTestService = new MockTestService();
            _candidateService = new CandidateService();
            InitializeComponent();
            _currentUser = user;
            LoadData();
            LoadCandidates();
        }

        private void LoadData()
        {
            var mockTests = _mockTestService.GetAll(); 
            var mockTestWithCandidates = new List<object>();

            foreach (var test in mockTests)
            {
                var candidate = _candidateService.GetById(test.CandidateId); 

                var mockTestData = new
                {
                    test.TestId,
                    test.TestTitle,
                    test.SkillArea,
                    test.StartTime,
                    test.EndTime,
                    CandidateName = candidate != null ? candidate.FullName : "No Candidate",
                    test.Score
                };

                mockTestWithCandidates.Add(mockTestData);
            }

            dgMockTests.ItemsSource = mockTestWithCandidates;
        }

        private void LoadCandidates()
        {
            var candidates = _candidateService.GetAll();
            cbCandidates.ItemsSource = candidates.Select(c => new { c.CandidateId, c.FullName });
            cbCandidates.DisplayMemberPath = "FullName";
            cbCandidates.SelectedValuePath = "CandidateId";
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!CanAccess(View: true)) return;

            string keyword = txtSearch.Text.Trim();
            var result = _mockTestService.Search(keyword);
            dgMockTests.ItemsSource = result.Select(test => new
            {
                test.TestId,
                test.TestTitle,
                test.SkillArea,
                test.StartTime,
                test.EndTime,
                CandidateName = test.Candidate.FullName,
                test.Score
            });
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CanAccess(Create: true)) return;

            var test = GetInputMockTest();
            if (test != null)
            {
                _mockTestService.Add(test);
                LoadData();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CanAccess(Update: true)) return;

            var test = GetInputMockTest();
            if (test != null)
            {
                _mockTestService.Update(test);
                LoadData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!CanAccess(Delete: true)) return;

            if (dgMockTests.SelectedItem is MockTest selected)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected test?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _mockTestService.Delete(selected.TestId);
                    LoadData();
                }
            }
        }


        private MockTest GetInputMockTest()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtSkill.Text)) return null;

            if (!dpStartTime.SelectedDate.HasValue || !dpEndTime.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select both start and end times.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            var startDate = dpStartTime.SelectedDate.Value;
            var endDate = dpEndTime.SelectedDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start time must be earlier than End time.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            var startTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            var endTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            var test = new MockTest
            {
                TestTitle = txtTitle.Text,
                SkillArea = txtSkill.Text,
                StartTime = TimeOnly.FromDateTime(startTime),
                EndTime = TimeOnly.FromDateTime(endTime),
                CandidateId = (int)cbCandidates.SelectedValue,
                Score = double.TryParse(txtScore.Text, out var sc) ? sc : (double?)null
            };

            if (test.TestTitle.Length < 5 || test.TestTitle.Length > 150) return null;

            if (!test.TestTitle.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))) return null;

            return test;
        }


        private bool CanAccess(bool Create = false, bool Update = false, bool Delete = false, bool View = false)
        {
            int role = _currentUser.Role.GetValueOrDefault(0);

            if ((Create || Update) && role > 2)
            {
                MessageBox.Show("Only Admin or Manager can Create/Update.", "Permission Denied");
                return false;
            }
            if (Delete && role != 1)
            {
                MessageBox.Show("Only Admin can Delete.", "Permission Denied");
                return false;
            }
            if (View && role > 3)
            {
                MessageBox.Show("You have no permission to view data.", "Permission Denied");
                return false;
            }
            return true;
        }
    }
}
