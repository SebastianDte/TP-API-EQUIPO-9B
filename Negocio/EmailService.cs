using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient("smtp.gmail.com", 587);
            server.Credentials = new NetworkCredential("progamacion9b@gmail.com", "kslr wame jeei mzrp");
            server.EnableSsl = true;
        }

        public void armarCorreo(string emailDestino, string nombreArticulo , string imageUrl)
        {
            email = new MailMessage();
            email.From = new MailAddress("progamacion9b@gmail.com", "Vouchers");
            email.To.Add(emailDestino);
            email.Subject = "🎉 ¡Tu participación en el sorteo fue registrada!";
            email.IsBodyHtml = true;
            email.Body = GenerarBodyHTML(nombreArticulo, imageUrl);
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo", ex);
            }
        }

        private string GenerarBodyHTML(string nombreArticulo, string imageUrl) 
        {
            string html = $@"
                <div style='font-family: Arial, sans-serif; color: #333; text-align: center; padding: 20px; background-color: #f9f9f9; border-radius: 8px;'>
                    <h2 style='color: #0066cc;'>🎉 ¡Tu participación fue registrada!</h2>
                    <p>Hemos recibido tu voucher para el sorteo.</p>
                
                    <div style='margin: 20px auto; padding: 15px; background-color: #fff; border: 1px solid #ddd; border-radius: 6px; max-width: 300px;'>
                        <h3 style='margin: 0; color: #444;'>Producto elegido '{nombreArticulo}'</h3>
                        <img src='{imageUrl}' alt='Producto' style='max-width: 100%; border-radius: 6px; margin-top: 10px; onerror=this.onerror=null; this.src='https://via.placeholder.com/250';'/>
                    </div>
                
                    <p style='margin-top: 10px;'>
                        <a href='https://instagram.com/colidevs' style='color: #fff; background-color: #e1306c; padding: 10px 15px; border-radius: 4px; text-decoration: none;'>Instagram</a>
                        &nbsp;
                        <a href='https://colitienda.com/' style='color: #fff; background-color: #0066cc; padding: 10px 15px; border-radius: 4px; text-decoration: none;'>Visitar sitio</a>
                    </p>
                
                    <p>¡Gracias por participar!</p>
                </div>";

            return html;
        }
    }
}
