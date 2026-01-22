# DigitalOOH - Digital Out-of-Home Ad Management System

## Summary
DigitalOOH is a Digital Out-of-Home (DOOH) advertisement management system that allows administartors to manage ads, campaigns, and screens, and enables display screens to retrieve their scheduled ad playlists dynamically.

The system consist of:

- A backend REST API built with ASP.NET Core
- A frontend application built with Angular 17
- A SQL Server database for presistent storage
- A public playlist API that screens can call to retrieve active ads based on time and campaig rules

<hr>

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8)
- **Frontend:** Angular CLI 17
- **Database:** Microsoft SQL Server 2025 ( Developer Edition )
- **ORM:** Entity Framework (Code-First with Migrations)
- **Authentication:** JWT (Access Token only)
- **API Documentation:** Swagger

<hr>

## Setup Steps

### Prerequisites

- .NET SDK 8+
- Node.js (20.20.0)
- Angular CLI 17
- SQL Server 2025 (Developer Edition)
- SSMS (optional, for database inspection)

<hr>

### Backend Setup

1. **Clone the repository**
```bash
    git clone https://github.com/GyanjuRai/DigitalOOH.git
```

2. **Configure database and JWT**
Create `appsettings.Development.json` (not tracked in source code):

```json
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=.;Database=DigitalOOH;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Jwt": {
        "Audience": "DigitalOOHAudience",
        "Issuer": "DigitalOOHIssuer",
        "SecretKey": "<your-secret-key>",
        "AccessTokenExpirationMin": 60,
        "AccessTokenClockSkewMin": 5
    }
}
```

3. **Apply EF Core migrations**
```bash
    dotnet ef database update
```

4. **Run the backend**
use the provide launch profile
```bash
    dotnet run
```

The API will be available at:
```bash
    http://localhost:5200
```

### Backend Configuration Notes
The backend uses a central configuration file located at `API/Const/AppData.cs` to define API metadata such as the API title, version, description, and Swagger-related settings

3. **Swagger UI**
```bash
    http://localhost:5200/swagger
```

<hr>

## Frontend Setup

1. Navigate to the frontend directory
```bash
    npm install
```

2. Ensure the following config file exist
`src/assets/const/app-const.json`
```json
    {
        "environment": "Dev",
        "ApiBaseUrl": "http://localhost:5200/",
        "apiSegment": "api/",
        "webUrl": "http://localhost:4200",
        "storageKey": "",
        "secretKey": ""
    }
```
`src/assets/const/app-const-prod.json`
(production equivalents as needed)

3. Run the frontend
```bash
    ng serve
```
Frontend will run at:
```bash
    http://localhost:4200
```
<hr>

## API Documentation
Swagger/OpenAPI documentation is available at:
```bash
    http://localhost:5200/swagger
```

<hr>

## Available APIs

**Ads Management**

- `GET /api/AdsMng/GetAds`
- `GET /api/AdsMng/GetAdsForDropdown`
- `POST /api/AdsMng/AdAdd`
- `DELETE /api/AdsMng/AdRemove/{id}`

**Campaigns**

- `GET /api/Screens/GetScreens`
- `GET /api/Screens/GetScreenForDropdown`
- `POST /api/Screens/ScreenEdit`
- `GET /api/Screens/GetPlaylist/{ScreenId}/playlist`

**Account**

- `POST /api/Account/Login`

<hr>

## Example API Request & Response

**Get Screen Playlist**

**Endpoint**

```bash
    GET /api/Screens/GetPlaylist/{ScreenId}/playlist
```

**Description**
Retrieves the active ad playlist for a specific screen at the requested time.
This endpoint is publicy accessible to allow screens to fetch playlist without authentication.
It also log proof-of-play data for analytics and to know what actually played.

**Request**
```bash

    GET /api/Screens/GetPlaylist/3fa85f64-5717-4562-b3fc-2c963f66afa6/playlist?At=2026-01-22T10:00:00Z

```

**Response**
```json

    {
        "message": "Playlist retrieved successfully",
        "type": "Sucess",
        "data": [
            {
                "adId": "f7ca3001-28f3-4f12-97a4-3c3aec01756a",
                "mediaUrl": "/media/videos/5565f8b8-148a-4931083150dfeb36462b6b.mp4",
                "durationSeconds": 120
            },
            {
                "adId": "23c2c111-82c9-40b8-a3ab-b7c6243c49e1",
                "mediaUrl": "/media/images/1c01fabc-93a5-4cbc-b0a9-d510dd3011c2.png",
                "durationSeconds": 120
            }
        ]
    }

```

<hr>

## Authentication
- JWT-based authentication
- Login endpoint issues **access tokens only** ( no refresh token logic)
- Protected APIs require:

    ```

    Authorization: Bearer <token>

    ```
- Public endpoints:
 - `POST /api/Account/Login`
 - `GET /api/Screens/GetPlaylist/{ScreenId}/playlist`

<hr>

## Key Assumptions
- Screens are always online when requesting playlists
- Campaign activation depends on current server time
- One playlist request represents a proof-of-play event
- Token refresh and long-lived sessions are out of scope
- Media files (images and video) are stored under `wwwroot/media/images` and `wwwroot/media/videos`. These directories are automatically created at runtime by the media upload service if they do not exist.

<hr>

## Known Limitations
- No refresh token or session renewak mechanism
- No role-based authorization 
- Minimal frontend UI intended only for basic management
- No responsive UI

<hr>

## Running the Application
1. Start SQL Server (optional)
2. Run backend (`dotnet run`)
3. Run frontend (`ng serve`)
4. Open Swagger to verify APIs
5. Use frontend or API clients to manage ads and campaigns