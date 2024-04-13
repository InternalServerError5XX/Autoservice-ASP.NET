document.addEventListener('DOMContentLoaded', () => {
    const header = document.querySelector('.header');

    header.style.animation = 'fadeInHeader 0.5s ease-in-out forwards';

    setTimeout(() => {
        header.style.animation = 'none';
    }, 500);
});

const header = document.querySelector('.header');
let lastScrollPosition = window.pageYOffset;
let isHeaderVisible = true;

function handleScroll() {
    const currentScrollPosition = window.pageYOffset;

    if (currentScrollPosition < lastScrollPosition) {
        if (!isHeaderVisible) {
            header.classList.remove('hidden');
            isHeaderVisible = true;
        }
    } else {
        if (isHeaderVisible) {
            header.classList.add('hidden');
            isHeaderVisible = false;
        }
    }

    lastScrollPosition = currentScrollPosition;
}

window.addEventListener('scroll', handleScroll);


const showModalButton = document.getElementById("showModalButton");
const modal = document.getElementById("modal");
const closeButton = document.querySelector(".close_button");
const body = document.body;

showModalButton.addEventListener("click", () => {
    modal.style.display = "block";
    body.style.overflow = "hidden";
});

closeButton.addEventListener("click", () => {
    modal.style.display = "none";
    body.style.overflow = "auto";
});

window.addEventListener("click", (event) => {
    if (event.target === modal) {
        modal.style.display = "none";
        body.style.overflow = "auto";
    }
});

function toggleCallBack() {
    var checkBox = document.getElementById("rememberMe");
    var callBackInput = document.getElementById("CallBack");

    callBackInput.value = checkBox.checked;
}

document.addEventListener("DOMContentLoaded", function () {
    const currentDate = new Date();
    currentDate.setHours(currentDate.getHours() + 3);
    const formattedDate = currentDate.toISOString().split('T')[0];
    document.getElementById('date').value = formattedDate;
});



window.addEventListener("DOMContentLoaded", function () {
    [].forEach.call(document.querySelectorAll('.inputPhone'), function (input) {
        var keyCode;
        function mask(event) {
            event.keyCode && (keyCode = event.keyCode);
            var pos = this.selectionStart;
            if (pos < 3) event.preventDefault();
            var matrix = "+380 (__) ___ ____",
                i = 0,
                def = matrix.replace(/\D/g, ""),
                val = this.value.replace(/\D/g, ""),
                new_value = matrix.replace(/[_\d]/g, function (a) {
                    return i < val.length ? val.charAt(i++) || def.charAt(i) : a
                });
            i = new_value.indexOf("_");
            if (i != -1) {
                i < 5 && (i = 3);
                new_value = new_value.slice(0, i)
            }
            var reg = matrix.substr(0, this.value.length).replace(/_+/g,
                function (a) {
                    return "\\d{1," + a.length + "}"
                }).replace(/[+()]/g, "\\$&");
            reg = new RegExp("^" + reg + "$");
            if (!reg.test(this.value) || this.value.length < 5 || keyCode > 47 && keyCode < 58) this.value = new_value;
            if (event.type == "blur" && this.value.length < 5) this.value = ""
        }

        input.addEventListener("input", mask, false);
        input.addEventListener("focus", mask, false);
        input.addEventListener("blur", mask, false);
        input.addEventListener("keydown", mask, false)

    });

});


