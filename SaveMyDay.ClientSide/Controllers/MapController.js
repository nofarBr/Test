
app.controller('mapCtrl', function ($scope, $rootScope, $http) {

    // Code that execute when the page is loaded
    $scope.$on('$viewContentLoaded', function () {

        // Initialize the map
        initializeMap();

        $scope.InitializeMapByJSon();

    });

    $scope.InitializeMapByJSon = function () {
        //get the path list from server.
        //could be undefined!
        var pathsList = $rootScope.pathsList;

        // Define the labels for each company type
        var arrCompanyTypeLabels = ['תור לרופא', 'תור לבנק', 'תור לדואר'];
        var arrCompanySubTypeLabels = ['רופא ילדים', 'רופא עור', 'רופא משפחה', 'בנק דיסקונט', 'בנק מזרחי', 'בנק לאומי', 'איסוף חבילות', 'משלוח חבילות', 'תשלומים ומשלוח מכתבים']
        
        var arrAlgorithmPaths = angular.fromJson(pathsList);
        $scope.AllPaths = arrAlgorithmPaths;
        var paths = [];

        // Running over all the given paths
        for (var i = 0; i < arrAlgorithmPaths.length; i++) {

            var locations = [];
            var labels = [];
            var path_details = [];

            var indexAppointment = 0;
            var indexConstraint = 0;
            //var newLocationsArray = [];
            //var newLabelsArray = [];
            var arrCombinedList = [];

            while (indexAppointment < arrAlgorithmPaths[i].Appointments.length &&
                   indexConstraint < arrAlgorithmPaths[i].Constraints.length)
            {
                var timeAppointment = new Date(arrAlgorithmPaths[i].Appointments[indexAppointment].Time);
                var timeConstraint = new Date(arrAlgorithmPaths[i].Constraints[indexConstraint].Start);

                if (timeAppointment < timeConstraint)
                {
                    arrCombinedList.push({
                        type: "appointment",
                        index: indexAppointment
                    });
                    //newLocationsArray.push(arrAlgorithmPaths[i].Appointments[indexAppointment].Company.Location);
                    //newLabelsArray.push(arrCompanyTypeLabels[arrAlgorithmPaths[i].Appointments[indexAppointment].Company.Type]);
                    indexAppointment++;
                }
                else
                {
                    arrCombinedList.push({
                        type: "constraint",
                        index: indexConstraint
                    });
                    //newLocationsArray.push(arrAlgorithmPaths[i].Constraints[indexConstraint].Location);
                    //newLabelsArray.push(arrAlgorithmPaths[i].Constraints[indexConstraint].Title);
                    indexConstraint++;
                }
            }

            if (indexAppointment < arrAlgorithmPaths[i].Appointments.length)
            {
                while (indexAppointment < arrAlgorithmPaths[i].Appointments.length)
                {
                    arrCombinedList.push({
                        type: "appointment",
                        index: indexAppointment
                    });
                    //newLocationsArray.push(arrAlgorithmPaths[i].Appointments[indexAppointment].Company.Location);
                    //newLabelsArray.push(arrCompanyTypeLabels[arrAlgorithmPaths[i].Appointments[indexAppointment].Company.Type]);
                    indexAppointment++;
                }
            }
            else
            {
                while (indexConstraint < arrAlgorithmPaths[i].Constraints.length)
                {
                    arrCombinedList.push({
                        type: "constraint",
                        index: indexConstraint
                    });
                    //newLocationsArray.push(arrAlgorithmPaths[i].Constraints[indexConstraint].Location);
                    //newLabelsArray.push(arrAlgorithmPaths[i].Constraints[indexConstraint].Title);
                    indexConstraint++;
                }
            }

            //alert("newLocationsArray = " + newLocationsArray);
            //alert("newLabelsArray = " + newLabelsArray);

            for (var k = 0; k < arrCombinedList.length; k++)
            {
                var itemIndex = arrCombinedList[k].index;
                if (arrCombinedList[k].type == 'appointment')
                {
                    // Appointment
                    locations.push(arrAlgorithmPaths[i].Appointments[itemIndex].Company.Location);
                    labels.push(arrCompanySubTypeLabels[arrAlgorithmPaths[i].Appointments[itemIndex].Company.SubType]);
                    var date = new Date(arrAlgorithmPaths[i].Appointments[itemIndex].Time);
                    path_details.push({
                        icon: arrAlgorithmPaths[i].Appointments[itemIndex].Company.Type,
                        sub_type: arrCompanySubTypeLabels[arrAlgorithmPaths[i].Appointments[itemIndex].Company.SubType],
                        time: date.toISOString().split('T')[1].replace(/^(\d{2}:\d{2}).*/, "$1"),
                        desc: arrAlgorithmPaths[i].Appointments[itemIndex].Remark,
                        address: arrAlgorithmPaths[i].Appointments[itemIndex].Company.Location
                    });
                }
                else
                {
                    // Constraint
                    locations.push(arrAlgorithmPaths[i].Constraints[itemIndex].Location);
                    labels.push(arrAlgorithmPaths[i].Constraints[itemIndex].Title);
                    var date = new Date(arrAlgorithmPaths[i].Constraints[itemIndex].Start);
                    path_details.push({
                        icon: 3,
                        sub_type: arrAlgorithmPaths[i].Constraints[itemIndex].Title,
                        time: date.toISOString().split('T')[1].replace(/^(\d{2}:\d{2}).*/, "$1"),
                        desc: "",
                        address: arrAlgorithmPaths[i].Constraints[itemIndex].Location
                    });
                }
            }

            // Add the current path as route in the map
            addNewRoute(i, locations, labels, arrAlgorithmPaths[i].type);
            paths.push({ id: i + 1, appointments: path_details });
        }

        $scope.paths = paths;
    }

    $scope.ChoosePath = function () {
        var url = 'http://localhost:53528/api/Path/PostAppointment';
        var data = angular.toJson($scope.AllPaths[$scope.chosen_path - 1])
        
        $http.post(url, data)
        .success(function (data) {
            if (data.success) {
                $('#resultModal').on('hidden.bs.modal', function (e) {
                    var choosePathBtnGroup = document.getElementById('ChoosePathBtnGroup');
                    var exportPathBtnGroup = document.getElementById('ExportPathBtnGroup');
                    var pathsPanel = document.getElementById('pathsPanel');

                    choosePathBtnGroup.style.display = 'none';
                    choosePathBtnGroup.style.visibility = 'hidden';
                    exportPathBtnGroup.style.display = 'block';
                    exportPathBtnGroup.style.visibility = 'visible';

                    pathsPanel.style.pointerEvents = 'none';
                    pathsPanel.style.opacity = 0.6;
                });

                $('#resultModal').modal({ backdrop: 'static', keyboard: false });
            } else {
                $('#failedResultModal').on('hidden.bs.modal', function (e) {
                });

                $('#failedResultModal').modal({ backdrop: 'static', keyboard: false });
            }
        })
        .error(function (data, status, header, config) {
            $('#failedResultModal').on('hidden.bs.modal', function (e) {
            });

            $('#failedResultModal').modal({ backdrop: 'static', keyboard: false });
        });
        
    }

    $scope.ShowPath = function (path_id) {

        $scope.chosen_path = path_id;

        // User chose path number 1
        if (path_id == 1) {

            // Highlight the chosen path
            modifyRoute(1, '#A8CFFF', 'false');
            modifyRoute(2, '#62FDCE', 'false');
            modifyRoute(0, '#0000FF', 'true');
        }
        // User chose path number 2
        else if (path_id == 2) {

            // Highlight the chosen path
            modifyRoute(0, '#62FDCE', 'false');
            modifyRoute(2, '#A8CFFF', 'false');
            modifyRoute(1, '#0000FF', 'true');
        }
        // User chose path number 3
        else if (path_id == 3) {

            // Highlight the chosen path
            modifyRoute(0, '#62FDCE', 'false');
            modifyRoute(1, '#A8CFFF', 'false');
            modifyRoute(2, '#0000FF', 'true');
        }
    }

    $scope.CreateNewDay = function () {
        window.location.href = '#/calendar';
    }

});

app.directive('myPostRepeatDirective', function () {
    return function (scope, element, attrs) {

        [].slice.call(document.querySelectorAll('.tabs')).forEach(function (el) {
            new CBPFWTabs(el);
        });
    };
});


