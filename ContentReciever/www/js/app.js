// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
angular.module('contentReceiver', ['ionic', 'ionic.contrib.ui.cards'])

.run(function($ionicPlatform, $rootScope) {
  $ionicPlatform.ready(function() {
    var logText = document.getElementById("log").innerText;

    if(window.cordova &&  window.cordova.plugins) {
      if(window.cordova.plugins.Keyboard){
        logText = logText + " - Loading Keyboard Settings";

        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

        // Don't remove this line unless you know what you are doing. It stops the viewport
        // from snapping when text inputs are focused. Ionic handles this internally for
        // a much nicer keyboard experience.
        cordova.plugins.Keyboard.disableScroll(true);
      }

      if(window.cordova.plugins.locationManager){
        logText = logText + " - Loading Location Settings";
        window.cordova.plugins.locationManager.enableBluetooth();

        var beaconLogic = new BeaconLogic(window.cordova.plugins.locationManager);

        // airlocate e2c56db5-dffb-48d2-b060-d0f5a71096e0
        var beaconRegion = beaconLogic.createBeaconRegion("74278BDA-B644-4520-8F0C-720EAF059935", "MiniBeacon_33497")
      }
    }

    document.getElementById("log").innerHTML = logText;

    if(window.StatusBar) {
      StatusBar.styleDefault();
    }
    //// new cordova().plugins.locationManager.enableBluetooth();
    // // // Setup the beacon listening job
    // // $cordovaBeacon.requestWhenInUseAuthorization();

    // // $rootScope.$on("$cordovaBeacon:didRangeBeaconsInRegion", function(event, pluginResult) {
    // //     var uniqueBeaconKey;
    // //     for(var i = 0; i < pluginResult.beacons.length; i++) {
    // //         $scope.getCardFromServer(pluginResult.beacons[i].uuid);
    // //     }
    // //     $scope.$apply();
    // // });

    // // $cordovaBeacon.startRangingBeaconsInRegion();
  });
})

.controller('CardsCtrl', function($scope, $http) {
  $scope.cards = [];

  $scope.getSavedCards = function(){
    if(typeof(Storage) != "undefined") {
        var cardsString = localStorage.getItem("cards");
        if(cardsString === null){
          var cards = [];
        }
        else{
          var cards = JSON.parse(cardsString);
        }

        return cards;
    }

    return null;
  }

  $scope.loadSavedCards = function(){
    // Retrieve the saved cards from device
    var cards = $scope.getSavedCards();
    for(cardIndex in cards){
      $scope.displayCard(cards[cardIndex]);
    }
  }

  $scope.saveCard = function(card){
    // Save the card
    if(typeof(Storage) != "undefined") {
        var cards = $scope.getSavedCards();
        cards.unshift(angular.extend({}, card));
        localStorage.setItem("cards", JSON.stringify(cards));
    } else {
        alert("Sorry, your browser does not support Web Storage...");
    }
  }

  $scope.getCardFromServer = function(beaconId){
    beaconId = "1";
    var url = "http://localhost:5000/api/Content?locationId=" + beaconId;
    $http.get(url).success( function(response) {
      response.forEach(function(element) {
        $scope.addAndDisplayCard(element.content, element.contentShortDescription);
      }, this);
    });
  }

  $scope.displayCard = function(card){
      $scope.cards.unshift(angular.extend({}, card));
  }

  $scope.addAndDisplayCard = function(content, name){
      var newCard = {id: Math.random(), htmlContent: content, title: name};
      $scope.displayCard(newCard);
      $scope.saveCard(newCard);
  }

  $scope.getCardFromServer("1");
  $scope.loadSavedCards();

  $scope.cardDestroyed = function(index) {
    $scope.cards.splice(index, 1);
  };
})

.controller('CardCtrl', function($scope, $ionicSwipeCardDelegate) {
  $scope.doAnything = function() {
    var card = $ionicSwipeCardDelegate.getSwipeableCard($scope);
    card.swipe();
  };
})