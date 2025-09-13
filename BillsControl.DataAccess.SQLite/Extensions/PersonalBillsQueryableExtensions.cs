using BillsControl.Core.Entities;

namespace BillsControl.DataAccess.SQLite.Extensions;

public static class PersonalBillsQueryableExtensions
{
    public static IQueryable<PersonalBillEntity> FilterByWithResidents(
        this IQueryable<PersonalBillEntity> query,
        bool? withResidents)
    {
        if (withResidents.HasValue)
        {
            return query
                .Where(bill => bill.Residents.Count > 0);
        }
        return query;
    }

    public static IQueryable<PersonalBillEntity> FilterByAddress(
        this IQueryable<PersonalBillEntity> query,
        string? address)
    {
        if (!string.IsNullOrEmpty(address))
        {
            return query
                .Where(bill => bill.Address.Contains(address));
        }
        return query;
    }

    public static IQueryable<PersonalBillEntity> FilterByBillNumber(
        this IQueryable<PersonalBillEntity> query,
        string? billNumber)
    {
        if (!string.IsNullOrEmpty(billNumber))
        {
            return query
                .Where(bill => bill.BillNumber == billNumber);
        }
        return query;
    }

    public static IQueryable<PersonalBillEntity> FilterByOpenDate(
        this IQueryable<PersonalBillEntity> query,
        DateOnly? openDate)
    {
        if (openDate.HasValue)
        {
            return query
                .Where(bill => bill.OpenDate == openDate);
        }
        return query;
    }

    public static IQueryable<PersonalBillEntity> FilterByName(
        this IQueryable<PersonalBillEntity> query,
        string? firstName,
        string? lastName,
        string? middleName)
    {
        if (!string.IsNullOrEmpty(lastName))
        {
            query = query
                .Where(bill => bill.Residents
                    .Any(resident => resident.Lastname == lastName));
        }
        
        if (!string.IsNullOrEmpty(firstName))
        {
            query = query
                .Where(bill => bill.Residents
                    .Any(resident => resident.FirstName == firstName));
        }

        if (!string.IsNullOrEmpty(middleName))
        {
            query = query
                .Where(bill => bill.Residents
                    .Any(resident => resident.MiddleName == middleName));
        }
        return query;
    }
}