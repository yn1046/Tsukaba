import React from 'react'
import ReactDOM from 'react-dom'
import './form.css'


export default class PostForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: '',
            message: '',
            images: []
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const target = event.target;
        const name = target.name;

        let value;
        if (target.type === 'file') {
            value = Array.from(target.files);
            if (value.length > 4) {
                value = value.slice(0, 4);
                alert('More than 4 files. Selecting first 4.');
            }
        }
        else {
            value = target.type === 'checkbox' ?
                target.checked :
                target.value;
        }

        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        let formData = new FormData();
        formData.append('title', this.state.title);
        formData.append('message', this.state.message);
        formData.append('boardId', this.props.boardId);
        this.state.images.forEach(image => formData.append('images', image));

        fetch('/api/Board', {
            method: 'POST',
            body: formData
        }).then(res => {if (res) console.log(res)});
    }

    render() {
        return (
            <div className="form-container">
                <form onSubmit={this.handleSubmit}>
                    <input
                        value={this.state.title}
                        onChange={this.handleChange}
                        name="title"
                        placeholder="Enter topic... (optional)"
                    />
                    <textarea
                        value={this.state.message}
                        onChange={this.handleChange}
                        name="message"
                        rows="5"
                        placeholder="Enter message..."
                    />
                    <input
                        onChange={this.handleChange}
                        type="file" multiple
                        name="images"
                    />
                    <input type="submit" value="Send" />
                </form>
            </div>
        );
    }
}