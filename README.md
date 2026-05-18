# DIS Project - Cycle Tracker

This project is a cycle tracking web application for the course **Databases and Information Systems**.

The app lets a user track menstrual cycles, daily logs, bleeding flow and physical symptoms.  
For this project, we use **one hardcoded test user** and therefore do not have a login system.

---

## Tech stack

### Backend

- ASP.NET Core
- Entity Framework Core
- PostgreSQL

### Frontend

- React
- Vite

---

## Project structure

```text
DIS-Project
├── DIS.Backend
│   ├── Data
│   │   └── AppDbContext.cs
│   ├── Models
│   │   ├── Person.cs
│   │   ├── Cycle.cs
│   │   ├── DailyLog.cs
│   │   ├── FlowLevel.cs
│   │   ├── PhysicalSymptom.cs
│   │   └── DailyLogSymptom.cs
│   ├── Migrations
│   ├── Program.cs
│   └── appsettings.Development.json
│
├── DIS.Frontend
│   ├── src
│   │   └── App.jsx
│   └── package.json
│
└── README.md
```

---

## Database structure

The database contains these main tables:

- `Persons`
- `Cycles`
- `DailyLogs`
- `FlowLevels`
- `PhysicalSymptoms`
- `DailyLogSymptoms`

Relationships:

- One `Person` can have many `Cycles`
- One `Cycle` can have many `DailyLogs`
- One `DailyLog` can have one `FlowLevel`
- One `DailyLog` can have many `PhysicalSymptoms`
- `DailyLogSymptoms` is the join table between `DailyLogs` and `PhysicalSymptoms`

---

## Local setup

### 1. Clone the project

```bash
git clone https://github.com/annhumle/DIS-Project.git
cd DIS-Project
```

If you need the setup branch:

```bash
git switch christine-setup
```

---

## PostgreSQL setup

Make sure PostgreSQL is installed and running.

Check PostgreSQL:

```bash
psql --version
pg_isready -h localhost -p 5432
```

You should see something like:

```text
localhost:5432 - accepting connections
```

Open PostgreSQL:

```bash
psql -d postgres
```

Create the database user and database:

```sql
CREATE ROLE cycle_user WITH LOGIN PASSWORD 'cycle_password';
CREATE DATABASE cycle_tracker OWNER cycle_user;
```

Exit PostgreSQL:

```sql
\q
```

If the role or database already exists, that is okay.

---

## Backend setup

Go to the backend folder:

```bash
cd DIS.Backend
```

The backend connection string is in:

```text
DIS.Backend/appsettings.Development.json
```

It should look like this:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cycle_tracker;Username=cycle_user;Password=cycle_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Install the Entity Framework tool if needed:

```bash
dotnet tool install --global dotnet-ef --version "9.*"
```

If it is already installed, update it:

```bash
dotnet tool update --global dotnet-ef --version "9.*"
```

Build the backend:

```bash
dotnet restore
dotnet build
```

Create/update the database tables:

```bash
dotnet ef database update
```

Run the backend:

```bash
dotnet run
```

The backend should run on:

```text
http://localhost:5221
```

---

## API endpoints

These endpoints can be tested in the browser:

```text
GET http://localhost:5221/
GET http://localhost:5221/api/person
GET http://localhost:5221/api/cycles
GET http://localhost:5221/api/cycles/1/logs
GET http://localhost:5221/api/dailylogs
GET http://localhost:5221/api/flow-levels
GET http://localhost:5221/api/symptoms
```

---

## Frontend setup

Open a new terminal and go to the frontend folder:

```bash
cd DIS.Frontend
```

Install dependencies:

```bash
npm install
```

Run the frontend:

```bash
npm run dev
```

The frontend should run on:

```text
http://localhost:5173
```

---

## Development split

### Anna: Cycle + Daily Log

Anna works mainly with:

- `Person`
- `Cycle`
- `DailyLog`

Relevant backend endpoints:

- `GET /api/cycles`
- `GET /api/cycles/{id}/logs`
- Later: `POST /api/dailylogs`

Relevant frontend parts:

- Cycle overview
- Daily log list
- Daily log form

---

### Christine: Flow + Symptoms

Christine works mainly with:

- `FlowLevel`
- `PhysicalSymptom`
- `DailyLogSymptom`

Relevant backend endpoints:

- `GET /api/flow-levels`
- `GET /api/symptoms`
- Later: `POST /api/dailylogs/{id}/flow`
- Later: `POST /api/dailylogs/{id}/symptoms`

Relevant frontend parts:

- Flow level selector
- Symptom selector
- Show flow and symptoms on daily logs
- Regex search/filter for symptoms

Example regex search:

```text
head.*
```

This should match:

```text
Headache
```

---

## Useful commands

### Check current Git status

```bash
git status
```

### Start backend

```bash
cd DIS.Backend
dotnet run
```

### Start frontend

```bash
cd DIS.Frontend
npm run dev
```

### Apply database migrations

```bash
cd DIS.Backend
dotnet ef database update
```

### Create a new migration after model changes

```bash
cd DIS.Backend
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

## Notes

- The app currently uses one hardcoded test user.
- There is no login system.
- `bin/`, `obj/` and `node_modules/` should not be committed to Git.
- PostgreSQL must be running before the backend can connect to the database.