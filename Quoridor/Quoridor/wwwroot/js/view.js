class View {
    constructor(controller) {
        this.controller = controller;

        this.htmlBoardTable = document.getElementById("board_table");
        this.htmlPawns = [document.getElementById("pawn0"), document.getElementById("pawn1")];

        this._renderPawnPositions();
    }

 
    _renderPawnPositions() {
        this.htmlBoardTable.rows[0].cells[4].appendChild(this.htmlPawns[0]);
        this.htmlBoardTable.rows[8].cells[4].appendChild(this.htmlPawns[1]);
    }
}
//(function () {
//    'use strict';

//    angular.module('view', [
//        // Angular modules 
//        'ngRoute'

//        // Custom modules 

//        // 3rd Party Modules

//    ]);
//})();