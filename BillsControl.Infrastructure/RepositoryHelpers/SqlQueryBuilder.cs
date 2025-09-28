using BillsControl.ApplicationCore.Dtos;

namespace BillsControl.Infrastructure.RepositoryHelpers;

/// <summary>
/// ...
/// </summary>
public static class SqlQueryBuilder
{
    /// <summary>
    /// Add a pagination line.
    /// </summary>
    /// <param name="page">page</param>
    /// <param name="pageSize">count of data on page.</param>
    /// <returns>
    /// string that represents a LIMIT and OFFSET sql query if <see cref="page"/>> and <see cref="pageSize"/>> not null.
    /// If null, return empty string.
    /// </returns>
    public static string AddPagination(int? page, int? pageSize)
    {
        if (!page.HasValue || !pageSize.HasValue) return string.Empty;
        return $"LIMIT {pageSize.Value} OFFSET {pageSize.Value * (page.Value - 1)}";
    }

    /// <summary>
    /// Build WHERE sql query from <see cref="conditionsList"/>.
    /// </summary>
    /// <param name="conditionsList">list of condition, that must be added to WHERE query.</param>
    /// <returns>
    /// string that represent a WHERE sql query with input conditions, if <see cref="conditionsList"/>> not empty.
    /// If list empty, return empty string.
    /// </returns>
    public static string BuildWhereQuery(List<string> conditionsList)
    {
        if (conditionsList.Count == 0) return string.Empty;
        return $"WHERE {string.Join(" AND ", conditionsList)}";
    }

    /// <summary>
    /// Check if the JOIN sql query is needed.
    /// </summary>
    /// <param name="fullNameDto">DTO that represents a fullname object of resident.</param>
    /// <param name="withResidents">represents, select data only with residents or not.</param>
    /// <returns>
    /// returns true if JOIN sql query is needed, otherwise return false.
    /// </returns>
    public static bool NeedsJoin(FullNameDto fullNameDto, bool? withResidents)
    {
        return withResidents.HasValue && withResidents.Value ||
               !string.IsNullOrEmpty(fullNameDto.LastName) ||
               !string.IsNullOrEmpty(fullNameDto.FirstName) ||
               !string.IsNullOrEmpty(fullNameDto.MiddleName);
    }

    /// <summary>
    /// Transform an input parameters <see cref="tableAndColumn"/> and <see cref="parameter"/> into part of WHERE sql condition
    /// then adds it to <see cref="conditions"/> list.
    /// </summary>
    /// <param name="conditions">list of conditions into which the transformed part of WHERE condition will be added.</param>
    /// <param name="tableAndColumn">table(optional) and column of database that will be compared to <see cref="parameter"/>.</param>
    /// <param name="parameter">value by which the condition will be checked.</param>
    /// <typeparam name="T">entry <see cref="parameter"/> type</typeparam>
    public static void AddConditionToList<T>(List<string> conditions, string tableAndColumn, T? parameter)
    {
        if (parameter == null || string.IsNullOrEmpty(parameter.ToString())) return;
        conditions.Add($"{tableAndColumn} = '{parameter}'");
    }
}