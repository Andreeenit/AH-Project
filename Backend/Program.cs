using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=sysdb.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Ladda in alla produkter från databasen
app.MapGet("/products", async (ApplicationDbContext db) => await db.Articles.ToListAsync());

//Ladda in en produkt som har samma sku (artikel-id) 
app.MapGet("/products/{sku}", async (string sku, ApplicationDbContext db) => await db.Articles.FirstAsync(a => a.SKU == sku));

// lägg till en product i databasen
app.MapPost("/products", async (ApplicationDbContext db, Article article) =>
{
    await db.Articles.AddAsync(article);
    await db.SaveChangesAsync();
});


// Ta bort en produkt från databasen som har en viss sku
app.MapDelete("/products/{sku}", async (ApplicationDbContext db, string sku) =>
{
    var article = await db.Articles.Where(a => a.SKU == sku).FirstAsync();
    db.Articles.Remove(article);
    await db.SaveChangesAsync();
});

// Uppdatera en produkt som redan finns i databasen
app.MapPut("/products", async (ApplicationDbContext db, Article article) =>
{
    db.Articles.Update(article);
    await db.SaveChangesAsync();
});

// Ladda in alla ordrar från databasen
app.MapGet("/orders", async (ApplicationDbContext db) => await db.Orders.ToListAsync());


//Ladda in en order som har samma sku (order-nummer) 
app.MapGet("/orders/{OrderId}", async (int OrderId, ApplicationDbContext db) => await db.Orders.FirstAsync(a => a.OrderId == OrderId));


// lägg till en order i databasen
app.MapPost("/orders", async (ApplicationDbContext db, Order order) =>
{
    await db.Orders.AddAsync(order);
    await db.SaveChangesAsync();
});


// Ta bort en order från databasen som har ett visst order-nummer
app.MapDelete("/orders/{OrderId}", async (ApplicationDbContext db, int OrderId) =>
{
    var order = await db.Orders.Where(a => a.OrderId == OrderId).FirstAsync();
    db.Orders.Remove(order);
    await db.SaveChangesAsync();
});


// Uppdatera en order som redan finns i databasen
app.MapPut("/orders", async (ApplicationDbContext db, Order order) =>
{
    db.Orders.Update(order);
    await db.SaveChangesAsync();
});

app.Run();
