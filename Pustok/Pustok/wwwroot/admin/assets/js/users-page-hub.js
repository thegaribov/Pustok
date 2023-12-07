const usersPageConnection = new signalR.HubConnectionBuilder()
    .withUrl("/userspagehub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

usersPageConnection.on("ReceiveUserStatus", (data) => {
    $(`[users]`)
        .find(`[data-id='${data.userId}']`)
        .find(`[user-status]`)
        .html(GetUserStatus(data.isOnline))
});


async function start() {
    try {
        await usersPageConnection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

usersPageConnection.onclose(async () => {
    await start();
});

// Start the connection.
start();

function GetUserStatus(isOnline) {
    console.log(isOnline)

    if (isOnline) {
        return `<span class="badge badge-soft-success">Online</span>`
    }
    else {
        return `<span class="badge badge-soft-danger">Offline</span>`
    }
}