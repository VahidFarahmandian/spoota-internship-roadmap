
import styles from "./../styles/Profile.module.css";
import styles2 from "./../styles/ProfileSidebar.module.css";
import {Link} from "react-router-dom";
import React, { useState } from "react";

import axios from "axios";

const SignUp2Fa = () => {
    const [usenames, setusenames] = useState('');
    const [passwords, setPasswords] = useState('');
    const [name, setnames] = useState('');
    const [phoneNumber, setphoneNumber] = useState('');
    const [favoriteColor, setfavoriteColor] = useState('');
    const [height, setheight] = useState('');
    const [showSuccess, setShowSuccess] = useState(false)
    const [showEroor, setShowError] = useState(false)
    
    const handleLogin_sign = () => {
        console.log("hihi")
        setShowError(false);
        axios.post('http://localhost:5281/Signup2FA', {
                username: usenames,
                name: name,
                password: passwords,
                phoneNumber: phoneNumber,
                favoriteColor: favoriteColor,
                height: height,  
    
        })
            .then(function (response) {
                console.log(response);
                window.location.href = '/'
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
                    <h1 className={styles.profile_right_font}>Here is Sign up 2fa</h1>
                </div>
                    <form>
                        <input
                            className={styles.profile_input}
                            type="text"
                            name="username"
                           placeholder="username"
                            required=""
                           // value={profileState.name.value}
                           onChange={(e) => setusenames(e.target.value)}
                        />
                        <input
                            className={styles.profile_input}
                            type="text"
                            name="phone_number"
                            placeholder="phone_number"
                            required=""
                          //  value={profileState.phone_number.value}
                          onChange={(e) => setphoneNumber(e.target.value)}
                        />
                        <input
                            className={styles.profile_input}
                            type="password"
                            name="password"
                            placeholder="New Password"
                            required=""
                           // value={profileState.password.value}
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
                        <input
                            className={styles.profile_input}
                            type="text"
                            name="favoriteColor"
                            placeholder="favoriteColor"
                            required=""
                            // value={profileState.name.value}
                            onChange={(e) => setfavoriteColor(e.target.value)}
                        />
                        <input
                            className={styles.profile_input}
                            type="text"
                            name="height"
                           placeholder="height"
                            required=""
                            // value={profileState.name.value}
                            onChange={(e) => setheight(e.target.value)}
                        />
                        <button onClick={handleLogin_sign} className={styles.profile_button} type="button">Save</button>
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

export default SignUp2Fa;