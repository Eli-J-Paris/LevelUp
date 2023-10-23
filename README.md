# LevelUp

LevelUp is a ASP.Net core web application that allows users to gamify life by completing real world tasks and stay connected to communities, all while keeping track of self care & mental health progress.

## Getting Started

### Prerequisites

* [pgAdmin](https://www.pgadmin.org/)
* [Visual Studio](https://visualstudio.microsoft.com/)

### Set up
1. Make a clone of this repo and open it in Visual Studio.
2. In visual studio naviagate to tools -> NuGet package manager -> Package Manager Console -> run update-database in the package manager console.
3. In either appsettings.json in the project or in your users sercret folder add:
   ```
   {
   "LEVELUP_DBCONNECTIONSTRING": "Server=localhost;Database=LevelUp;Port=5432;Username=YOURPGADMINUSERNAME;Password=YOURPGADMINPASSWORD",
   }
   ```
4. (optional): In your users secret folder add:
   ```
   {
   "LEVELUP_APICONNECTIONKEY": "YOUR OWN UNIQUE DALLE API KEY"
   }
   ```
    *If you don't have an API key the program will still run succesfully, but your account wont have AI generated profile pictures.*   
5. Thats it, you are ready to start Leveling Up!

## Context
Level up is group school project designed, devloped, & tested in 8 days by 
- Eli Paris [@Eli-J-Paris](https://github.com/Eli-J-Paris)
- Joe Centeno [@Joe10111](https://github.com/joe10111)
- Rafi Wick [@RafiWick](https://github.com/RafiWick)

## Features

## Tech Stack
- C#.net
- ASP.Net Core
- PostgreSQL
 -OpenAI API
- XUnit Testing
- Serilog
- JavaScript
- HTML
- CSS
- Bootstrap

## Acknowledgments
- [@Skylarsandler](https://github.com/skylarbsandler)
- [@Jcepriano](https://github.com/jcepriano)
- [@Jeremy-kimball](https://github.com/jeremy-kimball)


