using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class Settings : BaseEntity
    {
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
    }
}
