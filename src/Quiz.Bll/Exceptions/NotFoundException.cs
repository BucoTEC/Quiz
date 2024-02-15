namespace Quiz.Bll.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource is not found.
/// </summary>
public class NotFoundException(string message = "Not found") : Exception(message) { }
