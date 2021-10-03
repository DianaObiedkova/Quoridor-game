// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cell = document.querySelectorAll(".cell");

var counter = 0;


const htmlPawns = [document.getElementById("pawn0"), document.getElementById("pawn1")];
const htmlBoardTable = document.getElementById("board_table");

//init();
//_renderPawnPositions();

function init() {
    removeWalls();
    this.htmlPawns[0].classList.remove("hidden");
    this.htmlPawns[1].classList.remove("hidden");

    // initialize number of left walls box
    let symbolPawnList = document.getElementsByClassName("pawn symbol");
    let wallNumList = document.getElementsByClassName("wall_num");
    if (this.game.board.pawns[0].goalRow === 8) {
        symbolPawnList[0].classList.remove("pawn1");
        wallNumList[0].classList.remove("pawn1");
        symbolPawnList[0].classList.add("pawn0");
        wallNumList[0].classList.add("pawn0");

        symbolPawnList[1].classList.remove("pawn0");
        wallNumList[1].classList.remove("pawn0");
        symbolPawnList[1].classList.add("pawn1");
        wallNumList[1].classList.add("pawn1");
        this.htmlWallNum = { pawn0: wallNumList[0], pawn1: wallNumList[1] };
    } else {
        symbolPawnList[0].classList.remove("pawn0");
        wallNumList[0].classList.remove("pawn0");
        symbolPawnList[0].classList.add("pawn1");
        wallNumList[0].classList.add("pawn1");

        symbolPawnList[1].classList.remove("pawn1");
        wallNumList[1].classList.remove("pawn1");
        symbolPawnList[1].classList.add("pawn0");
        wallNumList[1].classList.add("pawn0");
        this.htmlWallNum = { pawn0: wallNumList[1], pawn1: wallNumList[0] };

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
    }
    else {
        pawn0.remove();
        pawn.classList.add("pawn0");
    }

    console.log(e);
    e.target.appendChild(pawn);
}

cell.forEach((element) => {
    element.addEventListener('click', selectCell);
});

//function removeWalls() {
//    let previousWalls = document.querySelectorAll("td > .horizontal_wall");
//    for (let i = 0; i < previousWalls.length; i++) {
//        previousWalls[i].remove();
//    }
//    previousWalls = document.querySelectorAll("td > .vertical_wall");
//    for (let i = 0; i < previousWalls.length; i++) {
//        previousWalls[i].remove();
//    }
//}


function _renderPawnPositions() {
    this.htmlBoardTable.rows[0].cells[4].appendChild(this.htmlPawns[0]);
    this.htmlBoardTable.rows[8].cells[4].appendChild(this.htmlPawns[1]);
}