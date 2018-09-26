import React from 'react'
import ReactDOM from 'react-dom'
import Post from '../post/post.jsx'
import PostForm from '../postForm/postForm.jsx'

export default class OpenThread extends React.Component {
    constructor(params) {
        super(params);
        this.state = {
            op: null,
            postList: null
        }
        this.threadId = +this.props.match.params.id;

        this.fetchOp = this.fetchOp.bind(this);
        this.fetchPosts = this.fetchPosts.bind(this);
        this.mapPosts = this.mapPosts.bind(this);
        this.submitPost = this.submitPost.bind(this);
    }

    componentDidMount() {
        this.fetchOp();
        this.fetchPosts();
    }

    fetchOp() {
        fetch(`/api/Topic/${this.props.boardId}/${this.threadId}`)
            .then(res => res.json())
            .then(op => this.setState({ op }));
    }

    fetchPosts() {
        fetch(`/api/Posts/${this.props.boardId}/${this.threadId}`)
            .then(res => res.json())
            .then(postList => this.setState({ postList }));
    }

    mapPosts() {
        return (
            <div>
                {this.state.postList.map(post =>
                    <Post
                        key={post.time}
                        post={post}
                        openable={false}
                    />
                )}
            </div>
        );
    }

    submitPost(formData) {
        formData.append('parentId', this.threadId);
        fetch('/api/Posts', {
            method: 'POST',
            body: formData
        });
    }

    render() {
        const id = this.threadId
        console.log(id);
        let op;
        if (!this.state.op) op = <h1>lololoading...</h1>
        else op = <Post post={this.state.op} openable={false} />;

        let posts;
        if (!this.state.postList) posts = <h3>No responses yet.</h3>
        else posts = this.mapPosts();

        return (
            <div>
                {op}
                {posts}
                <PostForm submit={this.submitPost} boardId={this.props.boardId} />
            </div>
        );
    }
}