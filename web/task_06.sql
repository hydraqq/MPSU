-- Схема library с таблицами books и authors

DROP SCHEMA IF EXISTS library CASCADE;

CREATE SCHEMA library;

-- Таблица books
CREATE TABLE library.books (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL UNIQUE,
    pages INTEGER NOT NULL,
    price NUMERIC(10, 2) NOT NULL DEFAULT 0
);

-- Таблица authors
CREATE TABLE library.authors (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    birth_year INTEGER
);
