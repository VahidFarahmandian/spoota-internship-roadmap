import React from "react";
import ReactDom from "react-dom/client";
import Login from "./components/Login";
import Landing from "./components/Landing";
import Panel from "./components/Panel";

import AllSignUps from "./components/AllSignUps";
import SignUp2Fa from "./components/SignUp2Fa";
import SignUpOTP from "./components/SignUpOTP";
import SignUpOIDC from "./components/SignUpOIDC";
import AllLogins  from "./components/AllLogins";
import Login2fa from "./components/Login2fa";
import Sms  from "./components/Sms";
import Loginoicd from "./components/Loginoicd";
import Sms2fa from "./components/Sms2fa";
import SecondLayer from "./components/SecondLayer";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";




export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Landing />} />
        
        <Route path="/login" element={<Login />} />
        <Route path="/panel" element={<Panel />} />

        <Route path="/allsign" element={<AllSignUps />} />
        <Route path="/signup2fa" element={<SignUp2Fa />} />
       
        <Route path="/signupotp" element={<SignUpOTP />} />
        <Route path="/signupoidc" element={<SignUpOIDC />} />
        <Route path="/alllogins" element={<AllLogins />} />
        <Route path="/sms" element={<Sms />} />
        <Route path="/login2fa" element={<Login2fa />} />
        <Route path="/loginoicd" element={<Loginoicd />} />
        <Route path="/sms2fa" element={<Sms2fa />} />
        <Route path="/second" element={<SecondLayer />} />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}

const root = ReactDom.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
