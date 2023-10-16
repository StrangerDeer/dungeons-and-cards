# Dungeons and Cards

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Dungeons and Cards is my pet project. The concept is a multiplayer card game in fantasy setting. The players can compete against their friends and/or other players. Your main task is to defend your dungeon and your gold against other players, and to steal others' gold from their dungeons. You can play defender and raider cards. The players have their own user profiles, where they can view owned cards and their friends.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


### Built With

##Frontend:
* ![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)
* ![TypeScript](https://img.shields.io/badge/typescript-%23007ACC.svg?style=for-the-badge&logo=typescript&logoColor=white)
* ![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)

##Backend:
* ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
* ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

##Database:
* ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)


<p align="right">(<a href="#readme-top">back to top</a>)</p>


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/StrangerDeer/dungeons-and-cards.git
   ```
2. Restore the project's file
   ```sh
   dotnet restore
   ```
3. Create database in MSSQL with 'dscs' name

4. Run this command in the PowerShell after open the project directory in that
   ```sh
   dotnet ef migrations add Init
   ```
    ```sh
   dotnet ef database update
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>
  
## Usage

On working
<!-- ROADMAP -->
## Roadmap

- [x] Create Database in MSSQL
- [x] Can add new user and ban
- [ ] Authentication
    - [ ] JWT token
    - [ ] Backend
    - [ ] Frontend
    - [ ] Send authentication email
- [ ] Frontend registration, login, user profile
- [ ] Generate Cards
- [ ] User can get Cards
- [ ] User can play

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

[StrangerDeer](https://github.com/StrangerDeer)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Stranger Deer
makelnotw69@gmail.com

<p align="right">(<a href="#readme-top">back to top</a>)</p>


