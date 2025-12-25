from django.shortcuts import render, redirect, get_object_or_404
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.models import User
from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.http import HttpResponse, HttpResponseForbidden
from django.views.decorators.http import require_POST
from .forms import LoginForm
from .models import Profile, Item

def _get_profile(user):
    return Profile.objects.get_or_create(user=user)[0]

def index(request):
    my_lists = [("/secure/items/", "Мои объекты")]
    actions = [
        ("/vuln/admin_panel/", "Админ-панель"),
    ]
    return render(request, "index.html", {"my_lists": my_lists, "actions": actions, "domain_desc": "Склад: операции с персоналом и назначением ролей"})

def login_view(request):
    if request.method == "POST":
        form = LoginForm(request.POST)
        if form.is_valid():
            user = authenticate(request, username=form.cleaned_data["username"], password=form.cleaned_data["password"])
            if user:
                login(request, user); messages.success(request, "OK"); return redirect("index")
            messages.error(request, "Неверные данные")
    else:
        form = LoginForm()
    return render(request, "login.html", {"form": form})

def logout_view(request):
    logout(request); messages.info(request, "Вышли")
    return redirect("index")

@login_required
def items_list(request):
    items = Item.objects.filter(owner=request.user).order_by("-id")
    return render(request, "items_list.html", {"items": items})

@login_required
def delete_user_vuln(request, user_id):
    prof = _get_profile(request.user)
    if prof.role != "admin":
        return HttpResponseForbidden("Admin only")
    if request.user.id == user_id:
        return HttpResponseForbidden("Cannot delete yourself")
    user = get_object_or_404(User, id=user_id)
    target_prof = _get_profile(user)
    if target_prof.role == "admin":
        return HttpResponseForbidden("Cannot delete other admins")
    user.delete()
    messages.success(request, f"Пользователь {user.username} удалён")
    return redirect("index")

@login_required
def admin_panel_vuln(request):
    if _get_profile(request.user).role != "admin":
        return HttpResponseForbidden("Admin only")
    
    users = User.objects.exclude(id=request.user.id)
    users_with_roles = []
    for user in users:
        prof = _get_profile(user)
        users_with_roles.append({
            'id': user.id,
            'username': user.username,
            'role': prof.role
        })
    
    return render(request, "admin_panel.html", {"users": users_with_roles, "mode": "secure"})

@login_required
@require_POST
def promote_user_vuln(request, user_id):
    if _get_profile(request.user).role != "admin":
        return HttpResponseForbidden("Admin only")
    target = get_object_or_404(User, id=user_id)
    if request.user.id == user_id:
        return HttpResponseForbidden("Cannot change your own role")
    role = request.POST.get("role")
    if not role or role not in dict(Profile.ROLE_CHOICES):
        return HttpResponseForbidden("Invalid role")
    prof, _ = Profile.objects.get_or_create(user=target)
    prof.role = role
    prof.save()
    messages.success(request, f"Роль пользователя {target.username} изменена на {role}")
    return redirect("index")
