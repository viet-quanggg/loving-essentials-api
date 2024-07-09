using LovingEssentials.DataAccess.DTOs.Payment;

namespace LovingEssentials.Repository.IRepository;

public interface IPaymentRepository
{
    Task<Response> CreatePaymentLink(CreatePaymentLinkRequest paymentLinkRequest);

    Task<Response> GetOrder(int orderId);
}