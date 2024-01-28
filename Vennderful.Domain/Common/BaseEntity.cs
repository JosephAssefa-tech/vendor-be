using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vennderful.Domain.Common
{
    public abstract class BaseEntity
    {
       public Guid Id { get; set; }
    }
}
