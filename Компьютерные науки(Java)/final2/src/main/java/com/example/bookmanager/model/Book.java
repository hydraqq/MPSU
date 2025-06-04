package com.example.bookmanager.model;

public interface Book {
    Long getId();
    void setId(Long id);
    String getTitle();
    void setTitle(String title);
    String getCategory();
    void setCategory(String category);
    String getDescription();
    void setDescription(String description);
}