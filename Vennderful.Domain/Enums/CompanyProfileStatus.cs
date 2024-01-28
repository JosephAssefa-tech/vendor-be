using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Domain.Enums
{
    /// <summary>
    /// Customer can be Venue, Event or Client
    /// </summary>
    public enum CompanyProfileStatus
    {
        Pending = 0,  // to show the onboarding process is in progress
        Completed = 1  // to show the onboarding process is completed and the profile is ready
    }
}
