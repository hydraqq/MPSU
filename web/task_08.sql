DROP TABLE IF EXISTS books CASCADE;

CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    genre TEXT NOT NULL,
    price NUMERIC(10, 2) NOT NULL CHECK (price > 0),
    published_date DATE NOT NULL CHECK (published_date <= CURRENT_DATE)
);

-- ДАННЫЕ
INSERT INTO books (title, author, genre, price, published_date) VALUES
('Dragon of the North', 'George R.R. Martin', 'Fantasy', 19.99, '2015-05-20'),
('Dragon Rider', 'Cornelia Funke', 'Fantasy', 14.99, '2010-01-01'),
('Dragon Quest', 'Yuji Horii', 'Fantasy', 24.99, '2020-12-31'),
('Dragon Age: The Stolen Throne', 'David Gaider', 'Fantasy', 17.99, '2009-12-31'),
('Dragons of Autumn Twilight', 'Margaret Weis', 'Fantasy', 18.50, '2012-06-15'),
('The Hobbit', 'J.R.R. Tolkien', 'fantasy', 15.99, '2012-09-21'),
('A Game of Thrones', 'George R.R. Martin', 'FANTASY', 16.99, '2011-03-15'),
('The Name of the Wind', 'Patrick Rothfuss', 'fantasy', 18.99, '2007-08-27'),
('Dragon Ball Z', 'Akira Toriyama', 'Manga', 12.99, '2015-04-10'),
('Dragon Fire', 'Rob Hayes', 'Science Fiction', 20.99, '2018-11-05'),
('The Martian', 'Andy Weir', 'Science Fiction', 14.99, '2015-02-11'),
('The Girl with the Dragon Tattoo', 'Stieg Larsson', 'Mystery', 18.99, '2008-08-15'),
('Foundation', 'Isaac Asimov', 'Science Fiction', 12.99, '2008-01-15'),
('Dune', 'Frank Herbert', 'Science Fiction', 16.99, '2006-06-01'),
('Neuromancer', 'William Gibson', 'Science Fiction', 11.99, '2000-07-01'),
('The Left Hand of Darkness', 'Ursula K. Le Guin', 'Science Fiction', 13.50, '2003-04-10'),
('Ender''s Game', 'Orson Scott Card', 'Science Fiction', 9.99, '2005-09-15'),
('Hyperion Box Set', 'Dan Simmons', 'Science Fiction', 15.99, '2010-03-20'),
('Altered Carbon', 'Richard K. Morgan', 'Science Fiction', 20.00, '2002-05-18'),
('The Three-Body Problem', 'Liu Cixin', 'Science Fiction', 9.98, '2008-01-01'),
('Blindsight', 'Peter Watts', 'Science Fiction', 14.50, '2006-10-03'),
('Reference Sample Database', 'Various Authors', 'Reference', 25.99, '1995-03-10'),
('Sample Dictionary', 'John Smith', 'Reference', 19.99, '1998-06-15'),
('Historical Sample', 'Mary Johnson', 'Reference', 21.99, '1999-12-31'),
('Database Reference Guide', 'David Lee', 'Reference', 22.99, '2000-01-01'),
('Sample Fiction Book', 'Susan Brown', 'Fiction', 18.99, '1998-05-20'),
('Learning Sample Code', 'Robert Wilson', 'Reference', 20.99, '2005-07-10'),
('Pride and Prejudice', 'Jane Austen', 'Romance', 10.99, '1813-01-28'),
('The Great Gatsby', 'F. Scott Fitzgerald', 'Romance', 11.99, '1925-04-10'),
('Murder on the Orient Express', 'Agatha Christie', 'Mystery', 12.99, '1934-01-01'),
('Sherlock Holmes Complete', 'Arthur Conan Doyle', 'Mystery', 29.99, '2012-05-15'),
('The Alchemist', 'Paulo Coelho', 'Fiction', 13.99, '1988-05-15');

-- SELECT
SELECT
    id,
    title,
    author,
    genre,
    price,
    published_date
FROM books
WHERE LOWER(genre) LIKE '%fantasy%'
  AND LOWER(title) LIKE 'dragon%'
  AND published_date BETWEEN '2010-01-01' AND '2020-12-31'
ORDER BY title;

-- UPDATE
UPDATE books
SET price = ROUND(price * 1.15, 2)
WHERE LOWER(genre) = 'science fiction'
  AND price BETWEEN 9.99 AND 19.99
  AND LOWER(title) NOT LIKE '%box set%';

-- DELETE
DELETE FROM books
WHERE LOWER(genre) = 'reference'
  AND published_date < '2000-01-01'
  AND LOWER(title) LIKE '%sample%';
