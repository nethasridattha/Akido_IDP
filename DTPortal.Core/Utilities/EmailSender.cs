using DTPortal.Common;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DTPortal.Core.Utilities
{
    public class EmailSenderOld
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public EmailSenderOld(ILogger<EmailSender> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        private async Task<MimeMessage> CreateEmailMessage(Message message)
        {
            _logger.LogDebug("-->CreateEmailMessage");

            // Validate Input Parameters
            if (null == message)
            {
                _logger.LogError("Invalid Input Parameter");
                return null;
            }
            var emailMessage = new MimeMessage();

            try
            {
                var emailSettings = await _unitOfWork.SMTP.GetByIdAsync(1);
                if(null == emailSettings)
                {
                    _logger.LogError("Failed to get email settings");
                    return null;
                }
                emailMessage.From.Add(MailboxAddress.Parse(emailSettings.FromEmailAddr));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message.Content
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateEmailMessage Failed : {0}", ex.Message);
                return null;
            }

            _logger.LogDebug("<--CreateEmailMessage");
            return emailMessage;
        }

        private async Task<int> Send(MimeMessage mailMessage)
        {
            _logger.LogDebug("-->Send");
            int result = -1;

            // Validate Input Parameters
            if (null == mailMessage)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            using (var client = new SmtpClient())
            {
                try
                {
                    // Get EncryptionKey
                    var EncKey = await _unitOfWork.EncDecKeys.GetByIdAsync(24);
                    if (null == EncKey)
                    {
                        _logger.LogError("Failed to get keys");
                        return result;
                    }

                    string encryptionPassword = 
                        Encoding.UTF8.GetString(EncKey.Key1);
                    if (null == encryptionPassword)
                    {
                        _logger.LogError("Failed to convert to bytes");
                        return result;
                    }

                    var smtp = await _unitOfWork.SMTP.GetByIdAsync(1);
                    if (null == EncKey)
                    {
                        _logger.LogError("Failed to SMTP settings");
                        return result;
                    }

                    var DecryptedPasswd = string.Empty;

                    // Decrypt Password
                    DecryptedPasswd = EncryptionLibrary.DecryptText(smtp.SmtpPwd,
                        encryptionPassword, "appshield3.0");
                    if (null == DecryptedPasswd)
                    {
                        _logger.LogError("Failed to decrypt text");
                        return result;
                    }

                    //var mailConfig = new EmailConfiguration();

                    //mailConfig.SmtpServer = smtp.SmtpHost;
                    //mailConfig.Port = smtp.SmtpPort;
                    //mailConfig.UserName = smtp.SmtpUserName;
                    //mailConfig.Password = DecryptedPasswd;

                    //client.Connect(mailConfig.SmtpServer,
                    //    mailConfig.Port, smtp.RequiresSsl);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //client.Authenticate(mailConfig.UserName,
                    //    mailConfig.Password);
                    //await client.SendAsync(mailMessage);
                    //result = 0;
                    var mailConfig = new EmailConfiguration
                    {
                        SmtpServer = smtp.SmtpHost,
                        Port = smtp.SmtpPort,
                        UserName = smtp.SmtpUserName,
                        Password = DecryptedPasswd
                    };

                    SecureSocketOptions socketOptions = SecureSocketOptions.None;

                    switch (mailConfig.Port)
                    {
                        case 465:
                            socketOptions = SecureSocketOptions.SslOnConnect;
                            break;

                        case 587:
                            socketOptions = SecureSocketOptions.StartTls;
                            break;

                        case 25:
                            socketOptions = SecureSocketOptions.None;
                            break;

                        default:
                            socketOptions = smtp.RequiresSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None;
                            break;
                    }

                    await client.ConnectAsync(mailConfig.SmtpServer, mailConfig.Port, socketOptions);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (mailConfig.Port != 25 && !string.IsNullOrWhiteSpace(mailConfig.UserName))
                    {
                        await client.AuthenticateAsync(mailConfig.UserName, mailConfig.Password);
                    }

                    await client.SendAsync(mailMessage);
                    result = 0;

                }
                catch (Exception error)
                {
                    _logger.LogError("Failed to send Mail: {0}",
                        error.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

            _logger.LogDebug("<--Send");
            return result;
        }

        public bool TestSmtpConnection(EmailConfiguration emailConfiguration)
        {
            _logger.LogDebug("-->TestSmtpConnection");
            bool result = false;

            // Validate Input Parameters
            if (null == emailConfiguration)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfiguration.SmtpServer,
                        emailConfiguration.Port,
                        true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfiguration.UserName,
                        emailConfiguration.Password);
                    result = true;
                }
                catch(Exception error)
                {
                    _logger.LogError("Failed to TestSmtpConnection: {0}",
                        error.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }

            _logger.LogDebug("<--TestSmtpConnection");
            return result;
        }

        public async Task<int> SendEmail(Message message)
        {
            _logger.LogDebug("-->SendEmail");
            int result = -1;

            // Validate Input Parameters
            if (null == message)
            {
                _logger.LogError("Invalid Input Parameter");
                return result;
            }

            var emailMessage = await CreateEmailMessage(message);
            if(null == emailMessage)
            {
                _logger.LogError("CreateEmailMessage Failed");
                return result;
            }

            // Send Email
            result = await Send(emailMessage);
            if(0 != result)
            {
                _logger.LogError("Send Email Failed");
                return result;
            }

            //Return Success
            result = 0;

            _logger.LogDebug("<--SendEmail");
            return result;
        }
    }
}
