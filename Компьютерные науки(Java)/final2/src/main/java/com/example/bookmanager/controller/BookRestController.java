package com.example.bookmanager.controller;

import com.example.bookmanager.model.AbstractBook;
import com.example.bookmanager.service.BookService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@RestController
@RequestMapping("/api/books")
public class BookRestController {
    private final BookService bookService;

    public BookRestController(BookService bookService) {
        this.bookService = bookService;
    }

    @GetMapping("/{id}")
    public ResponseEntity<AbstractBook> getBookById(@PathVariable Long id) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(book);
    }

    @PutMapping("/{id}")
    public ResponseEntity<AbstractBook> setBookById(@PathVariable Long id, @RequestBody AbstractBook book) {
        bookService.setBookById(id, book);
        return ResponseEntity.ok(book);
    }

    @PatchMapping("/{id}")
    public ResponseEntity<AbstractBook> updateBookPartially(@PathVariable Long id, @RequestBody Map<String, Object> updates) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }

        if (updates.containsKey("title")) {
            book.setTitle((String) updates.get("title"));
        }
        if (updates.containsKey("description")) {
            book.setDescription((String) updates.get("description"));
        }

        bookService.saveBook(book);
        return ResponseEntity.ok(book);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteBook(@PathVariable Long id) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }
        bookService.deleteBook(id);
        return ResponseEntity.noContent().build();
    }
}