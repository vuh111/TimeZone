const SEARCH_INPUT = document.getElementById('search-input');
const DROPDOWN_LIST = document.getElementById('dropdown-list');


var modalElement = document.querySelector('#myModal');
var modalContent = document.querySelector('.modal-content');


var ListTimezone = [];
var apiurl = `http://dev.bkholding.vn:9038/v1/timezones?page=1&size=73&filter=%7B%22FullTextSearch%22%3A%22%22%20%7D`;



fetchDataFromAPIwithFilter(apiurl).then(function (respone) {
    respone.forEach(function (value) {
        ListTimezone.push(value);

    });
    
});
SEARCH_INPUT.addEventListener('input', handleInput);



function fetchDataFromAPIwithFilter(apiurl) {
    return fetch(apiurl)
        .then(function (response) {
            return response.json();
        })
        .then(function (response) {
            return response.data;
        })
        .then(function (response) {

            var data = response.content;
            var datacount = response.content.length;
            var arr = []
            for (var index = 0; index < datacount; index++) {

                arr.push(data[index]);
            }
            return arr
        });
}




function handleInput(event) {
    
    $("#dropdown-list").empty();
    var context = SEARCH_INPUT.value;
    var resultFromSearchInput = ListTimezone.filter(function (value) {
        return value.name.toLowerCase().includes(context.toLowerCase())
    })
    addDropDownListOnchange(resultFromSearchInput);
}

function addDropDownListOnchange(data) {
    data.forEach((item) => {
        var listItemForDropList = document.createElement('li');
        listItemForDropList.className = 'List-item';
        listItemForDropList.textContent = item.name;
        listItemForDropList.addEventListener('click', () => {
            console.log(1)
            SEARCH_INPUT.value = item.name;
            DROPDOWN_LIST.innerHTML = '';
        });
        DROPDOWN_LIST.appendChild(listItemForDropList);
    });
}

function closeModal() {

    modalContent.innerHTML = ''

    modalElement.style.display = `none`
}

function openModal(data) {
    var keys = Object.keys(data);

    // Get the values of the object and log them to the console
    var values = Object.values(data);
    UpModalContent(keys, values)
}

function UpModalContent(key, arr) {
    
    var objectInfoHTML = ``;
    for (var index = 1; index < 6; index++) {
        objectInfoHTML +=
            `<span><strong>${key[index].toUpperCase()}</strong>:  ${arr[index]}</span>
     `
    }
    objectInfoHTML += `<span><strong>UTC: </strong></span>`
    var divobject = document.createElement('div');
    divobject.className = 'List_utc d-flex';
    divobject.style.flexWrap = 'wrap'; // Corrected property name
    divobject.style.maxWidth = '450px';

    var utc_styles = {
        border: '1px solid grey',
        color: 'white',
        fontSize: '20px',
        textAlign: 'center',
        margin: '2px',
        borderRadius: '5px',
         padding: '2px 3px'
    };


    for (var index = 0; index < arr[6].length; index++) {
        var x = document.createElement('div');
        Object.assign(x.style, utc_styles); // Apply styles to x.style, not x itself
        x.innerHTML += `<span>${arr[6][index]}</span>`;
        divobject.appendChild(x);
    }

    modalContent.innerHTML += objectInfoHTML
    modalContent.appendChild(divobject)
    modalElement.style.display = `block`
}



