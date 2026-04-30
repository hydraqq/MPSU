from django.shortcuts import render


def index(request):
    return render(request, 'blog/article_list.html')
