var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established
connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    var list = document.getElementById("notificationList").appendChild(li);
    li.textContent = `${message}`;
    console.log("OK");
});

connection.start().then(function () {
    console.log("Start");
}).catch(function (err) {
    return console.error(err.toString());
});