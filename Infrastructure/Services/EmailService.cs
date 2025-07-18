using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendUserInvitationAsync(string email, string name)
    {
        try
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Welcome to geidea VOC";

            var displayName = name;
            var invitationLink = emailSettings["LoginUrl"];
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f5f5f5;
                                margin: 0;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: white;
                                padding: 40px;
                                border-radius: 8px;
                                box-shadow: 0 2px 10px rgba(0,0,0,0.1);
                                text-align: center;
                            }}
                            .logo {{
                                width: 60px;
                                height: 60px;
                                background-color: #FF4500;
                                border-radius: 8px;
                                display: inline-flex;
                                align-items: center;
                                justify-content: center;
                                margin-bottom: 20px;
                                font-size: 24px;
                                font-weight: bold;
                                color: white;
                            }}
                            .title {{
                                font-size: 24px;
                                color: #333;
                                margin-bottom: 20px;
                            }}
                            .brand {{
                                color: #FF4500;
                                font-weight: bold;
                            }}
                            .content {{
                                font-size: 16px;
                                color: #666;
                                line-height: 1.6;
                                margin-bottom: 30px;
                            }}
                            .user-name {{
                                color: #0066CC;
                                font-weight: bold;
                            }}
                            .link-container {{
                                background-color: #f8f9fa;
                                padding: 20px;
                                border-radius: 8px;
                                margin: 20px 0;
                            }}
                            .invitation-link {{
                                color: #0066CC;
                                font-size: 18px;
                                font-weight: bold;
                                text-decoration: none;
                                word-break: break-all;
                            }}
                            .note {{
                                font-size: 14px;
                                color: #999;
                                margin-top: 15px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1 class='title'>Welcome to <span class='brand'>geidea VOC</span></h1>
                            <div class='content'>
                                <p>Hello <span class='user-name'>{displayName}</span>! Geidea invites you to access<br>The VOC Portal as an Admin.</p>
                                <p>You can join us using this email through this link:</p>
                            </div>
                            <div class='link-container'>
                                <a href='{invitationLink}' class='invitation-link'>{invitationLink}</a>
                            </div>
                            <p class='note'>Please note this link is only valid for 5 minutes.</p>
                        </div>
                    </body>
                    </html>",
                TextBody = $@"Welcome to geidea VOC

Hello {displayName}! Geidea invites you to access The VOC Portal as an Admin.

You can join us using this email through this link:

{invitationLink}"
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();

            // Connect to SMTP server
            await client.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["SmtpPort"] ?? "587"),
                SecureSocketOptions.StartTls);

            // Authenticate if credentials are provided
            var smtpUsername = emailSettings["SmtpUsername"];
            var smtpPassword = emailSettings["SmtpPassword"];

            if (!string.IsNullOrEmpty(smtpUsername) && !string.IsNullOrEmpty(smtpPassword))
            {
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
            }

            // Send the email
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Invitation email sent successfully to {Email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending invitation email to {Email}", email);
            throw;
        }
    }

}