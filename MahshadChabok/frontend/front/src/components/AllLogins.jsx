
import styles from "../styles/AllSignUps.module.css";
import React from "react";
import styles2 from "./../styles/ProfileSidebar.module.css";
import {Link} from "react-router-dom";

const AllLogins = () => {
    const logout = () => {
        //  localStorage.removeItem('token')
        window.location.href = '/'
    }

    return (
        <div className={styles.back} >
            <div className={styles.main2}>
                <h1 >All Login</h1>
                <h2 >Please Choose</h2>
                <div className={styles.button3}>
                    <div >
                        <a href="/login2fa">
                            <button >login 2fa</button>
                        </a>
                    </div>
                    <div >
                        <a  href="/loginoicd">
                            <button >login  oicd</button>
                        </a>
                    </div>
                    <div >
                        <a  href="/login">
                            <button >login  otp</button>
                        </a>
                    </div>
                </div>

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

export default AllLogins;