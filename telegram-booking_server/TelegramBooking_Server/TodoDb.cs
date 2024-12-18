﻿using Microsoft.EntityFrameworkCore;
using TelegramBooking_Server;

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();

    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<Queries> queries => Set<Queries>();
}
