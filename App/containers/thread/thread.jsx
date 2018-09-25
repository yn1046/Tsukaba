import React from 'react'
import ReactDOM from 'react-dom'
import { NavLink } from 'react-router-dom'
import Picture from '../picture/picture.jsx'

// TODO: remake into single thread, fetch and map in board.jsx
export default class Thread extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            images: null
        };
    }

    getTimeString() {
        const date = new Date(this.props.thread.time);
        const isoDate = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
        const timeString = isoDate.toISOString().split(/T|Z|\./).slice(0, 2).join(' ');
        return timeString;
    }

    render() {
        const thread = this.props.thread;
        
        let open;
        if (!this.props.only) open = <NavLink to={`/b/${thread.numberOnBoard}`}>[Open]</NavLink>;
        else open = false;

        return (
            <div>
                <h1>{thread.title}</h1>
                <h3>№{thread.numberOnBoard}</h3>
                {open}
                <p style={{ fontWeight: 'bold', fontStyle: 'italic' }}>{this.getTimeString()}</p>
                <pre>{thread.message}</pre>
                <Picture thread={thread} />
                <hr />
            </div>
        );
    }
}