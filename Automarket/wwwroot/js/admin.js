document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll('.delete_items');
    deleteLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            var modalId = this.getAttribute('data-account-id');

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '/Admin/DeleteAccount/' + modalId;
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll(".delete_appointments_button");

    deleteLinks.forEach(function (deleteLink) {
        deleteLink.addEventListener('click', function () {
            var modalId = this.getAttribute('data-appointments-id');

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '/Appointment/DeleteAppointment/' + modalId;
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll(".delete_services_button");

    deleteLinks.forEach(function (deleteLink) {
        deleteLink.addEventListener('click', function () {
            var modalId = this.getAttribute('data-services-id');

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '/Maintenance/DeleteMaintenance/' + modalId;
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var saveButton = document.getElementById('createAccountSubmit');

    if (saveButton) {
        saveButton.addEventListener('click', function (event) {
            event.preventDefault();

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById('createAccountForm').submit();
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    }
});


document.addEventListener('DOMContentLoaded', function () {
    var saveButton = document.getElementById('saveMaintenanceSubmit');

    if (saveButton) {
        saveButton.addEventListener('click', function (event) {
            event.preventDefault();

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById('saveMaintenanceForm').submit();
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    }
});


document.addEventListener('DOMContentLoaded', function () {
    var saveButton = document.getElementById('saveConsumableSubmit');

    if (saveButton) {
        saveButton.addEventListener('click', function (event) {
            event.preventDefault();

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#28a745",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                customClass: {
                    confirmButton: 'confirmSave',
                    cancelButton: 'confirmCancel'
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById('createConsumableForm').submit();
                }
            });

            var confirmButton = document.querySelector('.confirmSave');
            var cancelButton = document.querySelector('.confirmCancel');

            if (confirmButton && cancelButton) {
                confirmButton.style.fontSize = '17px';
                cancelButton.style.fontSize = '17px';
            }
        });
    }
});


function searchUsers() {
    var input, filter, table, tr, td, i, emailTxt, idTxt;
    input = document.getElementById("emailSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("usersTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        emailTd = tr[i].getElementsByTagName("td")[1];
        idTd = tr[i].getElementsByTagName("td")[0];

        if (emailTd && idTd) {
            emailTxt = emailTd.textContent || emailTd.innerText;
            idTxt = idTd.textContent || idTd.innerText;

            if (emailTxt.toUpperCase().indexOf(filter) > -1 || idTxt === filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


document.addEventListener("DOMContentLoaded", function () {
    var adminHeader = document.getElementById("adminHeader");
    var administratorHeader = document.getElementById("administratorHeader");
    var mechanicHeader = document.getElementById("mechanicHeader");
    var userHeader = document.getElementById("userHeader");
    var allUsersHeader = document.getElementById("allUsersHeader");
    var usersTable = document.getElementById("usersTable");

    adminHeader.addEventListener("click", function () {
        filterUsersByRole("Admin");
    });

    administratorHeader.addEventListener("click", function () {
        filterUsersByRole("Administrator");
    });

    mechanicHeader.addEventListener("click", function () {
        filterUsersByRole("Mechanic");
    });

    userHeader.addEventListener("click", function () {
        filterUsersByRole("User");
    });

    allUsersHeader.addEventListener("click", function () {
        showAllUsers();
    });


    function filterUsersByRole(role) {
        console.log("Filtering items by type: " + role);
        var rows = usersTable.querySelectorAll(".users_info_tr");
        for (var i = 0; i < rows.length; i++) {
            var userRole = rows[i].querySelector(".user_info:nth-child(3)").textContent.trim();
            if (userRole === role) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    }

    function showAllUsers() {
        var rows = usersTable.querySelectorAll(".users_info_tr");
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = "";
        }
    }
});



function searchItemsAdmin() {
    var input, filter, table, tr, emailTd, idTd, emailTxt, idTxt, i;
    input = document.getElementById("itemsSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("itemsTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        emailTd = tr[i].getElementsByTagName("td")[1];
        idTd = tr[i].getElementsByTagName("td")[0];

        if (emailTd && idTd) {
            emailTxt = emailTd.textContent || emailTd.innerText;
            idTxt = idTd.textContent || idTd.innerText;

            if (emailTxt.toUpperCase().indexOf(filter) > -1 || idTxt.toUpperCase() === filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var carOilsHeader = document.getElementById("carOilsHeader");
    var carChemistryHeader = document.getElementById("carChemistryHeader");
    var carCosmeticsHeader = document.getElementById("carCosmeticsHeader");
    var allItemsHeader = document.getElementById("allItemsHeader");
    var itemsTable = document.getElementById("itemsTable");

    carOilsHeader.addEventListener("click", function () {
        filterItemsByType("Car Oils");
    });

    carChemistryHeader.addEventListener("click", function () {
        filterItemsByType("Car Chemistry");
    });

    carCosmeticsHeader.addEventListener("click", function () {
        filterItemsByType("Car Cosmetics");
    });

    allItemsHeader.addEventListener("click", function () {
        showAllItems();
    });


    function getCategoryByDisplayName(displayName) {
        switch (displayName) {
            case "Car Oils":
                return "Car_Oils";
            case "Car Chemistry":
                return "Car_Chemistry";
            case "Car Cosmetics":
                return "Car_Cosmetics";
            default:
                return "";
        }
    }

    function filterItemsByType(type) {
        console.log("Filtering items by type: " + type);
        var rows = itemsTable.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            var itemType = rows[i].querySelector(".user_info:nth-child(4)").textContent.trim();
            var category = getCategoryByDisplayName(type);
            if (itemType === category) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    }  

    function showAllItems() {
        var rows = itemsTable.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = "";
        }
    }
});


function searchAppointmentsAdmin() {
    var input, filter, table, tr, emailTd, idTd, phnumTd, emailTxt, idTxt, phnumTxt, i;
    input = document.getElementById("appointmentsSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("appointmentsTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        emailTd = tr[i].getElementsByTagName("td")[1];
        idTd = tr[i].getElementsByTagName("td")[0];
        phnumTd = tr[i].getElementsByTagName("td")[3];

        if (emailTd && idTd && phnumTd) {
            emailTxt = emailTd.textContent || emailTd.innerText;
            idTxt = idTd.textContent || idTd.innerText;
            phnumTxt = phnumTd.textContent || phnumTd.innerText;

            if (emailTxt.toUpperCase().indexOf(filter) > -1 || idTxt.toUpperCase() === filter || phnumTxt.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


document.addEventListener("DOMContentLoaded", function () {
    var completedHeader = document.getElementById("completedHeader");
    var uncompletedHeader = document.getElementById("uncompletedHeader");
    var callbackHeader = document.getElementById("callbackHeader");
    var allAppointmentsHeader = document.getElementById("allAppointmentsHeader");
    var appointmentsTable = document.getElementById("appointmentsTable");

    completedHeader.addEventListener("click", function () {
        filterAppointmentsByType("Completed");
    });

    uncompletedHeader.addEventListener("click", function () {
        filterAppointmentsByType("Uncompleted");
    });

    callbackHeader.addEventListener("click", function () {
        filterAppointmentsByType("Waiting for callback");
    });

    allAppointmentsHeader.addEventListener("click", function () {
        showAllAppointments();
    });

    function filterAppointmentsByType(type) {
        console.log("Filtering appointments by type: " + type);
        var rows = appointmentsTable.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            var callbackCheck = rows[i].classList.contains("CallBack_True");
            var completedCheck = rows[i].classList.contains("Completed");

            if (type === "Completed" && completedCheck) {
                rows[i].style.display = "";
            } else if (type === "Uncompleted" && !completedCheck) {
                rows[i].style.display = "";
            } else if (type === "Waiting for callback" && callbackCheck && !completedCheck) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    }

    function showAllAppointments() {
        var rows = document.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = "";
        }
    }
});


function searchMaintenancesAdmin() {
    var input, filter, table, tr, emailTd, idTd, phnumTd, nameTd, emailTxt, idTxt, phnumTxt, nameTxt, i;
    input = document.getElementById("maintenancesSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("maintenancesTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        idTd = tr[i].getElementsByTagName("td")[0];
        emailTd = tr[i].getElementsByTagName("td")[1];  
        nameTd = tr[i].getElementsByTagName("td")[2];
        phnumTd = tr[i].getElementsByTagName("td")[3];

        if (emailTd && idTd && phnumTd) {
            emailTxt = emailTd.textContent || emailTd.innerText;
            idTxt = idTd.textContent || idTd.innerText;
            nameTxt = nameTd.textContent || nameTd.innerText;
            phnumTxt = phnumTd.textContent || phnumTd.innerText;

            if (emailTxt.toUpperCase().indexOf(filter) > -1 || idTxt.toUpperCase() === filter ||
                phnumTxt.toUpperCase().indexOf(filter) > -1 || nameTxt.toUpperCase() === filter) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var activeHeader = document.getElementById("activeHeader");
    var doneHeader = document.getElementById("doneHeader");
    var pendingHeader = document.getElementById("pendingHeader");
    var diagnosticsHeader = document.getElementById("diagnosticsHeader");
    var serviceHeader = document.getElementById("serviceHeader");
    var allMaintenancesHeader = document.getElementById("allMaintenancesHeader");
    var maintenancesTable = document.getElementById("maintenancesTable");

    activeHeader.addEventListener("click", function () {
        filterMaintenancesByType("Active");
    });

    doneHeader.addEventListener("click", function () {
        filterMaintenancesByType("Done");
    });

    pendingHeader.addEventListener("click", function () {
        filterMaintenancesByType("Pending");
    });

    diagnosticsHeader.addEventListener("click", function () {
        filterMaintenancesByType("Diagnostics");
    });

    serviceHeader.addEventListener("click", function () {
        filterMaintenancesByType("Service");
    });

    allMaintenancesHeader.addEventListener("click", function () {
        showAllMaintenances();
    });

    function filterMaintenancesByType(type) {
        console.log("Filtering items by type: " + type);
        var rows = maintenancesTable.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            var maintenanceType = rows[i].querySelector(".user_info:nth-child(5)").textContent.trim();
            if (type === "Active" && maintenanceType !== "Done") {
                rows[i].style.display = "";
            } else if (maintenanceType === type) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    }

    function showAllMaintenances() {
        var rows = document.querySelectorAll(".items_info_tr");
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = "";
        }
    }
});


document.addEventListener('DOMContentLoaded', function () {
    const appointmentsTab = document.getElementById('appointmentsTab');
    const usersTab = document.getElementById('usersTab');
    const itemsTab = document.getElementById('itemsTab');
    const servicesTab = document.getElementById('servicesTab');   

    const appointmentsContent = document.getElementById('appointments_content');
    const usersContent = document.getElementById('users_content');
    const itemsContent = document.getElementById('items_content');
    const servicesContent = document.getElementById('services_content');

    appointmentsContent.style.display = 'block';
    usersContent.style.display = 'none';
    itemsContent.style.display = 'none';
    servicesContent.style.display = 'none';

    appointmentsTab.addEventListener('click', function () {
        appointmentsContent.style.display = 'block';
        usersContent.style.display = 'none';
        itemsContent.style.display = 'none';
        servicesContent.style.display = 'none';

        appointmentsTab.classList.add('active-tab');
        usersTab.classList.remove('active-tab');
        itemsTab.classList.remove('active-tab');
        servicesTab.classList.remove('active-tab');
    }); 

    usersTab.addEventListener('click', function () {
        appointmentsContent.style.display = 'none';
        usersContent.style.display = 'block';
        itemsContent.style.display = 'none';
        servicesContent.style.display = 'none';

        appointmentsTab.classList.remove('active-tab');
        usersTab.classList.add('active-tab');
        itemsTab.classList.remove('active-tab');
        servicesTab.classList.remove('active-tab');
    });

    itemsTab.addEventListener('click', function () {
        appointmentsContent.style.display = 'none';
        usersContent.style.display = 'none';
        itemsContent.style.display = 'block';
        servicesContent.style.display = 'none';

        appointmentsTab.classList.remove('active-tab');
        usersTab.classList.remove('active-tab');
        itemsTab.classList.add('active-tab');
        servicesTab.classList.remove('active-tab');
    });

    servicesTab.addEventListener('click', function () {
        appointmentsContent.style.display = 'none';
        usersContent.style.display = 'none';
        itemsContent.style.display = 'none';
        servicesContent.style.display = 'block';

        appointmentsTab.classList.remove('active-tab');
        usersTab.classList.remove('active-tab');
        itemsTab.classList.remove('active-tab');
        servicesTab.classList.add('active-tab');
    });
});
