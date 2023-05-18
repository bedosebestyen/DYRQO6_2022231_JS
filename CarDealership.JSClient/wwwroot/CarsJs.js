let cars = [];
let customers = [];
let shops = [];
let managers = [];

let connection = null;

carsgetdata();
customersgetdata();
shopgetdata();
managergetdata();

setupSignalR();

let carIdToUpdate = -1;
let customerIdToUpdate = -1;
let shopIdToUpdate = -1;
let managerIdToUpdate = -1;

function setupSignalR() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18906/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        carsgetdata();
    });
    connection.on("CarDeleted", (user, message) => {
        carsgetdata();
    });
    connection.on("CarUpdated", (user, message) => {
        carsgetdata();
    });

    connection.on("CustomerCreated", (user, message) => {
        customersgetdata();
    });
    connection.on("CustomerDeleted", (user, message) => {
        customersgetdata();
    });
    connection.on("CustomerUpdated", (user, message) => {
        customersgetdata();
    });

    connection.on("ShopCreated", (user, message) => {
        shopgetdata();
    });
    connection.on("ShopDeleted", (user, message) => {
        shopgetdata();
    });
    connection.on("ShopUpdated", (user, message) => {
        shopgetdata();
    });

    connection.on("ManagerCreated", (user, message) => {
        managergetdata();
    });
    connection.on("ManagerDeleted", (user, message) => {
        managergetdata();
    });
    connection.on("ManagerUpdated", (user, message) => {
        managergetdata();
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

async function carsgetdata() {
    await fetch('http://localhost:18906/cars')
        .then(x => x.json())
        .then(y => {
            cars = y.$values;
            carsdisplay();
        });
}




function carsdisplay() {
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
            + `<button type="button" onclick="carremove(${t.carId})">Delete</button>`
        + `<button type="button" onclick="carshowupdate(${t.carId})">Update</button>`

            + "</td></tr>";
    });
}


function createform() {
    
    document.getElementById('formdiv').style.display = 'flex';
    document.getElementById('createbutton').style.display = 'none';
}


function carcreate() {
    document.getElementById('formdiv').style.display = 'none';
    document.getElementById('createbutton').style.display = 'flex';
    let type = document.getElementById('cartype').value;
    let color = document.getElementById('carcolor').value;
    let price = document.getElementById('carprice').value;
    fetch('http://localhost:18906/cars/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { carType: type, carColor: color, price: price })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            carsgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function carremove(id) {
    fetch('http://localhost:18906/cars/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            carsgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function carshowupdate(id) {
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
            carsgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}




async function customersgetdata() {
    await fetch('http://localhost:18906/customer')
        .then(x => x.json())
        .then(y => {
            customers = y.$values;
            customerdisplay();
            console.log(customers);
        });
}

function customerdisplay() {
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
            + `<button type="button" onclick="customerremove(${t.customerId})">Delete</button>`
            + `<button type="button" onclick="customershowupdate(${t.customerId})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}

function customer_createform() {

    document.getElementById('customer_formdiv').style.display = 'flex';
    document.getElementById('customer_createbutton').style.display = 'none';
}


function customercreate() {
    document.getElementById('customer_formdiv').style.display = 'none';
    document.getElementById('customer_createbutton').style.display = 'flex';
    let name = document.getElementById('customername').value;
    let age = document.getElementById('customerage').value;
    let address = document.getElementById('customeraddress').value;
    fetch('http://localhost:18906/customer/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, age: age, address: address })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            customersgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function customerremove(id) {
    fetch('http://localhost:18906/customer/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            customersgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function customershowupdate(id) {
    document.getElementById('nameupdate').value = customers.find(t => t['customerId'] == id)['name'];
    document.getElementById('ageupdate').value = customers.find(x => x['customerId'] == id)['age'];
    document.getElementById('addressupdate').value = customers.find(x => x['customerId'] == id)['address'];
    document.getElementById('customerupdateformdiv').style.display = 'flex';
    customerIdToUpdate = id;
}

function customerupdate() {
    document.getElementById('customerupdateformdiv').style.display = 'none';
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
            customersgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}





async function shopgetdata() {
    await fetch('http://localhost:18906/carshop')
        .then(x => x.json())
        .then(y => {
            shops = y.$values;
            shopdisplay();
        });
}




function shopdisplay() {
    document.getElementById('shop_resultarea').innerHTML = "";
    shops.forEach(t => {
        document.getElementById('shop_resultarea').innerHTML +=
            "<tr><td>" + t.shopId
            + "</td><td>"
            + t.name
            + "</td><td>"
            + t.availableCarsCount
            +
            "</td><td>"
            + t.address + "</td><td>"
            + `<button type="button" onclick="shopremove(${t.shopId})">Delete</button>`
            + `<button type="button" onclick="shopshowupdate(${t.shopId})">Update</button>`
            + "</td></tr>";
    });
}

function shop_createform() {

    document.getElementById('shop_formdiv').style.display = 'flex';
    document.getElementById('shop_createbutton').style.display = 'none';
}

function shopcreate() {
    document.getElementById('shop_formdiv').style.display = 'none';
    document.getElementById('shop_createbutton').style.display = 'flex';
    let name = document.getElementById('shopname').value;
    let address = document.getElementById('shopaddress').value;
    let carscount = document.getElementById('carscount').value;
    fetch('http://localhost:18906/carshop/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, availableCarsCount: carscount, address: address })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shopgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function shopremove(id) {
    fetch('http://localhost:18906/carshop/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shopgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function shopshowupdate(id) {
    document.getElementById('shopnameupdate').value = shops.find(t => t['shopId'] == id)['name'];
    document.getElementById('countupdate').value = shops.find(x => x['shopId'] == id)['availableCarsCount'];
    document.getElementById('shopaddressupdate').value = shops.find(x => x['shopId'] == id)['address'];
    document.getElementById('shopupdateformdiv').style.display = 'flex';
    shopIdToUpdate = id;
}

function shopupdate() {
    document.getElementById('shopupdateformdiv').style.display = 'none';
    let name = document.getElementById('shopnameupdate').value;
    let count = document.getElementById('countupdate').value;
    let address = document.getElementById('shopaddressupdate').value;

    fetch('http://localhost:18906/carshop/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, shopId: shopIdToUpdate, availableCarsCount: count, address: address })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shopgetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}



async function managergetdata() {
    await fetch('http://localhost:18906/manager')
        .then(x => x.json())
        .then(y => {
            managers = y.$values;
            managerdisplay();
            console.log(managers);
        });
}




function managerdisplay() {
    document.getElementById('manager_resultarea').innerHTML = "";
    managers.forEach(t => {
        document.getElementById('manager_resultarea').innerHTML +=
            "<tr><td>" + t.managerId
            + "</td><td>" + t.name + "</td><td>"
            + t.age + "</td><td>"
            + t.salary + " $" + "</td><td>"
            + `<button type="button" onclick="managerremove(${t.managerId})">Delete</button>`
            + `<button type="button" onclick="managerhowupdate(${t.managerId})">Update</button>`
            + "</td></tr>";
        console.log(t.Name);
    });
}
function manager_createform() {

    document.getElementById('manager_formdiv').style.display = 'flex';
    document.getElementById('manager_createbutton').style.display = 'none';
}
function managercreate() {
    document.getElementById('manager_formdiv').style.display = 'none';
    document.getElementById('manager_createbutton').style.display = 'flex';
    let name = document.getElementById('managername').value;
    let age = document.getElementById('managerage').value;
    let salary = document.getElementById('managersalary').value;
    fetch('http://localhost:18906/manager/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name , age: age, salary: salary})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            managergetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function managerremove(id) {
    fetch('http://localhost:18906/manager/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            managergetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function managerhowupdate(id) {
    document.getElementById('managernameupdate').value = managers.find(t => t['managerId'] == id)['name'];
    document.getElementById('managerageupdate').value = managers.find(x => x['managerId'] == id)['age'];
    document.getElementById('salaryupdate').value = managers.find(x => x['managerId'] == id)['salary'];
    document.getElementById('managerupdateformdiv').style.display = 'flex';
    managerIdToUpdate = id;
}

function managerupdate() {
    document.getElementById('managerupdateformdiv').style.display = 'none';
    let name = document.getElementById('managernameupdate').value;
    let age = document.getElementById('managerageupdate').value;
    let salary = document.getElementById('salaryupdate').value;

    fetch('http://localhost:18906/manager/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name, managerId: managerIdToUpdate, Age: age, Salary: salary })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            managergetdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

