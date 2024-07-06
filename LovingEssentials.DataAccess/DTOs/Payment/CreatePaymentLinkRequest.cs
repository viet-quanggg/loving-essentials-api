namespace LovingEssentials.DataAccess.DTOs.Payment;

public record CreatePaymentLinkRequest(
    string productName,
    string description,
    int price,
    string returnUrl,
    string cancelUrl
);