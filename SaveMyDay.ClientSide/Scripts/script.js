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
          .when('/map',{
              templateUrl: 'map.html',
              controller: 'mapCtrl'
          });

});

app.controller('homeCtrl', function ($scope) {

});

app.controller('loginCtrl', function ($scope) {

});

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
