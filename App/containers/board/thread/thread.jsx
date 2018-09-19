import React from 'react'
import ReactDOM from 'react-dom'

// TODO: remake into single thread, fetch and map in board.jsx
export default function Thread(props) {
    function getTimeString(threadTime) {
        const date = new Date(threadTime);
        const isoDate = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
        const timeString = isoDate.toISOString().split(/T|Z|\./).slice(0,2).join(' ');
        return timeString;
    }

    return (
            <div>
                <h1>{props.thread.title}</h1>
                <p style={{fontWeight: 'bold', fontStyle: 'italic'}}>{getTimeString(props.thread.time)}</p>
                <p>{props.thread.message}</p>
                <img src={'./files/'+props.thread.imageUrl} />
                <hr />
            </div>
    );
}