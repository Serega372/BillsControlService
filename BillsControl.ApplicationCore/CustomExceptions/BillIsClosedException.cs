namespace BillsControl.ApplicationCore.CustomExceptions;

/// <summary>
/// Represents an error that occurred when attempting to access a closed bill.
/// </summary>
/// <param name="id">Closed bill id.</param>
public class BillIsClosedException(Guid? id) 
    : Exception($"Bill with id '{id}' is closed, access denied");