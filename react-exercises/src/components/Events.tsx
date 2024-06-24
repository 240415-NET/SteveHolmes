import React from "react";
import './Events.css';

const Events = () => {

    const inputValueChanged = (event: any) => {console.log("input_field value =", event.target.value);} 

    const inputButtonClicked = () => {console.log("input_button clicked!");}

    return (
        <div className = "input_container">
            <input className = "input_field" onChange={inputValueChanged} type="input" placeholder="Enter something here" />
            <br />
            <button className = "input_button" onClick={inputButtonClicked} type="submit">Do it</button>      
        </div>
    )
}
export default Events;
 