// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cell = document.querySelectorAll(".cell");

let counter = 0;

const htmlPawns = [document.getElementById("pawn0"), document.getElementById("pawn1")];
const htmlBoardTable = document.getElementById("board_table");

//init();
//_renderPawnPositions();
_renderValidNextWalls();
//console.log(htmlBoardTable.rows[0].cells[1]);

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
    
    let onclickNextHorizontalWall, onclickNextVerticalWall;
    
    onclickNextHorizontalWall = function (e) {
        const x = e.currentTarget;
        horizontalWallShadow(x, false);
        //const row = (x.parentElement.rowIndex - 1) / 2;
        //const col = x.cellIndex / 2;
        //this.controller.doMove([null, [row, col], null]);
    };
    onclickNextVerticalWall = function (e) {
        const x = e.currentTarget;
        verticalWallShadow(x, false);
        //const row = x.parentElement.rowIndex / 2;
        //const col = (x.cellIndex - 1) / 2;
        //this.controller.doMove([null, null, [row, col]]);
    };

    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {
           //if (this.game.validNextWalls.horizontal[i][j] === true) {

           
                let element = htmlBoardTable.rows[i * 2 + 1].cells[j * 2];
                
                element.setAttribute("onmouseenter", "horizontalWallShadow(this, true)");
                element.setAttribute("onmouseleave", "horizontalWallShadow(this, false)");
                
                //element.onclick = onclickNextHorizontalWall.bind(this);
            //}
            //if (this.game.validNextWalls.vertical[i][j] === true) {
                
            //}
        }
    }

    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {
            let element = htmlBoardTable.rows[i * 2].cells[j * 2 + 1];

            element.setAttribute("onmouseenter", "verticalWallShadow(this, true)");
            element.setAttribute("onmouseleave", "verticalWallShadow(this, false)");
            //element.onclick = onclickNextVerticalWall.bind(this);
        }
    }
}

//function removePreviousFadeInoutBox() {
//    let previousBoxes;
//    if (previousBoxes = document.getElementsByClassName("fade_box inout")) {
//        while (previousBoxes.length !== 0) {
//            previousBoxes[0].remove();
//        }
//    }
//}


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

function printGameResultMessage(message) {
    //removePreviousFadeInoutBox();
    const box = document.createElement("div");
    box.classList.add("fade_box")
    box.classList.add("inout");
    box.id = "game_result_message_box";
    box.innerHTML = message;
    const boardTableContainer = document.getElementById("board_table_container");
    boardTableContainer.appendChild(box);
}