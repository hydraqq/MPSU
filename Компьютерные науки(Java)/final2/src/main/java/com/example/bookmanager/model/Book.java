package com.example.bookmanager.model;

public interface Book {
    Long getId();
    void setId(Long id);
    String getTitle();
    String getCategory();
    String getDescription();
}