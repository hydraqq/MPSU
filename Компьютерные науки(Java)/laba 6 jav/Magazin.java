public class Magazin {
    private int registerCount;
    private Product[] products;
    private int staffCount;

    public Magazin() {
    }

    public Magazin(int registerCount, Product[] products, int staffCount) {
        this.registerCount = registerCount;
        this.products = products;
        this.staffCount = staffCount;
    }

    public double calculateRegisterEfficiency() {
        if (registerCount == 0) return 0;
        return (double) staffCount / registerCount;
    }

    public double calculateAverageWeight() {
        if (products == null || products.length == 0) return 0;
        double totalWeight = 0;
        for (Product product : products) {
            totalWeight += product.getWeight();
        }
        return totalWeight / products.length;
    }

    public double calculateVenueEfficiency() {
        double averageWeight = calculateAverageWeight();
        double registerEfficiency = calculateRegisterEfficiency();
        return averageWeight * registerEfficiency;
    }

    public String toString() {
        return "Торговая точка{" +
                "Число касс=" + registerCount +
                ", Число персонала=" + staffCount +
                ", Эффективность точки=" + calculateVenueEfficiency() +
                '}';
    }

    public static class Product {
        private double weight;
        private double price;

        public Product() {
        }

        public Product(double weight, double price) {
            this.weight = weight;
            this.price = price;
        }

        public double getWeight() {
            return weight;
        }

        public void setWeight(double weight) {
            this.weight = weight;
        }

        public double getPrice() {
            return price;
        }

        public void setPrice(double price) {
            this.price = price;
        }
    }
}

class Supermarket extends Magazin {
    private double floorSpace;
    private String[] productCategories;

    public Supermarket() {
    }

    public Supermarket(int registerCount, Product[] products, int staffCount, double floorSpace, String[] productCategories) {
        super(registerCount, products, staffCount);
        this.floorSpace = floorSpace;
        this.productCategories = productCategories;
    }

    public double calculateVenueEfficiency() {
        double registerEfficiency = calculateRegisterEfficiency();
        double departmentEfficiency = floorSpace / (productCategories == null ? 1 : productCategories.length);
        return departmentEfficiency * registerEfficiency;
    }

    public String toString() {
        return "Гипермаркет{" +
                "Площадь помещения = " + floorSpace +
                ", Отделы = " + String.join(", ", productCategories) +
                ", Эффективность гипермаркета = " + calculateVenueEfficiency() +
                '}';
    }
}