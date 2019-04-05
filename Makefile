APP_NAME ?= yanfoui
DOCKER_NETWORK ?= yanfoui

#docker exec -it yafoui-app bash

build:
	docker-compose build --no-cache
	
up:
	docker-compose up -d

fup:
	docker-compose up -d --force-recreate

down:
	docker-compose down

clear:
	docker-compose down -v