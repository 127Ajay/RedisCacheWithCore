# RedisCacheWithCore

## Overview

RedisCacheWithCore is a .NET 9.0 project that demonstrates the integration of Redis caching with a web application. The project includes APIs for interacting with external data sources and caching responses to improve performance and scalability.

## Features

-   **Redis Caching**: Implements caching using `StackExchange.Redis` for efficient data retrieval.
-   **RESTful APIs**: Provides endpoints for fetching and refreshing data.
-   **Integration with JSONPlaceholder**: Demonstrates interaction with a mock API for testing purposes.
-   **.NET 9.0**: Built using the latest features of .NET 9.0.

## Project Structure

```
RedisCacheWithCore/
├── Controllers/
│   └── JsonPlaceHolderController.cs   # Handles API requests
├── Models/
│   └── JsonPlaceHolderDTOs.cs         # Data Transfer Objects for API responses
├── Service/
│   ├── IJsonPlaceholderService.cs     # Interface for JSONPlaceholder service
│   ├── JsonPlaceholderService.cs      # Implementation of JSONPlaceholder service
│   └── Redis/
│       ├── IRedisService.cs           # Interface for Redis service
│       └── RedisService.cs            # Implementation of Redis service
├── Properties/
│   └── launchSettings.json            # Launch settings for the application
├── appsettings.json                   # Application configuration
├── appsettings.Development.json       # Development-specific configuration
├── Program.cs                         # Entry point of the application
├── RedisCacheWithCore.csproj          # Project file
└── RedisCacheWithCore.sln             # Solution file
```

## API Endpoints

The following endpoints are exposed by the application:

### `GET /api/todos`

Fetches a list of todos from the JSONPlaceholder API.

### `GET /api/comments`

Fetches a list of comments from the JSONPlaceholder API.

### `GET /api/photos`

Fetches a list of photos from the JSONPlaceholder API.

### `GET /api/posts`

Fetches a list of posts from the JSONPlaceholder API.

### `GET /api/refresh-cache`

Refreshes the Redis cache with the latest data.

## Prerequisites

-   .NET 9.0 SDK
-   Redis server running locally or accessible remotely
    -   Alternatively, you can use a Redis instance on Docker by running the following command:
        ```bash
        docker run --name dotnet-redis -p 6379:6379 -d redis
        ```

## Getting Started

1. Clone the repository:

    ```bash
    git clone <repository-url>
    cd RedisCacheWithCore
    ```

2. Restore dependencies:

    ```bash
    dotnet restore
    ```

3. Run the application:

    ```bash
    dotnet run
    ```

4. Access the API at `http://localhost:<port>`.

## Dependencies

The project uses the following NuGet packages:

-   `Microsoft.Extensions.Caching.StackExchangeRedis` (>= 9.0.2)
-   `StackExchange.Redis` (>= 2.8.31)
-   `Microsoft.AspNetCore.OpenApi` (>= 9.0.2)
-   `Scalar.AspNetCore` (>= 2.2.1)

## Configuration

The application settings can be configured in the `appsettings.json` and `appsettings.Development.json` files. Update the Redis connection string as needed.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Acknowledgments

-   [JSONPlaceholder](https://jsonplaceholder.typicode.com/) for providing mock API data.
-   [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/) for Redis integration.
