import React from 'react'
import ReactDOM from 'react-dom'

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
        let images;
        if (!this.state.images) images = <em>loading images...</em>
        else images = this.state.images.map(i => <img key={i.imageUrl} src={'./files/'+i.imageUrl}></img>);
        
        return (
            <div>
                <h1>{this.props.thread.title}</h1>
                <h3>№{this.props.thread.numberOnBoard}</h3>
                <p style={{ fontWeight: 'bold', fontStyle: 'italic' }}>{this.getTimeString()}</p>
                <p>{this.props.thread.message}</p>
                {images}
                <hr />
            </div>
        );
    }
}