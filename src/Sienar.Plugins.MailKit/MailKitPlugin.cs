using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sienar.Extensions;
using Sienar.Infrastructure;
using Sienar.Plugins;

namespace Sienar.Email;

/// <summary>
/// Adds MailKit email support to Sienar applications
/// </summary>
public class MailKitPlugin : IPlugin
{
	private readonly IApplicationAdapter _adapter;
	private readonly IConfiguration _configuration;

	/// <summary>
	/// Creates a new instance of <c>MailKitPlugin</c>
	/// </summary>
	public MailKitPlugin(
		IApplicationAdapter adapter,
		IConfiguration configuration)
	{
		_adapter = adapter;
		_configuration = configuration;
	}

	/// <inheritdoc />
	public void Configure()
	{
		_adapter.AddServices(sp =>
		{
			sp
				.AddScoped<IEmailSender, MailKitSender>()
				.AddScoped<ISmtpClient, SmtpClient>()
				.ApplyDefaultConfiguration<SmtpOptions>(
					_configuration.GetSection("Sienar:Email:Smtp"));
		});
	}
}