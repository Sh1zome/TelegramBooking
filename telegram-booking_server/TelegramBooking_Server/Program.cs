using Microsoft.EntityFrameworkCore;
using TelegramBooking_Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
var DB = new Queries(new DB());

DB.Init();


app.MapGet("/users", async (TodoDb db) => 
    await db.Todos.ToListAsync());

app.MapGet("/users/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());


app.MapGet("/GetUsers", async () =>
{
    var res = await DB.GetUsers();
    return res;
});

app.MapGet("/GetUser", async (int id) =>
{
    var res = await DB.GetUser(id);
    return res;
});

app.MapPost("/SetUser", async (string channel_id, int id) =>
{
    var res = await DB.SetUser(channel_id, id);
    return res;
});

app.MapPost("/AddUser", async (string channel_id) =>
{
    var res = await DB.AddUser(channel_id);
    return res;
});

app.MapPost("/DelUser", async (int id) =>
{
    var res = await DB.DelUser(id);
    return res;
});


app.MapGet("/GetAdmin", async (int id) =>
{
    var res = await DB.GetAdmin(id);
    return res;
});

app.MapPost("/SetAdmin", async (int permissions, int id) =>
{
    var res = await DB.SetAdmin(permissions, id);
    return res;
});

app.MapPost("/AddAdmin", async (int id, int permissions) =>
{
    var res = await DB.AddAdmin(id, permissions);
    return res;
});

app.MapPost("/DelAdmin", async (int id) =>
{
    var res = await DB.DelAdmin(id);
    return res;
});


app.MapGet("/GetProvider", async (int id) =>
{
    var res = await DB.GetProvider(id);
    return res;
});

app.MapPost("/SetProvider", async (string name, int id) =>
{
    var res = await DB.SetProvider(name, id);
    return res;
});

app.MapPost("/AddProvider", async (string name) =>
{
    var res = await DB.AddProvider(name);
    return res;
});

app.MapPost("/DelProvider", async (int id) =>
{
    var res = await DB.DelProvider(id);
    return res;
});


app.MapGet("/GetService", async (int id) =>
{
    var res = await DB.GetService(id);
    return res;
});

app.MapPost("/SetService", async (string name, int id) =>
{
    var res = await DB.SetService(id, name);
    return res;
});

app.MapPost("/AddService", async (int provider_id, string name) =>
{
    var res = await DB.AddService(provider_id, name);
    return res;
});

app.MapPost("/DelService", async (int id) =>
{
    var res = await DB.DelService(id);
    return res;
});


app.MapGet("/GetCalendar", async (int id) =>
{
    var res = await DB.GetCalendar(id);
    return res;
});

app.MapPost("/SetCalendar", async (int id, string calendar) =>
{
    var res = await DB.SetCalendar(id, calendar);
    return res;
});

app.MapPost("/AddCalendar", async (int service_id, string calendar) =>
{
    var res = await DB.AddCalendar(service_id, calendar);
    return res;
});

app.MapPost("/DelCalendar", async (int id) =>
{
    var res = await DB.DelCalendar(id);
    return res;
});


app.MapPost("/users", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{todo.Id}", todo);
});

app.MapPut("/users/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/users/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.MapGet("/admins", async (TodoDb db) =>
    await db.Admins.ToListAsync());

app.MapGet("/Admins/{id}", async (int id, TodoDb db) =>
    await db.Admins.FindAsync(id)
        is Admin admin
            ? Results.Ok(admin)
            : Results.NotFound());

app.MapPost("/admins", async (Admin admin, TodoDb db) =>
{
    db.Admins.Add(admin);
    await db.SaveChangesAsync();

    return Results.Created($"/admins/{admin.Id}", admin);
});

app.MapPut("/admins/{id}", async (int id, Admin inputAdmin, TodoDb db) =>
{
    var admin = await db.Admins.FindAsync(id);

    if (admin is null) return Results.NotFound();

    admin.Permissions = inputAdmin.Permissions;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/admins/{id}", async (int id, TodoDb db) =>
{
    if (await db.Admins.FindAsync(id) is Admin admin)
    {
        db.Admins.Remove(admin);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();