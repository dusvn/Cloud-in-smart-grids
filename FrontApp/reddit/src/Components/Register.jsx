import React, { useEffect, useState } from 'react';
import '../Style/Register.css';
import { Link } from 'react-router-dom';
import RegisterApiCall, { DropErrorForField } from '../Services/RegisterService.js';
import { useNavigate } from 'react-router-dom';
export default function Register() {
    const apiEndpoint = "http://localhost:14543/user/register";
    const navigate = useNavigate();

    const [firstName, setFirstName] = useState('');
    const [firstNameError, setFirstNameError] = useState(true);

    const [lastName, setLastName] = useState('');
    const [lastNameError, setLastNameError] = useState(true);

    const [address, setAddress] = useState('');
    const [addressError, setAddressError] = useState(true);

    const [city, setCity] = useState('');
    const [cityError, setCityError] = useState(true);

    const [country, setCountry] = useState('');
    const [countryError, setCountryError] = useState(true);

    const [email, setEmail] = useState('');
    const [emailError, setEmailError] = useState(true);

    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(true);

    const [phoneNum,setPhoneNum] = useState('');
    const [phoneNumError,setPhoneNumError] = useState(true);

    const [imageUrl, setImageUrl] = useState(null);
    const [imageUrlError, setImageUrlError] = useState(true);

    //email clasic regex 
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
    // one big letter,1 special char and 8 chars is min
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
    //+381 621950549
    const handlePhoneNumChange = (e) => {
        const value = e.target.value;
        const isValidPhoneNumber = /^\+\d{12}$/.test(value);
        setPhoneNum(value);
        if (value.trim() === '') {
            setPhoneNumError(true);
        } else if (!isValidPhoneNumber) {
            setPhoneNumError(true);
        } else setPhoneNumError(false);
    };
    // not empty
    const handleFirstNameChange = (e) => {
        const value = e.target.value;
        setFirstName(value);
        if (value.trim() === '') {
            setFirstNameError(true);
        } else {
            setFirstNameError(false);
        }
    };
    //not empty
    const handleAddressChange = (e) => {
        const value = e.target.value;
        setAddress(value);
        if (value.trim() === '') {
            setAddressError(true);
        } else {
            setAddressError(false);
        }
    };
    // not empty
    const handleCityChange = (e) => {
        const value = e.target.value;
        setCity(value);
        if (value.trim() === '') {
            setCityError(true);
        } else {
            setCityError(false);
        }
    };
    //not empty
    const handleCountryChange = (e) => {
        const value = e.target.value;
        setCountry(value);
        if (value.trim() === '') {
            setCountryError(true);
        } else {
            setCountryError(false);
        }
    };
    // not empty 
    const handleLastNameChange = (e) => {
        const value = e.target.value;
        setLastName(value);
        if (value.trim() === '') {
            setLastNameError(true);
        } else {
            setLastNameError(false);
        }
    };
    const handleImageUrlChange = (e) => {
        const selectedFile = e.target.files[0];
    
        // Check if a file was actually selected
        if (!selectedFile ||!selectedFile.name) {
            setImageUrlError(true);
        } else {
            setImageUrl(selectedFile)
            setImageUrlError(false);
        }
    };
    
    
    const handleRegisterClick = (e) => {
        e.preventDefault();
    
        RegisterApiCall(
            firstNameError,
            lastNameError,
            cityError,
            addressError,
            countryError,
            emailError,
            passwordError,
            phoneNumError,
            imageUrlError,
            firstName,
            lastName,
            city,
            address,
            country,
            email,
            password,
            imageUrl,
            phoneNum,
            apiEndpoint
        )
        .then(data => {
            console.log("This is data:", data);
            if (data == "Successfully registered new user!") {
                navigate('/');
            } else {
                alert("User with this email already exists!");
            }
        })
        .catch(error => {
            console.error("An error occurred during registration:", error);
            alert("An error occurred during registration. Please try again later.");
        });
    };
    

    return (
    <div>
        <div className='black-header'>
            <h1>Reddit</h1>
        </div>
        <div className="register-container">
            <div className="register-form">
                <h3 className='text-4xl dark:text-white font-serif'>Registration</h3>
                <br></br>
                <div>
                    <form encType="multipart/form-data" method='post' onSubmit={handleRegisterClick}>
                        <div className="input-field">
                            <input
                                style={{ borderColor: firstNameError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="First Name"
                                value={firstName}
                                onChange={handleFirstNameChange}
                                required
                            />
                        </div>
                        <div className="input-field">
                            <input
                                style={{ borderColor: lastNameError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="Last Name"
                                value={lastName}
                                onChange={handleLastNameChange}
                                required
                            />
                        </div>
                        <div className="input-field ">
                            <input
                                style={{ borderColor: addressError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="Address"
                                value={address}
                                onChange={handleAddressChange}
                                required
                            />
                        </div>

                        <div className="input-field">
                            <input
                                style={{ borderColor: cityError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="City"
                                value={city}
                                onChange={handleCityChange}
                                required
                            />
                        </div>
                        <div className="input-field">
                            <input
                                style={{ borderColor: countryError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="Country"
                                value={country}
                                onChange={handleCountryChange}
                                required
                            />
                        </div>

                        <div className="input-field">
                            <input
                                style={{ borderColor: phoneNumError ? '#EF4444' : '#E5E7EB' }}
                                type="text"
                                placeholder="+381 XXXXXXXXX"
                                value={phoneNum}
                                onChange={handlePhoneNumChange}
                                required
                            />
                        </div>

                        <div className="input-field">
                            <input
                                style={{ borderColor: emailError ? '#EF4444' : '#E5E7EB' }}
                                type="email"
                                placeholder="example@example"
                                value={email}
                                onChange={handleEmailChange}
                                required
                            />
                        </div>

                        <div className="input-field ">
                            <input
                                style={{ borderColor: passwordError ? '#EF4444' : '#E5E7EB' }}
                                type="password"
                                title='Password needs 8 characters, one capital letter, one number, and one special character'
                                placeholder="Password"
                                value={password}
                                onChange={handlePasswordChange}
                                required
                            />
                        </div>
                        <div className="input-field">
                            <input type='file'
                                style={{ borderColor: imageUrlError ? '#EF4444' : '#E5E7EB' }}
                                onChange={handleImageUrlChange}
                                required
                            />
                        </div>
                        <div>
                            <button type='submit'>Register</button>
                        </div>
                        <p className="signup-link">Already have an account? &nbsp;
            <Link to="/" className="text-gray-800 font-bold">
              Login
            </Link>
          </p>
                    </form>
                </div>
            </div>
        </div>
    </div>
);
}