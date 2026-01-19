class Seat:
    def __init__(self, seat_number, service_class):
        self.seat_number = seat_number
        self.service_class = service_class
        self.is_occupied = False

    def __str__(self):
        status = "занято" if self.is_occupied else "свободно"
        return f"Место {self.seat_number} ({self.service_class}) - {status}"


class Passenger:
    def __init__(self, full_name, ticket_number, seat=None):
        self.full_name = full_name
        self.ticket_number = ticket_number
        self.seat = seat

    def assign_seat(self, seat):
        if seat.is_occupied:
            print(f"Место {seat.seat_number} уже занято!")
            return False
        self.seat = seat
        seat.is_occupied = True
        print(f"Пассажир {self.full_name} занял место {seat.seat_number}")
        return True

    def __str__(self):
        seat_info = f"место {self.seat.seat_number}" if self.seat else "место не назначено"
        return f"Пассажир: {self.full_name}, Билет: {self.ticket_number}, {seat_info}"


class Airplane:
    def __init__(self, registration_number, airline, capacity):
        self.registration_number = registration_number
        self.airline = airline
        self.capacity = capacity
        self.seats = []
        self.passengers = []

    def add_seat(self, seat):
        if len(self.seats) < self.capacity:
            self.seats.append(seat)
            return True
        else:
            print(f"Невозможно добавить место - достигнута вместимость {self.capacity}")
            return False

    def add_passenger(self, passenger):
        self.passengers.append(passenger)

    def show_seats(self):
        print(f"\n--- Места в самолете {self.registration_number} ---")
        for seat in self.seats:
            print(seat)

    def show_passengers(self):
        print(f"\n--- Пассажиры самолета {self.registration_number} ---")
        if not self.passengers:
            print("Пассажиров нет")
        else:
            for passenger in self.passengers:
                print(passenger)

    def __str__(self):
        return f"Самолет {self.registration_number}, Авиакомпания: {self.airline}, Вместимость: {self.capacity}"


# демонстрация работы классов
if __name__ == "__main__":
    print("=== Создание самолета ===")
    airplane = Airplane("RA-12345", "Аэрофлот", 6)
    print(airplane)
    print()

    print("=== Добавление мест в самолет ===")
    seat1 = Seat("1A", "Бизнес")
    seat2 = Seat("1B", "Бизнес")
    seat3 = Seat("2A", "Эконом")
    seat4 = Seat("2B", "Эконом")
    seat5 = Seat("3A", "Эконом")
    seat6 = Seat("3B", "Эконом")

    airplane.add_seat(seat1)
    airplane.add_seat(seat2)
    airplane.add_seat(seat3)
    airplane.add_seat(seat4)
    airplane.add_seat(seat5)
    airplane.add_seat(seat6)

    airplane.show_seats()
    print()

    print("=== Регистрация пассажиров ===")
    passenger1 = Passenger("Иванов Иван Иванович", "TK001")
    passenger2 = Passenger("Петрова Анна Сергеевна", "TK002")
    passenger3 = Passenger("Сидоров Петр Алексеевич", "TK003")

    airplane.add_passenger(passenger1)
    airplane.add_passenger(passenger2)
    airplane.add_passenger(passenger3)
    print()

    print("=== Назначение мест пассажирам ===")
    passenger1.assign_seat(seat1)
    passenger2.assign_seat(seat3)
    passenger3.assign_seat(seat3)  # попытка занять уже занятое место
    passenger3.assign_seat(seat5)
    print()

    airplane.show_passengers()
    print()

    airplane.show_seats()
