const usersPageConnection = new signalR.HubConnectionBuilder()
    .withUrl("/userspagehub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

usersPageConnection.on("ReceiveAlertMessage", (data) => {
    
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