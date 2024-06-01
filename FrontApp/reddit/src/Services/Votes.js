import axios from 'axios';


export async function Upvote(apiEndpoint,post,user) {
    try {
        const response = await axios.post(apiEndpoint, {}, {
            params: {
                postId: post,
                userId: user,
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

export async function Downvote(apiEndpoint,post,user) {
    try {
        const response = await axios.post(apiEndpoint, {}, {
            params: {
                postId: post,
                userId: user,
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


export async function GetVotes(apiEndpoint,postId) {
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