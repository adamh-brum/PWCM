import React from 'react';
import ReactDOM from 'react-dom';
import logo from './logo.svg';
import './App.css';
import BeaconImage from './img/generic-beacons.jpg';
import './font-awesome-4.7.0/css/font-awesome.min.css'
import FontAwesome from 'react-fontawesome';
import Input from 'material-ui/Input/Input';
import axios from 'axios';
import Button from 'material-ui/Button';
import Paper from 'material-ui/Paper';
import Grid from 'material-ui/Grid';
import Typography from 'material-ui/Typography';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import HomeComponent from './Home.js'

const ContentSchedulerComponent = class ContentScheduler extends React.Component {
    constructor(props) {
        super(props);
        this.state = { loadingState: 0, contentSearchText: '', selectedContentId: 0, selectedBeaconsAndTimes: [], content: [], beaconsAndAvailability: [] };

        this.contentSearchTextChanged = this.contentSearchTextChanged.bind(this);
        this.incrementLoadingState = this.incrementLoadingState.bind(this);

        this.getContent();
        this.getBeacons();
    }

    incrementLoadingState() {
        this.state.loadingState = this.state.loadingState + 1;

        if (this.state.loadingState === 2) {
            // Reveal form

        }
    }

    getContent() {
        axios.get("http://localhost:5000/api/Content/All").then(res => {
            this.state.content = res;

            this.incrementLoadingState();
        });
    }

    getBeacons() {
        axios.get("http://localhost:5000/api/Schedule").then(res => {
            this.state.beaconsAndAvailability = res;

            this.incrementLoadingState();
        });
    }

    contentSearchTextChanged(event) {
        this.setState({ contentSearchText: event.target.value });
    }

    cancelClicked(event) {
        ReactDOM.render(
            <MuiThemeProvider>
                <HomeComponent />
            </MuiThemeProvider>,
            document.getElementById('root'));
    }

    handleSubmit(event) {
        // Todo validate (All fields mandatory)
        event.preventDefault();

        // Submit to API
        var url = "http://localhost:5000/api/Beacon?id=" + this.state.uuid + "&Name=" + this.state.id + "&FriendlyName=" + this.state.friendlyName + "&Location=" + this.state.location;
        axios.post(url).then(res => {
            //const posts = res.data.data.children.map(obj => obj.data);
            //this.setState({ posts });

        });
    }

    render() {
        return (
            <div id="scheduleContent">
                <div className="App App-header">
                    <img src={logo} className="App-logo" alt="logo" />
                    <h2>Schedule a Message</h2>
                </div>
                <div>
                    <Grid container gutter={24}>
                        <Grid item xs={12}>
                            <Paper className="Paper">
                                <div id="loading">
                                </div>
                                <div id="mask" className="hidden">
                                    <div id="schedulerForm" className="contentSchedulerForm">
                                        <div id="selectContent" className="searchContentPane">
                                            <Input
                                                placeholder="Search for messages..."
                                                value={this.state.contentSearchText}
                                                onChange={this.contentSearchTextChanged}/>
                                        </div>
                                        <div id="selectBeacons" className="beaconSelectionPane">

                                        </div>
                                    </div>
                                    <div id="submitForm">

                                    </div>
                                </div>
                            </Paper>
                        </Grid>
                    </Grid>
                </div>
            </div>
        );
    }
}

export default ContentSchedulerComponent;