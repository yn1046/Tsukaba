import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Link, Switch, Route } from 'react-router-dom'
import Home from './home/home.jsx'
import Board from './board/board.jsx'
import OpenThread from './openThread/openThread.jsx'
import About from './about/about.jsx'

export default function App() {
    return (
        <Router>
            <div>
                <nav>
                    <Link to='/'>Home</Link>
                    <Link to='/b'>/b/</Link>
                    <Link to='/about'>About</Link>
                </nav>
                <Switch>
                    <Route exact={true} path='/' render={Home} />
                    <Route exact={true} path='/b/' render={(params) => <Board {...params} boardId={1} />} />
                    <Route exact={true} path='/b/res/:id' render={(params) => <OpenThread {...params} boardId={1} />} />
                    <Route path='/about' render={About} />
                </Switch>
            </div>
        </Router>
    );
}