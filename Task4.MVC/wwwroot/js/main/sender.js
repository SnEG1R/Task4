let blockBtn = document.getElementById('block-btn');
let unblockBtn = document.getElementById('unblock-btn');
let deleteBtn = document.getElementById('delete-btn');

blockBtn.addEventListener('click', async () => {
    let selectedIds = getToggleUserIds().map(item => +item);

    await SendData('/Main/Block', selectedIds);
});

unblockBtn.addEventListener('click', async () => {
    let selectedIds = getToggleUserIds().map(item => +item);

    await SendData('/Main/Unblock', selectedIds);
});

deleteBtn.addEventListener('click', async () => {
    let selectedIds = getToggleUserIds().map(item => +item);

    await SendData('/Main/Delete', selectedIds);
});

async function SendData(input, data) {
    await fetch(input, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            "Content-Type": "application/json"
        }
    });

    document.location.reload();
}