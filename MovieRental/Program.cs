using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.PaymentProviders;
using MovieRental.Price;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

builder.Services.AddScoped<IPriceCalculator, PriceCalculator>();

//change to scoped so it can be injected in the RentalController (since dbContext is scoped)
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

builder.Services.AddScoped<IPaymentProvider, PayPalProvider>();
builder.Services.AddScoped<IPaymentProvider, MbWayProvider>();
builder.Services.AddScoped<IPaymentProvider, FailPaymentProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/error");

//there is no need to instantiate the dbcontext here
//using (var client = new MovieRentalDbContext())
//{
//	client.Database.EnsureCreated();
//}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MovieRentalDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
