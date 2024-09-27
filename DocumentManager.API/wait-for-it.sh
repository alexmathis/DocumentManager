#!/bin/sh

# Usage: ./wait-for-postgres.sh documentmanager_db 5432

HOST="$1"
PORT="$2"
API_CMD="${@:3}"  # Collect all arguments after host and port for the API command

echo "Waiting for $HOST:$PORT..."

# Loop until PostgreSQL is reachable
while ! telnet $HOST $PORT 2>&1 | grep -q 'Connected'; do
  echo "Postgres is unavailable - sleeping"
  sleep 1
done

echo "Postgres is up - continuing"

# Start the API
exec $API_CMD