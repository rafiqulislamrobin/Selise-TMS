using MediatR;

namespace TMS.Application.features.Task.Post
{
    public class CreateTaskCommand : IRequest<CreateTaskResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DueDate { get; set; }
    }
}