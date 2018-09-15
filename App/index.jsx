import React from 'react';
import ReactDOM from 'react-dom';

function Hello(props) {
    return (
        <h1>
            Hello, {props.name}!
        </h1>
    );
}

ReactDOM.render(
    <Hello name="Vasya" />,
    document.getElementById('content')
);