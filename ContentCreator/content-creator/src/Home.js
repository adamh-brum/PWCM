import React from 'react';
import logo from './logo.svg';
import './App.css';
import BeaconImage from './img/generic-beacons.jpg';
import './font-awesome-4.7.0/css/font-awesome.min.css'
import FontAwesome from 'react-fontawesome';
import { GridList, GridTile } from 'material-ui/GridList';
import IconButton from 'material-ui/IconButton';
import PlusIcon from 'material-ui/svg-icons/content/add';

const HomeComponent = class HomeComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { value: '' };
        this.navigate = this.navigate.bind(this);
    }

    navigate(event) {
        alert('A name was submitted: ' + this.state.value);
        event.preventDefault();
    }

    render() {
        return (<div id="home" >
            <div className="App-header">
                <img src={logo} className="App-logo" alt="logo" />
                <h2>Welcome to the Design Studio</h2>
            </div>
            <p className="App-intro">
                Use the buttons below to get started.<FontAwesome name="plus" />
            </p>
            <div>
                <GridList>
                    <GridTile
                        title="Register a Beacon"
                        subtitle={<span>Register a new beacon to use in campaigns and events</span>}
                        actionIcon={<IconButton onClick=""><PlusIcon color="white" /></IconButton>}>
                        <img src={BeaconImage} />
                    </GridTile>
                </GridList>
            </div>
        </div >);
    }
}

export default HomeComponent;