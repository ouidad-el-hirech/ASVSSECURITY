namespace ASVSSECURITYAUDITOR.Core.Entities  
{
    public class ASVSRequirement
    {
        public int Id { get; set; }

        public string RequirementCode { get; set; }

        public string Chapter { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public ICollection<AssessmentItem> AssessmentItems { get; set; }
    }
}