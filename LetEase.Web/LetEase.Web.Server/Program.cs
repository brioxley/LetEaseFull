using Microsoft.OpenApi.Models;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "LetEase API", Version = "v1" });
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		//if (!app.Environment.IsDevelopment())
		//{
		//	app.UseHsts();
		//}
		//else
		//{
		//	app.UseSwagger();
		//	app.UseSwaggerUI();
		//}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller}/{action=Index}/{id?}");

		app.MapFallbackToFile("index.html");

		app.Run();
	}
}
