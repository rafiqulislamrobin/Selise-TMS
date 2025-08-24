using CSharpFunctionalExtensions;
using TMS.Application.Common.Domain;
using TMS.Application.Common.Enum;
using TaskStatus = TMS.Application.Common.Enum.TaskStatus;

namespace TMS.Application.Entities
{

    public class Task : Entity<int>, IAudit
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus Status { get; private set; }
        public string AssignedToUserId { get; private set; }
        public string CreatedUserId { get; private set; }
        public int TeamId { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? ModifiedOn { get; private set; }
        public Task(string title, string description, TaskStatus status, string assignedToUserId, string createdUserId, int teamId, DateTime dueDate)
        {
            Title = title;
            Description = description;
            Status = status;
            AssignedToUserId = assignedToUserId;
            CreatedUserId = createdUserId;
            TeamId = teamId;
            DueDate = dueDate;
        }

        public void Update(string title, string description, TaskStatus status, string assignedToUserId, int teamId, DateTime dueDate)
        {
            Title = title;
            Description = description;
            Status = status;
            AssignedToUserId = assignedToUserId;
            TeamId = teamId;
            DueDate = dueDate;
        }
        #region Audit_Properties


        public void SetCreatedOn(DateTimeOffset dateTimeOffset)
        {
            CreatedOn = dateTimeOffset;
        }

        public void SetModifiedOn(DateTimeOffset dateTimeOffset)
        {
            ModifiedOn = dateTimeOffset;
        }

        #endregion Audit_Properties
    }
}