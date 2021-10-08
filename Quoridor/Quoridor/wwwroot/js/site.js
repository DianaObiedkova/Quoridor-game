// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cell = document.querySelectorAll(".cell");

let counter = 0;

const htmlPawns = [document.getElementById("pawn0"), document.getElementById("pawn1")];
const htmlBoardTable = document.getElementById("board_table");
const hwall = document.querySelectorAll(".hwall");
const vwall = document.querySelectorAll(".vwall");

_renderValidNextWalls();

function horizontalWallShadow(x, turnOn) {
    if (turnOn === true) {
        const _horizontalWallShadow = document.createElement("div");
        _horizontalWallShadow.classList.add("horizontal_wall");
        _horizontalWallShadow.classList.add("shadow");
        x.appendChild(_horizontalWallShadow);
    } else {
        while (x.firstChild) {
            x.removeChild(x.firstChild);
        }
    }
}

function verticalWallShadow(x, turnOn) {
    if (turnOn === true) {
        const _verticalWallShadow = document.createElement("div");
        _verticalWallShadow.classList.add("vertical_wall");
        _verticalWallShadow.classList.add("shadow");
        x.appendChild(_verticalWallShadow);
    } else {
        while (x.firstChild) {
            x.removeChild(x.firstChild);
        }
    }

}

function _renderValidNextWalls() {

    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {

                let element = htmlBoardTable.rows[i * 2 + 1].cells[j * 2];
                element.setAttribute("onmouseenter", "horizontalWallShadow(this, true)");
                element.setAttribute("onmouseleave", "horizontalWallShadow(this, false)");

        }
    }

    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {

            let element = htmlBoardTable.rows[i * 2].cells[j * 2 + 1];
            element.setAttribute("onmouseenter", "verticalWallShadow(this, true)");
            element.setAttribute("onmouseleave", "verticalWallShadow(this, false)");
        }
    }
}

function selectCell(e) {

    counter++;
    let pawn0 = document.querySelector(".pawn0");
    let pawn1 = document.querySelector(".pawn1");
    let pawn = document.createElement('div');

    pawn.classList.add("pawn");

    if (counter % 2 === 0) {
        pawn1.remove();
        pawn.classList.add("pawn1");

        if (e.target.closest(".row1") !== null) {

            const box = document.createElement("div");
            box.classList.add("fade_box")
            box.classList.add("in");
            box.id = "game_result_message_box";
            box.innerHTML = "Player 2 wins!";
            const boardTableContainer = document.getElementById("board_table_container");
            boardTableContainer.appendChild(box);
        }
    }
    else {
        pawn0.remove();
        pawn.classList.add("pawn0");

        if (e.target.closest(".row10") !== null) {

            const box = document.createElement("div");
            box.classList.add("fade_box")
            box.classList.add("in");
            box.id = "game_result_message_box";
            box.innerHTML = "Player 1 wins!";
            const boardTableContainer = document.getElementById("board_table_container");
            boardTableContainer.appendChild(box);
        }
    }

    e.target.appendChild(pawn);

}

function selectHWall(e) {

    let parent = e.target.closest(".hwall");

    counter++;
    let wall = document.createElement('div');
    wall.classList.add("horizontal_wall");

    parent.removeAttribute("onmouseenter");
    parent.removeAttribute("onmouseleave");
    e.target.classList.remove("shadow");

    console.log(parent);
}

function selectVWall(e) {

    let parent = e.target.closest(".vwall");
    
    counter++;
    let wall = document.createElement('div');
    wall.classList.add("vertical_wall");
    
    parent.removeAttribute("onmouseenter");
    parent.removeAttribute("onmouseleave");
    e.target.classList.remove("shadow");

    console.log(parent);
}

function insertAfter(referenceNode, newNode) {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
}

cell.forEach((element) => {
    element.addEventListener('click', selectCell);
});

hwall.forEach((el) => {
    el.addEventListener('click', selectHWall);
});
vwall.forEach((el) => {
    el.addEventListener('click', selectVWall);
});

function _renderPawnPositions() {
    this.htmlBoardTable.rows[0].cells[4].appendChild(this.htmlPawns[0]);
    this.htmlBoardTable.rows[8].cells[4].appendChild(this.htmlPawns[1]);
}

function printGameResultMessage(message) {
    const box = document.createElement("div");
    box.classList.add("fade_box")
    box.classList.add("inout");
    box.id = "game_result_message_box";
    box.innerHTML = message;
    const boardTableContainer = document.getElementById("board_table_container");
    boardTableContainer.appendChild(box);
}