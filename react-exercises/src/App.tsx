import React from 'react';
import logo from './logo.svg';
import './App.css';
import './components/ComponentOne.css';

import ComponentOne from './components/ComponentOne';


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <ComponentOne />
      </header>
    </div>
  );
}

export default App;
