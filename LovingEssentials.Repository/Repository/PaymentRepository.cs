using LovingEssentials.DataAccess.DTOs.Payment;
using LovingEssentials.Repository.IRepository;
using Net.payOS;
using Net.payOS.Types;

namespace LovingEssentials.Repository.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly PayOS _payOs;
    public PaymentRepository(PayOS payOs)
    {
        _payOs = payOs;
    }
    
    
    public async Task<Response> CreatePaymentLink(CreatePaymentLinkRequest paymentLinkRequest)
    {
        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            ItemData item = new ItemData(paymentLinkRequest.productName, 1, paymentLinkRequest.price);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            PaymentData paymentData = new PaymentData(orderCode, paymentLinkRequest.price, "Thanh toan don hang", items, "", "");

            CreatePaymentResult createPayment = await _payOs.createPaymentLink(paymentData);

            return new Response(0, "success", createPayment);

            // return createPayment.checkoutUrl;
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            // return Redirect("https://localhost:3002/");
        }

        return null;
    }

    public async Task<Response> GetOrder(int orderId)
    {
        try
        {
            PaymentLinkInformation paymentLinkInformation = await _payOs.getPaymentLinkInformation(orderId);
            
            return new Response(0, "Ok", paymentLinkInformation);
        }
        catch (System.Exception exception)
        {

            Console.WriteLine(exception);
            return new Response(-1, "fail", null);
        }
    }
}