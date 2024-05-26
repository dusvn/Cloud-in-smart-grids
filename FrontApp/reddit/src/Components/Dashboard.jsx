import React from 'react';
import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import '../Style/Dashboard.css';
import { FaSignOutAlt } from 'react-icons/fa';
import { MdPerson } from 'react-icons/md';
import { FaBook } from 'react-icons/fa'; // Import the icon component
export default function Dashboard() {
    const navigate = useNavigate();
    console.log(localStorage.getItem('userId'));
    const handleSignOut = () => {
        localStorage.removeItem('userId');
        navigate('/');
    };

    const handleEditProfile = () => {
        navigate('/EditProfile');
    }

    return (
        <div style={{ display: 'flex', flexDirection: 'column', height: '100vh' }}>
            <div className="blackDashboar-header flex justify-between items-center p-4">
                <button
                    className="button-logout"
                    onClick={handleSignOut}
                    style={{ marginLeft: 'auto' }}  
                >
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <FaSignOutAlt size={25} style={{ marginRight: '4px' }} />
                        <span>Sign out</span>
                    </div>
                </button>
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', flex: 1, justifyContent: 'flex-start' }}>
                <div style={{ width: '20%', height: '100%', backgroundColor: 'rgb(255,69,0)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}>
                    <div>
                        <p style={{ color: "white", textAlign: "left", fontSize: "20px" }}></p>
                    </div>
                    <div>
                        <button className='button-logout' style={{ height: '45px', width: '180px' }} onClick={handleEditProfile}>
                            <div style={{ display: 'flex', alignItems: 'center' }}>
                                <MdPerson size={25} style={{ marginRight: '30px' }} />
                                <span>Profile</span>
                            </div>
                        </button>
                    </div>
                    <div style={{ marginTop: '30px' }}>
                        <button className='button-logout' style={{ height: '45px', width: '180px' }}>
                            <div style={{ display: 'flex', alignItems: 'center' }}>
                                <FaBook size={25} style={{ marginRight: '30px' }} />
                                <span>Topics</span>
                            </div>
                        </button>
                    </div>


                    <div style={{ flex: '70%', backgroundColor: 'white' }}>
                        {/* Right side content */}
                    </div>
                </div>

            </div>
        </div>
    );
}