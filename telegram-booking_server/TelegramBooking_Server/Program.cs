using Microsoft.EntityFrameworkCore;
using TelegramBooking_Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/users", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/users/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

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