using CsvHelper;
using ASVSSECURITYAUDITOR.Core.Entities;
using ASVSSECURITYAUDITOR.Core.Interfaces;
using ASVSSECURITYAUDITOR.Infrastructure.Data;
using System.Globalization;

namespace ASVSSECURITYAUDITOR.Core.Services
{
    public class CsvImportService : ICsvImportService
    {
        private readonly ApplicationDbContext _context;

        public CsvImportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ImportAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Fichier CSV invalide");

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<ASVSRequirement>().ToList();

            if (records.Any())
            {
                _context.ASVSRequirements.AddRange(records);
                await _context.SaveChangesAsync();
            }
        }
    }
}