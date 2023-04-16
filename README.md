# AutoHeadHunter
Автоматизация откликов и поднятия резюме на hh.ru без API-токена

# Stack

* .NET 6.0
* Quartz .NET
* MediatR 12.0
* Serilog

# Возможности

Данный сервис позволяет автоматически поднимать свое резюме каждые 4 часа по логину и паролю, без создания API-токена на стороне hh.ru.

Также стоит отметить возможность автоматического отклика на вакансии с возможность указать шаблон приветственного письма:

На портале hh.ru действует ограничение на количество откликов - 200 в день.

С помощью грамотной конфигурации целевых вакансий по ключевым словам вы можете автоматизировать процесс поиска работы.

# Конфигурация

Ключевым файлом конфигурации является [appsettings.json](https://github.com/bezlla/AutoHeadHunter/blob/master/src/HeadHunter.Application/appsettings.json).

В файле две секции - ```Serilog``` и ```HeadHunterSettings```, пройдемся по второй детальнее.

Пример конфигурации выглядит следующим образом:

```json
{
  "HeadHunterSettings": {
      "Users": [
        {
          "Username": "mail@gmail.com",
          "Password": "password",
          "ResumeConfigurations": [
            {
              "ResumeHash": "resume",
              "SearchQuery": "(\"c#\") and (\"разработчик\" OR \"backend\" OR \"developer\")",
              "LetterRespondTemplate": "",
              "ExpectSalary": 150000,
              "StopWords": [
                "Unity",
                "Bootstrap",
                "QA",
                "Xamarin",
                "Avalonia",
                "DevOps",
                "Data Engineer",
                "Java",
                "1C",
                "Школа",
                "IOS",
                "UX",
                "Тестировщик",
                "Стажер",
                "WinForm",
                "Windows",
                "Frontend Developer",
                "Копирайтер",
                "Аналитик",
                "C++"
              ],
              "AutoTouch": true,
              "AutoApply": true
            }
          ]
        }
      ]
   }
}
```
Имеется возможность задать конфигурацию для множества пользователей.

Поля ```Username``` и ```Password``` отвечают за логин и пароль от вашего личного кабинета на сайте hh.ru.

В разделе ```ResumeConfigurations``` указывается информация по резюме, с которыми предстоит работать сервису.

> ```ResumeHash``` - уникальный номер резюме, можно найти в ссылке на ваше резюме. aa29ae2bff0877e3eb0039ed1f555850364754

> ```SearchQuery``` - поисковый запрос вакансий. [Язык поисковых запросов](https://hh.ru/article/1175)

> ```LetterRespondTemplate``` - шаблон письма-отклика.

> ```ExpectSalary``` - ожидаемый уровень з/п

> ```StopWords``` - слова, которые не должны присутствовать в описании вакансии

> ```AutoTouch``` - автоподнятие резюме, при значении ```true``` поднимается в поиске каждые 4 часа

> ```AutoAppy``` - автоотклики на вакансии, при значении ```true``` автоматически откликается на подходящие вакансии каждые 12 часов


# Quick Start. Docker

[Docker Compose](https://github.com/bezlla/AutoHeadHunter/blob/master/docker-compose.yml)

Quick Start

```
docker pull bezla/headhunter
docker up bezla/headhunter
```
Обязательно стоит добавить свою конфигурацию через ```environment``` окружение.
