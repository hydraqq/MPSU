# TechBlog

Блог о технологиях, компьютерах и IT-индустрии.

## Описание

Платформа для публикации и обсуждения статей на технологическую тематику: обзоры железа, новости из мира разработки, разборы инструментов.

## Технологии

- Python 3.11+
- Django 5.x
- PostgreSQL 15
- Bootstrap 5
- Docker Compose

## Запуск

1. Клонировать репозиторий
2. Создать виртуальное окружение: `python -m venv venv`
3. Активировать: `venv\Scripts\activate` (Windows) / `source venv/bin/activate` (Linux/Mac)
4. Установить зависимости: `pip install -r requirements.txt`
5. Создать файл `.env` на основе `.env.example`
6. Запустить базу данных: `docker-compose up -d`
7. Применить миграции: `python manage.py migrate`
8. Создать суперпользователя: `python manage.py createsuperuser`
9. Запустить сервер: `python manage.py runserver`

Сайт будет доступен по адресу `http://127.0.0.1:8000/`
