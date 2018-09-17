import React from 'react'
import ReactDOM from 'react-dom'

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            thread: null
        }
    }

    componentDidMount() {
        fetch('/api/Values')
            .then(res => res.json())
            .then(thread => this.setState({ thread }));
    }

    getTimeString() {
        const date = new Date(this.state.thread.time);
        const isoDate = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
        const timeString = isoDate.toISOString().split(/T|Z|\./).slice(0,2).join(' ');
        return timeString;
    }

    render() {
        if (!this.state.thread) {
            return (<h1>lololoading...</h1>);
        }

        return (
            <div>
                <h1>{this.state.thread.title}</h1>
                <p style={{fontWeight: 'bold', fontStyle: 'italic'}}>{this.getTimeString()}</p>
                <p>{this.state.thread.message}</p>
                <img src={'./files/'+this.state.thread.imageUrl} />
            </div>
        );
    }
}