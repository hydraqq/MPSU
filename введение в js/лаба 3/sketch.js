let sound;
let isInitialised;
let isLoaded = false;
let amplitude;
let amplitudes = [];
let fft;

function preload() {
    soundFormats('mp3', 'wav');
    sound = loadSound('assets/yee-king_track.mp3', () => {
        console.log("sound is loaded!");
    });
    isInitialised = false; 
    sound.setVolume(0.2);
}

function setup() {
    createCanvas(1024, 1024);
    textAlign(CENTER);
    textSize(32);
    
    amplitude = new p5.Amplitude();
    
    fft = new p5.FFT();
}

function draw() {
    background(30, 30, 30);
    fill(255);
    
    if (isInitialised && !sound.isPlaying()) {
        text("Press any key for play sound", width / 2, height / 2);
    } else if (sound.isPlaying()) {
        let level = amplitude.getLevel();
        text(level, width / 2, 40);
        let freqs = fft.analyze();
        translate(width / 2, height / 2);
        noFill();

        for (let i = 2; i < freqs.length - 2; i += 5) {
            let avg = (freqs[i - 2] + freqs[i - 1] + freqs[i] + freqs[i + 1] + freqs[i + 2]) / 5;
            let radius = map(avg, 0, 255, 50, 400);
            strokeWeight(2);
            stroke(map(i, 0, freqs.length, 0, 255), 100, 200);
            ellipse(0, 0, radius, radius);
        }
    }
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
