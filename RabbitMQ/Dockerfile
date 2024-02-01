# Imagem oficial do RabbitMQ com a interface de gerenciamento
FROM rabbitmq:3-management

# Define a variável de ambiente para o RabbitMQ
ENV RABBITMQ_DEFAULT_USER=admin
ENV RABBITMQ_DEFAULT_PASS=admin

# Define variáveis de ambiente para o RabbitMQ apontar para o diretório de dados persistente
ENV RABBITMQ_MNESIA_BASE=/data/mnesia
ENV RABBITMQ_LOG_BASE=/data/log

# Cria o diretório de dados persistente
RUN mkdir -p /data/mnesia /data/log

# Cria os diretórios necessários e ajusta as permissões
RUN mkdir -p /data/mnesia /data/log && \
    chown -R rabbitmq:rabbitmq /data

# Não é necessário definir VOLUME para /var/lib/rabbitmq, pois estamos movendo a persistência para /data
# Isso é gerenciado pelo Home Assistant
#
# Configure o ponto de montagem para persistência de dados
#VOLUME ["/var/lib/rabbitmq"]

# Exponha as portas padrão do RabbitMQ
EXPOSE 5672 15672

# Comando para executar o servidor RabbitMQ
# Você pode adicionar comandos para configurar usuários e permissões aqui se necessário
CMD ["rabbitmq-server"]
