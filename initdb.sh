#!/bin/bash
set -e

POSTGRES="psql --username admin"

echo "Creating database"

$POSTGRES << EOSQL
CREATE DATABASE appdb OWNER admin;
EOSQL