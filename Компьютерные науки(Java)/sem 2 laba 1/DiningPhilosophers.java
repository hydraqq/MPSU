import java.util.concurrent.*;

class Philosopher implements Runnable {
    private final int id;
    private final Semaphore leftFork;
    private final Semaphore rightFork;

    public Philosopher(int id, Semaphore leftFork, Semaphore rightFork) {
        this.id = id;
        this.leftFork = leftFork;
        this.rightFork = rightFork;
    }

    @Override
    public void run() {
        try {
            while (true) {
                think();
                eat();
            }
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }

    private void think() throws InterruptedException {
        System.out.println("Philosopher " + id + " is thinking.");
        Thread.sleep((long) (Math.random() * 1000));
    }

    private void eat() throws InterruptedException {
        leftFork.acquire();
        rightFork.acquire();
        
        System.out.println("Philosopher " + id + " is eating.");
        Thread.sleep((long) (Math.random() * 1000));
        
        rightFork.release();
        leftFork.release();
        System.out.println("Philosopher " + id + " has finished eating.");
    }
}

public class DiningPhilosophers {
    public static void main(String[] args) {
        final int PHILOSOPHERS_COUNT = 5;
        Semaphore[] forks = new Semaphore[PHILOSOPHERS_COUNT];
        
        for (int i = 0; i < PHILOSOPHERS_COUNT; i++) {
            forks[i] = new Semaphore(1);
        }
        
        Thread[] philosophers = new Thread[PHILOSOPHERS_COUNT];
        
        for (int i = 0; i < PHILOSOPHERS_COUNT; i++) {
            Semaphore leftFork = forks[i];
            Semaphore rightFork = forks[(i + 1) % PHILOSOPHERS_COUNT];
            philosophers[i] = new Thread(new Philosopher(i, leftFork, rightFork));
            philosophers[i].start();
        }
    }
}
