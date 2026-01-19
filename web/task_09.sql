DROP TABLE IF EXISTS account CASCADE;

CREATE TABLE account (
    id              SERIAL PRIMARY KEY,
    full_name       TEXT,
    phone           TEXT,
    balance_cents   INTEGER,
    status          TEXT,
    registered_at   TIMESTAMP DEFAULT NOW(),
    note            TEXT
);

-- ДАННЫЕ
INSERT INTO account (full_name, phone, balance_cents, status, registered_at, note) VALUES
('Alice Johnson',    ' +46 70-123-45-67 ', 125000, 'active',  NOW() - INTERVAL '400 days', NULL),
('Bob Smith',        '+46-73-555-00-11',   8999,  'pending',  NOW() - INTERVAL '320 days', 'promo'),
('Charlie Brown',    '070 222 33 44',     159900, 'active',   NOW() - INTERVAL '280 days', NULL),
('Diana Prince',     '073-111-22-33',     4599,   'blocked',  NOW() - INTERVAL '250 days', NULL),
('Evan Lee',         '070-999-88-77',     219900, 'active',   NOW() - INTERVAL '200 days', 'vip'),
('Fiona Adams',      '0735550012',        9900,   NULL,       NOW() - INTERVAL '190 days', NULL),
('George Miller',    '070-700-70-70',     45999,  'active',   NOW() - INTERVAL '175 days', NULL),
('Hannah Davis',     '073 333 44 55',     2999,   'pending',  NOW() - INTERVAL '160 days', NULL),
('Ian Wright',       '+46 70 101 20 30',  119999, 'active',   NOW() - INTERVAL '150 days', NULL),
('Julia Stone',      '070-000-00-01',     25999,  'blocked',  NOW() - INTERVAL '140 days', NULL),
('Kevin Park',       '073-222-22-22',     34999,  'active',   NOW() - INTERVAL '130 days', NULL),
('Laura Chen',       '070-010-0100',      49999,  'active',   NOW() - INTERVAL '120 days', 'gift'),
('Mark Green',       '073-777-77-77',     1299,   'pending',  NOW() - INTERVAL '110 days', NULL),
('Nina Patel',       '070-234-56-78',     1899,   'active',   NOW() - INTERVAL '100 days', NULL),
('Oscar Diaz',       '+46-73-700-80-90',  45999,  'active',   NOW() - INTERVAL '95 days',  NULL),
('Paula Gomez',      '070 888 99 00',     219999, 'blocked',  NOW() - INTERVAL '80 days',  NULL),
('Quinn Baker',      '073-000-12-34',     89999,  'active',   NOW() - INTERVAL '70 days',  NULL),
('Rita Ora',         '070-333-66-99',     16999,  'pending',  NOW() - INTERVAL '60 days',  NULL),
('Sam Young',        '070-444-55-66',     13999,  'active',   NOW() - INTERVAL '45 days',  NULL),
('Tina King',        '073-111-00-00',     299999, 'active',   NOW() - INTERVAL '30 days',  NULL),
('Uma Reed',         '070-222-00-00',     9900,   NULL,       NOW() - INTERVAL '20 days',  NULL),
('Victor Hugo',      '073-123-45-67',     4999,   'active',   NOW() - INTERVAL '15 days',  NULL),
('Wendy Frost',      '070-765-43-21',     12345,  'blocked',  NOW() - INTERVAL '10 days',  NULL),
('Yara Novak',       '+46 73 987 65 43',  77777,  'active',   NOW() - INTERVAL '5 days',   NULL),
('Zack Cole',        '0700000002',        2500,   'pending',  NOW() - INTERVAL '2 days',   NULL);

ALTER TABLE account RENAME TO customers;

ALTER TABLE customers
ALTER COLUMN balance_cents TYPE NUMERIC(12, 2)
USING balance_cents / 100.0;

ALTER TABLE customers RENAME COLUMN balance_cents TO balance;

ALTER TABLE customers ADD COLUMN account_no TEXT;

UPDATE customers
SET account_no = 'ACC-' || TO_CHAR(registered_at, 'YYYY') || '-' || LPAD(id::TEXT, 5, '0');

UPDATE customers
SET phone = REGEXP_REPLACE(phone, '[[:space:]-]', '', 'g');

ALTER TABLE customers ALTER COLUMN phone TYPE VARCHAR(20);

ALTER TABLE customers ALTER COLUMN phone SET NOT NULL;

ALTER TABLE customers ADD CONSTRAINT phone_unique UNIQUE (phone);

UPDATE customers SET status = 'active' WHERE status IS NULL;

ALTER TABLE customers
ADD CONSTRAINT status_check CHECK (status IN ('active', 'blocked', 'pending'));

ALTER TABLE customers ALTER COLUMN status SET DEFAULT 'active';

ALTER TABLE customers ALTER COLUMN status SET NOT NULL;

ALTER TABLE customers DROP COLUMN note;
