import { FiChevronLeft } from 'react-icons/fi';
import { useNavigate, useLocation } from "react-router-dom";
import { useState, useEffect } from 'react';
import { GetTopic } from '../Services/Topics';

export default function FullTopic() {
    const location = useLocation();
    const postId = location.state?.postId;
    console.log("This is post id:", postId);
    console.log(localStorage.getItem('userId'));
    const navigate = useNavigate();
    const apiEndpoint = "http://localhost:14543/topic/getFullTopic";
    const handleSignOut = () => {
        navigate('/Dashboard');
    };

    const [topic, setTopic] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await GetTopic(apiEndpoint, postId);
                setTopic(data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [apiEndpoint, postId]);

    return (
        <div style={{ display: 'flex', flexDirection: 'column', height: '100vh' }}>
            <div className="blackDashboar-header flex justify-between items-center p-4">
                <button
                    className="button-logout"
                    onClick={handleSignOut}
                    style={{ marginLeft: 'auto' }}
                >
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <FiChevronLeft size={25} style={{ marginRight: '4px' }} />
                        <span>Dashboard</span>
                    </div>
                </button>
            </div>
            <div style={{ flex: 1, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                {topic ? (
                    <div style={{ textAlign: 'center' }}>
                        <h1>{topic.Title}</h1>
                        <p>{topic.Text}</p>
                        {topic.Image && (
                            <img 
                                src={topic.Image} 
                                alt={topic.Title} 
                                style={{ maxWidth: '100%', maxHeight: '400px', margin: '20px 0' }} 
                            />
                        )}
                    </div>
                ) : (
                    <p>Loading...</p>
                )}
            </div>
        </div>
    );
}
