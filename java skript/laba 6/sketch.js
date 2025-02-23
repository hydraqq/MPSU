let canvas;
let brushColor = 'black';
let brushSize = 2;
let drawing = false;
let currentShape = [];
let shapes = [];

function setup() {
    canvas = createCanvas(800, 800);
    background(255);
    noFill();
    
    let colorContainer = createDiv('').style('margin-bottom', '10px');
    let colors = ['black', 'red', 'blue', 'green', 'yellow'];
    colors.forEach(color => {
        let btn = createButton(' ');
        btn.style('background-color', color);
        btn.style('width', '30px');
        btn.style('height', '30px');
        btn.style('border', 'none');
        btn.style('margin', '5px');
        btn.style('cursor', 'pointer');
        btn.mousePressed(() => brushColor = color);
        colorContainer.child(btn);
    });
    
    let toolContainer = createDiv('').style('margin-bottom', '10px');
    let thinBtn = createButton('Thin Pencil');
    thinBtn.style('padding', '10px');
    thinBtn.style('margin-right', '10px');
    thinBtn.style('cursor', 'pointer');
    thinBtn.mousePressed(() => brushSize = 2);
    
    let thickBtn = createButton('Thick Pencil');
    thickBtn.style('padding', '10px');
    thickBtn.style('cursor', 'pointer');
    thickBtn.mousePressed(() => brushSize = 8);
    
    toolContainer.child(thinBtn);
    toolContainer.child(thickBtn);
    
    let clearBtn = createButton('Clear Canvas');
    clearBtn.style('padding', '10px');
    clearBtn.style('margin-top', '10px');
    clearBtn.style('cursor', 'pointer');
    clearBtn.mousePressed(() => {
        background(255);
        shapes = [];
    });
}

function draw() {
    background(255);
    for (let shape of shapes) {
        stroke(shape.color);
        strokeWeight(shape.size);
        noFill();
        beginShape();
        for (let point of shape.points) {
            vertex(point.x, point.y);
        }
        endShape();
    }
    if (drawing) {
        stroke(brushColor);
        strokeWeight(brushSize);
        noFill();
        beginShape();
        for (let p of currentShape) {
            vertex(p.x, p.y);
        }
        endShape();
    }
}

function mousePressed() {
    if (mouseX >= 0 && mouseX <= width && mouseY >= 0 && mouseY <= height) {
        drawing = true;
        currentShape = [];
    }
}

function mouseDragged() {
    if (drawing) {
        currentShape.push({ x: mouseX, y: mouseY });
    }
}

function mouseReleased() {
    if (drawing && currentShape.length > 0) {
        drawing = false;
        shapes.push({ points: [...currentShape], color: brushColor, size: brushSize });
    }
}