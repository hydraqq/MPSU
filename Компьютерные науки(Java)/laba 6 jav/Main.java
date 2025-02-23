public class Main {
    public static void main(String[] args) {
        Magazin.Product[] products = {
            new Magazin.Product(2.5, 175),
            new Magazin.Product(3.2, 225),
            new Magazin.Product(2.8, 550)
        };
        Magazin retail = new Magazin(23, products, 6);
        System.out.println(retail);
        String[] departments = {"Напитки", "Хлебобулочные", "Мясные изделия"};
        Supermarket supermarket = new Supermarket(7, products, 14, 350.0, departments);
        System.out.println(supermarket);
    }
}