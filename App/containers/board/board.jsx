import React from 'react'
import ReactDOM from 'react-dom'
import Thread from './thread/thread.jsx';
import PostForm from './postForm/postForm.jsx';

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            threadList: null
        }
    }

    componentDidMount() {
        fetch(`/api/Data/${this.props.boardId}`)
            .then(res => res.json())
            .then(threads => this.setState({ threadList: threads }));
    }

    render() {
        const threadList = this.state.threadList;
        let threads;
        if (!threadList) threads = <h1>lololoading...</h1>;
        else threads = this.state.threadList.map(thread =>
                <Thread
                    key={thread.id}
                    thread={thread}
                />
            ); 
            
        return (
            <div>
                <PostForm boardId={this.props.boardId} />
                {threads}
            </div>
        );
    }
}