var BeaconLogic = class BeaconLogic {
    constructor(locationManager) {
        this.locationManager = locationManager;
    }

    /**
     * Function that creates a BeaconRegion data transfer object.
     * uuid = guid 00000000-0000-0000-0000-000000000000
     * identifier = string
     * 
     * @throws Error if the BeaconRegion parameters are not valid.
     */
    createBeaconRegion(uuid, identifier) {
        return this.createBeaconRegion(uuid, identifier, minor, major);
    } 

    /**
     * Function that creates a BeaconRegion data transfer object.
     * uuid = guid 00000000-0000-0000-0000-000000000000
     * identifier = string
     * 
     * @throws Error if the BeaconRegion parameters are not valid.
     */
    createBeaconRegion(uuid, identifier, major, minor){
        return this.locationManager.BeaconRegion(identifier, uuid, major, minor);
    }
}