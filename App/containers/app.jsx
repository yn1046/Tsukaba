import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Link, Switch, Route } from 'react-router-dom'
import Home from './home/home.jsx'

export default function App() { 
    return (
        <Router>
            <div>
                <nav>
                    <Link to='/'>Home</Link>
                    <Link to='/b/'>/b/</Link>
                    <Link to='/about'>About</Link>
                </nav>
                <Switch>
                    <Route exact={true} path='/' render={Home} />
                </Switch>
            </div>
        </Router>
    );
}