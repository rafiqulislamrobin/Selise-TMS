namespace TMS.Application.Common.Domain
{
    public interface IAudit
    {
        DateTimeOffset CreatedOn { get; }
        DateTimeOffset? ModifiedOn { get; }

        void SetCreatedOn(DateTimeOffset dateTimeOffset);

        void SetModifiedOn(DateTimeOffset dateTimeOffset);
    }
}
