// ViewModels/ProjectStatisticsViewModel.cs (updated)
namespace BMSBT.ViewModels
{
    public class ProjectStatisticsViewModel
    {
        public string ProjectName { get; set; }
        public int TotalCustomers { get; set; }
    }

    public class BlockStatisticsViewModel
    {
        public string BlockName { get; set; }
        public int TotalCustomers { get; set; }
    }

    public class DashboardViewModel
    {
        public List<string> Projects { get; set; }
        public List<string> Blocks { get; set; }
        public int TotalAllCustomers { get; set; }
        public List<ProjectStatisticsViewModel> ProjectStatistics { get; set; }
        public List<BlockStatisticsViewModel> BlockStatistics { get; set; }
    }
}