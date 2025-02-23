import java.util.ArrayList;
import java.util.Random;

class Product {
    String name;
    double price;
    double weight;

    public Product(String name, double price, double weight) {
        this.name = name;
        this.price = price;
        this.weight = weight;
    }

    @Override
    public String toString() {
        return String.format("Продукт{Название = '%s', Цена = %.2f, Вес = %.2f kg}", name, price, weight);
    }
}

public class Manager {
    public static void main(String[] args) {
        ArrayList<Product> products = generateRandomProducts(15);

        System.out.println("Изначальные продукты:");
        products.forEach(System.out::println);

        products.removeIf(product -> product.weight > 5 || product.price > 10000);

        Product favoriteProduct = new Product("Торт опера", 300, 1);
        products.add(0, favoriteProduct);

        //продуктоы с ценой < 10 и весом > 2
        System.out.println("\nПродукты с ценой < 10 and weight > 2:");
        products.stream()
                .filter(product -> product.price < 10 && product.weight > 2)
                .forEach(System.out::println);

        System.out.println("\nФинальный список продуктов:");
        products.forEach(System.out::println);
    }

    //Генерация случайных продуктов
    private static ArrayList<Product> generateRandomProducts(int count) {
        String[] names = {
            "Овсянка", "Гречка", "Куриные крылья", "Свинина", 
            "Минеральная вода", "Творог", "Шоколадная паста", 
            "Картофель фри", "Йогурт", "Огурцы", "Помидоры", 
            "Зеленый чай", "Кофе", "Мандарины", "Лимоны"
        };

        Random random = new Random();
        ArrayList<Product> products = new ArrayList<>();

        for (int i = 0; i < count; i++) {
            String name = names[random.nextInt(names.length)];
            double price = Math.round((1 + random.nextDouble() * 20000) * 100.0) / 100.0;
            double weight = Math.round((0.1 + random.nextDouble() * 10) * 100.0) / 100.0;

            Product newProduct = new Product(name, price, weight);

            // Проверка на дублирование
            if (!products.contains(newProduct)) {
                products.add(newProduct);
            }
        }

        return products;
    }
}
