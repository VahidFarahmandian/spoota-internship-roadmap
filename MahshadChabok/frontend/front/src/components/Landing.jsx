import React from 'react';
import styles from "./../styles/Landing.module.css"
import styles2 from "../styles/Sidebar.module.css";
import {Link} from "react-router-dom";

const Landing = () => {
  
    return (    
        <div className={styles.back} >
            <div className={styles.main2}>
                <h1 >Welcome to our app</h1>
                <h2 >Please login or signup first.</h2>
                <div className={styles.button3}>
                    <div >
                        <a  href="/alllogins">
                            <button >All logins</button>
                        </a>
                    </div>
                    <div >
                        <a  href="/allsign">
                            <button >All SignUps</button>
                        </a>
                    </div>

                </div>
            </div>

            </div>

    );
};

export default Landing;
