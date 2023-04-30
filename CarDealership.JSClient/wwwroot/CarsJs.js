let cars = [];
const connection = null;
getdata();
setupSignalR();

let carIdToUpdate = -1;

function setupSignalR() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18906/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getdata();
    });
    connection.on("CarDeleted", (user, message) => {
        getdata();
    });
    connection.on("CarUpdated", (user, message) => {
        getdata();
    });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

async function getdata() {
    await fetch('http://localhost:18906/cars')
        .then(x => x.json())
        .then(y => {
            cars = y.$values;
            display();
        });
}




function display() {
    document.getElementById('car_resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('car_resultarea').innerHTML +=
            "<tr><td>" + t.carId
            + "</td><td>"
            + t.carColor
            + "</td><td>"
            + t.carType
            +
            "</td><td>"
            + t.price + "$" + "</td><td>"
            + `<button type="button" onclick="remove(${t.carId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.carId})">Update</button>`
            + "</td></tr>";
        console.log(t.carType);
    });
}

function carcreate() {
    let type = document.getElementById('cartype').value;
    fetch('http://localhost:18906/cars/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { carType: type })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function remove(id) {
    fetch('http://localhost:18906/cars/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function showupdate(id) {
    document.getElementById('carupdate').value = cars.find(t => t['carId'] == id)['carType'];
    document.getElementById('colorupdate').value = cars.find(x => x['carId'] == id)['carColor'];
    document.getElementById('priceupdate').value = cars.find(x => x['carId'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    carIdToUpdate = id;
}

function carupdate() {
    document.getElementById('updateformdiv').style.display = 'none';
    let type = document.getElementById('carupdate').value;
    let color = document.getElementById('colorupdate').value;
    let price = document.getElementById('priceupdate').value;

    fetch('http://localhost:18906/cars/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { carType: type, carId: carIdToUpdate, carColor: color, price: price })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}