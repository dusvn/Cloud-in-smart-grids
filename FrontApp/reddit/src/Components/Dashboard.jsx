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

    const handleAddNewTopic = () => {
        navigate('/NewTopic');
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
                </div>
                <div style={{ width: '25%', height: '100%', backgroundColor: '	rgb(220,220,220)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}>


                </div>
                <div style={{ width: '50%', height: '100%', backgroundColor: 'rgb(128,128,128)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', columnGap: '10px' }}>

                <h1 style={{marginLeft: '20px', textDecoration: 'underline', width:'200px'}}>Your topics</h1>
                <span><button className='button-logout' style={{marginLeft: '900px'}} onClick={handleAddNewTopic}>New topic</button></span>
                <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />

                <div>There is my topics</div>

                <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                <h1 style={{marginLeft: '20px', textDecoration: 'underline', width:'200px'}}>Topics</h1>
                <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                <div>
                    There is other topics
                </div>
                </div>
                <div style={{ width: '25%', height: '100%', backgroundColor: '	rgb(220,220,220)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}>
                </div>

            </div>
        </div>
    );
}
