using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PayrollEngine.Client.Scripting;
using PayrollEngine.Client.Scripting.Runtime;

namespace $Namespace$
{
    [ReportBuildFunction(
        tenantIdentifier: "$TenantIdentifier$",
        userIdentifier: "$UserIdentifier$",
        employeeIdentifier: "$EmployeeIdentifier$",
        payrollName: "$PayrollName$",
        regulationName: "$RegulationName$")]
    public class $ClassName$ : PayrollEngine.Client.Scripting.Function.ReportBuildFunction
    {
        public $ClassName$(IReportBuildRuntime runtime) :
            base(runtime)
        {
        }

        public $ClassName$() :
            base(GetSourceFileName())
        {
        }

        [ReportBuildScript(collectorName: "$ReportName$")]
        public new bool? Build()
        {
            $Expression$
#pragma warning disable CS0162
            return default;
#pragma warning restore CS0162
        }
    }
}
