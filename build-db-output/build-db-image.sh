#!/bin/bash

# 
#   Initializes a test trackItsValue database in a default MS SQL Docker image.
#
#   Authors: James Eby
#

docker rm tiv-sql-localtest -f

pswd=Xatr7xEvwtUP
db=trackItsValue
dacpac=tivBudget.Db.dacpac

docker run -e 'ACCEPT_EULA=Y' -e "SA_PASSWORD=$pswd" \
   -p 1433:1433 --name tiv-sql-localtest \
   -d registry.freebytech.com/freebytech-pub/msssql:latest

docker exec tiv-sql-localtest bash -c "/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '$pswd' -Q 'CREATE DATABASE $db'"

#docker exec tiv-sql-localtest bash -c "mkdir /opt/db-dacpac"

docker cp ./$dacpac tiv-sql-localtest:/opt/downloads/$dacpac

#docker exec -it tiv-sql-localtest dotnet /opt/sqlpackage/sqlpackage.dll /tsn:localhost /tu:SA /tp:'Xatr7xEvwtUP' /A:Import /tdn:trackItsValue /sf:/opt/db-dacpac/tivBudget.Db.dacpac

#docker exec -it tiv-sql-localtest bash -c "dotnet /opt/sqlpackage/sqlpackage.dll /Action:Publish /TargetConnectionString:\"Data Source=localhost;User ID=sa;Password=$pswd;Database=$db;Pooling=False\" /SourceFile:/opt/downloads/$dacpac /p:CreateNewDatabase=true"
