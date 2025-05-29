package com.example.bookmanager.model;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

@Entity
@Inheritance(strategy = InheritanceType.JOINED)
public abstract class AbstractBook implements Book {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotNull
    @Size(min = 1, max = 100)
    private String title;

    @NotNull
    @Size(min = 1, max = 500)
    private String description;

    @Override
    public Long getId() { return id; }
    @Override
    public void setId(Long id) { this.id = id; }
    @Override
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    @Override
    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
}