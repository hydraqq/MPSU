// Интерфейс Worker
interface Worker {
    default void work() {
        System.out.println("Работник выполняет общую работу.");
    }
    void performDuties();
}

class Person {
    private String name;
    private String surname;
    private String gender;
    private boolean active;

    public Person(String name, String surname, String gender, boolean active) {
        this.name = name;
        this.surname = surname;
        this.gender = gender;
        this.active = active;
    }

    public void sleep() {
        System.out.println(name + " " + surname + " спит.");
    }

    // Геттеры и сеттеры
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public boolean isActive() {
        return active;
    }

    public void setActive(boolean active) {
        this.active = active;
    }
}

// Класс-наследник Employee
class Employee extends Person implements Worker {
    private String position;
    private double payment;
    private int experience;
    private String department;

    public Employee(String name, String surname, String gender, boolean active, String position, double payment, int experience, String department) {
        super(name, surname, gender, active);
        this.position = position;
        this.payment = payment;
        this.experience = experience;
        this.department = department;
    }

    public void work() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") работает в должности " + position 
                + ". Опыт: " + experience + " лет, оплата: " + payment + " руб.");
    }

    public void performDuties() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") выполняет обязанности с опытом "
                + experience + " лет, получает оплату " + payment + " руб.");
    }

    // Геттеры и сеттеры
    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public double getPayment() {
        return payment;
    }

    public void setPayment(double payment) {
        this.payment = payment;
    }

    public int getExperience() {
        return experience;
    }

    public void setExperience(int experience) {
        this.experience = experience;
    }

    public String getDepartment() {
        return department;
    }

    public void setDepartment(String department) {
        this.department = department;
    }
}

// Наследник KitchenWorker
class KitchenWorker extends Employee {
    private String kitchenType;

    public KitchenWorker(String name, String surname, String gender, boolean active, String position, double payment, int experience, String department, String kitchenType) {
        super(name, surname, gender, active, position, payment, experience, department);
        this.kitchenType = kitchenType;
    }

    public void work() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") готовит еду на кухне " + kitchenType 
                + ". Опыт: " + getExperience() + " лет, оплата: " + getPayment() + " руб.");
    }

    public void performDuties() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") работает на кухне типа: " 
                + kitchenType + ". Опыт: " + getExperience() + " лет, получает оплату " + getPayment() + " руб.");
    }

    // Геттеры и сеттеры
    public String getKitchenType() {
        return kitchenType;
    }

    public void setKitchenType(String kitchenType) {
        this.kitchenType = kitchenType;
    }
}

// Наследник OfficeWorker
class OfficeWorker extends Employee {
    private String tools;

    public OfficeWorker(String name, String surname, String gender, boolean active, String position, double payment, int experience, String department, String tools) {
        super(name, surname, gender, active, position, payment, experience, department);
        this.tools = tools;
    }

    public void work() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") работает за компьютером. Опыт: " 
                + getExperience() + " лет, оплата: " + getPayment() + " руб.");
    }

    public void performDuties() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") использует инструменты: " 
                + tools + ". Опыт: " + getExperience() + " лет, получает оплату " + getPayment() + " руб.");
    }

    // Геттеры и сеттеры
    public String getTools() {
        return tools;
    }

    public void setTools(String tools) {
        this.tools = tools;
    }
}

// Наследник GuardWorker
class GuardWorker extends Employee {
    private String shift;

    public GuardWorker(String name, String surname, String gender, boolean active, String position, double payment, int experience, String department, String shift) {
        super(name, surname, gender, active, position, payment, experience, department);
        this.shift = shift;
    }

    public void work() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") следит за безопасностью. Смена: " 
                + shift + ". Опыт: " + getExperience() + " лет, оплата: " + getPayment() + " руб.");
    }

    public void performDuties() {
        System.out.println(getName() + " " + getSurname() + " (" + getGender() + ") работает в смену: " + shift 
                + ". Опыт: " + getExperience() + " лет, получает оплату " + getPayment() + " руб.");
    }

    // Геттеры и сеттеры
    public String getShift() {
        return shift;
    }

    public void setShift(String shift) {
        this.shift = shift;
    }
}

// Тестирование
public class Main {
    public static void main(String[] args) {
        KitchenWorker chef = new KitchenWorker("Алексей", "Смирнов", "мужчина", true, "Кондитер", 65000, 7, "Кондитерский цех", "Пекарня");
        chef.work();
        chef.performDuties();

        OfficeWorker manager = new OfficeWorker("Мария", "Козлова", "женщина", true, "Бухгалтер", 85000, 12, "Финансы", "Excel, 1C, калькулятор");
        manager.work();
        manager.performDuties();

        GuardWorker guard = new GuardWorker("Сергей", "Волков", "мужчина", true, "Охранник", 45000, 5, "Безопасность", "Дневная");
        guard.work();
        guard.performDuties();
    }
}
