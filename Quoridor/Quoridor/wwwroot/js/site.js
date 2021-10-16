// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cell = document.querySelectorAll(".cell");

let counter = 0;
let firstPWalls = 10;
let secondPWalls = 10;

const htmlPawns = [document.getElementById("pawn0"), document.getElementById("pawn1")];
const htmlBoardTable = document.getElementById("board_table");
const hwall = document.querySelectorAll(".hwall");
const vwall = document.querySelectorAll(".vwall");
const htmlRestartMessageBox = document.getElementById("restart_message_box");

_renderValidNextWalls();
validPawnMoves();
updateClicks();

const restartButton = document.getElementById("restart");

restartButton.onclick = function onclickRestartButton() {
    removePreviousFadeInoutBox();
    htmlRestartMessageBox.classList.remove("hidden");
};

const restartYesNoButton = {
    yes: document.getElementById("restart_yes"),
    no: document.getElementById("restart_no")
};
const onclickRestartYesNoButton = function (e) {
    const x = e.target;
    htmlRestartMessageBox.classList.add("hidden");
    
    if (x.id === "restart_yes") {
        restart();
    }
}
restartYesNoButton.yes.onclick = onclickRestartYesNoButton;
restartYesNoButton.no.onclick = onclickRestartYesNoButton;

function removePreviousFadeInoutBox() {
    let previousBoxes;
    if (previousBoxes === document.getElementsByClassName("fade_box inout")) {
        while (previousBoxes.length !== 0) {
            previousBoxes[0].remove();
        }
    }
}

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

function validPawnMoves() {
    let x = 0;
    let y = 0;

    let posMoveX = document.querySelectorAll(".posMoveX");
    let posMoveY = document.querySelectorAll(".posMoveY");

    for (let i = 0; i < htmlBoardTable.rows.length; i++) {
        for (let j = 0; j < htmlBoardTable.rows[0].cells.length; j++) {

            let el = htmlBoardTable.rows[i].cells[j];

            if (counter % 2 === 0) {

                if (el.hasChildNodes() && el.firstChild.classList.contains('pawn0')) {
                    x = j;
                    y = i;

                    for (let l = 0; l < posMoveX.length; l++) {
                        let newX = (parseInt(x) + 2 * parseInt(posMoveX[l].innerHTML));
                        let newY = (parseInt(y) + 2 * parseInt(posMoveY[l].innerHTML));
                        //console.log(newX, newY);

                        let element = htmlBoardTable.rows[newY].cells[newX];
                        if (!element.hasChildNodes()) {
                            //console.log(element);
                            let shadow = document.createElement("div");
                            shadow.classList.add("pawnShadow0");
                            element.appendChild(shadow);
                        }
                        
                    }
                }
            }
            else {
                if (el.hasChildNodes() && el.firstChild.classList.contains('pawn1')) {
                    x = j;
                    y = i;

                    for (let l = 0; l < posMoveX.length; l++) {
                        let newX = (parseInt(x) + 2 * parseInt(posMoveX[l].innerHTML));
                        let newY = (parseInt(y) + 2 * parseInt(posMoveY[l].innerHTML));
                        //console.log(newX, newY);

                        if (newX < 0 || newY < 0 || newX>16 ||newY>16) {
                            continue;
                        }

                        let element = htmlBoardTable.rows[newY].cells[newX];
                        if (!element.hasChildNodes()) {
                            //console.log(element);
                            let shadow = document.createElement("div");
                            shadow.classList.add("pawnShadow1");
                            element.appendChild(shadow);
                        }

                    }
                }
            }
            
        }
    }

    //removeHelperElements();

    cell.forEach((element) => {

        if (element.hasChildNodes() && (element.firstChild.classList.contains('pawnShadow0') || element.firstChild.classList.contains('pawnShadow1'))) {
        element.addEventListener('click', selectCell);
        }

    });

}

function removeHelperElements() {
    let posMoveX = document.querySelectorAll(".posMoveX");
    let posMoveY = document.querySelectorAll(".posMoveY");

    for (let i = 0; i < posMoveX.length; i++)
    {
        posMoveX[i].remove();
        posMoveY[i].remove();
    }
}

function cancelPawnShadows() {

    cell.forEach((element) => {

        if (element.hasChildNodes() && (element.firstChild.classList.contains('pawnShadow0') || element.firstChild.classList.contains('pawnShadow1'))) {
            element.removeEventListener('click', selectCell);
        }

    });

    let shadows0 = document.querySelectorAll(".pawnShadow0");
    let shadows1 = document.querySelectorAll(".pawnShadow1");

    for (sh of shadows0) {
        sh.remove();
    }
    for (sh of shadows1) {
        sh.remove();
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

    e.target.after(pawn);
    cancelPawnShadows();
    validPawnMoves();
}

function selectHWall(e) {

    let parent = e.target.closest(".hwall");

    counter++;

    if (counter % 2 === 0) {

        if (secondPWalls > 0) {
            cancelPawnShadows();

            secondPWalls -= 1;
            let wall = document.createElement('div');
            wall.classList.add("horizontal_wall");

            parent.removeAttribute("onmouseenter");
            parent.removeAttribute("onmouseleave");
            e.target.classList.remove("shadow");

            for (let i = 0; i < 8; i++) {
                for (let j = 0; j < 8; j++) {

                    let element = htmlBoardTable.rows[i * 2 + 1].cells[j * 2];
                    if (parent === element) {
                        if (i > 0) {
                            htmlBoardTable.rows[i * 2 + 1].cells[j * 2 - 2].removeAttribute("onmouseenter");
                            htmlBoardTable.rows[i * 2 + 1].cells[j * 2 - 2].removeAttribute("onmouseleave");
                        }

                        htmlBoardTable.rows[i * 2 + 1].cells[j * 2 + 2].removeAttribute("onmouseenter");
                        htmlBoardTable.rows[i * 2 + 1].cells[j * 2 + 2].removeAttribute("onmouseleave");
                    }
                }
            }
            updateClicks();
            updateWalls();
            validPawnMoves();
        }
        else {
            counter--;
            alert("Player 2, You're out of walls");
        }
    }
    else {
       
        if (firstPWalls > 0) {
            cancelPawnShadows();
            firstPWalls -= 1;
            let wall = document.createElement('div');
            wall.classList.add("horizontal_wall");

            parent.removeAttribute("onmouseenter");
            parent.removeAttribute("onmouseleave");
            e.target.classList.remove("shadow");

            for (let i = 0; i < 8; i++) {
                for (let j = 0; j < 8; j++) {

                    let element = htmlBoardTable.rows[i * 2 + 1].cells[j * 2];
                    if (parent === element) {
                        if (i > 0) {
                            htmlBoardTable.rows[i * 2 + 1].cells[j * 2 - 2].removeAttribute("onmouseenter");
                            htmlBoardTable.rows[i * 2 + 1].cells[j * 2 - 2].removeAttribute("onmouseleave");
                        }

                        htmlBoardTable.rows[i * 2 + 1].cells[j * 2 + 2].removeAttribute("onmouseenter");
                        htmlBoardTable.rows[i * 2 + 1].cells[j * 2 + 2].removeAttribute("onmouseleave");
                    }
                }
            }
            updateClicks();
            updateWalls();
            validPawnMoves();
        }
        else {
            counter--;
            alert("Player 1, You're out of walls!");
        }
    }

}

function selectVWall(e) {

    let parent = e.target.closest(".vwall");
    
    counter++;

    if (counter % 2 === 0) {

        if (secondPWalls > 0) {

            cancelPawnShadows();

            secondPWalls -= 1;
            let wall = document.createElement('div');
            wall.classList.add("vertical_wall");
    
            parent.removeAttribute("onmouseenter");
            parent.removeAttribute("onmouseleave");
            e.target.classList.remove("shadow");

            
            for (let i = 0; i < 8; i++) {
                for (let j = 0; j < 8; j++) {

                    let element = htmlBoardTable.rows[i * 2].cells[j * 2 + 1];
                    if (parent === element) {
                        if (i > 0) {
                            htmlBoardTable.rows[i * 2 - 2].cells[j * 2 + 1].removeAttribute("onmouseenter");
                            htmlBoardTable.rows[i * 2 - 2].cells[j * 2 + 1].removeAttribute("onmouseleave");
                        }

                        htmlBoardTable.rows[i * 2 + 2].cells[j * 2 + 1].removeAttribute("onmouseenter");
                        htmlBoardTable.rows[i * 2 + 2].cells[j * 2 + 1].removeAttribute("onmouseleave");
                    }
                }
            }
            updateClicks();
            updateWalls();
            validPawnMoves();
        }
        else {
            counter--;
            alert("Player 2, You're out of walls");
        }
    }
    else {
        
        if (firstPWalls > 0) {

            cancelPawnShadows();

            firstPWalls -= 1;
            let wall = document.createElement('div');
            wall.classList.add("vertical_wall");

            parent.removeAttribute("onmouseenter");
            parent.removeAttribute("onmouseleave");
            e.target.classList.remove("shadow");

            for (let i = 0; i < 8; i++) {
                for (let j = 0; j < 8; j++) {

                    let element = htmlBoardTable.rows[i * 2].cells[j * 2 + 1];
                    if (parent === element) {
                        if (i > 0) {
                            htmlBoardTable.rows[i * 2 - 2].cells[j * 2 + 1].removeAttribute("onmouseenter");
                            htmlBoardTable.rows[i * 2 - 2].cells[j * 2 + 1].removeAttribute("onmouseleave");
                        }

                        htmlBoardTable.rows[i * 2 + 2].cells[j * 2 + 1].removeAttribute("onmouseenter");
                        htmlBoardTable.rows[i * 2 + 2].cells[j * 2 + 1].removeAttribute("onmouseleave");
                    }
                }
            }
            updateClicks();

            updateWalls();
            validPawnMoves();
        }
        else {
            counter--;
            alert("Player 1, You're out of walls");
        }
    }
}

function updateWalls() {
    let num1 = document.getElementById("num1");
    let num2 = document.getElementById("num2");

    num1.innerHTML = firstPWalls;
    num2.innerHTML = secondPWalls;
}


function updateClicks() {
    hwall.forEach((el) => {
        if (el.hasAttribute("onmouseenter")) {
            el.addEventListener('click', selectHWall);
        }
        else {
            el.removeEventListener('click', selectHWall);
        }
    });

    vwall.forEach((el) => {
        if (el.hasAttribute("onmouseenter")) {
            el.addEventListener('click', selectVWall);
        }
        else {
            el.removeEventListener('click', selectVWall);
        }
    });
}


function renderPawnPositions() {

    let pawns = [document.querySelector(".pawn0"), document.querySelector(".pawn1")];
    pawns.forEach((el) => { el.remove(); });

    htmlBoardTable.rows[0].cells[8].appendChild(htmlPawns[0]);
    htmlBoardTable.rows[16].cells[8].appendChild(htmlPawns[1]);
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

function removePreviousRender() {
    for (let i = 0; i < htmlBoardTable.rows.length; i++) {
        for (let j = 0; j < htmlBoardTable.rows[0].cells.length; j++) {
            let element = htmlBoardTable.rows[i].cells[j];
            element.removeAttribute("onmouseenter");
            element.removeAttribute("onmouseleave");
            element.onclick = null;
        }
    }
    // remove pawn shadows which are for previous board
    let previousPawnShadows = document.getElementsByClassName("pawn shadow");
    while (previousPawnShadows.length !== 0) {
        previousPawnShadows[0].remove();
    }
}

function restart() {
    removePreviousRender();
    counter = 1;
    let vwalls = document.querySelectorAll(".vertical_wall");
    let hwalls = document.querySelectorAll(".horizontal_wall");

    vwalls.forEach((el) => { el.remove(); });
    hwalls.forEach((el) => { el.remove(); });


    renderPawnPositions();
}