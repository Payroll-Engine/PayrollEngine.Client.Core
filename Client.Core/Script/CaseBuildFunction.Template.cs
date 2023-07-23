using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PayrollEngine.Client.Scripting;
using PayrollEngine.Client.Scripting.Runtime;

namespace $Namespace$
{
    [CaseBuildFunction(
        tenantIdentifier: "$TenantIdentifier$",
        userIdentifier: "$UserIdentifier$",
        employeeIdentifier: "$EmployeeIdentifier$",
        payrollName: "$PayrollName$",
        regulationName: "$RegulationName$")]
    public class $ClassName$ : PayrollEngine.Client.Scripting.Function.CaseBuildFunction
    {
        public $ClassName$(ICaseBuildRuntime runtime) :
            base(runtime)
        {
        }

        public $ClassName$() :
            base(GetSourceFileName())
        {
        }

        [CaseBuildScript(caseName: "$CaseName$")]
        public new bool? Build()
        {
            $Expression$
#pragma warning disable CS0162
            return default;
#pragma warning restore CS0162
        }
    }
}
