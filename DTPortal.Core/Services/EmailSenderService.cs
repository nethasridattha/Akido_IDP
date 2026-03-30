using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Core.DTOs;
using DTPortal.Core.Utilities;
using Google.Apis.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTPortal.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public EmailSenderService(ILogger<EmailSenderService> logger,
            IWebHostEnvironment env,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _env = env;
            _emailSender = emailSender;
        }

        public async Task<ServiceResult> SendEmailAsync(EmailRequestDTO requestData)
        {
            try
            {
                string receiver = requestData.EmailId;
                bool isOrg = requestData.Org;
                string link = requestData.Link;
                bool eSeal = requestData.Eseal; // Assuming this is a stringified JSON
                //bool trustedStakeholder = (bool)requestData["trustedStakeholder"];
                int ttl = requestData.Ttl;
                string emailOtp = requestData.EmailOtp.ToString();

                string spocFullName = null;
                string orgName = null;
                string startDate = null;
                string endDate = null;
                string referenceId = null;
                string name = null;
                var mailBody = string.Empty;
                var mailSubject = string.Empty;
                string clientName = _configuration["ClientName"];
                string wwwRootPath = _env.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", "UgpassLogo.png");

                //string ImagePath = Path.Combine(_environment.ContentRootPath, subPath);
                byte[] imageArray = await System.IO.File.ReadAllBytesAsync(imagePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                string AdminBaseaddress = _configuration.GetValue<string>("AdminUrl");


                if (eSeal)
                {
                    EsealCertificateDto esealCertificateDto = requestData.EsealCertificateDto;
                    spocFullName = esealCertificateDto.SpocFullName;
                    orgName = esealCertificateDto.OrgName;
                    startDate = esealCertificateDto.StartDate;
                    endDate = esealCertificateDto.EndDate;


                    if (string.IsNullOrEmpty(startDate))
                    {
                        mailSubject = "Your E-seal Certificate is expiring soon!";
                        mailBody = "<p style='color:black'> Dear " + spocFullName + ",<br><br>This is to inform you that your E-seal certificate is going to expired for the organization " + orgName + ".<br><br>certificate expiry date: " + endDate + "<br><br>- MyTrust System </p> <img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                    }else
                    {
                        mailSubject = "Congratulations! Your E-seal Certificate is Successfully Issued";
                        mailBody = "<p style=\"color:black\"> Dear " + spocFullName + ",<br><br>This is to inform you that your E-seal certificate is successfully<br> issued  for the organization " + orgName + ".<br><br>certificate issued on\t:" + startDate + "<br>certificate valid upto :" + endDate + "<br><br>- MyTrust System </p>  <img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                    }
                }

                else if (isOrg)
                {
                    mailSubject = $"{clientName} System";
                    mailBody = $"<p style='color:black'>Dear Customer, please download the {clientName} app and onboard.<br><br>- {clientName} System </p><img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                }

                else if (!string.IsNullOrEmpty(link))
                {
                    TrustedStakeholder trustedStakeholderData = requestData.TrustedStakeholder;
                    referenceId = trustedStakeholderData.ReferenceId;
                    name = trustedStakeholderData.Name;
                    mailBody = $"<p style='color:black'> Dear Customer,your reference id " + referenceId + ", name is " + name + ".<br>Click the below link <br>" + link + "<br><br>- "+ clientName +" System </p> <img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                }

                else if (string.IsNullOrEmpty(emailOtp))
                {
                    mailSubject = $"{clientName} System";
                    mailBody = $"<p style='color:black'>Dear Customer,<br><br>Your Organisation has successfully been onboarded into our platform. Please go ahead and look into the services. Please purchase an e-seal membership through the {clientName} mobile application.<br><br>- {clientName} System </p><img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                }

                else if (ttl == 0)
                {
                    mailSubject = $"{clientName} System OTP";
                    mailBody = $"<p style='color:black'>Dear Customer,<br><br>Test OTP for {clientName} Registration is <u>"+emailOtp+ "</u>.<br><br>- "+ clientName +" System </p>  <img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                }
                else
                {
                    mailSubject = $"{clientName} System OTP";
                    mailBody = $"<p style='color:black'>Dear Customer,<br><br>Your OTP for {clientName} Registration is <u>"+emailOtp+"</u>, Please use this OTP to validate your Email. <br>This OTP is valid for "+ttl+ " Seconds.<br><br>- MyTrust System </p>  <img src='" + AdminBaseaddress + "images/UgpassLogo.png' width='150' height='50'/>";
                }


                var messageObj = new Message(new string[]
                {
                    receiver
                },
                mailSubject,
                mailBody
                );

                // Send email to user
                try
                {
                    await _emailSender.SendEmail(messageObj);
                    return new ServiceResult(true, "Email sent successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unable to send email");
                    _logger.LogError("Exception : "+ ex.Message);
                    return new ServiceResult(false, "Unable to send email");
                }

            }
            catch (Exception)
            {

            }
            return new ServiceResult(false, "Failed to send email");
        }
    }
}
