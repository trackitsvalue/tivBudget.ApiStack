#!/bin/bash

# 
#   Initializes a test trackItsValue database in a default MS SQL Docker image.
#
#   Authors: James Eby
#

docker rm tiv-sql-localtest -f

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Xatr7xEvwtUP' \
   -p 1433:1433 --name tiv-sql-localtest \
   -d registry.freebytech.com/freebytech-pub/msssql:latest

#docker exec tiv-sql-localtest bash -c "/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Xatr7xEvwtUP' -Q 'CREATE DATABASE trackItsValue'"

#docker exec tiv-sql-localtest bash -c "mkdir /opt/db-dacpac"

docker cp ./tivBudget.Db.dacpac tiv-sql-localtest:/opt/downloads/tivBudget.Db.dacpac

#docker exec -it tiv-sql-localtest dotnet /opt/sqlpackage/sqlpackage.dll /tsn:localhost /tu:SA /tp:'Xatr7xEvwtUP' /A:Import /tdn:trackItsValue /sf:/opt/db-dacpac/tivBudget.Db.dacpac

docker exec -it tiv-sql-localtest bash -c "dotnet /opt/sqlpackage/sqlpackage.dll /Action:Publish /TargetConnectionString:\"Data Source=localhost;User ID=sa;Password=Xatr7xEvwtUP;Database=trackItsValue;Pooling=False\" /SourceFile:/opt/downloads/tivBudget.Db.dacpac /p:CreateNewDatabase=true"
