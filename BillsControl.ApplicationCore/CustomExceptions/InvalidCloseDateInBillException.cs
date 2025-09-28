namespace BillsControl.ApplicationCore.CustomExceptions;

/// <summary>
/// Represents error that occurred when attempting set bill close date earlier than open date.
/// </summary>
/// <param name="openDate">bill open date</param>
/// <param name="closeDate">bill close date</param>
public class InvalidCloseDateInBillException(DateOnly openDate, DateOnly closeDate)
    : Exception($"Bill open date {openDate} must be earlier than close date {closeDate}.");