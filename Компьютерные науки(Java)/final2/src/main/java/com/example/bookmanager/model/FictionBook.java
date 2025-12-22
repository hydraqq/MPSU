package com.example.bookmanager.model;

import jakarta.persistence.Entity;

@Entity
public class FictionBook extends AbstractBook {
    public FictionBook() {}
    public FictionBook(String title, String description) {
        setTitle(title);
        setDescription(description);
    }
    @Override
    public String getCategory() { return "Fiction"; }
}