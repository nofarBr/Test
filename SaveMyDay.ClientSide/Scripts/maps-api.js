var map;
var geocoder;
var markers = [];
var directionsDisplays = [];
var markersPerPath = [];
var totalDistances = [];

function initializeMap() {
    markers = [];
    directionsDisplays = [];
    markersPerPath = [];
    totalDistances = [];
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


/*function addLabelMarker(path_id, address, text) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var textmarker = new google.maps.Marker({
                position: results[0].geometry.location,
                map: null,
                title: text
            });
            var infowindow = new google.maps.InfoWindow({
                content: text,
                maxWidth: 150
            });
            textmarker.addListener('click', function () {
                infowindow.open(map, textmarker);
            });
            var textmarker = new RichMarker({
                map: null,
                position: results[0].geometry.location,
                draggable: false,
                flat: true,
                anchor: RichMarkerPosition.CUSTOM,
                content: '<div class="map-labels">' + text + '</div>'
            });
            markersPerPath[path_id].push(textmarker);
        } else {
            //alert(address);
            //alert("Test-Geocode was not successful for the following reason: " + status);
        }
    });
}*/

function hideAllMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
}

function hideMarkersByPath(path_id) {
    for (var i = 0; i < markersPerPath[path_id].length; i++) {
        markersPerPath[path_id][i].setMap(null);
    }
}

function showAllMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

function showMarkersByPath(path_id) {
    for (var i = 0; i < markersPerPath[path_id].length; i++) {
        markersPerPath[path_id][i].setMap(map);
    }
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

function modifyRoute(path_id, color, set_markers) {

    if (path_id < directionsDisplays.length)
    {
        var supress_markers;
        var strokeWeight;

        if (set_markers == 'true') {
            supress_markers = false;
            strokeWeight = 6;
            showMarkersByPath(path_id);
        } else {
            supress_markers = true;
            strokeWeight = 4;
            hideMarkersByPath(path_id);
        }

        directionsDisplays[path_id].setOptions({
            suppressMarkers: supress_markers,
            polylineOptions: {
                strokeColor: color,
                strokeWeight: strokeWeight,
                strokeOpacity: 0.7
            }
        });

        directionsDisplays[path_id].setMap(map);
    }
}

function addNewRoute(path_id, places, labels, travel_type) {
    
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

    var directionsDisplay = new google.maps.DirectionsRenderer();
    directionsDisplay.setMap(null);
    var directionsService = new google.maps.DirectionsService;

    var travelModeType;

    if (travel_type == 0) {
        travelModeType = google.maps.TravelMode.DRIVING;
    } else {
        travelModeType = google.maps.TravelMode.WALKING;
    }

    directionsService.route({
        origin: start,
        destination: end,
        waypoints: waypts,
        optimizeWaypoints: false,
        travelMode: travelModeType
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            totalDistances.push(calculateTotalDistance(directionsDisplay));
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
    
    directionsDisplays.push(directionsDisplay);

    var currPathMarkers = [];
    markersPerPath.push(currPathMarkers);

    /*for (var i = 0; i < labels.length; i++) {
        addLabelMarker(path_id, places[i], labels[i]);
    }*/
}

function calculateTotalDistance(directionResult) {
    var result = directionResult.getDirections();
    var total = 0;
    var myroute = result.routes[0];
    for (var i = 0; i < myroute.legs.length; i++) {
        total += myroute.legs[i].distance.value;
    }
    total = total / 1000;
    return (total);
}

function getTotalDistance(path_id) {
    return (totalDistances[path_id]);
}


