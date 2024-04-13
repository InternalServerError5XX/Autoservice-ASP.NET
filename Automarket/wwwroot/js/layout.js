function searchItems() {
    var input, filter, div, item, i, txtValue, idValue;
    input = document.getElementById("itemsSearch");
    filter = input.value.toUpperCase();
    div = document.getElementsByClassName("car_consumables")[0];
    item = div.getElementsByClassName("card_item");
    var noItemsFound = document.getElementById("noItemsFound");

    var nothingFound = true;

    for (i = 0; i < item.length; i++) {
        txtValue = item[i].querySelector(".item_name").textContent || item[i].querySelector(".item_name").innerText;

        var link = item[i].querySelector("a.card_link");
        idValue = link.href.substring(link.href.lastIndexOf('/') + 1);

        if (idValue === filter || txtValue.toUpperCase().indexOf(filter) > -1) {
            item[i].style.display = "";
            nothingFound = false;
        } else {
            item[i].style.display = "none";
        }
    }

    noItemsFound.style.display = nothingFound ? "block" : "none";
}


var deleteAlert = document.getElementById('ActionAlert');
document.addEventListener('DOMContentLoaded', function () { 

    if (deleteAlert) {
        setTimeout(function () {
            closeAlert();
        }, 3000);

        deleteAlert.classList.add('show');
    }
});

function closeAlert() {
    if (deleteAlert) {
        deleteAlert.style.display = 'none';
    }
}



function showSuccessAlert(message) {
    Swal.fire({
        icon: "success",
        title: "SUCCESS",
        text: message,
        showConfirmButton: false,
        timer: 1500
    });
}

function showErrorAlert(message) {
    Swal.fire({
        icon: "error",
        title: "ERROR",
        text: message,
        showConfirmButton: false,
        timer: 1500
    });
}
