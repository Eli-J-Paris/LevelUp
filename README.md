**LevelUp: Take your life to the next level!**

**Overview**: LevelUp is an innovative ASP.Net Core web application that melds task management with gamification. Offering a unique approach to accomplishing real-world tasks, promoting community connection, and visualizing personal growth.

<img width="500" alt="image" src="https://github.com/Eli-J-Paris/LevelUp/assets/130601227/c139e175-a197-448c-a219-e184646eff51"> <img width="500" alt="image" src="https://github.com/Eli-J-Paris/LevelUp/assets/130601227/8a0ba21b-ba22-47e3-aa4f-259c9f1e5424">

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

**Features**:
1. **Task Management:**
    - **Task Tracking**: Users have the capability to maintain an organized list of tasks, ensuring nothing falls through the cracks.
    - **Custom Tasks**: Beyond the preset tasks, LevelUp offers a customization feature, allowing users to create daily, weekly and todo tasks tailored to their unique goals / aspirations.
2. **Gamified Progression**:
    - **Leveling System**: As users complete their tasks, they are rewarded by "leveling up" their account, turning mundane tasks into an engaging experience.
    - **AI-Generated Profile Pictures**: Leveraging the power of OpenAI API, users will receive AI-generated profile pictures that evolve as they level up, providing a visual representation of their progress.
3. **Visualized Achievement**:
    - **Dynamic Radar Graph**: This interactive graph alters based on the completion of weekly tasks, giving users a visual snapshot of their weekly achievements and areas of focus.
4. **Community Connectivity**:
    - **Camp System**: More than just individual achievement, LevelUp promotes community. The camp system enables groups of users to stay active and stay motivated in like minded communities. 
    - **Message Board**: Within each camp, users can communicate, share updates, and motivate one another via a built-in message board.

**Tech Stack**: LevelUp is constructed on a foundation of robust and contemporary technologies:
- **Backend**:
    - **C#.net & ASP.Net Core**: These form the backbone of the application, ensuring smooth operations, robustness, and scalability.
    - **PostgreSQL**: As the database solution, PostgreSQL guarantees data integrity, performance, and reliability.
    - **OpenAI API**: Powers the AI-generated profile pictures, bringing an element of machine learning and uniqueness to user profiles.
    - **Serilog**: This ensures efficient logging, aiding in troubleshooting and monitoring.
    
- **Frontend**:
    - **HTML, CSS, & Bootstrap**: These technologies ensure a responsive and visually appealing interface for the users, enhancing the overall user experience.
    - **JavaScript**: Powers the dynamic elements of the web application, like the interactive radar graph.
    
- **Testing & Quality Assurance**:
    - **XUnit Testing**: Ensures that the software components work as intended, promoting reliability and stability.

## Acknowledgments
- [@Skylarsandler](https://github.com/skylarbsandler)
- [@Jcepriano](https://github.com/jcepriano)
- [@Jeremy-kimball](https://github.com/jeremy-kimball)


