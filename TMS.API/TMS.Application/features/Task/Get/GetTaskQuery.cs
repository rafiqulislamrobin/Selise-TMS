using MediatR;

namespace TMS.Application.features.Task.Get
{
    public class GetTaskQuery : IRequest<List<TaskResponse>>
    {
        public ResourseParameter ResourseParameter { get; set; }
        public GetTaskQuery(ResourseParameter resourseParameter)
        {
            ResourseParameter = resourseParameter;
        }
    }
}