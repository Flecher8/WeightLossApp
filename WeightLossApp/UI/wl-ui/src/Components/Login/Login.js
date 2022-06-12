import { useState } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Login() {

    const [errors, setErrors] = useState({
        Global: "",
        Login: "",
        Password: "",
    });

    const [user, setUser] = useState({
        Id: undefined,
        Login: "",
    })

    function checkData() {

    }

    return(
        <div className="wrapper rounded shadow">
            <h2>Login</h2>
            <p>Please enter your data.</p>

            {errors.Global !== "" ? (
                <div className="alert alert-danger">{errors.Global}</div>
            ) : null}       

            <form action="login.php" method="post">
                <div className="form-group">
                    <label>Login</label>
                    {/* <input type="text" name="login" className="form-control <?php echo (!empty($loginErr)) ? 'is-invalid' : ''; ?>" value="<?php echo $login; ?>"> */}

                    <input 
                        type="text" 
                        name="login" 
                        className={errors.Login !== "" ? "is-invalid form-control" : "form-control"} 
                        value={user.Login}>
                    </input>
                    <span className="invalid-feedback">{errors.Login}</span>
                </div>    
                <div className="form-group">
                    <label>Password</label>
                    <input 
                        type="password" 
                        name="password" 
                        className={errors.Password !== "" ? "is-invalid form-control" : "form-control"}> 
                    </input>
                    <span className="invalid-feedback">{errors.Password}</span>
                </div>
                <div className="form-group">
                    <input 
                        type="submit" 
                        className="btn btn-dark my-3 px-4" 
                        value="Send"></input>
                </div>
            </form>
        </div>
    );
}

export default Login;