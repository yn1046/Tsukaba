import React from 'react'
import ReactDOM from 'react-dom'
import { NavLink } from 'react-router-dom'

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

    componentDidMount() {
        const thread = this.props.thread;
        fetch(`/api/Images/${thread.boardId}/${thread.numberOnBoard}`)
            .then(res => res.json())
            .then(images => this.setState({images}));
    }

    render() {
        const thread = this.props.thread;
        let images;
        if (!this.state.images) images = <em>loading images...</em>
        else images = this.state.images.map((i, j) => {
            console.log(i);
            return (<img key={j} src={i}></img>);
        });
        
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
                {images}
                <hr />
            </div>
        );
    }
}