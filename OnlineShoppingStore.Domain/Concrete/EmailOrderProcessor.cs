using OnlineShoppingStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShoppingStore.Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace OnlineShoppingStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSetings;
        private ShippingDetails shippingInfo;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSetings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSetings.UseSsl;
                smtpClient.Host = emailSetings.ServerName;
                smtpClient.Port = emailSetings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSetings.Username, emailSetings.Password);

                StringBuilder body = new StringBuilder();
                body.AppendLine("A new order has been submited");
                body.AppendLine("-------");
                body.AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} X {1} (subtotal :{2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value: {0:C}" ,cart.ComputeTotalValue());
                body.AppendLine("--------");
                body.AppendLine("Ship to:");
                body.AppendLine(shippingInfo.Name);
                body.AppendLine(shippingInfo.Line1 ?? "");
                body.AppendLine(shippingInfo.Line2 ?? "");
                body.AppendLine(shippingInfo.Line3 ?? "");
                body.AppendLine(shippingInfo.City);
                body.AppendLine(shippingInfo.State ?? "");
                body.AppendLine(shippingInfo.Country);
                body.AppendLine(shippingInfo.Zip);
                body.AppendLine("------");
                    body.AppendFormat("Gift wrap : {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSetings.MailFromAddress,
                    emailSetings.MailToAddress,
                    "New order submitted",
                    body.ToString());

                smtpClient.Send(mailMessage);




            }
        }
    }
}
