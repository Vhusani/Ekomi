# EkomiOrderTab#

Getting Started

This project is built using Angular for the frontend, .NET 8 Web API for the backend, and MySQL as the database. Follow the steps below to get the application up and running.

1) Set Up the Database
- Open your MySQL environment (e.g MySQL Workbench, phpMyAdmin, or terminal).
- Create a new database. You can name it anything you like (e.g EkomiDB).

2) Run the SQL Scripts
- In the project folder, navigate to the /Database directory.
- You'll find two SQL files:
    EkomiDB.sql – Creates the necessary tables and schema.
    EkomiDBScripts.sql – Inserts initial data (such as products, Orders, OrderDetails).
- Execute EkomiDB.sql first, then EkomiDBScripts.sql on the database you created.
- This will set up all required tables, data, and stored procedures needed for the API to work.

3) Configure the API
- Open the backend project in your IDE (e.g Visual Studio or VS Code).
- Locate the appsettings.json or appsettings.Development.json file.
- Under "ConnectionStrings", find the Live property and replace the value with your actual MySQL connection string. For example:

"ConnectionStrings": {
  "Live": "server=localhost;database=ekomi_db;user=root;password=your_password;"
}

- Save the file and run the Web API project.

4) Access Swagger for API Testing
- Once the API is running, it will automatically open the Swagger UI (usually at https://localhost:<port>/swagger).
- Swagger provides a simple interface to view and test all available API endpoints.

5) Authenticate and Authorize
- Use the /auth endpoint (in Swagger) to log in and retrieve a JWT token.
- Once you receive the token:
- Click the Authorize button in Swagger (top right corner).
- Enter the token in the field and click Authorize again.
- This token will allow you to make authenticated requests to protected endpoints.

6) Run the Angular Frontend (Optional Step)
If you are testing the full application including the Angular frontend:
- Navigate to the Angular project folder in your terminal.
- Run npm install to install dependencies.
- Then run ng serve to start the development server.
- The frontend should be accessible at http://localhost:4200.
- Make sure the frontend is configured to point to the correct API base URL (check environment.ts file if needed).


That’s it! You’re all set to start making API requests


