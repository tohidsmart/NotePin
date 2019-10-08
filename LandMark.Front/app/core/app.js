//This JS file initilizes the angualr module and routing. 
//It registers Map angular controller which assigns Google map object to window object
//And registers an angular service with module to keep track of all google map InfoWindow objects

'use strict';
var appModule = angular.module('app-landmark', ["ngRoute"]).config(function ($routeProvider, $locationProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider.when("/", {
        controller: "landmarkController",
        controllerAs: "lm",
        templateUrl: "/views/addNote.html"
    });

    $routeProvider.when("/search", {
        
        controller: "searchController",
        controllerAs: "sc",
        templateUrl: "/views/searchNotes.html"
    })

    $routeProvider.otherwise({
        redirectTo: "/"
    });

});

appModule.factory('InfoWindowService', function () {
    var InfoWindows = [];
    return {
        getWindows: function () {
            return InfoWindows;
        },
        addWindow: function (window) {
            InfoWindows.push(window);
            return true;
        }
    };
});

appModule.controller("MapController", function ($window) {

    $window.map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 10
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $window.position = position;
        }, function (error) {
            alert("Browser Geo location service is needed for this app");
            var infoWindow = new google.maps.InfoWindow;
            infoWindow.setPosition($window.map.getCenter())
            infoWindow.setContent("Geo location is needed")
            infoWindow.open($window.map);
        });
    }
});



