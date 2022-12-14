# EPAXSteams-Contacts
Marketing Contacts web app built in C#/.Net for EPAXSteams. Built to manage the company's marketing contacts without paying for a CRM.

![image](https://user-images.githubusercontent.com/11957289/181696815-95a38476-26ea-43a1-b434-b876061ebe06.png)


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

