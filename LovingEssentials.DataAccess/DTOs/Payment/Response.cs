namespace LovingEssentials.DataAccess.DTOs.Payment;

public record Response(
    int error,
    String message,
    object? data
);