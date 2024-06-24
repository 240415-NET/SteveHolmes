import React from 'react';
import logo from './logo.svg';
import './App.css';
// import './components/ComponentOne.css';
// import './components/Events.css';


// import ComponentOne from './components/ComponentOne';
// import Events from './components/Events';
import Lists from './components/Lists';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        {/* <ComponentOne /> */}
        {/* <Events /> */}
        <Lists />
      </header>
    </div>
  );
}

export default App;
