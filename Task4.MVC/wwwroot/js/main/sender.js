let blockBtn = document.getElementById('block-btn');
let unblockBtn = document.getElementById('unblock-btn');
let deleteBtn = document.getElementById('delete-btn');

blockBtn.addEventListener('click', async () => {
    let selectedIds = getToggleUserIds().map(item => +item);

    await fetch('/Main/Block', {
        method: 'POST',
        body: JSON.stringify(selectedIds),
        headers: {
            "Content-Type": "application/json"
        }
    });

    document.location.reload();
});