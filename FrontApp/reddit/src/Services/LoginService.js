import axios from 'axios';

export async function LoginApiCall(email, password, apiEndpoint) {
    try {
        // Create a new FormData object
        const formData = new FormData();
        formData.append('email', email);
        formData.append('password', password);
  

        // Make the POST request with the FormData object
        const response = await axios.post(apiEndpoint, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        
        return response.data;
    } catch (error) {
        console.error('Error while calling API for login user:', error);
    }
}