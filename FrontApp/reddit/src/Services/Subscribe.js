import axios from 'axios';


export async function GetSub(apiEndpoint,userId,postId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                userId: userId,
                postId: postId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}


export async function Subscribe(apiEndpoint,userId,postId) {
    try {
        const response = await axios.post(apiEndpoint, {}, {
            params: {
                userId: userId,
                postId: postId,
            },
            headers: {
                'Content-Type': 'application/json',
            },
        });

        console.log(response.data); // Handle the response
        return response.data;
    } catch (error) {
        console.error('Error while fetching comments:', error);
        throw error;
    }
}
