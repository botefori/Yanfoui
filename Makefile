APP_NAME ?= yanfoui
DOCKER_NETWORK ?= yanfoui

#docker exec -it yafoui-app bash
#mysql -h database -u root -proot yanfoui_api

build:
	docker-compose build --no-cache
	
up:
	docker-compose up -d
	@make nginxup

nginxup:
	docker-compose exec -d app service nginx start




bash:
	docker exec -it yafoui-app bash

down:
	docker-compose down

clear:
	docker-compose down -v