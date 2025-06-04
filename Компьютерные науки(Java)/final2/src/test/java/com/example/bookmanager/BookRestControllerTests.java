package com.example.bookmanager;

import com.example.bookmanager.model.FantasyBook;
import com.example.bookmanager.service.BookService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
class BookRestControllerTests {

    @Autowired
    private BookService bookService;

    @Test
    void testPatchBook() throws Exception {
        FantasyBook book = new FantasyBook("Test Book", "A fantasy adventure");
        book = (FantasyBook) bookService.saveBook(book);
        Long bookId = book.getId();
        book.setTitle("Updated Title");
        book.setDescription("Updated Description");

        bookService.saveBook(book);
        FantasyBook updatedBook = (FantasyBook) bookService.getBookById(bookId);
        assertEquals("Updated Title", updatedBook.getTitle());
        assertEquals("Updated Description", updatedBook.getDescription());
        assertEquals("Fantasy", updatedBook.getCategory());
    }

    @Test
    void testDeleteBook() throws Exception {
        FantasyBook book = new FantasyBook("Test Book", "A fantasy adventure");
        book = (FantasyBook) bookService.saveBook(book);
        Long bookId = book.getId();
        assertNotNull(bookService.getBookById(bookId));
        bookService.deleteBook(bookId);
        assertNull(bookService.getBookById(bookId));
    }
}