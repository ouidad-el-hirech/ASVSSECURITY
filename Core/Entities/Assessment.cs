namespace ASVSSECURITYAUDITOR.Core.Entities
{
    public class Assessment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<AssessmentItem> Items { get; set; }
    }
}