using System;
using System.Linq;
using MiniDataManager;
using MiniDataManager.Models;
using MiniDataManager.Services;
using Spectre.Console;

var store = new DataStore<Movie>();

while (true)
{
    var choice = AnsiConsole.Prompt(
        new Spectre.Console.SelectionPrompt<string>()
            .Title("Välj ett alternativ:")
            .AddChoices(new[]
            {
                            "Visa",
                            "Lägg till",
                            "Uppdatera",
                            "Ta bort",
                            "Sortera",
                            "Avsluta"
            }));

    if (choice == "Visa")
    {
        if (!store.GetAll().Any())
        {
            AnsiConsole.MarkupLine("[red]Inga filmer sparade.[/]");
            continue;
        }

        foreach (var m in store.GetAll())
        {
            AnsiConsole.MarkupLine(
                $"{m.Title} | {m.Genre} | {m.Year} | {m.Price} kr");
        }
    }

    else if (choice == "Lägg till")
    {
        try
        {
            var movie = new Movie
            {
                Title = AnsiConsole.Ask<string>("Titel:"),
                Genre = AnsiConsole.Ask<string>("Genre:"),
                Year = AnsiConsole.Ask<int>("År:"),
                Price = AnsiConsole.Ask<decimal>("Pris:")
            };

            store.Add(movie);
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]Felaktig inmatning.[/]");
        }
    }

    else if (choice == "Uppdatera")
    {
        var title = AnsiConsole.Ask<string>("Titel att uppdatera:");
        var movie = store.GetAll()
                         .FirstOrDefault(m => m.Title == title);

        if (movie == null)
        {
            AnsiConsole.MarkupLine("[red]Filmen hittades inte.[/]");
        }
        else
        {
            movie.Price = AnsiConsole.Ask<decimal>("Nytt pris:");
            store.Save();
        }
    }

    else if (choice == "Ta bort")
    {
        var title = AnsiConsole.Ask<string>("Titel att ta bort:");
        store.Remove(m => m.Title == title);
    }

    else if (choice == "Sortera")
    {
        var sorted = store.GetAll().OrderBy(m => m.Year);

        foreach (var m in sorted)
        {
            AnsiConsole.MarkupLine($"{m.Title} ({m.Year})");
        }
    }

    else if (choice == "Avsluta")
    {
        break;
    }
}

