# Imagem oficial do RabbitMQ com a interface de gerenciamento
FROM rabbitmq:3.12-management

# Define a vari�vel de ambiente para o RabbitMQ
ENV RABBITMQ_DEFAULT_USER=admin
ENV RABBITMQ_DEFAULT_PASS=admin

# Define vari�veis de ambiente para o RabbitMQ apontar para o diret�rio de dados persistente
ENV RABBITMQ_MNESIA_BASE=/data/rabbitmq/mnesia
ENV RABBITMQ_LOG_BASE=/data/rabbitmq/log

# Cria o diret�rio de dados persistente
#RUN mkdir -p /data/rabbitmq/mnesia /data/rabbitmq/log
#
# Cria os diret�rios necess�rios e ajusta as permiss�es
#RUN mkdir -p /data/rabbitmq/mnesia /data/rabbitmq/log && \
    #chown -R rabbitmq:rabbitmq /data/rabbitmq
#
#
# Copia arquivos de configura��o personalizados
#COPY rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
COPY configs/enabled_plugins /etc/rabbitmq/enabled_plugins

COPY start.sh /start.sh
RUN chmod +x /start.sh

# N�o � necess�rio definir VOLUME para /var/lib/rabbitmq, pois estamos movendo a persist�ncia para /data
# Isso � gerenciado pelo Home Assistant
#
# Configure o ponto de montagem para persist�ncia de dados
#VOLUME ["/var/lib/rabbitmq"]

# Exponha as portas padr�o do RabbitMQ
# 5672 � a porta de comunica��o padr�o
# 1883 � a porta de comunica��o MQTT
# 8883 � a porta de comunica��o MQTT com TLS
# 15672 � a porta da interface de gerenciamento
# 15674 � a porta de comunica��o STOMP com TLS
# 15675 � a porta de comunica��o HTTP/WebSockets
# 15692 � a porta de comunica��o HTTP/WebSockets com TLS
# 25672 � a porta de comunica��o segura (cluster)
# 61613 � a porta de comunica��o STOMP

EXPOSE 5672 15672 1883

# Comando para executar o servidor RabbitMQ
# Voc� pode adicionar comandos para configurar usu�rios e permiss�es aqui se necess�rio
#CMD ["rabbitmq-server"]
CMD ["/start.sh"]
