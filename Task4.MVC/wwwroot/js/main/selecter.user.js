function getToggleUserIds() {
    let ids = [];

    let checkboxes = document.querySelectorAll('.checkbox-table');
    checkboxes.forEach((item) => {
        if (item.checked) {
            let id = item.parentElement.querySelector('input[type=hidden]').value;
            ids.push(id);
        }
    });

    return ids;
}