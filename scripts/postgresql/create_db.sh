#!/bin/bash
set -e

POSTGRES="psql --username ${POSTGRES_USER}"

echo "Creating database"

$POSTGRES << EOSQL
CREATE DATABASE ${POSTGRES_DATABASE} OWNER ${POSTGRES_USER};
EOQSL