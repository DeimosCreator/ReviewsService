# Сервис отзывов и рейтингов товаров

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-336791?logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black)

REST API сервис, позволяющий оставлять отзывы к товарам и получать сводный рейтинг.

## Стек

- **C# / ASP.NET Core** — REST API
- **Entity Framework Core** — работа с базой данных
- **PostgreSQL** — хранение отзывов
- **Docker / Docker Compose** — запуск в контейнерах
- **Swagger** — документация и тестирование API

## Архитектура

Проект разделён на слои:

- `Controllers` — обработка HTTP-запросов
- `Services` — бизнес-логика (создание отзывов, расчёт рейтинга)
- `Data` — контекст базы данных (EF Core)
- `Models` — сущности и DTO

## Запуск

Требуется установленный **Docker**.

1. Скопируйте файл с переменными окружения и при необходимости измените значения:

```bash
cp .env.example .env
```

2. Запустите сервис одной командой:

```bash
docker compose up --build
```

При старте автоматически поднимается PostgreSQL, применяются миграции и запускается API.

3. Сервис доступен по адресу:

- API: `http://localhost:8080`
- Swagger UI: `http://localhost:8080/swagger`

## Переменные окружения (.env)

| Переменная          | Описание                        |
|---------------------|---------------------------------|
| `POSTGRES_DB`       | Имя базы данных                 |
| `POSTGRES_USER`     | Пользователь базы данных        |
| `POSTGRES_PASSWORD` | Пароль базы данных              |
| `CONNECTION_STRING` | Строка подключения для API      |

Шаблон значений — в файле `.env.example`.

## Эндпоинты

### 1. Создать отзыв

```
POST /products
```

Тело запроса:

```json
{
  "product_id": 42,
  "rating": 5,
  "text": "Отличный товар!"
}
```

Поле `rating` принимает только целые числа от 1 до 5. При выходе за диапазон
возвращается `400 Bad Request` с описанием ошибки.

Пример:

```bash
curl -X POST http://localhost:8080/products \
  -H "Content-Type: application/json" \
  -d '{"product_id": 42, "rating": 5, "text": "Отличный товар!"}'
```

### 2. Получить отзывы товара

```
GET /products/{id}/reviews
```

Возвращает список всех отзывов конкретного товара. Если отзывов нет — пустой массив.

Пример:

```bash
curl http://localhost:8080/products/42/reviews
```

### 3. Получить рейтинг товара

```
GET /products/{id}/rating
```

Возвращает среднюю оценку и общее количество отзывов. Если у товара ещё нет
отзывов, возвращается `{"averageScore": 0, "reviewsCount": 0}`.

Пример:

```bash
curl http://localhost:8080/products/42/rating
```

Ответ:

```json
{
  "averageScore": 4,
  "reviewsCount": 3
}
```

## Обработка ошибок

- Валидация `rating` (только 1–5) — при нарушении `400 Bad Request`.
- Запрос рейтинга товара без отзывов не вызывает ошибку — возвращаются нулевые значения.
- Расчёт среднего рейтинга выполняется агрегирующим запросом на стороне базы данных.

## Остановка

```bash
docker compose down
```

Для полной очистки вместе с данными базы:

```bash
docker compose down -v
```
