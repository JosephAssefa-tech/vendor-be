using System;

namespace Vennderful.Application.Common
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        private DateTime _Created = DateTime.UtcNow;
        public DateTime Created { get { return _Created; } set { _Created = Convert.ToDateTime(value).ToUniversalTime(); } }

        public string? CreatedBy { get; set; }

        private DateTime? _LastModified = DateTime.UtcNow;
        public DateTime? LastModified { get { return _LastModified; } set { _LastModified = Convert.ToDateTime(value).ToUniversalTime(); } }

        public string? LastModifiedBy { get; set; }
    }
}
