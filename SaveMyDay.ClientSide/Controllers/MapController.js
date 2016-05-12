﻿
app.controller('mapCtrl', function ($scope, $rootScope, $http) {

    // Code that execute when the page is loaded
    $scope.$on('$viewContentLoaded', function () {

        // Initialize the map
        initializeMap();

        // Define the waypoints of each path/route
        var places1 = ['Namer 10 ashkelon', 'Lachish 4 ashkelon', 'Sivan 2 Ashkelon', 'Megido 6 ashkelon'];
        var places2 = ['Hagvura 5 ashkelon', 'Hanasi 50 ashkelon'];
        var places3 = ['Lachish 4 ashkelon', 'Tsipori 1 ashkelon', 'Sinai 2 ashkelon', 'Ort 2 ashkelon'];

        var labels1 = ['A1', 'A2', 'A3', 'A4'];
        var labels2 = ['B1', 'B2'];
        var labels3 = ['C1', 'C2', 'C3', 'C4'];

        $scope.InitializeMapByJSon();

    });

    $scope.TryRest = function () {
        $http.get('http://cors.io/?u=http://rest-service.guides.spring.io/greeting')
            .success(function (response) {
                alert("id: " + response.id + "\n" + "content: " + response.content);
            }).error(function (response, status, headers, config) {
                alert('Error while fetching data from the web api');
            });
    }

    $scope.InitializeMapByJSon = function () {
        //get the path list from server.
        //could be undefined!
        var pathsList = $rootScope.pathsList;
        if (pathsList == null)
        {
            // Get the paths data in json format
            var jsonPath = "[{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Namer 10 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-17T09:30:31.0095157+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"רופא עור - ד''ר משעל\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Lachish 4 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T10:30:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"רופא עיניים - ד''ר בלומנפלד\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Sivan 2 Ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T14:10:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"בנק מזרחי - סניף אשקלון\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Megido 6 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":2,\"UrlForApi\":null},\"Time\":\"2015-12-21T19:30:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"דואר - סניף רמת גן\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0},{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Hagvura 5 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-17T18:45:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"רופא עיניים - ד''ר סימנובסקי\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Hanasi 50 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T19:30:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"בנק מזרחי - סניף תל אביב\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0},{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Lachish 4 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":2,\"UrlForApi\":null},\"Time\":\"2015-12-17T09:15:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"דואר - סניף אשקלון\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Tsipori 1 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T10:10:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"בנק מזרחי - סניף אשקלון\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Sinai 2 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T13:30:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"רופא עיניים - ד''ר כהן\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Ort 2 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T18:50:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"רופא עור - ד''ר דומסקי\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0}]";

            // Define the labels for each company type
            var arrCompanyTypeLabels = ['תור לרופא', 'תור לבנק', 'תור לדואר'];

            // Running over all the given paths
            var arrPaths = angular.fromJson(jsonPath);
            var paths = [];

            for (var i = 0; i < arrPaths.length; i++) {

                var locations = [];
                var labels = [];
                var path_details = [];

                // Running over all the appointments for the current path
                for (var j = 0; j < arrPaths[i].Appointments.length; j++) {
                    locations.push(arrPaths[i].Appointments[j].Company.Location.Address.Street);
                    labels.push(arrCompanyTypeLabels[arrPaths[i].Appointments[j].Company.Type]);
                    var date = new Date(arrPaths[i].Appointments[j].Time);
                    path_details.push({
                        icon: arrPaths[i].Appointments[j].Company.Type,
                        time: date.getHours() + ":" + date.getMinutes(),
                        desc: arrPaths[i].Appointments[j].Remark,
                        address: "רחוב ז'בוטינסקי 106, רמת גן"
                    });
                }

                // Add the current path as route in the map
                addNewRoute(i, locations, labels, "car");
                paths.push({ id: i + 1, appointments: path_details });
            }

            $scope.paths = paths;
        }
        else
        {
            var arrAlgorithmPaths = angular.fromJson(pathsList);
            var paths = [];

            // Running over all the given paths
            for (var i = 0; i < arrAlgorithmPaths.length; i++) {

                var locations = [];
                var labels = [];
                var path_details = [];
                
                // Running over all the appointments for the current path
                for (var j = 0; j < arrAlgorithmPaths[i].Appointments.length; j++) {
                    locations.push(arrAlgorithmPaths[i].Appointments[j].Company.Location.Address.Street);
                    labels.push(arrCompanyTypeLabels[arrAlgorithmPaths[i].Appointments[j].Company.Type]);
                    var date = new Date(arrAlgorithmPaths[i].Appointments[j].Time);
                    path_details.push({
                        icon: arrAlgorithmPaths[i].Appointments[j].Company.Type,
                        time: date.getHours() + ":" + date.getMinutes(),
                        desc: arrAlgorithmPaths[i].Appointments[j].Remark,
                        address: arrAlgorithmPaths[i].Appointments[j].Company.Location.Address.Street
                    });
                }

                // Add the current path as route in the map
                addNewRoute(i, locations, labels, "car");
                paths.push({ id: i + 1, appointments: path_details });
            }

            $scope.paths = paths;
        }

        //////////////////////////
        //                      //
        //    TEST TEST TEST    //
        //                      //
        //////////////////////////

        /*
        // Get the paths data in json format
        var jsonPath = "[{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Namer 10 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-17T09:30:31.0095157+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Lachish 4 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T10:30:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Sivan 2 Ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T14:10:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Megido 6 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":2,\"UrlForApi\":null},\"Time\":\"2015-12-21T19:30:31.0165161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0},{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Hagvura 5 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-17T18:45:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Hanasi 50 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T19:30:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0},{\"Id\":\"000000000000000000000000\",\"User\":{\"Id\":\"000000000000000000000000\",\"Name\":\"Ori\",\"Password\":\"Aa123456\"},\"Appointments\":[{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Lachish 4 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":2,\"UrlForApi\":null},\"Time\":\"2015-12-17T09:15:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Tsipori 1 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":1,\"UrlForApi\":null},\"Time\":\"2015-12-21T10:10:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Sinai 2 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T13:30:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"},{\"Id\":\"000000000000000000000000\",\"Company\":{\"Id\":\"000000000000000000000000\",\"Location\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"Ort 2 ashkelon\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"6\"}},\"Type\":0,\"UrlForApi\":null},\"Time\":\"2015-12-21T18:50:31.0175161+02:00\",\"LastModified\":\"0001-01-01T00:00:00\",\"Remark\":\"notes notes notes\"}],\"Constraints\":[{\"Code\":1,\"StartLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street1\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"10\"}},\"EndLocation\":{\"X\":0.0,\"Y\":0.0,\"Address\":{\"Street\":\"street2\",\"City\":{\"Code\":1,\"Decription\":\"wooo city\"},\"HouseNumber\":\"16\"}},\"StartTime\":\"2015-12-18T01:51:31.0175161+02:00\",\"EndTime\":\"2015-12-18T03:51:31.0175161+02:00\"}],\"type\":0}]";
        
        // Define the labels for each company type
        var arrCompanyTypeLabels = ['תור לרופא', 'תור לבנק', 'תור לדואר'];

        // Running over all the given paths
        var arrPaths = angular.fromJson(jsonPath);
        var paths = [];

        for (var i = 0; i < arrPaths.length; i++) {

            var locations = [];
            var labels = [];
            var path_details = [];

            // Running over all the appointments for the current path
            for (var j = 0; j < arrPaths[i].Appointments.length; j++) {
                locations.push(arrPaths[i].Appointments[j].Company.Location.Address.Street);
                labels.push(arrCompanyTypeLabels[arrPaths[i].Appointments[j].Company.Type]);
                var date = new Date(arrPaths[i].Appointments[j].Time);
                path_details.push({
                    icon: arrPaths[i].Appointments[j].Company.Type,
                    time: date.getHours() + ":" + date.getMinutes(),
                    desc: arrCompanyTypeLabels[arrPaths[i].Appointments[j].Company.Type],
                    address: "רחוב ז'בוטינסקי 106, רמת גן"
                });
            }

            // Add the current path as route in the map
            addNewRoute(i, locations, labels, "car");
            paths.push({ id: i + 1, appointments: path_details });
        }

        $scope.paths = paths;*/
    }

    $scope.ChoosePath = function () {
        var url = 'http://localhost:53528/api/Path/PostAppointment';
        var data = {
            'pathId': $scope.chosen_path
        };
        
        $http.post(url, data)
        .success(function (data) {
            alert('The path chosen successfully!');
            //$rootScope.pathsList = data.paths;
            //$location.path("/map");
        })
        .error(function (data, status, header, config) {
            alert('data = ' + data);
            alert('status = ' + status);
            alert('header = ' + header);
            alert('config = ' + config);
        });
        
    }

    $scope.ShowPath = function (path_id) {

        $scope.chosen_path = path_id;

        // User chose path number 1
        if (path_id == 1) {
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
        else if (path_id == 2) {
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
        else if (path_id == 3) {
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

app.directive('myPostRepeatDirective', function () {
    return function (scope, element, attrs) {

        [].slice.call(document.querySelectorAll('.tabs')).forEach(function (el) {
            new CBPFWTabs(el);
        });
    };
});


