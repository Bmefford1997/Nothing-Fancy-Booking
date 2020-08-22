﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function checkTextBox() {
    var dateBegin = document.getElementById("datepickerBegin").value;
    var dateEnd = document.getElementById("datepickerEnd").value;
    console.log(dateBegin);
    console.log(dateEnd);
    calculateCost(dateBegin, dateEnd)
}

function calcFormat(days, cost) {
    document.getElementById("totalCost").value = '$' + (days * cost).toFixed(2);
}

function calculateCost(dateBegin, dateEnd) {
    dateBegin = new Date(dateBegin);
    dateEnd = new Date(dateEnd)
    room = document.getElementById("roomSelection").value
    roomCost = 0
    var differenceInTime = dateEnd.getTime() - dateBegin.getTime();
    var differenceInDays = (differenceInTime / (1000 * 3600 * 24)) + 1
    
    if (isNaN(differenceInDays) == false) {
        
        switch (room) {
            case "Room101":
                roomCost = 10;
                calcFormat(differenceInDays, roomCost);
                break;
            case "Room102":
                roomCost = 20;
                calcFormat(differenceInDays, roomCost);
                break;
            case "Room103":
                roomCost = 30;
                calcFormat(differenceInDays, roomCost);
                break;
            default:
                
        }
    }
}