﻿
var connection = new signalR.HubConnectionBuilder().withUrl("/reload").build();

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();



//connection.on("Reload", function () {
//    location.reload(true);
//});

//connection
//    .start()
//    .catch(function (err) {
//        return console.error(err.toString());
//    });