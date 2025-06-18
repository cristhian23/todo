#!/usr/bin/env bash
# wait-for-it.sh

host="$1"
shift
cmd="$@"

until nc -z $host; do
  echo "🔁 Esperando a que $host esté disponible..."
  sleep 2
done

echo "✅ $host está disponible. Ejecutando comando..."
exec $cmd
