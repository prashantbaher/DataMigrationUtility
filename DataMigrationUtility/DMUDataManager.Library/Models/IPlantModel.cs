namespace DMUDataManager.Library.Models
{
    public interface IPlantModel
    {
        int CompanyId { get; set; }
        string DBName { get; set; }
        string PlantCode { get; set; }
        int PlantId { get; set; }
        string PlantLocation { get; set; }
        string PlantName { get; set; }
    }
}