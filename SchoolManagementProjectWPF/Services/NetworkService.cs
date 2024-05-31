using SchoolManagementProjectWPF.Models;
using System.Net;
using System.Net.Mail;

namespace SchoolManagementProjectWPF.Services;

public static class Network
{
	public static bool SendNotificationToEmail(Notification n, string addressEmail)
	{
		string fromAddress = "elsmith.256@gmail.com";
		string password = "nowoagcvkhfvtaeb";

		MailMessage mailMessage = new MailMessage(fromAddress, addressEmail)
		{
			IsBodyHtml = true,
			Subject = $"{n.HeaderText}",
			Body = @$"<!DOCTYPE html>
			<html lang=""en"">
			<head>
			    <meta charset=""UTF-8"">
			    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
			    <title>Document</title>
			    <style>
			        @import url('https://fonts.googleapis.com/css2?			family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&disp   lay=   swap	');
			        .box{{
			            text-align: center;
			            position: relative;
			            top: 60px;
			        }}
			    </style>
			</head>
			<body>
			    <div class=""box"">
			        <h2 class="" : "">"+@$"{n?.ToString()}</h2>
			    </div>
			</body>
			</html>"
		};

		SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
		{
			Port = 587,
			Credentials = new NetworkCredential(fromAddress, password),
			EnableSsl = true,
		};

		try
		{
			smtpClient.Send(mailMessage);
			return true;
		}
		catch { return false; }

	}
}