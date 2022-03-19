namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FineshedAt { get; set; }
    }
}
