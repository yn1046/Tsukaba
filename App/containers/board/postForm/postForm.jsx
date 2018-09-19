import React from 'react'
import ReactDOM from 'react-dom'
import './form.css'


export default class PostForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            title: '',
            message: '',
            image: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit(event) {
        alert(`You have submitted:\n${this.state.title}\n${this.state.message}`);
        event.preventDefault();
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
                        value={this.state.image}
                        onChange={this.handleChange}
                        type="file"
                        name="image"
                    />
                    <input type="submit" value="Send" />
                </form>
            </div>
        );
    }
}