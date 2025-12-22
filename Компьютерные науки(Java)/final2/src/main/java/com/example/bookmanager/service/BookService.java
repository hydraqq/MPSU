package com.example.bookmanager.service;

import com.example.bookmanager.model.AbstractBook;
import com.example.bookmanager.dao.BookDao;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class BookService {
    private final BookDao bookDao;

    public BookService(BookDao bookDao) {
        this.bookDao = bookDao;
    }

    public List<AbstractBook> getAllBooks() {
        return bookDao.findAll();
    }

    public AbstractBook getBookById(Long id) {
        return bookDao.findById(id);
    }

    public void setBookById(Long id, AbstractBook book) {
        book.setId(id);
        bookDao.save(book);
    }

    public AbstractBook saveBook(AbstractBook book) {
        return bookDao.save(book);
    }

    public void deleteBook(Long id) {
        bookDao.deleteById(id);
    }
}