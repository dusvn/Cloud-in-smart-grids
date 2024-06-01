import { FiChevronLeft } from 'react-icons/fi';
import { useNavigate, useLocation } from "react-router-dom";
import { useState, useEffect } from 'react';
import { GetTopic } from '../Services/Topics';
import { GetComments,AddComment,DeleteComment} from '../Services/Comments';
import { Upvote,Downvote,GetVotes } from '../Services/Votes';
import { GetSub,Subscribe } from '../Services/Subscribe';
export default function FullTopic() {
    const location = useLocation();
    const postId = location.state?.postId;
    console.log("This is post id:", postId);
    console.log(localStorage.getItem('userId'));
    const navigate = useNavigate();
    const apiEndpoint = "http://localhost:14543/topic/getFullTopic";
    const apiCommentEndpoint="http://localhost:14543/comments/addComment";
    const apiGetCommentEndpoint="http://localhost:14543/comments/getComments";
    const apiDeleteCommentEndpoint="http://localhost:14543/comments/deleteComments";
    const apiUpvoteEndpoint="http://localhost:14543/vote/upVote";
    const apiDownvoteEndpoint="http://localhost:14543/vote/downVote";
    const apiGvoteEndpoint="http://localhost:14543/vote/getVote";
    const apiSubscribe="http://localhost:14543/subscribe/subscribeTopic";
    const apiGetSub="http://localhost:14543/subscribe/getSubscribe";
    const handleSignOut = () => {
        navigate('/Dashboard');
    };

    const [topic, setTopic] = useState(null);
    const [comments,setComments]=useState([]);
    const [text, setText] = useState('');
    const [votes,setVotes]=useState(null);

    const [subButton,setSubButton]=useState(null);
    

    const handleSubmit = async (event) => {
        event.preventDefault();
        if(text==='') alert("Type text!");
        else{

            localStorage.getItem('userId')
            const data = await AddComment(apiCommentEndpoint,localStorage.getItem('userId'),topic.TopicId,text);
            console.log("This is data:",data);
            if(data=="Successfuly added new topic!") window.location.reload();

        }
    };


    const handleViewFullTopic = async (comm)=>{

        const data = await DeleteComment(apiDeleteCommentEndpoint,comm);
            console.log("This is data:",data);
            if(data=="Deleted") window.location.reload();

    }
   
    const handleUpvote=async (post,user)=>{

        const data= await Upvote(apiUpvoteEndpoint,post,user);
        console.log("This is data:",data);
        
        if(data=="Alert") alert("You can't upvote,because you downvoted!");
        if(data=="Double") alert("You already voted");
        if(data=="Done") window.location.reload();

    }

    const handleDownvote=async (post,user)=>{

        const data= await Downvote(apiDownvoteEndpoint,post,user);
        console.log("This is data:",data);
        
        if(data=="Alert") alert("You can't downvote,because you upvoted!");
        if(data=="Double") alert("You already voted");
        if(data=="Done") window.location.reload();


    }


    const handleSubscribe = async(user,post)=>{

        const data= await Subscribe(apiSubscribe,user,post);
        console.log("This is data:",data);
       
        if(data=="Done") window.location.reload();


    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                
                
                const subData = await GetSub(apiGetSub,localStorage.getItem('userId'),postId);
                setSubButton(subData);

                

                console.log('My Posts:', subData);

            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

    fetchData();
    }, []);




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


    useEffect(() => {
        const fetchData = async () => {
            try {
                const userId = localStorage.getItem('userId');
                
                const commentsData = await GetComments(apiGetCommentEndpoint, userId,postId);
                setComments(commentsData);

                

                console.log('My Posts:', commentsData);

            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);


    useEffect(() => {
        const fetchData = async () => {
            try {
                
                
                const commentsData = await GetVotes(apiGvoteEndpoint,postId);
                setVotes(commentsData);

                

                console.log('My Posts:', commentsData);

            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);



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
            <div style={{ flex: 1, display: 'flex',flexDirection:'column', justifyContent: 'center', alignItems: 'center' }}>
            
                {topic ? (
                    <div style={{ textAlign: 'center' }}>
                        <div style={{ display: 'flex', gridTemplateColumns: 'auto auto', gap: '20px', alignItems: 'center' }}>
                            <div style={{ marginLeft:'200px',marginRight:'100px', display: 'flex',flexDirection:'row', justifyContent: 'center', alignItems: 'center'  }}>
                                <h1>{topic.Title}</h1>
                            </div>
                            <div style={{ display: 'flex',flexDirection:'row', justifyContent: 'center', alignItems: 'center'  }}>
                                <button disabled={subButton.IsMyPost} onClick={() => handleSubscribe(localStorage.getItem('userId'),postId)}>
                                    {subButton.IsSubscribed?"Unsubscribe":"Subscribe"}
                                </button>
                            </div>
                        </div>

                        <p>{topic.Text}</p>
                        {topic.Image && (
                            <img 
                                src={topic.Image} 
                                alt={topic.Title} 
                                style={{ maxWidth: '100%', maxHeight: '400px', margin: '20px 0' }} 
                            />
                        )}
                        
                        <button style={{ display: 'flex',alignItems:'center', flexDirection: 'column', gap: '10px', width: '90px' }}
                                onClick={() => handleUpvote(postId,localStorage.getItem('userId'))}>Upvote {votes?votes.Up:0}</button>
                        <button style={{ display: 'flex',alignItems:'center', flexDirection: 'column', gap: '10px', width: '90px' }}
                                onClick={() => handleDownvote(postId,localStorage.getItem('userId'))}>Downvote {votes?votes.Down:0}</button>

                        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', width: '500px' }}>
                        
                            <label style={{ fontWeight: 'bold' }}>
                                Type to comment:
                                <textarea 
                                    value={text} 
                                    onChange={(e) => setText(e.target.value)} 
                                    style={{ padding: '8px', margin: '5px 0', height: '100px',width: '500px' }}
                                />
                            </label>
                    
                    <button 
                        type="submit" 
                        style={{ padding: '10px', backgroundColor: 'rgb(255,69,0)', color: 'white', border: 'none', cursor: 'pointer',width: '500px'}}
                        

                    >
                        Add Comment
                    </button>
                </form>
                    </div>
                ) : (
                    <p>Loading post...</p>
                )}
                <hr style={{width:'50%'}}/>
                <p ><strong>Comments</strong></p>
                {comments.length > 0 ? (
                            comments.map((post, index) => (
                                <div key={index}>
                                    {/* <h2>{post.Title}</h2> */}
                                    <p><b>{post.UserName}</b></p>
                                    <p>{post.Comment}</p>
                                    {post.EnableRemove?(<button onClick={()=>handleViewFullTopic(post.CommentId)}>Remove comment</button>):''}
                                    <hr style={{ border: 'none', height: '1px', backgroundColor: '#333', width: '100%', margin: '10px 0' }} />
                                </div>
                            ))
                        ) : (
                            <p>No comments to display</p>
                        )}
            </div>
        </div>
    );
}
