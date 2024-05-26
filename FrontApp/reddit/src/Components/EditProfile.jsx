import { FiChevronLeft } from 'react-icons/fi'; 
import { useNavigate } from "react-router-dom";
import { GetProfileInfo } from '../Services/EditProfileService';
import { useEffect, useState } from 'react';
import { changeUserFields } from '../Services/EditProfileService';
export default function EditProfile() {
    console.log(localStorage.getItem('userId'));
    const navigate = useNavigate();
    const getProfileInfoApi = "http://localhost:14543/user/getProfileInfo";
    const changeUserFieldsApi = "http://localhost:14543/user/changeFields";

    const [firstName,setFirstName] = useState();
    const [lastName,setLastName] = useState();
    const [city,setCity] = useState();
    const [address,setAddress] = useState();
    const [email,setEmail] = useState();
    const [password,setPassword] = useState(); // there is old pw 
    const [phoneNum,setPhoneNum] = useState();
    const [image,setImage] = useState();
    const [country,setCountry] = useState();
    const [isEditing,setIsEditing] = useState(false);
    
    const [newPassword,setNewPassword] = useState('');
    const [repeatNewPassword,setRepeatNewPassword] = useState('');
    const [oldPasswordRepeat,setOldPasswordRepeat] = useState('');
    const [initialUser, setInitialUser] = useState({});
    const [selectedFile, setSelectedFile] = useState(null);
    function makeImage(imageFile) {
        if (imageFile) {
            const byteCharacters = atob(imageFile);
            const byteNumbers = new Array(byteCharacters.length);
            for (let i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }
            const byteArray = new Uint8Array(byteNumbers);
            const blob = new Blob([byteArray], { type: 'image' });
            const url = URL.createObjectURL(blob);
            return url;
        }
    }

    const handleEditClick = () => {
        setIsEditing(true);
    };

    const handleImageChange = (e) => {
        const file = e.target.files[0];
        setSelectedFile(file);
        const reader = new FileReader();
        reader.onloadend = () => {
            setImage(reader.result);
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await GetProfileInfo(getProfileInfoApi,localStorage.getItem('userId'));
                setInitialUser(data.user);
                setFirstName(data.user.FirstName);
                setLastName(data.user.LastName);
                setCountry(data.user.Country);
                setEmail(data.user.Email);
                setCity(data.user.City);
                setAddress(data.user.Address);
                setImage(makeImage(data.user.Image))
                setPassword(data.user.Password);
                setPhoneNum(data.user.PhoneNum);
            } catch (error) {
                console.error('Error while calling api for profile info:', error);
            }
        };

        fetchData();
    }, []);


    const handleSignOut = () => {
        navigate('/Dashboard');
    };

     const handleCancelClick = () => {
        setIsEditing(false);

        setAddress(initialUser.Address);
        setEmail(initialUser.Email);
        setFirstName(initialUser.FirstName);
        setImage(makeImage(initialUser.Image));
        setLastName(initialUser.LastName);
        setPassword(initialUser.Password);
        setPhoneNum(initialUser.PhoneNum);
        setCountry(initialUser.Country);
        setPassword(initialUser.Password);
        setSelectedFile(null);
        setOldPasswordRepeat('');
        setNewPassword('');
        setRepeatNewPassword('');
    };

    const handleSaveClick = async () =>{
        const changedUser = await changeUserFields(changeUserFieldsApi,firstName,lastName,country,city,address,email,password,selectedFile,newPassword,repeatNewPassword,oldPasswordRepeat,localStorage.getItem('userId'),phoneNum)
        console.log("This is changed user:",changedUser);
        
        setInitialUser(changedUser);
        setFirstName(changedUser.FirstName);
        setCity(changedUser.City);
        setCountry(changedUser.Country);
        setEmail(changedUser.Email);
        setAddress(changedUser.Address);
        setLastName(changedUser.LastName);
        setPassword(changedUser.Password);
        setPhoneNum(changedUser.PhoneNum);
        setImage(makeImage(changedUser.Image));
        setOldPasswordRepeat('');
        setNewPassword('');
        setRepeatNewPassword('');
        setIsEditing(false);
        
   
    }

    console.log("This is initial user:",initialUser);

    return (
        <div style={{ display: 'flex', flexDirection: 'column', height: '100vh' }}>
            <div className="blackDashboar-header flex justify-between items-center p-4">
                <button
                    className="button-logout"
                    onClick={handleSignOut}
                    style={{ marginLeft: 'auto' }}  // This line pushes the button to the right
                >
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <FiChevronLeft size={25} style={{ marginRight: '4px' }} />
                        <span>Dashboard</span>
                    </div>
                </button>
            </div>
    
            <div style={{ width: '100%', backgroundColor: 'white' }}>
            <div>
                <div style={{marginLeft: '30px'}}>
                    <h1>Edit profile</h1>
                    </div>
                <div>
                    <img src={image} alt="User" style={{ width: '100px', height: '100px', marginLeft: '30px', marginTop: '20px', borderRadius: '50%' }} />
                </div>
                {isEditing ? (
                    <div  style={{ marginLeft: '30px', marginTop: '20px' }}>
                        <input type='file'  onChange={handleImageChange}/>
                    </div>
                ) : (
                    <div ></div>
                )}
                <div style={{ marginLeft: '30px', marginTop: '20px' }}>
                  
                    <hr/>
                    <div >First name</div>
                    {isEditing ? (
                        <input  type='text' value={firstName} onChange={(e) => setFirstName(e.target.value)} />
                    ) : (
                        <div >{firstName}</div>
                    )}
                    <hr/>
                    <div >Last name</div>
                    {isEditing ? (
                        <input  type='text' value={lastName} onChange={(e) => setLastName(e.target.value)} />
                    ) : (
                        <div >{lastName}</div>
                    )}
                    <hr/>
                    <div >Address</div>
                    {isEditing ? (
                        <input  type='text' value={address} onChange={(e) => setAddress(e.target.value)} />
                    ) : (
                        <div >{address}</div>
                    )}
                    <hr/>
                    <div >City</div>
                    {isEditing ? (
                        <input  type='text' value={city} onChange={(e) => setCity(e.target.value)} />
                    ) : (
                        <div >{city}</div>
                    )}
                    <hr/>
                    <div >Country</div>
                    {isEditing ? (
                        <input  type='text' value={country} onChange={(e) => setCountry(e.target.value)} />
                    ) : (
                        <div >{country}</div>
                    )}
                    <hr/>

                    <div >Email</div>
                    {isEditing ? (
                        <input  type='email' value={email} onChange={(e) => setEmail(e.target.value)} style={{ width: '250px' }} />
                    ) : (
                        <div >{email}</div>
                    )}
                    <hr/>
                    <div >Phone number</div>
                    {isEditing ? (
                        <input  type='text' value={phoneNum} onChange={(e) => setPhoneNum(e.target.value)} style={{ width: '250px' }} />
                    ) : (
                        <div >{phoneNum}</div>
                    )}
                    <hr/>
                    <div >Old password</div>
                    {isEditing ? (
                        <input  type='password' value={oldPasswordRepeat}  onChange={(e) => setOldPasswordRepeat(e.target.value)} />
                    ) : (
                        <div >
                            <input type='password' placeholder='********' disabled />
                        </div>
                    )}
                    <hr/>
                    <div >New password</div>
                    {isEditing ? (
                        <input  type='password' value={newPassword} onChange={(e) => setNewPassword(e.target.value)} />
                    ) : (
                        <div >
                            <input  type='password' placeholder='********' disabled />
                        </div>
                    )}
                    <hr/>
                    <div >Repeat new password</div>
                    {isEditing ? (
                        <input  type='password' value={repeatNewPassword} onChange={(e) => setRepeatNewPassword(e.target.value)} />
                    ) : (
                        <div >
                            <input  type='password' placeholder='********' disabled />
                        </div>
                    )}
                    <hr/>
                    <br />
                    {isEditing ? (
                        <div>
                            <button  onClick={handleSaveClick}>Save</button>
                            <button onClick={handleCancelClick}>Cancel</button>
                        </div>
                    ) : (
                        <div>
                            <button onClick={handleEditClick}>Edit</button>
                        </div>
                    )}
                </div>
            </div>
        </div>
        </div>
    );
}