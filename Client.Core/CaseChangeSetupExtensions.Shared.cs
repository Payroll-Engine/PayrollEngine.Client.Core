using System;
using System.Collections.Generic;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client;

/// <summary>Extension methods for the <see cref="CaseChangeSetup"/></summary>
public static class CaseChangeSetupExtensions
{
    /// <summary>Find case setup</summary>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseName">The case name</param>
    /// <param name="caseSlot">The case slot</param>
    /// <returns>The case value or null on missing case value</returns>
    public static CaseSetup FindCaseSetup(this CaseChangeSetup caseChangeSetup, string caseName, string caseSlot = null)
    {
        if (string.IsNullOrWhiteSpace(caseName))
        {
            throw new ArgumentException(nameof(caseName));
        }
        return caseChangeSetup == null ? null : FindCase(caseChangeSetup.Case, caseName, caseSlot);
    }

    private static CaseSetup FindCase(CaseSetup caseSetup, string caseName, string caseSlot)
    {
        if (caseSetup == null)
        {
            return null;
        }

        // case name and case slot
        if (string.Equals(caseSetup.CaseName, caseName) && string.Equals(caseSetup.CaseSlot, caseSlot))
        {
            return caseSetup;
        }

        // related cases
        if (caseSetup.RelatedCases != null)
        {
            foreach (var relatedCase in caseSetup.RelatedCases)
            {
                var @case = FindCase(relatedCase, caseName, caseSlot);
                if (@case != null)
                {
                    return @case;
                }
            }
        }

        return null;
    }

    /// <summary>Find case value</summary>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <param name="caseFieldName">The case field name</param>
    /// <param name="caseSlot">The case slot</param>
    /// <returns>The case value or null on missing case value</returns>
    public static CaseValue FindCaseValue(this CaseChangeSetup caseChangeSetup, string caseFieldName, string caseSlot = null)
    {
        if (string.IsNullOrWhiteSpace(caseFieldName))
        {
            throw new ArgumentException(nameof(caseFieldName));
        }
        return caseChangeSetup == null ? null : FindCaseValue(caseChangeSetup.Case, caseFieldName, caseSlot);
    }

    /// <summary>Finds the case value recursively</summary>
    /// <param name="caseSetup">The case setup</param>
    /// <param name="caseFieldName">Name of the case field</param>
    /// <param name="caseSlot">The case slot</param>
    private static CaseValue FindCaseValue(CaseSetup caseSetup, string caseFieldName, string caseSlot)
    {
        if (caseSetup == null)
        {
            return null;
        }

        // case value by slot
        if (string.Equals(caseSetup.CaseSlot, caseSlot))
        {
            if (caseSetup.Values != null)
            {
                foreach (var caseValue in caseSetup.Values)
                {
                    if (string.Equals(caseValue.CaseFieldName, caseFieldName))
                    {
                        return caseValue;
                    }
                }
            }
        }

        // related cases
        if (caseSetup.RelatedCases != null)
        {
            foreach (var relatedCase in caseSetup.RelatedCases)
            {
                var caseValue = FindCaseValue(relatedCase, caseFieldName, caseSlot);
                if (caseValue != null)
                {
                    return caseValue;
                }
            }
        }

        return null;
    }

    /// <summary>Search for duplicated case value</summary>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <returns>The duplicated case value, null without duplicates</returns>
    public static CaseValue FindDuplicatedCaseValue(this CaseChangeSetup caseChangeSetup)
    {
        var caseValueLookup = new Dictionary<Tuple<string, string>, CaseValue>();
        foreach (var caseValue in CollectCaseValues(caseChangeSetup))
        {
            var key = new Tuple<string, string>(caseValue.CaseFieldName, caseValue.CaseSlot);
            if (caseValueLookup.ContainsKey(key))
            {
                // duplicated case value
                return caseValue;
            }
            caseValueLookup.Add(key, caseValue);
        }
        return null;
    }

    /// <summary>Collect all case setups</summary>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <returns>List if case setups</returns>
    public static List<ICaseSetup> CollectCaseSetups(this CaseChangeSetup caseChangeSetup)
    {
        var caseSetups = new List<ICaseSetup>();
        if (caseChangeSetup != null)
        {
            CollectCaseSetups(caseChangeSetup.Case, caseSetups);
        }
        return caseSetups;
    }

    private static void CollectCaseSetups(ICaseSetup caseSetup, List<ICaseSetup> caseSetups)
    {
        if (caseSetup != null)
        {
            caseSetups.Add(caseSetup);
            if (caseSetup.RelatedCases != null)
            {
                foreach (var relatedCase in caseSetup.RelatedCases)
                {
                    CollectCaseSetups(relatedCase, caseSetups);
                }
            }
        }
    }

    /// <summary>Collect all case values</summary>
    /// <param name="caseChangeSetup">The case change setup</param>
    /// <returns>List if case values</returns>
    public static List<CaseValue> CollectCaseValues(this ICaseChangeSetup caseChangeSetup)
    {
        var caseValues = new List<CaseValue>();
        if (caseChangeSetup != null)
        {
            CollectCaseValues(caseChangeSetup.Case, caseValues);
        }
        return caseValues;
    }

    private static void CollectCaseValues(ICaseSetup caseSetup, List<CaseValue> caseValues)
    {
        if (caseSetup != null)
        {
            if (caseSetup.Values != null)
            {
                caseValues.AddRange(caseSetup.Values);
            }
            if (caseSetup.RelatedCases != null)
            {
                foreach (var relatedCase in caseSetup.RelatedCases)
                {
                    CollectCaseValues(relatedCase, caseValues);
                }
            }
        }
    }
}