using Application.Common.Domain;
using CSharpFunctionalExtensions;

namespace Application.Entities
{
    public class Customer : Entity<int>, IAudit
    {
        public string Name { get; private set; }
        public string Remarks { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? ModifiedOn { get; private set; }

        public Customer( string name, string remarks)
        {
            Name = name;
            Remarks = remarks;
        }

        public void Update(string name, string remarks)
        {
            Name = name;
            Remarks = remarks;
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
