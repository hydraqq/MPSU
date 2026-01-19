DROP TABLE IF EXISTS film_credit CASCADE;
DROP TABLE IF EXISTS film_info CASCADE;
DROP TABLE IF EXISTS film CASCADE;
DROP TABLE IF EXISTS director CASCADE;

-- DIRECTOR TABLE
CREATE TABLE director (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    country TEXT
);

-- FILM TABLE (1:N с DIRECTOR)
CREATE TABLE film (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    release_year INTEGER NOT NULL CHECK (release_year BETWEEN 1900 AND EXTRACT(YEAR FROM CURRENT_DATE)),
    primary_director_id INTEGER NOT NULL,
    FOREIGN KEY (primary_director_id) REFERENCES director(id) ON DELETE RESTRICT
);

-- FILM_INFO TABLE (1:1 с FILM)
CREATE TABLE film_info (
    film_id INTEGER PRIMARY KEY,
    duration_minutes INTEGER NOT NULL CHECK (duration_minutes > 0),
    rating TEXT NOT NULL CHECK (rating IN ('G', 'PG', 'PG-13', 'R', 'NC-17')),
    budget_usd NUMERIC(15, 2),
    FOREIGN KEY (film_id) REFERENCES film(id) ON DELETE CASCADE
);

-- FILM_CREDIT TABLE (M:N между FILM и DIRECTOR)
CREATE TABLE film_credit (
    film_id INTEGER NOT NULL,
    director_id INTEGER NOT NULL,
    role TEXT NOT NULL,
    PRIMARY KEY (film_id, director_id, role),
    FOREIGN KEY (film_id) REFERENCES film(id) ON DELETE CASCADE,
    FOREIGN KEY (director_id) REFERENCES director(id) ON DELETE RESTRICT
);

-- ДАННЫЕ
INSERT INTO director (name, country) VALUES
('Steven Spielberg', 'USA'),
('Christopher Nolan', 'UK'),
('Denis Villeneuve', 'Canada'),
('Martin Scorsese', 'USA'),
('Quentin Tarantino', 'USA'),
('David Fincher', 'USA'),
('Ridley Scott', 'UK'),
('James Cameron', 'Canada'),
('Peter Jackson', 'New Zealand'),
('Ang Lee', 'Taiwan');

INSERT INTO film (title, release_year, primary_director_id) VALUES
('Jaws', 1975, 1),
('E.T.', 1982, 1),
('Inception', 2010, 2),
('Interstellar', 2014, 2),
('Dune', 2021, 3),
('Blade Runner 2049', 2017, 3),
('Taxi Driver', 1976, 4),
('Goodfellas', 1990, 4),
('Pulp Fiction', 1994, 5),
('Kill Bill Vol. 1', 2003, 5),
('Se7en', 1995, 6),
('The Social Network', 2010, 6),
('Alien', 1979, 7),
('Gladiator', 2000, 7),
('Avatar', 2009, 8),
('Titanic', 1997, 8),
('The Lord of the Rings: The Fellowship of the Ring', 2001, 9),
('The Hobbit: An Unexpected Journey', 2012, 9),
('Brokeback Mountain', 2005, 10),
('Life of Pi', 2012, 10);

INSERT INTO film_info (film_id, duration_minutes, rating, budget_usd) VALUES
(1, 124, 'PG', 20000000),
(2, 115, 'PG', 10500000),
(3, 148, 'PG-13', 160000000),
(4, 169, 'PG-13', 165000000),
(5, 166, 'PG-13', 165000000),
(6, 164, 'R', 150000000),
(7, 114, 'R', 1900000),
(8, 146, 'R', 25000000),
(9, 154, 'R', 8000000),
(10, 111, 'R', 30000000),
(11, 127, 'R', 33000000),
(12, 120, 'PG-13', 50000000),
(13, 117, 'R', 11000000),
(14, 155, 'R', 103000000),
(15, 162, 'PG-13', 237000000),
(16, 194, 'PG-13', 200000000),
(17, 178, 'PG-13', 93000000),
(18, 169, 'PG-13', 180000000),
(19, 134, 'R', 14000000),
(20, 127, 'PG', 70000000);

INSERT INTO film_credit (film_id, director_id, role) VALUES
(1, 1, 'director'),
(2, 1, 'director'),
(3, 2, 'director'),
(3, 1, 'co-director'),
(4, 2, 'director'),
(5, 3, 'director'),
(5, 2, 'co-director'),
(6, 3, 'director'),
(7, 4, 'director'),
(8, 4, 'director'),
(8, 5, 'producer'),
(9, 5, 'director'),
(10, 5, 'director'),
(11, 6, 'director'),
(12, 6, 'director'),
(13, 7, 'director'),
(14, 7, 'director'),
(15, 8, 'director'),
(16, 8, 'director'),
(17, 9, 'director'),
(18, 9, 'director'),
(19, 10, 'director'),
(20, 10, 'director');
