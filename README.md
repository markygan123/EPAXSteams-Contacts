# EPAXSteams-Contacts
Marketing Contacts web app built in C#/.Net for EPAXSteams. Built to help manage the company's marketing contacts without paying for a CRM.


## Installation

Install Visual Studio 2022

```bash
  https://visualstudio.microsoft.com/
```
    
Install SQL Server Management Studio 

```bash
    https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
```

Set-up connection string and insert to appsettings.json (should look like this): 

```bash
    "Server=(local)\\sqlexpress;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
```


## Features

- Add new contact record
- Edit/update record information
- Delete contact records
- Search contacts
- Export records to excel


## Tech Stack

**Client:** CSS/Bootstrap 

**Server:** C#, .NET, EF Core

