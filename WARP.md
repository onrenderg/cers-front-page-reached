# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

CERS (Campaign Expenditure Reporting System) is a .NET MAUI cross-platform mobile application for managing election campaign expenditures. The application supports Android, iOS, macOS Catalyst, and Windows platforms.

## Common Development Tasks

### Build Commands

```bash
# Build the entire solution
dotnet build

# Build for specific platform
dotnet build -f net8.0-android
dotnet build -f net8.0-ios
dotnet build -f net8.0-maccatalyst
dotnet build -f net8.0-windows10.0.19041.0

# Clean build
dotnet clean
dotnet build --no-incremental

# Release build
dotnet build -c Release
```

### Run Commands

```bash
# Run on default platform
dotnet run

# Run on specific platform
dotnet run -f net8.0-android
dotnet run -f net8.0-ios
dotnet run -f net8.0-maccatalyst
dotnet run -f net8.0-windows10.0.19041.0

# Run with hot reload
dotnet watch run
```

### Testing Commands

```bash
# Run unit tests (if test projects exist)
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

### Package Management

```bash
# Restore NuGet packages
dotnet restore

# Add a package
dotnet add package PackageName

# Update packages
dotnet restore --force-evaluate
```

### Deployment Commands

```bash
# Publish for Android
dotnet publish -f net8.0-android -c Release

# Publish for iOS (requires Mac)
dotnet publish -f net8.0-ios -c Release

# Create Android APK
dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
```

## Code Architecture

### High-Level Architecture

The application follows a typical MAUI MVVM architecture with the following key components:

1. **Entry Points & Configuration**
   - `MauiProgram.cs`: Application bootstrapping, DI container setup, platform-specific service registration
   - `App.xaml.cs`: Application lifecycle management, global state initialization, language/localization setup
   - `AppShell.xaml`: Navigation structure and routing configuration

2. **Data Layer (Models/)**
   - **Database Models**: Entity classes representing database tables (UserDetails, ExpenditureDetails, ObserverLoginDetails, etc.)
   - **Database Services**: SQLite database access classes with CRUD operations (*Database.cs pattern)
   - **Encryption**: `AESCryptography.cs` handles data encryption/decryption
   - **Platform Interface**: `ISQLite.cs` defines platform-specific SQLite connection interface

3. **Presentation Layer**
   - **Pages**: XAML views with corresponding code-behind files
     - Login flow: `LoginPage`
     - Main navigation: `DashboardPage`, `MorePage`
     - Expenditure management: `AddExpenditureDetailsPage`, `EditExpenditureDetailsPage`, `ViewExpenditureDetailsPage`
     - Observer module: `Observer/` subdirectory with specialized observer views
   - **WebView Integration**: `LoadWebViewPage` for displaying web content

4. **Network Layer (WebApi/)**
   - `HitServices.cs`: Centralized API service handling all HTTP requests, authentication, and response processing
   - Implements token-based authentication with Basic Auth fallback
   - Handles AES encryption for request/response data

5. **Platform-Specific Implementations (Platforms/)**
   - Each platform folder contains:
     - Platform-specific app initialization code
     - `MauiSQLite.cs`: SQLite database path configuration per platform
   - Android: MainActivity, AndroidManifest configuration
   - iOS/MacCatalyst: AppDelegate setup
   - Windows: Windows-specific App.xaml overrides

### Key Architectural Patterns

1. **Dependency Injection**: Services registered in MauiProgram.cs, accessed via DependencyService
2. **Repository Pattern**: Database classes provide abstraction over SQLite operations
3. **Localization System**: Multi-language support via LanguageMaster database with runtime switching
4. **User Session Management**: Dual user type system (Regular users vs Observers) with separate authentication flows
5. **Encrypted Communication**: All API communications use AES encryption with configurable base URLs

### Database Architecture

The application uses SQLite with multiple domain-specific databases:
- User authentication and preferences
- Expenditure tracking and payment modes
- Observer-specific data and ward management
- Language resources for localization
- Cached API responses and offline capability

### Security Considerations

- AES encryption for sensitive data transmission
- Token-based authentication with automatic refresh
- Encrypted local storage for user credentials
- Platform-specific secure storage via Preferences API

## Development Environment Setup

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (17.14+) with MAUI workload
- Platform-specific requirements:
  - Android: Android SDK, emulator or device
  - iOS: macOS with Xcode (for iOS development)
  - Windows: Windows 10/11 SDK

### Initial Setup

1. Clone the repository
2. Open `CERS.sln` in Visual Studio or use command line
3. Restore NuGet packages: `dotnet restore`
4. Select target platform in Visual Studio or use `-f` flag with dotnet CLI
5. Build and run the application

### API Configuration

The application connects to a backend WebAPI. Current endpoints are configured in `WebApi/HitServices.cs`:
- Development: `http://10.146.2.8/CERSWebApi/`
- Production: Update baseurl as needed

## Key Dependencies

- **Microsoft.Maui.Controls**: Core MAUI framework
- **sqlite-net-pcl**: SQLite database operations
- **Newtonsoft.Json**: JSON serialization
- **Microsoft.Maui.Essentials**: Device capabilities and platform APIs

## Important Notes

- The app supports two user types: Regular candidates and Observers, each with distinct workflows
- Language preference is stored locally and persists across sessions
- Database migrations are handled automatically on first run
- Offline mode is partially supported with local database caching
