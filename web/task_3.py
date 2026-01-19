from abc import ABC, abstractmethod


class Instrument(ABC):
    def __init__(self, instrument_name, material):
        self.instrument_name = instrument_name
        self.material = material

    @abstractmethod
    def play_music(self):
        pass

    def __str__(self):
        return f"{self.instrument_name} из материала: {self.material}"


class Piano(Instrument):
    def __init__(self, instrument_name, material, key_count):
        super().__init__(instrument_name, material)
        self.key_count = key_count

    def play_music(self):
        return f"Играет {self.instrument_name}: звучат мелодии на {self.key_count} клавишах."

    def __str__(self):
        return f"{super().__str__()}, количество клавиш: {self.key_count}"


class Guitar(Instrument):
    def __init__(self, instrument_name, material, string_count):
        super().__init__(instrument_name, material)
        self.string_count = string_count

    def play_music(self):
        return f"Играет {self.instrument_name}: звенит {self.string_count}-струнная гитара."

    def __str__(self):
        return f"{super().__str__()}, количество струн: {self.string_count}"


# демонстрация работы классов
if __name__ == "__main__":
    print("=== Создание музыкальных инструментов ===\n")

    piano1 = Piano("Piano", "дерево и металл", 88)
    print(piano1)
    print(piano1.play_music())
    print()

    piano2 = Piano("Piano", "пластик", 61)
    print(piano2)
    print(piano2.play_music())
    print()

    guitar1 = Guitar("Guitar", "дерево", 6)
    print(guitar1)
    print(guitar1.play_music())
    print()

    guitar2 = Guitar("Guitar", "металл", 7)
    print(guitar2)
    print(guitar2.play_music())
    print()

    # создадим список инструментов и пройдемся по ним
    print("=== Все инструменты играют вместе ===\n")
    instruments = [piano1, piano2, guitar1, guitar2]

    for instrument in instruments:
        print(instrument.play_music())
