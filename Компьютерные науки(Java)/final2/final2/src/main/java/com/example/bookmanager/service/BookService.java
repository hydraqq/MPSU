package com.example.bookmanager.service;

import com.example.bookmanager.model.AbstractBook;
import com.example.bookmanager.repository.BookRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class BookService {
    private final BookRepository bookRepository;

    public BookService(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    public List<AbstractBook> getAllBooks() {
        return bookRepository.findAll();
    }

    public AbstractBook getBookById(Long id) {
        return bookRepository.findById(id).orElse(null);
    }

    public AbstractBook saveBook(AbstractBook book) {
        return bookRepository.save(book);
    }

    public void deleteBook(Long id) {
        bookRepository.deleteById(id);
    }
}