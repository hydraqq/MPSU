-- Таблица students с ограничениями CHECK

DROP TABLE IF EXISTS students CASCADE;

CREATE TABLE students (
    id SERIAL PRIMARY KEY,
    full_name TEXT NOT NULL,
    age INTEGER NOT NULL CHECK (age >= 16 AND age <= 100),
    gpa NUMERIC(3, 2) NOT NULL CHECK (gpa >= 0 AND gpa <= 10)
);

INSERT INTO students (full_name, age, gpa) VALUES
('Иванов Иван Иванович', 19, 8.50),
('Петрова Мария Сергеевна', 21, 9.20),
('Сидоров Алексей Петрович', 18, 7.80),
('Козлова Анна Дмитриевна', 20, 9.75),
('Смирнов Дмитрий Александрович', 22, 8.00);
