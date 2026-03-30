using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

using DTPortal.Core.Domain.Models;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services;
using DTPortal.Core.Domain.Services.Communication;
using DTPortal.Common;
using System.Text;

namespace DTPortal.Core.Services
{
    public class SMTPService : ISMTPService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SMTPService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Smtp> GetSMTPSettingsAsync(int id)
        {
            var smtpindb = await _unitOfWork.SMTP.GetByIdAsync(id);

            // Get EncryptionKey
            var EncKey = await _unitOfWork.EncDecKeys.GetByIdAsync(24);
            if (null == EncKey)
            {
            }

            string encryptionPassword = Encoding.UTF8.GetString(EncKey.Key1);

            var DecryptedPasswd = EncryptionLibrary.DecryptText(smtpindb.SmtpPwd,
                encryptionPassword, "appshield3.0");

            smtpindb.SmtpPwd = DecryptedPasswd;

            return smtpindb;
        }

        public async Task<SMTPResponse> UpdateSMTPSettingsAsync(Smtp smtp)
        {
            // Get EncryptionKey
            var EncKey = await _unitOfWork.EncDecKeys.GetByIdAsync(24);
            if (null == EncKey)
            {
            }

            string encryptionPassword = Encoding.UTF8.GetString(EncKey.Key1);

            var EncryptedPasswd = EncryptionLibrary.EncryptText(smtp.SmtpPwd,
                encryptionPassword, "appshield3.0");

            smtp.SmtpPwd = EncryptedPasswd;

            try
            {
                _unitOfWork.SMTP.Update(smtp);
                await _unitOfWork.SaveAsync();

                return new SMTPResponse(smtp);
            }
            catch (Exception)
            {
                // Log the exception 
                return new SMTPResponse("An error occurred while updating the SMTP settings. Please contact the admin.");
            }
        }

        public async Task<SMTPResponse> TestSMTPConnectionAsync(Smtp smtp)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtp.SmtpHost, smtp.SmtpPort, smtp.RequiresSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(smtp.SmtpUserName, smtp.SmtpPwd);
                    
                    return new SMTPResponse(smtp);
                }
            }
            catch (Exception)
            {
                var smtperror = string.Format("An error occurred while testing the SMTP connection. Please contact the admin");
                // Log the exception 
                return new SMTPResponse(smtperror);
            }
        }
    }
}
