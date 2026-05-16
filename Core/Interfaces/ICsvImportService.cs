namespace ASVSSECURITYAUDITOR.Core.Interfaces
{
    public interface ICsvImportService
    {
        Task ImportAsync(IFormFile file);
    }
}
