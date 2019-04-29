#!/bin/bash

# 
#   Sets up the environment to be able to test against the Beta database in Azure. Your machine needs a proper firewall exception
#   to be able connect directly to the SQL Server instance in Azure.
#
#   Authors: James Eby
#

# Proper secret files need to exist to populate all other secrets that are true secrets. These files were pulled from 
. .secrets/set-external-resources-environment-variables.sh
. .secrets/set-sql-server-environment-variables.sh

echo 'goto http://localhost:5000/swagger to see API documentation'

cd tivBudget.Api
dotnet run watch
cd ..