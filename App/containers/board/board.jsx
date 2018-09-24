import React from 'react'
import ReactDOM from 'react-dom'
import Thread from './thread/thread.jsx';
import PostForm from './postForm/postForm.jsx';

export default class Board extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            threadList: null,
            openThread: null
        }

        this.handleOpen = this.handleOpen.bind(this);
    }

    componentDidMount() {
        fetch(`/api/Board/${this.props.boardId}`)
            .then(res => res.json())
            .then(threads => this.setState({ threadList: threads }));
    }

    handleOpen(openThread) {
        this.setState({ openThread });
    }

    render() {
        const threadList = this.state.threadList;
        const openThread = this.state.openThread;
        let boardContent;
        if (!threadList) boardContent = <h1>lololoading...</h1>;
        else boardContent = this.state.threadList.map(thread =>
                <Thread
                    key={thread.numberOnBoard}
                    thread={thread}
                    only={false}
                    handleOpen={this.handleOpen}
                />
            );

        if (openThread) boardContent = <Thread thread={openThread} only={true} />

        return (
            <div>
                <PostForm boardId={this.props.boardId} />
                {boardContent}
            </div>
        );
    }
}