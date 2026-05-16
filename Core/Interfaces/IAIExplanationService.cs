namespace ASVSSECURITYAUDITOR.Core.Interfaces
{
    public interface IAIExplanationService
    {
        Task<string> ExplainRequirement(string requirement);
    }
}