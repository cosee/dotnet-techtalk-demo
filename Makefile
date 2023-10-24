CI_COMMIT_SHORT_SHA ?= development

publish:
	dotnet publish -c Release /p:InformationalVersion=$$(date '+%Y%m%d').$(CI_COMMIT_SHORT_SHA)

build-docker:
	docker build -t candybackend:snapshot .
