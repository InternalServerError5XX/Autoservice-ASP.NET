document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('openModal').addEventListener('click', function (e) {
        e.preventDefault();
        document.getElementById('modal_save').style.display = 'block';
    });

    document.getElementById('confirmCancel').addEventListener('click', function () {
        document.getElementById('modal_save').style.display = 'none';
    });

    document.getElementsByClassName('close')[0].addEventListener('click', function () {
        document.getElementById('modal_save').style.display = 'none';
    });

    window.addEventListener('click', function (event) {
        var modal = document.getElementById('modal_save');
        if (event.target == modal) {
            modal.style.display = 'none';
        }
    });

    document.getElementById('confirmSave').addEventListener('click', function () {
        document.getElementById('myForm').submit();
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var saveButton = document.getElementById('saveAppointmentSubmit');

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
                    document.getElementById('saveAppointmentForm').submit();
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