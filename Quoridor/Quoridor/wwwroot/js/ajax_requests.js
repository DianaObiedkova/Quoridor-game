function sendHWall () {
    $.ajax({
        type: "POST",
        url: "/Quoridor/SetFence",
        data: { "c1name": $(this).attr("cell1"), 
                "c2name": $(this).attr("cell2")},
        success: function (response) {
            alert("OK");
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

function sendVWall () {
    $.ajax({
        type: "POST",
        url: "/Quoridor/SetFence",
        data: { "c1name": $(this).attr("cell1"), 
                "c2name": $(this).attr("cell2")},
        success: function (response) {
            alert("OK");
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

function sendPawnMove() {
    $.ajax({
        type: "POST",
        url: "/Quoridor/MovePawn",
        data: { "name": $(this).attr("id") },
        success: function (response) {
            alert("OK");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            if(response.status == 400)
                alert("Invalid pawn move!");
            else
                alert(response.status);
        }
    })
}