// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
    document.getElementById("totalCost").value = (days * cost).toFixed(2);
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
            case "Room 101":
                roomCost = 10;
                calcFormat(differenceInDays, roomCost);
                break;
            case "Room 102":
                roomCost = 20;
                calcFormat(differenceInDays, roomCost);
                break;
            case "Room 103":
                roomCost = 30;
                calcFormat(differenceInDays, roomCost);
                break;
            default:

        }
    }
}

const stars = document.querySelector(".ratings").children;
const ratingValue = document.querySelector("#rating-value");
let index;

for (let i = 0; i < stars.length; i++) {
    stars[i].addEventListener("mouseover", function () {
        // console.log(i)
        for (let j = 0; j < stars.length; j++) {
            stars[j].classList.remove("fa-star");
            stars[j].classList.add("fa-star-o");
        }
        for (let j = 0; j <= i; j++) {
            stars[j].classList.remove("fa-star-o");
            stars[j].classList.add("fa-star");
        }
    })
    stars[i].addEventListener("click", function () {
        ratingValue.value = i + 1;
        index = i;
    })
    stars[i].addEventListener("mouseout", function () {

        for (let j = 0; j < stars.length; j++) {
            stars[j].classList.remove("fa-star");
            stars[j].classList.add("fa-star-o");
        }
        for (let j = 0; j <= index; j++) {
            stars[j].classList.remove("fa-star-o");
            stars[j].classList.add("fa-star");
        }
    })
}

function sortList() {
    var list, i, switching, b, shouldSwitch;
    list = document.getElementById("id01");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // start by saying: no switching is done:
        switching = false;
        b = list.getElementsByTagName("LI");
        // Loop through all list-items:
        for (i = 0; i < (b.length - 1); i++) {
            // start by saying there should be no switching:
            shouldSwitch = false;
            /* check if the next item should
            switch place with the current item: */
            if (b[i].innerHTML.toLowerCase() > b[i + 1].innerHTML.toLowerCase()) {
                /* if next item is alphabetically
                lower than current item, mark as a switch
                and break the loop: */
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark the switch as done: */
            b[i].parentNode.insertBefore(b[i + 1], b[i]);
            switching = true;
        }
    }
}
