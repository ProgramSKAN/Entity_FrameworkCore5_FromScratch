# Entity_FrameworkCore5_FromScratch

1. create ASP.net core web app > Wiblib
2. create data clas libraries


packages:

Microsoft.EntityFrameworkCore > install in webapp,dataaccess project
Microsoft.EntityFrameworkCore.SQLServer > install in webapp,dataaccess

## DBContext
* Instance of DBContext represents a session with th DB which can be used to query and save instances of your entities to a database.
* its allows to perform
    * Manage database connection
    * configure model & relationship
    * Querying database
    * Saving data to database
    * configure change tracking
    * Caching
    * Transation management


* Model class + Mappings => Database 
* Database must be in synk with models and mappings

## Migrations in EFCore

