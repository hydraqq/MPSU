package com.example.bookmanager.dao;

import com.example.bookmanager.model.AbstractBook;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Repository
@Transactional
public class BookDao {

    @PersistenceContext
    private EntityManager entityManager;

    public List<AbstractBook> findAll() {
        return entityManager.createQuery("SELECT b FROM AbstractBook b", AbstractBook.class)
                .getResultList();
    }

    public AbstractBook findById(Long id) {
        return entityManager.find(AbstractBook.class, id);
    }

    public AbstractBook save(AbstractBook book) {
        if (book.getId() == null) {
            entityManager.persist(book);
            return book;
        } else {
            return entityManager.merge(book);
        }
    }

    public void deleteById(Long id) {
        AbstractBook book = findById(id);
        if (book != null) {
            entityManager.remove(book);
        }
    }
}