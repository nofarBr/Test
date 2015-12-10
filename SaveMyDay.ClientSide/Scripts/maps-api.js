var map;
var geocoder;
var markers = [];
var directionsDisplays = [];

function initializeMap() {
    geocoder = new google.maps.Geocoder();
    var mapOptions = {
        center: new google.maps.LatLng(31.382911, 35.030984),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        disableDefaultUI: true,
        scrollwheel: true,
        draggable: true,
        disableDoubleClickZoom: true
    };
    map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
}

/*function addMarker(x, y, label) {
    var latlng = new google.maps.LatLng(x, y);
    var marker = new MarkerWithLabel({
        position: latlng,
        draggable: false,
        map: map,
        labelContent: label,
        labelAnchor: new google.maps.Point(40, 75),
        labelClass: "map-labels", // the CSS class for the label
        labelStyle: { opacity: 0.85 }
    });
    markers.push(marker);
}*/

function addLabelMarker(address, text) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var textmarker = new RichMarker({
                map: map,
                position: results[0].geometry.location,
                draggable: false,
                flat: true,
                anchor: RichMarkerPosition.CUSTOM,
                content: '<div class="map-labels">' + text + '</div>'
            });
            markers.push(textmarker);
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

function removeAllMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

function hideAllRoutes() {
    for (var i = 0; i < directionsDisplays.length; i++) {
        directionsDisplays[i].setMap(null);
    }
}

/*function showAllRoutes() {
    for (var i = 0; i < directionsDisplays.length; i++) {
        directionsDisplays[i].setMap(map);
    }
}*/

/*function showRoute(route_id) {
    console.log("show route number " + route_id);
    directionsDisplays[route_id].setMap(map);
}*/

function modifyRoute(route_id, color, set_markers) {

    var supress_markers;
    var strokeWeight;

    if (set_markers == 'true') {
        supress_markers = false;
        strokeWeight = 8;
    } else {
        supress_markers = true;
        strokeWeight = 5;
    }

    directionsDisplays[route_id].setOptions({
        suppressMarkers: supress_markers,
        polylineOptions: {
            strokeColor: color,
            strokeWeight: strokeWeight,
            strokeOpacity: 0.7
        }
    });

    directionsDisplays[route_id].setMap(map);
}

function addNewRoute(places, color, set_markers) {
    
    var start = places[0];
    var end = places[places.length - 1];
    
    var waypts = [];
    if (places.length > 2) {
        for (var i = 1; i < places.length - 1; i++) {
            waypts.push({
                location: places[i],
                stopover: true
            });
        }
    }
    
    var directionsDisplay = new google.maps.DirectionsRenderer({
        polylineOptions: {
            strokeColor: color,
            strokeWeight: 5,
            strokeOpacity: 0.7
        }
    });

    var supress_markers;
    if (set_markers == 'true')
    {
        supress_markers = false;
    }
    else
    {
        supress_markers = true;
    }
    directionsDisplay.setMap(map);
    directionsDisplay.setOptions({ suppressMarkers: supress_markers });
    var directionsService = new google.maps.DirectionsService;

    directionsService.route({
        origin: start,
        destination: end,
        waypoints: waypts,
        optimizeWaypoints: false,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
    
    directionsDisplays.push(directionsDisplay);
}

/*function addRoute(place1, place2) {

    //directionsService = new google.maps.DirectionsService();
    //directionsDisplay = new google.maps.DirectionsRenderer({ polylineOptions: { strokeColor: "#8b0013", strokeWeight: 4 } });
    var directionsDisplay = new google.maps.DirectionsRenderer;
    directionsDisplay.setMap(map);
    directionsDisplay.setOptions({ suppressMarkers: false });

    var directionsService = new google.maps.DirectionsService;
    //directionsDisplay = new google.maps.DirectionsRenderer;
    //directionsDisplay.setMap(map);
    directionsService.route({
        origin: place1,
        destination: place2,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
    directionsDisplays.push(directionsDisplay);
}*/

/*function removeAllRoutes() {
    for (var i = 0; i < directionsDisplays.length; i++) {
        directionsDisplays[i].set('directions', null);
    }
    directionsDisplays = [];
    //directionsDisplay.set('directions', null);
}*/

/*function addLongRoute() {
    var waypts = [];

    var start = "Ashkelon";
    var end = "Tel Aviv";


    waypts.push({
        location: "Rishon Letziyon",
        stopover: true
    });

    waypts.push({
        location: "Ashdod",
        stopover: true
    });

    waypts.push({
        location: "Gan Yavne",
        stopover: true
    });

 
    // Normal route
    //directionsDisplay = new google.maps.DirectionsRenderer;

    // Custom color route
    var directionsDisplay = new google.maps.DirectionsRenderer({ polylineOptions: { strokeColor: "#8b0013", strokeWeight: 5, strokeOpacity: 0.5 } });


    directionsDisplay.setMap(map);
    directionsDisplay.setOptions({ suppressMarkers: false });

    var directionsService = new google.maps.DirectionsService;
    directionsService.route({
        origin: start,
        destination: end,
        waypoints: waypts,
        optimizeWaypoints: false,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
    directionsDisplays.push(directionsDisplay);
}*/