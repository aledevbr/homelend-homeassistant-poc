#!/bin/sh

echo "Listando arquivos da pasta /data"

ls -lah /data

# Iniciando o servidor RabbitMQ
echo "Iniciando o servidor RabbitMQ"
exec rabbitmq-server