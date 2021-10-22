function sendHWall (e) {
    $.ajax({
        type: "POST",
        url: "/Quoridor/SetFence",
        data: { "c1name": $(this).attr("cell1"), 
                "c2name": $(this).attr("cell2")},
        success: function (response) {
            getPossibleMoves();
            selectHWall(e);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            if(response.status == 400)
                alert("Invalid wall placement!");
            else
                alert(response.status);
        }
    })
}

function sendVWall(e) {
    $.ajax({
        type: "POST",
        url: "/Quoridor/SetFence",
        data: {
            "c1name": $(this).attr("cell1"),
            "c2name": $(this).attr("cell2")
        },
        success: function (response) {
            getPossibleMoves();
            selectVWall(e);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            if (response.status == 400)
                alert("Invalid wall placement!");
            else
                alert(response.status);
        }
    })
}

function sendPawnMove() {
    $.ajax({
        type: "POST",
        url: "/Quoridor/MovePawn",
        data: { "name": $(this).attr("id") },
        success: function (response) {
            getPossibleMoves();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            if (response.status == 400)
                alert("Invalid pawn move!");
            else
                alert(response.status);
        }
    });

    
}

function getPossibleMoves() {
    $.ajax({
        type: "GET",
        url: "/Quoridor/PossibleMoves",
        success: function (response) {

            let helper = document.getElementById("movesHelper");

            removeHelperElements();

            helper.insertAdjacentHTML('afterbegin', response);
            
            validPawnMoves();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            if (response.status == 400)
                alert("Impossible moves!");
            else
                alert(response.status);
        }
    })
}