using MediatR;

namespace TMS.Application.features.Task.Put
{
    public class UpdateTaskCommand : IRequest<UpdateTaskResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string AssignedToUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DueDate { get; set; }
    }
}