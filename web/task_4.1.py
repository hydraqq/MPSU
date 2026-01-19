DISABLED = False


def disabled_if_flag(func):
    def wrapper(*args, **kwargs):
        if DISABLED is True:
            raise RuntimeError("Feature disabled")
        return func(*args, **kwargs)
    return wrapper


# демонстрация работы декоратора
if __name__ == "__main__":
    @disabled_if_flag
    def work():
        return "done"

    @disabled_if_flag
    def calculate(a, b):
        return a + b

    @disabled_if_flag
    def greet(name):
        return f"Привет, {name}!"

    print("=== Флаг DISABLED = False ===")
    print(f"DISABLED: {DISABLED}")
    print(f"work(): {work()}")
    print(f"calculate(5, 3): {calculate(5, 3)}")
    print(f"greet('Иван'): {greet('Иван')}")
    print()

    # меняем флаг на True
    DISABLED = True
    print("=== Флаг DISABLED = True ===")
    print(f"DISABLED: {DISABLED}")

    try:
        print(f"work(): {work()}")
    except RuntimeError as e:
        print(f"work() вызвало ошибку: {e}")

    try:
        print(f"calculate(5, 3): {calculate(5, 3)}")
    except RuntimeError as e:
        print(f"calculate(5, 3) вызвало ошибку: {e}")

    try:
        print(f"greet('Иван'): {greet('Иван')}")
    except RuntimeError as e:
        print(f"greet('Иван') вызвало ошибку: {e}")
