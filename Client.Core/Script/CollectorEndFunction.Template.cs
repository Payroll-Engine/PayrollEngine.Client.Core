using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PayrollEngine.Client.Scripting;
using PayrollEngine.Client.Scripting.Runtime;

namespace $Namespace$
{
    [CollectorEndFunction(
        tenantIdentifier: "$TenantIdentifier$",
        userIdentifier: "$UserIdentifier$",
        employeeIdentifier: "$EmployeeIdentifier$",
        payrollName: "$PayrollName$",
        regulationName: "$RegulationName$")]
    public class $ClassName$ : PayrollEngine.Client.Scripting.Function.CollectorEndFunction
    {
        public $ClassName$(ICollectorEndRuntime runtime) :
            base(runtime)
        {
        }

        public $ClassName$() :
            base(GetSourceFileName())
        {
        }

        [CollectorEndScript(collectorName: "$CollectorName$")]
        public new object End()
        {
            $Expression$
#pragma warning disable CS0162
            return default;
#pragma warning restore CS0162
        }
    }
}
