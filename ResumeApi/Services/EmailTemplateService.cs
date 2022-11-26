using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Drawing;
using ResumeApi.Models.Email;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Xml.Linq;

namespace ResumeApi.Services
{
    public interface IEmailTemplateService
    {
        public string GenerateSendEmailLink(string actionUrl, string email, string subject, string action);
    }


    public class EmailTemplateService : IEmailTemplateService
    {
        public EmailTemplateService() { }

        public string GenerateSendEmailLink(string actionUrl, string email, string subject, string action)
        {
            string emailBody = @"
                <td align = ""center"" style = ""padding-left: 8px; padding-right: 8px;"" >
                    < p style = ""margin-top: 8px; font-size: 32px"" > " + subject + @"</ p >
                    < p style = ""font-size: 20px;"" > You'll soon be able to start using your new account! Click the button to finish setting up your new account</p>
                    < a href = "" " + actionUrl + @" "" rel=""noopener noreferrer"" target=""_blank"" style=""text-decoration: none"">
                        <div style = ""padding: 12px; padding-left: 20px; padding-right: 20px; background-color: #3CBBFF; border-radius: 4px; color: white; max-width: 120px; font-size:20px"" >
                            " + action + @"
                        </div>
                    </a>
                </td>
            ";
            return emailBody;
        }


        private string GenerateEmailTemplate(string body)
        {
            string emailTemplate = @"

                <!doctype html>
                <html lang=""en"">
                    <head>
                    </head>

                    <body 
                        style=""
                        background-color: #f8f8f8;
                        height: 100%;
                        width: 100%;
                        font-family: Arial, Helvetica, sans-serif;
                        ""
                    >
                        <table  
                            width=""100%""
                            height=""100px""
                            align=""center""
                            style=""padding-bottom: 32px""
                        >
                            <tbody>
                                <td align=""center"">
                                    <br>
                                    <img style=""height: 50px; width: auto;"" src=""https://langable.com/assets/pictures/langable-email-logo.png"">
                                </td>
                            </tbody>

                        </table>
                        <table  
                            height=""100px""
                            align=""center""
                            style=""padding-bottom: 16px; background-color: #ffffff; border-radius: 4px; padding: 16px; max-width: 500px;""

                        >
                            <tbody>
                            " + body + @"
                            </tbody>
                        </table>
                        <br>
                    </body>
                </html>";
            return emailTemplate;
        }
    }
}

