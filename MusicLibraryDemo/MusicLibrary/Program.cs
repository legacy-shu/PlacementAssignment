var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var db = new DB("./musiclibrary.db");
db.CreateTable();
db.InsertValue();
db.testReadData();

app.Run();