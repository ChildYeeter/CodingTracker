# Coding Tracker

A simple C# console application for tracking the amount of time I spend coding.

The application allows me to add, view, update, and delete coding sessions. Each session stores a start time and an end time, with the duration being calculated automatically rather than entered manually.

The project uses SQLite for storing the data and Dapper for interacting with the database. Spectre.Console is used to make the console interface a little more readable and interactive.

## Features

* Add coding sessions
* View saved coding sessions
* Update existing sessions
* Delete coding sessions
* Automatically calculate session duration
* Validate date and time input
* Prevent an end time from being earlier than the start time
* Validate integer input
* Store data persistently using SQLite
* Use Dapper for database operations
* Use Spectre.Console for formatted console output

## How to Use

### Requirements

* .NET SDK
* A C# development environment such as Visual Studio or VS Code

### Running the Application

Clone the repository and navigate to the project directory:

```bash
git clone https://github.com/ChildYeeter/CodingTracker.git
cd CodingTracker/CodingTracker
```

Then run:

```bash
dotnet run
```

The application will create the required SQLite database and table if they do not already exist.

### Date Format

When adding or updating a coding session, dates must be entered using:

```text
dd-MM-yyyy HH:mm:ss
```

For example:

```text
03-09-2026 14:30:00
```

The duration is calculated automatically from the start and end times.

## Technologies Used

* C#
* .NET
* SQLite
* Dapper
* Spectre.Console

## Resources

The idea for this project came from **The C# Academy**, which provides a series of C# project ideas for learning through practice.

While working on the project, I made use of resources such as:

* [Spectre.Console](https://spectreconsole.net/) — documentation for the console UI
* [Learn Dapper](https://www.learndapper.com/) — Dapper tutorials and examples
* Stack Overflow — troubleshooting and understanding errors
* YouTube tutorials — additional explanations and examples

## What I Learned

This project gave me practical experience with:

* Working with SQLite databases from C#
* Writing SQL queries
* Using Dapper to execute queries and map database results to C# objects
* Performing CRUD operations
* Working with `DateTime` and `TimeSpan`
* Validating user input
* Reading configuration from `appsettings.json`
* Using external NuGet libraries
* Building a more usable console interface

## Future Improvements

Some things I may add or improve in the future:

* Improve the table visualization and separate it from the CRUD logic
* Add more statistics about coding sessions
* Improve the overall console interface
* Add filtering or searching for sessions
* Refactor parts of the application as I learn more about C# and software design
* and probably some more things that i learn as i go lol
