
// ******* Date functions  ********//

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
function getActualFullDate() {
    var d = new Date();
    var day = addZero(d.getDate());
    var month = addZero(d.getMonth() + 1);
    var year = addZero(d.getFullYear());
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
    var s = addZero(d.getSeconds());
    return day + ". " + month + ". " + year + " (" + h + ":" + m + ")";
}

function getActualHour() {
    var d = new Date();
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
    var s = addZero(d.getSeconds());
    return h + ":" + m + ":" + s;
}

function getActualDate() {
    var d = new Date();
    var day = addZero(d.getDate());
    var month = addZero(d.getMonth() + 1);
    var year = addZero(d.getFullYear());
    return month + "/" + day + "/" + year;
}

function custgetActualDate(cust) {
    var someDate = new Date();
    var numberOfDaysToAdd = cust;
    someDate.setDate(someDate.getDate() + numberOfDaysToAdd);

    var dd = addZero(someDate.getDate());
    var mm = addZero(someDate.getMonth() + 1);
    var y = addZero(someDate.getFullYear());

    var someFormattedDate = mm + '/' + dd + '/' + y;
    return someFormattedDate;
}

// ******* End of Date functions  ********//
