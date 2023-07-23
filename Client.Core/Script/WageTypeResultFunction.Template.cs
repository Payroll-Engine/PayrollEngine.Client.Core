using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PayrollEngine.Client.Scripting;
using PayrollEngine.Client.Scripting.Runtime;

namespace $Namespace$
{
    [WageTypeResultFunction(
        tenantIdentifier: "$TenantIdentifier$",
        userIdentifier: "$UserIdentifier$",
        employeeIdentifier: "$EmployeeIdentifier$",
        payrollName: "$PayrollName$",
        regulationName: "$RegulationName$")]
    public class $ClassName$ : PayrollEngine.Client.Scripting.Function.WageTypeResultFunction
    {
        public $ClassName$(IWageTypeResultRuntime runtime) :
            base(runtime)
        {
        }

        public $ClassName$() :
            base(GetSourceFileName())
        {
        }

        [WageTypeResultScript(wageTypeNumber: "$WageTypeNumber$")]
        public new object Result()
        {
            $Expression$
#pragma warning disable CS0162
            return default;
#pragma warning restore CS0162
        }
    }
}
