readCache = function() {
  if (typeof (Storage) != "undefined") {
    var cacheString = localStorage.getItem("cache");
    var cache = {cards: []};
    if (cacheString != null) {
      var cache = JSON.parse(cacheString);
    }

    return cache;
  }
}

updateCards = function(cards) {
  var cache = this.readCache();
  cache.cards = cards;
  this.writeCache(cards);
}

writeCache= function(cache) {
  // Cards are added to array and storage now
  localStorage.setItem("cache", JSON.stringify(cache));
}