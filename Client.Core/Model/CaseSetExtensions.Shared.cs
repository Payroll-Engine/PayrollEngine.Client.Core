using System;
using System.Linq;
using System.Collections.Generic;

namespace PayrollEngine.Client.Model;

/// <summary>Extension methods for the <see cref="CaseSet"/></summary>
public static class CaseSetExtensions
{
    /// <param name="caseSet">The case to search</param>
    extension(CaseSet caseSet)
    {
        /// <summary>
        /// Find case by name, considering related cases
        /// </summary>
        /// <param name="caseName">The name of the case</param>
        /// <returns>The value case field matching the name, null on missing case field</returns>
        public CaseSet FindCase(string caseName)
        {
            if (string.IsNullOrWhiteSpace(caseName))
            {
                throw new ArgumentException(nameof(caseName));
            }
            if (caseSet != null)
            {
                // local case
                if (string.Equals(caseSet.Name, caseName))
                {
                    return caseSet;
                }

                //  search case recursively in related cases
                if (caseSet.RelatedCases != null)
                {
                    foreach (var relatedCase in caseSet.RelatedCases)
                    {
                        var @case = relatedCase.FindCase(caseName);
                        if (@case != null)
                        {
                            return @case;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Find case field by name, considering related cases
        /// </summary>
        /// <param name="caseFieldName">The name of the case field</param>
        /// <returns>The value case field matching the name, null on missing case field</returns>
        public CaseFieldSet FindCaseField(string caseFieldName)
        {
            if (string.IsNullOrWhiteSpace(caseFieldName))
            {
                throw new ArgumentException(nameof(caseFieldName));
            }
            if (caseSet == null)
            {
                return null;
            }

            // case value reference
            var caseValueReference = new CaseValueReference(caseFieldName);

            // find local value case field
            CaseFieldSet caseFieldSet;
            if (caseValueReference.HasCaseSlot)
            {
                // case field name and case slot
                caseFieldSet = caseSet.Fields?.FirstOrDefault(x => string.Equals(x.Name, caseValueReference.CaseFieldName) &&
                                                                   string.Equals(x.CaseSlot, caseValueReference.CaseSlot));
            }
            else
            {
                // case field name only
                caseFieldSet = caseSet.Fields?.FirstOrDefault(x => string.Equals(x.Name, caseValueReference.CaseFieldName));
            }

            //  search field recursively in related cases
            if (caseFieldSet == null && caseSet.RelatedCases != null)
            {
                foreach (var relatedCase in caseSet.RelatedCases)
                {
                    caseFieldSet = relatedCase.FindCaseField(caseFieldName);
                    if (caseFieldSet != null)
                    {
                        return caseFieldSet;
                    }
                }
            }

            return caseFieldSet;
        }

        /// <summary>Ensure unique case names and case field names, including related cases</summary>
        /// <returns>throws a payroll exception if the derived case is invalid</returns>
        public void EnsureUniqueNames() =>
            new DerivedCaseNameChecker().Check(caseSet);
    }

    private sealed class DerivedCaseNameChecker
    {
        private readonly HashSet<string> caseNames = [];
        private readonly HashSet<string> caseFieldNames = [];

        internal void Check(CaseSet caseSet)
        {
            if (caseSet == null)
            {
                return;
            }

            // case name
            if (string.IsNullOrWhiteSpace(caseSet.Name))
            {
                throw new PayrollException($"Case {caseSet.Id} without name.");
            }
            if (!caseNames.Add(caseSet.Name))
            {
                throw new PayrollException($"Duplicated case name {caseSet.Name}.");
            }

            // case field names
            if (caseSet.Fields != null)
            {
                foreach (var field in caseSet.Fields)
                {
                    if (string.IsNullOrWhiteSpace(field.Name))
                    {
                        throw new PayrollException($"Case field {field.Id} without name.");
                    }
                    if (!caseFieldNames.Add(field.Name))
                    {
                        throw new PayrollException($"Duplicated case field name {field.Name}.");
                    }
                }
            }

            // related cases
            if (caseSet.RelatedCases != null)
            {
                foreach (var @case in caseSet.RelatedCases)
                {
                    // recursive check
                    Check(@case);
                }
            }
        }
    }
}