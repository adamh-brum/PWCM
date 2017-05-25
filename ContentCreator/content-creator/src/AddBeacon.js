import React from 'react';
import logo from './logo.svg';
import './App.css';
import BeaconImage from './img/generic-beacons.jpg';
import './font-awesome-4.7.0/css/font-awesome.min.css'
import FontAwesome from 'react-fontawesome';
import TextField from 'material-ui/TextField';


const AddBeaconComponent = class AddBeacon extends React.Component {
    constructor(props) {
        super(props);
        this.state = { uuid: '', beaconId: '', friendlyName: '', location: ''};

        this.uuidChanged = this.uuidChanged.bind(this);
        this.beaconIdChanged = this.beaconIdChanged.bind(this);
        this.friendlyNameChanged = this.friendlyNameChanged.bind(this);
        this.locationChanged = this.locationChanged.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    uuidChanged(event) {
        this.setState({ uuid: event.target.value });
    }

    beaconIdChanged(event) {
        this.setState({ beaconId: event.target.value });
    }

    friendlyNameChanged(event) {
        this.setState({ friendlyName: event.target.value });
    }

    locationChanged(event) {
        this.setState({ location: event.target.value });
    }

    handleSubmit(event) {
        // Todo validate (All fields mandatory)
        alert('A UUID was submitted: ' + this.state.uuid);
        event.preventDefault();
    }

    render() {
        return (
            <div id="addBeacon">
                <div className="App App-header">
                    <img src={logo} className="App-logo" alt="logo" />
                    <h2>Register a Beacon</h2>
                </div>
                <div>
                    <form onSubmit={this.handleSubmit}>
                        <TextField
                            hintText="UUID"
                            value={this.state.uuid}
                            onChange={this.uuidChanged}/>
                        <TextField
                            value={this.state.beaconId}
                            onChange={this.beaconIdChanged}
                            hintText="Beacon Identifier"/>
                        <TextField
                            value={this.state.friendlyName}
                            onChange={this.friendlyNameChanged}
                            hintText="Friendly Name"/>
                        <TextField
                            value={this.state.location}
                            onChange={this.locationChanged}
                            hintText="Location"/>
                        <input type="submit" value="Submit" />
                    </form>
                </div>
            </div>
        );
    }
}
export default AddBeaconComponent;