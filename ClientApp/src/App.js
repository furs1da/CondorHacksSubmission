import React, { Component } from 'react';
import { NavLink, Router, Route, Link } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { LoginPage } from './components/LoginPage';
import { AddAssignment } from './components/AddAssignment';
import { ListAssignment } from './components/ListOfTasks';
import { history, Role } from './helpers';
import { authenticationService } from './services';
import { PrivateRoute } from './components/PrivateRoute';
import './custom.css'
import { RegisterPage } from './components/RegisterPage';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import { DropdownSubmenu, NavDropdownMenu } from "react-bootstrap-submenu";

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);

        this.state = {
            currentUser: null,
            isAdmin: false,
            isUser: false,
        };
    }

    componentDidMount() {
        authenticationService.currentUser.subscribe(x => this.setState({
            currentUser: x,
            isAdmin: x && x.idRole === 1,
            isUser: x && x.idRole === 2,
        }));
    }
    logout() {
        authenticationService.logout();
        history.push('/login');
    }

    render() {
        const { currentUser, isAdmin, isUser } = this.state;
        return (
            <Router history={history} >
                <div>
                    {currentUser && isUser &&
                        <Navbar collapseOnSelect expand="lg" bg=".bg-danger.bg-gradient" variant="dark" style={{ backgroundColor: "#e30909"}}>
                            <Navbar.Brand as={Link} to="/"><i class="book icon"></i>Step-by-step</Navbar.Brand>
                            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                            <Navbar.Collapse id="responsive-navbar-nav">
                                <Nav>
                                    <Nav.Link as={Link} to="/addTask"><i class="user outline icon"></i>Add a task</Nav.Link>
                                    <Nav.Link as={Link} to="/tasks"><i class="envelope outline icon"></i>List of tasks</Nav.Link>
                                    <Nav.Link onClick={this.logout}><i class="sign out alternate icon"></i>Exit</Nav.Link>
                                </Nav>
                            </Navbar.Collapse>
                        </Navbar>
                    }
                    {currentUser && isAdmin &&
                        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
                            <Navbar.Brand as={Link} to="/"><i class="book icon"></i>Step-by-step</Navbar.Brand>
                            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                            <Navbar.Collapse id="responsive-navbar-nav">
                                <Nav>
                                    <Nav.Link as={Link} to="/myInfoPupil"><i class="user outline icon"></i>Edit personal data</Nav.Link>
                                    <Nav.Link as={Link} to="/watchFeedbackPupil"><i class="envelope outline icon"></i>Mails</Nav.Link>
                                    <Nav.Link onClick={this.logout}><i class="sign out alternate icon"></i>Exit</Nav.Link>
                                </Nav>
                            </Navbar.Collapse>
                        </Navbar>
                    }

                    <div className="jumbotron" style={{ minHeight: 90 + "vh", paddingBottom: 0, marginBottom: 0, backgroundColor: "white" }}>
                        <div className="container" class="d-flex justify-content-center" >
                            <div class="container-fluid">
                                <PrivateRoute exact path="/" component={Home} />


                                <PrivateRoute path="/addTaskARCHIEVE" roles={[Role.User]} component={AddAssignment} />
                                <Route path="/addTask" component={AddAssignment} />
                                <Route path="/tasks" component={ListAssignment} />
                                
                                <Route path="/login" component={LoginPage} />
                                <Route path="/register" component={RegisterPage} />
                            </div>
                        </div>

                    </div>

                </div>
                <div style={{ position: "relative" }}>
                    <footer class="text-50" style={{
                    minHeight: 5 + "vh",
                    backgroundColor: "#white",
                        color: "#A43023",
                        position: "absolute",
                    bottom: 0 + "%",
                    width: "100%",
                    borderTop: "3px solid #A43023",
                        paddingBottom: 0, marginBottom: 0,
                        overflow: "hidden"
                }}>
                    <div class="container text-center align-self-center" >
                        <p>Dmytrii Furs &copy; {new Date().getFullYear()}</p>
                    </div>
                    </footer>
                    </div>
            </Router>
        );
    }
}

