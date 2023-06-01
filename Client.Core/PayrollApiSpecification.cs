using System;

namespace PayrollEngine.Client
{
    /// <summary>
    /// The Payroll Api specification
    /// </summary>
    public static class PayrollApiSpecification
    {
        /// <summary>
        /// The current API version
        /// <remarks>Update backend version changes here</remarks>
        /// </summary>
        public static Version CurrentApiVersion => new(1, 0);
    }
}
