// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
angular.module('contentReceiver', ['ionic', 'ionic.contrib.ui.cards'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    if(window.cordova && window.cordova.plugins.Keyboard) {
      // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
      // for form inputs)
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

      // Don't remove this line unless you know what you are doing. It stops the viewport
      // from snapping when text inputs are focused. Ionic handles this internally for
      // a much nicer keyboard experience.
      cordova.plugins.Keyboard.disableScroll(true);
    }

    if(window.StatusBar) {
      StatusBar.styleDefault();
    }
  });
})

.controller('CardsCtrl', function($scope, $http, $ionicSwipeCardDelegate) {
  $scope.cards = [];
 
  $scope.addCard = function(content, name) {
      var cardId = Math.random();
      content = "Hey there " + cardId;
      name = cardId;
      var newCard = {htmlContent: content, title: name};
      newCard.id = cardId;
      $scope.cards.unshift(angular.extend({}, newCard));
  };

  $scope.addCard("", "");
  $scope.addCard("", "");
  $scope.addCard("", "");
 
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