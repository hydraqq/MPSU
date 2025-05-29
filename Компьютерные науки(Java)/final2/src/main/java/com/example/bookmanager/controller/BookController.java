package com.example.bookmanager.controller;

import com.example.bookmanager.model.*;
import com.example.bookmanager.service.BookService;
import jakarta.validation.Valid;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

@Controller
@RequestMapping("/books")
public class BookController {
    private final BookService bookService;

    public BookController(BookService bookService) {
        this.bookService = bookService;
    }

    // List all books
    @GetMapping
    public String listBooks(Model model) {
        model.addAttribute("books", bookService.getAllBooks());
        return "book-list";
    }

    // View book details
    @GetMapping("/{id}")
    public String viewBook(@PathVariable Long id, Model model) {
        AbstractBook book = bookService.getBookById(id);
        if (book == null) {
            return "redirect:/books";
        }
        model.addAttribute("book", book);
        return "book-details";
    }

    // Form for adding a new book
    @GetMapping("/new")
    public String newBookForm(Model model) {
        model.addAttribute("bookForm", new BookForm());
        return "book-form";
    }

    // Create a new book
    @PostMapping
    public String createBook(@Valid @ModelAttribute("bookForm") BookForm bookForm, BindingResult result, Model model) {
        if (result.hasErrors()) {
            return "book-form";
        }
        try {
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
                    model.addAttribute("error", "Invalid category selected");
                    return "book-form";
            }
            bookService.saveBook(newBook);
            return "redirect:/books";
        } catch (Exception e) {
            model.addAttribute("error", "Failed to create book: " + e.getMessage());
            return "book-form";
        }
    }

    // Form for editing a book
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

    // Update a book
    @PostMapping("/update/{id}")
    public String updateBook(@PathVariable Long id, @Valid @ModelAttribute("bookForm") BookForm bookForm, BindingResult result, Model model) {
        if (result.hasErrors()) {
            model.addAttribute("bookId", id);
            return "book-form";
        }
        AbstractBook existingBook = bookService.getBookById(id);
        if (existingBook == null) {
            return "redirect:/books";
        }
        try {
            existingBook.setTitle(bookForm.getTitle());
            existingBook.setDescription(bookForm.getDescription());
            bookService.saveBook(existingBook);
            return "redirect:/books";
        } catch (Exception e) {
            model.addAttribute("error", "Failed to update book: " + e.getMessage());
            model.addAttribute("bookId", id);
            return "book-form";
        }
    }

    // Delete a book
    @PostMapping("/delete/{id}")
    public String deleteBook(@PathVariable Long id) {
        bookService.deleteBook(id);
        return "redirect:/books";
    }
}