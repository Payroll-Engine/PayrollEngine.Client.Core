using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PayrollEngine.Client.Scripting;
using PayrollEngine.Client.Scripting.Runtime;

namespace $Namespace$
{
    /// <exclude />
    [ReportEndFunction(
        tenantIdentifier: "$TenantIdentifier$",
        userIdentifier: "$UserIdentifier$",
        employeeIdentifier: "$EmployeeIdentifier$",
        payrollName: "$PayrollName$",
        regulationName: "$RegulationName$")]
    public class $ClassName$ : PayrollEngine.Client.Scripting.Function.ReportEndFunction
    {
        public $ClassName$(IReportEndRuntime runtime) :
            base(runtime)
        {
        }

        public $ClassName$() :
            base(GetSourceFileName())
        {
        }

        [ReportEndScript(collectorName: "$ReportName$")]
        public new object End()
        {
            $Expression$
#pragma warning disable CS0162
            return default;
#pragma warning restore CS0162
        }
    }
}
