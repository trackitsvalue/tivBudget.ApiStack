#!/bin/bash

# 
#   Sets up the environment to be able to test against the Beta database in Azure. Your machine needs a proper firewall exception
#   to be able connect directly to the SQL Server instance in Azure.
#
#   Authors: James Eby
#

# Proper secret files need to exist to populate all other secrets that are true secrets. These files were pulled from 
. .secrets/set-external-resources-environment-variables-local.sh
. .secrets/set-sql-server-environment-variables.sh

export DB__ServerName=$AZURE_SQL_SERVER
export DB__UserName=$AZURE_SQL_SERVER_ADMIN_USER
export DB__UserPassword=$AZURE_SQL_SERVER_ADMIN_PASSWORD

echo 'goto http://localhost:5000/swagger to see API documentation'
echo 'connecting to' $DB__ServerName

cd tivBudget.Api
dotnet run watch
cd ..