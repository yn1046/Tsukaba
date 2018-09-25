import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Link, Switch, Route } from 'react-router-dom'
import Thread from '../thread/thread.jsx';
import PostForm from '../postForm/postForm.jsx';

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            threadList: null,
            toThread: false
        }

        this.AllThreads = this.AllThreads.bind(this);
        this.OpenThread = this.OpenThread.bind(this);
    }

    componentDidMount() {
        fetch(`/api/Board/1`)
            .then(res => res.json())
            .then(threads => this.setState({ threadList: threads }));
    }

    AllThreads() {
        return (
            <div>
                <PostForm boardId={this.props.boardId} />
                {this.state.threadList.map(thread =>
                    <Thread
                        key={thread.lastTimeBumped}
                        thread={thread}
                        only={false}
                    />
                )}
            </div>
        );
    }

    OpenThread(id) {
        console.log(id);
        return (
            <Thread only={true} thread={this.state.threadList.find(t => t.numberOnBoard == +id)} />
        );
    }

    render() {
        const id = this.props.match.params.id;
        console.log(this.props.match);

        let boardContent;
        if (!this.state.threadList) boardContent = <h1>lololoading...</h1>;
        else {
            if (id) boardContent = this.OpenThread(id);
            else boardContent = this.AllThreads();
        }

        return (
            boardContent
        );
    }
}