# LevelUp

LevelUp is a ASP.Net core web application that allows users to gamify life by completing real world tasks and stay connected to communities, all while keeping track of self care & mental health progress.

## Getting Started

### Prerequisites

* [pgAdmin](https://www.pgadmin.org/)
* [Visual Studio](https://visualstudio.microsoft.com/)

### Set up
1. Make a clone of this repo and open it in Visual Studio.
2. In visual studio naviagate to tools -> NuGet package manager -> Package Manager Console -> run update-database in the package manager console.
3. In either appsettings.json in the project or in your user sercret folder add:
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
| <img src="https://github.com/Eli-J-Paris.png?">    | <img src="https://github.com/Joe10111.png?">|<img src="https://github.com/RafiWick.png?"> |
|:---:|:--:|:----:|
| Eli Paris | Joe Centeno | Rafi Wick|
|  <a href="https://www.linkedin.com/in/eli-paris-96902a285/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"></img></a><a href="https://github.com/Eli-J-Paris"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></img></a>              |   <a href="https://www.linkedin.com/in/joe-centeno/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"></img></a><a href="https://github.com/joe10111"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></img></a>            |<a href="https://www.linkedin.com/in/raphael-wick-285489197/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"></img></a><a href="https://github.com/RafiWick"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></img></a>|
- Eli Paris [@Eli-J-Paris](https://github.com/Eli-J-Paris)
- Joe Centeno [@Joe10111](https://github.com/joe10111)
- Rafi Wick [@RafiWick](https://github.com/RafiWick)

## Features
- Ability to keep track of & create custom tasks
- Ability to "Level up" your account by completing tasks 
- AI generated profile pictures that change when you level up
- Dynamic radar graph that changes when you complete weekly tasks
- Camp system that allows multiple users to stay connected through a message board
  
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


