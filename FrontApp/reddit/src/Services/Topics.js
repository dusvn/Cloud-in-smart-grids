import axios from 'axios';

export async function GetAllMyPosts(apiEndpoint,userId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                userId: userId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}

export async function GetOtherPosts(apiEndpoint,userId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                userId: userId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}

export async function GetTopic(apiEndpoint,postId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                postId: postId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}




export async function DeleteTopic(apiEndpoint,postId) {
    try {
        const response = await axios.delete(apiEndpoint, {
            params: {
                postId: postId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching comments:', error);
        throw error;
    }
}

