import axios from 'axios';


export async function GetComments(apiEndpoint,userId,postId) {
    try {
        const response = await axios.get(apiEndpoint, {
            params: {
                userId: userId,
                postId:postId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching comments:', error);
        throw error;
    }
}



export async function DeleteComment(apiEndpoint,commentId) {
    try {
        const response = await axios.delete(apiEndpoint, {
            params: {
                commentId: commentId
            }
        });

        return response.data;
    } catch (error) {
        console.error('Error while fetching comments:', error);
        throw error;
    }
}






export async function AddComment(apiEndPoint,whoAdds,whichPost,comment)
{

    const topicData = new FormData();
    topicData.append('topicId', whichPost);
    topicData.append('userId', whoAdds);
    topicData.append('comment', comment);


    try {
        const response = await axios.post(apiEndPoint, topicData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data;
    } catch (error) {
        console.error('API call error:', error);
    }


}
