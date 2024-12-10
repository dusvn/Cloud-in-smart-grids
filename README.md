# **Cloud in Smart Grids**

## **1. System Functions**  
- **Register**: Users can create a new account in the system.  
- **Login**: Existing users can log in using their credentials.  
- **Change Profile**: Users can update their profile information.  
- **New Post/Comment**: Users can create new posts or add comments to existing posts.  
- **Search Topic**: Users can search for specific topics.  
- **Sort Topic**: Users can sort topics according to certain criteria (e.g., most recent, most popular).  
- **Delete Post/Comment**: Users can delete their own posts or comments.  
- **Upvote/Downvote Topic**: Users can upvote or downvote topics to indicate their relevance or quality.  
- **Email Notifications**: Users receive email notifications when a new comment is made on a post they are subscribed to.  
- **Text Pagination**: Large amounts of text are divided into multiple pages for better readability and performance.  
- **Health Monitoring of System State (Real-Time)**: The system's state is monitored in real-time to ensure availability, fault detection, and system health tracking.  

---

## **2. System Architecture**  
![System Architecture](https://github.com/dusvn/Cloud-in-smart-grids/blob/main/image.png)  

---

## **3. Testing Commands**  
To simulate a server fault, use the following commands in **Command Prompt (Run as Administrator)**:  

### **3.1 View active connections**  
```bash
netstat -ano | findstr :<PORT_NUMBER>
```
### **3.2 Kill process by PID**
If you want to stop a specific process, you can use the following command:
```bash
taskkill /F /PID <PID>
```
### **3.3 Kill multiple processes by PID**
If there are multiple instances of a process that need to be stopped, you can combine multiple taskkill commands like this:
```bash
(taskkill /F /PID <PID1>) && (taskkill /F /PID <PID2>) && (taskkill /F /PID <PID3>)
```
