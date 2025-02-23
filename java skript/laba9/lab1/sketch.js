let sound;
let isInitialised;
let isLoaded = false;
let amplitude;
let amplitudes = [];
let fft;
let rateSlider, volumeSlider;

function preload() {
    soundFormats('mp3', 'wav');
    sound = loadSound('assets/segway_loop.mp3', function () {
        console.log("sound is loaded!");
        isLoaded = true;
    });
    isInitialised = false;
    sound.setVolume(0.2);
}

function setup() {
    // Устанавливаем размер с соотношением 16:9
    let canvasWidth = windowWidth;
    let canvasHeight = windowWidth * 9 / 16;
    
    if (canvasHeight > windowHeight) {
        canvasHeight = windowHeight;
        canvasWidth = windowHeight * 16 / 9;
    }
    
    createCanvas(canvasWidth, canvasHeight);
    textAlign(CENTER);
    textSize(32);
    
    amplitude = new p5.Amplitude();
    
    for (let i = 0; i < 100; i++) {
        amplitudes.push(0);
    }
    
    fft = new p5.FFT();

    // Ползунок для изменения скорости
    rateSlider = createSlider(0.5, 2.0, 1, 0.01);
    rateSlider.style('width', '200px');
    rateSlider.position(20, height - 60);

    // Ползунок для изменения громкости
    volumeSlider = createSlider(0, 1, 0.2, 0.01);
    volumeSlider.style('width', '200px');
    volumeSlider.position(20, height - 30);
}

function draw() {
    background(30, 30, 30);
    fill(255);
    
    if (isInitialised && !sound.isPlaying()) {
        text("Press any key to play sound", width / 2, height / 2);
    } else if (sound.isPlaying()) {
        // Рисуем круги FFT
        let freqs = fft.analyze();
        
        push();
        translate(width / 2, height / 2);
        noFill();
        for (let i = 2; i < freqs.length - 2; i += 5) {
            let avg = (freqs[i - 2] + freqs[i - 1] + freqs[i] + freqs[i + 1] + freqs[i + 2]) / 5;
            let radius = map(avg, 0, 255, 50, min(width, height) / 2);
            strokeWeight(2);
            stroke(map(i, 0, freqs.length, 0, 255), 100, 200);
            ellipse(0, 0, radius, radius);
        }
        pop();
        
        drawWave();
    }

    // Обновляем скорость и громкость
    sound.rate(rateSlider.value());
    sound.setVolume(volumeSlider.value());

    // Подписи к ползункам
    noStroke();
    fill(255);
    textSize(16);
    text("Speed: " + rateSlider.value().toFixed(2), rateSlider.x + rateSlider.width + 60, rateSlider.y + 15);
    text("Volume: " + volumeSlider.value().toFixed(2), volumeSlider.x + volumeSlider.width + 60, volumeSlider.y + 15);
}

function drawWave() {
    let level = amplitude.getLevel();
    amplitudes.push(level);
    amplitudes.shift();
    
    // Фиолетовая линия
    stroke(160, 50, 255);
    strokeWeight(3);
    noFill();
    
    beginShape();
    let centerY = height * 3/4;
    for (let i = 0; i < amplitudes.length; i++) {
        let y = centerY - (amplitudes[i] * height / 2);
        vertex(map(i, 0, amplitudes.length, 0, width), y);
    }
    endShape();
}

function keyPressed() {
    if (!isInitialised) {
        isInitialised = true;
        
        if (isLoaded)
            sound.loop();
    } else {
        if (key == ' ') {
            if (sound.isPaused()) sound.play();
            else sound.pause();
        }
    }
}

function windowResized() {
    let canvasWidth = windowWidth;
    let canvasHeight = windowWidth * 9 / 16;
    
    if (canvasHeight > windowHeight) {
        canvasHeight = windowHeight;
        canvasWidth = windowHeight * 16 / 9;
    }
    
    resizeCanvas(canvasWidth, canvasHeight);
    rateSlider.position(20, height - 60);
    volumeSlider.position(20, height - 30);
}
