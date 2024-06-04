import axios from 'axios';

export async function AddNewTopic(topicName,topicText,topicPhoto,apiEndpoint,userId) {
    const topicData = new FormData();
    topicData.append('topicName', topicName);
    topicData.append('topicText', topicText);
    topicData.append('topicPhoto', topicPhoto);
    topicData.append('userId',userId);
    try {
        const response = await axios.post(apiEndpoint, topicData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data;
    } catch (error) {
        console.error('API call error:', error);
    }

}