using System.ComponentModel.DataAnnotations.Schema;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Common
{
    [NotMapped]
    public class Phone
    {
        public string PhoneNumber { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
