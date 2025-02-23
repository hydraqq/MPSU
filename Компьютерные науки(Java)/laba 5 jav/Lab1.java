import java.util.Scanner;

public class Lab1 {
    
    public static double computeArea(Point3d p1, Point3d p2, Point3d p3) {
        double a = p1.distanceTo(p2);
        double b = p2.distanceTo(p3);
        double c = p3.distanceTo(p1);
        
        double s = (a + b + c) / 2.0;
        
        return Math.sqrt(s * (s - a) * (s - b) * (s - c));
    }
    
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        System.out.println("Введите координаты первой точки (x y z):");
        Point3d p1 = new Point3d(scanner.nextDouble(), scanner.nextDouble(), scanner.nextDouble());
        
        System.out.println("Введите координаты второй точки (x y z):");
        Point3d p2 = new Point3d(scanner.nextDouble(), scanner.nextDouble(), scanner.nextDouble());
        
        System.out.println("Введите координаты третьей точки (x y z):");
        Point3d p3 = new Point3d(scanner.nextDouble(), scanner.nextDouble(), scanner.nextDouble());
        
        if (p1.equals(p2) || p2.equals(p3) || p3.equals(p1)) {
            System.out.println("Ошибка: некоторые точки совпадают. Невозможно вычислить площадь треугольника.");
        } else {
            double area = computeArea(p1, p2, p3);
            System.out.printf("Площадь треугольника: %.2f\n", area);
        }
        scanner.close();
    }
}