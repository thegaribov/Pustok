const connection = new signalR.HubConnectionBuilder()
    .withUrl("/alerthub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveAlertMessage", (data) => {
    let menuItem = AlertMenuItem(data);
    console.log(menuItem);
    $(".alert-menu").prepend(menuItem)

    const simpleBar = new SimpleBar(document.getElementByClassName('alert-menu'));
    simpleBar.getContentElement();

    console.log()

    console.log(data);
    console.log()
});


async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();


function AlertMenuItem(data) {

    let dropdownMenuItem = `<a href="#" class="dropdown-item py-3">
            <small class="float-end text-muted ps-2">${data.createdAt}</small>
                <div class="media">
                <div class="avatar-md bg-soft-primary">
                    <i class="ti ti-chart-arcs"></i>
                </div>
                <div class="media-body align-self-center ms-2 text-truncate">
                    <h6 class="my-0 fw-normal text-dark">${data.title}</h6>
                    <small class="text-muted mb-0">${data.content}</small>
                </div><!--end media-body-->
            </div><!--end media-->
        </a>`

    return dropdownMenuItem;
}