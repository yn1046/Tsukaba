import React from 'react'
import ReactDOM from 'react-dom'

export default class Picture extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            images: null
        };
    }

    componentDidMount() {
        const post = this.props.post;
        fetch(`/api/Images/${post.boardId}/${post.numberOnBoard}`)
            .then(res => res.json())
            .then(images => this.setState({ images }));
    }

    render() {
        const images = this.state.images;

        let display;
        if (!images) display = <h1>loading images...</h1>;
        else display=images.map((i,j) => <img key={j} src={i} />);
        return (
            display
        );
    }
}
