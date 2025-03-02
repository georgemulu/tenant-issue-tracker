**Tenant Issue Tracker**
A .NET-based web application designed to streamline communication between tenants and caretakers for efficient issue resolution. Tenants can report housing problems such as plumbing, electrical, or general maintenance, while caretakers can manage and resolve these issues effectively.

**Features**
**User-Friendly Reporting**: Tenants can submit detailed issue reports with descriptions, categories, and attachments.

**Caretaker Dashboard**: A centralized dashboard to view, update, and track issue status.

**Real-Time Notifications**: Email and SMS alerts keep tenants informed about updates.

**Feedback System**: Tenants can provide feedback and ratings for resolved issues.

**How to Set Up the Project**
Follow these steps to set up the project locally on your machine:

🔧**Prerequisites**
.NET SDK (version 8.0 or higher)

Visual Studio or Visual Studio Code

Git

A database system (e.g., SQL Server, SQLite, or PostgreSQL)

⚙️**Steps**
**Clone the Repository:**

git clone https://github.com/your-username/tenant-issue-tracker.git
cd tenant-issue-tracker

**Restore Dependencies:**

dotnet restore

**Set Up the Database:**

Update the connection string in appsettings.json to match your local database configuration.

**Run the database migrations:**
dotnet ef database update

**Run the Application:**

dotnet run
The application should start running at http://localhost:5050 (or another port specified in the logs).

**Access the Application:**

Open your browser and navigate to http://localhost:5000 to view the application.

**How to Collaborate on the Project**
We welcome contributions! Here’s how you can collaborate on the project:

**1. Fork the Repository**
Click the "Fork" button on the top right of the repository page to create your own copy of the project.

**2. Clone Your Fork**
git clone https://github.com/your-username/tenant-issue-tracker.git
cd tenant-issue-tracker

**3. Create a New Branch**
Always create a new branch for your changes:
git checkout -b feature/your-feature-name

**4. Make Your Changes**
Implement your changes or fixes in the codebase.

**5. Commit and Push Your Changes**
git add .
git commit -m "Add your commit message here"
git push origin feature/your-feature-name

**6. Create a Pull Request (PR)**
Go to the original repository on GitHub and click "New Pull Request."

Select your branch and provide a clear description of your changes.

Submit the PR and wait for feedback.

**7. Sync with the Main Repository**
If the main repository has been updated while you were working, sync your fork:

git remote add upstream https://github.com/georgemulu/tenant-issue-tracker.git
git fetch upstream
git merge upstream/main

**Code of Conduct**
Please read our Code of Conduct before contributing to ensure a welcoming and inclusive environment for everyone.

**License**
This project is licensed under the MIT License.

Feel free to reach out to the maintainers if you have any questions or need assistance. Happy coding! 🚀
