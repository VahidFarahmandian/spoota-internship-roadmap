import styles from "./../styles/Profile.module.css";
import styles2 from "./../styles/ProfileSidebar.module.css";

import React, { useState } from "react";
import {Link} from "react-router-dom";
import axios from "axios";

const SecondLayer = () => {
    
    const [favoriteColor, setfavoriteColor] = useState('');
    const [height, setheight] = useState('');
    const handle2fa = () => {
        console.log("hihi")
      
        axios.post('http://localhost:5281/CheckBiometricData2FA', {
                favoriteColor: favoriteColor,
                height: height,
                username: localStorage.getItem("username")
              
        },
        { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
            .then(function (response) {
                console.log(response);
                window.location.href = '/panel'
                
            })
            .catch(function (error) {
              
                console.log(error);
            });
    };
    const logout = () => {
        //  localStorage.removeItem('token')
        window.location.href = '/'
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
                        name="FavoriteColor"
                        placeholder="FavoriteColor"
                        required=""
                        onChange={(e) =>setfavoriteColor(e.target.value)}
                        
                    />
                    <input
                        className={styles.profile_input}
                        type="text"
                        name="height"
                        placeholder="height"
                        required=""
                        onChange={(e) =>setheight(e.target.value)}
                    />


                    <button onClick={handle2fa} className={styles.profile_button} type="button">OK</button>
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

export default SecondLayer ;