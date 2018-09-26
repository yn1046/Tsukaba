import React from 'react'
import ReactDOM from 'react-dom'
import { NavLink } from 'react-router-dom'
import Picture from '../picture/picture.jsx'

export default class Post extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            images: null
        };
    }

    getTimeString() {
        const date = new Date(this.props.post.time);
        const isoDate = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
        const timeString = isoDate.toISOString().split(/T|Z|\./).slice(0, 2).join(' ');
        return timeString;
    }

    render() {
        const post = this.props.post;
        
        let open;
        if (this.props.openable) open = <NavLink to={`/b/res/${post.numberOnBoard}`}>[Open]</NavLink>;
        else open = false;

        return (
            <div>
                <h1>{post.title}</h1>
                <h3>№{post.numberOnBoard}</h3>
                {open}
                <p style={{ fontWeight: 'bold', fontStyle: 'italic' }}>{this.getTimeString()}</p>
                <pre>{post.message}</pre>
                <Picture post={post} />
                <hr />
            </div>
        );
    }
}