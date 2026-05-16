namespace ASVSSECURITYAUDITOR.Models
{
    public class BenchmarkViewModel
    {
        public double CompliancePercentage { get; set; }

        public string RiskLevel { get; set; }

        public string MaturityLevel { get; set; }

        public List<ChapterScore> Chapters { get; set; }
            = new();
    }

    public class ChapterScore
    {
        public string Chapter { get; set; }

        public int Total { get; set; }

        public int Valid { get; set; }

        public double Percentage { get; set; }

        public string Recommendation { get; set; }
    }
}