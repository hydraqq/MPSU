package com.example.bookmanager.model;

import jakarta.persistence.Entity;

@Entity
public class FantasyBook extends AbstractBook {
    public FantasyBook() {}
    public FantasyBook(String title, String description) {
        setTitle(title);
        setDescription(description);
    }
    @Override
    public String getCategory() { return "Fantasy"; }
}