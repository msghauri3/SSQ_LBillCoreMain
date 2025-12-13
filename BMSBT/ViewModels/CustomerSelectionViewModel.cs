// ViewModels/CustomerSelectionViewModel.cs
namespace BMSBT.ViewModels
{
    public class CustomerSelectionViewModel
    {
        public string SelectedProject { get; set; }
        public string SelectedBlock { get; set; }
        public string SelectedCategory { get; set; }
        public int TotalRecords { get; set; }
        public List<CustomerDetailResult> CustomerDetails { get; set; }
    }

    public class CustomerDetailResult
    {
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CNICNo { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Sector { get; set; }
        public string Block { get; set; }
        public string PlotNo { get; set; }
        public string Project { get; set; }
        public string Category { get; set; }
    }

    public class SelectionCriteriaViewModel
    {
        public List<string> Projects { get; set; }
        public List<string> Blocks { get; set; }
        public List<string> Categories { get; set; }
    }
}