package com.example.bookmanager;

import com.example.bookmanager.model.FantasyBook;
import com.example.bookmanager.service.BookService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;

import java.util.HashMap;
import java.util.Map;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@SpringBootTest
@AutoConfigureMockMvc
class BookRestControllerTests {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private BookService bookService;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    void testPatchBook() throws Exception {
        FantasyBook book = new FantasyBook("Test Book", "A fantasy adventure");
        book = (FantasyBook) bookService.saveBook(book);

        Map<String, Object> updates = new HashMap<>();
        updates.put("title", "Updated Title");
        updates.put("description", "Updated Description");

        mockMvc.perform(patch("/api/books/" + book.getId())
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(updates)))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.title").value("Updated Title"))
                .andExpect(jsonPath("$.description").value("Updated Description"));
    }

    @Test
    void testDeleteBook() throws Exception {
        FantasyBook book = new FantasyBook("Test Book", "A fantasy adventure");
        book = (FantasyBook) bookService.saveBook(book);

        mockMvc.perform(delete("/api/books/" + book.getId()))
                .andExpect(status().isNoContent());

        mockMvc.perform(get("/api/books/" + book.getId()))
                .andExpect(status().isNotFound());
    }
}