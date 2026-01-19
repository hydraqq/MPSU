class Employee:
    def __init__(self, position, salary, experience):
        self.position = position
        self.salary = salary
        self.experience = experience

    def increase_experience(self, years):
        # увеличиваем стаж
        self.experience += years
        # повышаем зарплату пропорционально стажу (10% за каждый год)
        increase_percent = years * 0.1
        self.salary += self.salary * increase_percent

    def __str__(self):
        return f"Должность: {self.position}, Зарплата: {self.salary:.2f} руб., Стаж работы: {self.experience} лет"


# демонстрация работы класса
if __name__ == "__main__":
    print("=== Создание сотрудника ===")
    employee1 = Employee("Программист", 50000, 2)
    print(employee1)
    print()

    print("=== Повышение опыта на 1 год ===")
    employee1.increase_experience(1)
    print(employee1)
    print()

    print("=== Повышение опыта на 2 года ===")
    employee1.increase_experience(2)
    print(employee1)
    print()

    # создадим еще одного сотрудника для примера
    print("=== Создание второго сотрудника ===")
    employee2 = Employee("Менеджер", 60000, 5)
    print(employee2)
    print()

    print("=== Повышение опыта второму сотруднику на 3 года ===")
    employee2.increase_experience(3)
    print(employee2)
