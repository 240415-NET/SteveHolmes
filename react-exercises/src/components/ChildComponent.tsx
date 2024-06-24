import React from 'react';

interface MessageProps {
    aString: string;
}

function ChildComponent({aString}: MessageProps) {
    return <h3>{aString}</h3>;
};

export default ChildComponent;
