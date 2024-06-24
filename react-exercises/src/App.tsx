import React from 'react';
import logo from './logo.svg';
import './App.css';
// import './components/ComponentOne.css';
import './components/Events.css';

// import ComponentOne from './components/ComponentOne';
import Events from './components/Events';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        {/* <ComponentOne /> */}
        <Events />
      </header>
    </div>
  );
}

export default App;
