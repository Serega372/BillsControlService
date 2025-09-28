namespace BillsControl.ApplicationCore.CustomExceptions;

/// <summary>
/// Represents error that occurred while searching for a personal bill.
/// </summary>
/// <param name="id">Searching id.</param>
public class BillNotFoundException(Guid? id) 
    : Exception($"Bill with id '{id}' not found");