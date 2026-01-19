DROP TABLE IF EXISTS orders_import_lines CASCADE;

CREATE TABLE orders_import_lines (
    id SERIAL PRIMARY KEY,
    source_file TEXT NOT NULL,
    line_no INT NOT NULL,
    raw_line TEXT NOT NULL,
    imported_at TIMESTAMPTZ DEFAULT NOW(),
    note TEXT
);

-- ДАННЫЕ
INSERT INTO orders_import_lines (source_file, line_no, raw_line, note) VALUES
('marketplace_A_2025_11.csv', 1, 'Order#1001; Customer: Olga Petrova <olga.petrova@example.com>; +7 (921) 555-12-34; Items: SKU:AB-123-XY x1', 'order row'),
('marketplace_A_2025_11.csv', 2, 'Order#1002; Customer: Ivan <ivan@@example..com>; 8-921-5551234; Items: SKU:zx9999 x2', 'order row'),
('newsletter_upload.csv', 10, 'john.doe@domain.com; +44 7700 900123; tags: promo, holiday', 'marketing upload'),
('pricing_feed.csv', 3, 'product: ZX-11; price: "1,299.99" USD', 'price row'),
('pricing_feed.csv', 4, 'product: Y-200; price: "2 500,00" EUR', 'price row'),
('catalog_tags.csv', 1, 'tags: electronics, mobile,  accessories', 'tags row'),
('catalog_tags.csv', 2, 'tags: home,kitchen', 'tags row'),
('orders_dirty.csv', 5, '"Smith, John","12 Baker St, Apt 4","1,200.00","SKU: AB-123-XY"', 'dirty csv'),
('processor_log.txt', 100, 'INFO: Processing order 1001', 'log'),
('processor_log.txt', 101, 'warning: price parse failed for line 4', 'log'),
('processor_log.txt', 102, 'Error: invalid phone for order 1002', 'log'),
('processor_log.txt', 103, 'error: missing sku in items list', 'log'),
('marketplace_A_2025_11.csv', 20, 'Customer: bad@-domain.com; +7 921 ABC-12-34; Items: SKU: 12-AB-!!', 'trap-invalid-email-phone-sku'),
('orders_dirty.csv', 6, '"O''Connor, Liam","New York, NY","500"', 'dirty csv with apostrophe');

-- ЗАДАНИЕ 1: Найти строки с корректным email (~)
SELECT
    id,
    source_file,
    line_no,
    raw_line
FROM orders_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- ЗАДАНИЕ 2: Найти строки БЕЗ корректного email (!~)
SELECT
    id,
    source_file,
    line_no,
    raw_line
FROM orders_import_lines
WHERE raw_line !~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- ЗАДАНИЕ 3: Извлечь первый email (regexp_match)
SELECT
    id,
    source_file,
    line_no,
    (regexp_match(raw_line, '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}'))[1] AS email
FROM orders_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- ЗАДАНИЕ 4: Извлечь все SKU (regexp_matches)
SELECT
    id,
    source_file,
    line_no,
    (regexp_matches(raw_line, '[a-zA-Z0-9]+-[a-zA-Z0-9]+-?[a-zA-Z0-9]*|[a-zA-Z]{2}[0-9]+', 'g'))[1] AS sku
FROM orders_import_lines
WHERE raw_line ~ 'SKU'
ORDER BY id;

-- ЗАДАНИЕ 5: Нормализовать телефонные номера (regexp_replace)
SELECT
    id,
    source_file,
    line_no,
    regexp_replace(raw_line, '[^0-9]', '', 'g') AS normalized_phone
FROM orders_import_lines
WHERE raw_line ~ '[\+\(\)\-\s0-9]{7,}';

-- ЗАДАНИЕ 6: Нормализовать цены (regexp_replace)
SELECT
    id,
    source_file,
    line_no,
    regexp_replace(
        regexp_replace(raw_line, '[^0-9.,]', '', 'g'),
        '([0-9]+)[, ]([0-9]{3})',
        '\1\2',
        'g'
    ) AS normalized_price
FROM orders_import_lines
WHERE raw_line ~ 'price';

-- ЗАДАНИЕ 7: Разбить tags на массив (regexp_split_to_array)
SELECT
    id,
    source_file,
    line_no,
    array_remove(
        ARRAY(
            SELECT trim(unnest(
                regexp_split_to_array(
                    substring(raw_line FROM 'tags:\s*(.+)$'),
                    ','
                )
            ))
        ),
        ''
    ) AS tags_array
FROM orders_import_lines
WHERE raw_line ~ 'tags:';

-- ЗАДАНИЕ 8: Разбить CSV на отдельные поля (regexp_split_to_table)
SELECT
    id,
    source_file,
    line_no,
    field
FROM orders_import_lines,
LATERAL regexp_split_to_table(
    regexp_replace(raw_line, '^"|"$', '', 'g'),
    '","'
) AS field
WHERE source_file = 'orders_dirty.csv';

-- ЗАДАНИЕ 9: Найти ошибки в логах (регистронезависимо, ~*)
SELECT
    id,
    source_file,
    line_no,
    raw_line
FROM orders_import_lines
WHERE source_file = 'processor_log.txt'
  AND raw_line ~* 'error';

-- ЗАДАНИЕ 10: Заменить error на ERROR (регистронезависимо)
SELECT
    id,
    source_file,
    line_no,
    regexp_replace(raw_line, 'error', 'ERROR', 'gi') AS updated_line
FROM orders_import_lines
WHERE source_file = 'processor_log.txt';