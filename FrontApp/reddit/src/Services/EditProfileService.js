import axios from 'axios';

export async function GetProfileInfo(apiEndpoint,userId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                id: userId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}

export async function changeUserFieldsApiCall(apiEndpoint,formData){
    try {
        const response = await axios.put(apiEndpoint, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        
        });
        return response.data;
    } catch (error) {
        console.error('Error while calling API to change user fields:', error);
    }
}

export async function changeUserFields(apiEndpoint, firstName, lastName,country, city, address, email, password, imageUrl, newPassword, newPasswordRepeat, oldPasswordRepeat,id,phoneNumber) {
    const formData = new FormData();
    formData.append('FirstName', firstName);
    formData.append('LastName', lastName);
    formData.append('Address', address);
    formData.append('City',city);
    formData.append('Country',country)
    formData.append('Email', email);
    formData.append('PhoneNumber',phoneNumber)
    formData.append('Image', imageUrl); // probacu kao base64 string
    formData.append('NewPassword', newPassword);
    formData.append("Id",id);

    console.log("Password:",password);
    console.log("Old pw repeat",oldPasswordRepeat);
    console.log("New pw:",newPassword);
    console.log("New Pw repeat:",newPasswordRepeat);
    if(oldPasswordRepeat!=='' || newPasswordRepeat!=='' || newPassword!==''){
    if(checkNewPassword(password,oldPasswordRepeat,newPassword,newPasswordRepeat)){
            console.log("Succesfully entered new passwords");
            const dataOtherCall = await changeUserFieldsApiCall(apiEndpoint,formData);
            return dataOtherCall;
        }
    }else{
        const dataOtherCall2 = await changeUserFieldsApiCall(apiEndpoint,formData);
        return dataOtherCall2;
    }    
}

export function checkNewPassword(oldPassword,oldPasswordRepeat,newPassword,newPasswordRepeat) {
    if (oldPassword != oldPasswordRepeat) {
        alert("Old Password You Entered Was Incorrect");
        return false;
    }
    if (newPassword == newPasswordRepeat) return true;
    else {
        alert("New Passwords do NOT match");
        return false;
    }
}