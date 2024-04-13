//window.addEventListener("load", function () {
//    var cardItems = document.querySelectorAll(".card_item");
//    var maxHeight = 0;

//    cardItems.forEach(function (item) {
//        var itemHeight = item.clientHeight;
//        if (itemHeight > maxHeight) {
//            maxHeight = itemHeight;
//        }
//    });

//    cardItems.forEach(function (item) {
//        item.style.height = maxHeight + "px";
//    });
//});


document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll(".delete_items_button");

    deleteLinks.forEach(function (deleteLink) {
        deleteLink.addEventListener('click', function () {
            var modalId = this.getAttribute('data-items-id');

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
                    window.location.href = '/Consumable/DeleteItem/' + modalId;
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


function previewImage() {
    var newInput = document.getElementById('newInput');
    var preview = document.getElementById('image_preview');

    preview.innerHTML = '';

    if (newInput.files.length > 0) {
        var file = newInput.files[0];

        if (file.type.match(/image.*/)) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var img = document.createElement('img');
                img.src = e.target.result;
                img.className = 'card_img';

                preview.appendChild(img);
            };

            reader.readAsDataURL(file);
        } else {
            alert('Uploaded file is not an image.');
        }
    }
}

function showImage() {
    var fileInput = document.getElementById('fileEdit');
    var currentImage = document.getElementById('currentImage');
    var newImagePreview = document.getElementById('newImagePreview');

    if (fileInput.files.length > 0) {
        var file = fileInput.files[0];

        if (file.type.match(/image.*/)) {
            var reader = new FileReader();

            reader.onload = function (e) {
                newImagePreview.src = e.target.result;

                newImagePreview.style.display = 'inline';
                currentImage.style.display = 'none';
            };

            reader.readAsDataURL(file);
        } else {
            alert('Uploaded file is not an image.');
        }
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const aboutTab = document.getElementById('about_tab');
    const detailsTab = document.getElementById('details_tab');
    const aboutContent = document.getElementById('about_content');
    const detailsContent = document.getElementById('details_content');

    aboutContent.style.display = 'block';
    detailsContent.style.display = 'none';

    aboutTab.addEventListener('click', function () {
        aboutContent.style.display = 'block';
        detailsContent.style.display = 'none';

        aboutTab.classList.add('active-tab');
        detailsTab.classList.remove('active-tab');
    });

    detailsTab.addEventListener('click', function () {
        aboutContent.style.display = 'none';
        detailsContent.style.display = 'block';

        detailsTab.classList.add('active-tab');
        aboutTab.classList.remove('active-tab');
    });
});


