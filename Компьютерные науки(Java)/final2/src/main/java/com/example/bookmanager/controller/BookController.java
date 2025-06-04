package com.example.bookmanager.controller;

import com.example.bookmanager.model.*;
import com.example.bookmanager.service.BookService;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

@Controller
@RequestMapping("/books")
public class BookController {
    private final BookService bookService;

    public BookController(BookService bookService) {
        this.bookService = bookService;
    }

    @GetMapping
    public String listBooks(Model model) {
        model.addAttribute("books", bookService.getAllBooks());
        return "book-list";
    }

    @GetMapping("/{id}")
    public String viewBook(@PathVariable Long id, Model model) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return "redirect:/books";
        }
        model.addAttribute("book", book);
        return "book-details";
    }

    @GetMapping("/new")
    public String newBookForm(Model model) {
        model.addAttribute("bookForm", new BookForm());
        return "book-form";
    }

    @PostMapping
    public String createBook(@ModelAttribute("bookForm") BookForm bookForm) {
        AbstractBook newBook;
        String category = bookForm.getCategory();
        String title = bookForm.getTitle();
        String description = bookForm.getDescription();

        switch (category) {
            case "Fantasy":
                newBook = new FantasyBook(title, description);
                break;
            case "Fiction":
                newBook = new FictionBook(title, description);
                break;
            case "Horror":
                newBook = new HorrorBook(title, description);
                break;
            default:
                return "book-form";
        }
        bookService.saveBook(newBook);
        return "redirect:/books";
    }

    @GetMapping("/edit/{id}")
    public String editBookForm(@PathVariable Long id, Model model) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return "redirect:/books";
        }
        BookForm bookForm = new BookForm();
        bookForm.setTitle(book.getTitle());
        bookForm.setDescription(book.getDescription());
        bookForm.setCategory(book.getCategory());
        model.addAttribute("bookForm", bookForm);
        model.addAttribute("bookId", id);
        return "book-form";
    }

    @PutMapping("/update/{id}")
    public String updateBook(@PathVariable Long id, @ModelAttribute("bookForm") BookForm bookForm) {
        AbstractBook existingBook = bookService.getBookById(id);
        if (existingBook == null) {
            return "redirect:/books";
        }
        existingBook.setTitle(bookForm.getTitle());
        existingBook.setDescription(bookForm.getDescription());
        bookService.saveBook(existingBook);
        return "redirect:/books";
    }

    @DeleteMapping("/delete/{id}")
    public String deleteBook(@PathVariable Long id) {
        bookService.deleteBook(id);
        return "redirect:/books";
    }
}