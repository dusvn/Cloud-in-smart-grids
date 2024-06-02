import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../Style/Dashboard.css';
import { FaSignOutAlt } from 'react-icons/fa';
import { MdPerson } from 'react-icons/md';
import { GetAllMyPosts, GetOtherPosts,DeleteTopic } from '../Services/Topics';
import { FiChevronLeft } from 'react-icons/fi';
import { GetAllPosts,GetPostsByTitle,GetSortedPosts } from '../Services/Search';

export default function Search() {
    
    const apiSearchParam="http://localhost:14543/search/getByTitle";
    const apiSortParam="http://localhost:14543/search/sortAll";
    const apiGetAllParams="http://localhost:14543/search/getAll";



    const navigate = useNavigate();
    const[sortBtn,setSortBtn]=useState(true);
    const[allPosts,setAllPosts]=useState([])
    const [search, setsearch] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage] = useState(5);

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

    const handleViewFullTopic = (postId) => {
        navigate('/FullTopic' ,{ state: { postId: postId } });
    };

    const handleDash = () => {
        navigate('/Dashboard');
    };

    const handleSearchChange=(e)=>{

        const value = e.target.value;
        setsearch(value);
    
    }

    const handleSearch= async(e)=>{

        e.preventDefault();

        const data= await GetPostsByTitle(apiSearchParam,search);
        console.log("This is data:",data);
       
        if(data.length>0) setAllPosts(data);

        if(data.length==0) setAllPosts([]);
        
        setSortBtn(true);

    }

    const handleGetTop= async()=>{

        const data= await GetAllPosts(apiGetAllParams);
        console.log("This is data:",data);
       
      
        if(data.length>0){ 
            
            setAllPosts(data);
            setSortBtn(false)

        }
        if(data.length==0) {
            
            setAllPosts([]);
            setSortBtn(true);
        
        
        }

    }
    const handleSortTop= async()=>{

        const data= await GetSortedPosts(apiSortParam);
        console.log("This is data:",data);

        setAllPosts(data);
        setSortBtn(false);

    }

    

    return (
        <div style={{ display: 'flex', flexDirection: 'column', height: '100vh' }}>
            <div className="blackDashboar-header flex justify-between items-center p-4">
                
                <div  style={{ display: 'flex', flexDirection: 'row', width: '1000px',marginLeft:'460px' }}>
                    
                    <form onSubmit={handleSearch} method='post' style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', width: '100%' }}>
                        <div style={{ display: 'flex', flexDirection: 'row',alignItems: 'center', gap: '10px', width: '100%' }}>
                            <input style={{width:'300px'}} className="input-field" type="text" placeholder="Search..." onChange={handleSearchChange}/>
                            <button style={{ padding: '5px 10px', fontSize: '14px',marginBottom:'17px' }} className="search-button" type='submit'>
                                Search
                            </button>
                        </div>
                    </form>
                
                

                </div>
                

                <button
                    className="button-logout"
                    onClick={handleDash}
                    style={{ marginLeft: 'auto' }}
                >
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <FiChevronLeft size={25} style={{ marginRight: '4px' }} />
                        <span>Dashboard</span>
                    </div>
                </button>
            </div>
            <div style={{ display: 'flex', flexDirection: 'row', flex: 1, justifyContent: 'flex-start' }}>
                
                <div style={{ width: '25%', height: '100%', backgroundColor: 'rgb(220,220,220)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', alignItems: 'center', columnGap: '10px' }}></div>
                    <div style={{ width: '50%', height: '100%', backgroundColor: 'rgb(128,128,128)', display: 'flex', flexDirection: 'column', justifyContent: 'flex-start', columnGap: '10px' }}>
                        <h1 style={{ marginLeft: '20px', textDecoration: 'underline', width: '200px' }}>Search topics 
                        <button onClick={handleGetTop}>Get all topics</button>
                        <button disabled={sortBtn} onClick={handleSortTop}>Sort by topic name</button>
                        </h1>
                        <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                        <div style={{marginLeft:'20px'}}>
                            {allPosts.length > 0 ? (
                                paginate(allPosts).map((post, index) => (
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
