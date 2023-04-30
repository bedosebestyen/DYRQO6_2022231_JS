


function selectTable() {
    let tableSelect = document.getElementById('tableSelect');
    let selectedTable = tableSelect.options[tableSelect.selectedIndex].value;
    console.log(selectedTable);
    if (selectedTable == 'carstable') {
        document.getElementById('cars_container').style.display = 'flex';
    } else {
        document.getElementById('cars_container').style.display = 'none';
    }
    if (selectedTable == 'customertable') {
        document.getElementById('customers_container').style.display = 'flex';
    } else {
        document.getElementById('customers_container').style.display = 'none';
    }
    
    
}