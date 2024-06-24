import React, {useState} from "react";

const Lists = () => {

    const [index, setIndex] = useState<number>(0);
    const [item, setItem] = useState<string | undefined>(undefined);

    const dummyData = [ "first item", "second item", "third item", "fourth item"];

    const inputValueChanged = (event: any) => { setIndex(Number(event.target.value)); } 

    const findDummyDataItem = () => {
        if (index >= 0 && index < dummyData.length)
            setItem(dummyData[index]);
        else
            setItem('That index value is out of range!');
    }
     
    return (
        <div className = "input_container">
            <input id="indexField" className = "input_field" onChange={inputValueChanged} type="input" placeholder="Enter index" />
            <br />       
            <button className = "input_button" onClick={findDummyDataItem} type="submit">Find Item</button>      
            <br />
            <h3 id="display-text">{item}</h3>
        </div>
    )
}
export default Lists;