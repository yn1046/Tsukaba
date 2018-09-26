import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Link, Switch, Route } from 'react-router-dom'
import Post from '../post/post.jsx';
import PostForm from '../postForm/postForm.jsx';

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            threadList: null,
            toThread: false
        }

        this.allThreads = this.allThreads.bind(this);
        this.submitThread = this.submitThread.bind(this);
    }

    componentDidMount() {
        fetch(`/api/Board/${this.props.boardId}`)
            .then(res => res.json())
            .then(threads => this.setState({ threadList: threads }));
    }

    allThreads() {
        return (
            <div>
                <PostForm submit={this.submitThread} boardId={this.props.boardId} />
                {this.state.threadList.map(thread =>
                    <Post
                        key={thread.lastTimeBumped}
                        post={thread}
                        openable={true}
                    />
                )}
            </div>
        );
    }

    submitThread(formData) {
        if (!formData.get('images')) {
            alert('Can\'t create a thread without at least one image.');
            return;
        }

        fetch('/api/Board', {
            method: 'POST',
            body: formData
        });
    }

    render() {
        let boardContent;
        if (!this.state.threadList) boardContent = <h1>lololoading...</h1>;
        else boardContent = this.allThreads();

        return (
            boardContent
        );
    }
}