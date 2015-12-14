var app = angular.module('mainApp', ['ngRoute', 'ngMaterial', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'home.html',
            controller: 'homeCtrl'
        })
        .when('/home', {
            templateUrl: 'home.html',
            controller: 'homeCtrl'
        })
        .when('/login', {
            templateUrl: 'login.html',
            controller: 'loginCtrl'
        })
        .when('/calendar', {
            templateUrl: 'calendar.html',
            controller: 'calendarCtrl'
        })
        .when('/map', {
            templateUrl: 'map.html',
            controller: 'mapCtrl'
        })
        .when('/about', {
            templateUrl: 'about.html',
            controller: 'aboutCtrl'
        });

});
