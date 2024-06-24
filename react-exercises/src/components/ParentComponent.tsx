import React from 'react';
import ChildComponent from './ChildComponent';

function ParentComponent() {
    const aString = "Hello from the parent component to the child component!";

    return (
        <div>
            <ChildComponent aString ={aString} />
        </div>
    )
}
export default ParentComponent;