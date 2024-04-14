
import styles from "./../styles/Profile.module.css";
import styles2 from "./../styles/ProfileSidebar.module.css";
import React, { useState } from "react";
import {Link} from "react-router-dom";
import axios from "axios";

const SignUpOIDC = () => {
    const [usenames, setusenames] = useState('');
    const [passwords, setPasswords] = useState('');
    const [name, setnames] = useState('');
    const [showSuccess, setShowSuccess] = useState(false)
    const [showEroor, setShowError] = useState(false)
    
    const handleLogin_sign = () => {
        console.log("hihi")
        setShowError(false);
        axios.post('http://localhost:5281/SignupOIDC', {
            username: usenames,
            password: passwords,
            name: name,
           
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

    const logout = () => {
        //  localStorage.removeItem('token')
        window.location.href = '/'
    }

    const moveToPanel = () => {
        window.location.href = '/panel'
    }
    return (
        <div className={styles.profile_right}>

            <div className={styles.profile_formContainer}>
                <div className={styles.profile_h_position}>
                    <h1 className={styles.profile_right_font}>Here is Sign up oidc</h1>
                </div>
                <form>
                    <input
                        className={styles.profile_input}
                        type="text"
                        name="username"
                        placeholder="Username"
                        required=""
                        // value={profileState.name.value}
                        onChange={(e) => setusenames(e.target.value)}
                    />
                    <input
                        className={styles.profile_input}
                        type="text"
                        name="password"
                        placeholder="Password"
                        required=""
                        //  value={profileState.phone_number.value}
                        
                        onChange={(e) => setPasswords(e.target.value)}
                    />

                    <input
                        className={styles.profile_input}
                        type="text"
                        name="name"
                        placeholder="name"
                        required=""
                        // value={profileState.name.value}
                        
                        onChange={(e) => setnames(e.target.value)}
                    />



                    <button  onClick={handleLogin_sign} className={styles.profile_button} type="button">Save</button>
                </form>
            </div>
            <div className={styles2.profile_sidebar}>
                <Link to="/" className={styles2.profile_sidebarLink}>
                    Home
                </Link>


                <button className={styles2.profile_sidebar_button} onClick={logout}>
                    Landing page
                </button>
            </div>
        </div>




    );
};

export default SignUpOIDC;