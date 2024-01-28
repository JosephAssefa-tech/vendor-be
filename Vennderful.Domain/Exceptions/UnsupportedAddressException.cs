using System;

namespace Vennderful.Domain.Exceptions
{
    public class UnsupportedAddressException : Exception
    {
        public UnsupportedAddressException()
            : base("Address is unsupported.")
        {
        }
    }
}
