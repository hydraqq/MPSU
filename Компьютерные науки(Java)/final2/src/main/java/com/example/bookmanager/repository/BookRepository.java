package com.example.bookmanager.repository;

import com.example.bookmanager.model.AbstractBook;
import org.springframework.data.jpa.repository.JpaRepository;

public interface BookRepository extends JpaRepository<AbstractBook, Long> {
}