namespace ASVSSECURITYAUDITOR.Core.Interfaces
{
    public interface IPdfReportService
    {
        byte[] GenerateAssessmentReport(
            int assessmentId);
    }
}