# Монолитное приложение для создания сокращённых ссылок

## Архитектура
Приложение следует принципам чистой архитектуры:
- Domain layer: предоставляет сущность домена 'ShortUrl'
- Application layer: содержит интерфейсы приложения, ожидаемые ошибки, обработчики PipelineBehavior, логику связанную с CQRS и сервис по созданию сокращённых ссылок
- Infrastructure layer: отвечает за работу с БД
- Presentation layer: собирает конфигурацию всего приложения и обеспечивает связь с клиентом путём API и сопутствующими элементами, также на этом слое находится папка 'wwwroot', в которой располагаются все элементы, связанные с front-end

- Каждый слой содержит свой собственный файл DependencyInjection с регистрацией и настройкой всех связанных с ним сервисов.
- В корне приложения находится Docker-compose.yaml и связанные с ним для конфигурации .env файлы
    - Все строки подключения, переменные окружения, логины и пароли не были спрятаны и вынесены в Git для удобства запуска репозитория и его проверки

### Использованные технологии
- ASP.Net Core (Web API framework)
- MediatR (CQRS + pipeline behaviors)
- Entity framework Core (ORM)
- MariaDb (DB)
- ErrorOr (Error handling)
- Fluent Validation (Model validation)
- Serilog (Logging)
- Mapperly (Mapping)
- Docker Desktop

## Инструкция по запуску проекта
Нужно полностью клонировать репозиторий, затем, находясь в корневой папке приложения, запустить консольную команду:
``` Shell
docker-compose -f Docker-compose.development.yaml up -d
```
После использования команды желательно немного подождать, чтобы внешние сервисы успели раскрутиться и у внутренних не возникло ошибки при подключении к недонастроенным сервисам.

Дальше запускаем в компиляторе Shortly.sln.

Приложением можно начать пользоваться, открыв его главную страницу, находящуюся по пути:
...\Shortly\src\Shortly.Presentation\wwwroot\html\index.html

Или же можно протестировать только back-end часть, используя SwaggerUI по ссылке: 
https://localhost:7111/swagger/index.html
