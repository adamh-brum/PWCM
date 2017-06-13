class CacheManager {
  constructor() {
  }

  readCache() {
    if (typeof (Storage) != "undefined") {
      var cacheString = localStorage.getItem("cache");
      var cache = new CacheModel();
      if (cacheString != null) {
        var cache = JSON.parse(cacheString) as CacheModel;
      }

      return cache;
    }
  }

  updateCards(cards) {
    var cache = this.readCache();
    cache.cards = cards;
    this.writeCache(cards);
  }

  writeCache(cache) {
    // Cards are added to array and storage now
    localStorage.setItem("cards", JSON.stringify(cache.cards));
  }
}

class CacheModel {
  cards;

  constructor() {
  }
}