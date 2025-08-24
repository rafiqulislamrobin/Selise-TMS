using MediatR;

namespace TMS.Application.features.Task.Delete
{
    public class DeleteTaskCommand : IRequest<DeleteTaskResponse>
    {
        public int Id { get; set; }
        public DeleteTaskCommand(int id)
        {
            Id = id;
        }
    }
}