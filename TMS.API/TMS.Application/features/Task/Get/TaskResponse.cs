namespace TMS.Application.features.Task.Get
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string AssignedToUserId { get; set; }
        public string CreatedUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DueDate { get; set; }
    }
}