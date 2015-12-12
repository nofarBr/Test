var app = angular.module('single-page-app', ['ngRoute']);

app.config(function ($routeProvider) {
    
      $routeProvider
          .when('/',{
              templateUrl: 'home.html',
              controller: 'homeCtrl'
          })
          .when('/home', {
              templateUrl: 'home.html',
              controller: 'homeCtrl'
          })
          .when('/login',{
              templateUrl: 'login.html',
              controller: 'loginCtrl'
          })
          .when('/calendar', {
              templateUrl: 'calendar.html',
              controller: 'calendarCtrl'
          })
          .when('/map',{
              templateUrl: 'map.html',
              controller: 'mapCtrl'
          });

});

app.controller('homeCtrl', function ($scope) {

});

app.controller('loginCtrl', function ($scope) {

});

app.controller('arrangementCtrl', function ($scope) {
    $scope.arrangements = {
        types: [
            {
                type: 'Bank',
                branches: [{ name: 'Leumi', city: 'TLV' }, { name: 'Leumi', city: 'TLV2' }, { name: 'Hapoalim', city: 'TLV' }]
            },
            {
                type: 'Hair Dresser',
                branches: [{ name: 'Sami', city: 'TLV' }, { name: 'David', city: 'TLV2' }, { name: 'Eli', city: 'TLV' }]
            },
            {
                type: 'Post',
                branches: [{ name: 'Herzel', city: 'TLV' }, { name: 'Candy', city: 'TLV2' }, { name: 'Post', city: 'TLV' }]
            }
        ]
    }

    $scope.arrangementsNum = 0;

    $scope.addAppointment = function (parent) {
        var appointmentDiv = document.createElement('div');
        appointmentDiv.id = 'appointment-div-' + $scope.arrangementsNum;
        var typesCombo = document.createElement('select');
        typesCombo.id = 'appointment-types-combo';
        typesCombo.index = $scope.arrangementsNum++

        for (i = 0; i < $scope.arrangements.types.length; i++) {
            var typesComboOption = document.createElement('option');
            typesComboOption.id = i;
            typesComboOption.value = $scope.arrangements.types[i].type;
            typesComboOption.innerHTML = $scope.arrangements.types[i].type;
            typesComboOption.subMenu = $scope.arrangements.types[i].branches;
            typesCombo.appendChild(typesComboOption);
        }

        typesCombo.onchange = function () {
            if (document.getElementById('appointment-sub-types-combo')) {
                document.getElementById('appointment-sub-types-combo' + typesCombo.index).remove();
            }

            var subTypesCombo = document.createElement('select');
            subTypesCombo.id = 'appointment-sub-types-combo' + typesCombo.index;

            var selectedSubMenu = typesCombo.selectedOptions[0].subMenu;

            for (i = 0; i < selectedSubMenu.length; i++) {
                var typesComboOption = document.createElement('option');
                typesComboOption.id = i;
                typesComboOption.value = selectedSubMenu[i].name + ", " + selectedSubMenu[i].city;
                typesComboOption.innerHTML = selectedSubMenu[i].name + ", " + selectedSubMenu[i].city;
                subTypesCombo.appendChild(typesComboOption);
            }

            appointmentDiv.appendChild(subTypesCombo);
        }

        appointmentDiv.appendChild(typesCombo);

        $('#arrangements').append(appointmentDiv);
    }
})

app.controller('calendarCtrl', function ($scope) {
    $scope.$on('$viewContentLoaded', function () {
        $('#calendar').fullCalendar({
            customButtons: {
                addEvent: {
                    text: 'Add Event',
                    click: function() {
                        select();
                    }
                }
            },
            header: {
                left: 'prev,next today',
                center: 'title',
                right: '' //agendaDay month,agendaWeek,
            },
            width: ($(window).width() * 45) / 100,
            defaultView: 'agendaDay',
            selectable: true,
            selectHelper: true,
            select: function (start, end) {
                var title = prompt('Event Title:');
                var location = prompt('Event Location:');
                var eventData;
                if (title) {
                    eventData = {
                        title: title,
                        location: location,
                        start: start,
                        end: end
                    };
                    $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
                }
                $('#calendar').fullCalendar('unselect');
            },
            eventClick: function (event, element) {
                var retVal = confirm("Do you want to delete? (click cancel to update)");
                if (retVal == true) {
                    $('#calendar').fullCalendar('removeEvents', event._id);
                }
                else {
                    var title = prompt('Event Title:', event.title);
                    var location = prompt('Event Location:', event.location);
                    event.title = title;
                    event.location = location;
                    $('#calendar').fullCalendar('updateEvent', event);
                }
            },
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            events: [
                {
                    title: 'All Day Event',
                    start: '2015-12-01'
                },
                {
                    title: 'Long Event',
                    start: '2015-12-07',
                    end: '2015-12-10'
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: '2015-12-09T16:00:00'
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: '2015-12-16T16:00:00'
                },
                {
                    title: 'Conference',
                    start: '2015-12-11',
                    end: '2015-12-13'
                },
                {
                    title: 'Meeting',
                    start: '2015-12-12T10:30:00',
                    end: '2015-12-12T12:30:00'
                },
                {
                    title: 'Lunch',
                    start: '2015-12-12T12:00:00'
                },
                {
                    title: 'Meeting',
                    start: '2015-12-12T14:30:00'
                },
                {
                    title: 'Happy Hour',
                    start: '2015-12-12T17:30:00'
                },
                {
                    title: 'Dinner',
                    start: '2015-12-12T20:00:00'
                },
                {
                    title: 'Birthday Party',
                    start: '2015-12-13T07:00:00'
                },
                {
                    title: 'Click for Google',
                    url: 'http://google.com/',
                    start: '2015-12-28'
                }
            ]
        });
    });
})

app.controller('mapCtrl', function ($scope) {

    // Code that execute when the page is loaded
    $scope.$on('$viewContentLoaded', function () {

        // Initialize the map
        initializeMap();

        // Define the waypoints of each path/route
        var places1 = ['Namer 10 ashkelon', 'Lachish 4 ashkelon', 'Sivan 2 Ashkelon', 'Megido 6 ashkelon'];
        var places2 = ['Hagvura 5 ashkelon', 'Hanasi 50 ashkelon'];
        var places3 = ['Lachish 4 ashkelon', 'Tsipori 1 ashkelon', 'Sinai 2 ashkelon', 'Tzfanya 8 ashkelon'];

        // Create (and show) the routes in the map
        addNewRoute(places1, '#808080', 'false');
        addNewRoute(places2, '#808080', 'false');
        addNewRoute(places3, '#3399FF', 'true');
        
        // Hide the routes
        hideAllRoutes();
    });

    $scope.ShowPath = function (path_id) {
        /*
        for (var i = 1; i <= 3; i++) {
            if (i == path_id) {
                $scope['Path' + i] = true;
            } else {
                $scope['Path' + i] = false;
                modifyRoute(i - 1, '#808080', 'false');
            }
        }

        modifyRoute(path_id - 1, '#3399FF', 'true');
        */

        // User chose path number 1
        if (path_id == 1)
        {
            // Open information for the chosen path and close the others
            $scope['Path1'] = true;
            $scope['Path2'] = false;
            $scope['Path3'] = false;

            // Highlight the chosen path
            modifyRoute(1, '#A8CFFF', 'false');
            modifyRoute(2, '#62FDCE', 'false');
            modifyRoute(0, '#0000FF', 'true');
        }
        // User chose path number 2
        else if (path_id == 2)
        {
            // Open information for the chosen path and close the others
            $scope['Path1'] = false;
            $scope['Path2'] = true;
            $scope['Path3'] = false;

            // Highlight the chosen path
            modifyRoute(0, '#62FDCE', 'false');
            modifyRoute(2, '#A8CFFF', 'false');
            modifyRoute(1, '#0000FF', 'true');
        }
        // User chose path number 3
        else if (path_id == 3)
        {
            // Open information for the chosen path and close the others
            $scope['Path1'] = false;
            $scope['Path2'] = false;
            $scope['Path3'] = true;

            // Highlight the chosen path
            modifyRoute(0, '#62FDCE', 'false');
            modifyRoute(1, '#A8CFFF', 'false');
            modifyRoute(2, '#0000FF', 'true');
        }
    }
});
