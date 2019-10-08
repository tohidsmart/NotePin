//This JS file provide functionalities to work with Google map and browser cookies

function getMap(mapPlaceHolder) {
    map = new google.maps.Map(mapPlaceHolder, {
        zoom: 10
    });
}

function addNoteOnMap(note, window, InfoWindowService) {

    var infowindow = new google.maps.InfoWindow;
    var noteContent = '<h6>' + note.user + '\'s note</h6>' +
        '<hr/><p>' + note.content + ' </p>';
    infowindow.setContent(noteContent);
    var pos = {
        lat: note.latitude,
        lng: note.longitude
    }
    infowindow.setPosition(pos);
    infowindow.open(window.map);
    InfoWindowService.addWindow(infowindow)
    window.map.setCenter(pos);
}

function closeNotes(InfoWindowService) {
    for (var i = 0; i < InfoWindowService.getWindows().length; i++) {

        InfoWindowService.getWindows()[i].close();
    }
}

function renderNotes(notes, window, InfoWindowService) {


    InfoWindowService.InfoWindows = [];
    for (var i = 0; i < notes.length; i++) {
        addNoteOnMap(notes[i], window, InfoWindowService);
    }
}

function getLocation(lat, lng) {
    var geocoder = new google.maps.Geocoder;
    return new Promise(function (resolve, reject) {
        var latlng = { lat: lat, lng: lng };
        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                resolve(results);
            } else {
                reject(status);
            }
        });
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}


function getCookie() {
    var allcookies = document.cookie.split(';');
    var usernamekey = "username="
    var usernamevalue;
    for (var i = 0; i < allcookies.length; i++) {
        var cookie = allcookies[i];
        while (cookie.charAt(0) == ' ') cookie = cookie.substring(1, c.length);
        if (cookie.indexOf(usernamekey) == 0)
            usernamevalue = cookie.substring(usernamekey.length, cookie.length);
        return usernamevalue;
    }
}

function setCookie() {
    var name = prompt("Enter your name");
    var usernamekey = "username=";
    document.cookie = usernamekey + name;
}