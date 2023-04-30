let customers = [];
const connection = null;
getdata();
setupSignalR();

let customerIdToUpdate = -1;

function setupSignalR() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18906/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CustomerCreated", (user, message) => {
        getdata();
    });
    connection.on("CustomerDeleted", (user, message) => {
        getdata();
    });
    connection.on("CustomerUpdated", (user, message) => {
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
    await fetch('http://localhost:18906/customer')
        .then(x => x.json())
        .then(y => {
            customers = y.$values;
            display();
        });
}




function display() {
    document.getElementById('customer_resultarea').innerHTML = "";
    customers.forEach(t => {
        document.getElementById('customer_resultarea').innerHTML +=
            "<tr><td>" + t.customerId
        + "</td><td>"
        + t.name
        + "</td><td>"
        + t.age
        + "</td><td>"
        + t.address
            + "</td><td>"
        + `<button type="button" onclick="remove(${t.customerId})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.customerId})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}

function customercreate() {
    let name = document.getElementById('customername').value;
    fetch('http://localhost:18906/customer/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name : name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function remove(id) {
    fetch('http://localhost:18906/customer/' + id, {
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
    document.getElementById('nameupdate').value = customers.find(t => t['customerId'] == id)['name'];
    document.getElementById('ageupdate').value = customers.find(x => x['customerId'] == id)['age'];
    document.getElementById('addressupdate').value = customers.find(x => x['customerId'] == id)['address'];
    document.getElementById('updateformdiv').style.display = 'flex';
    customerIdToUpdate = id;
}

function customerupdate() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('nameupdate').value;
    let age = document.getElementById('ageupdate').value;
    let address = document.getElementById('addressupdate').value;

    fetch('http://localhost:18906/customer/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, customerId: customerIdToUpdate, age: age, address: address })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}