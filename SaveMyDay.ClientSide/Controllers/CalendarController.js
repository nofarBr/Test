
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
        appointmentDiv.className = "combo-box-margins";

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

        var typesComboOption = document.createElement('option');
        typesComboOption.selected = true;
        typesComboOption.disabled = true;
        typesComboOption.hidden = true;
        typesComboOption.innerHTML = "-- select an option --";
        typesComboOption.value = "-- select an option --";
        typesCombo.appendChild(typesComboOption);

        typesCombo.onchange = function () {
            var subBoxId = 'appointment-sub-types-combo' + typesCombo.index;
            for (i = 0; i < appointmentDiv.childNodes.length; i++) {
                var child = appointmentDiv.childNodes[i];
                if (child.id === subBoxId) {
                    child.remove();
                }
            }

            var subTypesCombo = document.createElement('select');
            subTypesCombo.id = subBoxId;

            var selectedSubMenu = typesCombo.selectedOptions[0].subMenu;

            for (i = 0; i < selectedSubMenu.length; i++) {
                var typesComboOption = document.createElement('option');
                typesComboOption.id = i;
                typesComboOption.value = selectedSubMenu[i].name + ", " + selectedSubMenu[i].city;
                typesComboOption.innerHTML = selectedSubMenu[i].name + ", " + selectedSubMenu[i].city;
                subTypesCombo.appendChild(typesComboOption);
            }

            var typesComboOption = document.createElement('option');
            typesComboOption.selected = true;
            typesComboOption.disabled = true;
            typesComboOption.hidden = true;
            typesComboOption.innerHTML = "-- select an option --";
            typesComboOption.value = "-- select an option --";
            subTypesCombo.appendChild(typesComboOption);

            appointmentDiv.appendChild(subTypesCombo);
        }

        var deleteButton = document.createElement("img");
        deleteButton.src = "https://cdn4.iconfinder.com/data/icons/32x32-free-design-icons/32/Delete.png";
        deleteButton.onclick = function () {
            appointmentDiv.remove();
        }

        deleteButton.className = "small-delete-button";

        appointmentDiv.appendChild(deleteButton);
        appointmentDiv.appendChild(typesCombo);

        $('#arrangements').append(appointmentDiv);
    }
});

app.controller('datePickCtrl', function ($scope) {
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

                document.getElementById('modalTitle').value = 'כותרת';
                document.getElementById('modalLocation').value ='מיקום';
                document.getElementById('modalStartTime').value =  moment(start).format('HH:mm');
                document.getElementById('modalEndTime').value = moment(end).format('HH:mm');

                document.getElementById('modalDelete').onclick = function () {
                    $('#calendar').fullCalendar('unselect');
                };

                document.getElementById('modalSave').onclick = function () {
                    event = {
                        title: '',
                        location: '',
                        end: '',
                        start: ''
                    };
                    var endTime = document.getElementById('modalEndTime').value.split(':');
                    var startTime = document.getElementById('modalStartTime').value.split(':');

                    event.title = document.getElementById('modalTitle').value;
                    event.location = document.getElementById('modalLocation').value;

                    end.hour(endTime[0]);
                    end.minute(endTime[1]);
                    event.end = end;

                    start.hour(startTime[0]);
                    start.minute(startTime[1]);
                    event.start = start;

                    $('#calendar').fullCalendar('renderEvent', event, true); // stick? = true
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
                document.getElementById('modalStartTime').value =  moment(event.start).format('HH:mm');
                document.getElementById('modalEndTime').value = moment(event.end).format('HH:mm');

                document.getElementById('modalDelete').onclick = function () {
                    $('#calendar').fullCalendar('removeEvents', event._id);
                };

                document.getElementById('modalSave').onclick = function () {
                    var endTime = document.getElementById('modalEndTime').value.split(':');
                    var startTime = document.getElementById('modalStartTime').value.split(':');

                    event.title = document.getElementById('modalTitle').value;
                    event.location = document.getElementById('modalLocation').value;

                    event.end.hour(endTime[0]);
                    event.end.minute(endTime[1]);

                    event.start.hour(startTime[0]);
                    event.start.minute(startTime[1]);

                    $('#calendar').fullCalendar('updateEvent', event);
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

app.controller('calculateRequestCtrl', function ($scope) {
    $scope.calculateRequest = function () {
        var input = document.getElementById('citySelect');

        //if ($scope.cities.indexOf(input.value.trim()) < 0) {
        //    alert("בחר עיר מרשימת הערים");
        //} else {
        $.post('http://localhost:52747/api/PathCalculator');
        //}
    }

    var dataList = document.getElementById('json-datalist');
    var input = document.getElementById('citySelect');

    $.get("../Datalist/CityList.txt", function (data) {
        // Loop over the JSON array.
        var cities = data.split('\n');
        $scope.cities = cities;
        cities.forEach(function (item) {
            // Create a new <option> element.
            var option = document.createElement('option');
            // Set the value using the item in the JSON array.
            option.value = item.trim();
            // Add the <option> element to the <datalist>.
            dataList.appendChild(option);
        });
    });
});