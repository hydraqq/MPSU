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
    public ResponseEntity<?> getBookById(@PathVariable Long id) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(book);
    }

    @PatchMapping("/{id}")
    public ResponseEntity<?> updateBookPartially(@PathVariable Long id, @RequestBody Map<String, Object> updates) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }
        try {

            if (updates.containsKey("title")) {
                String title = (String) updates.get("title");
                if (title == null || title.trim().isEmpty() || title.length() > 100) {
                    return ResponseEntity.badRequest().body("Title must be non-empty and less than 100 characters");
                }
                book.setTitle(title);
            }
            if (updates.containsKey("description")) {
                String description = (String) updates.get("description");
                if (description == null || description.trim().isEmpty() || description.length() > 500) {
                    return ResponseEntity.badRequest().body("Description must be non-empty and less than 500 characters");
                }
                book.setDescription(description);
            }
            bookService.saveBook(book);
            return ResponseEntity.ok(book);
        } catch (Exception e) {
            return ResponseEntity.badRequest().body("Failed to update book: " + e.getMessage());
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> deleteBook(@PathVariable Long id) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return ResponseEntity.notFound().build();
        }
        try {
            bookService.deleteBook(id);
            return ResponseEntity.noContent().build();
        } catch (Exception e) {
            return ResponseEntity.badRequest().body("Failed to delete book: " + e.getMessage());
        }
    }
}