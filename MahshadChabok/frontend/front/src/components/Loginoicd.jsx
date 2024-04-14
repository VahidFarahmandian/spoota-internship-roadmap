import React, { useState } from "react";
import styles from './../styles/Login.module.css';

import {Link} from "react-router-dom";
import axios from "axios";
const Loginoicd = () => {
    const [usename, setusename] = useState('');
    const [password, setPassword] = useState('');
    const [showSuccess, setShowSuccess] = useState(false)
    const [showEroor, setShowError] = useState(false)

    const handleLogin_log = () => {
        axios.post('http://localhost:5281/LoginOIDC', {
            username: usename,
            password: password,
          
        })
            .then(function (response) {
                localStorage.setItem('token', response.data.token)
                localStorage.setItem('username', response.data.user.username)
                window.location.href = '/panel'

            })
            .catch(function (error) {
                console.log(error);
            });
    };
    
    return (
        <div className={styles.first}>
            <div className={styles.main}>
                <div className={styles.login}>
                    <form>
                        <label htmlFor={styles.chk} aria-hidden="true">Login</label>
                        <input className="myinput"
                               type="Username"
                               name="Username"
                               placeholder="Username"
                               required=""
                              
                               onChange={(e) => setPassword(e.target.value)}
                        />
                        <input className="myinput"
                               type="password"
                               name="password"
                               placeholder="password"
                               required=""
                              
                               onChange={(e) => setusename(e.target.value)}
                        />

                        <button className="mybutton" type="button" onClick={handleLogin_log}>Login</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Loginoicd;