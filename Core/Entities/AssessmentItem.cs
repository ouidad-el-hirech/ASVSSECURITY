namespace ASVSSECURITYAUDITOR.Core.Entities
{
    public class AssessmentItem
    {
        public int Id { get; set; }

        public int AssessmentId { get; set; }

        public Assessment Assessment { get; set; }

        public int ASVSRequirementId { get; set; }

        public ASVSRequirement ASVSRequirement { get; set; }

        public AssessmentStatus Status { get; set; }

        public string Notes { get; set; } = "";
    }
}