
import styles from "./../styles/Profile.module.css";
import styles2 from "./../styles/ProfileSidebar.module.css";
import React, { useState } from "react";
import {Link} from "react-router-dom";
import axios from "axios";
    
const SignUpOTP = () => {
    const [usenames, setusenames] = useState('');
    const [phoneNumber, setphoneNumber] = useState('');
    const [name, setnames] = useState('');
    const [email, setemails] = useState('');
    const [showSuccess, setShowSuccess] = useState(false)
    const [showEroor, setShowError] = useState(false)
    
    const handleLogin_sign = () => {
        console.log("hihi")
        setShowError(false);
        axios.post('http://localhost:5281/SignupOTP', {
                username: usenames,
                name: name,
                email: email,
                phoneNumber: phoneNumber,
           
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
                    <h1 className={styles.profile_right_font}>Here is Sign up otp</h1>
                </div>
                <form>
                    <input
                        className={styles.profile_input}
                        type="text"
                        name="Username"
                        placeholder="Username"
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
                        name="Email"
                        placeholder="Email"
                        required=""
                        // value={profileState.name.value}
                        onChange={(e) => setemails(e.target.value)}
                    />


                    <button onClick={handleLogin_sign}  className={styles.profile_button} type="button">Save</button>
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

export default SignUpOTP;