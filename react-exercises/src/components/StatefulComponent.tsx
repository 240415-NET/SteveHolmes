import React, {useState} from "react";

const StatefulComponent = () => {

    const [index, setIndex] = useState<number>(0);
    const [item, setItem] = useState<string | undefined>(undefined);
    const [clickCount, setClickCount] = useState(0);

    const dummyData = [ "Spring", "Summer", "Autumn", "Winter", "Hibernation"];

    const inputValueChanged = (event: any) => { setIndex(Number(event.target.value)); } 

    const inputButtonClicked = () => {
        setClickCount(clickCount + 1);
        if (index >= 0 && index < dummyData.length)
            setItem(dummyData[index]);
        else
            setItem('That index value is out of range!');
    }
     
    return (
        <div className = "input_container">
            <input id="indexField" className = "input_field" onChange={inputValueChanged} type="input" placeholder="Enter index" />
            <br />       
            <button className = "input_button" onClick={inputButtonClicked} type="submit">Find Item</button>      
            <br />
            <h3 id="display-text">Selected item = {item}</h3>
            <h3 id="click-count">Click count = {clickCount}</h3>
        </div>
    )
}
export default StatefulComponent;
