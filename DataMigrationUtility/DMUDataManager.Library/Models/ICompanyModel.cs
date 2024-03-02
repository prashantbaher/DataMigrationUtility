namespace DMUDataManager.Library.Models
{
    public interface ICompanyModel
    {
        string CompanyCode { get; set; }
        string CompanyDescription { get; set; }
        int CompanyID { get; set; }
        string CompanyName { get; set; }
    }
}