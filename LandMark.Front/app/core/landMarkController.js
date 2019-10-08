//This JS file is main angular controller and fetch or post data from endpoint API
//functionalites exposed by this controller 
// 1.Post a Note to back-end
// 2. Get all the notes in database
// 3. Get  all the notes at a particular position
// 4. Get all the notes for a user at particular position

/// <reference path="../lib/angular/angular.js" />

(function () {

    "use strict";

    //Getting the existing module
    angular.module("app-landmark")
        .controller("landmarkController", landmarkController);

    function landmarkController($http, InfoWindowService) {
        var lm = this;
        lm.notes = [];
        lm.newNote = {};
        lm.errorMessage = "";

        var endpointUrl = "https://localhost:44399";
        var latitude = window.position.coords.latitude;
        var longitude = window.position.coords.longitude;
        var positionQueryString = "?lat=" + latitude + "&lng=" + longitude
        var allNotesUrl = endpointUrl + "/api/notes";
        var postUrl = endpointUrl + "/api/notes";
        var positionBasedNotes = allNotesUrl + positionQueryString;


        //POST
        lm.addNote = function () {

            lm.newNote.latitude = latitude;
            lm.newNote.longitude = longitude;
            getLocation(latitude, longitude).then(function (results) {
                lm.newNote.address = results[0].formatted_address;
                
                $http.post(postUrl, lm.newNote).then(function (response) {
                    addNoteOnMap(response.data, window, InfoWindowService)
                    lm.notes.push(response.data);
                    lm.newNote = {};
                }, function (response) {
                    lm.errorMessage = "Error" + response;
                }).finally(function () {
                    
                });
            });

        }

        lm.getNotes = function (url) {
            $http.get(url).then(function (response) {
                angular.copy(response.data, lm.notes)
                renderNotes(lm.notes, window, InfoWindowService)
            }, function (error) {
                alert("something went wrong\n Request end point url : " + error.config.url);
            })
        }

        lm.getAllNotes = function () {
            lm.closeAllNotes();
            lm.getNotes(positionBasedNotes);
            window.map.setZoom(12);
        }

        lm.getGlobalNotes = function () {
            lm.closeAllNotes();
            lm.getNotes(allNotesUrl);
            window.map.setZoom(8);
        }

        lm.getUserNotes = function () {
            if (getCookie()) {
                var username = getCookie();
                lm.closeAllNotes();
                lm.getNotes(allNotesUrl + "/" + username + "?lat=" + latitude + "&lng=" + longitude);
                window.map.setZoom(12);
            }
            else {
                lm.getGlobalNotes();
                alert("User name is not defined");
            }
        }

        lm.closeAllNotes = function () {
            closeNotes(InfoWindowService)
        }

        lm.InitilizeUserName = function (newNote) {
            var usernamevalue = getCookie();
            if (usernamevalue) {
                newNote.user = usernamevalue
            }
            else {
                setCookie();
                usernamevalue = getCookie();
                newNote.user = usernamevalue;
            }
        }

        lm.InitilizeUserName(lm.newNote);

        lm.getGlobalNotes();
        

    }

})();

