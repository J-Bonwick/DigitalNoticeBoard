var connection = new signalR.HubConnectionBuilder().withUrl("/reload").build();
connection.on("Reload", function () {
    location.reload(true);
});

connection
    .start()
    .catch(function (err) {
        return console.error(err.toString());
    });