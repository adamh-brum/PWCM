readCache = function () {
  if (typeof (Storage) != "undefined") {
    var cacheString = localStorage.getItem("cache");
    var cache = { cards: [] };
    if (cacheString != null) {
      var cache = JSON.parse(cacheString);
      if( cache == null ){
        cache = { cards: [] };
      }
    }

    return cache;
  }
}

updateCards = function (cards) {
  console.log("ordering cards before save. Cards before order: " + JSON.stringify(cards));

  // if length is greater than 4, delete excess cards
  // this means, after latest card is added, app is only storing 5
  var count = cards.length;
  console.log(count + " cards already saved on the device.");

  if (count > 4) {
    console.log("The device currently has too many cards saved. Cleaning.")
    var cardsToDelete = count - 4;

    console.log("Deleting " + cardsToDelete + " cards.")
    cards.splice(0, cardsToDelete);
  }

  console.log("Sorting cards by date");
  cards = cards.sort(function (a, b) {
    a = new Date(a.dateTime);
    b = new Date(b.dateTime);
    return a > b ? -1 : a < b ? 1 : 0;
  });

  console.log("Cards after order: " + JSON.stringify(cards));
  console.log("Saving cards");

  var cache = this.readCache();
  cache.cards = cards;
  this.writeCache(cards);

  console.log("Cards saved");
}

writeCache = function (cache) {
  // Cards are added to array and storage now
  localStorage.setItem("cache", JSON.stringify(cache));
}

clearCache = function (cache) {
  localStorage.setItem("cache", null);
}