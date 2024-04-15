
import styles from "./../styles/Profile.module.css";
import styles2 from "./../styles/ProfileSidebar.module.css";
import React from "react";
import {Link} from "react-router-dom";

const Sms = () => {
    const logout = () => {
        //  localStorage.removeItem('token')
        window.location.href = '/'
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
                        name="number"
                        placeholder="number"
                        required=""
                        // value={profileState.name.value}
                        //  onChange={onChangeInput}
                    />
                    <button  className={styles.profile_button} type="button">OK</button>
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

export default Sms;