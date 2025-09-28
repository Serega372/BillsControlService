namespace BillsControl.ApplicationCore.CustomExceptions;

/// <summary>
/// Represents error that occurred while searching for a resident.
/// </summary>
/// <param name="id">Searching id.</param>
public class ResidentNotFoundException(Guid id) 
    : Exception($"Resident with id '{id}' not found");