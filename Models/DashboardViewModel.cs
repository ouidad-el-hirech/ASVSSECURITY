namespace ASVSSECURITYAUDITOR.Models
{
    public class DashboardViewModel
    {
        public int TotalRequirements { get; set; }

        public int ValidCount { get; set; }

        public int NotValidCount { get; set; }

        public int PendingCount { get; set; }

        public int NotApplicableCount { get; set; }

        public double CompliancePercentage { get; set; }

        public string RiskLevel { get; set; }
    }
}