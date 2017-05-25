import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import './font-awesome-4.7.0/css/font-awesome.min.css'
import FontAwesome from 'react-fontawesome';

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>Welcome to the Design Studio</h2>
        </div>
        <p className="App-intro">
          Use the buttons below to get started.
        </p>
        <div>
          <FontAwesome name='rocket' size='2x' spin/>
        </div>
      </div>
    );
  }
}

export default App;
