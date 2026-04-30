from django.contrib import admin
from .models import Article, Tag, Comment


@admin.register(Article)
class ArticleAdmin(admin.ModelAdmin):
    list_display = ['title', 'author', 'created_at']
    list_filter = ['tags', 'created_at']
    search_fields = ['title', 'content']


@admin.register(Tag)
class TagAdmin(admin.ModelAdmin):
    list_display = ['name']


@admin.register(Comment)
class CommentAdmin(admin.ModelAdmin):
    list_display = ['author', 'article', 'created_at']
    list_filter = ['created_at']
