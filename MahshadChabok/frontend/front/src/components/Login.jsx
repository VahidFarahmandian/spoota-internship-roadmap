import React, { useState } from "react";
import styles from './../styles/Login.module.css';
import axios from "axios";
const Login = () => {
    const [usename, setusename] = useState('');
    const [password, setPassword] = useState('');
    const [usenames, setusenames] = useState('');
    const [passwords, setPasswords] = useState('');
    const [phoneNumber, setPhones] = useState('');
    const [showSuccess, setShowSuccess] = useState(false)
    const [showEroor, setShowError] = useState(false)
    const handleLogin_log = () => {

        axios.post('http://127.0.0.1:8088/users/login', {
            phone_number: usename,
            password: password,
        })
            .then(function (response) {
                localStorage.setItem('token', response.data.token.access_token)
                window.location.href = '/sms'

            })
            .catch(function (error) {
                console.log(error);
            });
    };
    const handleLogin_sign = () => {
        setShowError(false);
        axios.post('http://127.0.0.1:8088/users/register', {
            phone_number: phoneNumber,
            name: usenames,
            password: passwords,
        })
            .then(function (response) {
                console.log(response);
                setShowSuccess(true)
                setShowError(false)
            })
            .catch(function (error) {
                setShowError(true)
                setShowSuccess(false)
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
                               value={password}
                               onChange={(e) => setPassword(e.target.value)}
                        />
                        <input className="myinput"
                            type="phone number"
                            name="phone number"
                            placeholder="phone number"
                            required=""
                            value={usename}
                            onChange={(e) => setusename(e.target.value)}
                        />

                        <button className="mybutton" type="button" onClick={handleLogin_log}>Login</button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Login;