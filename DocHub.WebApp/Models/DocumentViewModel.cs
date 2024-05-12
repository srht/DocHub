namespace DocHub.WebApp.Models
{
    public class DocumentViewModel
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
