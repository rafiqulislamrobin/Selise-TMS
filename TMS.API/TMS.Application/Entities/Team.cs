using CSharpFunctionalExtensions;
using TMS.Application.Common.Domain;

namespace TMS.Application.Entities
{
    public class Team : Entity<int>, IAudit
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? ModifiedOn { get; private set; }
        public Team(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
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