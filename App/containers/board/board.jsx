import React from 'react'
import ReactDOM from 'react-dom'

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            thread: null,
            isLoading: true
        }
    }

    componentDidMount() {
        fetch('/api/Values')
            .then(res => res.json())
            .then(thread => this.setState({ thread, isLoading: false }));
    }

    render() {
        if (!this.state.thread) {
            return (<h1>lololoading...</h1>);
        }
        return (
            <div>
                <h1>{this.state.thread.title}</h1>
                <p>{this.state.thread.message}</p>
            </div>
        );
    }
}