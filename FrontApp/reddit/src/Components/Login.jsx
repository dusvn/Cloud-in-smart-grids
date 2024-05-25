import React from 'react';
import '../Style/LoginPage.css';
import { useState } from 'react';
import { Link } from 'react-router-dom';
import {useNavigate} from "react-router-dom";
import { LoginApiCall } from '../Services/LoginService';
export default function Login() {
  const [email, setEmail] = useState('');
  const [emailError, setEmailError] = useState(true);

  const [password, setPassword] = useState('');
  const [passwordError, setPasswordError] = useState(true);

  const apiEndpoint = "http://localhost:14543/user/login";
  const navigate = useNavigate();


  const handlePasswordChange = (e) => {
    const value = e.target.value;
    setPassword(value);
    const isValidPassword = /^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.{8,})/.test(value);
    if (value.trim() === '') {
      setPasswordError(true);
    } else if (!isValidPassword) {
      setPasswordError(true);
    } else if (!value.trim() === passwordError) {
      setPasswordError(true);
    }
    else {
      setPasswordError(false);
    }
  };

  const handleEmailChange = (e) => {
    const value = e.target.value;
    setEmail(value);
    const isValidEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
    if (value.trim() === '') {
      setEmailError(true);
    } else if (!isValidEmail) {
      setEmailError(true);
    } else {
      setEmailError(false);
    }
  };

  const handleLogin = async (e) =>{
    e.preventDefault();
   const data = await LoginApiCall(email,password,apiEndpoint);
   if(data.message == "Login successful"){
      navigate('/Dashboard');
   }
   console.log("Data from login",data);
  }

  return (
    <div className='font-serif'>
      <div className="black-header">
        <h1>Reddit</h1>
      </div>
      <div className="login-container flex justify-center items-center h-screen">

        <div className="login-form">
          <h3 className='text-4xl dark:text-white font-serif'>LOGIN</h3>
          <br></br>
          <form  method='post' onSubmit={handleLogin}>
            <input className="input-field" type="text" placeholder="Email" value={email} onChange={handleEmailChange} />
            <input className="input-field" type="password" placeholder="Password" value={password} onChange={handlePasswordChange} title='Passoword need 8 character one capital letter,number and special character' />
            <button className="login-button" type='submit'>Login</button>
          </form>
          <p className="signup-link">Don't have an account? &nbsp;
            <Link to="/Register" className="text-gray-800 font-bold">
              Register
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
}