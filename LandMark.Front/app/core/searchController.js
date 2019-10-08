//This JS class is angualr controler for search. It fetches the data from backend and diplay it in tabular format.
//It also display specific search result on the map.

/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    //Getting the existing module
    angular.module("app-landmark")
        .controller("searchController", searchController);

    function searchController($http, InfoWindowService) {

        var endpointUrl = "https://localhost:44399";
        var searchUrl = endpointUrl + "/api/notes/search"
        var sc = this;
        sc.notes = [];
        sc.query = null;

        sc.errorMessage = "";
        sc.isBusy = true;
        closeNotes(InfoWindowService)
        //Search
        sc.search = function (query) {
            sc.notes = [];
            $http.get(searchUrl + "/" + query).then(function (response) {
                
                
                angular.copy(response.data, sc.notes)
            }, function (error) {
                if (error.status == 404) {
                   
                    alert("No Result found ");
                   
                }
                sc.errorMessage = "Failed to load data" + error;
            }).finally(function () {
                sc.isBusy = false;
            });

        }

        sc.showOnMap = function (note) {
            addNoteOnMap(note, window, InfoWindowService)
            window.map.setZoom(13);
        }

    }

})();




