import { useEffect, useState } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Login() {

    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [loginError, setLoginError] = useState('');
    const [passwordError, setPasswordError] = useState('');
    const [globalError, setGlobalError] = useState('');
    const [users, setUsers] = useState([]);

    function getUsers() {
        fetch(constants.API_URL + "Admin")
			.then(response => response.json())
			.then(data => { 
                console.log(data);
                setUsers(data) });
    }

    function checkInput() {
        let passed = true;

        if (login === "") {
            setLoginError("Please enter your login");
            passed = false;
        }
        else {
            setLoginError("");
        }

        if (password === "") {
            setPasswordError("Please enter your password");
            passed = false;
        } 
        else {
            setPasswordError("");
        }

        if (passed) {
            getUsers();
            console.log(users);
            let user = users.find(u => u.Login === login);
            console.log(user);
            if (user) {

                if (user.Password === password.trim()) {
                    return user;
                }
                else {
                    setGlobalError("Password is invalid");
                    return null;
                }
            }
            else {
                setGlobalError("Invalid login. There is no user with such login");
                return null;
            }
        }
        else return null;
    }

    function onSend() {
        let user = checkInput();
        if (user) {
            let userData = {
                Id: user.Id,
                Login: user.Login
            };

            localStorage.setItem("user", JSON.stringify(userData));
            window.location.href = "/"
        }
    }

    return(
        <div className="login-wrap rounded shadow">
            <h2>Login</h2>
            <p>Please enter your data.</p>

            {globalError !== "" ? (
                <div className="alert alert-danger">{globalError}</div>
            ) : null}       

            <div className="form-group">
                <label>Login</label>
                <input 
                    type="text" 
                    name="login" 
                    className={loginError !== "" ? "is-invalid form-control" : "form-control"} 
                    value={login}
                    onChange={e => setLogin( e.target.value )}>
                </input>
                <span className="invalid-feedback">{loginError}</span>
            </div>    
            <div className="form-group">
                <label>Password</label>
                <input 
                    type="password" 
                    name="password" 
                    className={passwordError !== "" ? "is-invalid form-control" : "form-control"}
                    value={password}
                    onChange={e => setPassword( e.target.value )}> 
                </input>
                <span className="invalid-feedback">{passwordError}</span>
            </div>
            <div className="form-group">
                <input 
                    type="submit" 
                    className="btn btn-dark my-3 px-4" 
                    value="Send"
                    onClick={() => onSend() }>

                </input>
            </div>
        </div>
    );
}

export default Login;