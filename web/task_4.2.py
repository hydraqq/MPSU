def limit_length(max_len):
    def decorator(func):
        def wrapper(*args, **kwargs):
            result = func(*args, **kwargs)
            if isinstance(result, str) and len(result) > max_len:
                return result[:max_len] + "..."
            return result
        return wrapper
    return decorator


# демонстрация работы декоратора
if __name__ == "__main__":
    @limit_length(10)
    def get_text():
        return "This is a very long string"

    @limit_length(20)
    def get_description():
        return "Краткое описание функционала программы"

    @limit_length(15)
    def get_name():
        return "Иван"

    @limit_length(5)
    def get_message():
        return "Привет, как дела?"

    print("=== Тестирование декоратора limit_length ===\n")

    print("get_text() с лимитом 10 символов:")
    print(f"Результат: '{get_text()}'")
    print()

    print("get_description() с лимитом 20 символов:")
    print(f"Результат: '{get_description()}'")
    print()

    print("get_name() с лимитом 15 символов:")
    print(f"Результат: '{get_name()}'")
    print("(текст короче лимита, не обрезается)")
    print()

    print("get_message() с лимитом 5 символов:")
    print(f"Результат: '{get_message()}'")
    print()

    # дополнительный пример с разными лимитами
    @limit_length(30)
    def long_story():
        return "Это очень длинная история о том, как я изучал программирование"

    @limit_length(50)
    def medium_story():
        return "Это средней длины история о программировании"

    print("=== Дополнительные примеры ===\n")
    print(f"long_story() с лимитом 30: '{long_story()}'")
    print(f"medium_story() с лимитом 50: '{medium_story()}'")
