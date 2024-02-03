#!/bin/sh

echo "Listando arquivos da pasta /data"
ls -lah /data

echo "criando pastas para o RabbitMQ"
mkdir -p /data/rabbitmq/mnesia
mkdir -p /data/rabbitmq/logs

echo "Alterando permissões das pastas"
chown -R rabbitmq:rabbitmq /data/rabbitmq

echo "Listando arquivos da pasta /data (2)"
ls -lah /data


# Iniciando o servidor RabbitMQ
echo "Iniciando o servidor RabbitMQ"
exec rabbitmq-server