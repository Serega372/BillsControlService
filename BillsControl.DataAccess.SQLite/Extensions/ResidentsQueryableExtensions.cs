using BillsControl.Core.Entities;

namespace BillsControl.DataAccess.SQLite.Extensions;

public static class ResidentsQueryableExtensions
{
    public static IQueryable<ResidentEntity> FilterByBillNumber(
        this IQueryable<ResidentEntity> query,
        string? billNumber)
    {
        if (!string.IsNullOrEmpty(billNumber))
        {
            return query
                .Where(resident => resident.PersonalBillNumber == billNumber);
        }
        return query;
    }

    public static IQueryable<ResidentEntity> FilterByBillId(
        this IQueryable<ResidentEntity> query,
        Guid? billId)
    {
        if (billId.HasValue)
        {
            return query
                .Where(resident => resident.PersonalBillId == billId);
        }
        return query;
    }
}