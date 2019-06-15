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
	docker-compose exec -d app-dev service nginx start

	
	
bash:
	docker exec -it yanfoui-app-dev bash

watch:
		docker-compose run --rm -v "${CURDIR}/docker/developpement/wait-mysql.sh:/wait.sh" app-dev /wait.sh
		docker run --rm -it -v "$(PWD)/app:/app/" -e ASPNETCORE_ENVIRONMENT=Development -w /app yanfoui-app-dev dotnet watch run --urls="http://0.0.0.0:5000"

down:
	docker-compose down --remove-orphans

clear:
	docker-compose down -v