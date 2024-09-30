# StarWarsAPI Project

This project is a .NET 8 API developed using Clean Architecture principles. The application allows users to interact with Star Wars data, specifically focusing on Starships, using the StarWars API (https://swapi.dev). The API is designed to be flexible, maintainable, and scalable, utilizing the Backends for Frontends (BFF) pattern for efficient communication between the API and frontend.

## Live Demo

- **API (Swagger Documentation):** [StarWarsAPI](https://starwarsapi-e9evg0b8dhg8czbw.canadacentral-01.azurewebsites.net/swagger/index.html)
- **Frontend:** [StarWars Starships](https://thankful-desert-04bffb50f.5.azurestaticapps.net/)

## Features

- **Starship List:** Users can view a comprehensive list of Starships from the StarWars universe.
- **Manufacturer Filter:** A dropdown allows users to filter Starships by manufacturer, enhancing the user experience.
  
The frontend communicates with this API to fetch the Starship data and allows users to interact with it seamlessly.

## Project Structure

The API follows Clean Architecture principles, with the following folder structure:

1. **Presentation** - Responsible for the API controllers and the external interfaces.
2. **Application** - Contains the application logic, including service interfaces and use cases.
3. **Domain** - The core business logic and domain models.
4. **Infrastructure** - Implements the persistence, external API connections, and other low-level services.

## Technologies Used

- **.NET 8**: The API is built on the latest version of .NET, taking advantage of performance and feature improvements.
- **StarWars API (https://swapi.dev)**: The external API providing the data for Starships and other Star Wars entities.
- **Backends for Frontends (BFF) pattern**: This pattern allows for a tailored API experience for the frontend, ensuring that the API is optimized for client consumption.

## How to Run

1. Clone the repository and navigate to the project folder.
2. Ensure that you have .NET 8 SDK installed.
3. Build the project: dotnet build
4. Run the application: dotnet run
5. Open the API Swagger documentation in your browser at: https://localhost:5001/swagger/index.html

   ```bash
   dotnet build
