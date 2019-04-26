#!/bin/bash

# 
#   Initializes a test trackItsValue database in a default MS SQL Docker image.
#
#   Authors: James Eby
#

docker rm tiv-sql-localtest -f

export DB__SERVERNAME=localhost
export DB__USERNAME='sa'
export DB__USERPASSWORD='Xatr7xEvwtUP'

db=trackItsValue
dacpac=tivBudget.Db.dacpac

# Only do this when you need to start over, and don't care if you are destroying the data of other applications that can be using the same localhost
# SQL Server instance (with the same sa password of course)
docker rm tiv-sql-localtest -f

echo 'Running a SQL Server Container'

docker run -e 'ACCEPT_EULA=Y' -e "SA_PASSWORD=$DB__USERPASSWORD" \
   -p 1433:1433 --name tiv-sql-localtest \
   -d registry.freebytech.com/freebytech-pub/msssql:latest

echo 'Sleeping for 20 seconds to wait for SQL Server to be available'
sleep 20s

docker exec tiv-sql-localtest bash -c "/opt/mssql-tools/bin/sqlcmd -S localhost -U '$DB__USERNAME' -P '$DB__USERPASSWORD' -Q 'CREATE DATABASE $db'"

#docker exec tiv-sql-localtest bash -c "mkdir /opt/db-dacpac"

docker cp ./$dacpac tiv-sql-localtest:/opt/downloads/$dacpac

#docker exec -it tiv-sql-localtest dotnet /opt/sqlpackage/sqlpackage.dll /tsn:localhost /tu:SA /tp:'Xatr7xEvwtUP' /A:Import /tdn:trackItsValue /sf:/opt/db-dacpac/tivBudget.Db.dacpac

docker exec -it tiv-sql-localtest bash -c "dotnet /opt/sqlpackage/sqlpackage.dll /Action:Publish /TargetConnectionString:\"Data Source=localhost;User ID=$DB__USERNAME;Password=$DB__USERPASSWORD;Database=$db;Pooling=False\" /SourceFile:/opt/downloads/$dacpac /p:CreateNewDatabase=false"
