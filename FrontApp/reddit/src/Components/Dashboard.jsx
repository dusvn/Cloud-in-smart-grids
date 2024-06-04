import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../Style/Dashboard.css';
import { FaSignOutAlt } from 'react-icons/fa';
import { MdPerson } from 'react-icons/md';
import { GetAllMyPosts, GetOtherPosts,DeleteTopic } from '../Services/Topics';

export default function Dashboard() {
    const navigate = useNavigate();
    const apiForMyPosts = "http://localhost:14543/topic/getMyPosts";
    const apiForOtherPosts = "http://localhost:14543/topic/getOtherPosts";
    const apiDeletePost= "http://localhost:14543/topic/deletePost";
    console.log(localStorage.getItem('userId'));

    const [myPosts, setMyPosts] = useState([]);
    const [otherPosts, setOtherPosts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage] = useState(5);

    const handleSignOut = () => {
        localStorage.removeItem('userId');
        navigate('/');
    };

    const handleEditProfile = () => {
        navigate('/EditProfile');
    }

    const handleToSearch = () => {
        navigate('/Search');
    }

    const handleAddNewTopic = () => {
        navigate('/NewTopic');
    }

    const handleViewFullTopic = (postId) => {
        navigate('/FullTopic' ,{ state: { postId: postId } });
    };

    const handleToAddTopic=()=>{

        navigate("/NewTopic");

    }

    const handleDeleteTopic= async (postId)=>{

        const data = await DeleteTopic(apiDeletePost,postId);
            console.log("This is data:",data);
            if(data=="Deleted") window.location.reload();

    }


    useEffect(() => {
        const fetchData = async () => {
            try {
                const userId = localStorage.getItem('userId');
                // Call GetAllMyPosts
                const myPostsData = await GetAllMyPosts(apiForMyPosts, userId);
                setMyPosts(myPostsData);

                // Call GetOtherPosts
                const otherPostsData = await GetOtherPosts(apiForOtherPosts, userId);
                setOtherPosts(otherPostsData);

                console.log('My Posts:', myPostsData);
                console.log('Other Posts:', otherPostsData);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    const truncateText = (text, maxLength) => {
        if (text.length <= maxLength) {
            return text;
        }
        return text.substring(0, maxLength) + '...';
    };

    const paginate = (posts) => {
        const indexOfLastPost = currentPage * postsPerPage;
        const indexOfFirstPost = indexOfLastPost - postsPerPage;
        return posts.slice(indexOfFirstPost, indexOfLastPost);
    };

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
                        <button className='button-logout' style={{ alignItems:'center',alignContent:'center',height: '45px', width: '180px' }} onClick={handleToAddTopic}>
                            <div style={{ display: 'flex',alignContent:'center', alignItems: 'center' }}>
                                <span style={{ marginRight: '50px' }} ></span>
                                <span style={{alignContent:'center'}}>Add Topic</span>
                            </div>
                        </button>
                        <button className='button-logout' style={{ alignItems:'center',alignContent:'center',height: '45px', width: '180px' }} onClick={handleToSearch}>
                            <div style={{ display: 'flex',alignContent:'center', alignItems: 'center' }}>
                                <span style={{ marginRight: '50px' }} ></span>
                                <span style={{alignContent:'center'}}>Search</span>
                            </div>
                        </button>
                    </div>
                </div>
                <div style={{ width: '25%', height: '100%', backgroundColor: 'rgb(220,220,220)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}></div>
                <div style={{ width: '50%', height: '100%', backgroundColor: 'rgb(128,128,128)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', columnGap: '10px' }}>
                    <h1 style={{ marginLeft: '20px', textDecoration: 'underline', width: '200px' }}>Your topics</h1>
                    {/* <span><button className='button-logout' style={{ marginLeft: '900px' }} onClick={handleAddNewTopic}>New topic</button></span> */}
                    {/* <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} /> */}
                    <div style={{marginLeft:'20px'}}>
                        {myPosts.length > 0 ? (
                            paginate(myPosts).map((post, index) => (
                                <div key={index}>
                                    {/* <h2>{post.Title}</h2> */}
                                    <p><b>{post.Title}</b></p>
                                    <p>{truncateText(post.Text, 100)}</p>
                                    <button onClick={() => handleViewFullTopic(post.TopicId)}>View Full Topic</button>
                                    <button onClick={() => handleDeleteTopic(post.TopicId)}>Remove Topic</button>
                                    <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                                </div>
                            ))
                        ) : (
                            <p>No posts to display</p>
                        )}
                    </div>
                    <h1 style={{ marginLeft: '20px', textDecoration: 'underline', width: '200px' }}>Topics</h1>
                    <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                    <div style={{marginLeft:'20px'}}>
                        {otherPosts.length > 0 ? (
                            paginate(otherPosts).map((post, index) => (
                                <div key={index}>
                                     <p><b>{post.Title}</b></p>
                                    <p>{truncateText(post.Text, 100)}</p>
                                    <button onClick={() => handleViewFullTopic(post.TopicId)}>View Full Topic</button>
                                    <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                                </div>
                            ))
                        ) : (
                            <p>No posts to display</p>
                        )}
                    </div>
                </div>
                <div style={{ width: '25%', height: '100%', backgroundColor: 'rgb(220,220,220)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}></div>
            </div>
        </div>
    );
}
