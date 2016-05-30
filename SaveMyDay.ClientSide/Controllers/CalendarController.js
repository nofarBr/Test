
app.controller('arrangementCtrl', function ($scope) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', 'http://localhost:60799/api/Company', true);

    xmlhttp.onreadystatechange = function (data) {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            var dbArrangements = JSON.parse(xmlhttp.response);
            var dictionary = { Banks: "בנק", MedicalClinic: "קופת חולים", PostOffice: "דואר" }
            $scope.dictionary = { "בנק" : "Banks", "קופת חולים" : "MedicalClinic", "דואר" : "PostOffice" };

            var dictionary2 = {
                PackagesCollection: "איסוף חבילות", FamilyDoctor: "רופא משפחה", ChildsDoctor: "רופא ילדים", BankMizrahi: "בנק מזרחי",
                PaymentsAndLettersShipping: "תשלומים ושליחת מכתבים", PackagesShipping: "שליחת חבילות", SkinDoctor: "רופא עור",
                BankLeumi: "בנק לאומי", BankDiscount: "בנק דיסקונט"
            }
            $scope.subDictionary = {
                "איסוף חבילות": "PackagesCollection", "רופא משפחה": "FamilyDoctor", "רופא ילדים": "ChildsDoctor", "בנק מזרחי": "BankMizrahi",
                "תשלומים ושליחת מכתבים": "PaymentsAndLettersShipping", "שליחת חבילות": "PackagesShipping", "רופא עור": "SkinDoctor",
                "בנק לאומי": "BankLeumi", "בנק דיסקונט": "BankDiscount"
            };

            $scope.arrangements = {
                types: []
            };

            for (var i = 0; i < dbArrangements.length; i++) {
                dbArrangements[i].SubType = dictionary2[dbArrangements[i].SubType];
            }

            var arrangementsByType = []
            for (var i = 0; i < dbArrangements.length; i++) {
                if (!arrangementsByType[dbArrangements[i].companyType]) {
                    arrangementsByType[dbArrangements[i].companyType] = [];
                }

                arrangementsByType[dbArrangements[i].companyType].push(dbArrangements[i]);
            }

            for (var i = 0; i < Object.keys(arrangementsByType).length; i++) {
                $scope.arrangements.types.push({ CompanyType: dictionary[Object.keys(arrangementsByType)[i]], CompanyNames: arrangementsByType[Object.keys(arrangementsByType)[i]] });
            }
        }

    }

    xmlhttp.send();

    $scope.arrangementsNum = 0;

    $scope.addAppointment = function (parent) {
        if (document.getElementsByName("appointment-div").length >= 12) {
            document.getElementById('errorMsg').value = "ניתן להזין עד 12 תורים בלבד";
            document.getElementById('errorMsg').innerHTML = "ניתן להזין עד 12 תורים בלבד";
            $('#errorModal').modal();
        } else {
            var appointmentDiv = document.createElement('div');
            appointmentDiv.id = 'appointment-div-' + $scope.arrangementsNum;
            appointmentDiv.tagName = 'appointment-div';
            appointmentDiv.setAttribute('name', 'appointment-div');
            appointmentDiv.className = "combo-box-margins";

            var typesCombo = document.createElement('select');
            typesCombo.id = 'appointment-types-combo';
            typesCombo.index = $scope.arrangementsNum++
            typesCombo.className = "round-buttons-calendar-view";

            for (i = 0; i < $scope.arrangements.types.length; i++) {
                var typesComboOption = document.createElement('option');
                typesComboOption.id = i;
                typesComboOption.value = $scope.arrangements.types[i].CompanyType;
                //typesComboOption.mongoId = $scope.arrangements.types[i].companyId;
                typesComboOption.innerHTML = $scope.arrangements.types[i].CompanyType;
                typesComboOption.subMenu = $scope.arrangements.types[i].CompanyNames;
                typesCombo.appendChild(typesComboOption);
            }

            var typesComboOption = document.createElement('option');
            typesComboOption.selected = true;
            typesComboOption.disabled = true;
            typesComboOption.hidden = true;
            typesComboOption.innerHTML = "-- בחר אופציה --";
            typesComboOption.value = "-- בחר אופציה --";
            typesCombo.appendChild(typesComboOption);

            typesCombo.onchange = function () {
                if (typesCombo.selectedOptions[0].subMenu) {
                    var subBoxId = 'appointment-sub-types-combo' + typesCombo.index;
                    for (i = 0; i < appointmentDiv.childNodes.length; i++) {
                        var child = appointmentDiv.childNodes[i];
                        if (child.id === subBoxId) {
                            child.remove();
                        }
                    }

                    var subTypesCombo = document.createElement('select');
                    subTypesCombo.id = subBoxId;
                    subTypesCombo.className = "round-buttons-calendar-view";

                    var selectedSubMenu = typesCombo.selectedOptions[0].subMenu;

                    for (i = 0; i < selectedSubMenu.length; i++) {
                        var typesComboOption = document.createElement('option');
                        typesComboOption.id = i;
                        typesComboOption.value = selectedSubMenu[i].SubType /* + ", " + selectedSubMenu[i].Address*/;
                        typesComboOption.innerHTML = selectedSubMenu[i].SubType /* + ", " + selectedSubMenu[i].Address*/;
                        typesComboOption.subType = $scope.subDictionary[selectedSubMenu[i].SubType];
                        typesComboOption.mainType = $scope.dictionary[typesCombo.selectedOptions[0].value];
                        subTypesCombo.appendChild(typesComboOption);

                        if (selectedSubMenu[i].Remark && selectedSubMenu[i].Remark !== '') {
                            typesComboOption.innerHTML +=  ", " + selectedSubMenu[i].Remark;
                            typesComboOption.value += ", " + selectedSubMenu[i].Remark;
                        }
                    }

                    var typesComboOption = document.createElement('option');
                    typesComboOption.selected = true;
                    typesComboOption.disabled = true;
                    typesComboOption.hidden = true;
                    typesComboOption.innerHTML = "-- בחר אופציה --";
                    typesComboOption.value = "-- בחר אופציה --";
                    subTypesCombo.appendChild(typesComboOption);

                    appointmentDiv.appendChild(subTypesCombo);
                }
            }

            var deleteButton = document.createElement("img");
            deleteButton.src = "https://cdn4.iconfinder.com/data/icons/32x32-free-design-icons/32/Delete.png";
            deleteButton.onclick = function () {
                var parentSelectDiv = this.parentElement;
                var reachedParent = false;
                arrangementsChilds = document.getElementById('arrangements').childNodes;
                for (var element = 0; element < arrangementsChilds.length; element++) {
                    var child = arrangementsChilds[element];
                    if (child.id === parentSelectDiv.id) {
                        reachedParent = true;
                    }

                    if (child.tagName && child.tagName.toUpperCase() === 'BR' && reachedParent) {
                        child.remove();
                        break;
                    }
                }

                appointmentDiv.remove();
            }

            deleteButton.className = "small-delete-button";

            var brSpace = document.createElement('br');

            appointmentDiv.appendChild(deleteButton);
            appointmentDiv.appendChild(typesCombo);

            $('#addArrangement').before(appointmentDiv);
            $('#addArrangement').before("<br />");
        }
    }
});

app.controller('datePickCtrl', function ($scope) {
    var date = new Date();
    var yyyy = date.getFullYear().toString();
    var mm = (date.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = date.getDate().toString();
    var datePickerInit = yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]);
    var datePicker = document.getElementById('date-picker');
    datePicker.innerHTML = datePickerInit;
    datePicker.value = datePickerInit;

    $('#date-picker').change(function () {
        $('#calendar').fullCalendar('gotoDate', $('#date-picker')[0].valueAsDate);
    });
});

app.controller('calendarCtrl', function ($scope) {

    $('.fc-header-title').parent('td').after($('#date-picker'));

    $scope.$on('$viewContentLoaded', function () {
        $('#calendar').fullCalendar({
            header: {
                left: 'title',
                center: '',
                right: '' //agendaDay month,agendaWeek,
            },
            width: ($(window).width() * 45) / 100,
            defaultView: 'agendaDay',
            allDaySlot: false,
            selectable: true,
            selectHelper: true,
            maxTime: '24:00:00',
            minTime: '06:00:00',
            lang: 'he',
            slotLabelFormat: 'HH:mm',
            select: function (start, end) {
                $('#timepickerDiv .time').timepicker({
                    'scrollDefault': 'now',
                    'showDuration': true,
                    'disableTextInput': true,
                    'timeFormat': 'H:i'
                });

                // initialize datepair
                var timepickerDiv = document.getElementById('timepickerDiv');
                var datepair = new Datepair(timepickerDiv);

                document.getElementById('modalTitle').value = '';
                document.getElementById('modalLocation').value ='';
                document.getElementById('modalStartTime').value = moment(start).format('HH:mm');
                document.getElementById('modalEndTime').value = moment(end).format('HH:mm');

                var input = document.getElementById('modalLocation');
                var options = {
                    //types: ['(cities)'],
                    componentRestrictions: { country: 'il' }
                };

                autocomplete = new google.maps.places.Autocomplete(input, options);
                autocomplete.addListener('place_changed', function () {
                    var place = autocomplete.getPlace();
                    document.getElementById('modalLocation').googleValue = true;
                    document.getElementById('modalLocation').googleCity = place.address_components[1].long_name;
                });

                document.getElementById('modalDelete').onclick = function () {
                    $('#calendar').fullCalendar('unselect');
                };

                document.getElementById('modalSave').onclick = function () {
                    if (document.getElementById('modalTitle').value !== '' && document.getElementById('modalLocation').value !== '' && 
                        document.getElementById('modalLocation').googleValue) {
                        event = {
                            title: '',
                            location: '',
                            city: '',
                            end: '',
                            start: '',
                            date: ''
                        };
                        var endTime = document.getElementById('modalEndTime').value.split(':');
                        var startTime = document.getElementById('modalStartTime').value.split(':');

                        event.title = document.getElementById('modalTitle').value;
                        event.location = document.getElementById('modalLocation').value;
                        event.city = document.getElementById('modalLocation').googleCity;

                        end.hour(endTime[0]);
                        end.minute(endTime[1]);
                        event.end = end;

                        start.hour(startTime[0]);
                        start.minute(startTime[1]);
                        event.start = start;

                        event.date = document.getElementById('date-picker').value;

                        $('#calendar').fullCalendar('renderEvent', event, true); // stick? = true
                        $('#fullCalModal').modal("hide");
                    } else {
                        document.getElementById('errorMsg').value = "בחר עיר מתוך רשימת הערים והזן כותרת";
                        document.getElementById('errorMsg').innerHTML = "בחר עיר מתוך רשימת הערים והזן כותרת";
                        $('#errorModal').modal();
                    }
                }

                $('#fullCalModal').modal();
            },
            eventClick: function (event, jsEvent, view) {
                $('#timepickerDiv .time').timepicker({
                    'scrollDefault': 'now',
                    'showDuration': true,
                    'disableTextInput': true,
                    'timeFormat': 'H:i'
                });

                // initialize datepair
                var timepickerDiv = document.getElementById('timepickerDiv');
                var datepair = new Datepair(timepickerDiv);

                document.getElementById('modalTitle').value = event.title;
                document.getElementById('modalLocation').value = event.location;
                document.getElementById('modalStartTime').value = moment(event.start).format('HH:mm');
                document.getElementById('modalEndTime').value = moment(event.end).format('HH:mm');

                var input = document.getElementById('modalLocation');
                var options = {
                    //types: ['(cities)'],
                    componentRestrictions: { country: 'il' }
                };

                autocomplete = new google.maps.places.Autocomplete(input, options);
                autocomplete.addListener('place_changed', function () {
                    var place = autocomplete.getPlace();
                    document.getElementById('modalLocation').googleValue = true;
                    document.getElementById('modalLocation').googleCity = place.address_components[1].long_name;
                });

                document.getElementById('modalDelete').onclick = function () {
                    $('#calendar').fullCalendar('removeEvents', event._id);
                };

                document.getElementById('modalSave').onclick = function () {
                    if (document.getElementById('modalTitle').value !== '' && document.getElementById('modalLocation').value !== '' && 
                        document.getElementById('modalLocation').googleValue) {
                        var endTime = document.getElementById('modalEndTime').value.split(':');
                        var startTime = document.getElementById('modalStartTime').value.split(':');

                        event.title = document.getElementById('modalTitle').value;
                        event.location = document.getElementById('modalLocation').value;
                        event.city = document.getElementById('modalLocation').googleCity;

                        event.end.hour(endTime[0]);
                        event.end.minute(endTime[1]);

                        event.start.hour(startTime[0]);
                        event.start.minute(startTime[1]);

                        $('#calendar').fullCalendar('updateEvent', event);
                        $('#fullCalModal').modal("hide");
                    }
                }

                $('#fullCalModal').modal();
            },
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            events: [
                {
                    title: 'Meeting',
                    start: '2015-12-19T10:30:00',
                    end: '2015-12-19T12:30:00'
                }
            ]
        });
    });
});

app.controller('calculateRequestCtrl', function ($scope, $rootScope, $http, $location) {
    $scope.calculateRequest = function () {
        $scope.showBusy = true;
        var allEvents = $('#calendar').fullCalendar('clientEvents');
        var events = [];

        for (var event = 0; event < allEvents.length; event++) {
            if (allEvents[event].date === document.getElementById('date-picker').value) {
                var currEvent = {
                    title: allEvents[event].title,
                    location: allEvents[event].location,
                    city: allEvents[event].city,
                    end: allEvents[event].end,
                    start: allEvents[event].start,
                    date: allEvents[event].date
                };
                
                events.push(currEvent);
            }
        }

        var input = document.getElementById('citySelect');
        var cityList = document.getElementById('json-datalist');
        var valueInCityList = false;

        if (input.value !== '' && document.getElementById('citySelect').googleValue) {
            valueInCityList = true;
        }

        var unselectedDropDown = false;
        var selectedAppointments = [];
        var appointments = document.getElementsByName("appointment-div");

        for (var appoint = 0; appoint < appointments.length && !unselectedDropDown; appoint++) {
            var selectElements = appointments[appoint].childNodes;

            for (var select = 0; select < selectElements.length && !unselectedDropDown; select++) {
                if (selectElements[select].tagName && selectElements[select].tagName.toUpperCase() === 'SELECT') {
                    if (selectElements[select].selectedOptions[0].mainType) {
                        selectedAppointments.push({ companyType: selectElements[select].selectedOptions[0].mainType, SubType: selectElements[select].selectedOptions[0].subType });
                    }

                    if (selectElements[select].selectedOptions[0].id == '') {
                        unselectedDropDown = true;
                    }
                }
            }
        }

        var travelWay = $('input[name="travelWay"]:checked', '#travelWayForm').val();

        var dataObject = {};
        dataObject.events = events;
        dataObject.appointmentsCity = input.value;
        dataObject.travelWay = travelWay;
        dataObject.selectedAppointments = selectedAppointments;
        dataObject.selectedDate = document.getElementById('date-picker').value;

        if (!valueInCityList) {
            document.getElementById('errorMsg').value = "בחר עיר מתוך רשימת הערים";
            document.getElementById('errorMsg').innerHTML = "בחר עיר מתוך רשימת הערים";
            $('#errorModal').modal();
            $scope.showBusy = false;
        } else if (unselectedDropDown) {
            document.getElementById('errorMsg').value = "בחר סידור משימת הסידורים האפשריים";
            document.getElementById('errorMsg').innerHTML = "בחר סידור משימת הסידורים האפשריים";
            $('#errorModal').modal();
            $scope.showBusy = false;
        } else {
            var url = 'http://localhost:52747/api/PathCalculator';
            var data = dataObject;
            $http.post(url, data)
            .success(function (data) {
                $scope.showBusy = false;
                $rootScope.pathsList = data.paths;
                $location.path("/map");
            })
            .error(function (data, status, header, config) {
                var b = 3;
                $scope.showBusy = false;
            });
        }
    }

    var input = document.getElementById('citySelect');
    var options = {
        types: ['(cities)'],
        componentRestrictions: { country: 'il' }
    };

    autocomplete = new google.maps.places.Autocomplete(input, options);
    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();
        document.getElementById('citySelect').googleValue = true;
    });
});