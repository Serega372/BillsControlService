namespace BillsControl.ApplicationCore.CustomExceptions;

/// <summary>
/// Represents an error that occurred when attempting to close an already closed bill.
/// </summary>
/// <param name="id">Closed bill id.</param>
public class BillAlreadyClosedException(Guid id) 
    : Exception($"Bill with id '{id}' is already closed");