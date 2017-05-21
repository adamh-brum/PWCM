// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
angular.module('contentReceiver', ['ionic', 'ionic.contrib.ui.cards'])

.run(function($ionicPlatform, $rootScope) {
  $ionicPlatform.ready(function() {
    if(window.cordova &&  window.cordova.plugins) {
      if(window.cordova.plugins.Keyboard){
        console.log("Updating keyboard settings (Hide keyboard, disable scroll)")

        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

        // Don't remove this line unless you know what you are doing. It stops the viewport
        // from snapping when text inputs are focused. Ionic handles this internally for
        // a much nicer keyboard experience.
        cordova.plugins.Keyboard.disableScroll(true);
      }

      if(window.cordova.plugins.notification){
        console.log("Requesting notification permission");
        window.cordova.plugins.notification.local.promptForPermission();
      }

      if(window.cordova.plugins.locationManager){
        console.log("Requesting Location Permission");
        cordova.plugins.locationManager.requestAlwaysAuthorization();

        console.log("Location Manager enabled. Will begin ranging for beacons");
        var delegate = new window.cordova.plugins.locationManager.Delegate();

        delegate.didDetermineStateForRegion = function (pluginResult) {
            console.log('didDetermineStateForRegion: ' + JSON.stringify(pluginResult));
        };

        delegate.didExitRegion = function(pluginResult) {
            console.log('didExitRegion: ' + JSON.stringify(pluginResult));
        };

        delegate.didEnterRegion = function(pluginResult) {
          console.log('didEnterRegion: ' + JSON.stringify(pluginResult));
        };

        delegate.didStartMonitoringForRegion = function (pluginResult) {
            console.log('didStartMonitoringForRegion:', pluginResult);
        };

        cordova.plugins.locationManager.setDelegate(delegate);

        // airlocate e2c56db5-dffb-48d2-b060-d0f5a71096e0
        // var beaconLogic = new BeaconLogic(window.cordova.plugins.locationManager);
        // var beaconRegion = beaconLogic.createBeaconRegion("74278BDA-B644-4520-8F0C-720EAF059935", "MiniBeacon_33497")
        var region = new cordova.plugins.locationManager.BeaconRegion("MiniBeacon_33497", "74278BDA-B644-4520-8F0C-720EAF059935");
        console.log("Created region")
        console.log(JSON.stringify(region))

        cordova.plugins.locationManager.startMonitoringForRegion(region)
        .fail(console.error)
        .done();

        cordova.plugins.locationManager.startRangingBeaconsInRegion(region)
        .fail(console.error)
        .done();
      }
    }

    if(window.StatusBar) {
      StatusBar.styleDefault();
    }
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

  // $scope.getCardFromServer("1");\
  $scope.addAndDisplayCard("<p>Here with my mentor</p>", 'Catch up');
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