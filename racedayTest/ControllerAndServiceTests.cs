using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using raceday_part2.Controllers;
using raceday_part2.Data;
using raceday_part2.Models;
using raceday_part2.Services;
using Xunit;

namespace racedayTest;

internal sealed class TestSession : ISession
{
    private readonly Dictionary<string, byte[]> _store = new();

    public IEnumerable<string> Keys => _store.Keys;
    public string Id { get; } = Guid.NewGuid().ToString();
    public bool IsAvailable => true;

    public void Clear() => _store.Clear();

    public Task CommitAsync(
        System.Threading.CancellationToken cancellationToken = default) =>
        Task.CompletedTask;

    public Task LoadAsync(
        System.Threading.CancellationToken cancellationToken = default) =>
        Task.CompletedTask;

    public void Remove(string key) => _store.Remove(key);

    public void Set(string key, byte[] value) => _store[key] = value;

    public bool TryGetValue(string key, out byte[] value) =>
        _store.TryGetValue(key, out value!);
}

public class SessionUserTests
{
    [Fact]
    public void SessionUser_ReadsIdAndRole_AndMatchesRoleIgnoringCase()
    {
        var context = new DefaultHttpContext
        {
            Session = new TestSession()
        };

        context.Session.SetInt32(SessionUser.IdKey, 123);
        context.Session.SetString(SessionUser.RoleKey, "Admin");

        Assert.Equal(123, SessionUser.Id(context));
        Assert.Equal("Admin", SessionUser.Role(context));
        Assert.True(SessionUser.IsRole(context, "admin"));
        Assert.False(SessionUser.IsRole(context, "participant"));
    }

    [Fact]
    public void SessionUser_ReturnsNullValues_WhenSessionIsEmpty()
    {
        var context = new DefaultHttpContext
        {
            Session = new TestSession()
        };

        Assert.Null(SessionUser.Id(context));
        Assert.Null(SessionUser.Role(context));
        Assert.False(SessionUser.IsRole(context, "anything"));
    }
}

public class AuthAndProfileResultsControllerTests
{
    [Fact]
    public void Login_ReturnsOk_WhenCredentialsAreValid()
    {
        var controller = new AuthController();
        var request = new AuthController.LoginRequest
        {
            Email = "tester",
            Password = "password"
        };

        var result = Assert.IsType<OkObjectResult>(controller.Login(request));
        Assert.Equal(
            "Login successful.",
            GetProperty(result.Value!, "message"));
    }

    [Fact]
    public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        var controller = new AuthController();
        var request = new AuthController.LoginRequest
        {
            Email = "nope",
            Password = "bad"
        };

        var result = Assert.IsType<UnauthorizedObjectResult>(
            controller.Login(request));

        Assert.Equal(
            "Invalid email or password.",
            GetProperty(result.Value!, "message"));
    }

    [Fact]
    public void GetProfile_ReturnsExpectedProfile()
    {
        var controller = new ProfileController();

        var result = Assert.IsType<OkObjectResult>(controller.GetProfile());
        var profile = result.Value!;

        Assert.Equal(1, GetProperty(profile, "Id"));
        Assert.Equal("John Doe", GetProperty(profile, "Name"));
        Assert.Equal("john.doe@example.com", GetProperty(profile, "Email"));
    }

    [Fact]
    public void GetResults_ReturnsFourResults()
    {
        var controller = new ResultsController();

        var result = Assert.IsType<OkObjectResult>(controller.GetResults());
        var items = Assert.IsAssignableFrom<System.Collections.IEnumerable>(
            result.Value);

        var count = 0;
        foreach (var _ in items)
        {
            count++;
        }

        Assert.Equal(4, count);
    }

    private static object? GetProperty(object instance, string name)
    {
        var property = instance.GetType().GetProperty(
            name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        Assert.NotNull(property);
        return property!.GetValue(instance);
    }
}

public class EventsControllerTests
{
    private static RaceDayContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RaceDayContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new RaceDayContext(options);
    }

    [Fact]
    public async Task GetEvents_ReturnsAllEvents()
    {
        await using var context = CreateContext();
        context.Events.AddRange(
            new Event { EventId = 1, EventName = "A", Location = "L" },
            new Event { EventId = 2, EventName = "B", Location = "L" });
        await context.SaveChangesAsync();

        var controller = new EventsController(context);

        var result = await controller.GetEvents();
        var events = Assert.IsType<List<Event>>(result.Value);

        Assert.Equal(2, events.Count);
    }

    [Fact]
    public async Task GetEvent_ReturnsNotFound_WhenEventDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);

        var result = await controller.GetEvent(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetEvent_ReturnsEvent_WhenEventExists()
    {
        await using var context = CreateContext();
        context.Events.Add(
            new Event { EventId = 10, EventName = "Race", Location = "City" });
        await context.SaveChangesAsync();

        var controller = new EventsController(context);

        var result = await controller.GetEvent(10);

        Assert.Null(result.Result);
        Assert.NotNull(result.Value);
        Assert.Equal(10, result.Value.EventId);
        Assert.Equal("Race", result.Value.EventName);
    }

    [Fact]
    public async Task CreateEvent_SavesEventAndReturnsCreated()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);
        var newEvent = new Event
        {
            EventId = 0,
            EventName = "New",
            Location = "Nowhere"
        };

        var result = await controller.CreateEvent(newEvent);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(EventsController.GetEvent), created.ActionName);
        Assert.Equal(1, await context.Events.CountAsync());
    }

    [Fact]
    public async Task UpdateEvent_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);
        var eventToUpdate = new Event
        {
            EventId = 2,
            EventName = "X",
            Location = "L"
        };

        var result = await controller.UpdateEvent(5, eventToUpdate);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_UpdatesEventAndReturnsNoContent()
    {
        await using var context = CreateContext();
        context.Events.Add(
            new Event { EventId = 7, EventName = "Old", Location = "L" });
        await context.SaveChangesAsync();

        var updatedEvent = new Event
        {
            EventId = 7,
            EventName = "Updated",
            Location = "L2"
        };

        // Set the internal EventID property if the controller checks it.
        var eventIdProperty = typeof(Event).GetProperty(
            "EventID",
            BindingFlags.Public | BindingFlags.Instance);

        Assert.NotNull(eventIdProperty);
        eventIdProperty!.SetValue(updatedEvent, 7);

        var controller = new EventsController(context);
        var result = await controller.UpdateEvent(7, updatedEvent);

        Assert.IsType<NoContentResult>(result);

        var savedEvent = await context.Events.FindAsync(7);
        Assert.Equal("Updated", savedEvent!.EventName);
    }

    [Fact]
    public async Task DeleteEvent_ReturnsNotFound_WhenEventDoesNotExist()
    {
        await using var context = CreateContext();
        var controller = new EventsController(context);

        var result = await controller.DeleteEvent(42);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteEvent_RemovesEventAndReturnsNoContent()
    {
        await using var context = CreateContext();
        context.Events.Add(
            new Event { EventId = 21, EventName = "ToDelete", Location = "L" });
        await context.SaveChangesAsync();

        var controller = new EventsController(context);
        var result = await controller.DeleteEvent(21);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, await context.Events.CountAsync());
    }
}