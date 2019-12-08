# Documentation

## API
- clone repository
- launch solution by Visual Studio 2019
- open packge manager console
- change default project to DateSelector.API
- run command `update-database`
- run API project
- you can access to the project swagger via url `/swagger/index.html`

## Caller (Console aplication)
- run your API project
- copy url of your localhost
- open HttpCaller class
- change `_apiUrl` constant with copied url.
- run console application project

The rest of Caller project is pretty self descriptive.

## Persitence
- you can find connection string in `appsettings.Development.json` file
- in Microsoft SQL Server Management use Windows Authentication and default `(localdb)\MSSQLLocalDB` Server name
- default database name - `DateSelectorDB`

If you successfully configure API you should see two default row in database. 
First is range withing current day and tomorrow.
Second - today and (today + 10 days).
These two together with Swagger support were provided just for demonstration purpose.
