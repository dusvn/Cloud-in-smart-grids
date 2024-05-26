import { FiChevronLeft } from 'react-icons/fi'; 
import { useNavigate } from "react-router-dom";
import { useState } from 'react';
import {AddNewTopic} from '../Services/NewTopic';
export default function NewTopic() {
    console.log(localStorage.getItem('userId'));
    const navigate = useNavigate();
    const changeUserFieldsApi = "http://localhost:14543/topic/newTopic";
    const handleSignOut = () => {
        navigate('/Dashboard');
    };

    const [title, setTitle] = useState('');
    const [text, setText] = useState('');
    const [photo, setPhoto] = useState(null);

    const handlePhotoChange = (event) => {
        setPhoto(event.target.files[0]);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if(title==='') alert("Type tittle!");
        else if(text==='') alert("Type text!");
        else{
        const data = await AddNewTopic(title,text,photo,changeUserFieldsApi,localStorage.getItem('userId'));
        console.log("This is data:",data);
        }
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
                        <FiChevronLeft size={25} style={{ marginRight: '4px' }} />
                        <span>Dashboard</span>
                    </div>
                </button>
            </div>
            <div style={{ flex: 1, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', width: '500px' }}>
                    <label style={{ fontWeight: 'bold' }}>
                        Topic Title:
                        <input 
                            type="text" 
                            value={title} 
                            onChange={(e) => setTitle(e.target.value)} 
                            style={{ padding: '8px', margin: '5px 0' }}
                        />
                    </label>
                    <label style={{ fontWeight: 'bold' }}>
                        Text:
                        <textarea 
                            value={text} 
                            onChange={(e) => setText(e.target.value)} 
                            style={{ padding: '8px', margin: '5px 0', height: '100px',width: '500px' }}
                        />
                    </label>
                    <label style={{ fontWeight: 'bold' }}>
                        Photo:
                        <input 
                            type="file" 
                            onChange={handlePhotoChange} 
                            style={{ padding: '8px', margin: '5px 0' }}
                        />
                    </label>
                    <button 
                        type="submit" 
                        style={{ padding: '10px', backgroundColor: 'rgb(255,69,0)', color: 'white', border: 'none', cursor: 'pointer',width: '500px'}}
                        

                    >
                        Submit
                    </button>
                </form>
            </div>
        </div>
    );
}
