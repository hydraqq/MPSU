let sound; // Переменная где будет находится аудио-дорожка
let isInitialised; // Состояние, которое обозначает инициализированы ли значения или нет
let isLoaded = false;
let amplitude;
let amplitudes = [];

let fft;

function preload() {
    soundFormats('mp3', 'wav'); // Определяем аудио форматы, поддерживаемые плеером
    sound = loadSound('assets/yee-king_track.mp3', () => {
        console.log("sound is loaded!"); // Загружаем музыку и при успешной загрузке выводим в консоль сообщение, что музыка загрузилась
        isLoaded = true;
    });
    isInitialised = false; 
    sound.setVolume(0.2); // Устанавливаем громкость на 20%
}

function setup() {
    createCanvas(1024, 1024);
    textAlign(CENTER); // Центрируем следующий текст по центру
    textSize(32);
    
    amplitude = new p5.Amplitude();
    
    fft = new p5.FFT();
}

function draw() {
    background(30, 30, 30); // Темный фон
    fill(255);
    
    if (isInitialised && !sound.isPlaying()) {
        text("Press any key for play sound", width / 2, height / 2);
    } else if (sound.isPlaying()) {
        let level = amplitude.getLevel();
        text(level, width / 2, 40);

        // Анализируем частоты
        let freqs = fft.analyze();
        
        // Отображаем круговые волны для частот
        translate(width / 2, height / 2);
        noFill();

        for (let i = 2; i < freqs.length - 2; i += 5) {
            // Рассчитываем среднее значение для 5 ближайших HZ
            let avg = (freqs[i - 2] + freqs[i - 1] + freqs[i] + freqs[i + 1] + freqs[i + 2]) / 5;

            let radius = map(avg, 0, 255, 50, 400); // Преобразуем значение в радиус круга

            strokeWeight(2);
            stroke(map(i, 0, freqs.length, 0, 255), 100, 200); // Цвет круга
            ellipse(0, 0, radius, radius); // Рисуем круг
        }
    }
}

function keyPressed() {
    if (!isInitialised) {
        isInitialised = true;
        
        if (isLoaded)
            sound.loop(); // Запускаем звук в цикле с нормальной скоростью
    } else {
        if (key == ' ') {
            if (sound.isPaused()) sound.play();
            else sound.pause();
        }
    }
}