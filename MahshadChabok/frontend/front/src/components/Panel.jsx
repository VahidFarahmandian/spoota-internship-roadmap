import React from 'react';
import { useState, useEffect } from 'react';
import axios from 'axios';

import styles from './../styles/Panel.module.css';
import styles2 from './../styles/Sidebar.module.css';
import styles3 from './../styles/Penelright.module.css';
import { Link } from "react-router-dom";

//TODO API for taking favorit laptop //done
//TODO API for taking user //done


const logout = () => {
    localStorage.removeItem('token')
    window.location.href = '/'
}






const Panel = () => {

    const [screenWidth, setScreenWidth] = useState(window.innerWidth);

  useEffect(() => {
    const handleResize = () => {
      setScreenWidth(window.innerWidth);
    };

    window.addEventListener('resize', handleResize);

    // Clean up the event listener when the component unmounts
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

    const [user, setuser] = useState({ Info: [] });
    const [laptop, setlaptop] = useState([]);

    if (!localStorage.getItem('token')) {
        window.location.href = '/'
    }

    const getuser = () => {
        axios.get("http://127.0.0.1:8088/users/profile", { headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` } })
            .then(res => {
                setuser(res.data)
                // console.log(res.data)
                // console.log(user)
            }
            )
            .catch(err => {
                console.log(err);
            })
    }

 
    useEffect(() => {
        getuser();
       
    }, []);

    useEffect(() => {
        console.log(laptop);
    }, []);

    const screenfalg = screenWidth < 1000;


    
    return (

        <>
            <div className={styles3.panel_right}>

                <h1 className={styles3.panel_rightfont}>Your favorite laptops</h1>

                <div className={styles2.sidebar}>
                    <Link to="/" className={styles2.sidebarLink}>
                        Home
                    </Link>

                    <p className={styles2.p_style}>Welcome {user.Info.name}</p>
                    {/* <p className={styles2.p_style}>Welcome Amirreza Ahmadi</p> */}

                    <button className={styles2.mybutton4} onClick={logout}>
                        Logout
                    </button>

                </div>

            </div>
           
        </>
    );
};

export default Panel;