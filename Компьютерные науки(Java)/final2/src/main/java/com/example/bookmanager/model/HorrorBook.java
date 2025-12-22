package com.example.bookmanager.model;

import jakarta.persistence.Entity;

@Entity
public class HorrorBook extends AbstractBook {
    public HorrorBook() {}
    public HorrorBook(String title, String description) {
        setTitle(title);
        setDescription(description);
    }
    @Override
    public String getCategory() { return "Horror"; }
}