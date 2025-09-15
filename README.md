# Login_Registration

## Overview

Login_Registration is a sample .NET MAUI mobile application designed to help Software Testers practice and write automation tests. The project demonstrates core mobile app features and security flows, making it ideal for learning and testing automation with frameworks like Appium.

## Features

- **Login Page**
  - Secure user authentication
  - Username and password fields
  - Error messages for invalid credentials and lockout
  - Lockout after 3 failed login attempts (with countdown timer)
  - Navigation to registration and forgot password pages

- **Registration Page**
  - Full Name, Username, Password, Confirm Password fields
  - Validation for required fields and password length
  - Unique username enforcement
  - Back button to return to login

- **Dashboard (MainPage)**
  - Personalized welcome message with user's full name
  - Displays login count for the user
  - Logout button

- **Forgot Password Page**
  - Username, New Password, Confirm New Password fields
  - Validation for required fields, password length, and password change
  - Error messages for invalid input
  - Success alert and navigation back to login
  - Back button to return to login

- **Security**
  - Lockout state and countdown timer persisted in local SQLite database
  - Password reset only for existing usernames and cannot reuse previous password

## Technologies Used

- .NET MAUI (Android, Windows, iOS)
- MVVM architecture
- SQLite for local data persistence
- Appium (recommended for automation testing)

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone <repo-url>
   ```
2. **Open the solution in Visual Studio 2022+** (with .NET 9 and MAUI workloads installed).
3. **Restore NuGet packages** and build the solution.
4. **Run the app** on Android, Windows, or iOS emulator/device.

## Automation Testing Guidance

- The app is designed for UI automation testing using Appium or similar frameworks.
- Testers can automate:
  - Login and registration flows
  - Lockout and countdown timer scenarios
  - Password reset and validation
  - Navigation between pages
  - Dashboard content verification
- All UI elements have clear bindings and error messages for easy assertion in tests.

## Folder Structure

- `Models/` - Data models for user, login, registration
- `ViewModels/` - MVVM view models for each page
- `Views/` - XAML pages for UI
- `Services/` - Data access and persistence logic
- `Converters/` - Value converters for UI bindings
- `Resources/` - Styles and assets

## Contributing

This project is intended for learning and demonstration. Contributions for improved testability, new scenarios, or documentation are welcome.

## License

This sample project is provided for educational purposes.