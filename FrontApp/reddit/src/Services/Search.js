import axios from 'axios';

export async function GetAllPosts(apiEndpoint) {
    try {
        const response = await axios.get(apiEndpoint, {});

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}

export async function GetSortedPosts(apiEndpoint) {
    try {
        const response = await axios.get(apiEndpoint, {});

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}


export async function GetPostsByTitle(apiEndpoint,title) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                title: title
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching user info:', error);
        throw error;
    }
}