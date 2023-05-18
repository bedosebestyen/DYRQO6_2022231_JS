let carsContainer = document.getElementById('cars_container');

window.onload = function () {
    carsContainer.style.display = 'flex';
    carsContainer.style.flexDirection = 'column';
    carsContainer.style.alignItems = 'center';
    carsContainer.style.gap = '2em';
}

function selectTable() {
    let tableSelect = document.getElementById('tableSelect');
    let selectedTable = tableSelect.options[tableSelect.selectedIndex].value;
    console.log(selectedTable);

    
    if (selectedTable == 'carstable') {
        
        carsContainer.style.display = 'flex';
        carsContainer.style.flexDirection = 'column';
        carsContainer.style.alignItems = 'center';
        carsContainer.style.gap = '2em';
    } else {
        document.getElementById('cars_container').style.display = 'none';
    }
    if (selectedTable == 'customertable') {
        let customersContainer = document.getElementById('customers_container');
        customersContainer.style.display = 'flex';
        customersContainer.style.flexDirection = 'column';
        customersContainer.style.alignItems = 'center';
        customersContainer.style.gap = '2em';
    } else {
        document.getElementById('customers_container').style.display = 'none';
    }
    if (selectedTable == 'shoptable') {
        let shopContainer = document.getElementById('shop_container');
        shopContainer.style.display = 'flex';
        shopContainer.style.flexDirection = 'column';
        shopContainer.style.alignItems = 'center';
        shopContainer.style.gap = '2em';
    } else {
        document.getElementById('shop_container').style.display = 'none';
    }

    if (selectedTable == 'managertable') {
        let managersContainer = document.getElementById('managers_container');
        managersContainer.style.display = 'flex';
        managersContainer.style.flexDirection = 'column';
        managersContainer.style.alignItems = 'center';
        managersContainer.style.gap = '2em';
    } else {
        document.getElementById('managers_container').style.display = 'none';
    }

    
}