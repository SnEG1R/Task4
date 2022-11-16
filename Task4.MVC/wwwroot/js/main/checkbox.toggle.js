let checkbox = document.getElementById('check-all');

checkbox.addEventListener("change", function () {
    Toggle(checkbox.checked);
});

function Toggle(toggle) {
    let checkboxes = document.querySelectorAll('input[type=checkbox]');

    checkboxes.forEach((checkbox) => {
        checkbox.checked = toggle;
    })
}