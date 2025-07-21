using BIL.Service;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResearchProjectManagement_SE185005
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Bước 1: Khai báo biến private
        private ResearcherService _researcherService;
        //Bước 2: dựng contructor
        public MainWindow()
        {
            InitializeComponent();
            _researcherService = new ResearcherService();
            GetResearchers();
        }
        public void GetResearchers()
        {
            //Xuống Nhờ Service xử lí thông tin giúp
            //ĐỔ DỮ LIỆU TRONG DATA GRID (UI)
            dgResearcherList.ItemsSource = _researcherService.GetResearchers();
        }
    }
}