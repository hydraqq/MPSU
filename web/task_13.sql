DROP TABLE IF EXISTS booking CASCADE;
DROP TABLE IF EXISTS hotel_room CASCADE;
DROP TABLE IF EXISTS tourist_city CASCADE;
DROP TABLE IF EXISTS city CASCADE;
DROP TABLE IF EXISTS hotel CASCADE;
DROP TABLE IF EXISTS room_type CASCADE;
DROP TABLE IF EXISTS tourist CASCADE;
DROP TABLE IF EXISTS country CASCADE;

CREATE TABLE country (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    region TEXT
);

CREATE TABLE city (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    country_id INT REFERENCES country(id) ON DELETE SET NULL,
    population INT
);

CREATE TABLE tourist (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    birth_year INT
);

CREATE TABLE tourist_city (
    tourist_id INT NOT NULL REFERENCES tourist(id) ON DELETE CASCADE,
    city_id INT NOT NULL REFERENCES city(id) ON DELETE CASCADE,
    visited_at DATE NOT NULL,
    PRIMARY KEY (tourist_id, city_id)
);

CREATE TABLE hotel (
    id SERIAL PRIMARY KEY,
    city_id INT REFERENCES city(id) ON DELETE SET NULL,
    name TEXT NOT NULL,
    stars INT,
    year_opened INT
);

CREATE TABLE room_type (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    max_guests INT
);

CREATE TABLE hotel_room (
    hotel_id INT NOT NULL REFERENCES hotel(id) ON DELETE CASCADE,
    room_type_id INT NOT NULL REFERENCES room_type(id) ON DELETE CASCADE,
    rooms_available INT NOT NULL CHECK (rooms_available >= 0),
    PRIMARY KEY (hotel_id, room_type_id)
);

CREATE TABLE booking (
    id SERIAL PRIMARY KEY,
    tourist_id INT REFERENCES tourist(id),
    hotel_id INT REFERENCES hotel(id),
    room_type_id INT REFERENCES room_type(id),
    nights INT CHECK (nights > 0),
    check_in DATE,
    total_price NUMERIC
);

-- ДАННЫЕ
INSERT INTO country (id, name, region) VALUES
(1, 'Италия', 'Европа'),
(2, 'Япония', 'Азия'),
(3, 'Чили', 'Южная Америка'),
(4, 'Исландия', 'Европа'),
(5, 'Неизвестная страна', NULL);

INSERT INTO city (id, name, country_id, population) VALUES
(1, 'Рим', 1, 2800000),
(2, 'Милан', 1, 1400000),
(3, 'Токио', 2, 14000000),
(4, 'Саппоро', 2, 1900000),
(5, 'Сантьяго', 3, 5600000),
(6, 'Пунта-Аренас', 3, 130000),
(7, 'Рейкьявик', 4, 150000),
(8, 'Город-призрак', NULL, NULL);

INSERT INTO tourist (id, name, birth_year) VALUES
(1, 'Александр', 1990),
(2, 'Марина', 1985),
(3, 'Роберт', 1975),
(4, 'Турист без городов', 2000);

INSERT INTO tourist_city (tourist_id, city_id, visited_at) VALUES
(1, 1, '2022-05-10'),
(1, 3, '2023-11-01'),
(2, 2, '2024-02-12'),
(2, 4, '2025-01-15'),
(3, 5, '2025-03-18');

INSERT INTO hotel (id, city_id, name, stars, year_opened) VALUES
(1, 1, 'Roma Center Hotel', 5, 1990),
(2, 1, 'Budget Inn Rome', 3, 2005),
(3, 3, 'Tokyo Sky Hotel', 4, 2012),
(4, 3, 'Tiny Capsule Hotel', 2, 2018),
(5, 7, 'IceView Hotel', 4, 2020),
(6, NULL, 'Hotel Nowhere', 1, 2000);

INSERT INTO room_type (id, title, max_guests) VALUES
(1, 'Standard', 2),
(2, 'Deluxe', 3),
(3, 'Suite', 4),
(4, 'Capsule', 1);

INSERT INTO hotel_room (hotel_id, room_type_id, rooms_available) VALUES
(1, 1, 10),
(1, 2, 5),
(2, 1, 30),
(3, 1, 50),
(3, 3, 10),
(4, 4, 100),
(5, 3, 3);

INSERT INTO booking (tourist_id, hotel_id, room_type_id, nights, check_in, total_price) VALUES
(1, 1, 2, 3, '2025-10-10', 420),
(1, 3, 1, 5, '2025-11-15', 700),
(2, 4, 4, 2, '2025-09-01', 180),
(3, 5, 3, 1, '2025-07-22', 300);

-- 1.1 INNER JOIN: отели с городом и регионом
SELECT
    h.name AS hotel_name,
    c.name AS city_name,
    cnt.region
FROM hotel h
INNER JOIN city c ON h.city_id = c.id
INNER JOIN country cnt ON c.country_id = cnt.id;

-- 1.2 INNER JOIN: туристы и посещённые города
SELECT
    t.name AS tourist_name,
    c.name AS city_name,
    tc.visited_at
FROM tourist t
INNER JOIN tourist_city tc ON t.id = tc.tourist_id
INNER JOIN city c ON tc.city_id = c.id
ORDER BY t.name, tc.visited_at;

-- 2.1 LEFT JOIN: все города и отели в них
SELECT
    c.name AS city_name,
    h.name AS hotel_name
FROM city c
LEFT JOIN hotel h ON c.id = h.city_id
ORDER BY c.name;

-- 2.2 LEFT JOIN: туристы и количество посещённых городов
SELECT
    t.name AS tourist_name,
    COUNT(tc.city_id) AS cities_visited
FROM tourist t
LEFT JOIN tourist_city tc ON t.id = tc.tourist_id
GROUP BY t.id, t.name
ORDER BY t.name;

-- 3.1 RIGHT JOIN: все страны и их города
SELECT
    cnt.name AS country_name,
    c.name AS city_name
FROM city c
RIGHT JOIN country cnt ON c.country_id = cnt.id
ORDER BY cnt.name, c.name;

-- 3.2 RIGHT JOIN: все типы номеров и количество отелей с ними
SELECT
    rt.title AS room_type,
    COUNT(DISTINCT hr.hotel_id) AS hotel_count
FROM hotel_room hr
RIGHT JOIN room_type rt ON hr.room_type_id = rt.id
GROUP BY rt.id, rt.title
ORDER BY rt.title;

-- 4.1 FULL JOIN: города и отели
SELECT
    c.name AS city_name,
    h.name AS hotel_name
FROM city c
FULL JOIN hotel h ON c.id = h.city_id
ORDER BY c.name, h.name;

-- 4.2 FULL JOIN: типы номеров и отели
SELECT
    rt.title AS room_type,
    h.name AS hotel_name
FROM room_type rt
FULL JOIN hotel_room hr ON rt.id = hr.room_type_id
FULL JOIN hotel h ON hr.hotel_id = h.id
ORDER BY rt.title, h.name;

-- 5.1 CROSS JOIN: все пары (страна, тип номера)
SELECT
    cnt.name AS country,
    rt.title AS room_type
FROM country cnt
CROSS JOIN room_type rt
ORDER BY cnt.name, rt.title;

-- 5.2 CROSS JOIN: все пары (город, год открытия отеля)
SELECT DISTINCT
    c.name AS city,
    h.year_opened
FROM city c
CROSS JOIN (SELECT DISTINCT year_opened FROM hotel WHERE year_opened IS NOT NULL) h
ORDER BY c.name, h.year_opened;

-- 6.1 LATERAL JOIN: отель с максимальным количеством комнат в каждом городе
SELECT
    c.name AS city_name,
    h.name AS hotel_name,
    SUM(hr.rooms_available) AS total_rooms
FROM city c
JOIN LATERAL (
    SELECT h.id, h.name
    FROM hotel h
    WHERE h.city_id = c.id
    ORDER BY (SELECT SUM(hr.rooms_available) FROM hotel_room hr WHERE hr.hotel_id = h.id) DESC
    LIMIT 1
) h ON TRUE
JOIN hotel_room hr ON h.id = hr.hotel_id
GROUP BY c.id, c.name, h.id, h.name;

-- 6.2 LATERAL JOIN: последний визит для каждого туриста
SELECT
    t.name AS tourist_name,
    c.name AS city_name,
    tc.visited_at
FROM tourist t
JOIN LATERAL (
    SELECT tc.city_id, tc.visited_at
    FROM tourist_city tc
    WHERE tc.tourist_id = t.id
    ORDER BY tc.visited_at DESC
    LIMIT 1
) tc ON TRUE
JOIN city c ON tc.city_id = c.id;

-- 7.1 SELF JOIN: пары городов в одной стране
SELECT DISTINCT
    c1.name AS city_1,
    c2.name AS city_2,
    cnt.name AS country
FROM city c1
JOIN city c2 ON c1.country_id = c2.country_id
  AND c1.id < c2.id
  AND c1.country_id IS NOT NULL
JOIN country cnt ON c1.country_id = cnt.id
ORDER BY cnt.name, c1.name, c2.name;

-- 7.2 SELF JOIN: пары туристов, рождённых в один год
SELECT DISTINCT
    t1.name AS tourist_1,
    t2.name AS tourist_2,
    t1.birth_year
FROM tourist t1
JOIN tourist t2 ON t1.birth_year = t2.birth_year
  AND t1.id < t2.id
ORDER BY t1.birth_year, t1.name, t2.name;
