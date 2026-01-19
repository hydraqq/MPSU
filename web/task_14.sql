DROP TABLE IF EXISTS users CASCADE;

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL,
    phone TEXT NOT NULL,
    city TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);

-- ДАННЫЕ: 100,000 записей случайных данных
INSERT INTO users (username, phone, city, created_at)
SELECT
    'user_' || i,
    LPAD((RANDOM() * 9999999999)::BIGINT::TEXT, 10, '0'),
    (ARRAY['Moscow', 'Saint Petersburg', 'Novosibirsk', 'Yekaterinburg', 'Nizhny Novgorod', 'Kazan', 'Omsk', 'Samara', 'Rostov', 'Ufa'])[1 + (RANDOM() * 9)::INT],
    NOW() - (RANDOM() * 365)::INT * INTERVAL '1 day'
FROM GENERATE_SERIES(1, 100000) i;

-- ПУНКТ 3: Выборка по точному совпадению phone БЕЗ индекса
EXPLAIN ANALYZE
SELECT id, username, phone, city
FROM users
WHERE phone = '1234567890';

-- ПУНКТ 4: Создать обычный индекс на phone
CREATE INDEX idx_users_phone ON users(phone);

-- ПУНКТ 5: Повторить выборку с индексом на phone
EXPLAIN ANALYZE
SELECT id, username, phone, city
FROM users
WHERE phone = '1234567890';

-- ПУНКТ 6: Выборка по подстроке city БЕЗ индекса
EXPLAIN ANALYZE
SELECT id, username, phone, city
FROM users
WHERE city ILIKE '%ow%';

-- ПУНКТ 7: Создать индекс на вычисляемое выражение для ILIKE
CREATE INDEX idx_users_city_lower ON users(LOWER(city));

-- ПУНКТ 8: Повторить выборку с индексом на LOWER(city)
EXPLAIN ANALYZE
SELECT id, username, phone, city
FROM users
WHERE LOWER(city) LIKE '%ow%';
