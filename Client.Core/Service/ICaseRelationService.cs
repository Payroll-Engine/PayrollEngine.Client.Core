using System.Threading.Tasks;
using PayrollEngine.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace PayrollEngine.Client.Service;

/// <summary>Payroll case relation service</summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface ICaseRelationService : ICrudService<ICaseRelation, RegulationServiceContext, Query>
{
    /// <summary>Get case relation by relation names</summary>
    /// <param name="context">The service context</param>
    /// <param name="sourceCaseName">The source case name</param>
    /// <param name="targetCaseName">The target case name</param>
    /// <param name="sourceCaseSlot">The source case slot</param>
    /// <param name="targetCaseSlot">The target case slot</param>
    /// <returns>The case relation, null if missing</returns>
    Task<T> GetAsync<T>(RegulationServiceContext context, string sourceCaseName, string targetCaseName,
        string sourceCaseSlot = null, string targetCaseSlot = null)
        where T : class, ICaseRelation;

    /// <summary>Rebuild the case relation</summary>
    /// <param name="context">The service context</param>
    /// <param name="caseRelationId">The case relation id</param>
    Task RebuildAsync(RegulationServiceContext context, int caseRelationId);
}