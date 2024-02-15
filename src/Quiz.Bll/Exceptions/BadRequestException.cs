namespace Quiz.Bll.Exceptions;

/// <summary>
/// Exception thrown when the request is malformed or invalid.
/// </summary>
public class BadRequestException(string message = "Bad request") : Exception(message) { }

