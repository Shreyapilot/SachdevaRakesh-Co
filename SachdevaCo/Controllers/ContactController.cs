using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
using System.Net;
using System.Net.Mail;

namespace SachdevaCo.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly SmtpViewModels _smtpSettings;

        // Inject repository and SMTP settings
        public ContactController(IContactRepository contactRepository, IOptions<SmtpViewModels> smtpSettings)
        {
            _contactRepository = contactRepository;
            _smtpSettings = smtpSettings.Value;
        }

        public IActionResult Index()
        {
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];

            return View(new ContactMessageViewModel());
        }

        [HttpPost]
        public IActionResult Submit(ContactMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return the form with validation errors
                return View("Index", model);
            }

            // Map ViewModel ? Entity
            var entity = new ContactMessage
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                CreatedAt = DateTime.UtcNow
            };

            // Save to DB
            _contactRepository.AddMessage(entity);

            // Send email
            SendEmail(model);

            TempData["Success"] = "Thank You For Contacting Us";
            return RedirectToAction("Index");
        }

        private void SendEmail(ContactMessageViewModel model)
        {
            var fromAddress = new MailAddress(_smtpSettings.From, "Inedge Consulting");
            var toAddress = new MailAddress(_smtpSettings.Username);

            string subject = model.Subject ?? "Contact Form Submission";
            string body = $"From: {model.FirstName} {model.LastName}\n" +
                          $"Email: {model.Email}\n\n" +
                          $"Message:\n{model.Message}";

            using (var smtp = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            })
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
