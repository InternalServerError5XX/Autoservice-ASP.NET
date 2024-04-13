//function populateCountries() {
//    fetch('https://restcountries.com/v2/all')
//        .then(response => response.json())
//        .then(data => {
//            const countrySelector = document.getElementById("country");
//            data.forEach(country => {
//                const optionElement = document.createElement("option");
//                optionElement.value = country.name;
//                optionElement.textContent = country.name;
//                countrySelector.appendChild(optionElement);
//            });
//        })
//        .catch(error => {
//            console.error('Country list error', error);
//        });
//}

//function getCitiesByCountry(country) {
//    const apiKey = '777cf9e7382fe65a5d7264d9a5b6054b';

//    fetch(`https://api.openweathermap.org/data/2.5/find?q=${country}&type=like&mode=json&appid=${apiKey}`)
//        .then(response => response.json())
//        .then(data => {
//            const citySelector = document.getElementById("settlement");
//            citySelector.innerHTML = "<option value=''>Select the city</option>";

//            data.list.forEach(city => {
//                const optionElement = document.createElement("option");
//                optionElement.value = city.name;
//                optionElement.textContent = city.name;
//                citySelector.appendChild(optionElement);
//            });
//        })
//        .catch(error => {
//            console.error('Error fetching city list:', error);
//        });
//}

//document.addEventListener("DOMContentLoaded", function () {
//    const countrySelector = document.getElementById("country");
//    const settlementSelector = document.getElementById("settlement");

//    countrySelector.addEventListener("change", function () {
//        const selectedCountry = countrySelector.value;
//        if (selectedCountry) {
//            getCitiesByCountry(selectedCountry);
//        }
//    });

//    populateCountries();
//});


document.addEventListener("DOMContentLoaded", function () {
    const getElement = (id) => document.getElementById(id);
    const modal = getElement("myModal");
    const invitationLink = getElement("invitationLink");
    const copyLinkButton = getElement("copyLinkButton");
    const userId = window.location.pathname.split("/").pop();
    const link = window.location.origin + "/Account/Profile/" + userId;
    const socialMediaIcons = document.querySelectorAll(".socialmedia_icon");
    const copyAlert = getElement("copyAlert");

    const showCopyAlert = () => {
        copyAlert.style.display = "block";
        const closeCopyAlert = getElement("closeCopyAlert");
        closeCopyAlert.addEventListener("click", () => {
            copyAlert.style.animation = "fadeOut 0.4s ease-in-out both";
            setTimeout(() => {
                copyAlert.style.display = "none";
                copyAlert.style.animation = "";
            }, 400);
        });

        setTimeout(() => {
            copyAlert.style.animation = "fadeOut 0.4s ease-in-out both";
            setTimeout(() => {
                copyAlert.style.display = "none";
                copyAlert.style.animation = "";
            }, 400);
        }, 3000);
    };

    const handleModal = (action) => {
        modal.style.display = action;
    };

    const handleCopyLink = () => {
        invitationLink.value = link;
        invitationLink.select();
        document.execCommand("copy");
        showCopyAlert();
    };

    const openShareButton = getElement("openShare");
    const closeModalButton = getElement("closeModalButton");

    openShareButton.addEventListener("click", () => {
        handleModal("block");
        invitationLink.value = link;
    });

    closeModalButton.addEventListener("click", () => handleModal("none"));

    copyLinkButton.addEventListener("click", handleCopyLink);

    window.addEventListener("click", (event) => {
        if (event.target === modal) {
            handleModal("none");
        }
    });

    socialMediaIcons.forEach((icon) => {
        icon.addEventListener("click", () => {
            const socialMedia = icon.getAttribute("get_socialmedia");
            const socialMediaUrlMap = {
                telegram: `https://t.me/share/url?url=${link}`,
                instagram: `https://www.instagram.com/?url=${link}`,
                twitter: `https://twitter.com/intent/tweet?url=${link}`,
                facebook: `https://www.facebook.com/sharer/sharer.php?u=${encodeURIComponent(link)}`,
                viber: `viber://forward?text=${encodeURIComponent(link)}`,
                whatsapp: `https://wa.me/?text='${encodeURIComponent(link)}`
            };

            const socialMediaUrl = socialMediaUrlMap[socialMedia];
            if (socialMediaUrl) {
                window.open(socialMediaUrl, "blank");
            }
        });
    });
});



document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll('.delete_account');
    deleteLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            var modalId = this.getAttribute('data-account-id');
            var modal = document.getElementById('modal_delete_' + modalId);
            modal.style.display = 'block';
        });
    });

    var closeButtons = document.querySelectorAll('.close, .confirmCancel');
    closeButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var modalId = this.getAttribute('data-account-id');
            var modal = document.getElementById('modal_delete_' + modalId);
            modal.style.display = 'none';
        });
    });

    window.addEventListener('click', function (event) {
        var modals = document.querySelectorAll('.modal');
        modals.forEach(function (modal) {
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        });
    });
});



document.addEventListener('DOMContentLoaded', function () {
    var saveButtons = document.querySelectorAll('.save_profile_button');
    saveButtons.forEach(function (button) {
        button.addEventListener('click', function (event) {
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
                    document.getElementById('profileForm').submit();
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


