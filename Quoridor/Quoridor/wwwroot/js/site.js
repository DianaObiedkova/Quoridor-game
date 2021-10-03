// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const cell = document.querySelectorAll(".col");
const pawn = document.createElement('div');
pawn.classList.add("pawn");
pawn.classList.add("pawn0");

const pawn0 = document.querySelector(".pawn0");

console.log(cell);

function selectCell(e) {
    console.log(e);
    e.target.appendChild(pawn);
}

cell.forEach((element) => {
    element.addEventListener('click', selectCell);
});
