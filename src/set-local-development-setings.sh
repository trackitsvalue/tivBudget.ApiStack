#!/bin/bash

# 
#   Sets up the environment to be able to test against a local Docker database build via build-db-image.sh.
#
#   Authors: James Eby
#

# Proper secret files need to exist to populate all other secrets that are true secrets. These files were pulled from 
. .secrets/set-external-resources-environment-variables.sh
cd ../build-db-output
. ./build-db-image.sh

echo 'goto http://localhost:5000/swagger to see API documentation'

cd ../src/tivBudget.Api
dotnet run watch
cd ..